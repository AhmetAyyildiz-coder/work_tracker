using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using work_tracker.Forms;
using work_tracker.Helpers;
using work_tracker.Services;

namespace work_tracker
{
    public partial class MainForm : RibbonForm
    {
        // Form instance'larını saklamak için
        private InboxForm inboxForm;
        private KanbanBoardForm kanbanForm;
        private ScrumBoardForm scrumForm;
        private MeetingForm meetingForm;
        private ProjectManagementForm projectForm;
        private ModuleManagementForm moduleForm;
        private SprintManagementForm sprintForm;
        private ReportsForm reportsForm;
        private HelpForm helpForm;
        private AllWorkItemsForm allWorkItemsForm;
        private WikiForm wikiForm;
        private TimeEntryForm timeEntryForm;
        private WorkSummaryForm workSummaryForm;
        private WorkItemHierarchyForm hierarchyForm;
        private DashboardForm dashboardForm;

        // Günlük hatırlatıcı servisi
        private WorkReminderService _reminderService;

        public MainForm()
        {
            InitializeComponent();
            InitializeRibbon();
        }

        private void InitializeRibbon()
        {
            // Ribbon sayfaları ve butonlar designer'da tanımlanacak
        }

        private void btnInbox_ItemClick(object sender, ItemClickEventArgs e)
        {
            inboxForm = OpenOrActivateForm(inboxForm, () => new InboxForm(), f => inboxForm = f);
        }

        private void btnKanban_ItemClick(object sender, ItemClickEventArgs e)
        {
            kanbanForm = OpenOrActivateForm(kanbanForm, () => new KanbanBoardForm(), f => kanbanForm = f);
        }

        private void btnScrum_ItemClick(object sender, ItemClickEventArgs e)
        {
            scrumForm = OpenOrActivateForm(scrumForm, () => new ScrumBoardForm(), f => scrumForm = f);
        }

        private void btnMeetings_ItemClick(object sender, ItemClickEventArgs e)
        {
            meetingForm = OpenOrActivateForm(meetingForm, () => new MeetingForm(), f => meetingForm = f);
        }

        private void btnProjects_ItemClick(object sender, ItemClickEventArgs e)
        {
            projectForm = OpenOrActivateForm(projectForm, () => new ProjectManagementForm(), f => projectForm = f);
        }

        private void btnModules_ItemClick(object sender, ItemClickEventArgs e)
        {
            moduleForm = OpenOrActivateForm(moduleForm, () => new ModuleManagementForm(), f => moduleForm = f);
        }

        private void btnSprints_ItemClick(object sender, ItemClickEventArgs e)
        {
            sprintForm = OpenOrActivateForm(sprintForm, () => new SprintManagementForm(), f => sprintForm = f);
        }

        private void btnReports_ItemClick(object sender, ItemClickEventArgs e)
        {
            reportsForm = OpenOrActivateForm(reportsForm, () => new ReportsForm(), f => reportsForm = f);
        }

        private void btnHelp_ItemClick(object sender, ItemClickEventArgs e)
        {
            helpForm = OpenOrActivateForm(helpForm, () => new HelpForm(), f => helpForm = f, isDialog: true);
        }

        private void btnAllWorkItems_ItemClick(object sender, ItemClickEventArgs e)
        {
            allWorkItemsForm = OpenOrActivateForm(allWorkItemsForm, () => new AllWorkItemsForm(), f => allWorkItemsForm = f);
        }

        private void btnWiki_ItemClick(object sender, ItemClickEventArgs e)
        {
            wikiForm = OpenOrActivateForm(wikiForm, () => new WikiForm(), f => wikiForm = f);
        }

        private void btnTimeEntry_ItemClick(object sender, ItemClickEventArgs e)
        {
            timeEntryForm = OpenOrActivateForm(timeEntryForm, () => new TimeEntryForm(), f => timeEntryForm = f);
        }

        private void btnWorkSummary_ItemClick(object sender, ItemClickEventArgs e)
        {
            workSummaryForm = OpenOrActivateForm(workSummaryForm, () => new WorkSummaryForm(), f => workSummaryForm = f);
        }

        private void btnHierarchy_ItemClick(object sender, ItemClickEventArgs e)
        {
            hierarchyForm = OpenOrActivateForm(hierarchyForm, () => new WorkItemHierarchyForm(), f => hierarchyForm = f);
        }

        private void btnDashboard_ItemClick(object sender, ItemClickEventArgs e)
        {
            dashboardForm = OpenOrActivateForm(dashboardForm, () => new DashboardForm(), f => dashboardForm = f);
        }

        /// <summary>
        /// Log klasörünü aç - Designer'dan bağlanacak
        /// </summary>
        private void btnOpenLogs_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Logger.Info("Kullanıcı log klasörünü açtı");
                Logger.OpenLogFolder();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Log klasörü açılırken hata");
                XtraMessageBox.Show(
                    "Log klasörü açılamadı!\n\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Form'u aç veya zaten açıksa öne getir
        /// </summary>
        private T OpenOrActivateForm<T>(T formInstance, Func<T> createForm, Action<T> updateInstance, bool isDialog = false) where T : Form
        {
            // Eğer form null veya kapatılmışsa
            if (formInstance == null || formInstance.IsDisposed)
            {
                formInstance = createForm();
                
                if (!isDialog)
                {
                    formInstance.MdiParent = this;
                    formInstance.WindowState = FormWindowState.Maximized;
                }
                
                // Form kapatıldığında reference'ı temizle
                formInstance.FormClosed += (s, e) => updateInstance(null);
                
                formInstance.Show();
            }
            else
            {
                // Form zaten açık, öne getir
                formInstance.Activate();
                
                if (formInstance.WindowState == FormWindowState.Minimized)
                {
                    formInstance.WindowState = FormWindowState.Maximized;
                }
            }
            
            return formInstance;
        }

        private void OpenForm(Form form)
        {
            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // İlk açılışta veritabanını kontrol et
            try
            {
                Logger.Info("MainForm yükleniyor, veritabanı kontrolü başlıyor");
                
                using (var db = new Data.WorkTrackerDbContext())
                {
                    // Veritabanı bağlantısını test et
                    var projectCount = db.Projects.Count();
                    Logger.Info($"Veritabanı bağlantısı başarılı. {projectCount} proje bulundu.");

                    // Mevcut email kayıtlarını migrate et (ConversationId eksik olanlar için)
                    MigrateExistingEmailsIfNeeded(db);
                }

                // Günlük hatırlatıcı servisini başlat (17:30'da)
                _reminderService = new WorkReminderService(notifyIcon1, 17, 30);
                Logger.Info("Günlük hatırlatıcı servisi başlatıldı (17:30)");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Veritabanı bağlantısı kurulamadı");
                
                XtraMessageBox.Show(
                    "Veritabanı bağlantısı kurulamadı. Lütfen önce migration çalıştırın:\n\n" +
                    "Package Manager Console'da:\n" +
                    "Enable-Migrations\n" +
                    "Add-Migration InitialCreate\n" +
                    "Update-Database\n\n" +
                    "Hata: " + ex.Message,
                    "Veritabanı Hatası",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Eski email kayıtlarına ConversationId ekle (bir kerelik migration)
        /// </summary>
        private void MigrateExistingEmailsIfNeeded(Data.WorkTrackerDbContext db)
        {
            try
            {
                // ConversationId'si olmayan emailleri bul
                var emailsToMigrate = db.WorkItemEmails
                    .Where(e => e.ConversationId == null && e.OutlookEntryId != null)
                    .ToList();

                if (emailsToMigrate.Count == 0)
                    return;

                Logger.Info($"Email migration başlıyor: {emailsToMigrate.Count} kayıt güncellenecek");

                // Outlook'tan ConversationId'leri çek
                int updatedCount = OutlookHelper.MigrateExistingEmails(emailsToMigrate);

                // Değişiklikleri kaydet
                if (updatedCount > 0)
                {
                    db.SaveChanges();
                    Logger.Info($"Email migration tamamlandı: {updatedCount}/{emailsToMigrate.Count} kayıt güncellendi");
                }
            }
            catch (Exception ex)
            {
                // Migration hatası kritik değil, sessizce logla
                Logger.Warning($"Email migration sırasında hata (kritik değil): {ex.Message}");
            }
        }

        #region Tray Icon Events

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            ShowMainForm();
        }

        private void trayMenuOpen_Click(object sender, EventArgs e)
        {
            ShowMainForm();
        }

        private void trayMenuReminder_Click(object sender, EventArgs e)
        {
            // Manuel hatırlatma tetikle
            _reminderService?.TriggerReminderNow();
        }

        private void btnReminder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Ribbon'dan manuel hatırlatma tetikle
            _reminderService?.TriggerReminderNow();
        }

        private void trayMenuExit_Click(object sender, EventArgs e)
        {
            // Uygulamayı tamamen kapat
            _reminderService?.Dispose();
            notifyIcon1.Visible = false;
            Application.Exit();
        }

        private void ShowMainForm()
        {
            this.Show();
            this.WindowState = FormWindowState.Maximized;
            this.Activate();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // X'e basıldığında uygulamayı kapatma, tray'e küçült
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
                notifyIcon1.ShowBalloonTip(
                    3000,
                    "Work Tracker",
                    "Uygulama arka planda çalışmaya devam ediyor.\n17:30'da aktif işleriniz için hatırlatma alacaksınız.",
                    ToolTipIcon.Info
                );
            }
            else
            {
                _reminderService?.Dispose();
                notifyIcon1.Visible = false;
            }

            base.OnFormClosing(e);
        }

        #endregion
    }
}

