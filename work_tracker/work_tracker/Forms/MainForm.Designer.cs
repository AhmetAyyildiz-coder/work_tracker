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
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip trayContextMenu;
        private System.Windows.Forms.ToolStripMenuItem trayMenuOpen;
        private System.Windows.Forms.ToolStripMenuItem trayMenuReminder;
        private System.Windows.Forms.ToolStripSeparator trayMenuSeparator;
        private System.Windows.Forms.ToolStripMenuItem trayMenuExit;
        private BarButtonItem btnInbox;
        private BarButtonItem btnKanban;
        private BarButtonItem btnScrum;
        private BarButtonItem btnMeetings;
        private BarButtonItem btnProjects;
        private BarButtonItem btnModules;
        private BarButtonItem btnSprints;
        private BarButtonItem btnReports;
        private BarButtonItem btnHelp;
        private BarButtonItem btnAllWorkItems;
        private BarButtonItem btnWiki;
        private BarButtonItem btnTimeEntry;
        private BarButtonItem btnReminder;
        private BarButtonItem btnWorkSummary;
        private BarButtonItem btnHierarchy;

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
            this.components = new System.ComponentModel.Container();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.trayMenuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.trayMenuReminder = new System.Windows.Forms.ToolStripMenuItem();
            this.trayMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.trayMenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.trayContextMenu.SuspendLayout();
            this.btnInbox = new DevExpress.XtraBars.BarButtonItem();
            this.btnKanban = new DevExpress.XtraBars.BarButtonItem();
            this.btnScrum = new DevExpress.XtraBars.BarButtonItem();
            this.btnMeetings = new DevExpress.XtraBars.BarButtonItem();
            this.btnProjects = new DevExpress.XtraBars.BarButtonItem();
            this.btnModules = new DevExpress.XtraBars.BarButtonItem();
            this.btnSprints = new DevExpress.XtraBars.BarButtonItem();
            this.btnReports = new DevExpress.XtraBars.BarButtonItem();
            this.btnHelp = new DevExpress.XtraBars.BarButtonItem();
            this.btnAllWorkItems = new DevExpress.XtraBars.BarButtonItem();
            this.btnWiki = new DevExpress.XtraBars.BarButtonItem();
            this.btnTimeEntry = new DevExpress.XtraBars.BarButtonItem();
            this.btnReminder = new DevExpress.XtraBars.BarButtonItem();
            this.btnWorkSummary = new DevExpress.XtraBars.BarButtonItem();
            this.btnHierarchy = new DevExpress.XtraBars.BarButtonItem();
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
            this.btnKanban,
            this.btnScrum,
            this.btnMeetings,
            this.btnProjects,
            this.btnModules,
            this.btnSprints,
            this.btnReports,
            this.btnHelp,
            this.btnAllWorkItems,
            this.btnWiki,
            this.btnTimeEntry,
            this.btnReminder,
            this.btnWorkSummary,
            this.btnHierarchy});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 14;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageHome});
            this.ribbonControl1.Size = new System.Drawing.Size(1200, 158);
            // 
            // btnInbox
            // 
            this.btnInbox.Caption = "📥 Gelen Kutusu";
            this.btnInbox.Id = 1;
            this.btnInbox.Name = "btnInbox";
            this.btnInbox.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnInbox_ItemClick);
            // 
            // btnKanban
            // 
            this.btnKanban.Caption = "📋 Kanban Panosu";
            this.btnKanban.Id = 3;
            this.btnKanban.Name = "btnKanban";
            this.btnKanban.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnKanban_ItemClick);
            // 
            // btnScrum
            // 
            this.btnScrum.Caption = "🏃 Scrum Panosu";
            this.btnScrum.Id = 8;
            this.btnScrum.Name = "btnScrum";
            this.btnScrum.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnScrum_ItemClick);
            // 
            // btnMeetings
            // 
            this.btnMeetings.Caption = "📅 Toplantılar";
            this.btnMeetings.Id = 4;
            this.btnMeetings.Name = "btnMeetings";
            this.btnMeetings.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMeetings_ItemClick);
            // 
            // btnProjects
            // 
            this.btnProjects.Caption = "📁 Projeler";
            this.btnProjects.Id = 5;
            this.btnProjects.Name = "btnProjects";
            this.btnProjects.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnProjects_ItemClick);
            // 
            // btnModules
            // 
            this.btnModules.Caption = "📦 Modüller";
            this.btnModules.Id = 6;
            this.btnModules.Name = "btnModules";
            this.btnModules.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnModules_ItemClick);
            // 
            // btnSprints
            // 
            this.btnSprints.Caption = "🔄 Sprint Yönetimi";
            this.btnSprints.Id = 9;
            this.btnSprints.Name = "btnSprints";
            this.btnSprints.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSprints_ItemClick);
            // 
            // btnReports
            // 
            this.btnReports.Caption = "📈 Raporlar";
            this.btnReports.Id = 10;
            this.btnReports.Name = "btnReports";
            this.btnReports.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnReports_ItemClick);
            // 
            // btnHelp
            // 
            this.btnHelp.Caption = "❓ Nasıl Kullanılır?";
            this.btnHelp.Id = 7;
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnHelp_ItemClick);
            // 
            // btnAllWorkItems
            // 
            this.btnAllWorkItems.Caption = "📋 Tüm İşler";
            this.btnAllWorkItems.Id = 11;
            this.btnAllWorkItems.Name = "btnAllWorkItems";
            this.btnAllWorkItems.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAllWorkItems_ItemClick);
            // 
            // btnWiki
            // 
            this.btnWiki.Caption = "📚 Wiki";
            this.btnWiki.Id = 12;
            this.btnWiki.Name = "btnWiki";
            this.btnWiki.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnWiki_ItemClick);
            //
            // btnTimeEntry
            //
            this.btnTimeEntry.Caption = "⏱️ Zaman Kayıtları";
            this.btnTimeEntry.Id = 13;
            this.btnTimeEntry.Name = "btnTimeEntry";
            this.btnTimeEntry.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnTimeEntry_ItemClick);
            //
            // btnReminder
            //
            this.btnReminder.Caption = "🔔 Şimdi Hatırlat";
            this.btnReminder.Id = 14;
            this.btnReminder.Name = "btnReminder";
            this.btnReminder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnReminder_ItemClick);
            //
            // btnWorkSummary
            //
            this.btnWorkSummary.Caption = "📊 Çalışma Özeti";
            this.btnWorkSummary.Id = 15;
            this.btnWorkSummary.Name = "btnWorkSummary";
            this.btnWorkSummary.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnWorkSummary_ItemClick);
            //
            // btnHierarchy
            //
            this.btnHierarchy.Caption = "🔗 İş Hiyerarşisi";
            this.btnHierarchy.Id = 16;
            this.btnHierarchy.Name = "btnHierarchy";
            this.btnHierarchy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnHierarchy_ItemClick);
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
            this.ribbonPageGroupWorkflow.ItemLinks.Add(this.btnKanban);
            this.ribbonPageGroupWorkflow.ItemLinks.Add(this.btnScrum);
            this.ribbonPageGroupWorkflow.ItemLinks.Add(this.btnMeetings);
            this.ribbonPageGroupWorkflow.ItemLinks.Add(this.btnAllWorkItems);
            this.ribbonPageGroupWorkflow.ItemLinks.Add(this.btnTimeEntry);
            this.ribbonPageGroupWorkflow.ItemLinks.Add(this.btnWorkSummary);
            this.ribbonPageGroupWorkflow.ItemLinks.Add(this.btnHierarchy);
            this.ribbonPageGroupWorkflow.Name = "ribbonPageGroupWorkflow";
            this.ribbonPageGroupWorkflow.Text = "İş Akışı";
            // 
            // ribbonPageGroupSettings
            // 
            this.ribbonPageGroupSettings.ItemLinks.Add(this.btnProjects);
            this.ribbonPageGroupSettings.ItemLinks.Add(this.btnModules);
            this.ribbonPageGroupSettings.ItemLinks.Add(this.btnSprints);
            this.ribbonPageGroupSettings.ItemLinks.Add(this.btnWiki);
            this.ribbonPageGroupSettings.ItemLinks.Add(this.btnReports);
            this.ribbonPageGroupSettings.Name = "ribbonPageGroupSettings";
            this.ribbonPageGroupSettings.Text = "Ayarlar";
            // 
            // ribbonPageGroupHelp
            // 
            this.ribbonPageGroupHelp.ItemLinks.Add(this.btnHelp);
            this.ribbonPageGroupHelp.ItemLinks.Add(this.btnReminder);
            this.ribbonPageGroupHelp.Name = "ribbonPageGroupHelp";
            this.ribbonPageGroupHelp.Text = "Yardım";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.trayContextMenu;
            this.notifyIcon1.Icon = System.Drawing.SystemIcons.Application;
            this.notifyIcon1.Text = "Work Tracker";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // trayContextMenu
            // 
            this.trayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trayMenuOpen,
            this.trayMenuReminder,
            this.trayMenuSeparator,
            this.trayMenuExit});
            this.trayContextMenu.Name = "trayContextMenu";
            this.trayContextMenu.Size = new System.Drawing.Size(200, 76);
            // 
            // trayMenuOpen
            // 
            this.trayMenuOpen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.trayMenuOpen.Name = "trayMenuOpen";
            this.trayMenuOpen.Size = new System.Drawing.Size(199, 22);
            this.trayMenuOpen.Text = "Work Tracker'ı Aç";
            this.trayMenuOpen.Click += new System.EventHandler(this.trayMenuOpen_Click);
            // 
            // trayMenuReminder
            // 
            this.trayMenuReminder.Name = "trayMenuReminder";
            this.trayMenuReminder.Size = new System.Drawing.Size(199, 22);
            this.trayMenuReminder.Text = "Şimdi Hatırlat";
            this.trayMenuReminder.Click += new System.EventHandler(this.trayMenuReminder_Click);
            // 
            // trayMenuSeparator
            // 
            this.trayMenuSeparator.Name = "trayMenuSeparator";
            this.trayMenuSeparator.Size = new System.Drawing.Size(196, 6);
            // 
            // trayMenuExit
            // 
            this.trayMenuExit.Name = "trayMenuExit";
            this.trayMenuExit.Size = new System.Drawing.Size(199, 22);
            this.trayMenuExit.Text = "Çıkış";
            this.trayMenuExit.Click += new System.EventHandler(this.trayMenuExit_Click);
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
            this.trayContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

