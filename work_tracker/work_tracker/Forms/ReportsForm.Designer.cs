using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using DevExpress.XtraRichEdit;
using System.Windows.Forms.DataVisualization.Charting;

namespace work_tracker.Forms
{
    partial class ReportsForm
    {
        private System.ComponentModel.IContainer components = null;
        private XtraTabControl tabControl1;
        private XtraTabPage tabChart;
        private XtraTabPage tabDetailed;
        private XtraTabPage tabDailyEffort;
        private XtraTabPage tabActivityTimeline;
        private Chart chartEffort;
        private SimpleButton btnRefresh;
        private PanelControl panelControl1;
        private DateEdit dtFrom;
        private DateEdit dtTo;
        private LabelControl lblFrom;
        private LabelControl lblTo;
        private DevExpress.XtraGrid.GridControl gridDetailed;
        private DevExpress.XtraGrid.Views.Grid.GridView viewDetailed;
        private PanelControl panelSummaryCards;
        private LabelControl lblTotalHoursTitle;
        private LabelControl lblTotalHoursValue;
        private LabelControl lblAvgDailyTitle;
        private LabelControl lblAvgDailyValue;
        private LabelControl lblActiveDaysTitle;
        private LabelControl lblActiveDaysValue;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private DevExpress.XtraGrid.GridControl gridDailySummary;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDailySummary;
        private DevExpress.XtraGrid.GridControl gridWorkItemDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewWorkItemDetails;
        // Timeline controls
        private PanelControl panelTimelineHeader;
        private DateEdit dtTimelineDate;
        private LabelControl lblTimelineDate;
        private SimpleButton btnTimelinePrev;
        private SimpleButton btnTimelineNext;
        private LabelControl lblTimelineSummary;
        private System.Windows.Forms.Panel panelTimelineContainer;
        private System.Windows.Forms.FlowLayoutPanel flowTimelinePanel;
        // Fragmentation report controls
        private XtraTabPage tabFragmentation;
        private DevExpress.XtraGrid.GridControl gridFragmentation;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewFragmentation;
        private PanelControl panelFragmentationSummary;
        private LabelControl lblFragAvgCountTitle;
        private LabelControl lblFragAvgCountValue;
        private LabelControl lblFragTotalWorkItemsTitle;
        private LabelControl lblFragTotalWorkItemsValue;
        private LabelControl lblFragTopItemTitle;
        private LabelControl lblFragTopItemValue;

        private void InitializeComponent()
        {
            this.tabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabChart = new DevExpress.XtraTab.XtraTabPage();
            this.chartEffort = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabDetailed = new DevExpress.XtraTab.XtraTabPage();
            this.gridDetailed = new DevExpress.XtraGrid.GridControl();
            this.viewDetailed = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabDailyEffort = new DevExpress.XtraTab.XtraTabPage();
            this.tabActivityTimeline = new DevExpress.XtraTab.XtraTabPage();
            this.panelSummaryCards = new DevExpress.XtraEditors.PanelControl();
            this.lblTotalHoursTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalHoursValue = new DevExpress.XtraEditors.LabelControl();
            this.lblAvgDailyTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblAvgDailyValue = new DevExpress.XtraEditors.LabelControl();
            this.lblActiveDaysTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblActiveDaysValue = new DevExpress.XtraEditors.LabelControl();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.gridDailySummary = new DevExpress.XtraGrid.GridControl();
            this.gridViewDailySummary = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridWorkItemDetails = new DevExpress.XtraGrid.GridControl();
            this.gridViewWorkItemDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblFrom = new DevExpress.XtraEditors.LabelControl();
            this.dtFrom = new DevExpress.XtraEditors.DateEdit();
            this.lblTo = new DevExpress.XtraEditors.LabelControl();
            this.dtTo = new DevExpress.XtraEditors.DateEdit();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            // Timeline controls
            this.panelTimelineHeader = new DevExpress.XtraEditors.PanelControl();
            this.dtTimelineDate = new DevExpress.XtraEditors.DateEdit();
            this.lblTimelineDate = new DevExpress.XtraEditors.LabelControl();
            this.btnTimelinePrev = new DevExpress.XtraEditors.SimpleButton();
            this.btnTimelineNext = new DevExpress.XtraEditors.SimpleButton();
            this.lblTimelineSummary = new DevExpress.XtraEditors.LabelControl();
            this.panelTimelineContainer = new System.Windows.Forms.Panel();
            this.flowTimelinePanel = new System.Windows.Forms.FlowLayoutPanel();
            // Fragmentation controls
            this.tabFragmentation = new DevExpress.XtraTab.XtraTabPage();
            this.gridFragmentation = new DevExpress.XtraGrid.GridControl();
            this.gridViewFragmentation = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelFragmentationSummary = new DevExpress.XtraEditors.PanelControl();
            this.lblFragAvgCountTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblFragAvgCountValue = new DevExpress.XtraEditors.LabelControl();
            this.lblFragTotalWorkItemsTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblFragTotalWorkItemsValue = new DevExpress.XtraEditors.LabelControl();
            this.lblFragTopItemTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblFragTopItemValue = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartEffort)).BeginInit();
            this.tabDetailed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDetailed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewDetailed)).BeginInit();
            this.tabDailyEffort.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelSummaryCards)).BeginInit();
            this.panelSummaryCards.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDailySummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDailySummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridWorkItemDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewWorkItemDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties)).BeginInit();
            // Timeline BeginInit
            this.tabActivityTimeline.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelTimelineHeader)).BeginInit();
            this.panelTimelineHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtTimelineDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTimelineDate.Properties)).BeginInit();
            this.panelTimelineContainer.SuspendLayout();
            // Fragmentation BeginInit
            this.tabFragmentation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridFragmentation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFragmentation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelFragmentationSummary)).BeginInit();
            this.panelFragmentationSummary.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnRefresh);
            this.panelControl1.Controls.Add(this.dtTo);
            this.panelControl1.Controls.Add(this.lblTo);
            this.panelControl1.Controls.Add(this.dtFrom);
            this.panelControl1.Controls.Add(this.lblFrom);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1200, 50);
            this.panelControl1.TabIndex = 0;
            // 
            // lblFrom
            // 
            this.lblFrom.Location = new System.Drawing.Point(15, 18);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(36, 13);
            this.lblFrom.TabIndex = 0;
            this.lblFrom.Text = "Başlangıç";
            // 
            // dtFrom
            // 
            this.dtFrom.EditValue = null;
            this.dtFrom.Location = new System.Drawing.Point(60, 15);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFrom.Properties.DisplayFormat.FormatString = "dd.MM.yyyy";
            this.dtFrom.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtFrom.Properties.EditFormat.FormatString = "dd.MM.yyyy";
            this.dtFrom.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtFrom.Size = new System.Drawing.Size(110, 20);
            this.dtFrom.TabIndex = 1;
            // 
            // lblTo
            // 
            this.lblTo.Location = new System.Drawing.Point(185, 18);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(18, 13);
            this.lblTo.TabIndex = 2;
            this.lblTo.Text = "Bitiş";
            // 
            // dtTo
            // 
            this.dtTo.EditValue = null;
            this.dtTo.Location = new System.Drawing.Point(210, 15);
            this.dtTo.Name = "dtTo";
            this.dtTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTo.Properties.DisplayFormat.FormatString = "dd.MM.yyyy";
            this.dtTo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtTo.Properties.EditFormat.FormatString = "dd.MM.yyyy";
            this.dtTo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtTo.Size = new System.Drawing.Size(110, 20);
            this.dtTo.TabIndex = 3;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(340, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Yenile";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 50);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabPage = this.tabChart;
            this.tabControl1.Size = new System.Drawing.Size(1200, 650);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabChart,
            this.tabDetailed,
            this.tabDailyEffort,
            this.tabActivityTimeline,
            this.tabFragmentation});
            // 
            // tabChart
            // 
            this.tabChart.Controls.Add(this.chartEffort);
            this.tabChart.Name = "tabChart";
            this.tabChart.Size = new System.Drawing.Size(1194, 622);
            this.tabChart.Text = "Planlanan vs Gerçekleşen";
            // 
            // chartEffort
            // 
            this.chartEffort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartEffort.Location = new System.Drawing.Point(0, 0);
            this.chartEffort.Name = "chartEffort";
            this.chartEffort.Size = new System.Drawing.Size(1194, 622);
            this.chartEffort.TabIndex = 0;
            // 
            // tabDetailed
            // 
            this.tabDetailed.Controls.Add(this.gridDetailed);
            this.tabDetailed.Name = "tabDetailed";
            this.tabDetailed.Size = new System.Drawing.Size(1194, 622);
            this.tabDetailed.Text = "Detaylı İş Raporu";
            // 
            // gridDetailed
            // 
            this.gridDetailed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDetailed.Location = new System.Drawing.Point(0, 0);
            this.gridDetailed.MainView = this.viewDetailed;
            this.gridDetailed.Name = "gridDetailed";
            this.gridDetailed.Size = new System.Drawing.Size(1194, 622);
            this.gridDetailed.TabIndex = 0;
            this.gridDetailed.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewDetailed});
            // 
            // viewDetailed
            // 
            this.viewDetailed.GridControl = this.gridDetailed;
            this.viewDetailed.Name = "viewDetailed";
            this.viewDetailed.OptionsBehavior.Editable = false;
            this.viewDetailed.OptionsView.ShowGroupPanel = false;
            this.viewDetailed.OptionsView.ShowFooter = true;
            // 
            // tabDailyEffort
            // 
            this.tabDailyEffort.Controls.Add(this.splitContainerMain);
            this.tabDailyEffort.Controls.Add(this.panelSummaryCards);
            this.tabDailyEffort.Name = "tabDailyEffort";
            this.tabDailyEffort.Size = new System.Drawing.Size(1194, 622);
            this.tabDailyEffort.Text = "Günlük Efor Raporu";
            // 
            // panelSummaryCards
            // 
            this.panelSummaryCards.Controls.Add(this.lblTotalHoursTitle);
            this.panelSummaryCards.Controls.Add(this.lblTotalHoursValue);
            this.panelSummaryCards.Controls.Add(this.lblAvgDailyTitle);
            this.panelSummaryCards.Controls.Add(this.lblAvgDailyValue);
            this.panelSummaryCards.Controls.Add(this.lblActiveDaysTitle);
            this.panelSummaryCards.Controls.Add(this.lblActiveDaysValue);
            this.panelSummaryCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSummaryCards.Location = new System.Drawing.Point(0, 0);
            this.panelSummaryCards.Name = "panelSummaryCards";
            this.panelSummaryCards.Size = new System.Drawing.Size(1194, 80);
            this.panelSummaryCards.TabIndex = 0;
            // 
            // lblTotalHoursTitle
            // 
            this.lblTotalHoursTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblTotalHoursTitle.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblTotalHoursTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTotalHoursTitle.Name = "lblTotalHoursTitle";
            this.lblTotalHoursTitle.Size = new System.Drawing.Size(80, 14);
            this.lblTotalHoursTitle.TabIndex = 0;
            this.lblTotalHoursTitle.Text = "Toplam Süre";
            // 
            // lblTotalHoursValue
            // 
            this.lblTotalHoursValue.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblTotalHoursValue.Appearance.ForeColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.lblTotalHoursValue.Location = new System.Drawing.Point(20, 35);
            this.lblTotalHoursValue.Name = "lblTotalHoursValue";
            this.lblTotalHoursValue.Size = new System.Drawing.Size(60, 29);
            this.lblTotalHoursValue.TabIndex = 1;
            this.lblTotalHoursValue.Text = "0 sa";
            // 
            // lblAvgDailyTitle
            // 
            this.lblAvgDailyTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblAvgDailyTitle.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblAvgDailyTitle.Location = new System.Drawing.Point(250, 15);
            this.lblAvgDailyTitle.Name = "lblAvgDailyTitle";
            this.lblAvgDailyTitle.Size = new System.Drawing.Size(110, 14);
            this.lblAvgDailyTitle.TabIndex = 2;
            this.lblAvgDailyTitle.Text = "Günlük Ortalama";
            // 
            // lblAvgDailyValue
            // 
            this.lblAvgDailyValue.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblAvgDailyValue.Appearance.ForeColor = System.Drawing.Color.FromArgb(0, 153, 79);
            this.lblAvgDailyValue.Location = new System.Drawing.Point(250, 35);
            this.lblAvgDailyValue.Name = "lblAvgDailyValue";
            this.lblAvgDailyValue.Size = new System.Drawing.Size(60, 29);
            this.lblAvgDailyValue.TabIndex = 3;
            this.lblAvgDailyValue.Text = "0 sa";
            // 
            // lblActiveDaysTitle
            // 
            this.lblActiveDaysTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblActiveDaysTitle.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblActiveDaysTitle.Location = new System.Drawing.Point(480, 15);
            this.lblActiveDaysTitle.Name = "lblActiveDaysTitle";
            this.lblActiveDaysTitle.Size = new System.Drawing.Size(80, 14);
            this.lblActiveDaysTitle.TabIndex = 4;
            this.lblActiveDaysTitle.Text = "Aktif Gün";
            // 
            // lblActiveDaysValue
            // 
            this.lblActiveDaysValue.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblActiveDaysValue.Appearance.ForeColor = System.Drawing.Color.FromArgb(255, 142, 0);
            this.lblActiveDaysValue.Location = new System.Drawing.Point(480, 35);
            this.lblActiveDaysValue.Name = "lblActiveDaysValue";
            this.lblActiveDaysValue.Size = new System.Drawing.Size(30, 29);
            this.lblActiveDaysValue.TabIndex = 5;
            this.lblActiveDaysValue.Text = "0";
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 80);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Size = new System.Drawing.Size(1194, 542);
            this.splitContainerMain.SplitterDistance = 400;
            this.splitContainerMain.TabIndex = 1;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.gridDailySummary);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.gridWorkItemDetails);
            // 
            // gridDailySummary
            // 
            this.gridDailySummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDailySummary.Location = new System.Drawing.Point(0, 0);
            this.gridDailySummary.MainView = this.gridViewDailySummary;
            this.gridDailySummary.Name = "gridDailySummary";
            this.gridDailySummary.Size = new System.Drawing.Size(400, 542);
            this.gridDailySummary.TabIndex = 0;
            this.gridDailySummary.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDailySummary});
            // 
            // gridViewDailySummary
            // 
            this.gridViewDailySummary.GridControl = this.gridDailySummary;
            this.gridViewDailySummary.Name = "gridViewDailySummary";
            this.gridViewDailySummary.OptionsBehavior.Editable = false;
            this.gridViewDailySummary.OptionsView.ShowGroupPanel = false;
            this.gridViewDailySummary.OptionsView.ShowFooter = true;
            this.gridViewDailySummary.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewDailySummary_FocusedRowChanged);
            // 
            // gridWorkItemDetails
            // 
            this.gridWorkItemDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridWorkItemDetails.Location = new System.Drawing.Point(0, 0);
            this.gridWorkItemDetails.MainView = this.gridViewWorkItemDetails;
            this.gridWorkItemDetails.Name = "gridWorkItemDetails";
            this.gridWorkItemDetails.Size = new System.Drawing.Size(790, 542);
            this.gridWorkItemDetails.TabIndex = 0;
            this.gridWorkItemDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewWorkItemDetails});
            // 
            // gridViewWorkItemDetails
            // 
            this.gridViewWorkItemDetails.GridControl = this.gridWorkItemDetails;
            this.gridViewWorkItemDetails.Name = "gridViewWorkItemDetails";
            this.gridViewWorkItemDetails.OptionsBehavior.Editable = false;
            this.gridViewWorkItemDetails.OptionsView.ShowGroupPanel = false;
            this.gridViewWorkItemDetails.OptionsView.ShowFooter = true;
            // 
            // tabActivityTimeline
            // 
            this.tabActivityTimeline.Controls.Add(this.panelTimelineContainer);
            this.tabActivityTimeline.Controls.Add(this.panelTimelineHeader);
            this.tabActivityTimeline.Name = "tabActivityTimeline";
            this.tabActivityTimeline.Size = new System.Drawing.Size(1194, 622);
            this.tabActivityTimeline.Text = "📅 Günlük Aktivite Akışı";
            // 
            // panelTimelineHeader
            // 
            this.panelTimelineHeader.Controls.Add(this.btnTimelinePrev);
            this.panelTimelineHeader.Controls.Add(this.lblTimelineDate);
            this.panelTimelineHeader.Controls.Add(this.dtTimelineDate);
            this.panelTimelineHeader.Controls.Add(this.btnTimelineNext);
            this.panelTimelineHeader.Controls.Add(this.lblTimelineSummary);
            this.panelTimelineHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTimelineHeader.Location = new System.Drawing.Point(0, 0);
            this.panelTimelineHeader.Name = "panelTimelineHeader";
            this.panelTimelineHeader.Size = new System.Drawing.Size(1194, 60);
            this.panelTimelineHeader.TabIndex = 0;
            // 
            // btnTimelinePrev
            // 
            this.btnTimelinePrev.Location = new System.Drawing.Point(15, 15);
            this.btnTimelinePrev.Name = "btnTimelinePrev";
            this.btnTimelinePrev.Size = new System.Drawing.Size(40, 30);
            this.btnTimelinePrev.TabIndex = 0;
            this.btnTimelinePrev.Text = "◀";
            this.btnTimelinePrev.Click += new System.EventHandler(this.btnTimelinePrev_Click);
            // 
            // lblTimelineDate
            // 
            this.lblTimelineDate.Location = new System.Drawing.Point(65, 22);
            this.lblTimelineDate.Name = "lblTimelineDate";
            this.lblTimelineDate.Size = new System.Drawing.Size(30, 13);
            this.lblTimelineDate.TabIndex = 1;
            this.lblTimelineDate.Text = "Tarih:";
            // 
            // dtTimelineDate
            // 
            this.dtTimelineDate.EditValue = null;
            this.dtTimelineDate.Location = new System.Drawing.Point(100, 18);
            this.dtTimelineDate.Name = "dtTimelineDate";
            this.dtTimelineDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTimelineDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTimelineDate.Properties.DisplayFormat.FormatString = "dd MMMM yyyy dddd";
            this.dtTimelineDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtTimelineDate.Properties.EditFormat.FormatString = "dd.MM.yyyy";
            this.dtTimelineDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtTimelineDate.Size = new System.Drawing.Size(200, 20);
            this.dtTimelineDate.TabIndex = 2;
            this.dtTimelineDate.EditValueChanged += new System.EventHandler(this.dtTimelineDate_EditValueChanged);
            // 
            // btnTimelineNext
            // 
            this.btnTimelineNext.Location = new System.Drawing.Point(310, 15);
            this.btnTimelineNext.Name = "btnTimelineNext";
            this.btnTimelineNext.Size = new System.Drawing.Size(40, 30);
            this.btnTimelineNext.TabIndex = 3;
            this.btnTimelineNext.Text = "▶";
            this.btnTimelineNext.Click += new System.EventHandler(this.btnTimelineNext_Click);
            // 
            // lblTimelineSummary
            // 
            this.lblTimelineSummary.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblTimelineSummary.Appearance.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblTimelineSummary.Location = new System.Drawing.Point(380, 20);
            this.lblTimelineSummary.Name = "lblTimelineSummary";
            this.lblTimelineSummary.Size = new System.Drawing.Size(200, 16);
            this.lblTimelineSummary.TabIndex = 4;
            this.lblTimelineSummary.Text = "";
            // 
            // panelTimelineContainer
            // 
            this.panelTimelineContainer.AutoScroll = true;
            this.panelTimelineContainer.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.panelTimelineContainer.Controls.Add(this.flowTimelinePanel);
            this.panelTimelineContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTimelineContainer.Location = new System.Drawing.Point(0, 60);
            this.panelTimelineContainer.Name = "panelTimelineContainer";
            this.panelTimelineContainer.Size = new System.Drawing.Size(1194, 562);
            this.panelTimelineContainer.TabIndex = 1;
            // 
            // flowTimelinePanel
            // 
            this.flowTimelinePanel.AutoSize = true;
            this.flowTimelinePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowTimelinePanel.BackColor = System.Drawing.Color.Transparent;
            this.flowTimelinePanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowTimelinePanel.Location = new System.Drawing.Point(20, 10);
            this.flowTimelinePanel.MinimumSize = new System.Drawing.Size(1150, 100);
            this.flowTimelinePanel.Name = "flowTimelinePanel";
            this.flowTimelinePanel.Size = new System.Drawing.Size(1150, 100);
            this.flowTimelinePanel.TabIndex = 0;
            this.flowTimelinePanel.WrapContents = false;
            // 
            // tabFragmentation
            // 
            this.tabFragmentation.Controls.Add(this.gridFragmentation);
            this.tabFragmentation.Controls.Add(this.panelFragmentationSummary);
            this.tabFragmentation.Name = "tabFragmentation";
            this.tabFragmentation.Size = new System.Drawing.Size(1194, 622);
            this.tabFragmentation.Text = "📈 Bölünme Raporu";
            // 
            // panelFragmentationSummary
            // 
            this.panelFragmentationSummary.Controls.Add(this.lblFragAvgCountTitle);
            this.panelFragmentationSummary.Controls.Add(this.lblFragAvgCountValue);
            this.panelFragmentationSummary.Controls.Add(this.lblFragTotalWorkItemsTitle);
            this.panelFragmentationSummary.Controls.Add(this.lblFragTotalWorkItemsValue);
            this.panelFragmentationSummary.Controls.Add(this.lblFragTopItemTitle);
            this.panelFragmentationSummary.Controls.Add(this.lblFragTopItemValue);
            this.panelFragmentationSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFragmentationSummary.Location = new System.Drawing.Point(0, 0);
            this.panelFragmentationSummary.Name = "panelFragmentationSummary";
            this.panelFragmentationSummary.Size = new System.Drawing.Size(1194, 80);
            this.panelFragmentationSummary.TabIndex = 0;
            // 
            // lblFragTotalWorkItemsTitle
            // 
            this.lblFragTotalWorkItemsTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblFragTotalWorkItemsTitle.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblFragTotalWorkItemsTitle.Location = new System.Drawing.Point(20, 15);
            this.lblFragTotalWorkItemsTitle.Name = "lblFragTotalWorkItemsTitle";
            this.lblFragTotalWorkItemsTitle.Size = new System.Drawing.Size(60, 14);
            this.lblFragTotalWorkItemsTitle.TabIndex = 0;
            this.lblFragTotalWorkItemsTitle.Text = "Toplam İş";
            // 
            // lblFragTotalWorkItemsValue
            // 
            this.lblFragTotalWorkItemsValue.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblFragTotalWorkItemsValue.Appearance.ForeColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.lblFragTotalWorkItemsValue.Location = new System.Drawing.Point(20, 35);
            this.lblFragTotalWorkItemsValue.Name = "lblFragTotalWorkItemsValue";
            this.lblFragTotalWorkItemsValue.Size = new System.Drawing.Size(15, 29);
            this.lblFragTotalWorkItemsValue.TabIndex = 1;
            this.lblFragTotalWorkItemsValue.Text = "0";
            // 
            // lblFragAvgCountTitle
            // 
            this.lblFragAvgCountTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblFragAvgCountTitle.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblFragAvgCountTitle.Location = new System.Drawing.Point(250, 15);
            this.lblFragAvgCountTitle.Name = "lblFragAvgCountTitle";
            this.lblFragAvgCountTitle.Size = new System.Drawing.Size(100, 14);
            this.lblFragAvgCountTitle.TabIndex = 2;
            this.lblFragAvgCountTitle.Text = "Ort. Bölünme";
            // 
            // lblFragAvgCountValue
            // 
            this.lblFragAvgCountValue.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblFragAvgCountValue.Appearance.ForeColor = System.Drawing.Color.FromArgb(230, 126, 34);
            this.lblFragAvgCountValue.Location = new System.Drawing.Point(250, 35);
            this.lblFragAvgCountValue.Name = "lblFragAvgCountValue";
            this.lblFragAvgCountValue.Size = new System.Drawing.Size(30, 29);
            this.lblFragAvgCountValue.TabIndex = 3;
            this.lblFragAvgCountValue.Text = "0";
            // 
            // lblFragTopItemTitle
            // 
            this.lblFragTopItemTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblFragTopItemTitle.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblFragTopItemTitle.Location = new System.Drawing.Point(480, 15);
            this.lblFragTopItemTitle.Name = "lblFragTopItemTitle";
            this.lblFragTopItemTitle.Size = new System.Drawing.Size(100, 14);
            this.lblFragTopItemTitle.TabIndex = 4;
            this.lblFragTopItemTitle.Text = "En Çok Bölünen";
            // 
            // lblFragTopItemValue
            // 
            this.lblFragTopItemValue.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblFragTopItemValue.Appearance.ForeColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.lblFragTopItemValue.Location = new System.Drawing.Point(480, 35);
            this.lblFragTopItemValue.Name = "lblFragTopItemValue";
            this.lblFragTopItemValue.Size = new System.Drawing.Size(8, 16);
            this.lblFragTopItemValue.TabIndex = 5;
            this.lblFragTopItemValue.Text = "-";
            // 
            // gridFragmentation
            // 
            this.gridFragmentation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridFragmentation.Location = new System.Drawing.Point(0, 80);
            this.gridFragmentation.MainView = this.gridViewFragmentation;
            this.gridFragmentation.Name = "gridFragmentation";
            this.gridFragmentation.Size = new System.Drawing.Size(1194, 542);
            this.gridFragmentation.TabIndex = 1;
            this.gridFragmentation.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewFragmentation});
            // 
            // gridViewFragmentation
            // 
            this.gridViewFragmentation.GridControl = this.gridFragmentation;
            this.gridViewFragmentation.Name = "gridViewFragmentation";
            this.gridViewFragmentation.OptionsBehavior.Editable = false;
            this.gridViewFragmentation.OptionsView.ShowGroupPanel = false;
            this.gridViewFragmentation.OptionsView.ShowFooter = true;
            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "ReportsForm";
            this.Text = "Raporlar ve Analitik";
            this.Load += new System.EventHandler(this.ReportsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartEffort)).EndInit();
            this.tabDetailed.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDetailed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewDetailed)).EndInit();
            this.tabDailyEffort.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelSummaryCards)).EndInit();
            this.panelSummaryCards.ResumeLayout(false);
            this.panelSummaryCards.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDailySummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDailySummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridWorkItemDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewWorkItemDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties)).EndInit();
            // Timeline EndInit
            this.tabActivityTimeline.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelTimelineHeader)).EndInit();
            this.panelTimelineHeader.ResumeLayout(false);
            this.panelTimelineHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtTimelineDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTimelineDate.Properties)).EndInit();
            this.panelTimelineContainer.ResumeLayout(false);
            this.panelTimelineContainer.PerformLayout();
            // Fragmentation EndInit
            this.tabFragmentation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridFragmentation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFragmentation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelFragmentationSummary)).EndInit();
            this.panelFragmentationSummary.ResumeLayout(false);
            this.panelFragmentationSummary.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
