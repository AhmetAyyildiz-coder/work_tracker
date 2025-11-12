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
        
        private TextEdit txtTitle;
        private MemoEdit txtDescription;
        private TextEdit txtRequestedBy;
        private DateEdit dtRequestedAt;
        private ComboBoxEdit cmbStatus;
        private ComboBoxEdit cmbType;
        private ComboBoxEdit cmbUrgency;
        private TextEdit txtProject;
        private TextEdit txtModule;
        private TextEdit txtSprint;
        private TextEdit txtBoard;
        private TextEdit txtEffort;
        
        // Tab Control - Aktiviteler ve Dosyalar
        private DevExpress.XtraTab.XtraTabControl tabControl;
        private DevExpress.XtraTab.XtraTabPage tabPageActivities;
        private DevExpress.XtraTab.XtraTabPage tabPageAttachments;
        
        // Aktivite Timeline
        private GroupControl groupActivities;
        private ListView lstActivities;
        private ColumnHeader colDate;
        private ColumnHeader colType;
        private ColumnHeader colDescription;
        private ColumnHeader colCreatedBy;
        private LabelControl lblActivityCount;
        
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
        
        // Yorum ekleme
        private GroupControl groupAddComment;
        private MemoEdit txtNewComment;
        private SimpleButton btnAddComment;
        private SimpleButton btnChangeStatus;
        
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
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblDescription = new DevExpress.XtraEditors.LabelControl();
            this.lblRequestedBy = new DevExpress.XtraEditors.LabelControl();
            this.lblRequestedAt = new DevExpress.XtraEditors.LabelControl();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.lblType = new DevExpress.XtraEditors.LabelControl();
            this.lblUrgency = new DevExpress.XtraEditors.LabelControl();
            this.lblProject = new DevExpress.XtraEditors.LabelControl();
            this.lblModule = new DevExpress.XtraEditors.LabelControl();
            this.lblSprint = new DevExpress.XtraEditors.LabelControl();
            this.lblBoard = new DevExpress.XtraEditors.LabelControl();
            this.lblEffort = new DevExpress.XtraEditors.LabelControl();
            this.lblCreatedAt = new DevExpress.XtraEditors.LabelControl();
            this.lblCompletedAt = new DevExpress.XtraEditors.LabelControl();
            
            this.txtTitle = new DevExpress.XtraEditors.TextEdit();
            this.txtDescription = new DevExpress.XtraEditors.MemoEdit();
            this.txtRequestedBy = new DevExpress.XtraEditors.TextEdit();
            this.dtRequestedAt = new DevExpress.XtraEditors.DateEdit();
            this.cmbStatus = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbUrgency = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtProject = new DevExpress.XtraEditors.TextEdit();
            this.txtModule = new DevExpress.XtraEditors.TextEdit();
            this.txtSprint = new DevExpress.XtraEditors.TextEdit();
            this.txtBoard = new DevExpress.XtraEditors.TextEdit();
            this.txtEffort = new DevExpress.XtraEditors.TextEdit();
            
            this.tabControl = new DevExpress.XtraTab.XtraTabControl();
            this.tabPageActivities = new DevExpress.XtraTab.XtraTabPage();
            this.tabPageAttachments = new DevExpress.XtraTab.XtraTabPage();
            
            this.groupActivities = new DevExpress.XtraEditors.GroupControl();
            this.lstActivities = new System.Windows.Forms.ListView();
            this.colDate = new System.Windows.Forms.ColumnHeader();
            this.colType = new System.Windows.Forms.ColumnHeader();
            this.colDescription = new System.Windows.Forms.ColumnHeader();
            this.colCreatedBy = new System.Windows.Forms.ColumnHeader();
            this.lblActivityCount = new DevExpress.XtraEditors.LabelControl();
            
            this.groupAttachments = new DevExpress.XtraEditors.GroupControl();
            this.lstAttachments = new System.Windows.Forms.ListView();
            this.colIcon = new System.Windows.Forms.ColumnHeader();
            this.colFileName = new System.Windows.Forms.ColumnHeader();
            this.colFileSize = new System.Windows.Forms.ColumnHeader();
            this.colFileDescription = new System.Windows.Forms.ColumnHeader();
            this.colUploadedBy = new System.Windows.Forms.ColumnHeader();
            this.colUploadedAt = new System.Windows.Forms.ColumnHeader();
            this.lblAttachmentCount = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalSize = new DevExpress.XtraEditors.LabelControl();
            this.btnAddFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnDownloadFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpenFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteFile = new DevExpress.XtraEditors.SimpleButton();
            
            this.groupAddComment = new DevExpress.XtraEditors.GroupControl();
            this.txtNewComment = new DevExpress.XtraEditors.MemoEdit();
            this.btnAddComment = new DevExpress.XtraEditors.SimpleButton();
            this.btnChangeStatus = new DevExpress.XtraEditors.SimpleButton();
            
            this.panelBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            
            ((System.ComponentModel.ISupportInitialize)(this.groupWorkItemInfo)).BeginInit();
            this.groupWorkItemInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestedBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtRequestedAt.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtRequestedAt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUrgency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtModule.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSprint.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBoard.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEffort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPageActivities.SuspendLayout();
            this.tabPageAttachments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupActivities)).BeginInit();
            this.groupActivities.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupAttachments)).BeginInit();
            this.groupAttachments.SuspendLayout();
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
            this.groupWorkItemInfo.Controls.Add(this.txtSprint);
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
            this.groupWorkItemInfo.Size = new System.Drawing.Size(1200, 320);
            this.groupWorkItemInfo.TabIndex = 0;
            this.groupWorkItemInfo.Text = "İş Bilgileri";
            
            // Sol Sütun
            int leftX = 20;
            int rightX = 600;
            int labelWidth = 100;
            int controlWidth = 450;
            int rowHeight = 35;
            int startY = 35;
            
            // Başlık
            this.lblTitle.Location = new System.Drawing.Point(leftX, startY + 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(labelWidth, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Başlık:";
            
            this.txtTitle.Location = new System.Drawing.Point(leftX + labelWidth + 10, startY);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Properties.ReadOnly = true;
            this.txtTitle.Size = new System.Drawing.Size(controlWidth, 20);
            this.txtTitle.TabIndex = 1;
            
            // Açıklama
            this.lblDescription.Location = new System.Drawing.Point(leftX, startY + rowHeight + 5);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(labelWidth, 13);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Açıklama:";
            
            this.txtDescription.Location = new System.Drawing.Point(leftX + labelWidth + 10, startY + rowHeight);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Properties.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(controlWidth, 80);
            this.txtDescription.TabIndex = 3;
            
            // Talep Eden
            this.lblRequestedBy.Location = new System.Drawing.Point(leftX, startY + rowHeight * 2 + 80 + 5);
            this.lblRequestedBy.Name = "lblRequestedBy";
            this.lblRequestedBy.Size = new System.Drawing.Size(labelWidth, 13);
            this.lblRequestedBy.TabIndex = 4;
            this.lblRequestedBy.Text = "Talep Eden:";
            
            this.txtRequestedBy.Location = new System.Drawing.Point(leftX + labelWidth + 10, startY + rowHeight * 2 + 80);
            this.txtRequestedBy.Name = "txtRequestedBy";
            this.txtRequestedBy.Properties.ReadOnly = true;
            this.txtRequestedBy.Size = new System.Drawing.Size(controlWidth, 20);
            this.txtRequestedBy.TabIndex = 5;
            
            // Talep Tarihi
            this.lblRequestedAt.Location = new System.Drawing.Point(leftX, startY + rowHeight * 3 + 80 + 5);
            this.lblRequestedAt.Name = "lblRequestedAt";
            this.lblRequestedAt.Size = new System.Drawing.Size(labelWidth, 13);
            this.lblRequestedAt.TabIndex = 6;
            this.lblRequestedAt.Text = "Talep Tarihi:";
            
            this.dtRequestedAt.Location = new System.Drawing.Point(leftX + labelWidth + 10, startY + rowHeight * 3 + 80);
            this.dtRequestedAt.Name = "dtRequestedAt";
            this.dtRequestedAt.Properties.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
            this.dtRequestedAt.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtRequestedAt.Properties.ReadOnly = true;
            this.dtRequestedAt.Size = new System.Drawing.Size(controlWidth, 20);
            this.dtRequestedAt.TabIndex = 7;
            
            // Proje
            this.lblProject.Location = new System.Drawing.Point(leftX, startY + rowHeight * 4 + 80 + 5);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(labelWidth, 13);
            this.lblProject.TabIndex = 8;
            this.lblProject.Text = "Proje:";
            
            this.txtProject.Location = new System.Drawing.Point(leftX + labelWidth + 10, startY + rowHeight * 4 + 80);
            this.txtProject.Name = "txtProject";
            this.txtProject.Properties.ReadOnly = true;
            this.txtProject.Size = new System.Drawing.Size(controlWidth, 20);
            this.txtProject.TabIndex = 9;
            
            // Modül
            this.lblModule.Location = new System.Drawing.Point(leftX, startY + rowHeight * 5 + 80 + 5);
            this.lblModule.Name = "lblModule";
            this.lblModule.Size = new System.Drawing.Size(labelWidth, 13);
            this.lblModule.TabIndex = 10;
            this.lblModule.Text = "Modül:";
            
            this.txtModule.Location = new System.Drawing.Point(leftX + labelWidth + 10, startY + rowHeight * 5 + 80);
            this.txtModule.Name = "txtModule";
            this.txtModule.Properties.ReadOnly = true;
            this.txtModule.Size = new System.Drawing.Size(controlWidth, 20);
            this.txtModule.TabIndex = 11;
            
            // Sağ Sütun
            // Durum (Düzenlenebilir)
            this.lblStatus.Location = new System.Drawing.Point(rightX, startY + 5);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(labelWidth, 13);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "Durum:";
            
            this.cmbStatus.Location = new System.Drawing.Point(rightX + labelWidth + 10, startY);
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
                "Çözüldü"
            });
            this.cmbStatus.Size = new System.Drawing.Size(controlWidth, 20);
            this.cmbStatus.TabIndex = 13;
            
            // Tip
            this.lblType.Location = new System.Drawing.Point(rightX, startY + rowHeight + 5);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(labelWidth, 13);
            this.lblType.TabIndex = 14;
            this.lblType.Text = "Tip:";
            
            this.cmbType.Location = new System.Drawing.Point(rightX + labelWidth + 10, startY + rowHeight);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Items.AddRange(new object[] {
                "AcilArge",
                "Bug",
                "YeniOzellik",
                "Geliştirme",
                "Destek",
                "Analiz"
            });
            this.cmbType.Properties.ReadOnly = true;
            this.cmbType.Size = new System.Drawing.Size(controlWidth, 20);
            this.cmbType.TabIndex = 15;
            
            // Aciliyet
            this.lblUrgency.Location = new System.Drawing.Point(rightX, startY + rowHeight * 2 + 5);
            this.lblUrgency.Name = "lblUrgency";
            this.lblUrgency.Size = new System.Drawing.Size(labelWidth, 13);
            this.lblUrgency.TabIndex = 16;
            this.lblUrgency.Text = "Aciliyet:";
            
            this.cmbUrgency.Location = new System.Drawing.Point(rightX + labelWidth + 10, startY + rowHeight * 2);
            this.cmbUrgency.Name = "cmbUrgency";
            this.cmbUrgency.Properties.Items.AddRange(new object[] {
                "Kritik",
                "Yuksek",
                "Normal",
                "Düşük"
            });
            this.cmbUrgency.Properties.ReadOnly = true;
            this.cmbUrgency.Size = new System.Drawing.Size(controlWidth, 20);
            this.cmbUrgency.TabIndex = 17;
            
            // Sprint
            this.lblSprint.Location = new System.Drawing.Point(rightX, startY + rowHeight * 3 + 5);
            this.lblSprint.Name = "lblSprint";
            this.lblSprint.Size = new System.Drawing.Size(labelWidth, 13);
            this.lblSprint.TabIndex = 18;
            this.lblSprint.Text = "Sprint:";
            
            this.txtSprint.Location = new System.Drawing.Point(rightX + labelWidth + 10, startY + rowHeight * 3);
            this.txtSprint.Name = "txtSprint";
            this.txtSprint.Properties.ReadOnly = true;
            this.txtSprint.Size = new System.Drawing.Size(controlWidth, 20);
            this.txtSprint.TabIndex = 19;
            
            // Pano
            this.lblBoard.Location = new System.Drawing.Point(rightX, startY + rowHeight * 4 + 5);
            this.lblBoard.Name = "lblBoard";
            this.lblBoard.Size = new System.Drawing.Size(labelWidth, 13);
            this.lblBoard.TabIndex = 20;
            this.lblBoard.Text = "Pano:";
            
            this.txtBoard.Location = new System.Drawing.Point(rightX + labelWidth + 10, startY + rowHeight * 4);
            this.txtBoard.Name = "txtBoard";
            this.txtBoard.Properties.ReadOnly = true;
            this.txtBoard.Size = new System.Drawing.Size(controlWidth, 20);
            this.txtBoard.TabIndex = 21;
            
            // Efor
            this.lblEffort.Location = new System.Drawing.Point(rightX, startY + rowHeight * 5 + 5);
            this.lblEffort.Name = "lblEffort";
            this.lblEffort.Size = new System.Drawing.Size(labelWidth, 13);
            this.lblEffort.TabIndex = 22;
            this.lblEffort.Text = "Efor (gün):";
            
            this.txtEffort.Location = new System.Drawing.Point(rightX + labelWidth + 10, startY + rowHeight * 5);
            this.txtEffort.Name = "txtEffort";
            this.txtEffort.Properties.ReadOnly = true;
            this.txtEffort.Size = new System.Drawing.Size(controlWidth, 20);
            this.txtEffort.TabIndex = 23;
            
            // Tarih bilgileri (Alt)
            this.lblCreatedAt.Location = new System.Drawing.Point(leftX, 290);
            this.lblCreatedAt.Name = "lblCreatedAt";
            this.lblCreatedAt.Size = new System.Drawing.Size(300, 13);
            this.lblCreatedAt.TabIndex = 24;
            this.lblCreatedAt.Text = "Oluşturulma: -";
            
            this.lblCompletedAt.Location = new System.Drawing.Point(rightX, 290);
            this.lblCompletedAt.Name = "lblCompletedAt";
            this.lblCompletedAt.Size = new System.Drawing.Size(300, 13);
            this.lblCompletedAt.TabIndex = 25;
            this.lblCompletedAt.Text = "Tamamlanma: -";
            
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 320);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabPage = this.tabPageActivities;
            this.tabControl.Size = new System.Drawing.Size(1200, 300);
            this.tabControl.TabIndex = 1;
            this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
                this.tabPageActivities,
                this.tabPageAttachments});
            
            // 
            // tabPageActivities
            // 
            this.tabPageActivities.Controls.Add(this.groupActivities);
            this.tabPageActivities.Name = "tabPageActivities";
            this.tabPageActivities.Size = new System.Drawing.Size(1198, 274);
            this.tabPageActivities.Text = "📋 Aktivite Geçmişi";
            
            // 
            // tabPageAttachments
            // 
            this.tabPageAttachments.Controls.Add(this.groupAttachments);
            this.tabPageAttachments.Name = "tabPageAttachments";
            this.tabPageAttachments.Size = new System.Drawing.Size(1198, 274);
            this.tabPageAttachments.Text = "📎 Dosyalar";
            
            // 
            // groupActivities
            // 
            this.groupActivities.Controls.Add(this.lblActivityCount);
            this.groupActivities.Controls.Add(this.lstActivities);
            this.groupActivities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupActivities.Location = new System.Drawing.Point(0, 0);
            this.groupActivities.Name = "groupActivities";
            this.groupActivities.Size = new System.Drawing.Size(1198, 274);
            this.groupActivities.TabIndex = 0;
            this.groupActivities.Text = "Aktivite Geçmişi (Timeline)";
            
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
            this.lstActivities.Size = new System.Drawing.Size(1170, 235);
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
            // lblActivityCount
            // 
            this.lblActivityCount.Location = new System.Drawing.Point(15, 30);
            this.lblActivityCount.Name = "lblActivityCount";
            this.lblActivityCount.Size = new System.Drawing.Size(100, 13);
            this.lblActivityCount.TabIndex = 1;
            this.lblActivityCount.Text = "Toplam 0 aktivite";
            
            // 
            // groupAttachments
            // 
            this.groupAttachments.Controls.Add(this.btnDeleteFile);
            this.groupAttachments.Controls.Add(this.btnOpenFile);
            this.groupAttachments.Controls.Add(this.btnDownloadFile);
            this.groupAttachments.Controls.Add(this.btnAddFile);
            this.groupAttachments.Controls.Add(this.lblTotalSize);
            this.groupAttachments.Controls.Add(this.lblAttachmentCount);
            this.groupAttachments.Controls.Add(this.lstAttachments);
            this.groupAttachments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupAttachments.Location = new System.Drawing.Point(0, 0);
            this.groupAttachments.Name = "groupAttachments";
            this.groupAttachments.Size = new System.Drawing.Size(1198, 274);
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
            this.lstAttachments.Size = new System.Drawing.Size(1170, 180);
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
            // lblAttachmentCount
            // 
            this.lblAttachmentCount.Location = new System.Drawing.Point(15, 30);
            this.lblAttachmentCount.Name = "lblAttachmentCount";
            this.lblAttachmentCount.Size = new System.Drawing.Size(100, 13);
            this.lblAttachmentCount.TabIndex = 1;
            this.lblAttachmentCount.Text = "Toplam 0 dosya";
            
            // 
            // lblTotalSize
            // 
            this.lblTotalSize.Location = new System.Drawing.Point(200, 30);
            this.lblTotalSize.Name = "lblTotalSize";
            this.lblTotalSize.Size = new System.Drawing.Size(150, 13);
            this.lblTotalSize.TabIndex = 2;
            this.lblTotalSize.Text = "Toplam Boyut: 0 B";
            
            // 
            // btnAddFile
            // 
            this.btnAddFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFile.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnAddFile.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddFile.Appearance.Options.UseBackColor = true;
            this.btnAddFile.Appearance.Options.UseFont = true;
            this.btnAddFile.Location = new System.Drawing.Point(753, 241);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(130, 28);
            this.btnAddFile.TabIndex = 3;
            this.btnAddFile.Text = "📁 Dosya Ekle";
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            
            // 
            // btnDownloadFile
            // 
            this.btnDownloadFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownloadFile.Location = new System.Drawing.Point(889, 241);
            this.btnDownloadFile.Name = "btnDownloadFile";
            this.btnDownloadFile.Size = new System.Drawing.Size(120, 28);
            this.btnDownloadFile.TabIndex = 4;
            this.btnDownloadFile.Text = "💾 İndir";
            this.btnDownloadFile.Click += new System.EventHandler(this.btnDownloadFile_Click);
            
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFile.Location = new System.Drawing.Point(1015, 241);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(80, 28);
            this.btnOpenFile.TabIndex = 5;
            this.btnOpenFile.Text = "📂 Aç";
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            
            // 
            // btnDeleteFile
            // 
            this.btnDeleteFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteFile.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDeleteFile.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnDeleteFile.Appearance.Options.UseBackColor = true;
            this.btnDeleteFile.Appearance.Options.UseFont = true;
            this.btnDeleteFile.Location = new System.Drawing.Point(1101, 241);
            this.btnDeleteFile.Name = "btnDeleteFile";
            this.btnDeleteFile.Size = new System.Drawing.Size(85, 28);
            this.btnDeleteFile.TabIndex = 6;
            this.btnDeleteFile.Text = "🗑️ Sil";
            this.btnDeleteFile.Click += new System.EventHandler(this.btnDeleteFile_Click);
            
            // 
            // groupAddComment
            // 
            this.groupAddComment.Controls.Add(this.btnChangeStatus);
            this.groupAddComment.Controls.Add(this.btnAddComment);
            this.groupAddComment.Controls.Add(this.txtNewComment);
            this.groupAddComment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupAddComment.Location = new System.Drawing.Point(0, 620);
            this.groupAddComment.Name = "groupAddComment";
            this.groupAddComment.Size = new System.Drawing.Size(1200, 120);
            this.groupAddComment.TabIndex = 2;
            this.groupAddComment.Text = "Yeni Yorum veya Durum Güncelleme";
            
            // 
            // txtNewComment
            // 
            this.txtNewComment.Location = new System.Drawing.Point(15, 30);
            this.txtNewComment.Name = "txtNewComment";
            this.txtNewComment.Size = new System.Drawing.Size(1050, 70);
            this.txtNewComment.TabIndex = 0;
            
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
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnRefresh);
            this.panelBottom.Controls.Add(this.btnClose);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 740);
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
            this.ClientSize = new System.Drawing.Size(1200, 790);
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
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestedBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtRequestedAt.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtRequestedAt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUrgency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtModule.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSprint.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBoard.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEffort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPageActivities.ResumeLayout(false);
            this.tabPageAttachments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupActivities)).EndInit();
            this.groupActivities.ResumeLayout(false);
            this.groupActivities.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupAttachments)).EndInit();
            this.groupAttachments.ResumeLayout(false);
            this.groupAttachments.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupAddComment)).EndInit();
            this.groupAddComment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNewComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBottom)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}

