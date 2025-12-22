using DevExpress.XtraEditors;

namespace work_tracker.Forms
{
    partial class DocumentReferenceEditForm
    {
        private System.ComponentModel.IContainer components = null;

        private LabelControl lblTitle;
        private TextEdit txtTitle;
        private LabelControl lblFilePath;
        private TextEdit txtFilePath;
        private SimpleButton btnBrowse;
        private SimpleButton btnCreateNewWord;
        private SimpleButton btnCreateNewExcel;
        private SimpleButton btnCreateNewText;
        private LabelControl lblDescription;
        private MemoEdit txtDescription;
        private LabelControl lblProject;
        private LookUpEdit cmbProject;
        private LabelControl lblModule;
        private LookUpEdit cmbModule;
        private LabelControl lblWorkItem;
        private LookUpEdit cmbWorkItem;
        private LabelControl lblTags;
        private CheckedComboBoxEdit chkTags;
        private CheckEdit chkFavorite;
        private SimpleButton btnSave;
        private SimpleButton btnCancel;
        private GroupControl grpCreateNew;

        private void InitializeComponent()
        {
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.txtTitle = new DevExpress.XtraEditors.TextEdit();
            this.lblFilePath = new DevExpress.XtraEditors.LabelControl();
            this.txtFilePath = new DevExpress.XtraEditors.TextEdit();
            this.btnBrowse = new DevExpress.XtraEditors.SimpleButton();
            this.btnCreateNewWord = new DevExpress.XtraEditors.SimpleButton();
            this.btnCreateNewExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnCreateNewText = new DevExpress.XtraEditors.SimpleButton();
            this.grpCreateNew = new DevExpress.XtraEditors.GroupControl();
            this.lblDescription = new DevExpress.XtraEditors.LabelControl();
            this.txtDescription = new DevExpress.XtraEditors.MemoEdit();
            this.lblProject = new DevExpress.XtraEditors.LabelControl();
            this.cmbProject = new DevExpress.XtraEditors.LookUpEdit();
            this.lblModule = new DevExpress.XtraEditors.LabelControl();
            this.cmbModule = new DevExpress.XtraEditors.LookUpEdit();
            this.lblWorkItem = new DevExpress.XtraEditors.LabelControl();
            this.cmbWorkItem = new DevExpress.XtraEditors.LookUpEdit();
            this.lblTags = new DevExpress.XtraEditors.LabelControl();
            this.chkTags = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.chkFavorite = new DevExpress.XtraEditors.CheckEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbModule.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWorkItem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTags.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFavorite.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpCreateNew)).BeginInit();
            this.grpCreateNew.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(15, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(36, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Başlık:*";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(120, 17);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(400, 20);
            this.txtTitle.TabIndex = 1;
            // 
            // lblFilePath
            // 
            this.lblFilePath.Location = new System.Drawing.Point(15, 50);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(55, 13);
            this.lblFilePath.TabIndex = 2;
            this.lblFilePath.Text = "Dosya Yolu:*";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(120, 47);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(330, 20);
            this.txtFilePath.TabIndex = 3;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(455, 45);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "📂 Gözat...";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // grpCreateNew
            // 
            this.grpCreateNew.Controls.Add(this.btnCreateNewWord);
            this.grpCreateNew.Controls.Add(this.btnCreateNewExcel);
            this.grpCreateNew.Controls.Add(this.btnCreateNewText);
            this.grpCreateNew.Location = new System.Drawing.Point(540, 12);
            this.grpCreateNew.Name = "grpCreateNew";
            this.grpCreateNew.Size = new System.Drawing.Size(130, 130);
            this.grpCreateNew.TabIndex = 5;
            this.grpCreateNew.Text = "Yeni Dosya Oluştur";
            // 
            // btnCreateNewWord
            // 
            this.btnCreateNewWord.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.btnCreateNewWord.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnCreateNewWord.Appearance.Options.UseBackColor = true;
            this.btnCreateNewWord.Appearance.Options.UseForeColor = true;
            this.btnCreateNewWord.Location = new System.Drawing.Point(10, 30);
            this.btnCreateNewWord.Name = "btnCreateNewWord";
            this.btnCreateNewWord.Size = new System.Drawing.Size(110, 28);
            this.btnCreateNewWord.TabIndex = 0;
            this.btnCreateNewWord.Text = "📝 Word";
            this.btnCreateNewWord.Click += new System.EventHandler(this.btnCreateNewWord_Click);
            // 
            // btnCreateNewExcel
            // 
            this.btnCreateNewExcel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(115)))), ((int)(((byte)(70)))));
            this.btnCreateNewExcel.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnCreateNewExcel.Appearance.Options.UseBackColor = true;
            this.btnCreateNewExcel.Appearance.Options.UseForeColor = true;
            this.btnCreateNewExcel.Location = new System.Drawing.Point(10, 62);
            this.btnCreateNewExcel.Name = "btnCreateNewExcel";
            this.btnCreateNewExcel.Size = new System.Drawing.Size(110, 28);
            this.btnCreateNewExcel.TabIndex = 1;
            this.btnCreateNewExcel.Text = "📊 Excel";
            this.btnCreateNewExcel.Click += new System.EventHandler(this.btnCreateNewExcel_Click);
            // 
            // btnCreateNewText
            // 
            this.btnCreateNewText.Location = new System.Drawing.Point(10, 94);
            this.btnCreateNewText.Name = "btnCreateNewText";
            this.btnCreateNewText.Size = new System.Drawing.Size(110, 28);
            this.btnCreateNewText.TabIndex = 2;
            this.btnCreateNewText.Text = "📄 Text";
            this.btnCreateNewText.Click += new System.EventHandler(this.btnCreateNewText_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(15, 80);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(45, 13);
            this.lblDescription.TabIndex = 5;
            this.lblDescription.Text = "Açıklama:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(120, 77);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(400, 60);
            this.txtDescription.TabIndex = 6;
            // 
            // lblProject
            // 
            this.lblProject.Location = new System.Drawing.Point(15, 150);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(28, 13);
            this.lblProject.TabIndex = 7;
            this.lblProject.Text = "Proje:";
            // 
            // cmbProject
            // 
            this.cmbProject.Location = new System.Drawing.Point(120, 147);
            this.cmbProject.Name = "cmbProject";
            this.cmbProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbProject.Properties.NullText = "(Proje Seçin - Opsiyonel)";
            this.cmbProject.Size = new System.Drawing.Size(200, 20);
            this.cmbProject.TabIndex = 8;
            this.cmbProject.EditValueChanged += new System.EventHandler(this.cmbProject_EditValueChanged);
            // 
            // lblModule
            // 
            this.lblModule.Location = new System.Drawing.Point(15, 180);
            this.lblModule.Name = "lblModule";
            this.lblModule.Size = new System.Drawing.Size(32, 13);
            this.lblModule.TabIndex = 9;
            this.lblModule.Text = "Modül:";
            // 
            // cmbModule
            // 
            this.cmbModule.Enabled = false;
            this.cmbModule.Location = new System.Drawing.Point(120, 177);
            this.cmbModule.Name = "cmbModule";
            this.cmbModule.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbModule.Properties.NullText = "(Modül Seçin - Opsiyonel)";
            this.cmbModule.Size = new System.Drawing.Size(200, 20);
            this.cmbModule.TabIndex = 10;
            // 
            // lblWorkItem
            // 
            this.lblWorkItem.Location = new System.Drawing.Point(15, 210);
            this.lblWorkItem.Name = "lblWorkItem";
            this.lblWorkItem.Size = new System.Drawing.Size(50, 13);
            this.lblWorkItem.TabIndex = 11;
            this.lblWorkItem.Text = "İş Kalemi:";
            // 
            // cmbWorkItem
            // 
            this.cmbWorkItem.Location = new System.Drawing.Point(120, 207);
            this.cmbWorkItem.Name = "cmbWorkItem";
            this.cmbWorkItem.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWorkItem.Properties.NullText = "(İş Kalemi Seçin - Opsiyonel)";
            this.cmbWorkItem.Size = new System.Drawing.Size(400, 20);
            this.cmbWorkItem.TabIndex = 12;
            // 
            // lblTags
            // 
            this.lblTags.Location = new System.Drawing.Point(15, 240);
            this.lblTags.Name = "lblTags";
            this.lblTags.Size = new System.Drawing.Size(41, 13);
            this.lblTags.TabIndex = 13;
            this.lblTags.Text = "Etiketler:";
            // 
            // chkTags
            // 
            this.chkTags.Location = new System.Drawing.Point(120, 237);
            this.chkTags.Name = "chkTags";
            this.chkTags.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chkTags.Properties.NullText = "(Etiket Seçin)";
            this.chkTags.Size = new System.Drawing.Size(400, 20);
            this.chkTags.TabIndex = 14;
            // 
            // chkFavorite
            // 
            this.chkFavorite.Location = new System.Drawing.Point(120, 270);
            this.chkFavorite.Name = "chkFavorite";
            this.chkFavorite.Properties.Caption = "⭐ Favorilere Ekle";
            this.chkFavorite.Size = new System.Drawing.Size(150, 19);
            this.chkFavorite.TabIndex = 15;
            // 
            // btnSave
            // 
            this.btnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(124)))), ((int)(((byte)(16)))));
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.Location = new System.Drawing.Point(320, 310);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "💾 Kaydet";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(430, 310);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "İptal";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // DocumentReferenceEditForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 360);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.grpCreateNew);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblProject);
            this.Controls.Add(this.cmbProject);
            this.Controls.Add(this.lblModule);
            this.Controls.Add(this.cmbModule);
            this.Controls.Add(this.lblWorkItem);
            this.Controls.Add(this.cmbWorkItem);
            this.Controls.Add(this.lblTags);
            this.Controls.Add(this.chkTags);
            this.Controls.Add(this.chkFavorite);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DocumentReferenceEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Döküman Ekle/Düzenle";
            this.Load += new System.EventHandler(this.DocumentReferenceEditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbModule.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWorkItem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTags.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFavorite.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpCreateNew)).EndInit();
            this.grpCreateNew.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
