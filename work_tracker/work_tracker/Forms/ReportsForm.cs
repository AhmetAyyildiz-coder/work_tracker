using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using work_tracker.Data;
using work_tracker.Data.Entities;
using work_tracker.Helpers;

namespace work_tracker.Forms
{
    public partial class ReportsForm : XtraForm
    {
        private WorkTrackerDbContext _context;
        private static readonly string[] DevelopmentStatuses = { "Gelistirmede", "MudahaleEdiliyor" };

        public ReportsForm()
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            // Varsayılan tarih aralığı: Son 30 gün
            dtTo.EditValue = DateTime.Today;
            dtFrom.EditValue = DateTime.Today.AddDays(-30);
            // Timeline için bugünün tarihini ayarla
            dtTimelineDate.EditValue = DateTime.Today;
            LoadAllReports();
        }

        private void LoadAllReports()
        {
            LoadEffortChartReport();
            LoadDetailedReport();
            LoadDailyEffortReport();
            LoadActivityTimeline();
        }

        private Tuple<DateTime?, DateTime?> GetDateRange()
        {
            DateTime? from = dtFrom.EditValue as DateTime?;
            DateTime? to = dtTo.EditValue as DateTime?;
            return Tuple.Create(from, to);
        }

        private string GetDateRangeText()
        {
            var range = GetDateRange();
            var from = range.Item1;
            var to = range.Item2;

            if (!from.HasValue && !to.HasValue)
                return "";

            if (from.HasValue && to.HasValue)
                return $" ({from:dd.MM.yyyy} - {to:dd.MM.yyyy})";

            if (from.HasValue)
                return $" ({from:dd.MM.yyyy} sonrası)";

            return $" ({to:dd.MM.yyyy} öncesi)";
        }

        private IQueryable<Data.Entities.WorkItem> ApplyDateFilter(IQueryable<Data.Entities.WorkItem> query)
        {
            var range = GetDateRange();
            var from = range.Item1;
            var to = range.Item2;

            if (from.HasValue)
            {
                query = query.Where(w => w.CreatedAt >= from.Value);
            }
            if (to.HasValue)
            {
                var end = to.Value.AddDays(1);
                query = query.Where(w => w.CreatedAt < end);
            }

            return query;
        }

        // Eski HTML tabanlı rapor metotları (Kapasite, İş Dağılımı, Sprint Performansı) kaldırıldı.

        /// <summary>
        /// Planlanan vs gerçekleşen süreleri grafik olarak gösterir
        /// </summary>
        private void LoadEffortChartReport()
        {
            try
            {
                // Tarih aralığına göre tamamlanan işler
                var range = GetDateRange();
                DateTime? from = range.Item1;
                DateTime? to = range.Item2;

                var query = _context.WorkItems
                    .Where(w => w.CompletedAt.HasValue && w.EffortEstimate.HasValue);
                if (from.HasValue)
                {
                    query = query.Where(w => w.CompletedAt >= from.Value);
                }
                if (to.HasValue)
                {
                    var end = to.Value.AddDays(1);
                    query = query.Where(w => w.CompletedAt < end);
                }

                var list = query
                    .OrderByDescending(w => w.CompletedAt)
                    .Take(30)
                    .Select(w => new
                    {
                        w.Id,
                        w.Title,
                        w.EffortEstimate,
                        w.CreatedAt,
                        w.StartedAt,
                        w.CompletedAt
                    })
                    .ToList()
                    .Select(w => new
                    {
                        Key = $"#{w.Id} " + (w.Title.Length > 25 ? w.Title.Substring(0, 22) + "..." : w.Title),
                        PlannedMinutes = (double)((w.EffortEstimate ?? 0m) * 8m * 60m), // 8 saatlik iş günü varsayımı
                        ActualMinutes = CalculateActualWorkTime(w.Id, w.StartedAt, w.CreatedAt, w.CompletedAt)
                    })
                    .ToList();

                chartEffort.Series.Clear();
                chartEffort.ChartAreas.Clear();
                chartEffort.Legends.Clear();

                if (!list.Any())
                {
                    return;
                }

                var area = new ChartArea("MainArea");
                area.AxisX.Interval = 1;
                area.AxisX.LabelStyle.Angle = -45;
                area.AxisX.LabelStyle.IsStaggered = true;
                area.AxisY.Title = "Süre (dakika)";
                area.BackColor = Color.WhiteSmoke;
                chartEffort.ChartAreas.Add(area);

                var legend = new Legend("Legend");
                legend.Docking = Docking.Top;
                legend.Alignment = System.Drawing.StringAlignment.Center;
                chartEffort.Legends.Add(legend);

                chartEffort.BackColor = Color.White;

                var seriesPlanned = new Series("Planlanan");
                seriesPlanned.ChartType = SeriesChartType.Column;
                seriesPlanned.ChartArea = "MainArea";
                seriesPlanned.Color = Color.FromArgb(52, 152, 219);

                var seriesActual = new Series("Gerçekleşen");
                seriesActual.ChartType = SeriesChartType.Column;
                seriesActual.ChartArea = "MainArea";
                seriesActual.Color = Color.FromArgb(230, 126, 34);

                foreach (var item in list)
                {
                    seriesPlanned.Points.AddXY(item.Key, item.PlannedMinutes);
                    seriesActual.Points.AddXY(item.Key, item.ActualMinutes);
                }

                chartEffort.Series.Add(seriesPlanned);
                chartEffort.Series.Add(seriesActual);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Grafik raporu yüklenirken hata oluştu:\n\n{ex.Message}", "Rapor Hatası",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Detaylı grid tablosu - her iş için satır, özetler ve filtrelerle gerçek rapor görünümü
        /// </summary>
        private void LoadDetailedReport()
        {
            try
            {
                var baseQuery = ApplyDateFilter(_context.WorkItems
                    .Include(w => w.Project)
                    .Include(w => w.Module)
                    .Include(w => w.Sprint));

                var data = baseQuery
                    .OrderByDescending(w => w.CreatedAt)
                    .Select(w => new
                    {
                        w.Id,
                        w.Title,
                        w.Board,
                        w.Status,
                        w.Type,
                        w.Urgency,
                        Project = w.Project != null ? w.Project.Name : "",
                        Module = w.Module != null ? w.Module.Name : "",
                        Sprint = w.Sprint != null ? w.Sprint.Name : "",
                        w.RequestedBy,
                        w.CreatedAt,
                        w.StartedAt,
                        w.CompletedAt,
                        w.EffortEstimate,
                        CycleDays = w.CompletedAt.HasValue
                            ? DbFunctions.DiffDays(w.CreatedAt, w.CompletedAt).Value
                            : (int?)null,
                        ActiveDays = !w.CompletedAt.HasValue
                            ? DbFunctions.DiffDays(w.CreatedAt, DateTime.Now)
                            : (int?)null
                    })
                    .ToList();

                gridDetailed.DataSource = data;

                var view = viewDetailed;
                view.BestFitColumns();

                // Kolon başlıkları
                if (view.Columns["Id"] != null) view.Columns["Id"].Caption = "ID";
                if (view.Columns["Title"] != null) view.Columns["Title"].Caption = "Başlık";
                if (view.Columns["Board"] != null) view.Columns["Board"].Caption = "Pano";
                if (view.Columns["Status"] != null) view.Columns["Status"].Caption = "Durum";
                if (view.Columns["Type"] != null) view.Columns["Type"].Caption = "Tip";
                if (view.Columns["Urgency"] != null) view.Columns["Urgency"].Caption = "Aciliyet";
                if (view.Columns["Project"] != null) view.Columns["Project"].Caption = "Proje";
                if (view.Columns["Module"] != null) view.Columns["Module"].Caption = "Modül";
                if (view.Columns["Sprint"] != null) view.Columns["Sprint"].Caption = "Sprint";
                if (view.Columns["RequestedBy"] != null) view.Columns["RequestedBy"].Caption = "Talep Eden";
                if (view.Columns["CreatedAt"] != null) view.Columns["CreatedAt"].Caption = "Oluşturulma";
                if (view.Columns["StartedAt"] != null) view.Columns["StartedAt"].Caption = "Müdahale Başlangıcı";
                if (view.Columns["CompletedAt"] != null) view.Columns["CompletedAt"].Caption = "Tamamlanma";
                if (view.Columns["EffortEstimate"] != null) view.Columns["EffortEstimate"].Caption = "Efor (gün)";
                if (view.Columns["CycleDays"] != null) view.Columns["CycleDays"].Caption = "Toplam Süre (gün)";
                if (view.Columns["ActiveDays"] != null) view.Columns["ActiveDays"].Caption = "Açık Süre (gün)";

                // Özetler
                view.Columns["Id"].Summary.Clear();
                view.Columns["Id"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "Id", "Toplam İş: {0}");

                if (view.Columns["EffortEstimate"] != null)
                {
                    view.Columns["EffortEstimate"].Summary.Clear();
                    view.Columns["EffortEstimate"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "EffortEstimate", "Toplam Efor: {0:0.0}");
                }

                if (view.Columns["CycleDays"] != null)
                {
                    view.Columns["CycleDays"].Summary.Clear();
                    view.Columns["CycleDays"].Summary.Add(DevExpress.Data.SummaryItemType.Average, "CycleDays", "Ort. Süre: {0:0.0} gün");
                }

                // Gruplama için kullanıcıya alan bırakalım (sürükle-bırak group panel)
                view.OptionsView.ShowGroupPanel = true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Detaylı rapor yüklenirken hata oluştu:\n\n{ex.Message}", "Rapor Hatası",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Günlük efor raporu - Sol panelde günlük özet, sağ panelde seçilen güne göre iş detayı
        /// </summary>
        private void LoadDailyEffortReport()
        {
            try
            {
                var range = GetDateRange();
                DateTime? from = range.Item1?.Date;
                DateTime? to = range.Item2?.Date;
                DateTime? toExclusive = to?.AddDays(1);

                // First, get work items that have development status changes in the date range
                var activitiesQuery = _context.WorkItemActivities
                    .Where(a => a.ActivityType == WorkItemActivityTypes.StatusChange &&
                                (a.NewValue == "Gelistirmede" || a.NewValue == "MudahaleEdiliyor" ||
                                 a.OldValue == "Gelistirmede" || a.OldValue == "MudahaleEdiliyor"));
                
                // Apply date filtering to activities as well for better performance
                if (from.HasValue)
                {
                    activitiesQuery = activitiesQuery.Where(a => a.CreatedAt >= from.Value);
                }
                if (toExclusive.HasValue)
                {
                    activitiesQuery = activitiesQuery.Where(a => a.CreatedAt < toExclusive.Value);
                }
                
                var developmentWorkItemIds = activitiesQuery
                    .Select(a => a.WorkItemId)
                    .Distinct()
                    .ToList();

                // Then query work items with their activities, filtered by the IDs above and date range
                var workItemsQuery = _context.WorkItems
                    .Include(w => w.Project)
                    .Include(w => w.Activities)
                    .Where(w => !w.IsArchived && developmentWorkItemIds.Contains(w.Id));

                if (from.HasValue)
                {
                    workItemsQuery = workItemsQuery.Where(w =>
                        (w.CompletedAt ?? DateTime.Now) >= from.Value);
                }

                if (toExclusive.HasValue)
                {
                    workItemsQuery = workItemsQuery.Where(w =>
                        (w.StartedAt ?? w.CreatedAt) < toExclusive.Value);
                }

                var workItems = workItemsQuery
                    .OrderByDescending(w => w.CreatedAt)
                    .ToList();

                var dailyEntries = new List<DevelopmentDayRecord>();

                foreach (var workItem in workItems)
                {
                    var statusActivities = workItem.Activities?
                        .Where(a => a.ActivityType == WorkItemActivityTypes.StatusChange)
                        .OrderBy(a => a.CreatedAt)
                        .ToList() ?? new List<WorkItemActivity>();

                    var intervals = DevelopmentTimeHelper.CalculateIntervals(workItem, statusActivities);

                    foreach (var interval in intervals)
                    {
                        var intervalStart = interval.Start;
                        var intervalEnd = interval.End;

                        if (from.HasValue && intervalEnd <= from.Value)
                        {
                            continue;
                        }

                        if (toExclusive.HasValue && intervalStart >= toExclusive.Value)
                        {
                            continue;
                        }

                        var clampedStart = intervalStart;
                        var clampedEnd = intervalEnd;

                        if (from.HasValue && clampedStart < from.Value)
                        {
                            clampedStart = from.Value;
                        }

                        if (toExclusive.HasValue && clampedEnd > toExclusive.Value)
                        {
                            clampedEnd = toExclusive.Value;
                        }

                        if (clampedEnd <= clampedStart)
                        {
                            continue;
                        }

                        var dayCursor = clampedStart.Date;
                        while (dayCursor < clampedEnd)
                        {
                            var dayStart = dayCursor;
                            var dayEnd = dayCursor.AddDays(1);

                            var effectiveStart = clampedStart > dayStart ? clampedStart : dayStart;
                            var effectiveEnd = clampedEnd < dayEnd ? clampedEnd : dayEnd;

                            if (effectiveStart < effectiveEnd)
                            {
                                dailyEntries.Add(new DevelopmentDayRecord
                                {
                                    Date = dayStart,
                                    WorkItemId = workItem.Id,
                                    Title = workItem.Title,
                                    ProjectName = workItem.Project != null ? workItem.Project.Name : "",
                                    Minutes = (effectiveEnd - effectiveStart).TotalMinutes,
                                    ActivityType = "Development"
                                });
                            }

                            dayCursor = dayCursor.AddDays(1);
                        }
                    }
                }

                // Get time entries grouped by work item and date to add to work item totals
                var timeEntriesQuery = _context.TimeEntries
                    .Include(t => t.WorkItem)
                    .Include(t => t.Project);

                if (from.HasValue)
                {
                    timeEntriesQuery = timeEntriesQuery.Where(t => t.EntryDate >= from.Value);
                }
                if (toExclusive.HasValue)
                {
                    timeEntriesQuery = timeEntriesQuery.Where(t => t.EntryDate < toExclusive.Value);
                }

                var allTimeEntries = timeEntriesQuery.ToList();

                // Tüm TimeEntry kayıtlarını ayrı ayrı ekle (ActivityType'larını koruyarak)
                foreach (var entry in allTimeEntries)
                {
                    var date = entry.EntryDate.Date;
                    
                    // Check if the date is within our filtered range
                    if (from.HasValue && date < from.Value.Date)
                        continue;
                    if (to.HasValue && date > to.Value.Date)
                        continue;

                    string title;
                    string projectName = entry.Project?.Name ?? "";
                    int recordId;

                    if (entry.WorkItemId.HasValue)
                    {
                        // WorkItem'a bağlı TimeEntry
                        var workItem = entry.WorkItem ?? _context.WorkItems
                            .Include(w => w.Project)
                            .FirstOrDefault(w => w.Id == entry.WorkItemId.Value);
                        
                        if (workItem != null)
                        {
                            var activityLabel = GetActivityTypeLabel(entry.ActivityType);
                            title = $"[{activityLabel}] {workItem.Title}";
                            projectName = workItem.Project?.Name ?? projectName;
                            recordId = entry.Id; // Pozitif ID - TimeEntry
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        // Bağımsız TimeEntry (WorkItem olmadan)
                        var activityLabel = GetActivityTypeLabel(entry.ActivityType);
                        title = !string.IsNullOrEmpty(entry.Subject) 
                            ? $"[{activityLabel}] {entry.Subject}" 
                            : $"[{activityLabel}] {entry.Description?.Substring(0, Math.Min(50, entry.Description?.Length ?? 0))}";
                        recordId = -entry.Id; // Negatif ID - standalone TimeEntry
                    }

                    dailyEntries.Add(new DevelopmentDayRecord
                    {
                        Date = date,
                        WorkItemId = recordId,
                        Title = title,
                        ProjectName = projectName,
                        Minutes = entry.DurationMinutes,
                        ActivityType = entry.ActivityType
                    });
                }

                // Store daily entries for later use in row selection event
                _dailyEntriesCache = dailyEntries;

                // Load daily summary grid (left panel)
                var dailyData = dailyEntries
                    .GroupBy(e => e.Date)
                    .Select(g => new
                    {
                        Tarih = g.Key,
                        TarihStr = g.Key.ToString("dd.MM.yyyy (dddd)"),
                        ToplamDk = Math.Round(g.Sum(e => e.Minutes), 0),
                        IsSayisi = g.Count(e => e.ActivityType == "Development"),
                        TelefonSayisi = g.Count(e => e.ActivityType == TimeEntryActivityTypes.PhoneCall),
                        ToplantiSayisi = g.Count(e => e.ActivityType == TimeEntryActivityTypes.Meeting),
                        DigerSayisi = g.Count(e => e.ActivityType == TimeEntryActivityTypes.Work || e.ActivityType == TimeEntryActivityTypes.Other),
                        ToplamSure = FormatDuration((int)g.Sum(e => e.Minutes))
                    })
                    .OrderByDescending(g => g.Tarih)
                    .ToList();

                gridDailySummary.DataSource = dailyData;

                var view = gridViewDailySummary;
                view.BestFitColumns();

                // Kolon başlıkları ve sıralama
                if (view.Columns["Tarih"] != null) view.Columns["Tarih"].Visible = false;
                if (view.Columns["TarihStr"] != null) { view.Columns["TarihStr"].Caption = "Tarih"; view.Columns["TarihStr"].VisibleIndex = 0; }
                if (view.Columns["ToplamDk"] != null) view.Columns["ToplamDk"].Visible = false;
                if (view.Columns["IsSayisi"] != null) { view.Columns["IsSayisi"].Caption = "🖥️ İş"; view.Columns["IsSayisi"].VisibleIndex = 1; view.Columns["IsSayisi"].Width = 50; }
                if (view.Columns["TelefonSayisi"] != null) { view.Columns["TelefonSayisi"].Caption = "📞 Tel"; view.Columns["TelefonSayisi"].VisibleIndex = 2; view.Columns["TelefonSayisi"].Width = 50; }
                if (view.Columns["ToplantiSayisi"] != null) { view.Columns["ToplantiSayisi"].Caption = "📅 Top"; view.Columns["ToplantiSayisi"].VisibleIndex = 3; view.Columns["ToplantiSayisi"].Width = 50; }
                if (view.Columns["DigerSayisi"] != null) { view.Columns["DigerSayisi"].Caption = "📌 Diğer"; view.Columns["DigerSayisi"].VisibleIndex = 4; view.Columns["DigerSayisi"].Width = 50; }
                if (view.Columns["ToplamSure"] != null) { view.Columns["ToplamSure"].Caption = "⏱️ Toplam"; view.Columns["ToplamSure"].VisibleIndex = 5; }

                // Özetler
                view.Columns["ToplamDk"].Summary.Clear();
                view.Columns["ToplamDk"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "ToplamDk", "Toplam: {0} dk");

                if (view.Columns["IsSayisi"] != null)
                {
                    view.Columns["IsSayisi"].Summary.Clear();
                    view.Columns["IsSayisi"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "IsSayisi", "{0}");
                }
                if (view.Columns["TelefonSayisi"] != null)
                {
                    view.Columns["TelefonSayisi"].Summary.Clear();
                    view.Columns["TelefonSayisi"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TelefonSayisi", "{0}");
                }
                if (view.Columns["ToplantiSayisi"] != null)
                {
                    view.Columns["ToplantiSayisi"].Summary.Clear();
                    view.Columns["ToplantiSayisi"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "ToplantiSayisi", "{0}");
                }
                if (view.Columns["DigerSayisi"] != null)
                {
                    view.Columns["DigerSayisi"].Summary.Clear();
                    view.Columns["DigerSayisi"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "DigerSayisi", "{0}");
                }

                // Update summary cards
                var totalMinutes = dailyData.Sum(d => d.ToplamDk);
                var activeDays = dailyData.Count;
                var avgDaily = activeDays > 0 ? totalMinutes / activeDays : 0;

                lblTotalHoursValue.Text = FormatDuration((int)totalMinutes);
                lblAvgDailyValue.Text = FormatDuration((int)avgDaily);
                lblActiveDaysValue.Text = activeDays.ToString();

                // Clear right panel initially
                gridWorkItemDetails.DataSource = null;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Günlük efor raporu yüklenirken hata oluştu:\n\n{ex.Message}", "Rapor Hatası",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<DevelopmentDayRecord> _dailyEntriesCache;

        /// <summary>
        /// Sol panelde bir tarih seçildiğinde sağ paneli doldur
        /// </summary>
        private void gridViewDailySummary_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle < 0 || _dailyEntriesCache == null)
                {
                    gridWorkItemDetails.DataSource = null;
                    return;
                }

                var row = gridViewDailySummary.GetRow(e.FocusedRowHandle);
                if (row == null) return;

                var selectedDate = (DateTime)row.GetType().GetProperty("Tarih").GetValue(row);

                // Filter daily entries for this date
                var workItemsForDate = _dailyEntriesCache
                    .Where(x => x.Date == selectedDate)
                    .GroupBy(x => new { x.WorkItemId, x.Title, x.ProjectName })
                    .Select(g => new
                    {
                        IsId = g.Key.WorkItemId,
                        IsBaslik = g.Key.Title,
                        Proje = g.Key.ProjectName,
                        ToplamDk = Math.Round(g.Sum(y => y.Minutes), 0),
                        ToplamSure = FormatDuration((int)g.Sum(y => y.Minutes))
                    })
                    .OrderByDescending(i => i.ToplamDk)
                    .ToList();

                gridWorkItemDetails.DataSource = workItemsForDate;

                var detailView = gridViewWorkItemDetails;
                detailView.BestFitColumns();

                if (detailView.Columns["IsId"] != null) detailView.Columns["IsId"].Caption = "İş ID";
                if (detailView.Columns["IsBaslik"] != null) detailView.Columns["IsBaslik"].Caption = "İş Başlığı";
                if (detailView.Columns["Proje"] != null) detailView.Columns["Proje"].Caption = "Proje";
                if (detailView.Columns["ToplamDk"] != null) detailView.Columns["ToplamDk"].Visible = false;
                if (detailView.Columns["ToplamSure"] != null) detailView.Columns["ToplamSure"].Caption = "Süre";

                detailView.Columns["ToplamDk"].Summary.Clear();
                detailView.Columns["ToplamDk"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "ToplamDk", "Toplam: {0} dk");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"İş detayları yüklenirken hata oluştu:\n\n{ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string FormatDuration(int minutes)
        {
            if (minutes == 0) return "-";

            var hours = minutes / 60;
            var mins = minutes % 60;

            if (hours > 0 && mins > 0)
                return $"{hours} sa {mins} dk";
            else if (hours > 0)
                return $"{hours} sa";
            else
                return $"{mins} dk";
        }

        /// <summary>
        /// Bir iş öğesi için gerçek çalışma süresini hesaplar
        /// StatusChange aktivitelerine göre geliştirme sürelerini hesaplar
        /// </summary>
        private double CalculateActualWorkTime(int workItemId, DateTime? startedAt, DateTime? createdAt, DateTime? endTime)
        {
            try
            {
                // Aktiviteleri çek
                var activities = _context.WorkItemActivities
                    .Where(a => a.WorkItemId == workItemId && a.ActivityType == WorkItemActivityTypes.StatusChange)
                    .OrderBy(a => a.CreatedAt)
                    .ToList();

                TimeSpan totalDevTime = TimeSpan.Zero;
                DateTime? devStartTime = null;
                DateTime? lastExitTime = null;

                if (startedAt.HasValue)
                {
                    devStartTime = startedAt.Value;
                }
                else if (createdAt.HasValue)
                {
                    var firstActivity = activities.FirstOrDefault();
                    if (firstActivity != null
                        && createdAt.Value <= firstActivity.CreatedAt
                        && IsDevelopmentStatus(firstActivity.OldValue))
                    {
                        devStartTime = createdAt.Value;
                    }
                }

                foreach (var activity in activities)
                {
                    var enteredDevelopment = IsDevelopmentStatus(activity.NewValue) && !IsDevelopmentStatus(activity.OldValue);
                    var exitedDevelopment = IsDevelopmentStatus(activity.OldValue) && !IsDevelopmentStatus(activity.NewValue);

                    // Geliştirmeye giriş
                    if (enteredDevelopment && devStartTime == null)
                    {
                        devStartTime = activity.CreatedAt;
                    }
                    // Geliştirmeden çıkış
                    else if (exitedDevelopment && devStartTime != null)
                    {
                        // Çakışan zamanları önlemek için son çıkış zamanını kontrol et
                        if (lastExitTime.HasValue && activity.CreatedAt <= lastExitTime.Value)
                        {
                            continue; // Çakışan kayıtları atla
                        }
                        
                        totalDevTime += activity.CreatedAt - devStartTime.Value;
                        devStartTime = null;
                        lastExitTime = activity.CreatedAt;
                    }
                }

                // Şu an hala geliştirmedeyse ve bitiş zamanı yoksa
                if (devStartTime != null)
                {
                    var currentTime = endTime ?? DateTime.Now;
                    // Çakışan zamanları önlemek için son çıkış zamanını kontrol et
                    if (!lastExitTime.HasValue || currentTime > lastExitTime.Value)
                    {
                        totalDevTime += currentTime - devStartTime.Value;
                    }
                }

                return totalDevTime.TotalMinutes;
            }
            catch (Exception)
            {
                // Hata durumunda basit hesaplama
                if (startedAt.HasValue && endTime.HasValue)
                    return (endTime.Value - startedAt.Value).TotalMinutes;

                if (createdAt.HasValue && endTime.HasValue)
                    return (endTime.Value - createdAt.Value).TotalMinutes;
                return 0;
            }
        }

        private static bool IsDevelopmentStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return false;

            return DevelopmentStatuses.Any(s =>
                status.Equals(s, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Aktivite tipini kullanıcı dostu etiketle döner
        /// </summary>
        private static string GetActivityTypeLabel(string activityType)
        {
            switch (activityType)
            {
                case TimeEntryActivityTypes.PhoneCall:
                    return "Telefon";
                case TimeEntryActivityTypes.Meeting:
                    return "Toplantı";
                case TimeEntryActivityTypes.Work:
                    return "İş";
                case TimeEntryActivityTypes.Other:
                default:
                    return "Diğer";
            }
        }

        private class DevelopmentDayRecord
        {
            public DateTime Date { get; set; }
            public int WorkItemId { get; set; }
            public string Title { get; set; }
            public string ProjectName { get; set; }
            public double Minutes { get; set; }
            public string ActivityType { get; set; } // "Development", "PhoneCall", "Meeting", "Work", "Other"
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAllReports();
        }

        #region Activity Timeline

        private void btnTimelinePrev_Click(object sender, EventArgs e)
        {
            if (dtTimelineDate.EditValue is DateTime date)
            {
                dtTimelineDate.EditValue = date.AddDays(-1);
            }
        }

        private void btnTimelineNext_Click(object sender, EventArgs e)
        {
            if (dtTimelineDate.EditValue is DateTime date)
            {
                dtTimelineDate.EditValue = date.AddDays(1);
            }
        }

        private void dtTimelineDate_EditValueChanged(object sender, EventArgs e)
        {
            LoadActivityTimeline();
        }

        private void LoadActivityTimeline()
        {
            try
            {
                flowTimelinePanel.Controls.Clear();

                if (!(dtTimelineDate.EditValue is DateTime selectedDate))
                {
                    lblTimelineSummary.Text = "";
                    return;
                }

                var activities = GetDayActivities(selectedDate);

                if (!activities.Any())
                {
                    lblTimelineSummary.Text = "Bu tarihte aktivite bulunamadı.";
                    var emptyLabel = new Label
                    {
                        Text = "📭 Bu gün için aktivite kaydı yok.",
                        Font = new Font("Segoe UI", 12, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        AutoSize = true,
                        Padding = new Padding(50, 30, 0, 0)
                    };
                    flowTimelinePanel.Controls.Add(emptyLabel);
                    return;
                }

                // Summary oluştur
                var workCount = activities.Count(a => a.Category == "WorkItem");
                var phoneCount = activities.Count(a => a.Category == "Phone");
                var meetingCount = activities.Count(a => a.Category == "Meeting");
                var otherCount = activities.Count(a => a.Category == "Other" || a.Category == "TimeEntry");
                var totalMinutes = activities.Where(a => a.DurationMinutes > 0).Sum(a => a.DurationMinutes);

                lblTimelineSummary.Text = $"📊 {activities.Count} aktivite | 🖥️ {workCount} iş | 📞 {phoneCount} telefon | 📅 {meetingCount} toplantı | ⏱️ {FormatDuration(totalMinutes)}";

                // Timeline kartlarını oluştur
                foreach (var activity in activities.OrderBy(a => a.Time))
                {
                    var card = CreateTimelineCard(activity);
                    flowTimelinePanel.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Aktivite akışı yüklenirken hata oluştu:\n\n{ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<TimelineActivity> GetDayActivities(DateTime date)
        {
            var activities = new List<TimelineActivity>();
            var startOfDay = date.Date;
            var endOfDay = startOfDay.AddDays(1);

            // 1. WorkItem Activities (Comment hariç - durum değişikliği, oluşturma, güncelleme vb.)
            var workItemActivities = _context.WorkItemActivities
                .Include(a => a.WorkItem)
                .Include(a => a.WorkItem.Project)
                .Where(a => a.CreatedAt >= startOfDay && a.CreatedAt < endOfDay)
                .Where(a => a.ActivityType != WorkItemActivityTypes.Comment) // Yorumları hariç tut
                .OrderBy(a => a.CreatedAt)
                .ToList();

            foreach (var activity in workItemActivities)
            {
                var (icon, color, description) = GetWorkItemActivityInfo(activity);
                activities.Add(new TimelineActivity
                {
                    Time = activity.CreatedAt,
                    Icon = icon,
                    Color = color,
                    Title = activity.WorkItem?.Title ?? "Bilinmeyen İş",
                    Description = description,
                    ProjectName = activity.WorkItem?.Project?.Name ?? "",
                    Category = "WorkItem",
                    WorkItemId = activity.WorkItemId,
                    DurationMinutes = 0
                });
            }

            // 2. TimeEntries (Telefon, Toplantı, İş, Diğer)
            var timeEntries = _context.TimeEntries
                .Include(t => t.WorkItem)
                .Include(t => t.Project)
                .Include(t => t.Meeting)
                .Include(t => t.Person)
                .Where(t => t.EntryDate >= startOfDay && t.EntryDate < endOfDay)
                .OrderBy(t => t.EntryDate)
                .ToList();

            foreach (var entry in timeEntries)
            {
                var (icon, color, category) = GetTimeEntryInfo(entry.ActivityType);
                var title = GetTimeEntryTitle(entry);
                var description = GetTimeEntryDescription(entry);

                activities.Add(new TimelineActivity
                {
                    Time = entry.EntryDate,
                    Icon = icon,
                    Color = color,
                    Title = title,
                    Description = description,
                    ProjectName = entry.Project?.Name ?? entry.WorkItem?.Project?.Name ?? "",
                    Category = category,
                    WorkItemId = entry.WorkItemId,
                    DurationMinutes = entry.DurationMinutes
                });
            }

            // 3. Meetings (Toplantılar)
            var meetings = _context.Meetings
                .Where(m => m.MeetingDate >= startOfDay && m.MeetingDate < endOfDay)
                .OrderBy(m => m.MeetingDate)
                .ToList();

            foreach (var meeting in meetings)
            {
                // Eğer bu toplantı zaten TimeEntry olarak eklenmişse atlayalım
                var alreadyAdded = timeEntries.Any(t => t.MeetingId == meeting.Id);
                if (!alreadyAdded)
                {
                    activities.Add(new TimelineActivity
                    {
                        Time = meeting.MeetingDate,
                        Icon = "📅",
                        Color = Color.FromArgb(156, 39, 176),
                        Title = meeting.Subject,
                        Description = $"Katılımcılar: {meeting.Participants}",
                        ProjectName = "",
                        Category = "Meeting",
                        DurationMinutes = 0
                    });
                }
            }

            return activities;
        }

        private (string icon, Color color, string description) GetWorkItemActivityInfo(WorkItemActivity activity)
        {
            switch (activity.ActivityType)
            {
                case WorkItemActivityTypes.Created:
                    return ("🆕", Color.FromArgb(76, 175, 80), "Yeni iş oluşturuldu");
                
                case WorkItemActivityTypes.StatusChange:
                    var statusIcon = GetStatusChangeIcon(activity.OldValue, activity.NewValue);
                    var statusColor = GetStatusChangeColor(activity.NewValue);
                    return (statusIcon, statusColor, $"Durum: {activity.OldValue} → {activity.NewValue}");
                
                case WorkItemActivityTypes.AssignmentChange:
                    return ("👤", Color.FromArgb(33, 150, 243), $"Atama: {activity.OldValue} → {activity.NewValue}");
                
                case WorkItemActivityTypes.PriorityChange:
                    return ("⚡", Color.FromArgb(255, 152, 0), $"Öncelik: {activity.OldValue} → {activity.NewValue}");
                
                case WorkItemActivityTypes.EstimateChange:
                    return ("⏰", Color.FromArgb(0, 188, 212), $"Efor: {activity.OldValue} → {activity.NewValue}");
                
                case WorkItemActivityTypes.FieldUpdate:
                    return ("📝", Color.FromArgb(158, 158, 158), activity.Description ?? "Alan güncellendi");
                
                default:
                    return ("📌", Color.FromArgb(158, 158, 158), activity.Description ?? "Aktivite");
            }
        }

        private string GetStatusChangeIcon(string oldValue, string newValue)
        {
            if (newValue == "Gelistirmede" || newValue == "MudahaleEdiliyor")
                return "🟢";
            if (newValue == "Test" || newValue == "Review")
                return "🔵";
            if (newValue == "Tamamlandi" || newValue == "Done")
                return "✅";
            if (newValue == "Beklemede")
                return "⏸️";
            if (newValue == "Iptal")
                return "❌";
            return "🔄";
        }

        private Color GetStatusChangeColor(string newValue)
        {
            if (newValue == "Gelistirmede" || newValue == "MudahaleEdiliyor")
                return Color.FromArgb(76, 175, 80);
            if (newValue == "Test" || newValue == "Review")
                return Color.FromArgb(33, 150, 243);
            if (newValue == "Tamamlandi" || newValue == "Done")
                return Color.FromArgb(0, 150, 136);
            if (newValue == "Beklemede")
                return Color.FromArgb(255, 193, 7);
            if (newValue == "Iptal")
                return Color.FromArgb(244, 67, 54);
            return Color.FromArgb(158, 158, 158);
        }

        private (string icon, Color color, string category) GetTimeEntryInfo(string activityType)
        {
            switch (activityType)
            {
                case TimeEntryActivityTypes.PhoneCall:
                    return ("📞", Color.FromArgb(255, 152, 0), "Phone");
                case TimeEntryActivityTypes.Meeting:
                    return ("📅", Color.FromArgb(156, 39, 176), "Meeting");
                case TimeEntryActivityTypes.Work:
                    return ("💼", Color.FromArgb(33, 150, 243), "TimeEntry");
                default:
                    return ("📌", Color.FromArgb(158, 158, 158), "Other");
            }
        }

        private string GetTimeEntryTitle(TimeEntry entry)
        {
            if (!string.IsNullOrEmpty(entry.Subject))
                return entry.Subject;
            
            if (entry.WorkItem != null)
                return entry.WorkItem.Title;
            
            if (entry.Meeting != null)
                return entry.Meeting.Subject;
            
            if (!string.IsNullOrEmpty(entry.ContactName))
                return $"Telefon: {entry.ContactName}";
            
            if (entry.Person != null)
                return $"Telefon: {entry.Person.Name}";
            
            return entry.ActivityType == TimeEntryActivityTypes.PhoneCall ? "Telefon Görüşmesi" : "Aktivite";
        }

        private string GetTimeEntryDescription(TimeEntry entry)
        {
            var parts = new List<string>();
            
            if (entry.DurationMinutes > 0)
                parts.Add($"⏱️ {FormatDuration(entry.DurationMinutes)}");
            
            if (!string.IsNullOrEmpty(entry.Description))
            {
                var desc = entry.Description.Length > 100 
                    ? entry.Description.Substring(0, 97) + "..." 
                    : entry.Description;
                parts.Add(desc);
            }
            
            if (!string.IsNullOrEmpty(entry.PhoneNumber))
                parts.Add($"📱 {entry.PhoneNumber}");
            
            return string.Join(" | ", parts);
        }

        private Panel CreateTimelineCard(TimelineActivity activity)
        {
            var cardPanel = new Panel
            {
                Width = 1100,
                Height = 85,
                Margin = new Padding(0, 5, 0, 5),
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };

            // Sol taraf - Saat ve çizgi
            var timeLabel = new Label
            {
                Text = activity.Time.ToString("HH:mm"),
                Font = new Font("Consolas", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 100, 100),
                Location = new Point(10, 10),
                AutoSize = true
            };
            cardPanel.Controls.Add(timeLabel);

            // Dikey çizgi
            var linePanel = new Panel
            {
                BackColor = activity.Color,
                Location = new Point(70, 0),
                Size = new Size(4, 85)
            };
            cardPanel.Controls.Add(linePanel);

            // İkon dairesi
            var iconLabel = new Label
            {
                Text = activity.Icon,
                Font = new Font("Segoe UI Emoji", 16),
                Location = new Point(85, 25),
                AutoSize = true
            };
            cardPanel.Controls.Add(iconLabel);

            // Başlık
            var titleLabel = new Label
            {
                Text = activity.Title,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 33, 33),
                Location = new Point(130, 8),
                MaximumSize = new Size(700, 0),
                AutoSize = true
            };
            cardPanel.Controls.Add(titleLabel);

            // WorkItem ID badge
            if (activity.WorkItemId.HasValue)
            {
                var idLabel = new Label
                {
                    Text = $"#{activity.WorkItemId}",
                    Font = new Font("Segoe UI", 8),
                    ForeColor = Color.White,
                    BackColor = activity.Color,
                    Padding = new Padding(4, 2, 4, 2),
                    Location = new Point(130 + titleLabel.PreferredWidth + 10, 10),
                    AutoSize = true
                };
                cardPanel.Controls.Add(idLabel);
            }

            // Süre badge
            if (activity.DurationMinutes > 0)
            {
                var durationLabel = new Label
                {
                    Text = $"⏱️ {FormatDuration(activity.DurationMinutes)}",
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.FromArgb(0, 122, 204),
                    Location = new Point(850, 10),
                    AutoSize = true
                };
                cardPanel.Controls.Add(durationLabel);
            }

            // Açıklama
            var descLabel = new Label
            {
                Text = activity.Description,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(100, 100, 100),
                Location = new Point(130, 35),
                MaximumSize = new Size(800, 0),
                AutoSize = true
            };
            cardPanel.Controls.Add(descLabel);

            // Proje adı
            if (!string.IsNullOrEmpty(activity.ProjectName))
            {
                var projectLabel = new Label
                {
                    Text = $"📁 {activity.ProjectName}",
                    Font = new Font("Segoe UI", 8),
                    ForeColor = Color.FromArgb(130, 130, 130),
                    Location = new Point(130, 60),
                    AutoSize = true
                };
                cardPanel.Controls.Add(projectLabel);
            }

            // Hover efekti
            cardPanel.MouseEnter += (s, e) => cardPanel.BackColor = Color.FromArgb(245, 247, 250);
            cardPanel.MouseLeave += (s, e) => cardPanel.BackColor = Color.White;

            // Tüm child kontrollerine mouse event'i ekle
            foreach (Control ctrl in cardPanel.Controls)
            {
                ctrl.MouseEnter += (s, e) => cardPanel.BackColor = Color.FromArgb(245, 247, 250);
                ctrl.MouseLeave += (s, e) => cardPanel.BackColor = Color.White;
            }

            return cardPanel;
        }

        private class TimelineActivity
        {
            public DateTime Time { get; set; }
            public string Icon { get; set; }
            public Color Color { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string ProjectName { get; set; }
            public string Category { get; set; }
            public int? WorkItemId { get; set; }
            public int DurationMinutes { get; set; }
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

