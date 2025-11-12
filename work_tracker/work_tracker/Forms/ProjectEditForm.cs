using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using work_tracker.Data;
using work_tracker.Data.Entities;

namespace work_tracker.Forms
{
    public partial class ProjectEditForm : XtraForm
    {
        private WorkTrackerDbContext _context;
        private int? _projectId;

        public ProjectEditForm(int? projectId = null)
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
            _projectId = projectId;
        }

        private void ProjectEditForm_Load(object sender, EventArgs e)
        {
            if (_projectId.HasValue)
            {
                var project = _context.Projects.Find(_projectId.Value);
                if (project != null)
                {
                    txtName.Text = project.Name;
                    chkIsActive.Checked = project.IsActive;
                }
            }
            else
            {
                chkIsActive.Checked = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                XtraMessageBox.Show("Proje adı boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            try
            {
                if (_projectId.HasValue)
                {
                    var project = _context.Projects.Find(_projectId.Value);
                    if (project != null)
                    {
                        project.Name = txtName.Text.Trim();
                        project.IsActive = chkIsActive.Checked;
                    }
                }
                else
                {
                    var newProject = new Project
                    {
                        Name = txtName.Text.Trim(),
                        IsActive = chkIsActive.Checked,
                        CreatedAt = DateTime.Now
                    };
                    _context.Projects.Add(newProject);
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

