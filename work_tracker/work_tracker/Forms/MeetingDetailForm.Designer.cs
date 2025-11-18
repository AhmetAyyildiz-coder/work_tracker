using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit;

namespace work_tracker.Forms
{
    partial class MeetingDetailForm
    {
        private System.ComponentModel.IContainer components = null;
        private GroupControl groupMeetingInfo;
        private GroupControl groupNotes;
        private GroupControl groupWorkItems;
        private LabelControl labelControl1;
        private LabelControl labelControl2;
        private LabelControl labelControl3;
        private LabelControl lblWorkItemCount;
        private TextEdit txtSubject;
        private DateEdit dtMeetingDate;
        private TextEdit txtParticipants;
        private RichEditControl richEditControl1;
        private GridControl gridControl1;
        private GridView gridView1;
        private SimpleButton btnCreateWorkItem;
        private SimpleButton btnClose;
        private PanelControl panelBottom;
        private SplitContainerControl splitContainerMain;
        private SimpleButton btnEdit;
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
            this.groupMeetingInfo = new DevExpress.XtraEditors.GroupControl();
            this.txtParticipants = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.dtMeetingDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtSubject = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupNotes = new DevExpress.XtraEditors.GroupControl();
            this.btnCreateWorkItem = new DevExpress.XtraEditors.SimpleButton();
            this.richEditControl1 = new DevExpress.XtraRichEdit.RichEditControl();
            this.groupWorkItems = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblWorkItemCount = new DevExpress.XtraEditors.LabelControl();
            this.panelBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerMain = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupMeetingInfo)).BeginInit();
            this.groupMeetingInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtParticipants.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMeetingDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMeetingDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupNotes)).BeginInit();
            this.groupNotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupWorkItems)).BeginInit();
            this.groupWorkItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).BeginInit();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain.Panel1)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain.Panel2)).BeginInit();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupMeetingInfo
            // 
            this.groupMeetingInfo.Controls.Add(this.txtParticipants);
            this.groupMeetingInfo.Controls.Add(this.labelControl3);
            this.groupMeetingInfo.Controls.Add(this.dtMeetingDate);
            this.groupMeetingInfo.Controls.Add(this.labelControl2);
            this.groupMeetingInfo.Controls.Add(this.txtSubject);
            this.groupMeetingInfo.Controls.Add(this.labelControl1);
            this.groupMeetingInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupMeetingInfo.Location = new System.Drawing.Point(0, 0);
            this.groupMeetingInfo.Name = "groupMeetingInfo";
            this.groupMeetingInfo.Size = new System.Drawing.Size(550, 120);
            this.groupMeetingInfo.TabIndex = 0;
            this.groupMeetingInfo.Text = "Toplantı Bilgileri";
            // 
            // txtParticipants
            // 
            this.txtParticipants.Enabled = false;
            this.txtParticipants.Location = new System.Drawing.Point(400, 62);
            this.txtParticipants.Name = "txtParticipants";
            this.txtParticipants.Size = new System.Drawing.Size(180, 20);
            this.txtParticipants.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(330, 65);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(53, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Katılımcılar:";
            // 
            // dtMeetingDate
            // 
            this.dtMeetingDate.EditValue = null;
            this.dtMeetingDate.Enabled = false;
            this.dtMeetingDate.Location = new System.Drawing.Point(120, 62);
            this.dtMeetingDate.Name = "dtMeetingDate";
            this.dtMeetingDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtMeetingDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtMeetingDate.Size = new System.Drawing.Size(200, 20);
            this.dtMeetingDate.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(15, 65);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(71, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Toplantı Tarihi:";
            // 
            // txtSubject
            // 
            this.txtSubject.Enabled = false;
            this.txtSubject.Location = new System.Drawing.Point(120, 32);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(460, 20);
            this.txtSubject.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 35);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Toplantı Konusu:";
            // 
            // groupNotes
            // 
            this.groupNotes.Controls.Add(this.btnCreateWorkItem);
            this.groupNotes.Controls.Add(this.richEditControl1);
            this.groupNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupNotes.Location = new System.Drawing.Point(0, 0);
            this.groupNotes.Name = "groupNotes";
            this.groupNotes.Size = new System.Drawing.Size(840, 720);
            this.groupNotes.TabIndex = 1;
            this.groupNotes.Text = "Toplantı Notları";
            // 
            // btnCreateWorkItem
            // 
            this.btnCreateWorkItem.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCreateWorkItem.Location = new System.Drawing.Point(2, 668);
            this.btnCreateWorkItem.Name = "btnCreateWorkItem";
            this.btnCreateWorkItem.Size = new System.Drawing.Size(836, 50);
            this.btnCreateWorkItem.TabIndex = 1;
            this.btnCreateWorkItem.Text = "📋 Seçili Metni İş Talebine Dönüştür (Not: Önce metin seçin)";
            this.btnCreateWorkItem.Click += new System.EventHandler(this.btnCreateWorkItem_Click);
            // 
            // richEditControl1
            // 
            this.richEditControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richEditControl1.Location = new System.Drawing.Point(2, 23);
            this.richEditControl1.Name = "richEditControl1";
            this.richEditControl1.Options.DocumentCapabilities.CharacterFormatting = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.Options.DocumentCapabilities.CharacterStyle = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.Options.DocumentCapabilities.Hyperlinks = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.Options.DocumentCapabilities.InlinePictures = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.Options.DocumentCapabilities.ParagraphFormatting = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.Options.DocumentCapabilities.Paragraphs = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.Options.DocumentCapabilities.Tables = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.ReadOnly = true;
            this.richEditControl1.Size = new System.Drawing.Size(836, 695);
            this.richEditControl1.TabIndex = 0;
            // 
            // groupWorkItems
            // 
            this.groupWorkItems.Controls.Add(this.gridControl1);
            this.groupWorkItems.Controls.Add(this.lblWorkItemCount);
            this.groupWorkItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupWorkItems.Location = new System.Drawing.Point(0, 120);
            this.groupWorkItems.Name = "groupWorkItems";
            this.groupWorkItems.Size = new System.Drawing.Size(550, 600);
            this.groupWorkItems.TabIndex = 2;
            this.groupWorkItems.Text = "Bu Toplantıdan Oluşturulan İş Talepleri";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 47);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(546, 551);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            // 
            // lblWorkItemCount
            // 
            this.lblWorkItemCount.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblWorkItemCount.Appearance.Options.UseFont = true;
            this.lblWorkItemCount.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblWorkItemCount.Location = new System.Drawing.Point(2, 23);
            this.lblWorkItemCount.Name = "lblWorkItemCount";
            this.lblWorkItemCount.Padding = new System.Windows.Forms.Padding(10, 5, 0, 5);
            this.lblWorkItemCount.Size = new System.Drawing.Size(240, 24);
            this.lblWorkItemCount.TabIndex = 0;
            this.lblWorkItemCount.Text = "Bu toplantıdan 0 iş talebi oluşturuldu";
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Controls.Add(this.btnSave);
            this.panelBottom.Controls.Add(this.btnEdit);
            this.panelBottom.Controls.Add(this.btnClose);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 720);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1400, 50);
            this.panelBottom.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(204, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "İptal";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(108, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Kaydet";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(12, 10);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(90, 30);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Düzenle";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.Location = new System.Drawing.Point(1290, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Kapat";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.groupWorkItems);
            this.splitContainerMain.Panel1.Controls.Add(this.groupMeetingInfo);
            this.splitContainerMain.Panel1.Text = "Panel1";
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.groupNotes);
            this.splitContainerMain.Panel2.Text = "Panel2";
            this.splitContainerMain.Size = new System.Drawing.Size(1400, 720);
            this.splitContainerMain.SplitterPosition = 550;
            this.splitContainerMain.TabIndex = 4;
            // 
            // MeetingDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 770);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.panelBottom);
            this.Name = "MeetingDetailForm";
            this.Text = "Toplantı Detayı";
            this.Load += new System.EventHandler(this.MeetingDetailForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupMeetingInfo)).EndInit();
            this.groupMeetingInfo.ResumeLayout(false);
            this.groupMeetingInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtParticipants.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMeetingDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMeetingDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupNotes)).EndInit();
            this.groupNotes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupWorkItems)).EndInit();
            this.groupWorkItems.ResumeLayout(false);
            this.groupWorkItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).EndInit();
            this.panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain.Panel1)).EndInit();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain.Panel2)).EndInit();
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}

