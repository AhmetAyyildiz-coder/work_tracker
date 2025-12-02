using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using work_tracker.Data;
using work_tracker.Data.Entities;
using work_tracker.Helpers;

namespace work_tracker.Forms
{
    public partial class WorkSummaryForm : XtraForm
    {
        private WorkTrackerDbContext _context;
        private string _currentUser = Environment.UserName;
        private DateTime _startDate;
        private DateTime _endDate;
        private string _currentPeriod = "Bugün";

        public WorkSummaryForm()
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
        }

        private void WorkSummaryForm_Load(object sender, EventArgs e)
        {
            // Tarih kontrollerini varsayılan değerlerle ayarla
            dtStart.EditValue = DateTime.Today.AddDays(-7); // Son 1 hafta
            dtEnd.EditValue = DateTime.Today;
            
            // Tarih kontrolü event handler'larını ekle
            dtStart.EditValueChanged += DtDate_EditValueChanged;
            dtEnd.EditValueChanged += DtDate_EditValueChanged;
            
            // Varsayılan olarak bugünü göster
            SetPeriod("Bugün");
        }

        private void DtDate_EditValueChanged(object sender, EventArgs e)
        {
            // Tarih doğrulaması yap
            var startDate = dtStart.EditValue as DateTime?;
            var endDate = dtEnd.EditValue as DateTime?;
            
            if (startDate.HasValue && endDate.HasValue && endDate.Value < startDate.Value)
            {
                XtraMessageBox.Show("Bitiş tarihi başlangıç tarihinden önce olamaz!", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtEnd.EditValue = startDate.Value;
                return;
            }
            
            // Kullanıcı tarih değiştirdiğinde otomatik olarak "Özel" moduna geç
            if (_currentPeriod != "Özel")
            {
                SetPeriod("Özel");
            }
        }

        private void SetPeriod(string period)
        {
            _currentPeriod = period;
            var today = DateTime.Today;

            switch (period)
            {
                case "Bugün":
                    _startDate = today;
                    _endDate = today.AddDays(1).AddSeconds(-1);
                    break;
                case "Bu Hafta":
                    // Pazartesi başlangıç
                    int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
                    _startDate = today.AddDays(-diff);
                    _endDate = _startDate.AddDays(7).AddSeconds(-1);
                    break;
                case "Bu Ay":
                    _startDate = new DateTime(today.Year, today.Month, 1);
                    _endDate = _startDate.AddMonths(1).AddSeconds(-1);
                    break;
                case "Özel":
                    // Tarih kontrollerinden değerleri al
                    var startDateValue = dtStart.EditValue as DateTime?;
                    var endDateValue = dtEnd.EditValue as DateTime?;
                    
                    if (startDateValue.HasValue && endDateValue.HasValue)
                    {
                        _startDate = startDateValue.Value.Date;
                        _endDate = endDateValue.Value.Date.AddDays(1).AddSeconds(-1);
                    }
                    else
                    {
                        // Eğer tarihler null ise bugünü kullan
                        _startDate = DateTime.Today;
                        _endDate = DateTime.Today.AddDays(1).AddSeconds(-1);
                    }
                    break;
            }

            // Buton stillerini güncelle
            UpdateButtonStyles(period);
            
            // Tarih göstergesini güncelle
            UpdateDateLabel();

            // Verileri yükle
            LoadSummaryData();
        }

        private void UpdateButtonStyles(string activePeriod)
        {
            var activeColor = Color.FromArgb(0, 122, 204);
            var normalColor = Color.FromArgb(240, 240, 240);

            btnToday.Appearance.BackColor = activePeriod == "Bugün" ? activeColor : normalColor;
            btnToday.Appearance.ForeColor = activePeriod == "Bugün" ? Color.White : Color.Black;

            btnThisWeek.Appearance.BackColor = activePeriod == "Bu Hafta" ? activeColor : normalColor;
            btnThisWeek.Appearance.ForeColor = activePeriod == "Bu Hafta" ? Color.White : Color.Black;

            btnThisMonth.Appearance.BackColor = activePeriod == "Bu Ay" ? activeColor : normalColor;
            btnThisMonth.Appearance.ForeColor = activePeriod == "Bu Ay" ? Color.White : Color.Black;

            // Özel tarih aralığı butonu stilini güncelle
            btnCustomRange.Appearance.BackColor = activePeriod == "Özel" ? activeColor : normalColor;
            btnCustomRange.Appearance.ForeColor = activePeriod == "Özel" ? Color.White : Color.Black;
        }

        private void UpdateDateLabel()
        {
            if (_currentPeriod == "Bugün")
                lblDateRange.Text = $"📅 {_startDate:dd MMMM yyyy, dddd}";
            else if (_currentPeriod == "Özel")
                lblDateRange.Text = $"📅 {_startDate:dd.MM.yyyy} - {_endDate:dd.MM.yyyy}";
            else
                lblDateRange.Text = $"📅 {_startDate:dd.MM.yyyy} - {_endDate:dd.MM.yyyy}";
        }

        private void LoadSummaryData()
        {
            try
            {
                // 1. Tarih aralığında StatusChange aktivitesi olan işleri bul
                var statusChangeActivities = _context.WorkItemActivities
                    .Where(a => a.ActivityType == WorkItemActivityTypes.StatusChange &&
                               a.CreatedAt >= _startDate &&
                               a.CreatedAt <= _endDate)
                    .Include(a => a.WorkItem)
                    .ToList();

                // 2. Tarih aralığında "Gelistirmede" veya "MudahaleEdiliyor" durumunda olan işleri bul
                var workItemIds = statusChangeActivities
                    .Where(a => a.NewValue == "Gelistirmede" || a.NewValue == "MudahaleEdiliyor" ||
                               a.OldValue == "Gelistirmede" || a.OldValue == "MudahaleEdiliyor")
                    .Select(a => a.WorkItemId)
                    .Distinct()
                    .ToList();

                // Ayrıca şu anda "Gelistirmede" durumunda olan işleri de ekle
                var currentlyInDevelopment = _context.WorkItems
                    .Where(w => (w.Status == "Gelistirmede" || w.Status == "MudahaleEdiliyor") &&
                               w.StartedAt.HasValue && w.StartedAt.Value <= _endDate)
                    .Select(w => w.Id)
                    .ToList();

                workItemIds = workItemIds.Union(currentlyInDevelopment).Distinct().ToList();

                // 3. İlgili WorkItem'ları ve tüm StatusChange aktivitelerini al
                var workItems = _context.WorkItems
                    .Include(w => w.Project)
                    .Where(w => workItemIds.Contains(w.Id))
                    .ToList();

                var allStatusActivities = _context.WorkItemActivities
                    .Where(a => workItemIds.Contains(a.WorkItemId) && 
                               a.ActivityType == WorkItemActivityTypes.StatusChange)
                    .ToList();

                // 4. Her iş için geliştirme süresini hesapla (sadece seçilen tarih aralığında)
                var developmentTimes = CalculateDevelopmentTimesForPeriod(workItems, allStatusActivities);

                // 5. Aktiviteleri al (yorumlar vb.)
                var activities = _context.WorkItemActivities
                    .Where(a => a.CreatedBy == _currentUser &&
                               a.CreatedAt >= _startDate &&
                               a.CreatedAt <= _endDate)
                    .Include(a => a.WorkItem)
                    .ToList();

                // 6. TimeEntry'leri al (telefon görüşmeleri, toplantılar vb.)
                var timeEntries = _context.TimeEntries
                    .Where(t => t.CreatedBy == _currentUser &&
                               t.EntryDate >= _startDate &&
                               t.EntryDate <= _endDate)
                    .Include(t => t.WorkItem)
                    .Include(t => t.Project)
                    .Include(t => t.Person)
                    .ToList();

                // 7. Tamamlanan işleri al
                var completedWorkItems = _context.WorkItems
                    .Include(w => w.Project)
                    .Where(w => w.CompletedAt >= _startDate &&
                               w.CompletedAt <= _endDate)
                    .ToList();

                // Özet kartlarını güncelle
                UpdateSummaryCards(developmentTimes, activities, timeEntries, completedWorkItems);

                // Grid'leri doldur
                LoadTimeDistribution(developmentTimes);
                LoadActivities(activities);
                LoadTimeEntries(timeEntries);
                LoadCompletedItems(completedWorkItems);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Veriler yüklenirken hata oluştu: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Seçilen tarih aralığında her iş için geliştirme süresini hesaplar
        /// </summary>
        private List<WorkItemDevelopmentTime> CalculateDevelopmentTimesForPeriod(
            List<WorkItem> workItems, 
            List<WorkItemActivity> allStatusActivities)
        {
            var result = new List<WorkItemDevelopmentTime>();

            foreach (var workItem in workItems)
            {
                var statusActivities = allStatusActivities
                    .Where(a => a.WorkItemId == workItem.Id)
                    .ToList();

                // Günlük breakdown hesapla
                var dailyBreakdown = DevelopmentTimeHelper.CalculateDailyBreakdown(workItem, statusActivities);

                // Sadece seçilen tarih aralığındaki günleri filtrele
                var filteredMinutes = dailyBreakdown
                    .Where(kvp => kvp.Key >= _startDate.Date && kvp.Key <= _endDate.Date)
                    .Sum(kvp => kvp.Value.TotalMinutes);

                if (filteredMinutes > 0)
                {
                    result.Add(new WorkItemDevelopmentTime
                    {
                        WorkItemId = workItem.Id,
                        Title = workItem.Title,
                        Status = workItem.Status,
                        Project = workItem.Project?.Name ?? "-",
                        TotalMinutes = (int)filteredMinutes
                    });
                }
            }

            return result.OrderByDescending(x => x.TotalMinutes).ToList();
        }

        private void UpdateSummaryCards(List<WorkItemDevelopmentTime> developmentTimes,
            List<WorkItemActivity> activities, List<TimeEntry> timeEntries, List<WorkItem> completedWorkItems)
        {
            // Toplam geliştirme süresi
            var developmentMinutes = developmentTimes.Sum(t => t.TotalMinutes);
            
            // Zaman kayıtları süresi (telefon görüşmeleri, toplantılar vb.)
            var timeEntryMinutes = timeEntries.Sum(t => t.DurationMinutes);
            
            // Toplam çalışma süresi = Geliştirme süresi + Zaman kayıtları süresi
            var totalMinutes = developmentMinutes + timeEntryMinutes;
            var hours = totalMinutes / 60;
            var minutes = totalMinutes % 60;
            lblTotalTime.Text = $"{hours}s {minutes}dk";

            // Çalışılan iş sayısı (geliştirmede zaman geçirilen)
            var workedItemCount = developmentTimes.Count;
            lblWorkedItems.Text = workedItemCount.ToString();

            // Tamamlanan iş sayısı
            lblCompletedItems.Text = completedWorkItems.Count.ToString();

            // Aktivite sayısı (yorumlar + durum değişiklikleri)
            var activityCount = activities.Count;
            lblActivityCount.Text = activityCount.ToString();
        }

        private void LoadTimeDistribution(List<WorkItemDevelopmentTime> developmentTimes)
        {
            var distribution = developmentTimes
                .Select(d => new
                {
                    d.WorkItemId,
                    d.Title,
                    d.Status,
                    d.Project,
                    d.TotalMinutes,
                    Süre = FormatDuration(d.TotalMinutes)
                })
                .ToList();

            gridTimeDistribution.DataSource = distribution;
            
            var view = gridViewTimeDistribution;
            view.BestFitColumns();

            if (view.Columns["WorkItemId"] != null) view.Columns["WorkItemId"].Caption = "İş ID";
            if (view.Columns["Title"] != null) view.Columns["Title"].Caption = "İş Başlığı";
            if (view.Columns["Status"] != null) view.Columns["Status"].Caption = "Durum";
            if (view.Columns["Project"] != null) view.Columns["Project"].Caption = "Proje";
            if (view.Columns["TotalMinutes"] != null) view.Columns["TotalMinutes"].Visible = false;
            if (view.Columns["Süre"] != null) view.Columns["Süre"].Caption = "Geliştirme Süresi";
        }

        private void LoadActivities(List<WorkItemActivity> activities)
        {
            var activityList = activities
                .Select(a => new
                {
                    a.CreatedAt,
                    Tarih = a.CreatedAt.ToString("dd.MM.yyyy HH:mm"),
                    WorkItemId = a.WorkItemId,
                    Title = a.WorkItem?.Title ?? "Bilinmeyen İş",
                    ActivityType = GetActivityTypeText(a.ActivityType),
                    a.Description
                })
                .OrderByDescending(a => a.CreatedAt)
                .ToList();

            gridActivities.DataSource = activityList;

            var view = gridViewActivities;
            view.BestFitColumns();

            if (view.Columns["CreatedAt"] != null) view.Columns["CreatedAt"].Visible = false;
            if (view.Columns["Tarih"] != null) view.Columns["Tarih"].Caption = "Tarih/Saat";
            if (view.Columns["WorkItemId"] != null) view.Columns["WorkItemId"].Caption = "İş ID";
            if (view.Columns["Title"] != null) view.Columns["Title"].Caption = "İş Başlığı";
            if (view.Columns["ActivityType"] != null) view.Columns["ActivityType"].Caption = "Aktivite Tipi";
            if (view.Columns["Description"] != null) view.Columns["Description"].Caption = "Açıklama";
        }

        private void LoadTimeEntries(List<TimeEntry> timeEntries)
        {
            var timeEntryList = timeEntries
                .Select(t => new
                {
                    t.Id,
                    Tarih = t.EntryDate.ToString("dd.MM.yyyy HH:mm"),
                    t.Subject,
                    t.ActivityType,
                    Simge = GetActivityTypeIcon(t.ActivityType),
                    Süre = FormatDuration(t.DurationMinutes),
                    İlgiliİş = t.WorkItem?.Title ?? "-",
                    Proje = t.Project?.Name ?? "-",
                    Kişi = t.Person?.Name ?? t.ContactName ?? "-",
                    t.Description
                })
                .OrderByDescending(t => t.Tarih)
                .ToList();

            gridTimeEntries.DataSource = timeEntryList;

            var view = gridViewTimeEntries;
            view.BestFitColumns();

            if (view.Columns["Id"] != null) view.Columns["Id"].Visible = false;
            if (view.Columns["Tarih"] != null) view.Columns["Tarih"].Caption = "Tarih/Saat";
            if (view.Columns["Subject"] != null) view.Columns["Subject"].Caption = "Konu";
            if (view.Columns["ActivityType"] != null) view.Columns["ActivityType"].Caption = "Tip";
            if (view.Columns["GetActivityTypeIcon"] != null) view.Columns["GetActivityTypeIcon"].Caption = "";
            if (view.Columns["Süre"] != null) view.Columns["Süre"].Caption = "Süre";
            if (view.Columns["İlgiliİş"] != null) view.Columns["İlgiliİş"].Caption = "İlgili İş";
            if (view.Columns["Proje"] != null) view.Columns["Proje"].Caption = "Proje";
            if (view.Columns["Kişi"] != null) view.Columns["Kişi"].Caption = "Kişi";
            if (view.Columns["Description"] != null) view.Columns["Description"].Caption = "Açıklama";
        }

        private string GetActivityTypeIcon(string activityType)
        {
            switch (activityType)
            {
                case "PhoneCall": return "📞";
                case "Work": return "💼";
                case "Meeting": return "👥";
                case "Other": return "📝";
                default: return "📝";
            }
        }

        private void LoadCompletedItems(List<WorkItem> completedWorkItems)
        {
            var completedList = completedWorkItems
                .Select(w => new
                {
                    w.Id,
                    w.Title,
                    w.Type,
                    Project = w.Project?.Name ?? "-",
                    CompletedAt = w.CompletedAt?.ToString("dd.MM.yyyy HH:mm") ?? "-"
                })
                .OrderByDescending(w => w.CompletedAt)
                .ToList();

            gridCompleted.DataSource = completedList;

            var view = gridViewCompleted;
            view.BestFitColumns();

            if (view.Columns["Id"] != null) view.Columns["Id"].Caption = "İş ID";
            if (view.Columns["Title"] != null) view.Columns["Title"].Caption = "İş Başlığı";
            if (view.Columns["Type"] != null) view.Columns["Type"].Caption = "Tip";
            if (view.Columns["Project"] != null) view.Columns["Project"].Caption = "Proje";
            if (view.Columns["CompletedAt"] != null) view.Columns["CompletedAt"].Caption = "Tamamlanma Tarihi";
        }

        private string FormatDuration(int totalMinutes)
        {
            var hours = totalMinutes / 60;
            var minutes = totalMinutes % 60;
            if (hours > 0)
                return $"{hours}s {minutes}dk";
            return $"{minutes}dk";
        }

        private string FormatTimeSpan(TimeSpan ts)
        {
            if (ts.TotalDays >= 1)
                return $"{(int)ts.TotalDays}g {ts.Hours}s {ts.Minutes}dk";
            if (ts.TotalHours >= 1)
                return $"{(int)ts.TotalHours}s {ts.Minutes}dk";
            return $"{ts.Minutes}dk";
        }

        private string GetActivityTypeText(string activityType)
        {
            switch (activityType)
            {
                case "Comment": return "💬 Yorum";
                case "StatusChange": return "📊 Durum Değişikliği";
                case "Created": return "✨ Oluşturuldu";
                case "FieldUpdate": return "✏️ Güncelleme";
                case "PriorityChange": return "⚡ Öncelik Değişikliği";
                default: return activityType;
            }
        }

        #region Button Events

        private void btnToday_Click(object sender, EventArgs e)
        {
            SetPeriod("Bugün");
        }

        private void btnThisWeek_Click(object sender, EventArgs e)
        {
            SetPeriod("Bu Hafta");
        }

        private void btnThisMonth_Click(object sender, EventArgs e)
        {
            SetPeriod("Bu Ay");
        }

        private void btnCustomRange_Click(object sender, EventArgs e)
        {
            SetPeriod("Özel");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSummaryData();
        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            try
            {
                var sb = new StringBuilder();
                
                // Başlık
                sb.AppendLine($"📊 Çalışma Özeti - {_currentPeriod}");
                sb.AppendLine($"📅 {lblDateRange.Text.Replace("📅 ", "")}");
                sb.AppendLine($"👤 {_currentUser}");
                sb.AppendLine();
                
                // Özet
                sb.AppendLine("═══════════════════════════════════");
                sb.AppendLine($"⏱️ Toplam Geliştirme Süresi: {lblTotalTime.Text}");
                sb.AppendLine($"📋 Çalışılan İş Sayısı: {lblWorkedItems.Text}");
                sb.AppendLine($"✅ Tamamlanan İş: {lblCompletedItems.Text}");
                sb.AppendLine($"💬 Aktivite Sayısı: {lblActivityCount.Text}");
                sb.AppendLine("═══════════════════════════════════");
                sb.AppendLine();

                // Zaman dağılımı detayları
                sb.AppendLine("📝 Geliştirme Süresi Dağılımı:");
                sb.AppendLine("-----------------------------------");
                
                var view = gridViewTimeDistribution;
                for (int i = 0; i < view.RowCount; i++)
                {
                    var workItemId = view.GetRowCellValue(i, "WorkItemId");
                    var title = view.GetRowCellValue(i, "Title");
                    var süre = view.GetRowCellValue(i, "Süre");
                    var status = view.GetRowCellValue(i, "Status");
                    sb.AppendLine($"  • [#{workItemId}] {title} ({status}) - {süre}");
                }

                sb.AppendLine();
                sb.AppendLine("⏱️ Zaman Kayıtları:");
                sb.AppendLine("-------------------");
                
                var timeEntryView = gridViewTimeEntries;
                for (int i = 0; i < timeEntryView.RowCount; i++)
                {
                    var tarih = timeEntryView.GetRowCellValue(i, "Tarih");
                    var konu = timeEntryView.GetRowCellValue(i, "Subject");
                    var süre = timeEntryView.GetRowCellValue(i, "Süre");
                    var tip = timeEntryView.GetRowCellValue(i, "GetActivityTypeIcon");
                    var kişi = timeEntryView.GetRowCellValue(i, "Kişi");
                    sb.AppendLine($"  • {tip} {tarih} - {konu} ({kişi}) - {süre}");
                }

                sb.AppendLine();
                sb.AppendLine($"📆 Rapor oluşturma: {DateTime.Now:dd.MM.yyyy HH:mm}");

                Clipboard.SetText(sb.ToString());
                XtraMessageBox.Show("Çalışma özeti panoya kopyalandı!", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Kopyalama hatası: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Excel Dosyası (*.xlsx)|*.xlsx";
                    saveDialog.FileName = $"Calisma_Ozeti_{_startDate:yyyyMMdd}_{_endDate:yyyyMMdd}.xlsx";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        gridTimeDistribution.ExportToXlsx(saveDialog.FileName);
                        XtraMessageBox.Show("Excel dosyası başarıyla oluşturuldu!", "Başarılı",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Excel export hatası: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

    /// <summary>
    /// İş öğesi geliştirme süresi bilgilerini tutar
    /// </summary>
    public class WorkItemDevelopmentTime
    {
        public int WorkItemId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string Project { get; set; }
        public int TotalMinutes { get; set; }
    }
}
