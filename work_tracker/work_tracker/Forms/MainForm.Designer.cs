using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace work_tracker
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private RibbonControl ribbonControl1;
        private RibbonPage ribbonPageHome;
        private RibbonPageGroup ribbonPageGroupWorkflow;
        private RibbonPageGroup ribbonPageGroupSettings;
        private RibbonPageGroup ribbonPageGroupHelp;
        private BarButtonItem btnInbox;
        private BarButtonItem btnTriage;
        private BarButtonItem btnKanban;
        private BarButtonItem btnScrum;
        private BarButtonItem btnMeetings;
        private BarButtonItem btnProjects;
        private BarButtonItem btnModules;
        private BarButtonItem btnSprints;
        private BarButtonItem btnReports;
        private BarButtonItem btnHelp;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnInbox = new DevExpress.XtraBars.BarButtonItem();
            this.btnTriage = new DevExpress.XtraBars.BarButtonItem();
            this.btnKanban = new DevExpress.XtraBars.BarButtonItem();
            this.btnScrum = new DevExpress.XtraBars.BarButtonItem();
            this.btnMeetings = new DevExpress.XtraBars.BarButtonItem();
            this.btnProjects = new DevExpress.XtraBars.BarButtonItem();
            this.btnModules = new DevExpress.XtraBars.BarButtonItem();
            this.btnSprints = new DevExpress.XtraBars.BarButtonItem();
            this.btnReports = new DevExpress.XtraBars.BarButtonItem();
            this.btnHelp = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageHome = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupWorkflow = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupSettings = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupHelp = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.btnInbox,
            this.btnTriage,
            this.btnKanban,
            this.btnScrum,
            this.btnMeetings,
            this.btnProjects,
            this.btnModules,
            this.btnSprints,
            this.btnReports,
            this.btnHelp});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 11;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageHome});
            this.ribbonControl1.Size = new System.Drawing.Size(1200, 158);
            // 
            // btnInbox
            // 
            this.btnInbox.Caption = "Gelen Kutusu";
            this.btnInbox.Id = 1;
            this.btnInbox.Name = "btnInbox";
            this.btnInbox.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnInbox_ItemClick);
            // 
            // btnTriage
            // 
            this.btnTriage.Caption = "Sınıflandırma";
            this.btnTriage.Id = 2;
            this.btnTriage.Name = "btnTriage";
            this.btnTriage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnTriage_ItemClick);
            // 
            // btnKanban
            // 
            this.btnKanban.Caption = "Kanban Panosu";
            this.btnKanban.Id = 3;
            this.btnKanban.Name = "btnKanban";
            this.btnKanban.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnKanban_ItemClick);
            // 
            // btnScrum
            // 
            this.btnScrum.Caption = "Scrum Panosu";
            this.btnScrum.Id = 8;
            this.btnScrum.Name = "btnScrum";
            this.btnScrum.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnScrum_ItemClick);
            // 
            // btnMeetings
            // 
            this.btnMeetings.Caption = "Toplantılar";
            this.btnMeetings.Id = 4;
            this.btnMeetings.Name = "btnMeetings";
            this.btnMeetings.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMeetings_ItemClick);
            // 
            // btnProjects
            // 
            this.btnProjects.Caption = "Projeler";
            this.btnProjects.Id = 5;
            this.btnProjects.Name = "btnProjects";
            this.btnProjects.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnProjects_ItemClick);
            // 
            // btnModules
            // 
            this.btnModules.Caption = "Modüller";
            this.btnModules.Id = 6;
            this.btnModules.Name = "btnModules";
            this.btnModules.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnModules_ItemClick);
            // 
            // btnSprints
            // 
            this.btnSprints.Caption = "Sprint Yönetimi";
            this.btnSprints.Id = 9;
            this.btnSprints.Name = "btnSprints";
            this.btnSprints.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSprints_ItemClick);
            // 
            // btnReports
            // 
            this.btnReports.Caption = "Raporlar";
            this.btnReports.Id = 10;
            this.btnReports.Name = "btnReports";
            this.btnReports.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnReports_ItemClick);
            // 
            // btnHelp
            // 
            this.btnHelp.Caption = "Nasıl Kullanılır?";
            this.btnHelp.Id = 7;
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnHelp_ItemClick);
            // 
            // ribbonPageHome
            // 
            this.ribbonPageHome.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupWorkflow,
            this.ribbonPageGroupSettings,
            this.ribbonPageGroupHelp});
            this.ribbonPageHome.Name = "ribbonPageHome";
            this.ribbonPageHome.Text = "Ana Sayfa";
            // 
            // ribbonPageGroupWorkflow
            // 
            this.ribbonPageGroupWorkflow.ItemLinks.Add(this.btnInbox);
            this.ribbonPageGroupWorkflow.ItemLinks.Add(this.btnTriage);
            this.ribbonPageGroupWorkflow.ItemLinks.Add(this.btnKanban);
            this.ribbonPageGroupWorkflow.ItemLinks.Add(this.btnScrum);
            this.ribbonPageGroupWorkflow.ItemLinks.Add(this.btnMeetings);
            this.ribbonPageGroupWorkflow.Name = "ribbonPageGroupWorkflow";
            this.ribbonPageGroupWorkflow.Text = "İş Akışı";
            // 
            // ribbonPageGroupSettings
            // 
            this.ribbonPageGroupSettings.ItemLinks.Add(this.btnProjects);
            this.ribbonPageGroupSettings.ItemLinks.Add(this.btnModules);
            this.ribbonPageGroupSettings.ItemLinks.Add(this.btnSprints);
            this.ribbonPageGroupSettings.ItemLinks.Add(this.btnReports);
            this.ribbonPageGroupSettings.Name = "ribbonPageGroupSettings";
            this.ribbonPageGroupSettings.Text = "Ayarlar";
            // 
            // ribbonPageGroupHelp
            // 
            this.ribbonPageGroupHelp.ItemLinks.Add(this.btnHelp);
            this.ribbonPageGroupHelp.Name = "ribbonPageGroupHelp";
            this.ribbonPageGroupHelp.Text = "Yardım";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.ribbonControl1);
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Ribbon = this.ribbonControl1;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "İş Akışı Yönetim Aracı";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

