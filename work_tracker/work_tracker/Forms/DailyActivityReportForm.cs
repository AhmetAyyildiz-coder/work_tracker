using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraEditors;
using work_tracker.Data;
using work_tracker.Data.Entities;
using work_tracker.Helpers;
using System.Collections.Generic;

namespace work_tracker.Forms
{
    /// <summary>
    /// Günlük aktivite raporu - Bir iş için günlük bazda aktivite özeti
    /// </summary>
    public partial class DailyActivityReportForm : XtraForm
    {
        private WorkTrackerDbContext _context;
        private int _workItemId;
        private WorkItem _workItem;

        public DailyActivityReportForm(int workItemId)
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
            _workItemId = workItemId;
        }

        private void DailyActivityReportForm_Load(object sender, EventArgs e)
        {
            LoadWorkItem();
            LoadDailyActivityReport();
        }

        private void LoadWorkItem()
        {
            _workItem = _context.WorkItems.Find(_workItemId);
            if (_workItem != null)
            {
                this.Text = $"Günlük Aktivite Raporu: {_workItem.Title}";
                lblWorkItemTitle.Text = _workItem.Title;
            }
        }

        private void LoadDailyActivityReport()
        {
            try
            {
                // TimeEntry kayıtlarını çek (telefon görüşmeleri için)
                var timeEntries = _context.TimeEntries
                    .Where(t => t.WorkItemId == _workItemId)
                    .ToList();

                // WorkItemActivity kayıtlarını çek (durum değişiklikleri)
                var activities = _context.WorkItemActivities
                    .Where(a => a.WorkItemId == _workItemId && a.ActivityType == WorkItemActivityTypes.StatusChange)
                    .OrderBy(a => a.CreatedAt)
                    .ToList();

                var developmentBreakdown = DevelopmentTimeHelper
                    .CalculateDailyBreakdown(_workItem, activities);

                var allDates = new HashSet<DateTime>(developmentBreakdown.Keys);

                foreach (var entry in timeEntries)
                {
                    allDates.Add(entry.EntryDate.Date);
                }

                foreach (var activity in activities)
                {
                    allDates.Add(activity.CreatedAt.Date);
                }

                // Her tarih için günlük rapor satırı oluştur
                var dailyReport = allDates
                    .Select(date => new
                    {
                        Tarih = date,
                        TelefonGorusmeDk = timeEntries
                            .Where(t => t.EntryDate.Date == date && t.ActivityType == TimeEntryActivityTypes.PhoneCall)
                            .Sum(t => (int?)t.DurationMinutes) ?? 0,
                        MudahaleEdiliyorSayisi = activities
                            .Count(a => a.CreatedAt.Date == date && a.NewValue == "MudahaleEdiliyor"),
                        ToplamCalismaDk = developmentBreakdown.ContainsKey(date)
                            ? (int)developmentBreakdown[date].TotalMinutes
                            : 0
                    })
                    .OrderByDescending(r => r.Tarih)
                    .Select(r => new
                    {
                        Tarih = r.Tarih,
                        TarihStr = r.Tarih.ToString("dd.MM.yyyy (dddd)"),
                        TelefonGorusmeDk = r.TelefonGorusmeDk,
                        TelefonGorusmeSure = FormatDuration(r.TelefonGorusmeDk),
                        MudahaleEdiliyorSayisi = r.MudahaleEdiliyorSayisi,
                        ToplamCalismaDk = r.ToplamCalismaDk,
                        ToplamCalismaSure = FormatDuration(r.ToplamCalismaDk)
                    })
                    .ToList();

                gridDailyReport.DataSource = dailyReport;

                var view = gridViewDailyReport;
                view.BestFitColumns();

                // Kolon başlıkları
                if (view.Columns["Tarih"] != null) view.Columns["Tarih"].Visible = false;
                if (view.Columns["TarihStr"] != null) view.Columns["TarihStr"].Caption = "Tarih";
                if (view.Columns["TelefonGorusmeDk"] != null) view.Columns["TelefonGorusmeDk"].Visible = false;
                if (view.Columns["TelefonGorusmeSure"] != null) view.Columns["TelefonGorusmeSure"].Caption = "Telefon Görüşme";
                if (view.Columns["MudahaleEdiliyorSayisi"] != null) view.Columns["MudahaleEdiliyorSayisi"].Caption = "Müdahale Bekliyor Sayısı";
                if (view.Columns["ToplamCalismaDk"] != null) view.Columns["ToplamCalismaDk"].Visible = false;
                if (view.Columns["ToplamCalismaSure"] != null) view.Columns["ToplamCalismaSure"].Caption = "Toplam Geliştirme Süresi";

                // Özetler
                view.Columns["TelefonGorusmeDk"].Summary.Clear();
                view.Columns["TelefonGorusmeDk"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TelefonGorusmeDk", "Toplam: {0} dk");

                view.Columns["MudahaleEdiliyorSayisi"].Summary.Clear();
                view.Columns["MudahaleEdiliyorSayisi"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MudahaleEdiliyorSayisi", "Toplam: {0}");

                view.Columns["ToplamCalismaDk"].Summary.Clear();
                view.Columns["ToplamCalismaDk"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "ToplamCalismaDk", "Toplam: {0} dk");

                view.OptionsBehavior.Editable = false;
                view.OptionsView.ShowGroupPanel = false;
                view.OptionsView.ShowFooter = true;

                // Özet bilgiler
                var totalPhoneDuration = dailyReport.Sum(r => r.TelefonGorusmeDk);
                var totalMudahaleCount = dailyReport.Sum(r => r.MudahaleEdiliyorSayisi);
                var totalWorkDuration = dailyReport.Sum(r => r.ToplamCalismaDk);

                lblTotalPhone.Text = $"Toplam Telefon: {FormatDuration(totalPhoneDuration)}";
                lblTotalMudahale.Text = $"Toplam Müdahale Bekliyor: {totalMudahaleCount} kez";
                lblTotalWork.Text = $"Toplam Geliştirme: {FormatDuration(totalWorkDuration)}";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Rapor yüklenirken hata oluştu:\n\n{ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDailyActivityReport();
        }
    }
}
