using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using work_tracker.Data;
using work_tracker.Data.Entities;
using work_tracker.Helpers;

namespace work_tracker.Services
{
    /// <summary>
    /// İş hatırlatıcı servisi - Zamanı gelen hatırlatmaları gösterir
    /// </summary>
    public class WorkItemReminderService : IDisposable
    {
        private readonly Timer _timer;
        private readonly NotifyIcon _notifyIcon;
        private readonly int _checkIntervalSeconds;
        private bool _isDisposed;

        public event EventHandler<ReminderEventArgs> ReminderTriggered;

        public WorkItemReminderService(NotifyIcon notifyIcon, int checkIntervalSeconds = 60)
        {
            _notifyIcon = notifyIcon;
            _checkIntervalSeconds = checkIntervalSeconds;

            _timer = new Timer
            {
                Interval = checkIntervalSeconds * 1000 // Varsayılan 60 saniye
            };
            _timer.Tick += Timer_Tick;
        }

        public void Start()
        {
            _timer.Start();
            Logger.Info($"WorkItemReminderService başlatıldı (kontrol aralığı: {_checkIntervalSeconds} saniye)");
            
            // İlk kontrolü hemen yap
            CheckReminders();
        }

        public void Stop()
        {
            _timer.Stop();
            Logger.Info("WorkItemReminderService durduruldu");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            CheckReminders();
        }

        private void CheckReminders()
        {
            try
            {
                using (var context = new WorkTrackerDbContext())
                {
                    var now = DateTime.Now;
                    var currentUser = Environment.UserName;

                    // Zamanı gelmiş ve henüz kapatılmamış hatırlatıcıları bul
                    var dueReminders = context.WorkItemReminders
                        .Include(r => r.WorkItem)
                        .Include(r => r.WorkItem.Project)
                        .Where(r => r.ReminderDate <= now &&
                                   !r.IsDismissed &&
                                   r.CreatedBy == currentUser)
                        .OrderBy(r => r.ReminderDate)
                        .ToList();

                    if (dueReminders.Count > 0)
                    {
                        Logger.Info($"{dueReminders.Count} hatırlatıcı zamanı geldi");
                        
                        foreach (var reminder in dueReminders)
                        {
                            // Henüz gösterilmemişse göster
                            if (!reminder.IsShown)
                            {
                                reminder.IsShown = true;
                                context.SaveChanges();
                            }

                            // Event'i tetikle
                            ReminderTriggered?.Invoke(this, new ReminderEventArgs(reminder));

                            // Bildirim göster
                            ShowReminderNotification(reminder);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Hatırlatıcı kontrolü sırasında hata", ex);
            }
        }

        private void ShowReminderNotification(WorkItemReminder reminder)
        {
            try
            {
                var title = $"🔔 Hatırlatıcı: #{reminder.WorkItemId}";
                var message = $"{reminder.WorkItem?.Title ?? "İş"}\n";
                
                if (!string.IsNullOrEmpty(reminder.Note))
                    message += $"📝 {reminder.Note}\n";
                
                if (reminder.WorkItem?.Project != null)
                    message += $"📁 {reminder.WorkItem.Project.Name}";

                _notifyIcon.ShowBalloonTip(
                    10000, // 10 saniye
                    title,
                    message,
                    ToolTipIcon.Warning
                );
            }
            catch (Exception ex)
            {
                Logger.Error("Hatırlatıcı bildirimi gösterilirken hata", ex);
            }
        }

        /// <summary>
        /// Hatırlatıcıyı ertele
        /// </summary>
        public void SnoozeReminder(int reminderId, TimeSpan snoozeTime)
        {
            try
            {
                using (var context = new WorkTrackerDbContext())
                {
                    var reminder = context.WorkItemReminders.Find(reminderId);
                    if (reminder != null)
                    {
                        reminder.ReminderDate = DateTime.Now.Add(snoozeTime);
                        reminder.IsShown = false;
                        reminder.SnoozeCount++;
                        reminder.LastSnoozedAt = DateTime.Now;
                        context.SaveChanges();

                        Logger.Info($"Hatırlatıcı #{reminderId} {snoozeTime.TotalMinutes} dakika ertelendi");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Hatırlatıcı erteleme hatası (ID: {reminderId})", ex);
            }
        }

        /// <summary>
        /// Hatırlatıcıyı kapat (dismiss)
        /// </summary>
        public void DismissReminder(int reminderId)
        {
            try
            {
                using (var context = new WorkTrackerDbContext())
                {
                    var reminder = context.WorkItemReminders.Find(reminderId);
                    if (reminder != null)
                    {
                        reminder.IsDismissed = true;
                        context.SaveChanges();

                        Logger.Info($"Hatırlatıcı #{reminderId} kapatıldı");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Hatırlatıcı kapatma hatası (ID: {reminderId})", ex);
            }
        }

        /// <summary>
        /// Aktif hatırlatıcıları getir
        /// </summary>
        public List<WorkItemReminder> GetActiveReminders()
        {
            try
            {
                using (var context = new WorkTrackerDbContext())
                {
                    var currentUser = Environment.UserName;
                    return context.WorkItemReminders
                        .Include(r => r.WorkItem)
                        .Include(r => r.WorkItem.Project)
                        .Where(r => !r.IsDismissed && r.CreatedBy == currentUser)
                        .OrderBy(r => r.ReminderDate)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Aktif hatırlatıcılar getirilirken hata", ex);
                return new List<WorkItemReminder>();
            }
        }

        /// <summary>
        /// Zamanı geçmiş hatırlatıcıları getir
        /// </summary>
        public List<WorkItemReminder> GetOverdueReminders()
        {
            try
            {
                using (var context = new WorkTrackerDbContext())
                {
                    var currentUser = Environment.UserName;
                    var now = DateTime.Now;
                    
                    return context.WorkItemReminders
                        .Include(r => r.WorkItem)
                        .Include(r => r.WorkItem.Project)
                        .Where(r => r.ReminderDate <= now &&
                                   !r.IsDismissed &&
                                   r.CreatedBy == currentUser)
                        .OrderBy(r => r.ReminderDate)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Gecikmiş hatırlatıcılar getirilirken hata", ex);
                return new List<WorkItemReminder>();
            }
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _timer?.Stop();
                _timer?.Dispose();
                _isDisposed = true;
            }
        }
    }

    public class ReminderEventArgs : EventArgs
    {
        public WorkItemReminder Reminder { get; }

        public ReminderEventArgs(WorkItemReminder reminder)
        {
            Reminder = reminder;
        }
    }
}
