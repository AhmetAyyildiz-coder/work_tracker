using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using work_tracker.Forms;
using work_tracker.Helpers;

namespace work_tracker
{
    public partial class MainForm : RibbonForm
    {
        // Form instance'larını saklamak için
        private InboxForm inboxForm;
        private TriageForm triageForm;
        private KanbanBoardForm kanbanForm;
        private ScrumBoardForm scrumForm;
        private MeetingForm meetingForm;
        private ProjectManagementForm projectForm;
        private ModuleManagementForm moduleForm;
        private SprintManagementForm sprintForm;
        private ReportsForm reportsForm;
        private HelpForm helpForm;
        private AllWorkItemsForm allWorkItemsForm;

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

        private void btnTriage_ItemClick(object sender, ItemClickEventArgs e)
        {
            triageForm = OpenOrActivateForm(triageForm, () => new TriageForm(), f => triageForm = f);
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
                }
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
    }
}

