namespace work_tracker.Forms
{
    partial class CommentSearchForm
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
            this.panelTop = new DevExpress.XtraEditors.PanelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelFilters = new DevExpress.XtraEditors.PanelControl();
            this.chkEmailBody = new DevExpress.XtraEditors.CheckEdit();
            this.chkEmailNotes = new DevExpress.XtraEditors.CheckEdit();
            this.chkActivities = new DevExpress.XtraEditors.CheckEdit();
            this.chkDescription = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelBottom = new DevExpress.XtraEditors.PanelControl();
            this.lblResultCount = new DevExpress.XtraEditors.LabelControl();
            this.btnGoToWorkItem = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).BeginInit();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelFilters)).BeginInit();
            this.panelFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkEmailBody.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEmailNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActivities.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btnClear);
            this.panelTop.Controls.Add(this.btnSearch);
            this.panelTop.Controls.Add(this.txtSearch);
            this.panelTop.Controls.Add(this.labelControl1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(984, 50);
            this.panelTop.TabIndex = 0;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(889, 12);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(83, 26);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Temizle";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(800, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(83, 26);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Ara";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(129, 15);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Properties.NullValuePrompt = "Aranacak metin...";
            this.txtSearch.Size = new System.Drawing.Size(665, 20);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 17);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(111, 16);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "🔍 Yorum Arama:";
            // 
            // panelFilters
            // 
            this.panelFilters.Controls.Add(this.chkEmailBody);
            this.panelFilters.Controls.Add(this.chkEmailNotes);
            this.panelFilters.Controls.Add(this.chkActivities);
            this.panelFilters.Controls.Add(this.chkDescription);
            this.panelFilters.Controls.Add(this.labelControl2);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Location = new System.Drawing.Point(0, 50);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(984, 40);
            this.panelFilters.TabIndex = 1;
            // 
            // chkEmailBody
            // 
            this.chkEmailBody.Location = new System.Drawing.Point(430, 10);
            this.chkEmailBody.Name = "chkEmailBody";
            this.chkEmailBody.Properties.Caption = "Email İçerikleri";
            this.chkEmailBody.Size = new System.Drawing.Size(110, 20);
            this.chkEmailBody.TabIndex = 4;
            // 
            // chkEmailNotes
            // 
            this.chkEmailNotes.Location = new System.Drawing.Point(320, 10);
            this.chkEmailNotes.Name = "chkEmailNotes";
            this.chkEmailNotes.Properties.Caption = "Email Notları";
            this.chkEmailNotes.Size = new System.Drawing.Size(100, 20);
            this.chkEmailNotes.TabIndex = 3;
            this.chkEmailNotes.EditValue = true;
            // 
            // chkActivities
            // 
            this.chkActivities.Location = new System.Drawing.Point(216, 10);
            this.chkActivities.Name = "chkActivities";
            this.chkActivities.Properties.Caption = "Yorumlar";
            this.chkActivities.Size = new System.Drawing.Size(90, 20);
            this.chkActivities.TabIndex = 2;
            this.chkActivities.EditValue = true;
            // 
            // chkDescription
            // 
            this.chkDescription.Location = new System.Drawing.Point(100, 10);
            this.chkDescription.Name = "chkDescription";
            this.chkDescription.Properties.Caption = "İş Açıklamaları";
            this.chkDescription.Size = new System.Drawing.Size(110, 20);
            this.chkDescription.TabIndex = 1;
            this.chkDescription.EditValue = true;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(82, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Arama Kapsamı:";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 90);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(984, 421);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.DoubleClick += new System.EventHandler(this.gridControl1_DoubleClick);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.lblResultCount);
            this.panelBottom.Controls.Add(this.btnGoToWorkItem);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 511);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(984, 50);
            this.panelBottom.TabIndex = 3;
            // 
            // lblResultCount
            // 
            this.lblResultCount.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblResultCount.Appearance.Options.UseFont = true;
            this.lblResultCount.Location = new System.Drawing.Point(12, 17);
            this.lblResultCount.Name = "lblResultCount";
            this.lblResultCount.Size = new System.Drawing.Size(0, 14);
            this.lblResultCount.TabIndex = 1;
            // 
            // btnGoToWorkItem
            // 
            this.btnGoToWorkItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoToWorkItem.Location = new System.Drawing.Point(859, 12);
            this.btnGoToWorkItem.Name = "btnGoToWorkItem";
            this.btnGoToWorkItem.Size = new System.Drawing.Size(113, 26);
            this.btnGoToWorkItem.TabIndex = 0;
            this.btnGoToWorkItem.Text = "İşe Git";
            this.btnGoToWorkItem.Click += new System.EventHandler(this.btnGoToWorkItem_Click);
            // 
            // CommentSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelFilters);
            this.Controls.Add(this.panelTop);
            this.Name = "CommentSearchForm";
            this.Text = "🔍 Yorum ve Not Arama";
            this.Load += new System.EventHandler(this.CommentSearchForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelFilters)).EndInit();
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkEmailBody.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEmailNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActivities.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelTop;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelFilters;
        private DevExpress.XtraEditors.CheckEdit chkEmailBody;
        private DevExpress.XtraEditors.CheckEdit chkEmailNotes;
        private DevExpress.XtraEditors.CheckEdit chkActivities;
        private DevExpress.XtraEditors.CheckEdit chkDescription;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.PanelControl panelBottom;
        private DevExpress.XtraEditors.LabelControl lblResultCount;
        private DevExpress.XtraEditors.SimpleButton btnGoToWorkItem;
    }
}
