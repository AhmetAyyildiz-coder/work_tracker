using System;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using work_tracker.Data;
using work_tracker.Data.Entities;

namespace work_tracker.Forms
{
    public partial class DocumentLibraryForm : XtraForm
    {
        private WorkTrackerDbContext _context;

        public DocumentLibraryForm()
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
        }

        private void DocumentLibraryForm_Load(object sender, EventArgs e)
        {
            EnsureDefaultTags();
            LoadFilters();
            LoadDocuments();
            UpdateRepositoryPathLabel();
        }

        /// <summary>
        /// Döküman deposu yolunu gösterir
        /// </summary>
        private void UpdateRepositoryPathLabel()
        {
            string path = ConfigurationManager.AppSettings["DocumentRepositoryPath"];
            if (string.IsNullOrEmpty(path))
            {
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documents");
            }
            lblRepositoryPath.Text = $"📁 Döküman Deposu: {path}";
        }

        /// <summary>
        /// Döküman deposu klasörünü aç
        /// </summary>
        private void btnOpenRepository_Click(object sender, EventArgs e)
        {
            string path = ConfigurationManager.AppSettings["DocumentRepositoryPath"];
            if (string.IsNullOrEmpty(path))
            {
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documents");
            }

            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                Process.Start("explorer.exe", path);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Klasör açılamadı:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Varsayılan etiketlerin oluşturulmasını sağlar
        /// </summary>
        private void EnsureDefaultTags()
        {
            if (!_context.DocumentTags.Any())
            {
                var defaultTags = DocumentTag.GetDefaultTags();
                foreach (var tag in defaultTags)
                {
                    _context.DocumentTags.Add(tag);
                }
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Filtre combobox'larını yükler
        /// </summary>
        private void LoadFilters()
        {
            // Proje filtresi
            var projects = _context.Projects
                .Where(p => p.IsActive)
                .OrderBy(p => p.Name)
                .ToList();
            
            cmbProject.Properties.DataSource = projects;
            cmbProject.Properties.DisplayMember = "Name";
            cmbProject.Properties.ValueMember = "Id";
            cmbProject.Properties.NullText = "(Tüm Projeler)";
            cmbProject.Properties.Columns.Clear();
            cmbProject.Properties.Columns.Add(new LookUpColumnInfo("Name", "Proje"));

            // Etiket filtresi
            var tags = _context.DocumentTags.OrderBy(t => t.Name).ToList();
            cmbTag.Properties.DataSource = tags;
            cmbTag.Properties.DisplayMember = "Name";
            cmbTag.Properties.ValueMember = "Id";
            cmbTag.Properties.NullText = "(Tüm Etiketler)";
            cmbTag.Properties.Columns.Clear();
            cmbTag.Properties.Columns.Add(new LookUpColumnInfo("Name", "Etiket"));

            // Dosya türü filtresi
            cmbFileType.Properties.Items.Clear();
            cmbFileType.Properties.Items.AddRange(new string[] 
            { 
                "(Tümü)", "Word", "Excel", "PDF", "PowerPoint", "Text", "Image", "Archive", "Other" 
            });
            cmbFileType.SelectedIndex = 0;
        }

        /// <summary>
        /// Dökümanları yükler
        /// </summary>
        private void LoadDocuments()
        {
            var query = _context.DocumentReferences
                .Include(d => d.Project)
                .Include(d => d.Module)
                .Include(d => d.WorkItem)
                .Include(d => d.Tags)
                .Where(d => !d.IsArchived);

            // Arama filtresi
            var searchText = txtSearch.Text?.Trim();
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(d => 
                    d.Title.Contains(searchText) || 
                    d.Description.Contains(searchText) ||
                    d.FilePath.Contains(searchText));
            }

            // Proje filtresi
            if (cmbProject.EditValue != null)
            {
                int projectId = Convert.ToInt32(cmbProject.EditValue);
                query = query.Where(d => d.ProjectId == projectId);
            }

            // Etiket filtresi
            if (cmbTag.EditValue != null)
            {
                int tagId = Convert.ToInt32(cmbTag.EditValue);
                query = query.Where(d => d.Tags.Any(t => t.Id == tagId));
            }

            // Dosya türü filtresi
            var fileType = cmbFileType.Text;
            if (!string.IsNullOrEmpty(fileType) && fileType != "(Tümü)")
            {
                query = query.Where(d => d.FileType == fileType);
            }

            // Favori filtresi
            if (chkFavorites.Checked)
            {
                query = query.Where(d => d.IsFavorite);
            }

            // Önce veritabanından veriyi çek, sonra bellek içinde dönüşüm yap
            var rawDocuments = query
                .OrderByDescending(d => d.IsFavorite)
                .ThenByDescending(d => d.LastAccessedAt ?? d.CreatedAt)
                .Select(d => new
                {
                    d.Id,
                    d.Title,
                    d.FileType,
                    d.FilePath,
                    d.Description,
                    ProjectName = d.Project != null ? d.Project.Name : "-",
                    ModuleName = d.Module != null ? d.Module.Name : "-",
                    WorkItemTitle = d.WorkItem != null ? "WI-" + d.WorkItem.Id : "-",
                    TagNames = d.Tags.Select(t => t.Name),
                    d.IsFavorite,
                    d.CreatedAt,
                    d.LastAccessedAt
                })
                .ToList();

            // Bellek içinde Tags string'ini oluştur
            var documents = rawDocuments.Select(d => new
            {
                d.Id,
                d.Title,
                d.FileType,
                d.FilePath,
                d.Description,
                d.ProjectName,
                d.ModuleName,
                d.WorkItemTitle,
                Tags = string.Join(", ", d.TagNames),
                d.IsFavorite,
                d.CreatedAt,
                d.LastAccessedAt
            }).ToList();

            gridControl1.DataSource = documents;

            var view = gridControl1.MainView as GridView;
            if (view != null)
            {
                // Kolon başlıkları
                if (view.Columns["Id"] != null) view.Columns["Id"].Caption = "ID";
                if (view.Columns["Title"] != null)
                {
                    view.Columns["Title"].Caption = "Başlık";
                    view.Columns["Title"].Width = 200;
                }
                if (view.Columns["FileType"] != null)
                {
                    view.Columns["FileType"].Caption = "Tür";
                    view.Columns["FileType"].Width = 80;
                }
                if (view.Columns["FilePath"] != null)
                {
                    view.Columns["FilePath"].Caption = "Dosya Yolu";
                    view.Columns["FilePath"].Width = 300;
                }
                if (view.Columns["Description"] != null)
                {
                    view.Columns["Description"].Caption = "Açıklama";
                    view.Columns["Description"].Width = 200;
                }
                if (view.Columns["ProjectName"] != null)
                {
                    view.Columns["ProjectName"].Caption = "Proje";
                    view.Columns["ProjectName"].Width = 120;
                }
                if (view.Columns["ModuleName"] != null)
                {
                    view.Columns["ModuleName"].Caption = "Modül";
                    view.Columns["ModuleName"].Width = 100;
                }
                if (view.Columns["WorkItemTitle"] != null)
                {
                    view.Columns["WorkItemTitle"].Caption = "İş Kalemi";
                    view.Columns["WorkItemTitle"].Width = 80;
                }
                if (view.Columns["Tags"] != null)
                {
                    view.Columns["Tags"].Caption = "Etiketler";
                    view.Columns["Tags"].Width = 150;
                }
                if (view.Columns["IsFavorite"] != null)
                {
                    view.Columns["IsFavorite"].Caption = "⭐";
                    view.Columns["IsFavorite"].Width = 40;
                }
                if (view.Columns["CreatedAt"] != null)
                {
                    view.Columns["CreatedAt"].Caption = "Eklenme";
                    view.Columns["CreatedAt"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    view.Columns["CreatedAt"].DisplayFormat.FormatString = "dd.MM.yyyy";
                    view.Columns["CreatedAt"].Width = 80;
                }
                if (view.Columns["LastAccessedAt"] != null)
                {
                    view.Columns["LastAccessedAt"].Caption = "Son Erişim";
                    view.Columns["LastAccessedAt"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    view.Columns["LastAccessedAt"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
                    view.Columns["LastAccessedAt"].Width = 100;
                }

                view.OptionsView.ShowAutoFilterRow = true;
                view.OptionsView.ColumnAutoWidth = false;
            }

            lblStatus.Text = $"Toplam: {documents.Count} döküman";
        }

        /// <summary>
        /// Yeni döküman ekle
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new DocumentReferenceEditForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _context = new WorkTrackerDbContext();
                    LoadDocuments();
                }
            }
        }

        /// <summary>
        /// Seçili dökümanı düzenle
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view == null || view.FocusedRowHandle < 0) return;

            var id = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "Id"));

            using (var form = new DocumentReferenceEditForm(id))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _context = new WorkTrackerDbContext();
                    LoadDocuments();
                }
            }
        }

        /// <summary>
        /// Seçili dökümanı sil
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view == null || view.FocusedRowHandle < 0) return;

            var id = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "Id"));
            var title = view.GetRowCellValue(view.FocusedRowHandle, "Title")?.ToString();

            var result = XtraMessageBox.Show(
                $"'{title}' döküman referansını silmek istediğinizden emin misiniz?\n\n(Not: Orijinal dosya silinmeyecek, sadece referans kaldırılacak)",
                "Silme Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var doc = _context.DocumentReferences.Find(id);
                    if (doc != null)
                    {
                        doc.Tags.Clear();
                        _context.DocumentReferences.Remove(doc);
                        _context.SaveChanges();
                        LoadDocuments();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Silme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Seçili dökümanı aç
        /// </summary>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenSelectedDocument();
        }

        /// <summary>
        /// Grid'de çift tıklama ile dökümanı aç
        /// </summary>
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            OpenSelectedDocument();
        }

        /// <summary>
        /// Seçili dökümanı varsayılan uygulama ile açar
        /// </summary>
        private void OpenSelectedDocument()
        {
            var view = gridControl1.MainView as GridView;
            if (view == null || view.FocusedRowHandle < 0) return;

            var id = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "Id"));
            var filePath = view.GetRowCellValue(view.FocusedRowHandle, "FilePath")?.ToString();

            if (string.IsNullOrEmpty(filePath))
            {
                XtraMessageBox.Show("Dosya yolu bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Dosya varlığını kontrol et
            if (!File.Exists(filePath))
            {
                var result = XtraMessageBox.Show(
                    $"Dosya bulunamadı:\n{filePath}\n\nYeni konum seçmek ister misiniz?",
                    "Dosya Bulunamadı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    using (var openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Title = "Dökümanın Yeni Konumunu Seçin";
                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Yeni yolu kaydet
                            var doc = _context.DocumentReferences.Find(id);
                            if (doc != null)
                            {
                                doc.FilePath = openFileDialog.FileName;
                                doc.FileType = DocumentReference.GetFileType(openFileDialog.FileName);
                                _context.SaveChanges();
                                LoadDocuments();
                                filePath = openFileDialog.FileName;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else
                {
                    return;
                }
            }

            try
            {
                // Son erişim tarihini güncelle
                var doc = _context.DocumentReferences.Find(id);
                if (doc != null)
                {
                    doc.LastAccessedAt = DateTime.Now;
                    _context.SaveChanges();
                }

                // Dosyayı varsayılan uygulama ile aç
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Dosya açılamadı:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Favori durumunu değiştir
        /// </summary>
        private void btnToggleFavorite_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view == null || view.FocusedRowHandle < 0) return;

            var id = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "Id"));

            var doc = _context.DocumentReferences.Find(id);
            if (doc != null)
            {
                doc.IsFavorite = !doc.IsFavorite;
                _context.SaveChanges();
                LoadDocuments();
            }
        }

        /// <summary>
        /// Etiket yönetim formunu aç
        /// </summary>
        private void btnManageTags_Click(object sender, EventArgs e)
        {
            using (var form = new DocumentTagManagementForm())
            {
                form.ShowDialog();
                _context = new WorkTrackerDbContext();
                LoadFilters();
            }
        }

        /// <summary>
        /// Yenile
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _context = new WorkTrackerDbContext();
            LoadFilters();
            LoadDocuments();
        }

        /// <summary>
        /// Filtreleri temizle
        /// </summary>
        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            cmbProject.EditValue = null;
            cmbTag.EditValue = null;
            cmbFileType.SelectedIndex = 0;
            chkFavorites.Checked = false;
            LoadDocuments();
        }

        /// <summary>
        /// Filtre değiştiğinde
        /// </summary>
        private void Filter_Changed(object sender, EventArgs e)
        {
            LoadDocuments();
        }

        /// <summary>
        /// Arama kutusunda Enter'a basıldığında
        /// </summary>
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadDocuments();
            }
        }

        /// <summary>
        /// Dosya konumunu aç (klasörü göster)
        /// </summary>
        private void btnOpenLocation_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view == null || view.FocusedRowHandle < 0) return;

            var filePath = view.GetRowCellValue(view.FocusedRowHandle, "FilePath")?.ToString();

            if (string.IsNullOrEmpty(filePath))
            {
                XtraMessageBox.Show("Dosya yolu bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (File.Exists(filePath))
                {
                    // Dosyayı Windows Explorer'da seçili olarak göster
                    Process.Start("explorer.exe", $"/select,\"{filePath}\"");
                }
                else
                {
                    var directory = Path.GetDirectoryName(filePath);
                    if (Directory.Exists(directory))
                    {
                        Process.Start("explorer.exe", directory);
                    }
                    else
                    {
                        XtraMessageBox.Show($"Klasör bulunamadı:\n{directory}", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Klasör açılamadı:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
