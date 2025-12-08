using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using work_tracker.Data;
using work_tracker.Data.Entities;

namespace work_tracker.Forms
{
    /// <summary>
    /// Zaman Kayıtları Formu - Tüm zaman kayıtlarını görüntüleme ve yönetme
    /// </summary>
    public partial class TimeEntryForm : XtraForm
    {
        private WorkTrackerDbContext _context;

        public TimeEntryForm()
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
        }

        private void TimeEntryForm_Load(object sender, EventArgs e)
        {
            InitializeControls();
            LoadTimeEntries();
        }

        private void InitializeControls()
        {
            // Form başlığı
            this.Text = "Zaman Kayıtları";

            // Tarih aralığı için varsayılan değerler
            dtFromDate.EditValue = DateTime.Today.AddDays(-30);
            dtToDate.EditValue = DateTime.Today;

            // Aktivite tipi filtresi için seçenekler
            cmbActivityType.Properties.Items.Add("Tümü");
            cmbActivityType.Properties.Items.Add(TimeEntryActivityTypes.PhoneCall);
            cmbActivityType.Properties.Items.Add(TimeEntryActivityTypes.Work);
            cmbActivityType.Properties.Items.Add(TimeEntryActivityTypes.Meeting);
            cmbActivityType.Properties.Items.Add(TimeEntryActivityTypes.Break);
            cmbActivityType.Properties.Items.Add(TimeEntryActivityTypes.Other);
            cmbActivityType.SelectedIndex = 0;
        }

        private void LoadTimeEntries()
        {
            try
            {
                var fromDate = dtFromDate.EditValue as DateTime?;
                var toDate = dtToDate.EditValue as DateTime?;
                var selectedActivityType = cmbActivityType.SelectedItem?.ToString();

                var query = _context.TimeEntries
                    .Include(t => t.WorkItem)
                    .Include(t => t.Project)
                    .AsQueryable();

                // Tarih filtresi
                if (fromDate.HasValue)
                {
                    query = query.Where(t => DbFunctions.TruncateTime(t.EntryDate) >= DbFunctions.TruncateTime(fromDate.Value));
                }

                if (toDate.HasValue)
                {
                    var endOfDay = toDate.Value.Date.AddDays(1).AddTicks(-1);
                    query = query.Where(t => t.EntryDate <= endOfDay);
                }

                // Aktivite tipi filtresi
                if (!string.IsNullOrEmpty(selectedActivityType) && selectedActivityType != "Tümü")
                {
                    query = query.Where(t => t.ActivityType == selectedActivityType);
                }

                var timeEntries = query
                    .OrderByDescending(t => t.EntryDate)
                    .Select(t => new
                    {
                        t.Id,
                        t.EntryDate,
                        t.DurationMinutes,
                        t.Subject,
                        t.ActivityType,
                        t.WorkItem,
                        t.Project,
                        t.ContactName,
                        t.PhoneNumber,
                        t.Description,
                        t.CreatedBy,
                        t.CreatedAt
                    })
                    .ToList()
                    .Select(t => new
                    {
                        t.Id,
                        t.EntryDate,
                        t.DurationMinutes,
                        t.Subject,
                        Konu = string.IsNullOrWhiteSpace(t.Subject) ? "" : t.Subject,
                        Saat = TimeSpan.FromMinutes(t.DurationMinutes).ToString(@"hh\:mm"),
                        t.ActivityType,
                        AktiviteTipi = GetActivityTypeDisplay(t.ActivityType),
                        t.WorkItem,
                        WorkItemBaslik = t.WorkItem != null ? $"#{t.WorkItem.Id} {t.WorkItem.Title}" : "",
                        t.Project,
                        ProjeAdi = t.Project != null ? t.Project.Name : "",
                        t.ContactName,
                        t.PhoneNumber,
                        t.Description,
                        t.CreatedBy,
                        t.CreatedAt
                    })
                    .ToList();

                gridTimeEntries.DataSource = timeEntries;

                // Grid kolon başlıkları
                var view = gridViewTimeEntries;
                view.BestFitColumns();

                if (view.Columns["Id"] != null) view.Columns["Id"].Caption = "ID";
                if (view.Columns["EntryDate"] != null) view.Columns["EntryDate"].Caption = "Tarih";
                if (view.Columns["DurationMinutes"] != null) view.Columns["DurationMinutes"].Caption = "Süre (dk)";
                if (view.Columns["Saat"] != null) view.Columns["Saat"].Caption = "Saat";
                if (view.Columns["Subject"] != null) view.Columns["Subject"].Visible = false;
                if (view.Columns["Konu"] != null) view.Columns["Konu"].Caption = "Konu";
                if (view.Columns["ActivityType"] != null) view.Columns["ActivityType"].Visible = false;
                if (view.Columns["AktiviteTipi"] != null) view.Columns["AktiviteTipi"].Caption = "Aktivite Tipi";
                if (view.Columns["WorkItem"] != null) view.Columns["WorkItem"].Visible = false;
                if (view.Columns["WorkItemBaslik"] != null) view.Columns["WorkItemBaslik"].Caption = "İş Öğesi";
                if (view.Columns["Project"] != null) view.Columns["Project"].Visible = false;
                if (view.Columns["ProjeAdi"] != null) view.Columns["ProjeAdi"].Caption = "Proje";
                if (view.Columns["ContactName"] != null) view.Columns["ContactName"].Caption = "Kişi";
                if (view.Columns["PhoneNumber"] != null) view.Columns["PhoneNumber"].Caption = "Telefon";
                if (view.Columns["Description"] != null) view.Columns["Description"].Caption = "Açıklama";
                if (view.Columns["CreatedBy"] != null) view.Columns["CreatedBy"].Caption = "Oluşturan";
                if (view.Columns["CreatedAt"] != null) view.Columns["CreatedAt"].Caption = "Kayıt Tarihi";

                // Özetler
                view.Columns["DurationMinutes"].Summary.Clear();
                view.Columns["DurationMinutes"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "DurationMinutes", "Toplam Süre: {0} dk");

                view.Columns["Id"].Summary.Clear();
                view.Columns["Id"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "Id", "Toplam Kayıt: {0}");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Zaman kayıtları yüklenirken hata oluştu:\n\n{ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetActivityTypeDisplay(string activityType)
        {
            switch (activityType)
            {
                case TimeEntryActivityTypes.PhoneCall:
                    return "Telefon Görüşmesi";
                case TimeEntryActivityTypes.Work:
                    return "İş";
                case TimeEntryActivityTypes.Meeting:
                    return "Toplantı";
                case TimeEntryActivityTypes.Other:
                    return "Diğer";
                default:
                    return activityType;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new TimeEntryEditForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadTimeEntries();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (gridViewTimeEntries.FocusedRowHandle < 0)
            {
                XtraMessageBox.Show("Lütfen düzenlemek için bir kayıt seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var idObj = gridViewTimeEntries.GetRowCellValue(gridViewTimeEntries.FocusedRowHandle, "Id");
            if (idObj == null)
            {
                XtraMessageBox.Show("Lütfen düzenlemek için bir kayıt seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int timeEntryId = Convert.ToInt32(idObj);
            var form = new TimeEntryEditForm(timeEntryId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadTimeEntries();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gridViewTimeEntries.FocusedRowHandle < 0)
            {
                XtraMessageBox.Show("Lütfen silmek için bir kayıt seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var idObj = gridViewTimeEntries.GetRowCellValue(gridViewTimeEntries.FocusedRowHandle, "Id");
            if (idObj == null)
            {
                XtraMessageBox.Show("Lütfen silmek için bir kayıt seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = XtraMessageBox.Show("Seçili zaman kaydını silmek istediğinizden emin misiniz?", 
                "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int timeEntryId = Convert.ToInt32(idObj);
                    var timeEntry = _context.TimeEntries.Find(timeEntryId);
                    
                    if (timeEntry != null)
                    {
                        _context.TimeEntries.Remove(timeEntry);
                        _context.SaveChanges();
                        LoadTimeEntries();
                        
                        XtraMessageBox.Show("Zaman kaydı başarıyla silindi.", "Bilgi",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Silme işlemi sırasında hata oluştu:\n\n{ex.Message}", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadTimeEntries();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadTimeEntries();
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