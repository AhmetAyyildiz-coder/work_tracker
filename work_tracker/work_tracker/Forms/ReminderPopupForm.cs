using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using work_tracker.Data.Entities;
using work_tracker.Services;
using work_tracker.Helpers;

namespace work_tracker.Forms
{
    /// <summary>
    /// Hatırlatıcı popup formu - Zamanı gelen hatırlatıcıları gösterir
    /// </summary>
    public partial class ReminderPopupForm : XtraForm
    {
        private readonly List<WorkItemReminder> _reminders;
        private readonly WorkItemReminderService _reminderService;
        private int _currentIndex = 0;

        public ReminderPopupForm(List<WorkItemReminder> reminders, WorkItemReminderService reminderService)
        {
            _reminders = reminders;
            _reminderService = reminderService;
            
            InitializeComponents();
            ShowCurrentReminder();
        }

        private void InitializeComponents()
        {
            this.Text = "🔔 Hatırlatıcı";
            this.Size = new Size(450, 320);
            this.StartPosition = FormStartPosition.Manual;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.TopMost = true;
            this.ShowInTaskbar = true;

            // Ekranın sağ alt köşesine konumlandır
            var screen = Screen.PrimaryScreen.WorkingArea;
            this.Location = new Point(screen.Right - this.Width - 20, screen.Bottom - this.Height - 20);

            // Ana panel
            var mainPanel = new PanelControl
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15)
            };

            // Başlık paneli
            var headerPanel = new PanelControl
            {
                Dock = DockStyle.Top,
                Height = 50
            };

            var lblIcon = new LabelControl
            {
                Text = "🔔",
                Location = new Point(10, 10),
                Font = new Font("Segoe UI", 20)
            };

            var lblTitle = new LabelControl
            {
                Text = "Hatırlatıcı",
                Location = new Point(50, 15),
                Font = new Font("Segoe UI Semibold", 14)
            };

            lblCounter = new LabelControl
            {
                Location = new Point(350, 15),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };

            headerPanel.Controls.Add(lblIcon);
            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(lblCounter);

            // İçerik paneli
            var contentPanel = new PanelControl
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };

            lblWorkItemId = new LabelControl
            {
                Location = new Point(10, 10),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(0, 122, 204)
            };

            lblWorkItemTitle = new LabelControl
            {
                Location = new Point(10, 35),
                Font = new Font("Segoe UI Semibold", 11),
                AutoSizeMode = LabelAutoSizeMode.None,
                Width = 380
            };

            lblProject = new LabelControl
            {
                Location = new Point(10, 65),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };

            lblNote = new MemoEdit
            {
                Location = new Point(10, 95),
                Size = new Size(380, 50),
                Properties = { ReadOnly = true }
            };
            lblNote.Properties.Appearance.BackColor = Color.FromArgb(255, 255, 240);

            lblReminderTime = new LabelControl
            {
                Location = new Point(10, 155),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.DarkOrange
            };

            contentPanel.Controls.Add(lblWorkItemId);
            contentPanel.Controls.Add(lblWorkItemTitle);
            contentPanel.Controls.Add(lblProject);
            contentPanel.Controls.Add(lblNote);
            contentPanel.Controls.Add(lblReminderTime);

            // Buton paneli
            var buttonPanel = new PanelControl
            {
                Dock = DockStyle.Bottom,
                Height = 80
            };

            // Erteleme seçenekleri
            var snoozePanel = new PanelControl
            {
                Dock = DockStyle.Top,
                Height = 35
            };

            var lblSnooze = new LabelControl
            {
                Text = "Ertele:",
                Location = new Point(10, 8),
                Font = new Font("Segoe UI", 9)
            };

            btnSnooze5 = new SimpleButton
            {
                Text = "5 dk",
                Location = new Point(60, 5),
                Size = new Size(55, 25)
            };
            btnSnooze5.Click += (s, e) => SnoozeCurrentReminder(TimeSpan.FromMinutes(5));

            btnSnooze15 = new SimpleButton
            {
                Text = "15 dk",
                Location = new Point(120, 5),
                Size = new Size(55, 25)
            };
            btnSnooze15.Click += (s, e) => SnoozeCurrentReminder(TimeSpan.FromMinutes(15));

            btnSnooze1h = new SimpleButton
            {
                Text = "1 saat",
                Location = new Point(180, 5),
                Size = new Size(55, 25)
            };
            btnSnooze1h.Click += (s, e) => SnoozeCurrentReminder(TimeSpan.FromHours(1));

            btnSnoozeTomorrow = new SimpleButton
            {
                Text = "Yarın 09:00",
                Location = new Point(240, 5),
                Size = new Size(85, 25)
            };
            btnSnoozeTomorrow.Click += (s, e) => 
            {
                var tomorrow9am = DateTime.Today.AddDays(1).AddHours(9);
                SnoozeCurrentReminder(tomorrow9am - DateTime.Now);
            };

            snoozePanel.Controls.Add(lblSnooze);
            snoozePanel.Controls.Add(btnSnooze5);
            snoozePanel.Controls.Add(btnSnooze15);
            snoozePanel.Controls.Add(btnSnooze1h);
            snoozePanel.Controls.Add(btnSnoozeTomorrow);

            // Ana butonlar
            var actionPanel = new PanelControl
            {
                Dock = DockStyle.Bottom,
                Height = 40
            };

            btnOpenWorkItem = new SimpleButton
            {
                Text = "📋 İşi Aç",
                Location = new Point(10, 5),
                Size = new Size(100, 30)
            };
            btnOpenWorkItem.Click += BtnOpenWorkItem_Click;

            btnDismiss = new SimpleButton
            {
                Text = "✓ Tamam",
                Location = new Point(120, 5),
                Size = new Size(100, 30)
            };
            btnDismiss.Appearance.BackColor = Color.FromArgb(16, 124, 16);
            btnDismiss.Appearance.ForeColor = Color.White;
            btnDismiss.Click += BtnDismiss_Click;

            btnDismissAll = new SimpleButton
            {
                Text = "✓ Tümünü Kapat",
                Location = new Point(230, 5),
                Size = new Size(120, 30)
            };
            btnDismissAll.Click += BtnDismissAll_Click;

            btnNext = new SimpleButton
            {
                Text = "Sonraki →",
                Location = new Point(360, 5),
                Size = new Size(80, 30)
            };
            btnNext.Click += BtnNext_Click;

            actionPanel.Controls.Add(btnOpenWorkItem);
            actionPanel.Controls.Add(btnDismiss);
            actionPanel.Controls.Add(btnDismissAll);
            actionPanel.Controls.Add(btnNext);

            buttonPanel.Controls.Add(actionPanel);
            buttonPanel.Controls.Add(snoozePanel);

            mainPanel.Controls.Add(contentPanel);
            mainPanel.Controls.Add(headerPanel);
            mainPanel.Controls.Add(buttonPanel);

            this.Controls.Add(mainPanel);

            // Keyboard shortcuts
            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                    this.Close();
                else if (e.KeyCode == Keys.Enter)
                    BtnDismiss_Click(s, e);
                else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.N)
                    BtnNext_Click(s, e);
            };
        }

        // UI Controls
        private LabelControl lblCounter;
        private LabelControl lblWorkItemId;
        private LabelControl lblWorkItemTitle;
        private LabelControl lblProject;
        private MemoEdit lblNote;
        private LabelControl lblReminderTime;
        private SimpleButton btnSnooze5;
        private SimpleButton btnSnooze15;
        private SimpleButton btnSnooze1h;
        private SimpleButton btnSnoozeTomorrow;
        private SimpleButton btnOpenWorkItem;
        private SimpleButton btnDismiss;
        private SimpleButton btnDismissAll;
        private SimpleButton btnNext;

        private void ShowCurrentReminder()
        {
            if (_reminders.Count == 0 || _currentIndex >= _reminders.Count)
            {
                this.Close();
                return;
            }

            var reminder = _reminders[_currentIndex];

            lblCounter.Text = $"{_currentIndex + 1} / {_reminders.Count}";
            lblWorkItemId.Text = $"#{reminder.WorkItemId}";
            lblWorkItemTitle.Text = reminder.WorkItem?.Title ?? "Bilinmeyen İş";
            lblProject.Text = $"📁 {reminder.WorkItem?.Project?.Name ?? "-"}";
            lblNote.Text = reminder.Note ?? "";
            lblNote.Visible = !string.IsNullOrEmpty(reminder.Note);

            var timeDiff = DateTime.Now - reminder.ReminderDate;
            if (timeDiff.TotalMinutes < 1)
                lblReminderTime.Text = "⏰ Şimdi";
            else if (timeDiff.TotalMinutes < 60)
                lblReminderTime.Text = $"⏰ {(int)timeDiff.TotalMinutes} dakika önce";
            else if (timeDiff.TotalHours < 24)
                lblReminderTime.Text = $"⏰ {(int)timeDiff.TotalHours} saat önce";
            else
                lblReminderTime.Text = $"⏰ {reminder.ReminderDate:dd.MM.yyyy HH:mm}";

            if (reminder.SnoozeCount > 0)
                lblReminderTime.Text += $" (Ertelenme: {reminder.SnoozeCount}x)";

            // Sonraki butonu görünürlüğü
            btnNext.Visible = _reminders.Count > 1;
            btnDismissAll.Visible = _reminders.Count > 1;
        }

        private void SnoozeCurrentReminder(TimeSpan snoozeTime)
        {
            if (_currentIndex < _reminders.Count)
            {
                var reminder = _reminders[_currentIndex];
                _reminderService.SnoozeReminder(reminder.Id, snoozeTime);
                _reminders.RemoveAt(_currentIndex);
                
                if (_currentIndex >= _reminders.Count && _reminders.Count > 0)
                    _currentIndex = 0;
                
                ShowCurrentReminder();
            }
        }

        private void BtnOpenWorkItem_Click(object sender, EventArgs e)
        {
            if (_currentIndex < _reminders.Count)
            {
                var reminder = _reminders[_currentIndex];
                try
                {
                    var detailForm = new WorkItemDetailForm(reminder.WorkItemId);
                    detailForm.Show();
                }
                catch (Exception ex)
                {
                    Logger.Error("İş detay formu açılırken hata", ex);
                }
            }
        }

        private void BtnDismiss_Click(object sender, EventArgs e)
        {
            if (_currentIndex < _reminders.Count)
            {
                var reminder = _reminders[_currentIndex];
                _reminderService.DismissReminder(reminder.Id);
                _reminders.RemoveAt(_currentIndex);
                
                if (_currentIndex >= _reminders.Count && _reminders.Count > 0)
                    _currentIndex = 0;
                
                ShowCurrentReminder();
            }
        }

        private void BtnDismissAll_Click(object sender, EventArgs e)
        {
            foreach (var reminder in _reminders.ToList())
            {
                _reminderService.DismissReminder(reminder.Id);
            }
            _reminders.Clear();
            this.Close();
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (_reminders.Count > 1)
            {
                _currentIndex = (_currentIndex + 1) % _reminders.Count;
                ShowCurrentReminder();
            }
        }
    }
}
