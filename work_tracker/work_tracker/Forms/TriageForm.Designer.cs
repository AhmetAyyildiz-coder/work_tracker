using DevExpress.XtraEditors;

namespace work_tracker.Forms
{
    partial class TriageForm
    {
        private System.ComponentModel.IContainer components = null;
        private LabelControl lblWorkItemId;
        private GroupControl groupControl1;
        private GroupControl groupControl2;
        private LabelControl labelControl1;
        private LabelControl labelControl2;
        private LabelControl labelControl3;
        private LabelControl labelControl4;
        private LabelControl labelControl5;
        private LabelControl labelControl6;
        private LabelControl labelControl7;
        private LabelControl labelControl8;
        private LabelControl labelControl9;
        private TextEdit txtTitle;
        private MemoEdit txtDescription;
        private TextEdit txtRequestedBy;
        private DateEdit dtRequestedAt;
        private LookUpEdit cmbProject;
        private LookUpEdit cmbModule;
        private ComboBoxEdit cmbType;
        private ComboBoxEdit cmbUrgency;
        private TextEdit txtEffort;
        private ComboBoxEdit cmbTargetBoard;
        private LabelControl lblSprint;
        private LookUpEdit cmbSprint;
        private SimpleButton btnSaveAndRoute;
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
            this.lblWorkItemId = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.dtRequestedAt = new DevExpress.XtraEditors.DateEdit();
            this.txtRequestedBy = new DevExpress.XtraEditors.TextEdit();
            this.txtDescription = new DevExpress.XtraEditors.MemoEdit();
            this.txtTitle = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.cmbTargetBoard = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblSprint = new DevExpress.XtraEditors.LabelControl();
            this.cmbSprint = new DevExpress.XtraEditors.LookUpEdit();
            this.txtEffort = new DevExpress.XtraEditors.TextEdit();
            this.cmbUrgency = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbModule = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbProject = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnSaveAndRoute = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtRequestedAt.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtRequestedAt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestedBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTargetBoard.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEffort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUrgency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbModule.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSprint.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblWorkItemId
            // 
            this.lblWorkItemId.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblWorkItemId.Appearance.Options.UseFont = true;
            this.lblWorkItemId.Location = new System.Drawing.Point(20, 20);
            this.lblWorkItemId.Name = "lblWorkItemId";
            this.lblWorkItemId.Size = new System.Drawing.Size(126, 19);
            this.lblWorkItemId.TabIndex = 0;
            this.lblWorkItemId.Text = "İş Talebi #0000";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.dtRequestedAt);
            this.groupControl1.Controls.Add(this.txtRequestedBy);
            this.groupControl1.Controls.Add(this.txtDescription);
            this.groupControl1.Controls.Add(this.txtTitle);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(20, 50);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(920, 250);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Talep Bilgileri";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.labelControl9);
            this.groupControl2.Controls.Add(this.cmbTargetBoard);
            this.groupControl2.Controls.Add(this.lblSprint);
            this.groupControl2.Controls.Add(this.cmbSprint);
            this.groupControl2.Controls.Add(this.txtEffort);
            this.groupControl2.Controls.Add(this.cmbUrgency);
            this.groupControl2.Controls.Add(this.cmbType);
            this.groupControl2.Controls.Add(this.cmbModule);
            this.groupControl2.Controls.Add(this.cmbProject);
            this.groupControl2.Controls.Add(this.labelControl8);
            this.groupControl2.Controls.Add(this.labelControl7);
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Location = new System.Drawing.Point(20, 310);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(920, 200);
            this.groupControl2.TabIndex = 2;
            this.groupControl2.Text = "Sınıflandırma";
            // 
            // lblWorkItemId properties at line 20
            // txtTitle
            this.txtTitle.Enabled = false;
            this.txtTitle.Location = new System.Drawing.Point(15, 50);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(890, 20);
            this.txtTitle.TabIndex = 0;
            // 
            // txtDescription
            this.txtDescription.Enabled = false;
            this.txtDescription.Location = new System.Drawing.Point(15, 100);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(890, 60);
            this.txtDescription.TabIndex = 1;
            // 
            // txtRequestedBy
            this.txtRequestedBy.Enabled = false;
            this.txtRequestedBy.Location = new System.Drawing.Point(15, 190);
            this.txtRequestedBy.Name = "txtRequestedBy";
            this.txtRequestedBy.Size = new System.Drawing.Size(400, 20);
            this.txtRequestedBy.TabIndex = 2;
            // 
            // dtRequestedAt
            this.dtRequestedAt.EditValue = null;
            this.dtRequestedAt.Enabled = false;
            this.dtRequestedAt.Location = new System.Drawing.Point(450, 190);
            this.dtRequestedAt.Name = "dtRequestedAt";
            this.dtRequestedAt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtRequestedAt.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtRequestedAt.Size = new System.Drawing.Size(455, 20);
            this.dtRequestedAt.TabIndex = 3;
            // 
            // Labels for groupControl1
            this.labelControl1.Location = new System.Drawing.Point(15, 30);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(31, 13);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Başlık:";
            // 
            // labelControl2
            this.labelControl2.Location = new System.Drawing.Point(15, 80);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(47, 13);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "Açıklama:";
            // 
            // labelControl3
            this.labelControl3.Location = new System.Drawing.Point(15, 170);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(61, 13);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "Talep Eden:";
            // 
            // labelControl4
            this.labelControl4.Location = new System.Drawing.Point(450, 170);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(64, 13);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "Talep Tarihi:";
            // 
            // cmbProject
            this.cmbProject.Location = new System.Drawing.Point(15, 50);
            this.cmbProject.Name = "cmbProject";
            this.cmbProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbProject.Properties.NullText = "Proje seçin...";
            this.cmbProject.Size = new System.Drawing.Size(400, 20);
            this.cmbProject.TabIndex = 0;
            this.cmbProject.EditValueChanged += new System.EventHandler(this.cmbProject_EditValueChanged);
            // 
            // cmbModule
            this.cmbModule.Location = new System.Drawing.Point(450, 50);
            this.cmbModule.Name = "cmbModule";
            this.cmbModule.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbModule.Properties.NullText = "Modül seçin...";
            this.cmbModule.Size = new System.Drawing.Size(455, 20);
            this.cmbModule.TabIndex = 1;
            // 
            // cmbType
            this.cmbType.Location = new System.Drawing.Point(15, 100);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Size = new System.Drawing.Size(290, 20);
            this.cmbType.TabIndex = 2;
            // 
            // cmbUrgency
            this.cmbUrgency.Location = new System.Drawing.Point(320, 100);
            this.cmbUrgency.Name = "cmbUrgency";
            this.cmbUrgency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbUrgency.Size = new System.Drawing.Size(290, 20);
            this.cmbUrgency.TabIndex = 3;
            // 
            // txtEffort
            this.txtEffort.Location = new System.Drawing.Point(625, 100);
            this.txtEffort.Name = "txtEffort";
            this.txtEffort.Size = new System.Drawing.Size(280, 20);
            this.txtEffort.TabIndex = 4;
            // 
            // cmbTargetBoard
            this.cmbTargetBoard.Location = new System.Drawing.Point(15, 150);
            this.cmbTargetBoard.Name = "cmbTargetBoard";
            this.cmbTargetBoard.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTargetBoard.Size = new System.Drawing.Size(400, 20);
            this.cmbTargetBoard.TabIndex = 5;
            this.cmbTargetBoard.EditValueChanged += new System.EventHandler(this.cmbTargetBoard_EditValueChanged);
            // 
            // lblSprint
            this.lblSprint.Location = new System.Drawing.Point(450, 130);
            this.lblSprint.Name = "lblSprint";
            this.lblSprint.Size = new System.Drawing.Size(33, 13);
            this.lblSprint.TabIndex = 11;
            this.lblSprint.Text = "Sprint:";
            // 
            // cmbSprint
            this.cmbSprint.Location = new System.Drawing.Point(450, 150);
            this.cmbSprint.Name = "cmbSprint";
            this.cmbSprint.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSprint.Properties.NullText = "Sprint seçin...";
            this.cmbSprint.Size = new System.Drawing.Size(455, 20);
            this.cmbSprint.TabIndex = 6;
            // 
            // Labels for groupControl2
            this.labelControl5.Location = new System.Drawing.Point(15, 30);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(72, 13);
            this.labelControl5.TabIndex = 6;
            this.labelControl5.Text = "Proje / Modül:";
            // 
            // labelControl6
            this.labelControl6.Location = new System.Drawing.Point(15, 80);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(36, 13);
            this.labelControl6.TabIndex = 7;
            this.labelControl6.Text = "İş Tipi:";
            // 
            // labelControl7
            this.labelControl7.Location = new System.Drawing.Point(320, 80);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(39, 13);
            this.labelControl7.TabIndex = 8;
            this.labelControl7.Text = "Aciliyet:";
            // 
            // labelControl8
            this.labelControl8.Location = new System.Drawing.Point(625, 80);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(93, 13);
            this.labelControl8.TabIndex = 9;
            this.labelControl8.Text = "Tahmini Efor (gün):";
            // 
            // labelControl9
            this.labelControl9.Location = new System.Drawing.Point(15, 130);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(188, 13);
            this.labelControl9.TabIndex = 10;
            this.labelControl9.Text = "Hedef Pano (Scrum / Kanban):";
            // 
            // btnSaveAndRoute
            this.btnSaveAndRoute.Location = new System.Drawing.Point(740, 530);
            this.btnSaveAndRoute.Name = "btnSaveAndRoute";
            this.btnSaveAndRoute.Size = new System.Drawing.Size(120, 35);
            this.btnSaveAndRoute.TabIndex = 3;
            this.btnSaveAndRoute.Text = "Kaydet ve Yönlendir";
            this.btnSaveAndRoute.Click += new System.EventHandler(this.btnSaveAndRoute_Click);
            // 
            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(860, 530);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 35);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "İptal";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // TriageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 580);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveAndRoute);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.lblWorkItemId);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TriageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "İş Talebi Sınıflandırma";
            this.Load += new System.EventHandler(this.TriageForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtRequestedAt.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtRequestedAt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestedBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTargetBoard.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEffort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUrgency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbModule.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSprint.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

