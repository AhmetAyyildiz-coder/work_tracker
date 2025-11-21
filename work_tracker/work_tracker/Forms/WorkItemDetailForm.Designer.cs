using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.Drawing;

namespace work_tracker.Forms
{
    partial class WorkItemDetailForm
    {
        private System.ComponentModel.IContainer components = null;
        
        // Üst panel - İş bilgileri
        private GroupControl groupWorkItemInfo;
        private LabelControl lblTitle;
        private LabelControl lblDescription;
        private LabelControl lblRequestedBy;
        private LabelControl lblRequestedAt;
        private LabelControl lblStatus;
        private LabelControl lblType;
        private LabelControl lblUrgency;
        private LabelControl lblProject;
        private LabelControl lblModule;
        private LabelControl lblSprint;
        private LabelControl lblBoard;
        private LabelControl lblEffort;
        private LabelControl lblCreatedAt;
        private LabelControl lblCompletedAt;
        private LabelControl lblInitialSprint;
        private LabelControl lblCompletedInSprint;
        
        private TextEdit txtTitle;
        private MemoEdit txtDescription;
        private TextEdit txtRequestedBy;
        private DateEdit dtRequestedAt;
        private ComboBoxEdit cmbStatus;
        private ComboBoxEdit cmbType;
        private ComboBoxEdit cmbUrgency;
        private TextEdit txtProject;
        private TextEdit txtModule;
        private LookUpEdit cmbSprint;
        private SimpleButton btnChangeSprint;
        private TextEdit txtBoard;
        private TextEdit txtEffort;
        
        // Tab Control - Aktiviteler, Yorumlar, Dosyalar ve Email'ler
        private DevExpress.XtraTab.XtraTabControl tabControl;
        private DevExpress.XtraTab.XtraTabPage tabPageActivities;
        private DevExpress.XtraTab.XtraTabPage tabPageComments;
        private DevExpress.XtraTab.XtraTabPage tabPageAttachments;
        private DevExpress.XtraTab.XtraTabPage tabPageEmails;
        private DevExpress.XtraTab.XtraTabPage tabPageTimeEntries;
        
        // Zaman Kayıtları
        private GroupControl groupTimeEntries;
        private DevExpress.XtraGrid.GridControl gridTimeEntries;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTimeEntries;
        private LabelControl lblTimeEntryCount;
        
        // Aktivite Timeline
        private GroupControl groupActivities;
        private ListView lstActivities;
        private ColumnHeader colDate;
        private ColumnHeader colType;
        private ColumnHeader colDescription;
        private ColumnHeader colCreatedBy;
        private LabelControl lblActivityCount;
        
        // Yorumlar
        private GroupControl groupComments;
        private ListView lstComments;
        private ColumnHeader colCommentDate;
        private ColumnHeader colCommentAuthor;
        private ColumnHeader colCommentText;
        private LabelControl lblCommentCount;
        
        // Dosya Yönetimi
        private GroupControl groupAttachments;
        private ListView lstAttachments;
        private ColumnHeader colIcon;
        private ColumnHeader colFileName;
        private ColumnHeader colFileSize;
        private ColumnHeader colFileDescription;
        private ColumnHeader colUploadedBy;
        private ColumnHeader colUploadedAt;
        private LabelControl lblAttachmentCount;
        private LabelControl lblTotalSize;
        private SimpleButton btnAddFile;
        private SimpleButton btnDownloadFile;
        private SimpleButton btnOpenFile;
        private SimpleButton btnDeleteFile;
        private SimpleButton btnPreviewFile;
        
        // Yorum ekleme
        private GroupControl groupAddComment;
        private MemoEdit txtNewComment;
        private SimpleButton btnAddComment;
        private SimpleButton btnChangeStatus;
        
        // Email Yönetimi
        private GroupControl groupEmails;
        private ListView lstEmails;
        private ColumnHeader colEmailDate;
        private ColumnHeader colEmailFrom;
        private ColumnHeader colEmailSubject;
        private ColumnHeader colEmailRead;
        private ColumnHeader colEmailAttachments;
        private LabelControl lblEmailCount;
        private SimpleButton btnLinkEmail;
        private SimpleButton btnOpenEmail;
        private SimpleButton btnUnlinkEmail;
        private SimpleButton btnRefreshEmails;
        private TextEdit txtSearchEmail;

        
        // Alt panel
        private PanelControl panelBottom;
        private SimpleButton btnClose;
        private SimpleButton btnRefresh;

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
            this.groupWorkItemInfo = new DevExpress.XtraEditors.GroupControl();
            this.lblCompletedAt = new DevExpress.XtraEditors.LabelControl();
            this.lblCreatedAt = new DevExpress.XtraEditors.LabelControl();
            this.txtEffort = new DevExpress.XtraEditors.TextEdit();
            this.lblEffort = new DevExpress.XtraEditors.LabelControl();
            this.txtBoard = new DevExpress.XtraEditors.TextEdit();
            this.lblBoard = new DevExpress.XtraEditors.LabelControl();
            this.lblCompletedInSprint = new DevExpress.XtraEditors.LabelControl();
            this.lblInitialSprint = new DevExpress.XtraEditors.LabelControl();
            this.btnChangeSprint = new DevExpress.XtraEditors.SimpleButton();
            this.cmbSprint = new DevExpress.XtraEditors.LookUpEdit();
            this.lblSprint = new DevExpress.XtraEditors.LabelControl();
            this.txtModule = new DevExpress.XtraEditors.TextEdit();
            this.lblModule = new DevExpress.XtraEditors.LabelControl();
            this.txtProject = new DevExpress.XtraEditors.TextEdit();
            this.lblProject = new DevExpress.XtraEditors.LabelControl();
            this.cmbUrgency = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblUrgency = new DevExpress.XtraEditors.LabelControl();
            this.cmbType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblType = new DevExpress.XtraEditors.LabelControl();
            this.cmbStatus = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.dtRequestedAt = new DevExpress.XtraEditors.DateEdit();
            this.lblRequestedAt = new DevExpress.XtraEditors.LabelControl();
            this.txtRequestedBy = new DevExpress.XtraEditors.TextEdit();
            this.lblRequestedBy = new DevExpress.XtraEditors.LabelControl();
            this.txtDescription = new DevExpress.XtraEditors.MemoEdit();
            this.lblDescription = new DevExpress.XtraEditors.LabelControl();
            this.txtTitle = new DevExpress.XtraEditors.TextEdit();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.tabControl = new DevExpress.XtraTab.XtraTabControl();
            this.tabPageActivities = new DevExpress.XtraTab.XtraTabPage();
            this.groupActivities = new DevExpress.XtraEditors.GroupControl();
            this.lblActivityCount = new DevExpress.XtraEditors.LabelControl();
            this.lstActivities = new System.Windows.Forms.ListView();
            this.colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCreatedBy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageComments = new DevExpress.XtraTab.XtraTabPage();
            this.groupComments = new DevExpress.XtraEditors.GroupControl();
            this.lblCommentCount = new DevExpress.XtraEditors.LabelControl();
            this.lstComments = new System.Windows.Forms.ListView();
            this.colCommentDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCommentAuthor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCommentText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageAttachments = new DevExpress.XtraTab.XtraTabPage();
            this.groupAttachments = new DevExpress.XtraEditors.GroupControl();
            this.lstAttachments = new System.Windows.Forms.ListView();
            this.colIcon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFileSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFileDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUploadedBy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUploadedAt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnDeleteFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnPreviewFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpenFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnDownloadFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddFile = new DevExpress.XtraEditors.SimpleButton();
            this.lblTotalSize = new DevExpress.XtraEditors.LabelControl();
            this.lblAttachmentCount = new DevExpress.XtraEditors.LabelControl();
            this.tabPageEmails = new DevExpress.XtraTab.XtraTabPage();
            this.groupEmails = new DevExpress.XtraEditors.GroupControl();
            this.btnUnlinkEmail = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpenEmail = new DevExpress.XtraEditors.SimpleButton();
            this.btnLinkEmail = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefreshEmails = new DevExpress.XtraEditors.SimpleButton();
            this.txtSearchEmail = new DevExpress.XtraEditors.TextEdit();
            this.lblEmailCount = new DevExpress.XtraEditors.LabelControl();
            this.lstEmails = new System.Windows.Forms.ListView();
            this.colEmailDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEmailFrom = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEmailSubject = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEmailRead = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEmailAttachments = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupAddComment = new DevExpress.XtraEditors.GroupControl();
            this.btnChangeStatus = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddComment = new DevExpress.XtraEditors.SimpleButton();
            this.txtNewComment = new DevExpress.XtraEditors.MemoEdit();
            this.panelBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupWorkItemInfo)).BeginInit();
            this.groupWorkItemInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEffort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBoard.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSprint.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtModule.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUrgency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtRequestedAt.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtRequestedAt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestedBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPageActivities.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupActivities)).BeginInit();
            this.groupActivities.SuspendLayout();
            this.tabPageComments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupComments)).BeginInit();
            this.groupComments.SuspendLayout();
            this.tabPageAttachments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupAttachments)).BeginInit();
            this.groupAttachments.SuspendLayout();
            this.tabPageEmails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupEmails)).BeginInit();
            this.groupEmails.SuspendLayout();
            this.tabPageTimeEntries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupTimeEntries)).BeginInit();
            this.groupTimeEntries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTimeEntries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTimeEntries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupAddComment)).BeginInit();
            this.groupAddComment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupWorkItemInfo
            // 
            this.groupWorkItemInfo.Controls.Add(this.lblCompletedAt);
            this.groupWorkItemInfo.Controls.Add(this.lblCreatedAt);
            this.groupWorkItemInfo.Controls.Add(this.txtEffort);
            this.groupWorkItemInfo.Controls.Add(this.lblEffort);
            this.groupWorkItemInfo.Controls.Add(this.txtBoard);
            this.groupWorkItemInfo.Controls.Add(this.lblBoard);
            this.groupWorkItemInfo.Controls.Add(this.lblCompletedInSprint);
            this.groupWorkItemInfo.Controls.Add(this.lblInitialSprint);
            this.groupWorkItemInfo.Controls.Add(this.btnChangeSprint);
            this.groupWorkItemInfo.Controls.Add(this.cmbSprint);
            this.groupWorkItemInfo.Controls.Add(this.lblSprint);
            this.groupWorkItemInfo.Controls.Add(this.txtModule);
            this.groupWorkItemInfo.Controls.Add(this.lblModule);
            this.groupWorkItemInfo.Controls.Add(this.txtProject);
            this.groupWorkItemInfo.Controls.Add(this.lblProject);
            this.groupWorkItemInfo.Controls.Add(this.cmbUrgency);
            this.groupWorkItemInfo.Controls.Add(this.lblUrgency);
            this.groupWorkItemInfo.Controls.Add(this.cmbType);
            this.groupWorkItemInfo.Controls.Add(this.lblType);
            this.groupWorkItemInfo.Controls.Add(this.cmbStatus);
            this.groupWorkItemInfo.Controls.Add(this.lblStatus);
            this.groupWorkItemInfo.Controls.Add(this.dtRequestedAt);
            this.groupWorkItemInfo.Controls.Add(this.lblRequestedAt);
            this.groupWorkItemInfo.Controls.Add(this.txtRequestedBy);
            this.groupWorkItemInfo.Controls.Add(this.lblRequestedBy);
            this.groupWorkItemInfo.Controls.Add(this.txtDescription);
            this.groupWorkItemInfo.Controls.Add(this.lblDescription);
            this.groupWorkItemInfo.Controls.Add(this.txtTitle);
            this.groupWorkItemInfo.Controls.Add(this.lblTitle);
            this.groupWorkItemInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupWorkItemInfo.Location = new System.Drawing.Point(0, 0);
            this.groupWorkItemInfo.Name = "groupWorkItemInfo";
            this.groupWorkItemInfo.Size = new System.Drawing.Size(1200, 350);
            this.groupWorkItemInfo.TabIndex = 0;
            this.groupWorkItemInfo.Text = "İş Bilgileri";
            // 
            // lblCompletedAt
            // 
            this.lblCompletedAt.Location = new System.Drawing.Point(600, 290);
            this.lblCompletedAt.Name = "lblCompletedAt";
            this.lblCompletedAt.Size = new System.Drawing.Size(73, 13);
            this.lblCompletedAt.TabIndex = 25;
            this.lblCompletedAt.Text = "Tamamlanma: -";
            // 
            // lblCreatedAt
            // 
            this.lblCreatedAt.Location = new System.Drawing.Point(20, 331);
            this.lblCreatedAt.Name = "lblCreatedAt";
            this.lblCreatedAt.Size = new System.Drawing.Size(68, 13);
            this.lblCreatedAt.TabIndex = 24;
            this.lblCreatedAt.Text = "Oluşturulma: -";
            // 
            // txtEffort
            // 
            this.txtEffort.Location = new System.Drawing.Point(710, 230);
            this.txtEffort.Name = "txtEffort";
            this.txtEffort.Properties.ReadOnly = true;
            this.txtEffort.Size = new System.Drawing.Size(450, 20);
            this.txtEffort.TabIndex = 25;
            // 
            // lblEffort
            // 
            this.lblEffort.Location = new System.Drawing.Point(600, 235);
            this.lblEffort.Name = "lblEffort";
            this.lblEffort.Size = new System.Drawing.Size(53, 13);
            this.lblEffort.TabIndex = 26;
            this.lblEffort.Text = "Efor (gün):";
            // 
            // txtBoard
            // 
            this.txtBoard.Location = new System.Drawing.Point(710, 195);
            this.txtBoard.Name = "txtBoard";
            this.txtBoard.Properties.ReadOnly = true;
            this.txtBoard.Size = new System.Drawing.Size(450, 20);
            this.txtBoard.TabIndex = 23;
            // 
            // lblBoard
            // 
            this.lblBoard.Location = new System.Drawing.Point(600, 200);
            this.lblBoard.Name = "lblBoard";
            this.lblBoard.Size = new System.Drawing.Size(28, 13);
            this.lblBoard.TabIndex = 24;
            this.lblBoard.Text = "Pano:";
            // 
            // lblCompletedInSprint
            // 
            this.lblCompletedInSprint.Appearance.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Italic);
            this.lblCompletedInSprint.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblCompletedInSprint.Appearance.Options.UseFont = true;
            this.lblCompletedInSprint.Appearance.Options.UseForeColor = true;
            this.lblCompletedInSprint.Location = new System.Drawing.Point(820, 170);
            this.lblCompletedInSprint.Name = "lblCompletedInSprint";
            this.lblCompletedInSprint.Size = new System.Drawing.Size(98, 12);
            this.lblCompletedInSprint.TabIndex = 22;
            this.lblCompletedInSprint.Text = "Tamamlanan Sprint: -";
            // 
            // lblInitialSprint
            // 
            this.lblInitialSprint.Appearance.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Italic);
            this.lblInitialSprint.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblInitialSprint.Appearance.Options.UseFont = true;
            this.lblInitialSprint.Appearance.Options.UseForeColor = true;
            this.lblInitialSprint.Location = new System.Drawing.Point(600, 170);
            this.lblInitialSprint.Name = "lblInitialSprint";
            this.lblInitialSprint.Size = new System.Drawing.Size(54, 12);
            this.lblInitialSprint.TabIndex = 21;
            this.lblInitialSprint.Text = "İlk Sprint: -";
            // 
            // btnChangeSprint
            // 
            this.btnChangeSprint.Location = new System.Drawing.Point(1070, 138);
            this.btnChangeSprint.Name = "btnChangeSprint";
            this.btnChangeSprint.Size = new System.Drawing.Size(90, 24);
            this.btnChangeSprint.TabIndex = 20;
            this.btnChangeSprint.Text = "Sprint Değiştir";
            this.btnChangeSprint.Click += new System.EventHandler(this.btnChangeSprint_Click);
            // 
            // cmbSprint
            // 
            this.cmbSprint.Location = new System.Drawing.Point(710, 140);
            this.cmbSprint.Name = "cmbSprint";
            this.cmbSprint.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSprint.Properties.NullText = "(Sprint seçilmedi)";
            this.cmbSprint.Size = new System.Drawing.Size(350, 20);
            this.cmbSprint.TabIndex = 19;
            // 
            // lblSprint
            // 
            this.lblSprint.Location = new System.Drawing.Point(600, 145);
            this.lblSprint.Name = "lblSprint";
            this.lblSprint.Size = new System.Drawing.Size(57, 13);
            this.lblSprint.TabIndex = 18;
            this.lblSprint.Text = "Aktif Sprint:";
            // 
            // txtModule
            // 
            this.txtModule.Location = new System.Drawing.Point(130, 290);
            this.txtModule.Name = "txtModule";
            this.txtModule.Properties.ReadOnly = true;
            this.txtModule.Size = new System.Drawing.Size(450, 20);
            this.txtModule.TabIndex = 11;
            // 
            // lblModule
            // 
            this.lblModule.Location = new System.Drawing.Point(20, 295);
            this.lblModule.Name = "lblModule";
            this.lblModule.Size = new System.Drawing.Size(32, 13);
            this.lblModule.TabIndex = 10;
            this.lblModule.Text = "Modül:";
            // 
            // txtProject
            // 
            this.txtProject.Location = new System.Drawing.Point(130, 255);
            this.txtProject.Name = "txtProject";
            this.txtProject.Properties.ReadOnly = true;
            this.txtProject.Size = new System.Drawing.Size(450, 20);
            this.txtProject.TabIndex = 9;
            // 
            // lblProject
            // 
            this.lblProject.Location = new System.Drawing.Point(20, 260);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(29, 13);
            this.lblProject.TabIndex = 8;
            this.lblProject.Text = "Proje:";
            // 
            // cmbUrgency
            // 
            this.cmbUrgency.Location = new System.Drawing.Point(710, 105);
            this.cmbUrgency.Name = "cmbUrgency";
            this.cmbUrgency.Properties.Items.AddRange(new object[] {
            "Kritik",
            "Yuksek",
            "Normal",
            "Düşük"});
            this.cmbUrgency.Properties.ReadOnly = true;
            this.cmbUrgency.Size = new System.Drawing.Size(450, 20);
            this.cmbUrgency.TabIndex = 17;
            // 
            // lblUrgency
            // 
            this.lblUrgency.Location = new System.Drawing.Point(600, 110);
            this.lblUrgency.Name = "lblUrgency";
            this.lblUrgency.Size = new System.Drawing.Size(38, 13);
            this.lblUrgency.TabIndex = 16;
            this.lblUrgency.Text = "Aciliyet:";
            // 
            // cmbType
            // 
            this.cmbType.Location = new System.Drawing.Point(710, 70);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Items.AddRange(new object[] {
            "AcilArge",
            "Bug",
            "YeniOzellik",
            "Geliştirme",
            "Destek",
            "Analiz"});
            this.cmbType.Properties.ReadOnly = true;
            this.cmbType.Size = new System.Drawing.Size(450, 20);
            this.cmbType.TabIndex = 15;
            // 
            // lblType
            // 
            this.lblType.Location = new System.Drawing.Point(600, 75);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(18, 13);
            this.lblType.TabIndex = 14;
            this.lblType.Text = "Tip:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.Location = new System.Drawing.Point(710, 35);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Properties.Items.AddRange(new object[] {
            "Bekliyor",
            "Triage",
            "SprintBacklog",
            "Gelistirmede",
            "Testte",
            "Tamamlandi",
            "Gelen Acil İşler",
            "Sırada",
            "Müdahale Ediliyor",
            "Doğrulama Bekliyor",
            "Çözüldü"});
            this.cmbStatus.Size = new System.Drawing.Size(450, 20);
            this.cmbStatus.TabIndex = 13;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(600, 40);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(35, 13);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "Durum:";
            // 
            // dtRequestedAt
            // 
            this.dtRequestedAt.EditValue = new System.DateTime(2025, 11, 17, 0, 0, 0, 0);
            this.dtRequestedAt.Location = new System.Drawing.Point(130, 220);
            this.dtRequestedAt.Name = "dtRequestedAt";
            this.dtRequestedAt.Properties.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
            this.dtRequestedAt.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtRequestedAt.Properties.ReadOnly = true;
            this.dtRequestedAt.Size = new System.Drawing.Size(450, 20);
            this.dtRequestedAt.TabIndex = 7;
            // 
            // lblRequestedAt
            // 
            this.lblRequestedAt.Location = new System.Drawing.Point(20, 225);
            this.lblRequestedAt.Name = "lblRequestedAt";
            this.lblRequestedAt.Size = new System.Drawing.Size(59, 13);
            this.lblRequestedAt.TabIndex = 6;
            this.lblRequestedAt.Text = "Talep Tarihi:";
            // 
            // txtRequestedBy
            // 
            this.txtRequestedBy.Location = new System.Drawing.Point(130, 185);
            this.txtRequestedBy.Name = "txtRequestedBy";
            this.txtRequestedBy.Properties.ReadOnly = true;
            this.txtRequestedBy.Size = new System.Drawing.Size(450, 20);
            this.txtRequestedBy.TabIndex = 5;
            // 
            // lblRequestedBy
            // 
            this.lblRequestedBy.Location = new System.Drawing.Point(20, 190);
            this.lblRequestedBy.Name = "lblRequestedBy";
            this.lblRequestedBy.Size = new System.Drawing.Size(57, 13);
            this.lblRequestedBy.TabIndex = 4;
            this.lblRequestedBy.Text = "Talep Eden:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(130, 70);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Properties.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(450, 80);
            this.txtDescription.TabIndex = 3;
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(20, 75);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(45, 13);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Açıklama:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(130, 35);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Properties.ReadOnly = true;
            this.txtTitle.Size = new System.Drawing.Size(450, 20);
            this.txtTitle.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(20, 40);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(30, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Başlık:";
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 350);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabPage = this.tabPageActivities;
            this.tabControl.Size = new System.Drawing.Size(1200, 345);
            this.tabControl.TabIndex = 1;
            this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageActivities,
            this.tabPageComments,
            this.tabPageAttachments,
            this.tabPageEmails});
            // 
            // tabPageActivities
            // 
            this.tabPageActivities.Controls.Add(this.groupActivities);
            this.tabPageActivities.Name = "tabPageActivities";
            this.tabPageActivities.Size = new System.Drawing.Size(1198, 320);
            this.tabPageActivities.Text = "📋 Aktivite Geçmişi";
            // 
            // groupActivities
            // 
            this.groupActivities.Controls.Add(this.lblActivityCount);
            this.groupActivities.Controls.Add(this.lstActivities);
            this.groupActivities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupActivities.Location = new System.Drawing.Point(0, 0);
            this.groupActivities.Name = "groupActivities";
            this.groupActivities.Size = new System.Drawing.Size(1198, 320);
            this.groupActivities.TabIndex = 0;
            this.groupActivities.Text = "Aktivite Geçmişi (Timeline)";
            // 
            // lblActivityCount
            // 
            this.lblActivityCount.Location = new System.Drawing.Point(15, 30);
            this.lblActivityCount.Name = "lblActivityCount";
            this.lblActivityCount.Size = new System.Drawing.Size(81, 13);
            this.lblActivityCount.TabIndex = 1;
            this.lblActivityCount.Text = "Toplam 0 aktivite";
            // 
            // lstActivities
            // 
            this.lstActivities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstActivities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDate,
            this.colType,
            this.colDescription,
            this.colCreatedBy});
            this.lstActivities.FullRowSelect = true;
            this.lstActivities.GridLines = true;
            this.lstActivities.HideSelection = false;
            this.lstActivities.Location = new System.Drawing.Point(15, 55);
            this.lstActivities.Name = "lstActivities";
            this.lstActivities.Size = new System.Drawing.Size(1170, 281);
            this.lstActivities.TabIndex = 0;
            this.lstActivities.UseCompatibleStateImageBehavior = false;
            this.lstActivities.View = System.Windows.Forms.View.Details;
            // 
            // colDate
            // 
            this.colDate.Text = "Tarih/Saat";
            this.colDate.Width = 150;
            // 
            // colType
            // 
            this.colType.Text = "Tip";
            this.colType.Width = 150;
            // 
            // colDescription
            // 
            this.colDescription.Text = "Açıklama";
            this.colDescription.Width = 650;
            // 
            // colCreatedBy
            // 
            this.colCreatedBy.Text = "Yapan";
            this.colCreatedBy.Width = 200;
            // 
            // tabPageComments
            // 
            this.tabPageComments.Controls.Add(this.groupComments);
            this.tabPageComments.Name = "tabPageComments";
            this.tabPageComments.Size = new System.Drawing.Size(1198, 275);
            this.tabPageComments.Text = "💬 Yorumlar";
            // 
            // groupComments
            // 
            this.groupComments.Controls.Add(this.lblCommentCount);
            this.groupComments.Controls.Add(this.lstComments);
            this.groupComments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupComments.Location = new System.Drawing.Point(0, 0);
            this.groupComments.Name = "groupComments";
            this.groupComments.Size = new System.Drawing.Size(1198, 275);
            this.groupComments.TabIndex = 0;
            this.groupComments.Text = "Yorumlar";
            // 
            // lblCommentCount
            // 
            this.lblCommentCount.Location = new System.Drawing.Point(15, 30);
            this.lblCommentCount.Name = "lblCommentCount";
            this.lblCommentCount.Size = new System.Drawing.Size(76, 13);
            this.lblCommentCount.TabIndex = 1;
            this.lblCommentCount.Text = "Toplam 0 yorum";
            // 
            // lstComments
            // 
            this.lstComments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstComments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCommentDate,
            this.colCommentAuthor,
            this.colCommentText});
            this.lstComments.FullRowSelect = true;
            this.lstComments.GridLines = true;
            this.lstComments.HideSelection = false;
            this.lstComments.Location = new System.Drawing.Point(15, 55);
            this.lstComments.Name = "lstComments";
            this.lstComments.Size = new System.Drawing.Size(1170, 216);
            this.lstComments.TabIndex = 0;
            this.lstComments.UseCompatibleStateImageBehavior = false;
            this.lstComments.View = System.Windows.Forms.View.Details;
            // 
            // colCommentDate
            // 
            this.colCommentDate.Text = "Tarih/Saat";
            this.colCommentDate.Width = 150;
            // 
            // colCommentAuthor
            // 
            this.colCommentAuthor.Text = "Yazar";
            this.colCommentAuthor.Width = 200;
            // 
            // colCommentText
            // 
            this.colCommentText.Text = "Yorum";
            this.colCommentText.Width = 800;
            // 
            // tabPageAttachments
            // 
            this.tabPageAttachments.Controls.Add(this.groupAttachments);
            this.tabPageAttachments.Name = "tabPageAttachments";
            this.tabPageAttachments.Size = new System.Drawing.Size(1198, 320);
            this.tabPageAttachments.Text = "📎 Dosyalar";
            // 
            // groupAttachments
            // 
            this.groupAttachments.Controls.Add(this.lstAttachments);
            this.groupAttachments.Controls.Add(this.btnDeleteFile);
            this.groupAttachments.Controls.Add(this.btnPreviewFile);
            this.groupAttachments.Controls.Add(this.btnOpenFile);
            this.groupAttachments.Controls.Add(this.btnDownloadFile);
            this.groupAttachments.Controls.Add(this.btnAddFile);
            this.groupAttachments.Controls.Add(this.lblTotalSize);
            this.groupAttachments.Controls.Add(this.lblAttachmentCount);
            this.groupAttachments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupAttachments.Location = new System.Drawing.Point(0, 0);
            this.groupAttachments.Name = "groupAttachments";
            this.groupAttachments.Size = new System.Drawing.Size(1198, 320);
            this.groupAttachments.TabIndex = 0;
            this.groupAttachments.Text = "Ekli Dosyalar";
            // 
            // lstAttachments
            // 
            this.lstAttachments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstAttachments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIcon,
            this.colFileName,
            this.colFileSize,
            this.colFileDescription,
            this.colUploadedBy,
            this.colUploadedAt});
            this.lstAttachments.FullRowSelect = true;
            this.lstAttachments.GridLines = true;
            this.lstAttachments.HideSelection = false;
            this.lstAttachments.Location = new System.Drawing.Point(15, 55);
            this.lstAttachments.Name = "lstAttachments";
            this.lstAttachments.Size = new System.Drawing.Size(1170, 226);
            this.lstAttachments.TabIndex = 0;
            this.lstAttachments.UseCompatibleStateImageBehavior = false;
            this.lstAttachments.View = System.Windows.Forms.View.Details;
            // 
            // colIcon
            // 
            this.colIcon.Text = "";
            this.colIcon.Width = 40;
            // 
            // colFileName
            // 
            this.colFileName.Text = "Dosya Adı";
            this.colFileName.Width = 350;
            // 
            // colFileSize
            // 
            this.colFileSize.Text = "Boyut";
            this.colFileSize.Width = 100;
            // 
            // colFileDescription
            // 
            this.colFileDescription.Text = "Açıklama";
            this.colFileDescription.Width = 300;
            // 
            // colUploadedBy
            // 
            this.colUploadedBy.Text = "Yükleyen";
            this.colUploadedBy.Width = 180;
            // 
            // colUploadedAt
            // 
            this.colUploadedAt.Text = "Yükleme Tarihi";
            this.colUploadedAt.Width = 180;
            // 
            // btnDeleteFile
            // 
            this.btnDeleteFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteFile.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDeleteFile.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnDeleteFile.Appearance.Options.UseBackColor = true;
            this.btnDeleteFile.Appearance.Options.UseFont = true;
            this.btnDeleteFile.Location = new System.Drawing.Point(1101, 287);
            this.btnDeleteFile.Name = "btnDeleteFile";
            this.btnDeleteFile.Size = new System.Drawing.Size(85, 28);
            this.btnDeleteFile.TabIndex = 7;
            this.btnDeleteFile.Text = "🗑️ Sil";
            this.btnDeleteFile.Click += new System.EventHandler(this.btnDeleteFile_Click);
            // 
            // btnPreviewFile
            // 
            this.btnPreviewFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreviewFile.Location = new System.Drawing.Point(915, 287);
            this.btnPreviewFile.Name = "btnPreviewFile";
            this.btnPreviewFile.Size = new System.Drawing.Size(94, 28);
            this.btnPreviewFile.TabIndex = 6;
            this.btnPreviewFile.Text = "👁️ Önizle";
            this.btnPreviewFile.Click += new System.EventHandler(this.btnPreviewFile_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFile.Location = new System.Drawing.Point(1015, 287);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(80, 28);
            this.btnOpenFile.TabIndex = 5;
            this.btnOpenFile.Text = "📂 Aç";
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnDownloadFile
            // 
            this.btnDownloadFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownloadFile.Location = new System.Drawing.Point(815, 287);
            this.btnDownloadFile.Name = "btnDownloadFile";
            this.btnDownloadFile.Size = new System.Drawing.Size(94, 28);
            this.btnDownloadFile.TabIndex = 4;
            this.btnDownloadFile.Text = "💾 İndir";
            this.btnDownloadFile.Click += new System.EventHandler(this.btnDownloadFile_Click);
            // 
            // btnAddFile
            // 
            this.btnAddFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFile.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnAddFile.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddFile.Appearance.Options.UseBackColor = true;
            this.btnAddFile.Appearance.Options.UseFont = true;
            this.btnAddFile.Location = new System.Drawing.Point(679, 287);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(130, 28);
            this.btnAddFile.TabIndex = 3;
            this.btnAddFile.Text = "📁 Dosya Ekle";
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // lblTotalSize
            // 
            this.lblTotalSize.Location = new System.Drawing.Point(200, 30);
            this.lblTotalSize.Name = "lblTotalSize";
            this.lblTotalSize.Size = new System.Drawing.Size(87, 13);
            this.lblTotalSize.TabIndex = 2;
            this.lblTotalSize.Text = "Toplam Boyut: 0 B";
            // 
            // lblAttachmentCount
            // 
            this.lblAttachmentCount.Location = new System.Drawing.Point(15, 30);
            this.lblAttachmentCount.Name = "lblAttachmentCount";
            this.lblAttachmentCount.Size = new System.Drawing.Size(75, 13);
            this.lblAttachmentCount.TabIndex = 1;
            this.lblAttachmentCount.Text = "Toplam 0 dosya";
            // 
            // tabPageEmails
            // 
            this.tabPageEmails.Controls.Add(this.groupEmails);
            this.tabPageEmails.Name = "tabPageEmails";
            this.tabPageEmails.Size = new System.Drawing.Size(1198, 275);
            this.tabPageEmails.Text = "📧 Email\'ler";
            // 
            // groupEmails
            // 
            this.groupEmails.Controls.Add(this.btnUnlinkEmail);
            this.groupEmails.Controls.Add(this.btnOpenEmail);
            this.groupEmails.Controls.Add(this.btnLinkEmail);
            this.groupEmails.Controls.Add(this.btnRefreshEmails);
            this.groupEmails.Controls.Add(this.txtSearchEmail);
            this.groupEmails.Controls.Add(this.lblEmailCount);
            this.groupEmails.Controls.Add(this.lstEmails);
            this.groupEmails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupEmails.Location = new System.Drawing.Point(0, 0);
            this.groupEmails.Name = "groupEmails";
            this.groupEmails.Size = new System.Drawing.Size(1198, 275);
            this.groupEmails.TabIndex = 0;
            this.groupEmails.Text = "Bağlı Email\'ler";
            // 
            // btnUnlinkEmail
            // 
            this.btnUnlinkEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnlinkEmail.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnUnlinkEmail.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnUnlinkEmail.Appearance.Options.UseBackColor = true;
            this.btnUnlinkEmail.Appearance.Options.UseFont = true;
            this.btnUnlinkEmail.Location = new System.Drawing.Point(1101, 242);
            this.btnUnlinkEmail.Name = "btnUnlinkEmail";
            this.btnUnlinkEmail.Size = new System.Drawing.Size(85, 28);
            this.btnUnlinkEmail.TabIndex = 6;
            this.btnUnlinkEmail.Text = "🔗 Bağlantıyı Kaldır";
            this.btnUnlinkEmail.Click += new System.EventHandler(this.btnUnlinkEmail_Click);
            // 
            // btnOpenEmail
            // 
            this.btnOpenEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenEmail.Location = new System.Drawing.Point(1015, 242);
            this.btnOpenEmail.Name = "btnOpenEmail";
            this.btnOpenEmail.Size = new System.Drawing.Size(80, 28);
            this.btnOpenEmail.TabIndex = 5;
            this.btnOpenEmail.Text = "📧 Outlook\'ta Aç";
            this.btnOpenEmail.Click += new System.EventHandler(this.btnOpenEmail_Click);
            // 
            // btnLinkEmail
            // 
            this.btnLinkEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLinkEmail.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnLinkEmail.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnLinkEmail.Appearance.Options.UseBackColor = true;
            this.btnLinkEmail.Appearance.Options.UseFont = true;
            this.btnLinkEmail.Location = new System.Drawing.Point(815, 242);
            this.btnLinkEmail.Name = "btnLinkEmail";
            this.btnLinkEmail.Size = new System.Drawing.Size(194, 28);
            this.btnLinkEmail.TabIndex = 4;
            this.btnLinkEmail.Text = "📧 Outlook\'tan Email Bağla";
            this.btnLinkEmail.Click += new System.EventHandler(this.btnLinkEmail_Click);
            // 
            // btnRefreshEmails
            // 
            this.btnRefreshEmails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefreshEmails.Location = new System.Drawing.Point(200, 242);
            this.btnRefreshEmails.Name = "btnRefreshEmails";
            this.btnRefreshEmails.Size = new System.Drawing.Size(85, 28);
            this.btnRefreshEmails.TabIndex = 3;
            this.btnRefreshEmails.Text = "🔄 Yenile";
            this.btnRefreshEmails.Click += new System.EventHandler(this.btnRefreshEmails_Click);
            // 
            // txtSearchEmail
            // 
            this.txtSearchEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSearchEmail.Location = new System.Drawing.Point(15, 245);
            this.txtSearchEmail.Name = "txtSearchEmail";
            this.txtSearchEmail.Properties.NullText = "Email ara...";
            this.txtSearchEmail.Size = new System.Drawing.Size(179, 20);
            this.txtSearchEmail.TabIndex = 2;
            // 
            // lblEmailCount
            // 
            this.lblEmailCount.Location = new System.Drawing.Point(15, 30);
            this.lblEmailCount.Name = "lblEmailCount";
            this.lblEmailCount.Size = new System.Drawing.Size(70, 13);
            this.lblEmailCount.TabIndex = 1;
            this.lblEmailCount.Text = "Toplam 0 email";
            // 
            // lstEmails
            // 
            this.lstEmails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstEmails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colEmailDate,
            this.colEmailFrom,
            this.colEmailSubject,
            this.colEmailRead,
            this.colEmailAttachments});
            this.lstEmails.FullRowSelect = true;
            this.lstEmails.GridLines = true;
            this.lstEmails.HideSelection = false;
            this.lstEmails.Location = new System.Drawing.Point(15, 55);
            this.lstEmails.Name = "lstEmails";
            this.lstEmails.Size = new System.Drawing.Size(1170, 181);
            this.lstEmails.TabIndex = 0;
            this.lstEmails.UseCompatibleStateImageBehavior = false;
            this.lstEmails.View = System.Windows.Forms.View.Details;
            // 
            // colEmailDate
            // 
            this.colEmailDate.Text = "Tarih";
            this.colEmailDate.Width = 150;
            // 
            // colEmailFrom
            // 
            this.colEmailFrom.Text = "Gönderen";
            this.colEmailFrom.Width = 250;
            // 
            // colEmailSubject
            // 
            this.colEmailSubject.Text = "Konu";
            this.colEmailSubject.Width = 500;
            // 
            // colEmailRead
            // 
            this.colEmailRead.Text = "Okundu";
            this.colEmailRead.Width = 80;
            // 
            // colEmailAttachments
            // 
            this.colEmailAttachments.Text = "Ekler";
            this.colEmailAttachments.Width = 80;
            // 
            // groupAddComment
            // 
            this.groupAddComment.Controls.Add(this.btnChangeStatus);
            this.groupAddComment.Controls.Add(this.btnAddComment);
            this.groupAddComment.Controls.Add(this.txtNewComment);
            this.groupAddComment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupAddComment.Location = new System.Drawing.Point(0, 695);
            this.groupAddComment.Name = "groupAddComment";
            this.groupAddComment.Size = new System.Drawing.Size(1200, 120);
            this.groupAddComment.TabIndex = 2;
            this.groupAddComment.Text = "Yeni Yorum veya Durum Güncelleme";
            // 
            // btnChangeStatus
            // 
            this.btnChangeStatus.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnChangeStatus.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnChangeStatus.Appearance.Options.UseBackColor = true;
            this.btnChangeStatus.Appearance.Options.UseFont = true;
            this.btnChangeStatus.Location = new System.Drawing.Point(1075, 68);
            this.btnChangeStatus.Name = "btnChangeStatus";
            this.btnChangeStatus.Size = new System.Drawing.Size(110, 32);
            this.btnChangeStatus.TabIndex = 2;
            this.btnChangeStatus.Text = "📊 Durum Değiştir";
            this.btnChangeStatus.Click += new System.EventHandler(this.btnChangeStatus_Click);
            // 
            // btnAddComment
            // 
            this.btnAddComment.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnAddComment.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddComment.Appearance.Options.UseBackColor = true;
            this.btnAddComment.Appearance.Options.UseFont = true;
            this.btnAddComment.Location = new System.Drawing.Point(1075, 30);
            this.btnAddComment.Name = "btnAddComment";
            this.btnAddComment.Size = new System.Drawing.Size(110, 32);
            this.btnAddComment.TabIndex = 1;
            this.btnAddComment.Text = "💬 Yorum Ekle";
            this.btnAddComment.Click += new System.EventHandler(this.btnAddComment_Click);
            // 
            // txtNewComment
            // 
            this.txtNewComment.Location = new System.Drawing.Point(15, 30);
            this.txtNewComment.Name = "txtNewComment";
            this.txtNewComment.Size = new System.Drawing.Size(1050, 70);
            this.txtNewComment.TabIndex = 0;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnRefresh);
            this.panelBottom.Controls.Add(this.btnClose);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 815);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1200, 50);
            this.panelBottom.TabIndex = 3;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(985, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "🔄 Yenile";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(1090, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Kapat";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // WorkItemDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 865);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.groupAddComment);
            this.Controls.Add(this.groupWorkItemInfo);
            this.Controls.Add(this.panelBottom);
            this.Name = "WorkItemDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "İş Detayı";
            this.Load += new System.EventHandler(this.WorkItemDetailForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupWorkItemInfo)).EndInit();
            this.groupWorkItemInfo.ResumeLayout(false);
            this.groupWorkItemInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEffort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBoard.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSprint.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtModule.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUrgency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtRequestedAt.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtRequestedAt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestedBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPageActivities.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupActivities)).EndInit();
            this.groupActivities.ResumeLayout(false);
            this.groupActivities.PerformLayout();
            this.tabPageComments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupComments)).EndInit();
            this.groupComments.ResumeLayout(false);
            this.groupComments.PerformLayout();
            this.tabPageAttachments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupAttachments)).EndInit();
            this.groupAttachments.ResumeLayout(false);
            this.groupAttachments.PerformLayout();
            this.tabPageEmails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupEmails)).EndInit();
            this.groupEmails.ResumeLayout(false);
            this.groupEmails.PerformLayout();
            this.tabPageTimeEntries.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupTimeEntries)).EndInit();
            this.groupTimeEntries.ResumeLayout(false);
            this.groupTimeEntries.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTimeEntries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTimeEntries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupAddComment)).EndInit();
            this.groupAddComment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNewComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}

