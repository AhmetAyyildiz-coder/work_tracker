using System;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using work_tracker.Data;
using work_tracker.Data.Entities;
using work_tracker.Helpers;

namespace work_tracker.Forms
{
    public partial class WikiForm : XtraForm
    {
        private WorkTrackerDbContext _context;
        private int? _selectedPageId;
        private bool _isEditing = false;

        private int? _linkedWorkItemId;

        public WikiForm()
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
        }

        /// <summary>
        /// İş öğesinden Wiki sayfası oluşturmak için başlangıç değerlerini ayarlar
        /// </summary>
        public void InitializeForWorkItem(WorkItem workItem)
        {
            if (workItem == null) return;

            _linkedWorkItemId = workItem.Id;

            // Yeni sayfa moduna geç
            _selectedPageId = null;
            
            // Varsayılan başlık
            txtTitle.Text = $"WI-{workItem.Id} - {workItem.Title}";
            
            // Varsayılan özet
            txtSummary.Text = $"{workItem.Type} | {workItem.Status} | Proje: {workItem.Project?.Name ?? "-"}";

            // Varsayılan içerik şablonu
            var safeTitle = workItem.Title?.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;") ?? "";
            var templateHtml = $@"
<h1>WI-{workItem.Id} - {safeTitle}</h1>

<table border='1' cellpadding='5' style='border-collapse: collapse; margin-bottom: 15px;'>
  <tr><td><b>Durum</b></td><td>{workItem.Status}</td></tr>
  <tr><td><b>Tip</b></td><td>{workItem.Type}</td></tr>
  <tr><td><b>Proje</b></td><td>{workItem.Project?.Name ?? "-"}</td></tr>
  <tr><td><b>Modül</b></td><td>{workItem.Module?.Name ?? "-"}</td></tr>
  <tr><td><b>Oluşturma</b></td><td>{workItem.CreatedAt:dd.MM.yyyy}</td></tr>
</table>

<h2>Özet</h2>
<p>(Bu işi neden yaptım? Kısa açıklama.)</p>

<h2>Teknik Detaylar</h2>
<p>(Burada gerçek çözümü, kullanılan pattern'leri, önemli kod parçalarını yaz.)</p>

<h2>Sonuç / Notlar</h2>
<p>(İleride kendime bırakmak istediğim notlar.)</p>
";

            richEditContent.HtmlText = templateHtml;
            lblInfo.Text = "";

            // Düzenleme modunu aç
            LoadParentPages();
            SetEditMode(true);
            txtTitle.Focus();
        }

        private void WikiForm_Load(object sender, EventArgs e)
        {
            LoadPages();
            SetEditMode(false);

            // RichEdit görünümünü iyileştir
            if (richEditContent != null)
            {
                richEditContent.ActiveViewType = RichEditViewType.PrintLayout;
                richEditContent.ActiveView.ZoomFactor = 1.1f;
                richEditContent.Options.HorizontalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
                
                // Hyperlink tıklama olayını bağla
                richEditContent.HyperlinkClick += RichEditContent_HyperlinkClick;
            }
        }

        private void LoadPages()
        {
            var pages = _context.WikiPages
                .Where(p => !p.IsArchived)
                .OrderBy(p => p.ParentPageId.HasValue ? 1 : 0)
                .ThenBy(p => p.Title)
                .Select(p => new
                {
                    p.Id,
                    p.Title,
                    p.Summary,
                    ParentTitle = p.ParentPage != null ? p.ParentPage.Title : "-",
                    p.UpdatedAt,
                    p.UpdatedBy
                })
                .ToList();

            gridControl1.DataSource = pages;

            var view = gridControl1.MainView as GridView;
            if (view != null)
            {
                view.BestFitColumns();
                view.OptionsView.ShowAutoFilterRow = true;

                if (view.Columns["Id"] != null) view.Columns["Id"].Caption = "ID";
                if (view.Columns["Title"] != null) 
                {
                    view.Columns["Title"].Caption = "Başlık";
                    view.Columns["Title"].Width = 250;
                }
                if (view.Columns["Summary"] != null)
                {
                    view.Columns["Summary"].Caption = "Özet";
                    view.Columns["Summary"].Width = 300;
                }
                if (view.Columns["ParentTitle"] != null) view.Columns["ParentTitle"].Caption = "Üst Sayfa";
                if (view.Columns["UpdatedAt"] != null) 
                {
                    view.Columns["UpdatedAt"].Caption = "Son Güncelleme";
                    view.Columns["UpdatedAt"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    view.Columns["UpdatedAt"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
                }
                if (view.Columns["UpdatedBy"] != null) view.Columns["UpdatedBy"].Caption = "Güncelleyen";

                if (view.RowCount > 0)
                {
                    view.FocusedRowHandle = 0;
                    var firstId = (int)view.GetRowCellValue(0, "Id");
                    LoadPageDetails(firstId);
                }
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var view = sender as GridView;
            if (view == null) return;

            if (e.FocusedRowHandle >= 0)
            {
                var idObj = view.GetRowCellValue(e.FocusedRowHandle, "Id");
                if (idObj != null && int.TryParse(idObj.ToString(), out var id))
                {
                    _selectedPageId = id;
                    LoadPageDetails(id);
                }
            }
        }

        private void LoadPageDetails(int pageId)
        {
            var page = _context.WikiPages.Find(pageId);
            if (page != null)
            {
                txtTitle.Text = page.Title;
                txtSummary.Text = page.Summary;
                richEditContent.HtmlText = page.ContentHtml ?? "";
                cmbParentPage.EditValue = page.ParentPageId;

                lblInfo.Text = $"Oluşturan: {page.CreatedBy} ({page.CreatedAt:dd.MM.yyyy HH:mm})";
                if (page.UpdatedBy != null)
                {
                    lblInfo.Text += $" | Son Güncelleme: {page.UpdatedBy} ({page.UpdatedAt:dd.MM.yyyy HH:mm})";
                }
            }
        }

        private void LoadParentPages()
        {
            var pages = _context.WikiPages
                .Where(p => !p.IsArchived)
                .OrderBy(p => p.Title)
                .ToList();

            cmbParentPage.Properties.DataSource = pages;
            cmbParentPage.Properties.DisplayMember = "Title";
            cmbParentPage.Properties.ValueMember = "Id";
            cmbParentPage.Properties.NullText = "(Üst Sayfa Yok)";

            cmbParentPage.Properties.Columns.Clear();
            cmbParentPage.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Title", "Sayfa Başlığı"));
        }

        private void SetEditMode(bool editing)
        {
            _isEditing = editing;
            txtTitle.Enabled = editing;
            txtSummary.Enabled = editing;
            richEditContent.ReadOnly = !editing;
            cmbParentPage.Enabled = editing;

            btnNew.Enabled = !editing;
            btnEdit.Enabled = !editing && _selectedPageId.HasValue;
            btnSave.Enabled = editing;
            btnCancel.Enabled = editing;
            btnDelete.Enabled = !editing && _selectedPageId.HasValue;

            panelToolbar.Enabled = editing;
        }

        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _selectedPageId = null;
            txtTitle.Text = "";
            txtSummary.Text = "";
            richEditContent.HtmlText = "";
            cmbParentPage.EditValue = null;
            lblInfo.Text = "";

            LoadParentPages();
            SetEditMode(true);
            txtTitle.Focus();
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!_selectedPageId.HasValue)
            {
                XtraMessageBox.Show("Lütfen bir sayfa seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadParentPages();
            SetEditMode(true);
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                XtraMessageBox.Show("Başlık boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return;
            }

            try
            {
                if (_selectedPageId.HasValue)
                {
                    var page = _context.WikiPages.Find(_selectedPageId.Value);
                    if (page != null)
                    {
                        page.Title = txtTitle.Text.Trim();
                        page.Summary = txtSummary.Text?.Trim();
                        page.ContentHtml = richEditContent.HtmlText;
                        page.ParentPageId = cmbParentPage.EditValue as int?;
                        page.UpdatedBy = Environment.UserName;
                        page.UpdatedAt = DateTime.Now;
                    }
                }
                else
                {
                    var newPage = new WikiPage
                    {
                        Title = txtTitle.Text.Trim(),
                        Summary = txtSummary.Text?.Trim(),
                        ContentHtml = richEditContent.HtmlText,
                        ParentPageId = cmbParentPage.EditValue as int?,
                        WorkItemId = _linkedWorkItemId,
                        CreatedBy = Environment.UserName,
                        CreatedAt = DateTime.Now
                    };
                    _context.WikiPages.Add(newPage);
                    _context.SaveChanges();
                    _selectedPageId = newPage.Id;
                    _linkedWorkItemId = null; // Reset after save
                }

                _context.SaveChanges();
                LoadPages();
                SetEditMode(false);
                XtraMessageBox.Show("Sayfa kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Kayıt sırasında hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_selectedPageId.HasValue)
            {
                LoadPageDetails(_selectedPageId.Value);
            }
            else
            {
                txtTitle.Text = "";
                txtSummary.Text = "";
                richEditContent.HtmlText = "";
                cmbParentPage.EditValue = null;
            }
            SetEditMode(false);
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!_selectedPageId.HasValue)
            {
                XtraMessageBox.Show("Lütfen bir sayfa seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = XtraMessageBox.Show(
                "Bu wiki sayfasını arşivlemek istediğinize emin misiniz?",
                "Onay",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var page = _context.WikiPages.Find(_selectedPageId.Value);
                    if (page != null)
                    {
                        page.IsArchived = true;
                        _context.SaveChanges();
                        LoadPages();
                        XtraMessageBox.Show("Sayfa arşivlendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Arşivleme sırasında hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadPages();
            if (_selectedPageId.HasValue)
            {
                LoadPageDetails(_selectedPageId.Value);
            }
        }

        #region RichEdit Formatting Buttons

        private void btnBold_Click(object sender, EventArgs e)
        {
            if (richEditContent == null || richEditContent.ReadOnly)
                return;

            var doc = richEditContent.Document;
            var range = doc.Selection;
            if (range.Length == 0)
                return;

            var cp = doc.BeginUpdateCharacters(range);
            try
            {
                cp.Bold = !cp.Bold;
            }
            finally
            {
                doc.EndUpdateCharacters(cp);
            }
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            if (richEditContent == null || richEditContent.ReadOnly)
                return;

            var doc = richEditContent.Document;
            var range = doc.Selection;
            if (range.Length == 0)
                return;

            var cp = doc.BeginUpdateCharacters(range);
            try
            {
                cp.Italic = !cp.Italic;
            }
            finally
            {
                doc.EndUpdateCharacters(cp);
            }
        }

        private void btnBulletList_Click(object sender, EventArgs e)
        {
            XtraMessageBox.Show("Madde işaretli liste için RichEdit'in kendi kısayollarını kullanabilirsiniz:\nCtrl+Shift+L", 
                "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnNumberedList_Click(object sender, EventArgs e)
        {
            XtraMessageBox.Show("Numaralı liste için RichEdit'in kendi kısayollarını kullanabilirsiniz.", 
                "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnInsertWorkItemLink_Click(object sender, EventArgs e)
        {
            InsertWorkItemLink();
        }

        private void btnInsertWikiLink_Click(object sender, EventArgs e)
        {
            InsertWikiLink();
        }

        private void btnInsertUrlLink_Click(object sender, EventArgs e)
        {
            InsertUrlLink();
        }

        #endregion

        #region Hyperlink İşlemleri

        /// <summary>
        /// RichEdit içindeki hyperlink'e tıklandığında çağrılır
        /// </summary>
        private void RichEditContent_HyperlinkClick(object sender, HyperlinkClickEventArgs e)
        {
            var uri = e.Hyperlink.NavigateUri;
            
            if (string.IsNullOrEmpty(uri))
                return;

            // HyperlinkHelper ile işle
            bool handled = HyperlinkHelper.HandleLinkClick(uri, this);
            
            if (handled)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Seçili metni İş Öğesi linkine çevirir (#123 formatı)
        /// </summary>
        public void InsertWorkItemLink()
        {
            if (richEditContent == null || richEditContent.ReadOnly)
                return;

            // Kullanıcıdan iş ID'si al
            string input = XtraInputBox.Show("İş öğesi numarasını girin:", "İş Öğesi Linki Ekle", "");
            
            if (string.IsNullOrWhiteSpace(input))
                return;

            if (!int.TryParse(input.Replace("#", ""), out int workItemId))
            {
                XtraMessageBox.Show("Geçerli bir numara girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // İş öğesinin var olup olmadığını kontrol et
            var workItem = _context.WorkItems.Find(workItemId);
            if (workItem == null)
            {
                XtraMessageBox.Show($"#{workItemId} numaralı iş öğesi bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hyperlink ekle
            var doc = richEditContent.Document;
            var pos = doc.Selection.End;
            
            string linkText = $"#{workItemId} - {workItem.Title}";
            string linkUri = $"workitem:{workItemId}";
            
            var insertedRange = doc.InsertText(pos, linkText);
            var hyperlink = doc.Hyperlinks.Create(insertedRange);
            hyperlink.NavigateUri = linkUri;
            hyperlink.ToolTip = $"İş Öğesi: {workItem.Title}";
        }

        /// <summary>
        /// Seçili metni Wiki sayfası linkine çevirir ([[Sayfa]] formatı)
        /// </summary>
        public void InsertWikiLink()
        {
            if (richEditContent == null || richEditContent.ReadOnly)
                return;

            // Mevcut wiki sayfalarını göster
            var pages = _context.WikiPages
                .Where(p => !p.IsArchived)
                .OrderBy(p => p.Title)
                .Select(p => new { p.Id, p.Title })
                .ToList();

            if (pages.Count == 0)
            {
                XtraMessageBox.Show("Henüz wiki sayfası bulunmuyor!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Basit seçim için ComboBox dialog
            using (var form = new XtraForm())
            {
                form.Text = "Wiki Sayfası Seç";
                form.Size = new System.Drawing.Size(400, 150);
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;

                var lbl = new LabelControl { Text = "Sayfa:", Location = new System.Drawing.Point(20, 25) };
                var cmb = new LookUpEdit 
                { 
                    Location = new System.Drawing.Point(80, 22), 
                    Width = 280 
                };
                cmb.Properties.DataSource = pages;
                cmb.Properties.DisplayMember = "Title";
                cmb.Properties.ValueMember = "Id";
                cmb.Properties.NullText = "(Sayfa Seçin)";
                cmb.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Title", "Başlık"));

                var btnOk = new SimpleButton { Text = "Ekle", Location = new System.Drawing.Point(180, 70), Width = 80, DialogResult = DialogResult.OK };
                var btnCancel = new SimpleButton { Text = "İptal", Location = new System.Drawing.Point(280, 70), Width = 80, DialogResult = DialogResult.Cancel };

                form.Controls.AddRange(new Control[] { lbl, cmb, btnOk, btnCancel });
                form.AcceptButton = btnOk;
                form.CancelButton = btnCancel;

                if (form.ShowDialog(this) == DialogResult.OK && cmb.EditValue != null)
                {
                    var selectedId = (int)cmb.EditValue;
                    var selectedPage = pages.First(p => p.Id == selectedId);

                    // Hyperlink ekle
                    var doc = richEditContent.Document;
                    var pos = doc.Selection.End;

                    string linkText = $"📄 {selectedPage.Title}";
                    string linkUri = $"wiki:{Uri.EscapeDataString(selectedPage.Title)}";

                    var insertedRange = doc.InsertText(pos, linkText);
                    var hyperlink = doc.Hyperlinks.Create(insertedRange);
                    hyperlink.NavigateUri = linkUri;
                    hyperlink.ToolTip = $"Wiki: {selectedPage.Title}";
                }
            }
        }

        /// <summary>
        /// Seçili metni URL linkine çevirir
        /// </summary>
        public void InsertUrlLink()
        {
            if (richEditContent == null || richEditContent.ReadOnly)
                return;

            var doc = richEditContent.Document;
            var selection = doc.Selection;
            string selectedText = doc.GetText(selection);

            // URL'yi al
            string url = XtraInputBox.Show("URL adresini girin:", "Link Ekle", 
                selectedText.StartsWith("http") ? selectedText : "https://");

            if (string.IsNullOrWhiteSpace(url))
                return;

            // URL formatını kontrol et
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                url = "https://" + url;

            // Link metnini al
            string linkText = string.IsNullOrEmpty(selectedText) 
                ? XtraInputBox.Show("Link metni:", "Link Ekle", url) 
                : selectedText;

            if (string.IsNullOrWhiteSpace(linkText))
                linkText = url;

            // Seçili metni sil ve yeni link ekle
            if (selection.Length > 0)
                doc.Delete(selection);

            var pos = doc.Selection.End;
            var insertedRange = doc.InsertText(pos, linkText);
            var hyperlink = doc.Hyperlinks.Create(insertedRange);
            hyperlink.NavigateUri = url;
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Belirtilen başlıkla eşleşen wiki sayfasını arar ve gösterir.
        /// [[Sayfa Adı]] formatındaki referanslar için kullanılır.
        /// </summary>
        /// <param name="searchTitle">Aranacak sayfa başlığı</param>
        public void SearchAndNavigate(string searchTitle)
        {
            if (string.IsNullOrEmpty(searchTitle))
                return;

            // Form yüklendiğinde arama yap
            this.Load += (s, e) =>
            {
                var view = gridControl1.MainView as GridView;
                if (view == null) return;

                // Başlığa göre ara (case-insensitive, kısmi eşleşme)
                for (int i = 0; i < view.RowCount; i++)
                {
                    var title = view.GetRowCellValue(i, "Title")?.ToString();
                    if (title != null && title.IndexOf(searchTitle, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        view.FocusedRowHandle = i;
                        var id = (int)view.GetRowCellValue(i, "Id");
                        LoadPageDetails(id);
                        return;
                    }
                }

                // Bulunamadıysa yeni sayfa oluşturma önerisi
                var result = XtraMessageBox.Show(
                    $"'{searchTitle}' başlıklı wiki sayfası bulunamadı.\n\nBu başlıkla yeni bir sayfa oluşturmak ister misiniz?",
                    "Sayfa Bulunamadı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _selectedPageId = null;
                    txtTitle.Text = searchTitle;
                    txtSummary.Text = "";
                    richEditContent.HtmlText = $"<h1>{System.Net.WebUtility.HtmlEncode(searchTitle)}</h1><p></p>";
                    cmbParentPage.EditValue = null;
                    lblInfo.Text = "";
                    LoadParentPages();
                    SetEditMode(true);
                    txtTitle.Focus();
                }
            };
        }
    }
}

