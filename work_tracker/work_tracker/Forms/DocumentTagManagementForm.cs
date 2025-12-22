using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using work_tracker.Data;
using work_tracker.Data.Entities;

namespace work_tracker.Forms
{
    public partial class DocumentTagManagementForm : XtraForm
    {
        private WorkTrackerDbContext _context;

        public DocumentTagManagementForm()
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
        }

        private void DocumentTagManagementForm_Load(object sender, EventArgs e)
        {
            LoadTags();
        }

        private void LoadTags()
        {
            var tags = _context.DocumentTags
                .OrderBy(t => t.Name)
                .Select(t => new
                {
                    t.Id,
                    t.Name,
                    t.Color,
                    t.Description,
                    DocumentCount = t.Documents.Count,
                    t.CreatedAt
                })
                .ToList();

            gridControl1.DataSource = tags;

            var view = gridControl1.MainView as GridView;
            if (view != null)
            {
                if (view.Columns["Id"] != null) view.Columns["Id"].Caption = "ID";
                if (view.Columns["Name"] != null)
                {
                    view.Columns["Name"].Caption = "Etiket Adı";
                    view.Columns["Name"].Width = 150;
                }
                if (view.Columns["Color"] != null)
                {
                    view.Columns["Color"].Caption = "Renk";
                    view.Columns["Color"].Width = 80;
                }
                if (view.Columns["Description"] != null)
                {
                    view.Columns["Description"].Caption = "Açıklama";
                    view.Columns["Description"].Width = 200;
                }
                if (view.Columns["DocumentCount"] != null)
                {
                    view.Columns["DocumentCount"].Caption = "Döküman Sayısı";
                    view.Columns["DocumentCount"].Width = 100;
                }
                if (view.Columns["CreatedAt"] != null)
                {
                    view.Columns["CreatedAt"].Caption = "Oluşturulma";
                    view.Columns["CreatedAt"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    view.Columns["CreatedAt"].DisplayFormat.FormatString = "dd.MM.yyyy";
                }

                view.OptionsView.ShowAutoFilterRow = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new DocumentTagEditForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _context = new WorkTrackerDbContext();
                    LoadTags();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view == null || view.FocusedRowHandle < 0) return;

            var id = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "Id"));

            using (var form = new DocumentTagEditForm(id))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _context = new WorkTrackerDbContext();
                    LoadTags();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view == null || view.FocusedRowHandle < 0) return;

            var id = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "Id"));
            var name = view.GetRowCellValue(view.FocusedRowHandle, "Name")?.ToString();
            var docCount = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "DocumentCount"));

            string message = $"'{name}' etiketini silmek istediğinizden emin misiniz?";
            if (docCount > 0)
            {
                message += $"\n\nBu etikete bağlı {docCount} döküman bulunuyor. Etiket silinirse bu dökümanlardan kaldırılacak.";
            }

            var result = XtraMessageBox.Show(message, "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var tag = _context.DocumentTags.Find(id);
                    if (tag != null)
                    {
                        tag.Documents.Clear();
                        _context.DocumentTags.Remove(tag);
                        _context.SaveChanges();
                        LoadTags();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Silme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(sender, e);
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

    /// <summary>
    /// Etiket ekleme/düzenleme formu
    /// </summary>
    public partial class DocumentTagEditForm : XtraForm
    {
        private WorkTrackerDbContext _context;
        private int? _tagId;
        private DocumentTag _tag;

        private LabelControl lblName;
        private TextEdit txtName;
        private LabelControl lblColor;
        private ColorEdit colorEdit;
        private LabelControl lblDescription;
        private MemoEdit txtDescription;
        private SimpleButton btnSave;
        private SimpleButton btnCancel;

        public DocumentTagEditForm() : this(null)
        {
        }

        public DocumentTagEditForm(int? tagId)
        {
            _context = new WorkTrackerDbContext();
            _tagId = tagId;
            InitializeComponents();
            LoadData();
        }

        private void InitializeComponents()
        {
            this.lblName = new LabelControl();
            this.txtName = new TextEdit();
            this.lblColor = new LabelControl();
            this.colorEdit = new ColorEdit();
            this.lblDescription = new LabelControl();
            this.txtDescription = new MemoEdit();
            this.btnSave = new SimpleButton();
            this.btnCancel = new SimpleButton();

            // lblName
            this.lblName.Location = new Point(15, 20);
            this.lblName.Text = "Etiket Adı:*";

            // txtName
            this.txtName.Location = new Point(100, 17);
            this.txtName.Size = new Size(250, 20);

            // lblColor
            this.lblColor.Location = new Point(15, 50);
            this.lblColor.Text = "Renk:";

            // colorEdit
            this.colorEdit.Location = new Point(100, 47);
            this.colorEdit.Size = new Size(100, 20);
            this.colorEdit.EditValue = Color.FromArgb(16, 124, 16);

            // lblDescription
            this.lblDescription.Location = new Point(15, 80);
            this.lblDescription.Text = "Açıklama:";

            // txtDescription
            this.txtDescription.Location = new Point(100, 77);
            this.txtDescription.Size = new Size(250, 60);

            // btnSave
            this.btnSave.Location = new Point(170, 150);
            this.btnSave.Size = new Size(90, 30);
            this.btnSave.Text = "💾 Kaydet";
            this.btnSave.Appearance.BackColor = Color.FromArgb(16, 124, 16);
            this.btnSave.Appearance.ForeColor = Color.White;
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.Click += BtnSave_Click;

            // btnCancel
            this.btnCancel.Location = new Point(265, 150);
            this.btnCancel.Size = new Size(85, 30);
            this.btnCancel.Text = "İptal";
            this.btnCancel.Click += BtnCancel_Click;

            // Form
            this.ClientSize = new Size(380, 200);
            this.Controls.AddRange(new Control[] 
            { 
                lblName, txtName, lblColor, colorEdit, lblDescription, txtDescription, btnSave, btnCancel 
            });
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = _tagId.HasValue ? "Etiket Düzenle" : "Yeni Etiket Ekle";
            this.AcceptButton = btnSave;
        }

        private void LoadData()
        {
            if (_tagId.HasValue)
            {
                _tag = _context.DocumentTags.Find(_tagId);
                if (_tag != null)
                {
                    txtName.Text = _tag.Name;
                    txtDescription.Text = _tag.Description;
                    try
                    {
                        colorEdit.Color = ColorTranslator.FromHtml(_tag.Color);
                    }
                    catch
                    {
                        colorEdit.Color = Color.FromArgb(16, 124, 16);
                    }
                }
            }
            else
            {
                _tag = new DocumentTag();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                XtraMessageBox.Show("Etiket adı zorunludur.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            // Aynı isimde başka etiket var mı?
            var existingTag = _context.DocumentTags
                .FirstOrDefault(t => t.Name == txtName.Text.Trim() && t.Id != _tagId);
            
            if (existingTag != null)
            {
                XtraMessageBox.Show("Bu isimde bir etiket zaten mevcut.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            try
            {
                _tag.Name = txtName.Text.Trim();
                _tag.Color = ColorTranslator.ToHtml(colorEdit.Color);
                _tag.Description = txtDescription.Text?.Trim();

                if (!_tagId.HasValue)
                {
                    _context.DocumentTags.Add(_tag);
                }

                _context.SaveChanges();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Kayıt hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

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
