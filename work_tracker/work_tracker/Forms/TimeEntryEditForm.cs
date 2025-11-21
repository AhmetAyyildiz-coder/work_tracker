using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using work_tracker.Data;
using work_tracker.Data.Entities;

namespace work_tracker.Forms
{
    /// <summary>
    /// Zaman Kaydı Ekleme/Düzenleme Formu
    /// </summary>
    public partial class TimeEntryEditForm : XtraForm
    {
        private WorkTrackerDbContext _context;
        private TimeEntry _timeEntry;
        private int? _timeEntryId;

        public TimeEntryEditForm()
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
            _timeEntryId = null;
        }

        public TimeEntryEditForm(int timeEntryId)
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
            _timeEntryId = timeEntryId;
        }

        private void TimeEntryEditForm_Load(object sender, EventArgs e)
        {
            InitializeControls();
            LoadData();

            if (_timeEntryId.HasValue)
            {
                LoadTimeEntry();
                this.Text = "Zaman Kaydını Düzenle";
            }
            else
            {
                this.Text = "Yeni Zaman Kaydı";
            }
        }

        private void InitializeControls()
        {
            lblSubject.Text = "Konu / Özet";

            // Aktivite tipi seçenekleri
            cmbActivityType.Properties.Items.Add(TimeEntryActivityTypes.PhoneCall);
            cmbActivityType.Properties.Items.Add(TimeEntryActivityTypes.Work);
            cmbActivityType.Properties.Items.Add(TimeEntryActivityTypes.Meeting);
            cmbActivityType.Properties.Items.Add(TimeEntryActivityTypes.Other);
            cmbActivityType.SelectedIndex = 0;

            // Projeleri yükle
            LoadProjects();

            // Person'ları yükle
            LoadPersons();

            // Varsayılan değerler
            dtEntryDate.EditValue = DateTime.Now;
            spinDurationMinutes.EditValue = 30;
            txtSubject.Text = string.Empty;
        }

        private void LoadData()
        {
            // WorkItem ve Project ID'lerini al (eğer düzenleme modundaysa)
            int? workItemId = null;
            int? projectId = null;
            
            if (_timeEntryId.HasValue)
            {
                var timeEntry = _context.TimeEntries
                    .FirstOrDefault(t => t.Id == _timeEntryId.Value);
                if (timeEntry != null)
                {
                    workItemId = timeEntry.WorkItemId;
                    projectId = timeEntry.ProjectId;
                }
            }
            
            // WorkItem'ları yükle (seçili olanı da ekle)
            LoadWorkItems(workItemId);
            
            // Project'leri yükle (seçili olanı da ekle)
            LoadProjects(projectId);
        }

        private void LoadWorkItems(int? selectedWorkItemId = null)
        {
            var workItems = _context.WorkItems
                .Include(w => w.Project)
                .Where(w => !w.IsArchived)
                .OrderByDescending(w => w.CreatedAt)
                .Take(100) // Performans için son 100 iş
                .ToList();

            // Eğer seçili WorkItem listede yoksa ekle
            if (selectedWorkItemId.HasValue && !workItems.Any(w => w.Id == selectedWorkItemId.Value))
            {
                var selectedWorkItem = _context.WorkItems
                    .Include(w => w.Project)
                    .FirstOrDefault(w => w.Id == selectedWorkItemId.Value && !w.IsArchived);
                if (selectedWorkItem != null)
                {
                    workItems.Insert(0, selectedWorkItem);
                }
            }

            cmbWorkItem.Properties.DataSource = workItems;
            cmbWorkItem.Properties.DisplayMember = "Title";
            cmbWorkItem.Properties.ValueMember = "Id";
            cmbWorkItem.Properties.NullText = "Seçiniz";
            
            // LookUpEdit için kolonları ayarla
            cmbWorkItem.Properties.Columns.Clear();
            cmbWorkItem.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Title", "Başlık"));
            cmbWorkItem.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Project.Name", "Proje") 
            { 
                Width = 150
            });
            cmbWorkItem.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Status", "Durum") 
            { 
                Width = 100
            });
            
            cmbWorkItem.EditValue = null;
        }

        private void LoadProjects(int? selectedProjectId = null)
        {
            var projects = _context.Projects
                .Where(p => p.IsActive)
                .OrderBy(p => p.Name)
                .ToList();

            // Eğer seçili Project listede yoksa ekle
            if (selectedProjectId.HasValue && !projects.Any(p => p.Id == selectedProjectId.Value))
            {
                var selectedProject = _context.Projects
                    .FirstOrDefault(p => p.Id == selectedProjectId.Value && p.IsActive);
                if (selectedProject != null)
                {
                    projects.Insert(0, selectedProject);
                }
            }

            cmbProject.Properties.DataSource = projects;
            cmbProject.Properties.DisplayMember = "Name";
            cmbProject.Properties.ValueMember = "Id";
            cmbProject.Properties.NullText = "Seçiniz";
            
            // LookUpEdit için kolonları ayarla
            cmbProject.Properties.Columns.Clear();
            cmbProject.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Proje Adı"));
            
            cmbProject.EditValue = null;
        }

        private void LoadTimeEntry()
        {
            if (!_timeEntryId.HasValue) return;

            _timeEntry = _context.TimeEntries
                .Include(t => t.WorkItem)
                .Include(t => t.Project)
                .Include(t => t.Person)
                .FirstOrDefault(t => t.Id == _timeEntryId.Value);

            if (_timeEntry != null)
            {
                dtEntryDate.EditValue = _timeEntry.EntryDate;
                spinDurationMinutes.EditValue = _timeEntry.DurationMinutes;
                cmbActivityType.SelectedItem = _timeEntry.ActivityType;
                
                // WorkItem ve Project'i yeniden yükle (seçili olanları da ekle)
                LoadWorkItems(_timeEntry.WorkItemId);
                LoadProjects(_timeEntry.ProjectId);
                
                // WorkItem seç
                if (_timeEntry.WorkItemId.HasValue)
                {
                    cmbWorkItem.EditValue = _timeEntry.WorkItemId.Value;
                    
                    // WorkItem seçildiğinde projesini de seç (eğer ProjectId yoksa)
                    if (!_timeEntry.ProjectId.HasValue && _timeEntry.WorkItem != null && _timeEntry.WorkItem.Project != null)
                    {
                        cmbProject.EditValue = _timeEntry.WorkItem.Project.Id;
                    }
                }
                
                // Project seç (WorkItem'dan gelmediyse)
                if (_timeEntry.ProjectId.HasValue)
                {
                    cmbProject.EditValue = _timeEntry.ProjectId.Value;
                }
                
                txtSubject.Text = _timeEntry.Subject ?? string.Empty;

                if (_timeEntry.PersonId.HasValue)
                {
                    cmbPerson.EditValue = _timeEntry.PersonId.Value;
                }
                else if (!string.IsNullOrEmpty(_timeEntry.ContactName))
                {
                    // Eski ContactName değerini Person olarak bul veya göster
                    var existingPerson = _context.Persons.FirstOrDefault(p => p.Name == _timeEntry.ContactName);
                    if (existingPerson != null)
                    {
                        cmbPerson.EditValue = existingPerson.Id;
                    }
                }
                
                txtPhoneNumber.Text = _timeEntry.PhoneNumber ?? "";
                memDescription.Text = _timeEntry.Description ?? "";
            }
        }

        private void cmbActivityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedType = cmbActivityType.SelectedItem?.ToString();
            
            // Telefon görüşmesi seçildiğinde kişi ve telefon alanlarını göster
            bool isPhoneCall = selectedType == TimeEntryActivityTypes.PhoneCall;
            lblContactName.Visible = isPhoneCall;
            cmbPerson.Visible = isPhoneCall;
            btnAddPerson.Visible = isPhoneCall;
            lblPhoneNumber.Visible = isPhoneCall;
            txtPhoneNumber.Visible = isPhoneCall;
        }

        private void cmbWorkItem_EditValueChanged(object sender, EventArgs e)
        {
            var selectedWorkItemId = cmbWorkItem.EditValue as int?;
            
            if (selectedWorkItemId.HasValue && selectedWorkItemId.Value > 0)
            {
                var workItem = _context.WorkItems
                    .Include(w => w.Project)
                    .FirstOrDefault(w => w.Id == selectedWorkItemId.Value);

                if (workItem != null && workItem.Project != null)
                {
                    // WorkItem seçildiğinde otomatik olarak projesini seç
                    cmbProject.EditValue = workItem.Project.Id;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            try
            {
                if (_timeEntryId.HasValue)
                {
                    // Mevcut kaydı güncelle
                    _timeEntry.EntryDate = dtEntryDate.DateTime;
                    _timeEntry.DurationMinutes = Convert.ToInt32(spinDurationMinutes.Value);
                    _timeEntry.ActivityType = cmbActivityType.SelectedItem.ToString();
                    
                    var workItemId = cmbWorkItem.EditValue as int?;
                    _timeEntry.WorkItemId = workItemId.HasValue && workItemId.Value > 0 ? workItemId : null;
                    
                    var projectId = cmbProject.EditValue as int?;
                    _timeEntry.ProjectId = projectId.HasValue && projectId.Value > 0 ? projectId : null;

                    _timeEntry.Subject = txtSubject.Text.Trim();
                    
                    var personId = cmbPerson.EditValue as int?;
                    _timeEntry.PersonId = personId.HasValue && personId.Value > 0 ? personId : null;
                    if (_timeEntry.PersonId.HasValue)
                    {
                        var person = _context.Persons.Find(_timeEntry.PersonId.Value);
                        _timeEntry.ContactName = person?.Name ?? "";
                        _timeEntry.PhoneNumber = person?.PhoneNumber ?? txtPhoneNumber.Text.Trim();
                    }
                    else
                    {
                        _timeEntry.ContactName = "";
                        _timeEntry.PhoneNumber = txtPhoneNumber.Text.Trim();
                    }
                    _timeEntry.Description = memDescription.Text.Trim();
                    _timeEntry.UpdatedAt = DateTime.Now;
                }
                else
                {
                    // Yeni kayıt oluştur
                    var newTimeEntry = new TimeEntry
                    {
                        EntryDate = dtEntryDate.DateTime,
                        DurationMinutes = Convert.ToInt32(spinDurationMinutes.Value),
                        ActivityType = cmbActivityType.SelectedItem.ToString(),
                        
                        WorkItemId = (cmbWorkItem.EditValue as int?).HasValue && (cmbWorkItem.EditValue as int?).Value > 0 
                            ? cmbWorkItem.EditValue as int? : null,
                        
                        ProjectId = (cmbProject.EditValue as int?).HasValue && (cmbProject.EditValue as int?).Value > 0 
                            ? cmbProject.EditValue as int? : null,

                        Subject = txtSubject.Text.Trim(),
                        
                        PersonId = (cmbPerson.EditValue as int?).HasValue && (cmbPerson.EditValue as int?).Value > 0 
                            ? cmbPerson.EditValue as int? : null,
                        
                        Description = memDescription.Text.Trim(),
                        CreatedBy = Environment.UserName,
                        CreatedAt = DateTime.Now
                    };
                    
                    // Person seçildiyse ContactName ve PhoneNumber'ı doldur
                    if (newTimeEntry.PersonId.HasValue)
                    {
                        var person = _context.Persons.Find(newTimeEntry.PersonId.Value);
                        newTimeEntry.ContactName = person?.Name ?? "";
                        newTimeEntry.PhoneNumber = person?.PhoneNumber ?? txtPhoneNumber.Text.Trim();
                    }
                    else
                    {
                        newTimeEntry.ContactName = "";
                        newTimeEntry.PhoneNumber = txtPhoneNumber.Text.Trim();
                    }

                    _context.TimeEntries.Add(newTimeEntry);
                }

                _context.SaveChanges();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Kayıt sırasında hata oluştu:\n\n{ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            if (dtEntryDate.EditValue == null)
            {
                XtraMessageBox.Show("Lütfen tarih seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtEntryDate.Focus();
                return false;
            }

            if (spinDurationMinutes.Value <= 0)
            {
                XtraMessageBox.Show("Süre 0'dan büyük olmalıdır.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                spinDurationMinutes.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cmbActivityType.SelectedItem?.ToString()))
            {
                XtraMessageBox.Show("Lütfen aktivite tipi seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbActivityType.Focus();
                return false;
            }

            var selectedType = cmbActivityType.SelectedItem.ToString();
            if (selectedType == TimeEntryActivityTypes.PhoneCall)
            {
                if (cmbPerson.EditValue == null)
                {
                    XtraMessageBox.Show("Telefon görüşmesi için kişi seçilmelidir.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbPerson.Focus();
                    return false;
                }
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void LoadPersons()
        {
            var persons = _context.Persons
                .OrderBy(p => p.Name)
                .ToList();

            cmbPerson.Properties.DataSource = persons;
            cmbPerson.Properties.DisplayMember = "Name";
            cmbPerson.Properties.ValueMember = "Id";
            cmbPerson.Properties.NullText = "Kişi seçin (opsiyonel)...";
            
            // LookUpEdit için kolonları ayarla
            cmbPerson.Properties.Columns.Clear();
            cmbPerson.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Ad"));
            cmbPerson.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Email", "E-posta"));
            cmbPerson.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PhoneNumber", "Telefon"));
        }

        private void cmbPerson_EditValueChanged(object sender, EventArgs e)
        {
            var personId = cmbPerson.EditValue as int?;
            if (personId.HasValue)
            {
                var person = _context.Persons.Find(personId.Value);
                if (person != null)
                {
                    txtPhoneNumber.Text = person.PhoneNumber ?? "";
                }
            }
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            var personName = XtraInputBox.Show("Yeni kişi adını girin:", "Kişi Ekle", string.Empty);
            if (string.IsNullOrWhiteSpace(personName))
                return;

            personName = personName.Trim();

            if (_context.Persons.Any(p => p.Name == personName))
            {
                XtraMessageBox.Show("Bu isimde bir kişi zaten mevcut.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var person = new Person
            {
                Name = personName
            };

            _context.Persons.Add(person);
            _context.SaveChanges();

            LoadPersons();
            cmbPerson.EditValue = person.Id;

            XtraMessageBox.Show("Kişi oluşturuldu ve seçildi.", "Bilgi",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
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