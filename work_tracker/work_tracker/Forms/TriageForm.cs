using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using work_tracker.Data;
using work_tracker.Data.Entities;

namespace work_tracker.Forms
{
    public partial class TriageForm : XtraForm
    {
        private WorkTrackerDbContext _context;
        private int? _selectedWorkItemId;

        public TriageForm(int? workItemId = null)
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
            _selectedWorkItemId = workItemId;
        }

        private void TriageForm_Load(object sender, EventArgs e)
        {
            LoadComboboxes();

            if (_selectedWorkItemId.HasValue)
            {
                LoadWorkItem(_selectedWorkItemId.Value);
            }
        }

        private void LoadComboboxes()
        {
            // Projeler
            var projects = _context.Projects.Where(p => p.IsActive).OrderBy(p => p.Name).ToList();
            cmbProject.Properties.DataSource = projects;
            cmbProject.Properties.DisplayMember = "Name";
            cmbProject.Properties.ValueMember = "Id";
            cmbProject.Properties.NullText = "Proje seçin...";
            
            // LookUpEdit için kolonları ayarla
            cmbProject.Properties.Columns.Clear();
            cmbProject.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Proje Adı"));
            
            // Modüller - başlangıçta boş
            cmbModule.Properties.NullText = "Önce proje seçin...";
            cmbModule.EditValue = null;

            // İş Tipleri
            cmbType.Properties.Items.Clear();
            cmbType.Properties.Items.AddRange(new[] { "AcilArge", "Bug", "YeniÖzellik", "İyileştirme", "Diğer","Arge" });

            // Aciliyet
            cmbUrgency.Properties.Items.Clear();
            cmbUrgency.Properties.Items.AddRange(new[] { "Kritik", "Yüksek", "Normal", "Düşük" });

            // Hedef Pano (Board)
            cmbTargetBoard.Properties.Items.Clear();
            cmbTargetBoard.Properties.Items.AddRange(new[] { "Scrum", "Kanban" });

            // Sprint'leri yükle (başlangıçta gizli)
            LoadSprints();
            lblSprint.Visible = false;
            cmbSprint.Visible = false;
        }

        private void LoadSprints()
        {
            var sprints = _context.Sprints
                .Where(s => s.Status == "Active" || s.Status == "Planned")
                .OrderByDescending(s => s.Status == "Active")
                .ThenByDescending(s => s.StartDate)
                .ToList();

            cmbSprint.Properties.DataSource = sprints;
            cmbSprint.Properties.DisplayMember = "Name";
            cmbSprint.Properties.ValueMember = "Id";
            cmbSprint.Properties.NullText = "Sprint seçin...";

            // LookUpEdit için kolonları ayarla
            cmbSprint.Properties.Columns.Clear();
            cmbSprint.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Sprint Adı"));
            cmbSprint.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Status", "Durum"));
        }

        private void LoadWorkItem(int workItemId)
        {
            var workItem = _context.WorkItems.Find(workItemId);
            if (workItem != null)
            {
                lblWorkItemId.Text = $"İş Talebi #{workItem.Id}";
                txtTitle.Text = workItem.Title;
                txtDescription.Text = workItem.Description;
                txtRequestedBy.Text = workItem.RequestedBy;
                dtRequestedAt.EditValue = workItem.RequestedAt;
                
                cmbProject.EditValue = workItem.ProjectId;
                cmbModule.EditValue = workItem.ModuleId;
                
                cmbType.EditValue = workItem.Type;
                cmbUrgency.EditValue = workItem.Urgency;
                
                if (!string.IsNullOrEmpty(workItem.EffortEstimate?.ToString()))
                {
                    txtEffort.Text = workItem.EffortEstimate.ToString();
                }

                if (!string.IsNullOrEmpty(workItem.Board))
                {
                    cmbTargetBoard.EditValue = workItem.Board;
                }

                if (workItem.SprintId.HasValue)
                {
                    cmbSprint.EditValue = workItem.SprintId.Value;
                }
            }
        }

        private void cmbProject_EditValueChanged(object sender, EventArgs e)
        {
            cmbModule.Properties.DataSource = null;
            cmbModule.EditValue = null;
            
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
                cmbModule.Properties.NullText = "Modül seçin...";
                
                // LookUpEdit için kolonları ayarla
                cmbModule.Properties.Columns.Clear();
                cmbModule.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Modül Adı"));
            }
            else
            {
                cmbModule.Properties.NullText = "Önce proje seçin...";
            }
        }

        private void cmbTargetBoard_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbTargetBoard.EditValue != null && cmbTargetBoard.EditValue.ToString() == "Scrum")
            {
                lblSprint.Visible = true;
                cmbSprint.Visible = true;
            }
            else
            {
                lblSprint.Visible = false;
                cmbSprint.Visible = false;
                cmbSprint.EditValue = null;
            }
        }

        private void btnSaveAndRoute_Click(object sender, EventArgs e)
        {
            if (!_selectedWorkItemId.HasValue)
            {
                XtraMessageBox.Show("İş talebi seçilmedi!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbType.EditValue == null)
            {
                XtraMessageBox.Show("İş tipi seçilmelidir!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbType.Focus();
                return;
            }

            if (cmbUrgency.EditValue == null)
            {
                XtraMessageBox.Show("Aciliyet seçilmelidir!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbUrgency.Focus();
                return;
            }

            if (cmbTargetBoard.EditValue == null)
            {
                XtraMessageBox.Show("Hedef pano seçilmelidir!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTargetBoard.Focus();
                return;
            }

            // Scrum seçildiyse Sprint zorunlu
            if (cmbTargetBoard.EditValue.ToString() == "Scrum" && cmbSprint.EditValue == null)
            {
                XtraMessageBox.Show("Scrum için Sprint seçilmelidir!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbSprint.Focus();
                return;
            }

            try
            {
                var workItem = _context.WorkItems.Find(_selectedWorkItemId.Value);
                if (workItem != null)
                {
                    workItem.ProjectId = cmbProject.EditValue as int?;
                    workItem.ModuleId = cmbModule.EditValue as int?;
                    workItem.Type = cmbType.EditValue?.ToString();
                    workItem.Urgency = cmbUrgency.EditValue?.ToString();
                    
                    decimal effort;
                    if (decimal.TryParse(txtEffort.Text, out effort))
                    {
                        // Validation: Effort should be between 0.1 and 30 days
                        if (effort < 0.1m || effort > 30m)
                        {
                            XtraMessageBox.Show("Efor tahmini 0.1 ile 30 gün arasında olmalıdır!",
                                "Geçersiz Değer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtEffort.Focus();
                            return;
                        }
                        
                        workItem.EffortEstimate = effort;
                    }
                    else if (!string.IsNullOrEmpty(txtEffort.Text))
                    {
                        XtraMessageBox.Show("Geçersiz efor değeri! Lütfen geçerli bir sayı girin.",
                            "Geçersiz Değer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtEffort.Focus();
                        return;
                    }

                    var targetBoard = cmbTargetBoard.EditValue.ToString();
                    workItem.Board = targetBoard;
                    
                    // Board'a göre durum ata
                    if (targetBoard == "Kanban")
                    {
                        // Aciliyet'e göre başlangıç sütununu belirle
                        // Kritik/Yüksek => GelenAcilIsler, Normal/Düşük => Sirada
                        var urgency = (cmbUrgency.EditValue ?? "").ToString();
                        if (urgency == "Kritik" || urgency == "Yüksek")
                        {
                            workItem.Status = "GelenAcilIsler";
                        }
                        else
                        {
                            workItem.Status = "Sirada";
                        }

                        // Kanban'a alınan işlerin Sprint bağını kopar, ancak tarihçeyi koru
                        workItem.SprintId = null; // Kanban'da aktif sprint bilgisi tutulmaz
                    }
                    else if (targetBoard == "Scrum")
                    {
                        workItem.Status = "SprintBacklog";

                        var selectedSprintId = cmbSprint.EditValue as int?;
                        workItem.SprintId = selectedSprintId;

                        // İlk kez sprint'e alınıyorsa InitialSprintId'yi set et
                        if (!workItem.InitialSprintId.HasValue && selectedSprintId.HasValue)
                        {
                            workItem.InitialSprintId = selectedSprintId;
                        }
                    }

                    workItem.TriagedBy = Environment.UserName;
                    workItem.TriagedAt = DateTime.Now;

                    _context.SaveChanges();

                    XtraMessageBox.Show("İş talebi başarıyla yönlendirildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Kayıt sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

