using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using work_tracker.Data;
using work_tracker.Data.Entities;
using work_tracker.Helpers;

namespace work_tracker.Forms
{
    public partial class DashboardForm : XtraForm
    {
        private DateTime _selectedDate = DateTime.Today;
        private DateTime _calendarStartDate;
        private List<DailyWorkSummary> _dailySummaries = new List<DailyWorkSummary>();
        private List<OutlookCalendarItem> _outlookMeetings = new List<OutlookCalendarItem>();

        public DashboardForm()
        {
            InitializeComponent();
            this.Text = "📊 Dashboard";
            this.WindowState = FormWindowState.Maximized;
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Ay başına git
                _calendarStartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                
                LoadDashboardData();
                UpdateCalendar();
                SelectDate(_selectedDate);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Dashboard yüklenirken hata");
                XtraMessageBox.Show(
                    "Dashboard yüklenirken hata oluştu:\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tüm dashboard verilerini yükle
        /// </summary>
        private void LoadDashboardData()
        {
            LoadStatistics();
            LoadCalendarData();
            LoadOutlookMeetings();
        }

        /// <summary>
        /// İstatistik kartlarını doldur
        /// </summary>
        private void LoadStatistics()
        {
            using (var db = new WorkTrackerDbContext())
            {
                var today = DateTime.Today;
                var weekStart = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
                var monthStart = new DateTime(today.Year, today.Month, 1);

                // Bugünün işleri
                var todayItems = db.WorkItems
                    .Where(w => !w.IsArchived)
                    .Where(w => DbFunctions.TruncateTime(w.CreatedAt) == today)
                    .Count();

                var todayCompleted = db.WorkItems
                    .Where(w => !w.IsArchived)
                    .Where(w => w.CompletedAt.HasValue && DbFunctions.TruncateTime(w.CompletedAt) == today)
                    .Count();

                // Bu haftanın işleri
                var weekItems = db.WorkItems
                    .Where(w => !w.IsArchived)
                    .Where(w => w.CreatedAt >= weekStart)
                    .Count();

                var weekCompleted = db.WorkItems
                    .Where(w => !w.IsArchived)
                    .Where(w => w.CompletedAt.HasValue && w.CompletedAt >= weekStart)
                    .Count();

                // Aktif işler
                var activeItems = db.WorkItems
                    .Where(w => !w.IsArchived)
                    .Where(w => w.Status == "Gelistirmede" || w.Status == "MudahaleEdiliyor" || w.Status == "Testte")
                    .Count();

                // Bekleyen işler
                var pendingItems = db.WorkItems
                    .Where(w => !w.IsArchived)
                    .Where(w => w.Status == "Bekliyor" || w.Status == "SprintBacklog" || w.Status == "Triage")
                    .Count();

                // Bugünkü toplantılar
                var todayMeetings = db.Meetings
                    .Where(m => DbFunctions.TruncateTime(m.MeetingDate) == today)
                    .Count();

                // UI Güncelle
                lblTodayItems.Text = todayItems.ToString();
                lblTodayCompleted.Text = todayCompleted.ToString();
                lblWeekItems.Text = weekItems.ToString();
                lblWeekCompleted.Text = weekCompleted.ToString();
                lblActiveItems.Text = activeItems.ToString();
                lblPendingItems.Text = pendingItems.ToString();
                lblTodayMeetings.Text = todayMeetings.ToString();
            }
        }

        /// <summary>
        /// Takvim verilerini yükle (günlük iş sayıları)
        /// </summary>
        private void LoadCalendarData()
        {
            _dailySummaries.Clear();

            var startDate = _calendarStartDate;
            var endDate = startDate.AddMonths(1).AddDays(-1);
            var endDatePlusOne = endDate.AddDays(1); // LINQ sorgusu dışında hesapla

            using (var db = new WorkTrackerDbContext())
            {
                // Oluşturulan işler
                var createdItems = db.WorkItems
                    .Where(w => !w.IsArchived)
                    .Where(w => w.CreatedAt >= startDate && w.CreatedAt <= endDatePlusOne)
                    .GroupBy(w => DbFunctions.TruncateTime(w.CreatedAt))
                    .Select(g => new { Date = g.Key, Count = g.Count() })
                    .ToList();

                // Tamamlanan işler
                var completedItems = db.WorkItems
                    .Where(w => !w.IsArchived)
                    .Where(w => w.CompletedAt.HasValue && w.CompletedAt >= startDate && w.CompletedAt <= endDatePlusOne)
                    .GroupBy(w => DbFunctions.TruncateTime(w.CompletedAt))
                    .Select(g => new { Date = g.Key, Count = g.Count() })
                    .ToList();

                // Toplantılar
                var meetings = db.Meetings
                    .Where(m => m.MeetingDate >= startDate && m.MeetingDate <= endDatePlusOne)
                    .GroupBy(m => DbFunctions.TruncateTime(m.MeetingDate))
                    .Select(g => new { Date = g.Key, Count = g.Count() })
                    .ToList();

                // Gün gün özet oluştur
                for (var date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    var summary = new DailyWorkSummary
                    {
                        Date = date,
                        CreatedCount = createdItems.FirstOrDefault(c => c.Date == date)?.Count ?? 0,
                        CompletedCount = completedItems.FirstOrDefault(c => c.Date == date)?.Count ?? 0,
                        MeetingCount = meetings.FirstOrDefault(c => c.Date == date)?.Count ?? 0
                    };
                    _dailySummaries.Add(summary);
                }
            }
        }

        /// <summary>
        /// Outlook takvim verilerini yükle
        /// </summary>
        private void LoadOutlookMeetings()
        {
            try
            {
                var startDate = _calendarStartDate;
                var endDate = startDate.AddMonths(1);
                _outlookMeetings = OutlookHelper.GetCalendarItems(startDate, endDate);
            }
            catch (Exception ex)
            {
                Logger.Warning($"Outlook takvimi yüklenemedi: {ex.Message}");
                _outlookMeetings = new List<OutlookCalendarItem>();
            }
        }

        /// <summary>
        /// Takvim görünümünü güncelle
        /// </summary>
        private void UpdateCalendar()
        {
            lblMonthYear.Text = _calendarStartDate.ToString("MMMM yyyy");
            
            // Takvim günlerini oluştur
            panelCalendarDays.Controls.Clear();

            var firstDayOfMonth = _calendarStartDate;
            var daysInMonth = DateTime.DaysInMonth(firstDayOfMonth.Year, firstDayOfMonth.Month);
            
            // Ayın ilk gününün haftanın hangi günü olduğunu bul
            var startDayOfWeek = (int)firstDayOfMonth.DayOfWeek;
            if (startDayOfWeek == 0) startDayOfWeek = 7; // Pazar = 7
            startDayOfWeek--; // 0-indexed yap (Pazartesi = 0)

            var cellWidth = 80;
            var cellHeight = 70;
            var padding = 2;

            // Günleri oluştur
            for (int i = 0; i < daysInMonth; i++)
            {
                var date = firstDayOfMonth.AddDays(i);
                var col = (startDayOfWeek + i) % 7;
                var row = (startDayOfWeek + i) / 7;

                var dayPanel = CreateDayCell(date, col * (cellWidth + padding), row * (cellHeight + padding), cellWidth, cellHeight);
                panelCalendarDays.Controls.Add(dayPanel);
            }
        }

        /// <summary>
        /// Takvim gün hücresi oluştur
        /// </summary>
        private Panel CreateDayCell(DateTime date, int x, int y, int width, int height)
        {
            var summary = _dailySummaries.FirstOrDefault(s => s.Date == date);
            var outlookCount = _outlookMeetings.Count(m => m.Start.Date == date);
            var isToday = date == DateTime.Today;
            var isSelected = date == _selectedDate;
            var isWeekend = date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;

            var panel = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
                Tag = date
            };

            // Arka plan rengi
            if (isSelected)
                panel.BackColor = Color.FromArgb(224, 247, 250);
            else if (isToday)
                panel.BackColor = Color.FromArgb(255, 249, 196);
            else if (isWeekend)
                panel.BackColor = Color.FromArgb(250, 250, 250);
            else
                panel.BackColor = Color.White;

            // Gün numarası
            var lblDay = new Label
            {
                Text = date.Day.ToString(),
                Font = new Font("Segoe UI", 11, isToday ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = isToday ? Color.FromArgb(0, 150, 136) : (isWeekend ? Color.Gray : Color.Black),
                Location = new Point(4, 2),
                AutoSize = true
            };
            panel.Controls.Add(lblDay);

            // İstatistikler
            var yPos = 22;
            if (summary != null && summary.CreatedCount > 0)
            {
                var lblCreated = new Label
                {
                    Text = $"📥 {summary.CreatedCount}",
                    Font = new Font("Segoe UI", 8),
                    ForeColor = Color.FromArgb(33, 150, 243),
                    Location = new Point(4, yPos),
                    AutoSize = true
                };
                panel.Controls.Add(lblCreated);
                yPos += 14;
            }

            if (summary != null && summary.CompletedCount > 0)
            {
                var lblCompleted = new Label
                {
                    Text = $"✅ {summary.CompletedCount}",
                    Font = new Font("Segoe UI", 8),
                    ForeColor = Color.FromArgb(76, 175, 80),
                    Location = new Point(4, yPos),
                    AutoSize = true
                };
                panel.Controls.Add(lblCompleted);
                yPos += 14;
            }

            var totalMeetings = (summary?.MeetingCount ?? 0) + outlookCount;
            if (totalMeetings > 0)
            {
                var lblMeetings = new Label
                {
                    Text = $"📅 {totalMeetings}",
                    Font = new Font("Segoe UI", 8),
                    ForeColor = Color.FromArgb(255, 152, 0),
                    Location = new Point(4, yPos),
                    AutoSize = true
                };
                panel.Controls.Add(lblMeetings);
            }

            // Click event
            panel.Click += (s, e) => SelectDate(date);
            foreach (Control ctrl in panel.Controls)
            {
                ctrl.Click += (s, e) => SelectDate(date);
            }

            return panel;
        }

        /// <summary>
        /// Belirtilen tarihi seç ve detayları göster
        /// </summary>
        private void SelectDate(DateTime date)
        {
            _selectedDate = date;
            UpdateCalendar();
            LoadDayDetails(date);
        }

        /// <summary>
        /// Seçilen günün detaylarını yükle
        /// </summary>
        private void LoadDayDetails(DateTime date)
        {
            lblSelectedDate.Text = date.ToString("dd MMMM yyyy, dddd");

            using (var db = new WorkTrackerDbContext())
            {
                // O gün oluşturulan işler
                var createdItems = db.WorkItems
                    .Include(w => w.Project)
                    .Where(w => !w.IsArchived)
                    .Where(w => DbFunctions.TruncateTime(w.CreatedAt) == date)
                    .OrderByDescending(w => w.CreatedAt)
                    .Select(w => new
                    {
                        w.Id,
                        w.Title,
                        ProjectName = w.Project != null ? w.Project.Name : "",
                        w.Type,
                        w.Status,
                        w.CreatedAt,
                        Category = "Oluşturulan"
                    })
                    .ToList();

                // O gün tamamlanan işler
                var completedItems = db.WorkItems
                    .Include(w => w.Project)
                    .Where(w => !w.IsArchived)
                    .Where(w => w.CompletedAt.HasValue && DbFunctions.TruncateTime(w.CompletedAt) == date)
                    .OrderByDescending(w => w.CompletedAt)
                    .Select(w => new
                    {
                        w.Id,
                        w.Title,
                        ProjectName = w.Project != null ? w.Project.Name : "",
                        w.Type,
                        w.Status,
                        w.CreatedAt,
                        Category = "Tamamlanan"
                    })
                    .ToList();

                // Birleştir
                var allItems = createdItems.Concat(completedItems).ToList();
                gridWorkItems.DataSource = allItems;

                // Günün toplantıları
                var meetings = db.Meetings
                    .Where(m => DbFunctions.TruncateTime(m.MeetingDate) == date)
                    .OrderBy(m => m.MeetingDate)
                    .Select(m => new
                    {
                        m.Id,
                        m.Subject,
                        m.MeetingDate,
                        m.Participants,
                        Source = "Work Tracker"
                    })
                    .ToList();

                // Outlook toplantıları ekle
                var outlookMeetings = _outlookMeetings
                    .Where(m => m.Start.Date == date)
                    .OrderBy(m => m.Start)
                    .Select(m => new
                    {
                        Id = 0,
                        Subject = m.Subject,
                        MeetingDate = m.Start,
                        Participants = m.RequiredAttendees,
                        Source = "Outlook"
                    })
                    .ToList();

                var allMeetings = meetings.Cast<object>().Concat(outlookMeetings.Cast<object>()).ToList();
                gridMeetings.DataSource = meetings.Concat(outlookMeetings).ToList();
            }
        }

        #region Event Handlers

        private void btnPrevMonth_Click(object sender, EventArgs e)
        {
            _calendarStartDate = _calendarStartDate.AddMonths(-1);
            LoadCalendarData();
            LoadOutlookMeetings();
            UpdateCalendar();
        }

        private void btnNextMonth_Click(object sender, EventArgs e)
        {
            _calendarStartDate = _calendarStartDate.AddMonths(1);
            LoadCalendarData();
            LoadOutlookMeetings();
            UpdateCalendar();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            _calendarStartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            _selectedDate = DateTime.Today;
            LoadCalendarData();
            LoadOutlookMeetings();
            UpdateCalendar();
            LoadDayDetails(_selectedDate);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDashboardData();
            UpdateCalendar();
            LoadDayDetails(_selectedDate);
            XtraMessageBox.Show("Dashboard yenilendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSyncOutlook_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                LoadOutlookMeetings();
                UpdateCalendar();
                LoadDayDetails(_selectedDate);
                XtraMessageBox.Show(
                    $"{_outlookMeetings.Count} Outlook toplantısı senkronize edildi.",
                    "Outlook Sync",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    "Outlook senkronizasyonu başarısız:\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void gridViewWorkItems_DoubleClick(object sender, EventArgs e)
        {
            var view = gridWorkItems.MainView as GridView;
            if (view == null) return;

            var focusedRow = view.GetFocusedRow();
            if (focusedRow == null) return;

            var idProperty = focusedRow.GetType().GetProperty("Id");
            if (idProperty == null) return;

            var workItemId = (int)idProperty.GetValue(focusedRow);
            if (workItemId > 0)
            {
                var detailForm = new WorkItemDetailForm(workItemId);
                detailForm.MdiParent = this.MdiParent;
                detailForm.Show();
            }
        }

        private void gridViewMeetings_DoubleClick(object sender, EventArgs e)
        {
            var view = gridMeetings.MainView as GridView;
            if (view == null) return;

            var focusedRow = view.GetFocusedRow();
            if (focusedRow == null) return;

            var idProperty = focusedRow.GetType().GetProperty("Id");
            var sourceProperty = focusedRow.GetType().GetProperty("Source");
            
            if (idProperty == null || sourceProperty == null) return;

            var meetingId = (int)idProperty.GetValue(focusedRow);
            var source = sourceProperty.GetValue(focusedRow)?.ToString();

            if (source == "Outlook")
            {
                XtraMessageBox.Show(
                    "Bu bir Outlook toplantısıdır. Outlook'ta açmak için Outlook uygulamanızı kullanın.",
                    "Bilgi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if (meetingId > 0)
            {
                var detailForm = new MeetingDetailForm(meetingId);
                detailForm.MdiParent = this.MdiParent;
                detailForm.Show();
            }
        }

        #endregion
    }

    /// <summary>
    /// Günlük iş özeti modeli
    /// </summary>
    public class DailyWorkSummary
    {
        public DateTime Date { get; set; }
        public int CreatedCount { get; set; }
        public int CompletedCount { get; set; }
        public int MeetingCount { get; set; }
        public int TotalCount => CreatedCount + CompletedCount;
    }
}
