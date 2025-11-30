using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using work_tracker.Data;
using work_tracker.Helpers;

namespace work_tracker.Services
{
    /// <summary>
    /// Günlük iş hatırlatıcı servisi
    /// Her gün belirlenen saatte aktif işleri kontrol eder ve bildirim gösterir
    /// </summary>
    public class WorkReminderService : IDisposable
    {
        private readonly System.Threading.Timer _timer;
        private readonly NotifyIcon _notifyIcon;
        private readonly TimeSpan _reminderTime;
        private DateTime _lastReminderDate;
        private bool _disposed;

        /// <summary>
        /// WorkReminderService oluşturur
        /// </summary>
        /// <param name="notifyIcon">Bildirim göstermek için NotifyIcon</param>
        /// <param name="reminderHour">Hatırlatma saati (varsayılan: 17)</param>
        /// <param name="reminderMinute">Hatırlatma dakikası (varsayılan: 30)</param>
        public WorkReminderService(NotifyIcon notifyIcon, int reminderHour = 17, int reminderMinute = 30)
        {
            _notifyIcon = notifyIcon ?? throw new ArgumentNullException(nameof(notifyIcon));
            _reminderTime = new TimeSpan(reminderHour, reminderMinute, 0);
            _lastReminderDate = DateTime.MinValue;

            // Her dakika kontrol et
            _timer = new System.Threading.Timer(CheckReminder, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            
            Logger.Info($"WorkReminderService başlatıldı. Hatırlatma saati: {_reminderTime}");
        }

        private void CheckReminder(object state)
        {
            try
            {
                var now = DateTime.Now;
                var todayReminderTime = now.Date.Add(_reminderTime);

                // Bugün zaten hatırlatma yapıldı mı?
                if (_lastReminderDate.Date == now.Date)
                    return;

                // Hatırlatma zamanı geldi mi? (±2 dakika tolerans)
                if (now >= todayReminderTime && now <= todayReminderTime.AddMinutes(2))
                {
                    _lastReminderDate = now.Date;
                    CheckActiveWorkItems();
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "WorkReminderService.CheckReminder hatası");
            }
        }

        private void CheckActiveWorkItems()
        {
            try
            {
                using (var context = new WorkTrackerDbContext())
                {
                    // Kanban'da aktif işler (MudahaleEdiliyor durumunda)
                    var kanbanActiveCount = context.WorkItems
                        .Count(w => w.Board == "Kanban" && 
                                   w.Status == "MudahaleEdiliyor" && 
                                   !w.IsArchived);

                    // Scrum'da aktif işler (Gelistirmede veya Testte durumunda)
                    var scrumActiveCount = context.WorkItems
                        .Count(w => w.Board == "Scrum" && 
                                   (w.Status == "Gelistirmede" || w.Status == "Testte") && 
                                   !w.IsArchived);

                    // Toplam bekleyen işler (Inbox + Triage)
                    var pendingCount = context.WorkItems
                        .Count(w => (w.Board == "Inbox" || w.Status == "Bekliyor" || w.Status == "Triage") && 
                                   !w.IsArchived);

                    var totalActive = kanbanActiveCount + scrumActiveCount;

                    if (totalActive > 0 || pendingCount > 0)
                    {
                        ShowNotification(kanbanActiveCount, scrumActiveCount, pendingCount);
                    }
                    else
                    {
                        ShowAllClearNotification();
                    }

                    Logger.Info($"Günlük hatırlatma: Kanban={kanbanActiveCount}, Scrum={scrumActiveCount}, Bekleyen={pendingCount}");
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "CheckActiveWorkItems hatası");
            }
        }

        private void ShowNotification(int kanbanCount, int scrumCount, int pendingCount)
        {
            // UI thread'de çalıştır
            if (_notifyIcon.ContextMenuStrip?.InvokeRequired == true)
            {
                _notifyIcon.ContextMenuStrip.Invoke(new Action(() => 
                    ShowNotificationInternal(kanbanCount, scrumCount, pendingCount)));
            }
            else
            {
                ShowNotificationInternal(kanbanCount, scrumCount, pendingCount);
            }
        }

        private void ShowNotificationInternal(int kanbanCount, int scrumCount, int pendingCount)
        {
            var message = "🕐 Günün Sonu Özeti:\n\n";
            
            if (kanbanCount > 0)
                message += $"🔴 Kanban'da {kanbanCount} aktif iş var\n";
            
            if (scrumCount > 0)
                message += $"🔵 Scrum'da {scrumCount} aktif iş var\n";
            
            if (pendingCount > 0)
                message += $"📥 {pendingCount} bekleyen iş var\n";

            message += "\nGitmeden önce durumları kontrol et!";

            _notifyIcon.ShowBalloonTip(
                10000, // 10 saniye göster
                "⏰ Work Tracker - Günlük Hatırlatma",
                message,
                ToolTipIcon.Warning
            );
        }

        private void ShowAllClearNotification()
        {
            if (_notifyIcon.ContextMenuStrip?.InvokeRequired == true)
            {
                _notifyIcon.ContextMenuStrip.Invoke(new Action(ShowAllClearNotificationInternal));
            }
            else
            {
                ShowAllClearNotificationInternal();
            }
        }

        private void ShowAllClearNotificationInternal()
        {
            _notifyIcon.ShowBalloonTip(
                5000,
                "✅ Work Tracker",
                "Harika! Aktif iş yok. İyi akşamlar! 🎉",
                ToolTipIcon.Info
            );
        }

        /// <summary>
        /// Manuel olarak hatırlatmayı tetikle (test için veya talep üzerine)
        /// </summary>
        public void TriggerReminderNow()
        {
            CheckActiveWorkItems();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _timer?.Dispose();
                _disposed = true;
                Logger.Info("WorkReminderService dispose edildi");
            }
        }
    }
}
