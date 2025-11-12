using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using work_tracker.Data;
using work_tracker.Data.Entities;

namespace work_tracker.Forms
{
    public partial class ModuleEditForm : XtraForm
    {
        private WorkTrackerDbContext _context;
        private int? _moduleId;

        public ModuleEditForm(int? moduleId = null)
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
            _moduleId = moduleId;
        }

        private void ModuleEditForm_Load(object sender, EventArgs e)
        {
            LoadProjects();

            if (_moduleId.HasValue)
            {
                var module = _context.ProjectModules.Find(_moduleId.Value);
                if (module != null)
                {
                    cmbProject.EditValue = module.ProjectId;
                    txtName.EditValue = module.Name;
                    chkIsActive.Checked = module.IsActive;
                    this.Text = "Modül Düzenle";
                }
            }
            else
            {
                chkIsActive.Checked = true;
                this.Text = "Yeni Modül";
            }
        }

        private void LoadProjects()
        {
            var projects = _context.Projects
                .Where(p => p.IsActive)
                .OrderBy(p => p.Name)
                .ToList();

            cmbProject.Properties.DataSource = projects;
            cmbProject.Properties.DisplayMember = "Name";
            cmbProject.Properties.ValueMember = "Id";
            cmbProject.Properties.NullText = "Proje seçin...";
            
            // LookUpEdit için kolonları ayarla
            cmbProject.Properties.Columns.Clear();
            cmbProject.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Proje Adı"));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbProject.EditValue == null)
            {
                XtraMessageBox.Show("Lütfen bir proje seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProject.Focus();
                return;
            }

            var moduleName = txtName.EditValue?.ToString()?.Trim();
            if (string.IsNullOrWhiteSpace(moduleName))
            {
                XtraMessageBox.Show("Modül adı boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            try
            {
                if (_moduleId.HasValue)
                {
                    var module = _context.ProjectModules.Find(_moduleId.Value);
                    if (module != null)
                    {
                        module.ProjectId = (int)cmbProject.EditValue;
                        module.Name = moduleName;
                        module.IsActive = chkIsActive.Checked;
                    }
                }
                else
                {
                    var newModule = new ProjectModule
                    {
                        ProjectId = (int)cmbProject.EditValue,
                        Name = moduleName,
                        IsActive = chkIsActive.Checked,
                        CreatedAt = DateTime.Now
                    };
                    _context.ProjectModules.Add(newModule);
                }

                _context.SaveChanges();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Kayıt sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

