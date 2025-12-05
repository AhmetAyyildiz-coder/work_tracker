namespace work_tracker.Forms
{
    partial class EmailToWorkItemForm
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panelEmail = new DevExpress.XtraEditors.PanelControl();
            this.gridControlEmails = new DevExpress.XtraGrid.GridControl();
            this.gridViewEmails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelEmailTop = new DevExpress.XtraEditors.PanelControl();
            this.spinDaysBack = new DevExpress.XtraEditors.SpinEdit();
            this.lblDaysBack = new DevExpress.XtraEditors.LabelControl();
            this.txtEmailFilter = new DevExpress.XtraEditors.TextEdit();
            this.btnRefreshEmails = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelConversion = new DevExpress.XtraEditors.PanelControl();
            this.groupControlPreview = new DevExpress.XtraEditors.GroupControl();
            this.memoDescription = new DevExpress.XtraEditors.MemoEdit();
            this.lblDescriptionLabel = new DevExpress.XtraEditors.LabelControl();
            this.cmbUrgency = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblUrgency = new DevExpress.XtraEditors.LabelControl();
            this.cmbType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblType = new DevExpress.XtraEditors.LabelControl();
            this.txtRequestedBy = new DevExpress.XtraEditors.TextEdit();
            this.lblRequestedBy = new DevExpress.XtraEditors.LabelControl();
            this.txtTitle = new DevExpress.XtraEditors.TextEdit();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.cmbProject = new DevExpress.XtraEditors.LookUpEdit();
            this.lblProject = new DevExpress.XtraEditors.LabelControl();
            this.panelConversionTop = new DevExpress.XtraEditors.PanelControl();
            this.btnConvert = new DevExpress.XtraEditors.SimpleButton();
            this.btnPreview = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelBottom = new DevExpress.XtraEditors.PanelControl();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelEmail)).BeginInit();
            this.panelEmail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlEmails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewEmails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelEmailTop)).BeginInit();
            this.panelEmailTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinDaysBack.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmailFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelConversion)).BeginInit();
            this.panelConversion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlPreview)).BeginInit();
            this.groupControlPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUrgency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestedBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelConversionTop)).BeginInit();
            this.panelConversionTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.panelEmail);
            this.splitContainerControl1.Panel1.Controls.Add(this.panelEmailTop);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.panelConversion);
            this.splitContainerControl1.Panel2.Controls.Add(this.panelConversionTop);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1184, 611);
            this.splitContainerControl1.SplitterPosition = 550;
            this.splitContainerControl1.TabIndex = 0;
            // 
            // panelEmail
            // 
            this.panelEmail.Controls.Add(this.gridControlEmails);
            this.panelEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEmail.Location = new System.Drawing.Point(0, 50);
            this.panelEmail.Name = "panelEmail";
            this.panelEmail.Size = new System.Drawing.Size(550, 561);
            this.panelEmail.TabIndex = 1;
            // 
            // gridControlEmails
            // 
            this.gridControlEmails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlEmails.Location = new System.Drawing.Point(2, 2);
            this.gridControlEmails.MainView = this.gridViewEmails;
            this.gridControlEmails.Name = "gridControlEmails";
            this.gridControlEmails.Size = new System.Drawing.Size(546, 557);
            this.gridControlEmails.TabIndex = 0;
            this.gridControlEmails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewEmails});
            // 
            // gridViewEmails
            // 
            this.gridViewEmails.GridControl = this.gridControlEmails;
            this.gridViewEmails.Name = "gridViewEmails";
            this.gridViewEmails.OptionsBehavior.Editable = false;
            this.gridViewEmails.OptionsView.ShowGroupPanel = false;
            this.gridViewEmails.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewEmails_FocusedRowChanged);
            // 
            // panelEmailTop
            // 
            this.panelEmailTop.Controls.Add(this.spinDaysBack);
            this.panelEmailTop.Controls.Add(this.lblDaysBack);
            this.panelEmailTop.Controls.Add(this.txtEmailFilter);
            this.panelEmailTop.Controls.Add(this.btnRefreshEmails);
            this.panelEmailTop.Controls.Add(this.labelControl1);
            this.panelEmailTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEmailTop.Location = new System.Drawing.Point(0, 0);
            this.panelEmailTop.Name = "panelEmailTop";
            this.panelEmailTop.Size = new System.Drawing.Size(550, 50);
            this.panelEmailTop.TabIndex = 0;
            // 
            // spinDaysBack
            // 
            this.spinDaysBack.EditValue = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.spinDaysBack.Location = new System.Drawing.Point(428, 15);
            this.spinDaysBack.Name = "spinDaysBack";
            this.spinDaysBack.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinDaysBack.Properties.MaxValue = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.spinDaysBack.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinDaysBack.Size = new System.Drawing.Size(50, 20);
            this.spinDaysBack.TabIndex = 4;
            // 
            // lblDaysBack
            // 
            this.lblDaysBack.Location = new System.Drawing.Point(365, 18);
            this.lblDaysBack.Name = "lblDaysBack";
            this.lblDaysBack.Size = new System.Drawing.Size(57, 13);
            this.lblDaysBack.TabIndex = 3;
            this.lblDaysBack.Text = "Son (gün):";
            // 
            // txtEmailFilter
            // 
            this.txtEmailFilter.Location = new System.Drawing.Point(133, 15);
            this.txtEmailFilter.Name = "txtEmailFilter";
            this.txtEmailFilter.Properties.NullValuePrompt = "Konu filtresi...";
            this.txtEmailFilter.Size = new System.Drawing.Size(226, 20);
            this.txtEmailFilter.TabIndex = 2;
            // 
            // btnRefreshEmails
            // 
            this.btnRefreshEmails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshEmails.Location = new System.Drawing.Point(484, 12);
            this.btnRefreshEmails.Name = "btnRefreshEmails";
            this.btnRefreshEmails.Size = new System.Drawing.Size(54, 26);
            this.btnRefreshEmails.TabIndex = 1;
            this.btnRefreshEmails.Text = "Yenile";
            this.btnRefreshEmails.Click += new System.EventHandler(this.btnRefreshEmails_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 16);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(115, 16);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "📧 Gelen Mailler:";
            // 
            // panelConversion
            // 
            this.panelConversion.Controls.Add(this.groupControlPreview);
            this.panelConversion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConversion.Location = new System.Drawing.Point(0, 50);
            this.panelConversion.Name = "panelConversion";
            this.panelConversion.Size = new System.Drawing.Size(624, 561);
            this.panelConversion.TabIndex = 1;
            // 
            // groupControlPreview
            // 
            this.groupControlPreview.Controls.Add(this.memoDescription);
            this.groupControlPreview.Controls.Add(this.lblDescriptionLabel);
            this.groupControlPreview.Controls.Add(this.cmbUrgency);
            this.groupControlPreview.Controls.Add(this.lblUrgency);
            this.groupControlPreview.Controls.Add(this.cmbType);
            this.groupControlPreview.Controls.Add(this.lblType);
            this.groupControlPreview.Controls.Add(this.txtRequestedBy);
            this.groupControlPreview.Controls.Add(this.lblRequestedBy);
            this.groupControlPreview.Controls.Add(this.txtTitle);
            this.groupControlPreview.Controls.Add(this.lblTitle);
            this.groupControlPreview.Controls.Add(this.cmbProject);
            this.groupControlPreview.Controls.Add(this.lblProject);
            this.groupControlPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControlPreview.Location = new System.Drawing.Point(2, 2);
            this.groupControlPreview.Name = "groupControlPreview";
            this.groupControlPreview.Size = new System.Drawing.Size(620, 557);
            this.groupControlPreview.TabIndex = 0;
            this.groupControlPreview.Text = "İş Talebi Önizleme";
            // 
            // memoDescription
            // 
            this.memoDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memoDescription.Location = new System.Drawing.Point(95, 205);
            this.memoDescription.Name = "memoDescription";
            this.memoDescription.Size = new System.Drawing.Size(512, 337);
            this.memoDescription.TabIndex = 11;
            // 
            // lblDescriptionLabel
            // 
            this.lblDescriptionLabel.Location = new System.Drawing.Point(14, 208);
            this.lblDescriptionLabel.Name = "lblDescriptionLabel";
            this.lblDescriptionLabel.Size = new System.Drawing.Size(47, 13);
            this.lblDescriptionLabel.TabIndex = 10;
            this.lblDescriptionLabel.Text = "Açıklama:";
            // 
            // cmbUrgency
            // 
            this.cmbUrgency.Location = new System.Drawing.Point(95, 169);
            this.cmbUrgency.Name = "cmbUrgency";
            this.cmbUrgency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbUrgency.Properties.Items.AddRange(new object[] {
            "Kritik",
            "Yüksek",
            "Normal",
            "Düşük"});
            this.cmbUrgency.Size = new System.Drawing.Size(150, 20);
            this.cmbUrgency.TabIndex = 9;
            // 
            // lblUrgency
            // 
            this.lblUrgency.Location = new System.Drawing.Point(14, 172);
            this.lblUrgency.Name = "lblUrgency";
            this.lblUrgency.Size = new System.Drawing.Size(41, 13);
            this.lblUrgency.TabIndex = 8;
            this.lblUrgency.Text = "Aciliyet:";
            // 
            // cmbType
            // 
            this.cmbType.Location = new System.Drawing.Point(95, 133);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Properties.Items.AddRange(new object[] {
            "Bug",
            "YeniOzellik",
            "Degisiklik",
            "AcilArge",
            "Destek",
            "Arastirma",
            "Test",
            "Dokumantasyon"});
            this.cmbType.Size = new System.Drawing.Size(150, 20);
            this.cmbType.TabIndex = 7;
            // 
            // lblType
            // 
            this.lblType.Location = new System.Drawing.Point(14, 136);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(17, 13);
            this.lblType.TabIndex = 6;
            this.lblType.Text = "Tip:";
            // 
            // txtRequestedBy
            // 
            this.txtRequestedBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRequestedBy.Location = new System.Drawing.Point(95, 97);
            this.txtRequestedBy.Name = "txtRequestedBy";
            this.txtRequestedBy.Size = new System.Drawing.Size(512, 20);
            this.txtRequestedBy.TabIndex = 5;
            // 
            // lblRequestedBy
            // 
            this.lblRequestedBy.Location = new System.Drawing.Point(14, 100);
            this.lblRequestedBy.Name = "lblRequestedBy";
            this.lblRequestedBy.Size = new System.Drawing.Size(58, 13);
            this.lblRequestedBy.TabIndex = 4;
            this.lblRequestedBy.Text = "Talep Eden:";
            // 
            // txtTitle
            // 
            this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitle.Location = new System.Drawing.Point(95, 61);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(512, 20);
            this.txtTitle.TabIndex = 3;
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(14, 64);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(30, 13);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Başlık:";
            // 
            // cmbProject
            // 
            this.cmbProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbProject.Location = new System.Drawing.Point(95, 30);
            this.cmbProject.Name = "cmbProject";
            this.cmbProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbProject.Properties.NullText = "(Proje Seçin)";
            this.cmbProject.Size = new System.Drawing.Size(512, 20);
            this.cmbProject.TabIndex = 1;
            // 
            // lblProject
            // 
            this.lblProject.Location = new System.Drawing.Point(14, 33);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(28, 13);
            this.lblProject.TabIndex = 0;
            this.lblProject.Text = "Proje:";
            // 
            // panelConversionTop
            // 
            this.panelConversionTop.Controls.Add(this.btnConvert);
            this.panelConversionTop.Controls.Add(this.btnPreview);
            this.panelConversionTop.Controls.Add(this.labelControl2);
            this.panelConversionTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelConversionTop.Location = new System.Drawing.Point(0, 0);
            this.panelConversionTop.Name = "panelConversionTop";
            this.panelConversionTop.Size = new System.Drawing.Size(624, 50);
            this.panelConversionTop.TabIndex = 0;
            // 
            // btnConvert
            // 
            this.btnConvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConvert.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnConvert.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnConvert.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnConvert.Appearance.Options.UseBackColor = true;
            this.btnConvert.Appearance.Options.UseFont = true;
            this.btnConvert.Appearance.Options.UseForeColor = true;
            this.btnConvert.Location = new System.Drawing.Point(485, 12);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(127, 26);
            this.btnConvert.TabIndex = 2;
            this.btnConvert.Text = "İş Olarak Kaydet";
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreview.Location = new System.Drawing.Point(379, 12);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(100, 26);
            this.btnPreview.TabIndex = 1;
            this.btnPreview.Text = "Önizle";
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(12, 16);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(176, 16);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "📋 Mail → İş Talebi Dönüşüm";
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.lblStatus);
            this.panelBottom.Controls.Add(this.btnClose);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 611);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1184, 50);
            this.panelBottom.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblStatus.Appearance.Options.UseFont = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 17);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 14);
            this.lblStatus.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(1097, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 26);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Kapat";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // EmailToWorkItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.panelBottom);
            this.Name = "EmailToWorkItemForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "📧 Mail\'den İş Talebi Oluştur";
            this.Load += new System.EventHandler(this.EmailToWorkItemForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelEmail)).EndInit();
            this.panelEmail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlEmails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewEmails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelEmailTop)).EndInit();
            this.panelEmailTop.ResumeLayout(false);
            this.panelEmailTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinDaysBack.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmailFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelConversion)).EndInit();
            this.panelConversion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlPreview)).EndInit();
            this.groupControlPreview.ResumeLayout(false);
            this.groupControlPreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUrgency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestedBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelConversionTop)).EndInit();
            this.panelConversionTop.ResumeLayout(false);
            this.panelConversionTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PanelControl panelEmail;
        private DevExpress.XtraGrid.GridControl gridControlEmails;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewEmails;
        private DevExpress.XtraEditors.PanelControl panelEmailTop;
        private DevExpress.XtraEditors.SpinEdit spinDaysBack;
        private DevExpress.XtraEditors.LabelControl lblDaysBack;
        private DevExpress.XtraEditors.TextEdit txtEmailFilter;
        private DevExpress.XtraEditors.SimpleButton btnRefreshEmails;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelConversion;
        private DevExpress.XtraEditors.GroupControl groupControlPreview;
        private DevExpress.XtraEditors.MemoEdit memoDescription;
        private DevExpress.XtraEditors.LabelControl lblDescriptionLabel;
        private DevExpress.XtraEditors.ComboBoxEdit cmbUrgency;
        private DevExpress.XtraEditors.LabelControl lblUrgency;
        private DevExpress.XtraEditors.ComboBoxEdit cmbType;
        private DevExpress.XtraEditors.LabelControl lblType;
        private DevExpress.XtraEditors.TextEdit txtRequestedBy;
        private DevExpress.XtraEditors.LabelControl lblRequestedBy;
        private DevExpress.XtraEditors.TextEdit txtTitle;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LookUpEdit cmbProject;
        private DevExpress.XtraEditors.LabelControl lblProject;
        private DevExpress.XtraEditors.PanelControl panelConversionTop;
        private DevExpress.XtraEditors.SimpleButton btnConvert;
        private DevExpress.XtraEditors.SimpleButton btnPreview;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PanelControl panelBottom;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private DevExpress.XtraEditors.SimpleButton btnClose;
    }
}
