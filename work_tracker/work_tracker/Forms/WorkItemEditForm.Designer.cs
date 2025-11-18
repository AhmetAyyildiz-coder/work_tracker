using DevExpress.XtraEditors;

namespace work_tracker.Forms
{
    partial class WorkItemEditForm
    {
        private System.ComponentModel.IContainer components = null;
        private LabelControl labelControl1;
        private LabelControl labelControl2;
        private LabelControl labelControl3;
        private LabelControl labelControl4;
        private LabelControl labelControl5;
        private LabelControl labelControl6;
        private LabelControl labelControl7;
        private TextEdit txtTitle;
        private MemoEdit txtDescription;
        private TextEdit txtRequestedBy;
        private DateEdit dtRequestedAt;
        private LookUpEdit cmbProject;
        private LookUpEdit cmbModule;
        private LookUpEdit cmbMeeting;
        private SimpleButton btnSave;
        private SimpleButton btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                _context?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtTitle = new DevExpress.XtraEditors.TextEdit();
            this.txtDescription = new DevExpress.XtraEditors.MemoEdit();
            this.txtRequestedBy = new DevExpress.XtraEditors.TextEdit();
            this.dtRequestedAt = new DevExpress.XtraEditors.DateEdit();
            this.cmbProject = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbModule = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbMeeting = new DevExpress.XtraEditors.LookUpEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestedBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtRequestedAt.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtRequestedAt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbModule.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMeeting.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(20, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(30, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Başlık:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(20, 70);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(45, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Açıklama:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(20, 180);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(57, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Talep Eden:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(400, 180);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(59, 13);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Talep Tarihi:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(20, 230);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(29, 13);
            this.labelControl5.TabIndex = 4;
            this.labelControl5.Text = "Proje:";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(400, 230);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(32, 13);
            this.labelControl6.TabIndex = 5;
            this.labelControl6.Text = "Modül:";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(20, 280);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(127, 13);
            this.labelControl7.TabIndex = 6;
            this.labelControl7.Text = "İlişkili Toplantı (Opsiyonel):";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(20, 40);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(720, 20);
            this.txtTitle.TabIndex = 7;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(20, 90);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(720, 70);
            this.txtDescription.TabIndex = 8;
            // 
            // txtRequestedBy
            // 
            this.txtRequestedBy.Location = new System.Drawing.Point(20, 200);
            this.txtRequestedBy.Name = "txtRequestedBy";
            this.txtRequestedBy.Size = new System.Drawing.Size(350, 20);
            this.txtRequestedBy.TabIndex = 9;
            // 
            // dtRequestedAt
            // 
            this.dtRequestedAt.EditValue = null;
            this.dtRequestedAt.Location = new System.Drawing.Point(400, 200);
            this.dtRequestedAt.Name = "dtRequestedAt";
            this.dtRequestedAt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtRequestedAt.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtRequestedAt.Size = new System.Drawing.Size(340, 20);
            this.dtRequestedAt.TabIndex = 10;
            // 
            // cmbProject
            // 
            this.cmbProject.Location = new System.Drawing.Point(20, 250);
            this.cmbProject.Name = "cmbProject";
            this.cmbProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbProject.Properties.NullText = "Proje seçin...";
            this.cmbProject.Size = new System.Drawing.Size(350, 20);
            this.cmbProject.TabIndex = 11;
            this.cmbProject.EditValueChanged += new System.EventHandler(this.cmbProject_EditValueChanged);
            // 
            // cmbModule
            // 
            this.cmbModule.Location = new System.Drawing.Point(400, 250);
            this.cmbModule.Name = "cmbModule";
            this.cmbModule.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbModule.Properties.NullText = "Modül seçin...";
            this.cmbModule.Size = new System.Drawing.Size(340, 20);
            this.cmbModule.TabIndex = 12;
            // 
            // cmbMeeting
            // 
            this.cmbMeeting.Location = new System.Drawing.Point(20, 300);
            this.cmbMeeting.Name = "cmbMeeting";
            this.cmbMeeting.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMeeting.Properties.NullText = "Toplantı seçin (opsiyonel)...";
            this.cmbMeeting.Size = new System.Drawing.Size(720, 20);
            this.cmbMeeting.TabIndex = 13;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(560, 340);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Kaydet";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(650, 340);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "İptal";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // WorkItemEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 390);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbMeeting);
            this.Controls.Add(this.cmbModule);
            this.Controls.Add(this.cmbProject);
            this.Controls.Add(this.dtRequestedAt);
            this.Controls.Add(this.txtRequestedBy);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WorkItemEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "İş Talebi Düzenle";
            this.Load += new System.EventHandler(this.WorkItemEditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestedBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtRequestedAt.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtRequestedAt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbModule.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMeeting.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

