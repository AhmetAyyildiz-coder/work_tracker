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
        private Chart chartEffort;
        private SimpleButton btnRefresh;
        private PanelControl panelControl1;
        private DateEdit dtFrom;
        private DateEdit dtTo;
        private LabelControl lblFrom;
        private LabelControl lblTo;
        private DevExpress.XtraGrid.GridControl gridDetailed;
        private DevExpress.XtraGrid.Views.Grid.GridView viewDetailed;

        private void InitializeComponent()
        {
            this.tabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabChart = new DevExpress.XtraTab.XtraTabPage();
            this.chartEffort = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabDetailed = new DevExpress.XtraTab.XtraTabPage();
            this.gridDetailed = new DevExpress.XtraGrid.GridControl();
            this.viewDetailed = new DevExpress.XtraGrid.Views.Grid.GridView();
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
            this.tabDetailed});
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
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDetailed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewDetailed)).EndInit();
            this.ResumeLayout(false);
        }
    }
}

