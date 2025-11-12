using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using work_tracker.Data;
using work_tracker.Data.Entities;

namespace work_tracker.Forms
{
    public partial class SprintEditForm : XtraForm
    {
        private WorkTrackerDbContext _context;
        private int? _sprintId;

        public SprintEditForm(int? sprintId = null)
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
            _sprintId = sprintId;
        }

        private void SprintEditForm_Load(object sender, EventArgs e)
        {
            // Status combobox'ını doldur
            cmbStatus.Properties.Items.AddRange(new string[] { "Planned", "Active", "Completed", "Cancelled" });

            if (_sprintId.HasValue)
            {
                var sprint = _context.Sprints.Find(_sprintId.Value);
                if (sprint != null)
                {
                    txtName.Text = sprint.Name;
                    txtGoals.Text = sprint.Goals;
                    dateStartDate.EditValue = sprint.StartDate;
                    dateEndDate.EditValue = sprint.EndDate;
                    cmbStatus.EditValue = sprint.Status;
                }
            }
            else
            {
                cmbStatus.EditValue = "Planned";
                dateStartDate.EditValue = DateTime.Now;
                dateEndDate.EditValue = DateTime.Now.AddDays(14); // Varsayılan 2 haftalık sprint
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                XtraMessageBox.Show("Sprint adı boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            if (dateStartDate.EditValue == null)
            {
                XtraMessageBox.Show("Başlangıç tarihi boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateStartDate.Focus();
                return;
            }

            if (dateEndDate.EditValue == null)
            {
                XtraMessageBox.Show("Bitiş tarihi boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateEndDate.Focus();
                return;
            }

            DateTime startDate = Convert.ToDateTime(dateStartDate.EditValue);
            DateTime endDate = Convert.ToDateTime(dateEndDate.EditValue);

            if (endDate <= startDate)
            {
                XtraMessageBox.Show("Bitiş tarihi başlangıç tarihinden sonra olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateEndDate.Focus();
                return;
            }

            try
            {
                if (_sprintId.HasValue)
                {
                    var sprint = _context.Sprints.Find(_sprintId.Value);
                    if (sprint != null)
                    {
                        sprint.Name = txtName.Text.Trim();
                        sprint.Goals = txtGoals.Text?.Trim();
                        sprint.StartDate = startDate;
                        sprint.EndDate = endDate;
                        sprint.Status = cmbStatus.EditValue.ToString();
                        
                        if (sprint.Status == "Completed" && !sprint.CompletedAt.HasValue)
                        {
                            sprint.CompletedAt = DateTime.Now;
                        }
                    }
                }
                else
                {
                    var newSprint = new Sprint
                    {
                        Name = txtName.Text.Trim(),
                        Goals = txtGoals.Text?.Trim(),
                        StartDate = startDate,
                        EndDate = endDate,
                        Status = cmbStatus.EditValue.ToString(),
                        CreatedAt = DateTime.Now
                    };
                    _context.Sprints.Add(newSprint);
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

