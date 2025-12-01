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
    /// Yemek saatlerinde (11:00-11:30) geliştirmedeki işleri hatırlatır
    /// </summary>
    public class WorkReminderService : IDisposable
    {
        private readonly System.Threading.Timer _timer;
        private readonly NotifyIcon _notifyIcon;
        private readonly TimeSpan _reminderTime;
        private DateTime _lastReminderDate;
        private DateTime _lastLunchReminderTime;
        private bool _disposed;

        // Yemek saati hatırlatma zamanları (11:00, 11:10, 11:20, 11:30)
        private readonly TimeSpan[] _lunchReminderTimes = new[]
        {
            new TimeSpan(11, 0, 0),
            new TimeSpan(11, 10, 0),
            new TimeSpan(11, 20, 0),
            new TimeSpan(11, 30, 0)
        };

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
                
                // Yemek saati hatırlatması (hafta içi 11:00-11:30)
                CheckLunchTimeReminder(now);
                
                // Günlük hatırlatma (17:30)
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

        /// <summary>
        /// Yemek saati hatırlatması - Hafta içi 11:00, 11:10, 11:20, 11:30'da kontrol
        /// </summary>
        private void CheckLunchTimeReminder(DateTime now)
        {
            try
            {
                // Sadece hafta içi
                if (now.DayOfWeek == DayOfWeek.Saturday || now.DayOfWeek == DayOfWeek.Sunday)
                    return;

                var currentTime = now.TimeOfDay;

                // Yemek saatlerinden birinde miyiz?
                foreach (var lunchTime in _lunchReminderTimes)
                {
                    // ±1 dakika tolerans
                    if (currentTime >= lunchTime && currentTime < lunchTime.Add(TimeSpan.FromMinutes(1)))
                    {
                        // Bu dakikada zaten hatırlatma yaptık mı?
                        if (_lastLunchReminderTime.Date == now.Date && 
                            _lastLunchReminderTime.Hour == now.Hour && 
                            _lastLunchReminderTime.Minute == now.Minute)
                            return;

                        _lastLunchReminderTime = now;
                        CheckInProgressWorkItems(lunchTime);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "CheckLunchTimeReminder hatası");
            }
        }

        /// <summary>
        /// Geliştirmede olan işleri kontrol et ve uyarı göster
        /// </summary>
        private void CheckInProgressWorkItems(TimeSpan reminderTime)
        {
            try
            {
                using (var context = new WorkTrackerDbContext())
                {
                    // Geliştirmede veya MudahaleEdiliyor durumundaki işler
                    var inProgressItems = context.WorkItems
                        .Where(w => !w.IsArchived && 
                                   (w.Status == "Gelistirmede" || 
                                    w.Status == "MudahaleEdiliyor" ||
                                    w.Status == "Testte"))
                        .Select(w => new { w.Id, w.Title, w.Status })
                        .ToList();

                    if (inProgressItems.Count > 0)
                    {
                        ShowLunchReminder(inProgressItems.Count, reminderTime, 
                            inProgressItems.Select(x => $"#{x.Id} - {x.Title}").Take(3).ToList());
                        
                        Logger.Info($"Yemek hatırlatması ({reminderTime}): {inProgressItems.Count} iş geliştirmede");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "CheckInProgressWorkItems hatası");
            }
        }

        /// <summary>
        /// Yemek saati uyarısını göster
        /// </summary>
        private void ShowLunchReminder(int count, TimeSpan time, System.Collections.Generic.List<string> topItems)
        {
            if (_notifyIcon.ContextMenuStrip?.InvokeRequired == true)
            {
                _notifyIcon.ContextMenuStrip.Invoke(new Action(() => 
                    ShowLunchReminderInternal(count, time, topItems)));
            }
            else
            {
                ShowLunchReminderInternal(count, time, topItems);
            }
        }

        private void ShowLunchReminderInternal(int count, TimeSpan time, System.Collections.Generic.List<string> topItems)
        {
            string timeStr = $"{time.Hours:D2}:{time.Minutes:D2}";
            string urgency = time.Hours == 11 && time.Minutes >= 20 ? "⚠️ " : "";
            
            var message = $"{urgency}Yemek vakti yaklaşıyor! ({timeStr})\n\n";
            message += $"🔧 {count} iş hala geliştirmede:\n";
            
            foreach (var item in topItems)
            {
                message += $"  • {item}\n";
            }
            
            if (count > 3)
                message += $"  ... ve {count - 3} iş daha\n";
            
            message += "\n💡 Yemeğe çıkmadan önce işleri 'Beklemede' durumuna al!";

            _notifyIcon.ShowBalloonTip(
                8000, // 8 saniye göster
                "🍽️ Work Tracker - Yemek Hatırlatması",
                message,
                ToolTipIcon.Warning
            );
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

        /// <summary>
        /// Manuel olarak yemek hatırlatmasını tetikle (test için)
        /// </summary>
        public void TriggerLunchReminderNow()
        {
            CheckInProgressWorkItems(new TimeSpan(11, 0, 0));
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
