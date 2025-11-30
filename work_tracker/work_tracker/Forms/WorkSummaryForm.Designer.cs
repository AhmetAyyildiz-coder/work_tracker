using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;

namespace work_tracker.Forms
{
    partial class WorkSummaryForm
    {
        private System.ComponentModel.IContainer components = null;

        // Üst panel - Dönem seçimi
        private PanelControl panelTop;
        private SimpleButton btnToday;
        private SimpleButton btnThisWeek;
        private SimpleButton btnThisMonth;
        private DateEdit dtStart;
        private DateEdit dtEnd;
        private SimpleButton btnCustomRange;
        private SimpleButton btnRefresh;
        private LabelControl lblDateRange;

        // Özet kartları
        private PanelControl panelSummary;
        private GroupControl cardTotalTime;
        private GroupControl cardWorkedItems;
        private GroupControl cardCompletedItems;
        private GroupControl cardActivityCount;
        private LabelControl lblTotalTime;
        private LabelControl lblWorkedItems;
        private LabelControl lblCompletedItems;
        private LabelControl lblActivityCount;
        private LabelControl lblTotalTimeTitle;
        private LabelControl lblWorkedItemsTitle;
        private LabelControl lblCompletedItemsTitle;
        private LabelControl lblActivityCountTitle;

        // Tab Control
        private XtraTabControl tabControl;
        private XtraTabPage tabTimeDistribution;
        private XtraTabPage tabActivities;
        private XtraTabPage tabCompleted;

        // Grid'ler
        private GridControl gridTimeDistribution;
        private GridView gridViewTimeDistribution;
        private GridControl gridActivities;
        private GridView gridViewActivities;
        private GridControl gridCompleted;
        private GridView gridViewCompleted;

        // Alt panel - Butonlar
        private PanelControl panelBottom;
        private SimpleButton btnCopyToClipboard;
        private SimpleButton btnExportExcel;
        private SimpleButton btnClose;

        private void InitializeComponent()
        {
            this.panelTop = new DevExpress.XtraEditors.PanelControl();
            this.btnToday = new DevExpress.XtraEditors.SimpleButton();
            this.btnThisWeek = new DevExpress.XtraEditors.SimpleButton();
            this.btnThisMonth = new DevExpress.XtraEditors.SimpleButton();
            this.dtStart = new DevExpress.XtraEditors.DateEdit();
            this.dtEnd = new DevExpress.XtraEditors.DateEdit();
            this.btnCustomRange = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.lblDateRange = new DevExpress.XtraEditors.LabelControl();
            
            this.panelSummary = new DevExpress.XtraEditors.PanelControl();
            this.cardTotalTime = new DevExpress.XtraEditors.GroupControl();
            this.cardWorkedItems = new DevExpress.XtraEditors.GroupControl();
            this.cardCompletedItems = new DevExpress.XtraEditors.GroupControl();
            this.cardActivityCount = new DevExpress.XtraEditors.GroupControl();
            this.lblTotalTime = new DevExpress.XtraEditors.LabelControl();
            this.lblWorkedItems = new DevExpress.XtraEditors.LabelControl();
            this.lblCompletedItems = new DevExpress.XtraEditors.LabelControl();
            this.lblActivityCount = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalTimeTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblWorkedItemsTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblCompletedItemsTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblActivityCountTitle = new DevExpress.XtraEditors.LabelControl();

            this.tabControl = new DevExpress.XtraTab.XtraTabControl();
            this.tabTimeDistribution = new DevExpress.XtraTab.XtraTabPage();
            this.tabActivities = new DevExpress.XtraTab.XtraTabPage();
            this.tabCompleted = new DevExpress.XtraTab.XtraTabPage();

            this.gridTimeDistribution = new DevExpress.XtraGrid.GridControl();
            this.gridViewTimeDistribution = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridActivities = new DevExpress.XtraGrid.GridControl();
            this.gridViewActivities = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridCompleted = new DevExpress.XtraGrid.GridControl();
            this.gridViewCompleted = new DevExpress.XtraGrid.Views.Grid.GridView();

            this.panelBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnCopyToClipboard = new DevExpress.XtraEditors.SimpleButton();
            this.btnExportExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();

            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).BeginInit();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelSummary)).BeginInit();
            this.panelSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardTotalTime)).BeginInit();
            this.cardTotalTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardWorkedItems)).BeginInit();
            this.cardWorkedItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardCompletedItems)).BeginInit();
            this.cardCompletedItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardActivityCount)).BeginInit();
            this.cardActivityCount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabTimeDistribution.SuspendLayout();
            this.tabActivities.SuspendLayout();
            this.tabCompleted.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTimeDistribution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTimeDistribution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridActivities)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewActivities)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCompleted)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCompleted)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.lblDateRange);
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Controls.Add(this.btnCustomRange);
            this.panelTop.Controls.Add(this.dtEnd);
            this.panelTop.Controls.Add(this.dtStart);
            this.panelTop.Controls.Add(this.btnThisMonth);
            this.panelTop.Controls.Add(this.btnThisWeek);
            this.panelTop.Controls.Add(this.btnToday);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1000, 60);
            this.panelTop.TabIndex = 0;
            // 
            // btnToday
            // 
            this.btnToday.Appearance.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.btnToday.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnToday.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnToday.Appearance.Options.UseBackColor = true;
            this.btnToday.Appearance.Options.UseFont = true;
            this.btnToday.Appearance.Options.UseForeColor = true;
            this.btnToday.Location = new System.Drawing.Point(15, 15);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(80, 30);
            this.btnToday.TabIndex = 0;
            this.btnToday.Text = "Bugün";
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // btnThisWeek
            // 
            this.btnThisWeek.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnThisWeek.Appearance.Options.UseFont = true;
            this.btnThisWeek.Location = new System.Drawing.Point(100, 15);
            this.btnThisWeek.Name = "btnThisWeek";
            this.btnThisWeek.Size = new System.Drawing.Size(80, 30);
            this.btnThisWeek.TabIndex = 1;
            this.btnThisWeek.Text = "Bu Hafta";
            this.btnThisWeek.Click += new System.EventHandler(this.btnThisWeek_Click);
            // 
            // btnThisMonth
            // 
            this.btnThisMonth.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnThisMonth.Appearance.Options.UseFont = true;
            this.btnThisMonth.Location = new System.Drawing.Point(185, 15);
            this.btnThisMonth.Name = "btnThisMonth";
            this.btnThisMonth.Size = new System.Drawing.Size(80, 30);
            this.btnThisMonth.TabIndex = 2;
            this.btnThisMonth.Text = "Bu Ay";
            this.btnThisMonth.Click += new System.EventHandler(this.btnThisMonth_Click);
            // 
            // dtStart
            // 
            this.dtStart.EditValue = System.DateTime.Today;
            this.dtStart.Location = new System.Drawing.Point(290, 18);
            this.dtStart.Name = "dtStart";
            this.dtStart.Properties.DisplayFormat.FormatString = "dd.MM.yyyy";
            this.dtStart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtStart.Size = new System.Drawing.Size(100, 20);
            this.dtStart.TabIndex = 3;
            // 
            // dtEnd
            // 
            this.dtEnd.EditValue = System.DateTime.Today;
            this.dtEnd.Location = new System.Drawing.Point(400, 18);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Properties.DisplayFormat.FormatString = "dd.MM.yyyy";
            this.dtEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtEnd.Size = new System.Drawing.Size(100, 20);
            this.dtEnd.TabIndex = 4;
            // 
            // btnCustomRange
            // 
            this.btnCustomRange.Location = new System.Drawing.Point(510, 15);
            this.btnCustomRange.Name = "btnCustomRange";
            this.btnCustomRange.Size = new System.Drawing.Size(60, 30);
            this.btnCustomRange.TabIndex = 5;
            this.btnCustomRange.Text = "Uygula";
            this.btnCustomRange.Click += new System.EventHandler(this.btnCustomRange_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(580, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(70, 30);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "🔄 Yenile";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblDateRange
            // 
            this.lblDateRange.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblDateRange.Appearance.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblDateRange.Appearance.Options.UseFont = true;
            this.lblDateRange.Appearance.Options.UseForeColor = true;
            this.lblDateRange.Location = new System.Drawing.Point(700, 20);
            this.lblDateRange.Name = "lblDateRange";
            this.lblDateRange.Size = new System.Drawing.Size(200, 16);
            this.lblDateRange.TabIndex = 7;
            this.lblDateRange.Text = "📅 Tarih Aralığı";
            // 
            // panelSummary
            // 
            this.panelSummary.Controls.Add(this.cardTotalTime);
            this.panelSummary.Controls.Add(this.cardWorkedItems);
            this.panelSummary.Controls.Add(this.cardCompletedItems);
            this.panelSummary.Controls.Add(this.cardActivityCount);
            this.panelSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSummary.Location = new System.Drawing.Point(0, 60);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Size = new System.Drawing.Size(1000, 100);
            this.panelSummary.TabIndex = 1;
            // 
            // cardTotalTime
            // 
            this.cardTotalTime.Appearance.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.cardTotalTime.Appearance.Options.UseBackColor = true;
            this.cardTotalTime.Controls.Add(this.lblTotalTime);
            this.cardTotalTime.Controls.Add(this.lblTotalTimeTitle);
            this.cardTotalTime.Location = new System.Drawing.Point(15, 10);
            this.cardTotalTime.Name = "cardTotalTime";
            this.cardTotalTime.Size = new System.Drawing.Size(180, 80);
            this.cardTotalTime.TabIndex = 0;
            this.cardTotalTime.Text = "⏱️ Toplam Süre";
            // 
            // lblTotalTime
            // 
            this.lblTotalTime.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblTotalTime.Appearance.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.lblTotalTime.Appearance.Options.UseFont = true;
            this.lblTotalTime.Appearance.Options.UseForeColor = true;
            this.lblTotalTime.Location = new System.Drawing.Point(15, 35);
            this.lblTotalTime.Name = "lblTotalTime";
            this.lblTotalTime.Size = new System.Drawing.Size(80, 29);
            this.lblTotalTime.TabIndex = 0;
            this.lblTotalTime.Text = "0s 0dk";
            // 
            // lblTotalTimeTitle
            // 
            this.lblTotalTimeTitle.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblTotalTimeTitle.Appearance.Options.UseForeColor = true;
            this.lblTotalTimeTitle.Location = new System.Drawing.Point(15, 65);
            this.lblTotalTimeTitle.Name = "lblTotalTimeTitle";
            this.lblTotalTimeTitle.Size = new System.Drawing.Size(60, 13);
            this.lblTotalTimeTitle.TabIndex = 1;
            this.lblTotalTimeTitle.Text = "Çalışma Süresi";
            // 
            // cardWorkedItems
            // 
            this.cardWorkedItems.Appearance.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.cardWorkedItems.Appearance.Options.UseBackColor = true;
            this.cardWorkedItems.Controls.Add(this.lblWorkedItems);
            this.cardWorkedItems.Controls.Add(this.lblWorkedItemsTitle);
            this.cardWorkedItems.Location = new System.Drawing.Point(210, 10);
            this.cardWorkedItems.Name = "cardWorkedItems";
            this.cardWorkedItems.Size = new System.Drawing.Size(180, 80);
            this.cardWorkedItems.TabIndex = 1;
            this.cardWorkedItems.Text = "📋 Çalışılan İş";
            // 
            // lblWorkedItems
            // 
            this.lblWorkedItems.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblWorkedItems.Appearance.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.lblWorkedItems.Appearance.Options.UseFont = true;
            this.lblWorkedItems.Appearance.Options.UseForeColor = true;
            this.lblWorkedItems.Location = new System.Drawing.Point(15, 35);
            this.lblWorkedItems.Name = "lblWorkedItems";
            this.lblWorkedItems.Size = new System.Drawing.Size(14, 29);
            this.lblWorkedItems.TabIndex = 0;
            this.lblWorkedItems.Text = "0";
            // 
            // lblWorkedItemsTitle
            // 
            this.lblWorkedItemsTitle.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblWorkedItemsTitle.Appearance.Options.UseForeColor = true;
            this.lblWorkedItemsTitle.Location = new System.Drawing.Point(15, 65);
            this.lblWorkedItemsTitle.Name = "lblWorkedItemsTitle";
            this.lblWorkedItemsTitle.Size = new System.Drawing.Size(50, 13);
            this.lblWorkedItemsTitle.TabIndex = 1;
            this.lblWorkedItemsTitle.Text = "İş Sayısı";
            // 
            // cardCompletedItems
            // 
            this.cardCompletedItems.Appearance.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.cardCompletedItems.Appearance.Options.UseBackColor = true;
            this.cardCompletedItems.Controls.Add(this.lblCompletedItems);
            this.cardCompletedItems.Controls.Add(this.lblCompletedItemsTitle);
            this.cardCompletedItems.Location = new System.Drawing.Point(405, 10);
            this.cardCompletedItems.Name = "cardCompletedItems";
            this.cardCompletedItems.Size = new System.Drawing.Size(180, 80);
            this.cardCompletedItems.TabIndex = 2;
            this.cardCompletedItems.Text = "✅ Tamamlanan";
            // 
            // lblCompletedItems
            // 
            this.lblCompletedItems.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblCompletedItems.Appearance.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.lblCompletedItems.Appearance.Options.UseFont = true;
            this.lblCompletedItems.Appearance.Options.UseForeColor = true;
            this.lblCompletedItems.Location = new System.Drawing.Point(15, 35);
            this.lblCompletedItems.Name = "lblCompletedItems";
            this.lblCompletedItems.Size = new System.Drawing.Size(14, 29);
            this.lblCompletedItems.TabIndex = 0;
            this.lblCompletedItems.Text = "0";
            // 
            // lblCompletedItemsTitle
            // 
            this.lblCompletedItemsTitle.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblCompletedItemsTitle.Appearance.Options.UseForeColor = true;
            this.lblCompletedItemsTitle.Location = new System.Drawing.Point(15, 65);
            this.lblCompletedItemsTitle.Name = "lblCompletedItemsTitle";
            this.lblCompletedItemsTitle.Size = new System.Drawing.Size(70, 13);
            this.lblCompletedItemsTitle.TabIndex = 1;
            this.lblCompletedItemsTitle.Text = "Tamamlanan İş";
            // 
            // cardActivityCount
            // 
            this.cardActivityCount.Appearance.BackColor = System.Drawing.Color.FromArgb(230, 126, 34);
            this.cardActivityCount.Appearance.Options.UseBackColor = true;
            this.cardActivityCount.Controls.Add(this.lblActivityCount);
            this.cardActivityCount.Controls.Add(this.lblActivityCountTitle);
            this.cardActivityCount.Location = new System.Drawing.Point(600, 10);
            this.cardActivityCount.Name = "cardActivityCount";
            this.cardActivityCount.Size = new System.Drawing.Size(180, 80);
            this.cardActivityCount.TabIndex = 3;
            this.cardActivityCount.Text = "💬 Aktiviteler";
            // 
            // lblActivityCount
            // 
            this.lblActivityCount.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblActivityCount.Appearance.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.lblActivityCount.Appearance.Options.UseFont = true;
            this.lblActivityCount.Appearance.Options.UseForeColor = true;
            this.lblActivityCount.Location = new System.Drawing.Point(15, 35);
            this.lblActivityCount.Name = "lblActivityCount";
            this.lblActivityCount.Size = new System.Drawing.Size(14, 29);
            this.lblActivityCount.TabIndex = 0;
            this.lblActivityCount.Text = "0";
            // 
            // lblActivityCountTitle
            // 
            this.lblActivityCountTitle.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblActivityCountTitle.Appearance.Options.UseForeColor = true;
            this.lblActivityCountTitle.Location = new System.Drawing.Point(15, 65);
            this.lblActivityCountTitle.Name = "lblActivityCountTitle";
            this.lblActivityCountTitle.Size = new System.Drawing.Size(70, 13);
            this.lblActivityCountTitle.TabIndex = 1;
            this.lblActivityCountTitle.Text = "Yorum/Durum";
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 160);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabPage = this.tabTimeDistribution;
            this.tabControl.Size = new System.Drawing.Size(1000, 340);
            this.tabControl.TabIndex = 2;
            this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabTimeDistribution,
            this.tabActivities,
            this.tabCompleted});
            // 
            // tabTimeDistribution
            // 
            this.tabTimeDistribution.Controls.Add(this.gridTimeDistribution);
            this.tabTimeDistribution.Name = "tabTimeDistribution";
            this.tabTimeDistribution.Size = new System.Drawing.Size(998, 315);
            this.tabTimeDistribution.Text = "⏱️ Zaman Dağılımı";
            // 
            // gridTimeDistribution
            // 
            this.gridTimeDistribution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTimeDistribution.Location = new System.Drawing.Point(0, 0);
            this.gridTimeDistribution.MainView = this.gridViewTimeDistribution;
            this.gridTimeDistribution.Name = "gridTimeDistribution";
            this.gridTimeDistribution.Size = new System.Drawing.Size(998, 315);
            this.gridTimeDistribution.TabIndex = 0;
            this.gridTimeDistribution.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewTimeDistribution});
            // 
            // gridViewTimeDistribution
            // 
            this.gridViewTimeDistribution.GridControl = this.gridTimeDistribution;
            this.gridViewTimeDistribution.Name = "gridViewTimeDistribution";
            this.gridViewTimeDistribution.OptionsBehavior.Editable = false;
            this.gridViewTimeDistribution.OptionsView.ShowGroupPanel = false;
            // 
            // tabActivities
            // 
            this.tabActivities.Controls.Add(this.gridActivities);
            this.tabActivities.Name = "tabActivities";
            this.tabActivities.Size = new System.Drawing.Size(998, 315);
            this.tabActivities.Text = "📋 Aktiviteler";
            // 
            // gridActivities
            // 
            this.gridActivities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridActivities.Location = new System.Drawing.Point(0, 0);
            this.gridActivities.MainView = this.gridViewActivities;
            this.gridActivities.Name = "gridActivities";
            this.gridActivities.Size = new System.Drawing.Size(998, 315);
            this.gridActivities.TabIndex = 0;
            this.gridActivities.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewActivities});
            // 
            // gridViewActivities
            // 
            this.gridViewActivities.GridControl = this.gridActivities;
            this.gridViewActivities.Name = "gridViewActivities";
            this.gridViewActivities.OptionsBehavior.Editable = false;
            this.gridViewActivities.OptionsView.ShowGroupPanel = false;
            // 
            // tabCompleted
            // 
            this.tabCompleted.Controls.Add(this.gridCompleted);
            this.tabCompleted.Name = "tabCompleted";
            this.tabCompleted.Size = new System.Drawing.Size(998, 315);
            this.tabCompleted.Text = "✅ Tamamlanan İşler";
            // 
            // gridCompleted
            // 
            this.gridCompleted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCompleted.Location = new System.Drawing.Point(0, 0);
            this.gridCompleted.MainView = this.gridViewCompleted;
            this.gridCompleted.Name = "gridCompleted";
            this.gridCompleted.Size = new System.Drawing.Size(998, 315);
            this.gridCompleted.TabIndex = 0;
            this.gridCompleted.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewCompleted});
            // 
            // gridViewCompleted
            // 
            this.gridViewCompleted.GridControl = this.gridCompleted;
            this.gridViewCompleted.Name = "gridViewCompleted";
            this.gridViewCompleted.OptionsBehavior.Editable = false;
            this.gridViewCompleted.OptionsView.ShowGroupPanel = false;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnCopyToClipboard);
            this.panelBottom.Controls.Add(this.btnExportExcel);
            this.panelBottom.Controls.Add(this.btnClose);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 500);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1000, 50);
            this.panelBottom.TabIndex = 3;
            // 
            // btnCopyToClipboard
            // 
            this.btnCopyToClipboard.Appearance.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.btnCopyToClipboard.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnCopyToClipboard.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnCopyToClipboard.Appearance.Options.UseBackColor = true;
            this.btnCopyToClipboard.Appearance.Options.UseFont = true;
            this.btnCopyToClipboard.Appearance.Options.UseForeColor = true;
            this.btnCopyToClipboard.Location = new System.Drawing.Point(15, 10);
            this.btnCopyToClipboard.Name = "btnCopyToClipboard";
            this.btnCopyToClipboard.Size = new System.Drawing.Size(150, 30);
            this.btnCopyToClipboard.TabIndex = 0;
            this.btnCopyToClipboard.Text = "📋 Panoya Kopyala";
            this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Appearance.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnExportExcel.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnExportExcel.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnExportExcel.Appearance.Options.UseBackColor = true;
            this.btnExportExcel.Appearance.Options.UseFont = true;
            this.btnExportExcel.Appearance.Options.UseForeColor = true;
            this.btnExportExcel.Location = new System.Drawing.Point(175, 10);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(130, 30);
            this.btnExportExcel.TabIndex = 1;
            this.btnExportExcel.Text = "📄 Excel'e Aktar";
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(890, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Kapat";
            // 
            // WorkSummaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1000, 550);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panelSummary);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelBottom);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "WorkSummaryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "📊 Çalışma Özeti - Bugün Ne Yaptım?";
            this.Load += new System.EventHandler(this.WorkSummaryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelSummary)).EndInit();
            this.panelSummary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cardTotalTime)).EndInit();
            this.cardTotalTime.ResumeLayout(false);
            this.cardTotalTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardWorkedItems)).EndInit();
            this.cardWorkedItems.ResumeLayout(false);
            this.cardWorkedItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardCompletedItems)).EndInit();
            this.cardCompletedItems.ResumeLayout(false);
            this.cardCompletedItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardActivityCount)).EndInit();
            this.cardActivityCount.ResumeLayout(false);
            this.cardActivityCount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabTimeDistribution.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTimeDistribution)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTimeDistribution)).EndInit();
            this.tabActivities.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridActivities)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewActivities)).EndInit();
            this.tabCompleted.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCompleted)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCompleted)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
