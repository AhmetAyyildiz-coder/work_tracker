namespace work_tracker.Forms
{
    partial class TimeEntryEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelMain = new DevExpress.XtraEditors.PanelControl();
            this.panelButtons = new DevExpress.XtraEditors.PanelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            
            // Row 1: Date, Duration, Activity Type
            this.lblEntryDate = new DevExpress.XtraEditors.LabelControl();
            this.dtEntryDate = new DevExpress.XtraEditors.DateEdit();
            this.lblDurationMinutes = new DevExpress.XtraEditors.LabelControl();
            this.spinDurationMinutes = new DevExpress.XtraEditors.SpinEdit();
            this.lblActivityType = new DevExpress.XtraEditors.LabelControl();
            this.cmbActivityType = new DevExpress.XtraEditors.ComboBoxEdit();
            
            // Row 2: Subject (prominent)
            this.lblSubject = new DevExpress.XtraEditors.LabelControl();
            this.txtSubject = new DevExpress.XtraEditors.TextEdit();
            
            // Row 3: Project, WorkItem
            this.lblProject = new DevExpress.XtraEditors.LabelControl();
            this.cmbProject = new DevExpress.XtraEditors.LookUpEdit();
            this.lblWorkItem = new DevExpress.XtraEditors.LabelControl();
            this.cmbWorkItem = new DevExpress.XtraEditors.LookUpEdit();
            
            // Row 4: Person, Phone
            this.lblContactName = new DevExpress.XtraEditors.LabelControl();
            this.cmbPerson = new DevExpress.XtraEditors.LookUpEdit();
            this.btnAddPerson = new DevExpress.XtraEditors.SimpleButton();
            this.lblPhoneNumber = new DevExpress.XtraEditors.LabelControl();
            this.txtPhoneNumber = new DevExpress.XtraEditors.TextEdit();
            
            // Row 5: Description
            this.lblDescription = new DevExpress.XtraEditors.LabelControl();
            this.memDescription = new DevExpress.XtraEditors.MemoEdit();
            
            ((System.ComponentModel.ISupportInitialize)(this.panelMain)).BeginInit();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelButtons)).BeginInit();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtEntryDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEntryDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDurationMinutes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbActivityType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWorkItem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPerson.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhoneNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memDescription.Properties)).BeginInit();
            this.SuspendLayout();
            
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(12);
            this.panelMain.Size = new System.Drawing.Size(650, 480);
            this.panelMain.TabIndex = 0;
            this.panelMain.Controls.Add(this.lblEntryDate);
            this.panelMain.Controls.Add(this.dtEntryDate);
            this.panelMain.Controls.Add(this.lblDurationMinutes);
            this.panelMain.Controls.Add(this.spinDurationMinutes);
            this.panelMain.Controls.Add(this.lblActivityType);
            this.panelMain.Controls.Add(this.cmbActivityType);
            this.panelMain.Controls.Add(this.lblSubject);
            this.panelMain.Controls.Add(this.txtSubject);
            this.panelMain.Controls.Add(this.lblProject);
            this.panelMain.Controls.Add(this.cmbProject);
            this.panelMain.Controls.Add(this.lblWorkItem);
            this.panelMain.Controls.Add(this.cmbWorkItem);
            this.panelMain.Controls.Add(this.lblContactName);
            this.panelMain.Controls.Add(this.cmbPerson);
            this.panelMain.Controls.Add(this.btnAddPerson);
            this.panelMain.Controls.Add(this.lblPhoneNumber);
            this.panelMain.Controls.Add(this.txtPhoneNumber);
            this.panelMain.Controls.Add(this.lblDescription);
            this.panelMain.Controls.Add(this.memDescription);
            this.panelMain.Controls.Add(this.panelButtons);
            
            // 
            // panelButtons
            // 
            this.panelButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panelButtons.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelButtons.Location = new System.Drawing.Point(440, 430);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(190, 35);
            this.panelButtons.TabIndex = 20;
            this.panelButtons.Controls.Add(this.btnSave);
            this.panelButtons.Controls.Add(this.btnCancel);
            
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(5, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(85, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "💾 Kaydet";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(100, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "❌ İptal";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            
            // ============ ROW 1: Date, Duration, Activity Type ============
            // 
            // lblEntryDate
            // 
            this.lblEntryDate.Location = new System.Drawing.Point(15, 20);
            this.lblEntryDate.Name = "lblEntryDate";
            this.lblEntryDate.Size = new System.Drawing.Size(70, 13);
            this.lblEntryDate.TabIndex = 1;
            this.lblEntryDate.Text = "Tarih:";
            
            // 
            // dtEntryDate
            // 
            this.dtEntryDate.EditValue = null;
            this.dtEntryDate.Location = new System.Drawing.Point(15, 38);
            this.dtEntryDate.Name = "dtEntryDate";
            this.dtEntryDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEntryDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEntryDate.Properties.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
            this.dtEntryDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtEntryDate.Properties.EditFormat.FormatString = "dd.MM.yyyy HH:mm";
            this.dtEntryDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtEntryDate.Properties.Mask.EditMask = "dd.MM.yyyy HH:mm";
            this.dtEntryDate.Size = new System.Drawing.Size(180, 20);
            this.dtEntryDate.TabIndex = 2;
            
            // 
            // lblDurationMinutes
            // 
            this.lblDurationMinutes.Location = new System.Drawing.Point(210, 20);
            this.lblDurationMinutes.Name = "lblDurationMinutes";
            this.lblDurationMinutes.Size = new System.Drawing.Size(80, 13);
            this.lblDurationMinutes.TabIndex = 3;
            this.lblDurationMinutes.Text = "Süre (Dakika):";
            
            // 
            // spinDurationMinutes
            // 
            this.spinDurationMinutes.EditValue = new decimal(new int[] { 30, 0, 0, 0 });
            this.spinDurationMinutes.Location = new System.Drawing.Point(210, 38);
            this.spinDurationMinutes.Name = "spinDurationMinutes";
            this.spinDurationMinutes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinDurationMinutes.Properties.MaxValue = new decimal(new int[] { 1440, 0, 0, 0 });
            this.spinDurationMinutes.Properties.MinValue = new decimal(new int[] { 1, 0, 0, 0 });
            this.spinDurationMinutes.Size = new System.Drawing.Size(100, 20);
            this.spinDurationMinutes.TabIndex = 4;
            
            // 
            // lblActivityType
            // 
            this.lblActivityType.Location = new System.Drawing.Point(330, 20);
            this.lblActivityType.Name = "lblActivityType";
            this.lblActivityType.Size = new System.Drawing.Size(70, 13);
            this.lblActivityType.TabIndex = 5;
            this.lblActivityType.Text = "Aktivite Tipi:";
            
            // 
            // cmbActivityType
            // 
            this.cmbActivityType.Location = new System.Drawing.Point(330, 38);
            this.cmbActivityType.Name = "cmbActivityType";
            this.cmbActivityType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbActivityType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbActivityType.Size = new System.Drawing.Size(200, 20);
            this.cmbActivityType.TabIndex = 6;
            this.cmbActivityType.SelectedIndexChanged += new System.EventHandler(this.cmbActivityType_SelectedIndexChanged);
            
            // ============ ROW 2: Subject (PROMINENT - full width) ============
            // 
            // lblSubject
            // 
            this.lblSubject.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblSubject.Appearance.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblSubject.Appearance.Options.UseFont = true;
            this.lblSubject.Appearance.Options.UseForeColor = true;
            this.lblSubject.Location = new System.Drawing.Point(15, 75);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(100, 13);
            this.lblSubject.TabIndex = 7;
            this.lblSubject.Text = "📝 KONU:";
            
            // 
            // txtSubject
            // 
            this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubject.Location = new System.Drawing.Point(15, 93);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular);
            this.txtSubject.Properties.Appearance.Options.UseFont = true;
            this.txtSubject.Properties.MaxLength = 200;
            this.txtSubject.Properties.NullValuePrompt = "Zaman kaydının konusunu girin...";
            this.txtSubject.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtSubject.Size = new System.Drawing.Size(615, 22);
            this.txtSubject.TabIndex = 8;
            
            // ============ ROW 3: Project, WorkItem ============
            // 
            // lblProject
            // 
            this.lblProject.Location = new System.Drawing.Point(15, 130);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(35, 13);
            this.lblProject.TabIndex = 9;
            this.lblProject.Text = "Proje:";
            
            // 
            // cmbProject
            // 
            this.cmbProject.Location = new System.Drawing.Point(15, 148);
            this.cmbProject.Name = "cmbProject";
            this.cmbProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbProject.Properties.NullText = "(Proje Seçiniz)";
            this.cmbProject.Size = new System.Drawing.Size(300, 20);
            this.cmbProject.TabIndex = 10;
            
            // 
            // lblWorkItem
            // 
            this.lblWorkItem.Location = new System.Drawing.Point(330, 130);
            this.lblWorkItem.Name = "lblWorkItem";
            this.lblWorkItem.Size = new System.Drawing.Size(52, 13);
            this.lblWorkItem.TabIndex = 11;
            this.lblWorkItem.Text = "İş Öğesi:";
            
            // 
            // cmbWorkItem
            // 
            this.cmbWorkItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWorkItem.Location = new System.Drawing.Point(330, 148);
            this.cmbWorkItem.Name = "cmbWorkItem";
            this.cmbWorkItem.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWorkItem.Properties.NullText = "(İş Öğesi Seçiniz)";
            this.cmbWorkItem.Size = new System.Drawing.Size(300, 20);
            this.cmbWorkItem.TabIndex = 12;
            this.cmbWorkItem.EditValueChanged += new System.EventHandler(this.cmbWorkItem_EditValueChanged);
            
            // ============ ROW 4: Person, Phone ============
            // 
            // lblContactName
            // 
            this.lblContactName.Location = new System.Drawing.Point(15, 185);
            this.lblContactName.Name = "lblContactName";
            this.lblContactName.Size = new System.Drawing.Size(31, 13);
            this.lblContactName.TabIndex = 13;
            this.lblContactName.Text = "Kişi:";
            
            // 
            // cmbPerson
            // 
            this.cmbPerson.Location = new System.Drawing.Point(15, 203);
            this.cmbPerson.Name = "cmbPerson";
            this.cmbPerson.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPerson.Properties.NullText = "(Kişi Seçiniz - Opsiyonel)";
            this.cmbPerson.Size = new System.Drawing.Size(265, 20);
            this.cmbPerson.TabIndex = 14;
            this.cmbPerson.EditValueChanged += new System.EventHandler(this.cmbPerson_EditValueChanged);
            
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.Location = new System.Drawing.Point(285, 203);
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.Size = new System.Drawing.Size(30, 20);
            this.btnAddPerson.TabIndex = 15;
            this.btnAddPerson.Text = "+";
            this.btnAddPerson.ToolTip = "Yeni kişi ekle";
            this.btnAddPerson.Click += new System.EventHandler(this.btnAddPerson_Click);
            
            // 
            // lblPhoneNumber
            // 
            this.lblPhoneNumber.Location = new System.Drawing.Point(330, 185);
            this.lblPhoneNumber.Name = "lblPhoneNumber";
            this.lblPhoneNumber.Size = new System.Drawing.Size(100, 13);
            this.lblPhoneNumber.TabIndex = 16;
            this.lblPhoneNumber.Text = "Telefon Numarası:";
            
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPhoneNumber.Location = new System.Drawing.Point(330, 203);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Properties.NullValuePrompt = "(Opsiyonel)";
            this.txtPhoneNumber.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtPhoneNumber.Size = new System.Drawing.Size(300, 20);
            this.txtPhoneNumber.TabIndex = 17;
            
            // ============ ROW 5: Description ============
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(15, 240);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(52, 13);
            this.lblDescription.TabIndex = 18;
            this.lblDescription.Text = "Açıklama:";
            
            // 
            // memDescription
            // 
            this.memDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.memDescription.Location = new System.Drawing.Point(15, 258);
            this.memDescription.Name = "memDescription";
            this.memDescription.Properties.NullValuePrompt = "Detaylı açıklama girin (opsiyonel)...";
            this.memDescription.Properties.NullValuePromptShowForEmptyValue = true;
            this.memDescription.Size = new System.Drawing.Size(615, 160);
            this.memDescription.TabIndex = 19;
            
            // 
            // TimeEntryEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 480);
            this.Controls.Add(this.panelMain);
            this.MinimumSize = new System.Drawing.Size(600, 450);
            this.Name = "TimeEntryEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "⏱️ Zaman Kaydı";
            this.Load += new System.EventHandler(this.TimeEntryEditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelMain)).EndInit();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelButtons)).EndInit();
            this.panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtEntryDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEntryDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDurationMinutes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbActivityType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWorkItem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPerson.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhoneNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memDescription.Properties)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelMain;
        private DevExpress.XtraEditors.PanelControl panelButtons;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl lblEntryDate;
        private DevExpress.XtraEditors.DateEdit dtEntryDate;
        private DevExpress.XtraEditors.LabelControl lblDurationMinutes;
        private DevExpress.XtraEditors.SpinEdit spinDurationMinutes;
        private DevExpress.XtraEditors.LabelControl lblActivityType;
        private DevExpress.XtraEditors.ComboBoxEdit cmbActivityType;
        private DevExpress.XtraEditors.LabelControl lblSubject;
        private DevExpress.XtraEditors.TextEdit txtSubject;
        private DevExpress.XtraEditors.LabelControl lblProject;
        private DevExpress.XtraEditors.LookUpEdit cmbProject;
        private DevExpress.XtraEditors.LabelControl lblWorkItem;
        private DevExpress.XtraEditors.LookUpEdit cmbWorkItem;
        private DevExpress.XtraEditors.LabelControl lblContactName;
        private DevExpress.XtraEditors.LookUpEdit cmbPerson;
        private DevExpress.XtraEditors.SimpleButton btnAddPerson;
        private DevExpress.XtraEditors.LabelControl lblPhoneNumber;
        private DevExpress.XtraEditors.TextEdit txtPhoneNumber;
        private DevExpress.XtraEditors.LabelControl lblDescription;
        private DevExpress.XtraEditors.MemoEdit memDescription;
    }
}
