using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit;

namespace work_tracker.Forms
{
    partial class MeetingForm
    {
        private System.ComponentModel.IContainer components = null;
        private PanelControl panelLeft;
        private PanelControl panelRight;
        private PanelControl panelTop;
        private SplitterControl splitterLeftRight;
        private GridControl gridControl1;
        private GridView gridView1;
        private GridControl gridControl2;
        private GridView gridView2;
        private TextEdit txtSubject;
        private DateEdit dtMeetingDate;
        private TextEdit txtParticipants;
        private RichEditControl richEditControl1;
        private SimpleButton btnNewMeeting;
        private SimpleButton btnEditMeeting;
        private SimpleButton btnDeleteMeeting;
        private SimpleButton btnRefresh;
        private SimpleButton btnViewDetails;
        private SimpleButton btnCreateWorkItem;
        private LabelControl labelControl1;
        private LabelControl labelControl2;
        private LabelControl labelControl3;
        private LabelControl labelControl4;
        private LabelControl labelControl5;
        private SimpleButton btnInlineEdit;
        private SimpleButton btnInlineSave;
        private SimpleButton btnInlineCancel;
        private SimpleButton btnFilterUpcoming;
        private SimpleButton btnFilterToday;
        private SimpleButton btnFilterPast;
        private SimpleButton btnFilterClear;
        // Yeni: Sağ panelde notlar ve iş listesi arasında daha iyi düzen için
        private PanelControl panelNotes;
        private SplitterControl splitterNotesGrid;

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
            this.panelTop = new DevExpress.XtraEditors.PanelControl();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.btnViewDetails = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteMeeting = new DevExpress.XtraEditors.SimpleButton();
            this.btnEditMeeting = new DevExpress.XtraEditors.SimpleButton();
            this.btnNewMeeting = new DevExpress.XtraEditors.SimpleButton();
            this.btnFilterUpcoming = new DevExpress.XtraEditors.SimpleButton();
            this.btnFilterToday = new DevExpress.XtraEditors.SimpleButton();
            this.btnFilterPast = new DevExpress.XtraEditors.SimpleButton();
            this.btnFilterClear = new DevExpress.XtraEditors.SimpleButton();
            this.panelLeft = new DevExpress.XtraEditors.PanelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelRight = new DevExpress.XtraEditors.PanelControl();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnCreateWorkItem = new DevExpress.XtraEditors.SimpleButton();
            this.richEditControl1 = new DevExpress.XtraRichEdit.RichEditControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnInlineEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnInlineSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnInlineCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtParticipants = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.dtMeetingDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtSubject = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.splitterLeftRight = new DevExpress.XtraEditors.SplitterControl();
            this.panelNotes = new DevExpress.XtraEditors.PanelControl();
            this.splitterNotesGrid = new DevExpress.XtraEditors.SplitterControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).BeginInit();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelLeft)).BeginInit();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelRight)).BeginInit();
            this.panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParticipants.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMeetingDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMeetingDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelNotes)).BeginInit();
            this.panelNotes.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Controls.Add(this.btnViewDetails);
            this.panelTop.Controls.Add(this.btnDeleteMeeting);
            this.panelTop.Controls.Add(this.btnEditMeeting);
            this.panelTop.Controls.Add(this.btnNewMeeting);
            this.panelTop.Controls.Add(this.btnFilterUpcoming);
            this.panelTop.Controls.Add(this.btnFilterToday);
            this.panelTop.Controls.Add(this.btnFilterPast);
            this.panelTop.Controls.Add(this.btnFilterClear);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1400, 50);
            this.panelTop.TabIndex = 0;
            // 
            // Button definitions
            this.btnNewMeeting.Location = new System.Drawing.Point(10, 10);
            this.btnNewMeeting.Name = "btnNewMeeting";
            this.btnNewMeeting.Size = new System.Drawing.Size(120, 30);
            this.btnNewMeeting.TabIndex = 0;
            this.btnNewMeeting.Text = "Yeni Toplantı";
            this.btnNewMeeting.Click += new System.EventHandler(this.btnNewMeeting_Click);
            
            this.btnEditMeeting.Location = new System.Drawing.Point(140, 10);
            this.btnEditMeeting.Name = "btnEditMeeting";
            this.btnEditMeeting.Size = new System.Drawing.Size(100, 30);
            this.btnEditMeeting.TabIndex = 1;
            this.btnEditMeeting.Text = "Düzenle";
            this.btnEditMeeting.Click += new System.EventHandler(this.btnEditMeeting_Click);
            
            this.btnDeleteMeeting.Location = new System.Drawing.Point(250, 10);
            this.btnDeleteMeeting.Name = "btnDeleteMeeting";
            this.btnDeleteMeeting.Size = new System.Drawing.Size(100, 30);
            this.btnDeleteMeeting.TabIndex = 2;
            this.btnDeleteMeeting.Text = "Sil";
            this.btnDeleteMeeting.Click += new System.EventHandler(this.btnDeleteMeeting_Click);
            
            this.btnViewDetails.Location = new System.Drawing.Point(360, 10);
            this.btnViewDetails.Name = "btnViewDetails";
            this.btnViewDetails.Size = new System.Drawing.Size(120, 30);
            this.btnViewDetails.TabIndex = 3;
            this.btnViewDetails.Text = "Detayları Göster";
            this.btnViewDetails.Click += new System.EventHandler(this.btnViewDetails_Click);
            
            this.btnRefresh.Location = new System.Drawing.Point(490, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Yenile";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnFilterUpcoming
            // 
            this.btnFilterUpcoming.Location = new System.Drawing.Point(600, 10);
            this.btnFilterUpcoming.Name = "btnFilterUpcoming";
            this.btnFilterUpcoming.Size = new System.Drawing.Size(110, 30);
            this.btnFilterUpcoming.TabIndex = 5;
            this.btnFilterUpcoming.Text = "Yaklaşan";
            this.btnFilterUpcoming.Click += new System.EventHandler(this.btnFilterUpcoming_Click);
            // 
            // btnFilterToday
            // 
            this.btnFilterToday.Location = new System.Drawing.Point(715, 10);
            this.btnFilterToday.Name = "btnFilterToday";
            this.btnFilterToday.Size = new System.Drawing.Size(90, 30);
            this.btnFilterToday.TabIndex = 6;
            this.btnFilterToday.Text = "Bugün";
            this.btnFilterToday.Click += new System.EventHandler(this.btnFilterToday_Click);
            // 
            // btnFilterPast
            // 
            this.btnFilterPast.Location = new System.Drawing.Point(810, 10);
            this.btnFilterPast.Name = "btnFilterPast";
            this.btnFilterPast.Size = new System.Drawing.Size(90, 30);
            this.btnFilterPast.TabIndex = 7;
            this.btnFilterPast.Text = "Geçmiş";
            this.btnFilterPast.Click += new System.EventHandler(this.btnFilterPast_Click);
            // 
            // btnFilterClear
            // 
            this.btnFilterClear.Location = new System.Drawing.Point(905, 10);
            this.btnFilterClear.Name = "btnFilterClear";
            this.btnFilterClear.Size = new System.Drawing.Size(110, 30);
            this.btnFilterClear.TabIndex = 8;
            this.btnFilterClear.Text = "Filtreyi Temizle";
            this.btnFilterClear.Click += new System.EventHandler(this.btnFilterClear_Click);
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.gridControl1);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 50);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(500, 650);
            this.panelLeft.TabIndex = 1;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 2);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(496, 646);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.gridControl2);
            this.panelRight.Controls.Add(this.labelControl5);
            this.panelRight.Controls.Add(this.splitterNotesGrid);
            this.panelRight.Controls.Add(this.panelNotes);
            this.panelRight.Controls.Add(this.btnInlineEdit);
            this.panelRight.Controls.Add(this.btnInlineSave);
            this.panelRight.Controls.Add(this.btnInlineCancel);
            this.panelRight.Controls.Add(this.txtParticipants);
            this.panelRight.Controls.Add(this.labelControl3);
            this.panelRight.Controls.Add(this.dtMeetingDate);
            this.panelRight.Controls.Add(this.labelControl2);
            this.panelRight.Controls.Add(this.txtSubject);
            this.panelRight.Controls.Add(this.labelControl1);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(503, 50);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(900, 650);
            this.panelRight.TabIndex = 2;
            // 
            // Right panel controls
            this.labelControl1.Location = new System.Drawing.Point(15, 15);
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(78, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Toplantı Konusu:";
            
            this.txtSubject.Enabled = false;
            this.txtSubject.Location = new System.Drawing.Point(15, 35);
            this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(870, 20);
            this.txtSubject.TabIndex = 1;
            // 
            // btnInlineEdit
            // 
            this.btnInlineEdit.Location = new System.Drawing.Point(690, 10);
            this.btnInlineEdit.Name = "btnInlineEdit";
            this.btnInlineEdit.Size = new System.Drawing.Size(60, 20);
            this.btnInlineEdit.TabIndex = 100;
            this.btnInlineEdit.Text = "Düzenle";
            this.btnInlineEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInlineEdit.Click += new System.EventHandler(this.btnInlineEdit_Click);
            // 
            // btnInlineSave
            // 
            this.btnInlineSave.Location = new System.Drawing.Point(755, 10);
            this.btnInlineSave.Name = "btnInlineSave";
            this.btnInlineSave.Size = new System.Drawing.Size(60, 20);
            this.btnInlineSave.TabIndex = 101;
            this.btnInlineSave.Text = "Kaydet";
            this.btnInlineSave.Enabled = false;
            this.btnInlineSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInlineSave.Click += new System.EventHandler(this.btnInlineSave_Click);
            // 
            // btnInlineCancel
            // 
            this.btnInlineCancel.Location = new System.Drawing.Point(820, 10);
            this.btnInlineCancel.Name = "btnInlineCancel";
            this.btnInlineCancel.Size = new System.Drawing.Size(60, 20);
            this.btnInlineCancel.TabIndex = 102;
            this.btnInlineCancel.Text = "İptal";
            this.btnInlineCancel.Enabled = false;
            this.btnInlineCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInlineCancel.Click += new System.EventHandler(this.btnInlineCancel_Click);
            
            this.labelControl2.Location = new System.Drawing.Point(15, 65);
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(76, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Toplantı Tarihi:";
            
            this.dtMeetingDate.EditValue = null;
            this.dtMeetingDate.Enabled = false;
            this.dtMeetingDate.Location = new System.Drawing.Point(15, 85);
            this.dtMeetingDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtMeetingDate.Name = "dtMeetingDate";
            this.dtMeetingDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtMeetingDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtMeetingDate.Size = new System.Drawing.Size(870, 20);
            this.dtMeetingDate.TabIndex = 3;
            
            this.labelControl3.Location = new System.Drawing.Point(15, 115);
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(55, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Katılımcılar:";
            
            this.txtParticipants.Enabled = false;
            this.txtParticipants.Location = new System.Drawing.Point(15, 135);
            this.txtParticipants.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtParticipants.Name = "txtParticipants";
            this.txtParticipants.Size = new System.Drawing.Size(870, 20);
            this.txtParticipants.TabIndex = 5;

            // panelNotes (Toplantı Notları alanı)
            this.panelNotes.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNotes.Location = new System.Drawing.Point(2, 160);
            this.panelNotes.Name = "panelNotes";
            this.panelNotes.Size = new System.Drawing.Size(896, 300);
            this.panelNotes.TabIndex = 200;
            // labelControl4
            this.labelControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControl4.Location = new System.Drawing.Point(2, 2);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Padding = new System.Windows.Forms.Padding(13, 5, 0, 5);
            this.labelControl4.Size = new System.Drawing.Size(97, 23);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "Toplantı Notları";
            // btnCreateWorkItem
            this.btnCreateWorkItem.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCreateWorkItem.Location = new System.Drawing.Point(2, 25);
            this.btnCreateWorkItem.Name = "btnCreateWorkItem";
            this.btnCreateWorkItem.Size = new System.Drawing.Size(892, 30);
            this.btnCreateWorkItem.TabIndex = 8;
            this.btnCreateWorkItem.Text = "İş Talebine Dönüştür (Seçili Metin)";
            this.btnCreateWorkItem.Click += new System.EventHandler(this.btnCreateWorkItem_Click);
            // richEditControl1
            this.richEditControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richEditControl1.Location = new System.Drawing.Point(2, 55);
            this.richEditControl1.Name = "richEditControl1";
            this.richEditControl1.ReadOnly = true;
            this.richEditControl1.Options.DocumentCapabilities.CharacterFormatting = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.Options.DocumentCapabilities.CharacterStyle = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.Options.DocumentCapabilities.InlinePictures = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.Options.DocumentCapabilities.Hyperlinks = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.Options.DocumentCapabilities.Tables = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.Options.DocumentCapabilities.Paragraphs = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.Options.DocumentCapabilities.ParagraphFormatting = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditControl1.Size = new System.Drawing.Size(892, 243);
            this.richEditControl1.TabIndex = 7;
            // panelNotes children
            this.panelNotes.Controls.Add(this.richEditControl1);
            this.panelNotes.Controls.Add(this.btnCreateWorkItem);
            this.panelNotes.Controls.Add(this.labelControl4);

            // splitterNotesGrid (Notlar ve Grid arasında)
            this.splitterNotesGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterNotesGrid.Location = new System.Drawing.Point(2, 460);
            this.splitterNotesGrid.Name = "splitterNotesGrid";
            this.splitterNotesGrid.Size = new System.Drawing.Size(896, 5);
            this.splitterNotesGrid.TabIndex = 201;
            
            this.labelControl5.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControl5.Location = new System.Drawing.Point(2, 465);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Padding = new System.Windows.Forms.Padding(13, 5, 0, 5);
            this.labelControl5.Size = new System.Drawing.Size(170, 23);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "Bu Toplantıdan Gelen İş Talepleri:";
            
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(2, 488);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(896, 160);
            this.gridControl2.TabIndex = 10;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;

            // splitterLeftRight (Sol/sağ arası)
            this.splitterLeftRight.Dock = System.Windows.Forms.DockStyle.Left;
            this.splitterLeftRight.Location = new System.Drawing.Point(500, 50);
            this.splitterLeftRight.Name = "splitterLeftRight";
            this.splitterLeftRight.Size = new System.Drawing.Size(3, 650);
            this.splitterLeftRight.TabIndex = 3;
            // 
            // MeetingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 700);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.splitterLeftRight);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelTop);
            this.Name = "MeetingForm";
            this.Text = "Toplantı Yönetimi";
            this.Load += new System.EventHandler(this.MeetingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).EndInit();
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelLeft)).EndInit();
            this.panelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelRight)).EndInit();
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParticipants.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMeetingDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMeetingDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelNotes)).EndInit();
            this.panelNotes.ResumeLayout(false);
            this.panelNotes.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}

