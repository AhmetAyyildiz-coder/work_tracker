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
        private DevExpress.XtraGrid.GridControl gridDailyEffort;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDailyEffort;
        private DevExpress.XtraGrid.GridControl gridWorkItemEffort;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewWorkItemEffort;
        private LabelControl lblDailyEffortTitle;
        private LabelControl lblWorkItemEffortTitle;

        private void InitializeComponent()
        {
            this.tabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabChart = new DevExpress.XtraTab.XtraTabPage();
            this.chartEffort = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabDetailed = new DevExpress.XtraTab.XtraTabPage();
            this.gridDetailed = new DevExpress.XtraGrid.GridControl();
            this.viewDetailed = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabDailyEffort = new DevExpress.XtraTab.XtraTabPage();
            this.gridDailyEffort = new DevExpress.XtraGrid.GridControl();
            this.gridViewDailyEffort = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridWorkItemEffort = new DevExpress.XtraGrid.GridControl();
            this.gridViewWorkItemEffort = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblDailyEffortTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblWorkItemEffortTitle = new DevExpress.XtraEditors.LabelControl();
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
            ((System.ComponentModel.ISupportInitialize)(this.gridDailyEffort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDailyEffort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridWorkItemEffort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewWorkItemEffort)).BeginInit();
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
            // tabDailyEffort
            //
            this.tabDailyEffort.Controls.Add(this.lblWorkItemEffortTitle);
            this.tabDailyEffort.Controls.Add(this.gridWorkItemEffort);
            this.tabDailyEffort.Controls.Add(this.lblDailyEffortTitle);
            this.tabDailyEffort.Controls.Add(this.gridDailyEffort);
            this.tabDailyEffort.Name = "tabDailyEffort";
            this.tabDailyEffort.Size = new System.Drawing.Size(1194, 622);
            this.tabDailyEffort.Text = "Günlük Efor Raporu";
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
            // gridDailyEffort
            //
            this.gridDailyEffort.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridDailyEffort.Location = new System.Drawing.Point(0, 30);
            this.gridDailyEffort.MainView = this.gridViewDailyEffort;
            this.gridDailyEffort.Name = "gridDailyEffort";
            this.gridDailyEffort.Size = new System.Drawing.Size(1194, 300);
            this.gridDailyEffort.TabIndex = 0;
            this.gridDailyEffort.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDailyEffort});
            //
            // gridViewDailyEffort
            //
            this.gridViewDailyEffort.GridControl = this.gridDailyEffort;
            this.gridViewDailyEffort.Name = "gridViewDailyEffort";
            this.gridViewDailyEffort.OptionsBehavior.Editable = false;
            this.gridViewDailyEffort.OptionsView.ShowGroupPanel = true;
            this.gridViewDailyEffort.OptionsView.ShowFooter = true;
            //
            // gridWorkItemEffort
            //
            this.gridWorkItemEffort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridWorkItemEffort.Location = new System.Drawing.Point(0, 345);
            this.gridWorkItemEffort.MainView = this.gridViewWorkItemEffort;
            this.gridWorkItemEffort.Name = "gridWorkItemEffort";
            this.gridWorkItemEffort.Size = new System.Drawing.Size(1194, 277);
            this.gridWorkItemEffort.TabIndex = 1;
            this.gridWorkItemEffort.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewWorkItemEffort});
            //
            // gridViewWorkItemEffort
            //
            this.gridViewWorkItemEffort.GridControl = this.gridWorkItemEffort;
            this.gridViewWorkItemEffort.Name = "gridViewWorkItemEffort";
            this.gridViewWorkItemEffort.OptionsBehavior.Editable = false;
            this.gridViewWorkItemEffort.OptionsView.ShowGroupPanel = true;
            this.gridViewWorkItemEffort.OptionsView.ShowFooter = true;
            //
            // lblDailyEffortTitle
            //
            this.lblDailyEffortTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblDailyEffortTitle.Location = new System.Drawing.Point(12, 5);
            this.lblDailyEffortTitle.Name = "lblDailyEffortTitle";
            this.lblDailyEffortTitle.Size = new System.Drawing.Size(200, 18);
            this.lblDailyEffortTitle.TabIndex = 2;
            this.lblDailyEffortTitle.Text = "Günlük Efor Dağılımı";
            //
            // lblWorkItemEffortTitle
            //
            this.lblWorkItemEffortTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblWorkItemEffortTitle.Location = new System.Drawing.Point(12, 335);
            this.lblWorkItemEffortTitle.Name = "lblWorkItemEffortTitle";
            this.lblWorkItemEffortTitle.Size = new System.Drawing.Size(200, 18);
            this.lblWorkItemEffortTitle.TabIndex = 3;
            this.lblWorkItemEffortTitle.Text = "İş Bazında Efor Dağılımı";
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
            this.tabDetailed.ResumeLayout(false);
            this.tabDailyEffort.ResumeLayout(false);
            this.tabDailyEffort.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDetailed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewDetailed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDailyEffort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDailyEffort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridWorkItemEffort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewWorkItemEffort)).EndInit();
            this.ResumeLayout(false);
        }
    }
}

