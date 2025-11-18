using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit;
using work_tracker.Data;
using work_tracker.Data.Entities;

namespace work_tracker.Forms
{
    public partial class WikiForm : XtraForm
    {
        private WorkTrackerDbContext _context;
        private int? _selectedPageId;
        private bool _isEditing = false;

        public WikiForm()
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
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
                        CreatedBy = Environment.UserName,
                        CreatedAt = DateTime.Now
                    };
                    _context.WikiPages.Add(newPage);
                    _context.SaveChanges();
                    _selectedPageId = newPage.Id;
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

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

