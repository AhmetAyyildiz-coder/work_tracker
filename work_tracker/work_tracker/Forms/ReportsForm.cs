using System;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
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
            LoadAllReports();
        }

        private void LoadAllReports()
        {
            LoadCapacityReport();
            LoadWorkDistributionReport();
            LoadSprintPerformanceReport();
            LoadEffortTrendReport();
        }

        private void LoadCapacityReport()
        {
            try
            {
                // Toplam iş sayıları
                var totalScrum = _context.WorkItems.Count(w => w.Board == "Scrum");
                var totalKanban = _context.WorkItems.Count(w => w.Board == "Kanban");
                var totalInbox = _context.WorkItems.Count(w => w.Board == "Inbox");

                // Tamamlanan işler
                var completedScrum = _context.WorkItems.Count(w => w.Board == "Scrum" && w.Status == "Tamamlandi");
                var completedKanban = _context.WorkItems.Count(w => w.Board == "Kanban" && w.Status == "Cozuldu");

                // Toplam efor
                var effortScrum = _context.WorkItems
                    .Where(w => w.Board == "Scrum" && w.EffortEstimate.HasValue)
                    .Sum(w => (decimal?)w.EffortEstimate) ?? 0;

                var effortKanban = _context.WorkItems
                    .Where(w => w.Board == "Kanban" && w.EffortEstimate.HasValue)
                    .Sum(w => (decimal?)w.EffortEstimate) ?? 0;

                // HTML rapor oluştur
                var html = $@"
<h2 style='color: #0078D4;'>📊 Kapasite Dağılım Raporu</h2>
<p><i>Planlı (Scrum) ve Plansız (Kanban) İş Dağılımı</i></p>
<hr/>

<h3>📈 Genel Özet</h3>
<table border='1' cellpadding='8' style='border-collapse: collapse; width: 100%;'>
<tr style='background-color: #F3F3F3;'>
    <th>Pano</th>
    <th>Toplam İş</th>
    <th>Tamamlanan</th>
    <th>Tamamlanma %</th>
    <th>Toplam Efor (gün)</th>
</tr>
<tr>
    <td><b style='color: #28a745;'>Scrum (Planlı)</b></td>
    <td>{totalScrum}</td>
    <td>{completedScrum}</td>
    <td>{(totalScrum > 0 ? (completedScrum * 100.0 / totalScrum).ToString("F1") : "0")}%</td>
    <td>{effortScrum:F1}</td>
</tr>
<tr>
    <td><b style='color: #dc3545;'>Kanban (Acil)</b></td>
    <td>{totalKanban}</td>
    <td>{completedKanban}</td>
    <td>{(totalKanban > 0 ? (completedKanban * 100.0 / totalKanban).ToString("F1") : "0")}%</td>
    <td>{effortKanban:F1}</td>
</tr>
<tr>
    <td><b>Gelen Kutusu</b></td>
    <td>{totalInbox}</td>
    <td>-</td>
    <td>-</td>
    <td>-</td>
</tr>
</table>

<h3>💡 Analiz ve Öneriler</h3>
<ul>";

                var totalWork = totalScrum + totalKanban;
                if (totalWork > 0)
                {
                    var kanbanPercentage = (totalKanban * 100.0 / totalWork);
                    if (kanbanPercentage > 50)
                    {
                        html += "<li style='color: #dc3545;'><b>UYARI:</b> Kanban işleri %50'yi aşıyor! Plansız iş yükü çok fazla.</li>";
                        html += "<li>Öneri: Acil işlerin kaynağını tespit edin ve önlem alın.</li>";
                    }
                    else if (kanbanPercentage > 30)
                    {
                        html += "<li style='color: #ffc107;'><b>DİKKAT:</b> Kanban işleri %30-50 arasında. İş yükü dengesini gözden geçirin.</li>";
                    }
                    else
                    {
                        html += "<li style='color: #28a745;'><b>İYİ:</b> Scrum/Kanban dengesi sağlıklı görünüyor.</li>";
                    }

                    html += $"<li>Scrum işleri: %{(totalScrum * 100.0 / totalWork):F1}</li>";
                    html += $"<li>Kanban işleri: %{kanbanPercentage:F1}</li>";
                }

                if (totalInbox > 10)
                {
                    html += $"<li style='color: #ffc107;'><b>UYARI:</b> Gelen Kutusunda {totalInbox} iş bekliyor! Triage yapılmalı.</li>";
                }

                html += "</ul>";

                richEditCapacity.HtmlText = html;
            }
            catch (Exception ex)
            {
                richEditCapacity.HtmlText = $"<p style='color: red;'>Hata: {ex.Message}</p>";
            }
        }

        private void LoadWorkDistributionReport()
        {
            try
            {
                // Proje bazlı dağılım
                var projectDistribution = _context.WorkItems
                    .Where(w => w.ProjectId.HasValue)
                    .GroupBy(w => w.Project.Name)
                    .Select(g => new
                    {
                        ProjectName = g.Key,
                        Count = g.Count(),
                        CompletedCount = g.Count(w => w.Status == "Tamamlandi" || w.Status == "Cozuldu"),
                        TotalEffort = g.Sum(w => (decimal?)w.EffortEstimate) ?? 0
                    })
                    .OrderByDescending(x => x.Count)
                    .ToList();

                var html = @"
<h2 style='color: #0078D4;'>📊 İş Dağılım Raporu</h2>
<p><i>Proje ve Modül Bazında İş Dağılımı</i></p>
<hr/>

<h3>🎯 Proje Bazlı Dağılım</h3>
<table border='1' cellpadding='8' style='border-collapse: collapse; width: 100%;'>
<tr style='background-color: #F3F3F3;'>
    <th>Proje</th>
    <th>Toplam İş</th>
    <th>Tamamlanan</th>
    <th>Tamamlanma %</th>
    <th>Toplam Efor (gün)</th>
</tr>";

                foreach (var project in projectDistribution)
                {
                    var completionRate = project.Count > 0 ? (project.CompletedCount * 100.0 / project.Count) : 0;
                    html += $@"
<tr>
    <td><b>{project.ProjectName}</b></td>
    <td>{project.Count}</td>
    <td>{project.CompletedCount}</td>
    <td>{completionRate:F1}%</td>
    <td>{project.TotalEffort:F1}</td>
</tr>";
                }

                html += "</table>";

                // Modül bazlı dağılım
                var moduleDistribution = _context.WorkItems
                    .Where(w => w.ModuleId.HasValue)
                    .GroupBy(w => w.Module.Name)
                    .Select(g => new
                    {
                        ModuleName = g.Key,
                        Count = g.Count(),
                        TotalEffort = g.Sum(w => (decimal?)w.EffortEstimate) ?? 0
                    })
                    .OrderByDescending(x => x.Count)
                    .Take(10)
                    .ToList();

                if (moduleDistribution.Any())
                {
                    html += @"
<h3>🔧 Modül Bazlı Dağılım (Top 10)</h3>
<table border='1' cellpadding='8' style='border-collapse: collapse; width: 100%;'>
<tr style='background-color: #F3F3F3;'>
    <th>Modül</th>
    <th>İş Sayısı</th>
    <th>Toplam Efor (gün)</th>
</tr>";

                    foreach (var module in moduleDistribution)
                    {
                        html += $@"
<tr>
    <td><b>{module.ModuleName}</b></td>
    <td>{module.Count}</td>
    <td>{module.TotalEffort:F1}</td>
</tr>";
                    }

                    html += "</table>";
                }

                html += @"
<h3>💡 Öneriler</h3>
<ul>
    <li>En çok iş yapılan projelere daha fazla kaynak ayrılabilir</li>
    <li>Modül dağılımı ekip uzmanlık alanlarıyla uyumlu mu kontrol edin</li>
    <li>Yoğun modüllerde bottleneck oluşmaması için kapasite planlaması yapın</li>
</ul>";

                richEditWorkDistribution.HtmlText = html;
            }
            catch (Exception ex)
            {
                richEditWorkDistribution.HtmlText = $"<p style='color: red;'>Hata: {ex.Message}</p>";
            }
        }

        private void LoadSprintPerformanceReport()
        {
            try
            {
                var sprints = _context.Sprints
                    .OrderByDescending(s => s.StartDate)
                    .Take(10)
                    .ToList();

                var html = @"
<h2 style='color: #0078D4;'>🏃 Sprint Performans Raporu</h2>
<p><i>Son 10 Sprint'in Performans Analizi</i></p>
<hr/>";

                if (!sprints.Any())
                {
                    html += "<p><i>Henüz tamamlanmış sprint bulunmamaktadır.</i></p>";
                }
                else
                {
                    html += @"
<table border='1' cellpadding='8' style='border-collapse: collapse; width: 100%;'>
<tr style='background-color: #F3F3F3;'>
    <th>Sprint</th>
    <th>Durum</th>
    <th>Tarih</th>
    <th>Süre (gün)</th>
    <th>Toplam İş</th>
    <th>Tamamlanan</th>
    <th>Velocity</th>
</tr>";

                    foreach (var sprint in sprints)
                    {
                        var totalItems = _context.WorkItems.Count(w => w.SprintId == sprint.Id);
                        var completedItems = _context.WorkItems.Count(w => w.SprintId == sprint.Id && w.Status == "Tamamlandi");
                        var completedEffort = _context.WorkItems
                            .Where(w => w.SprintId == sprint.Id && w.Status == "Tamamlandi" && w.EffortEstimate.HasValue)
                            .Sum(w => (decimal?)w.EffortEstimate) ?? 0;

                        var statusColor = sprint.Status == "Active" ? "#28a745" : 
                                        sprint.Status == "Completed" ? "#6c757d" : "#0078D4";

                        html += $@"
<tr>
    <td><b>{sprint.Name}</b></td>
    <td style='color: {statusColor};'><b>{sprint.Status}</b></td>
    <td>{sprint.StartDate:dd.MM.yyyy} - {sprint.EndDate:dd.MM.yyyy}</td>
    <td>{sprint.DurationDays}</td>
    <td>{totalItems}</td>
    <td>{completedItems}</td>
    <td>{completedEffort:F1} gün</td>
</tr>";
                    }

                    html += "</table>";

                    // Ortalama velocity hesapla
                    var completedSprints = sprints.Where(s => s.Status == "Completed").ToList();
                    if (completedSprints.Any())
                    {
                        var avgVelocity = completedSprints.Average(s =>
                        {
                            var effort = _context.WorkItems
                                .Where(w => w.SprintId == s.Id && w.Status == "Tamamlandi" && w.EffortEstimate.HasValue)
                                .Sum(w => (decimal?)w.EffortEstimate) ?? 0;
                            return (double)effort;
                        });

                        html += $@"
<h3>📈 Sprint Metrikleri</h3>
<ul>
    <li><b>Ortalama Velocity:</b> {avgVelocity:F1} gün/sprint</li>
    <li><b>Tamamlanan Sprint Sayısı:</b> {completedSprints.Count}</li>
    <li><b>Aktif Sprint:</b> {(sprints.Any(s => s.Status == "Active") ? "Var" : "Yok")}</li>
</ul>";
                    }
                }

                html += @"
<h3>💡 Öneriler</h3>
<ul>
    <li>Ortalama velocity'yi baz alarak gelecek sprint planlaması yapın</li>
    <li>Sprint başarı oranı %80'in altındaysa, sprint kapasitesini gözden geçirin</li>
    <li>Tutarlı velocity için sprint sürelerini sabit tutun</li>
</ul>";

                richEditSprintPerformance.HtmlText = html;
            }
            catch (Exception ex)
            {
                richEditSprintPerformance.HtmlText = $"<p style='color: red;'>Hata: {ex.Message}</p>";
            }
        }

        private void LoadEffortTrendReport()
        {
            try
            {
                // Son 30 günün efor trendi
                var thirtyDaysAgo = DateTime.Now.AddDays(-30);
                var effortByDay = _context.WorkItems
                    .Where(w => w.CompletedAt.HasValue && w.CompletedAt >= thirtyDaysAgo)
                    .GroupBy(w => DbFunctions.TruncateTime(w.CompletedAt))
                    .Select(g => new
                    {
                        Date = g.Key,
                        Count = g.Count(),
                        TotalEffort = g.Sum(w => (decimal?)w.EffortEstimate) ?? 0
                    })
                    .OrderBy(x => x.Date)
                    .ToList();

                var html = @"
<h2 style='color: #0078D4;'>📈 Efor Analizi ve Trend</h2>
<p><i>Son 30 Günün Tamamlanma Trendi</i></p>
<hr/>";

                if (!effortByDay.Any())
                {
                    html += "<p><i>Son 30 günde tamamlanan iş bulunmamaktadır.</i></p>";
                }
                else
                {
                    var totalCompleted = effortByDay.Sum(x => x.Count);
                    var totalEffort = effortByDay.Sum(x => x.TotalEffort);
                    var avgDailyCompletion = effortByDay.Average(x => x.Count);

                    html += $@"
<h3>📊 Özet İstatistikler (Son 30 Gün)</h3>
<ul>
    <li><b>Toplam Tamamlanan İş:</b> {totalCompleted}</li>
    <li><b>Toplam Tamamlanan Efor:</b> {totalEffort:F1} gün</li>
    <li><b>Günlük Ortalama Tamamlama:</b> {avgDailyCompletion:F1} iş</li>
    <li><b>Günlük Ortalama Efor:</b> {(totalEffort / effortByDay.Count):F1} gün</li>
</ul>

<h3>📅 Günlük Detay</h3>
<table border='1' cellpadding='8' style='border-collapse: collapse; width: 100%;'>
<tr style='background-color: #F3F3F3;'>
    <th>Tarih</th>
    <th>Tamamlanan İş</th>
    <th>Toplam Efor (gün)</th>
</tr>";

                    // Son 14 günü göster
                    var last14Days = effortByDay.Skip(Math.Max(0, effortByDay.Count - 14)).ToList();
                    foreach (var day in last14Days)
                    {
                        html += $@"
<tr>
    <td>{day.Date:dd.MM.yyyy (ddd)}</td>
    <td>{day.Count}</td>
    <td>{day.TotalEffort:F1}</td>
</tr>";
                    }

                    html += "</table>";
                }

                // İş tipi bazlı analiz
                var typeDistribution = _context.WorkItems
                    .Where(w => w.CompletedAt.HasValue && !string.IsNullOrEmpty(w.Type))
                    .GroupBy(w => w.Type)
                    .Select(g => new
                    {
                        Type = g.Key,
                        Count = g.Count()
                    })
                    .OrderByDescending(x => x.Count)
                    .ToList();

                if (typeDistribution.Any())
                {
                    html += @"
<h3>🏷️ İş Tipi Dağılımı (Tamamlanan İşler)</h3>
<table border='1' cellpadding='8' style='border-collapse: collapse; width: 100%;'>
<tr style='background-color: #F3F3F3;'>
    <th>İş Tipi</th>
    <th>Sayı</th>
    <th>Oran</th>
</tr>";

                    var total = typeDistribution.Sum(x => x.Count);
                    foreach (var type in typeDistribution)
                    {
                        var percentage = (type.Count * 100.0 / total);
                        html += $@"
<tr>
    <td><b>{type.Type}</b></td>
    <td>{type.Count}</td>
    <td>{percentage:F1}%</td>
</tr>";
                    }

                    html += "</table>";
                }

                html += @"
<h3>💡 Öneriler</h3>
<ul>
    <li>Tamamlama trendini takip ederek ekip kapasitesini planlayın</li>
    <li>Düşük tamamlama oranı görürseniz, WIP limitini ve iş yükünü gözden geçirin</li>
    <li>Bug oranı yüksekse, kalite süreçlerini iyileştirin</li>
</ul>";

                richEditEffortTrend.HtmlText = html;
            }
            catch (Exception ex)
            {
                richEditEffortTrend.HtmlText = $"<p style='color: red;'>Hata: {ex.Message}</p>";
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAllReports();
            XtraMessageBox.Show("Raporlar güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

