using System;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using System.Windows.Forms.DataVisualization.Charting;
using work_tracker.Data;

namespace work_tracker.Forms
{
    public partial class ReportsForm : XtraForm
    {
        private WorkTrackerDbContext _context;

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
            LoadAllReports();
        }

        private void LoadAllReports()
        {
            LoadEffortChartReport();
            LoadDetailedReport();
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
                        PlannedMinutes = (double)((w.EffortEstimate ?? 0m) * 24m * 60m),
                        ActualMinutes = w.CompletedAt.HasValue
                            ? (w.CompletedAt.Value - (w.StartedAt ?? w.CreatedAt)).TotalMinutes
                            : 0
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
                chartEffort.ChartAreas.Add(area);

                var legend = new Legend("Legend");
                chartEffort.Legends.Add(legend);

                var seriesPlanned = new Series("Planlanan");
                seriesPlanned.ChartType = SeriesChartType.Column;
                seriesPlanned.ChartArea = "MainArea";

                var seriesActual = new Series("Gerçekleşen");
                seriesActual.ChartType = SeriesChartType.Column;
                seriesActual.ChartArea = "MainArea";

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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAllReports();
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

