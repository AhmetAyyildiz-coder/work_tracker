using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using work_tracker.Data;
using work_tracker.Data.Entities;

namespace work_tracker.Forms
{
    public partial class WorkItemEditForm : XtraForm
    {
        private WorkTrackerDbContext _context;
        private int? _workItemId;

        public WorkItemEditForm(int? workItemId = null, int? meetingId = null, string initialTitle = null, string initialDescription = null)
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
            _workItemId = workItemId;

            // Toplantıdan gelen veri varsa ön-doldur
            if (meetingId.HasValue)
            {
                cmbMeeting.EditValue = meetingId.Value;
            }
            if (!string.IsNullOrEmpty(initialTitle))
            {
                txtTitle.Text = initialTitle;
            }
            if (!string.IsNullOrEmpty(initialDescription))
            {
                txtDescription.Text = initialDescription;
            }
        }

        private void WorkItemEditForm_Load(object sender, EventArgs e)
        {
            LoadComboboxes();

            if (_workItemId.HasValue)
            {
                LoadWorkItem(_workItemId.Value);
            }
            else
            {
                // Varsayılan değerler
                dtRequestedAt.EditValue = DateTime.Now;
                txtRequestedBy.Text = Environment.UserName;
            }
        }

        private void LoadComboboxes()
        {
            // Projeler
            var projects = _context.Projects.Where(p => p.IsActive).OrderBy(p => p.Name).ToList();
            cmbProject.Properties.DataSource = projects;
            cmbProject.Properties.DisplayMember = "Name";
            cmbProject.Properties.ValueMember = "Id";
            cmbProject.Properties.NullText = "Proje seçin (opsiyonel)...";
            
            // LookUpEdit için kolonları ayarla
            cmbProject.Properties.Columns.Clear();
            cmbProject.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Proje Adı"));

            // Modüller - başlangıçta boş
            cmbModule.Properties.NullText = "Önce proje seçin...";
            cmbModule.EditValue = null;

            // Toplantılar
            var meetings = _context.Meetings.OrderByDescending(m => m.MeetingDate).Take(50).ToList();
            cmbMeeting.Properties.DataSource = meetings;
            cmbMeeting.Properties.DisplayMember = "Subject";
            cmbMeeting.Properties.ValueMember = "Id";
            cmbMeeting.Properties.NullText = "Toplantı seçin (opsiyonel)...";
            
            // LookUpEdit için kolonları ayarla
            cmbMeeting.Properties.Columns.Clear();
            cmbMeeting.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Subject", "Toplantı Konusu"));
            cmbMeeting.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MeetingDate", "Tarih") 
            { 
                Width = 100,
                FormatString = "dd.MM.yyyy"
            });
        }

        private void LoadWorkItem(int workItemId)
        {
            var workItem = _context.WorkItems.Find(workItemId);
            if (workItem != null)
            {
                txtTitle.Text = workItem.Title;
                txtDescription.Text = workItem.Description;
                txtRequestedBy.Text = workItem.RequestedBy;
                dtRequestedAt.EditValue = workItem.RequestedAt;
                cmbProject.EditValue = workItem.ProjectId;
                cmbModule.EditValue = workItem.ModuleId;
                cmbMeeting.EditValue = workItem.SourceMeetingId;
            }
        }

        private void cmbProject_EditValueChanged(object sender, EventArgs e)
        {
            cmbModule.Properties.DataSource = null;
            cmbModule.EditValue = null;
            
            // Seçili projeye göre modülleri yükle
            if (cmbProject.EditValue != null)
            {
                var projectId = (int)cmbProject.EditValue;
                var modules = _context.ProjectModules
                    .Where(m => m.ProjectId == projectId && m.IsActive)
                    .OrderBy(m => m.Name)
                    .ToList();

                cmbModule.Properties.DataSource = modules;
                cmbModule.Properties.DisplayMember = "Name";
                cmbModule.Properties.ValueMember = "Id";
                cmbModule.Properties.NullText = "Modül seçin (opsiyonel)...";
                
                // LookUpEdit için kolonları ayarla
                cmbModule.Properties.Columns.Clear();
                cmbModule.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Modül Adı"));
            }
            else
            {
                cmbModule.Properties.NullText = "Önce proje seçin...";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                XtraMessageBox.Show("Başlık boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtRequestedBy.Text))
            {
                XtraMessageBox.Show("Talep eden kişi boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRequestedBy.Focus();
                return;
            }

            try
            {
                if (_workItemId.HasValue)
                {
                    var workItem = _context.WorkItems.Find(_workItemId.Value);
                    if (workItem != null)
                    {
                        workItem.Title = txtTitle.Text.Trim();
                        workItem.Description = txtDescription.Text;
                        workItem.RequestedBy = txtRequestedBy.Text.Trim();
                        workItem.RequestedAt = Convert.ToDateTime(dtRequestedAt.EditValue);
                        workItem.ProjectId = cmbProject.EditValue as int?;
                        workItem.ModuleId = cmbModule.EditValue as int?;
                        workItem.SourceMeetingId = cmbMeeting.EditValue as int?;
                    }
                }
                else
                {
                    var newWorkItem = new WorkItem
                    {
                        Title = txtTitle.Text.Trim(),
                        Description = txtDescription.Text,
                        RequestedBy = txtRequestedBy.Text.Trim(),
                        RequestedAt = Convert.ToDateTime(dtRequestedAt.EditValue),
                        ProjectId = cmbProject.EditValue as int?,
                        ModuleId = cmbModule.EditValue as int?,
                        SourceMeetingId = cmbMeeting.EditValue as int?,
                        Board = "Inbox",
                        Status = "Bekliyor",
                        CreatedAt = DateTime.Now
                    };
                    _context.WorkItems.Add(newWorkItem);
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

