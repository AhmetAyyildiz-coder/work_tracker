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

        private void InitializeComponent()
        {
            this.tabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabChart = new DevExpress.XtraTab.XtraTabPage();
            this.chartEffort = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabDetailed = new DevExpress.XtraTab.XtraTabPage();
            this.gridDetailed = new DevExpress.XtraGrid.GridControl();
            this.viewDetailed = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabDailyEffort = new DevExpress.XtraTab.XtraTabPage();
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
            this.tabDailyEffort});
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
            this.ResumeLayout(false);
        }
    }
}
