namespace work_tracker.Forms
{
    partial class DailyActivityReportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gridDailyReport = new DevExpress.XtraGrid.GridControl();
            this.gridViewDailyReport = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblTotalWork = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalMudahale = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalPhone = new DevExpress.XtraEditors.LabelControl();
            this.lblWorkItemTitle = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridDailyReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDailyReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridDailyReport
            // 
            this.gridDailyReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDailyReport.Location = new System.Drawing.Point(0, 60);
            this.gridDailyReport.MainView = this.gridViewDailyReport;
            this.gridDailyReport.Name = "gridDailyReport";
            this.gridDailyReport.Size = new System.Drawing.Size(1000, 490);
            this.gridDailyReport.TabIndex = 0;
            this.gridDailyReport.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDailyReport});
            // 
            // gridViewDailyReport
            // 
            this.gridViewDailyReport.GridControl = this.gridDailyReport;
            this.gridViewDailyReport.Name = "gridViewDailyReport";
            this.gridViewDailyReport.OptionsBehavior.Editable = false;
            this.gridViewDailyReport.OptionsView.ShowGroupPanel = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblTotalWork);
            this.panelControl1.Controls.Add(this.lblTotalMudahale);
            this.panelControl1.Controls.Add(this.lblTotalPhone);
            this.panelControl1.Controls.Add(this.lblWorkItemTitle);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1000, 60);
            this.panelControl1.TabIndex = 1;
            // 
            // lblTotalWork
            // 
            this.lblTotalWork.Location = new System.Drawing.Point(450, 36);
            this.lblTotalWork.Name = "lblTotalWork";
            this.lblTotalWork.Size = new System.Drawing.Size(100, 13);
            this.lblTotalWork.TabIndex = 3;
            this.lblTotalWork.Text = "Toplam Çalışma: 0 dk";
            // 
            // lblTotalMudahale
            // 
            this.lblTotalMudahale.Location = new System.Drawing.Point(200, 36);
            this.lblTotalMudahale.Name = "lblTotalMudahale";
            this.lblTotalMudahale.Size = new System.Drawing.Size(155, 13);
            this.lblTotalMudahale.TabIndex = 2;
            this.lblTotalMudahale.Text = "Toplam Müdahale Bekliyor: 0 kez";
            // 
            // lblTotalPhone
            // 
            this.lblTotalPhone.Location = new System.Drawing.Point(12, 36);
            this.lblTotalPhone.Name = "lblTotalPhone";
            this.lblTotalPhone.Size = new System.Drawing.Size(100, 13);
            this.lblTotalPhone.TabIndex = 1;
            this.lblTotalPhone.Text = "Toplam Telefon: 0 dk";
            // 
            // lblWorkItemTitle
            // 
            this.lblWorkItemTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblWorkItemTitle.Appearance.Options.UseFont = true;
            this.lblWorkItemTitle.Location = new System.Drawing.Point(12, 12);
            this.lblWorkItemTitle.Name = "lblWorkItemTitle";
            this.lblWorkItemTitle.Size = new System.Drawing.Size(95, 16);
            this.lblWorkItemTitle.TabIndex = 0;
            this.lblWorkItemTitle.Text = "İş Öğesi Başlığı";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnRefresh);
            this.panelControl2.Controls.Add(this.btnClose);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 550);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1000, 50);
            this.panelControl2.TabIndex = 2;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 26);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Yenile";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(888, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 26);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Kapat";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // DailyActivityReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.gridDailyReport);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "DailyActivityReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Günlük Aktivite Raporu";
            this.Load += new System.EventHandler(this.DailyActivityReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridDailyReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDailyReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridDailyReport;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDailyReport;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblWorkItemTitle;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraEditors.LabelControl lblTotalPhone;
        private DevExpress.XtraEditors.LabelControl lblTotalMudahale;
        private DevExpress.XtraEditors.LabelControl lblTotalWork;
    }
}
