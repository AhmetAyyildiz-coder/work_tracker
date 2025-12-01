using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;

namespace work_tracker.Forms
{
    partial class MeetingEditForm
    {
        private System.ComponentModel.IContainer components = null;
        private LabelControl labelControl1;
        private LabelControl labelControl2;
        private LabelControl labelControl3;
        private LabelControl labelControl4;
        private TextEdit txtSubject;
        private DateEdit dtMeetingDate;
        private TextEdit txtParticipants;
        private RichEditControl richEditControl1;
        private SimpleButton btnSave;
        private SimpleButton btnCancel;
        private PanelControl panelNotesToolbar;
        private SimpleButton btnNoteNumberedList;
        private SimpleButton btnNoteBulletList;
        private SimpleButton btnNoteItalic;
        private SimpleButton btnNoteBold;
        private SimpleButton btnH1;
        private SimpleButton btnH2;
        private SimpleButton btnH3;

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
            this.components = new System.ComponentModel.Container();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtSubject = new DevExpress.XtraEditors.TextEdit();
            this.dtMeetingDate = new DevExpress.XtraEditors.DateEdit();
            this.txtParticipants = new DevExpress.XtraEditors.TextEdit();
            this.richEditControl1 = new DevExpress.XtraRichEdit.RichEditControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelNotesToolbar = new DevExpress.XtraEditors.PanelControl();
            this.btnNoteNumberedList = new DevExpress.XtraEditors.SimpleButton();
            this.btnNoteBulletList = new DevExpress.XtraEditors.SimpleButton();
            this.btnNoteItalic = new DevExpress.XtraEditors.SimpleButton();
            this.btnNoteBold = new DevExpress.XtraEditors.SimpleButton();
            this.btnH1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnH2 = new DevExpress.XtraEditors.SimpleButton();
            this.btnH3 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMeetingDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMeetingDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParticipants.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelNotesToolbar)).BeginInit();
            this.panelNotesToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(20, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(78, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Toplantı Konusu:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(430, 20);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(76, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Toplantı Tarihi:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(770, 20);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(55, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Katılımcılar:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(20, 70);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(78, 13);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Toplantı Notları:";
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(20, 40);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(400, 20);
            this.txtSubject.TabIndex = 4;
            // 
            // dtMeetingDate
            // 
            this.dtMeetingDate.EditValue = null;
            this.dtMeetingDate.Location = new System.Drawing.Point(430, 40);
            this.dtMeetingDate.Name = "dtMeetingDate";
            this.dtMeetingDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtMeetingDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtMeetingDate.Size = new System.Drawing.Size(330, 20);
            this.dtMeetingDate.TabIndex = 5;
            // 
            // txtParticipants
            // 
            this.txtParticipants.Location = new System.Drawing.Point(770, 40);
            this.txtParticipants.Name = "txtParticipants";
            this.txtParticipants.Size = new System.Drawing.Size(580, 20);
            this.txtParticipants.TabIndex = 6;
            // 
            // richEditControl1
            // 
            this.richEditControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richEditControl1.Location = new System.Drawing.Point(20, 165);
            this.richEditControl1.Name = "richEditControl1";
            this.richEditControl1.Options.DocumentCapabilities.CharacterFormatting = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.Options.DocumentCapabilities.CharacterStyle = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.Options.DocumentCapabilities.InlinePictures = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.Options.DocumentCapabilities.Hyperlinks = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.Options.DocumentCapabilities.Tables = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.Options.DocumentCapabilities.Paragraphs = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.Options.DocumentCapabilities.ParagraphFormatting = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.Options.DocumentCapabilities.Bookmarks = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.Options.DocumentCapabilities.Numbering.Bulleted = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.Options.DocumentCapabilities.Numbering.Simple = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.Options.DocumentCapabilities.Numbering.MultiLevel = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.Options.DocumentCapabilities.HeadersFooters = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.Options.DocumentCapabilities.Tables = DevExpress.XtraRichEdit.DocumentCapability.Enabled; 
            this.richEditControl1.Size = new System.Drawing.Size(1330, 540);
            this.richEditControl1.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Location = new System.Drawing.Point(1150, 720);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "💾 Kaydet";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(1260, 720);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "❌ İptal";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelNotesToolbar
            // 
            this.panelNotesToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelNotesToolbar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelNotesToolbar.Controls.Add(this.btnH3);
            this.panelNotesToolbar.Controls.Add(this.btnH2);
            this.panelNotesToolbar.Controls.Add(this.btnH1);
            this.panelNotesToolbar.Controls.Add(this.btnNoteNumberedList);
            this.panelNotesToolbar.Controls.Add(this.btnNoteBulletList);
            this.panelNotesToolbar.Controls.Add(this.btnNoteItalic);
            this.panelNotesToolbar.Controls.Add(this.btnNoteBold);
            this.panelNotesToolbar.Location = new System.Drawing.Point(20, 125);
            this.panelNotesToolbar.Name = "panelNotesToolbar";
            this.panelNotesToolbar.Size = new System.Drawing.Size(1330, 30);
            this.panelNotesToolbar.TabIndex = 10;
            // 
            // btnNoteNumberedList
            // 
            this.btnNoteNumberedList.Location = new System.Drawing.Point(150, 3);
            this.btnNoteNumberedList.Name = "btnNoteNumberedList";
            this.btnNoteNumberedList.Size = new System.Drawing.Size(50, 24);
            this.btnNoteNumberedList.TabIndex = 3;
            this.btnNoteNumberedList.Text = "1. List";
            this.btnNoteNumberedList.Click += new System.EventHandler(this.btnNoteNumberedList_Click);
            // 
            // btnNoteBulletList
            // 
            this.btnNoteBulletList.Location = new System.Drawing.Point(100, 3);
            this.btnNoteBulletList.Name = "btnNoteBulletList";
            this.btnNoteBulletList.Size = new System.Drawing.Size(44, 24);
            this.btnNoteBulletList.TabIndex = 2;
            this.btnNoteBulletList.Text = "• List";
            this.btnNoteBulletList.Click += new System.EventHandler(this.btnNoteBulletList_Click);
            // 
            // btnNoteItalic
            // 
            this.btnNoteItalic.Location = new System.Drawing.Point(54, 3);
            this.btnNoteItalic.Name = "btnNoteItalic";
            this.btnNoteItalic.Size = new System.Drawing.Size(40, 24);
            this.btnNoteItalic.TabIndex = 1;
            this.btnNoteItalic.Text = "I";
            this.btnNoteItalic.Click += new System.EventHandler(this.btnNoteItalic_Click);
            // 
            // btnNoteBold
            // 
            this.btnNoteBold.Location = new System.Drawing.Point(8, 3);
            this.btnNoteBold.Name = "btnNoteBold";
            this.btnNoteBold.Size = new System.Drawing.Size(40, 24);
            this.btnNoteBold.TabIndex = 0;
            this.btnNoteBold.Text = "B";
            this.btnNoteBold.Click += new System.EventHandler(this.btnNoteBold_Click);
            // 
            // btnH1
            // 
            this.btnH1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnH1.Appearance.Options.UseFont = true;
            this.btnH1.Location = new System.Drawing.Point(220, 3);
            this.btnH1.Name = "btnH1";
            this.btnH1.Size = new System.Drawing.Size(36, 24);
            this.btnH1.TabIndex = 4;
            this.btnH1.Text = "H1";
            this.btnH1.ToolTip = "Büyük Başlık";
            this.btnH1.Click += new System.EventHandler(this.btnH1_Click);
            // 
            // btnH2
            // 
            this.btnH2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnH2.Appearance.Options.UseFont = true;
            this.btnH2.Location = new System.Drawing.Point(262, 3);
            this.btnH2.Name = "btnH2";
            this.btnH2.Size = new System.Drawing.Size(36, 24);
            this.btnH2.TabIndex = 5;
            this.btnH2.Text = "H2";
            this.btnH2.ToolTip = "Orta Başlık";
            this.btnH2.Click += new System.EventHandler(this.btnH2_Click);
            // 
            // btnH3
            // 
            this.btnH3.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnH3.Appearance.Options.UseFont = true;
            this.btnH3.Location = new System.Drawing.Point(304, 3);
            this.btnH3.Name = "btnH3";
            this.btnH3.Size = new System.Drawing.Size(36, 24);
            this.btnH3.TabIndex = 6;
            this.btnH3.Text = "H3";
            this.btnH3.ToolTip = "Küçük Başlık";
            this.btnH3.Click += new System.EventHandler(this.btnH3_Click);
            // 
            // MeetingEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 775);
            this.Controls.Add(this.panelNotesToolbar);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.richEditControl1);
            this.Controls.Add(this.txtParticipants);
            this.Controls.Add(this.dtMeetingDate);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.Name = "MeetingEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Toplantı Düzenle";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MeetingEditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMeetingDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMeetingDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParticipants.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelNotesToolbar)).EndInit();
            this.panelNotesToolbar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

