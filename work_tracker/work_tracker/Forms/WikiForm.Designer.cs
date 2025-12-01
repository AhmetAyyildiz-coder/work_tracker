using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace work_tracker.Forms
{
    partial class WikiForm
    {
        private System.ComponentModel.IContainer components = null;
        private RibbonControl ribbonControl1;
        private RibbonPage ribbonPage1;
        private RibbonPageGroup ribbonPageGroup1;
        private BarButtonItem btnNew;
        private BarButtonItem btnEdit;
        private BarButtonItem btnSave;
        private BarButtonItem btnCancel;
        private BarButtonItem btnDelete;
        private BarButtonItem btnRefresh;
        
        private PanelControl panelLeft;
        private PanelControl panelRight;
        private SplitterControl splitter;
        private GridControl gridControl1;
        private GridView gridView1;
        
        private GroupControl groupDetails;
        private LabelControl lblTitle;
        private TextEdit txtTitle;
        private LabelControl lblSummary;
        private MemoEdit txtSummary;
        private LabelControl lblParent;
        private LookUpEdit cmbParentPage;
        private LabelControl lblContent;
        private RichEditControl richEditContent;
        private LabelControl lblInfo;
        
        private PanelControl panelToolbar;
        private SimpleButton btnBold;
        private SimpleButton btnItalic;
        private SimpleButton btnBulletList;
        private SimpleButton btnNumberedList;
        private SimpleButton btnInsertWorkItemLink;
        private SimpleButton btnInsertWikiLink;
        private SimpleButton btnInsertUrlLink;

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (components != null)
        //        {
        //            components.Dispose();
        //        }
        //        _context?.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private void InitializeComponent()
        {
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnNew = new DevExpress.XtraBars.BarButtonItem();
            this.btnEdit = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnCancel = new DevExpress.XtraBars.BarButtonItem();
            this.btnDelete = new DevExpress.XtraBars.BarButtonItem();
            this.btnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.panelLeft = new DevExpress.XtraEditors.PanelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitter = new DevExpress.XtraEditors.SplitterControl();
            this.panelRight = new DevExpress.XtraEditors.PanelControl();
            this.groupDetails = new DevExpress.XtraEditors.GroupControl();
            this.lblInfo = new DevExpress.XtraEditors.LabelControl();
            this.panelToolbar = new DevExpress.XtraEditors.PanelControl();
            this.btnNumberedList = new DevExpress.XtraEditors.SimpleButton();
            this.btnBulletList = new DevExpress.XtraEditors.SimpleButton();
            this.btnItalic = new DevExpress.XtraEditors.SimpleButton();
            this.btnBold = new DevExpress.XtraEditors.SimpleButton();
            this.btnInsertWorkItemLink = new DevExpress.XtraEditors.SimpleButton();
            this.btnInsertWikiLink = new DevExpress.XtraEditors.SimpleButton();
            this.btnInsertUrlLink = new DevExpress.XtraEditors.SimpleButton();
            this.richEditContent = new DevExpress.XtraRichEdit.RichEditControl();
            this.lblContent = new DevExpress.XtraEditors.LabelControl();
            this.cmbParentPage = new DevExpress.XtraEditors.LookUpEdit();
            this.lblParent = new DevExpress.XtraEditors.LabelControl();
            this.txtSummary = new DevExpress.XtraEditors.MemoEdit();
            this.lblSummary = new DevExpress.XtraEditors.LabelControl();
            this.txtTitle = new DevExpress.XtraEditors.TextEdit();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelLeft)).BeginInit();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelRight)).BeginInit();
            this.panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupDetails)).BeginInit();
            this.groupDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelToolbar)).BeginInit();
            this.panelToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbParentPage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSummary.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.btnNew,
            this.btnEdit,
            this.btnSave,
            this.btnCancel,
            this.btnDelete,
            this.btnRefresh});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 7;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.Size = new System.Drawing.Size(1400, 158);
            // 
            // btnNew
            // 
            this.btnNew.Caption = "Yeni Sayfa";
            this.btnNew.Id = 1;
            this.btnNew.Name = "btnNew";
            this.btnNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNew_ItemClick);
            // 
            // btnEdit
            // 
            this.btnEdit.Caption = "Düzenle";
            this.btnEdit.Id = 2;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEdit_ItemClick);
            // 
            // btnSave
            // 
            this.btnSave.Caption = "Kaydet";
            this.btnSave.Id = 3;
            this.btnSave.Name = "btnSave";
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSave_ItemClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Caption = "İptal";
            this.btnCancel.Id = 4;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCancel_ItemClick);
            // 
            // btnDelete
            // 
            this.btnDelete.Caption = "Sil";
            this.btnDelete.Id = 5;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDelete_ItemClick);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Caption = "Yenile";
            this.btnRefresh.Id = 6;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRefresh_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Wiki";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btnNew);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnEdit);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnSave);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnCancel);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnDelete);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnRefresh);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Sayfa İşlemleri";
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.gridControl1);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 158);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(500, 642);
            this.panelLeft.TabIndex = 1;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 2);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.ribbonControl1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(496, 638);
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
            // splitter
            // 
            this.splitter.Dock = System.Windows.Forms.DockStyle.Left;
            this.splitter.Location = new System.Drawing.Point(500, 158);
            this.splitter.Name = "splitter";
            this.splitter.Size = new System.Drawing.Size(3, 642);
            this.splitter.TabIndex = 2;
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.groupDetails);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(503, 158);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(897, 642);
            this.panelRight.TabIndex = 3;
            // 
            // groupDetails
            // 
            this.groupDetails.Controls.Add(this.lblInfo);
            this.groupDetails.Controls.Add(this.panelToolbar);
            this.groupDetails.Controls.Add(this.richEditContent);
            this.groupDetails.Controls.Add(this.lblContent);
            this.groupDetails.Controls.Add(this.cmbParentPage);
            this.groupDetails.Controls.Add(this.lblParent);
            this.groupDetails.Controls.Add(this.txtSummary);
            this.groupDetails.Controls.Add(this.lblSummary);
            this.groupDetails.Controls.Add(this.txtTitle);
            this.groupDetails.Controls.Add(this.lblTitle);
            this.groupDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupDetails.Location = new System.Drawing.Point(2, 2);
            this.groupDetails.Name = "groupDetails";
            this.groupDetails.Size = new System.Drawing.Size(893, 638);
            this.groupDetails.TabIndex = 0;
            this.groupDetails.Text = "Sayfa Detayı";
            // 
            // lblInfo
            // 
            this.lblInfo.Appearance.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Italic);
            this.lblInfo.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblInfo.Appearance.Options.UseFont = true;
            this.lblInfo.Appearance.Options.UseForeColor = true;
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblInfo.Location = new System.Drawing.Point(2, 619);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Padding = new System.Windows.Forms.Padding(5);
            this.lblInfo.Size = new System.Drawing.Size(10, 22);
            this.lblInfo.TabIndex = 9;
            this.lblInfo.Text = "";
            // 
            // panelToolbar
            // 
            this.panelToolbar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelToolbar.Controls.Add(this.btnInsertUrlLink);
            this.panelToolbar.Controls.Add(this.btnInsertWikiLink);
            this.panelToolbar.Controls.Add(this.btnInsertWorkItemLink);
            this.panelToolbar.Controls.Add(this.btnNumberedList);
            this.panelToolbar.Controls.Add(this.btnBulletList);
            this.panelToolbar.Controls.Add(this.btnItalic);
            this.panelToolbar.Controls.Add(this.btnBold);
            this.panelToolbar.Location = new System.Drawing.Point(15, 250);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(600, 30);
            this.panelToolbar.TabIndex = 8;
            // 
            // btnNumberedList
            // 
            this.btnNumberedList.Location = new System.Drawing.Point(120, 3);
            this.btnNumberedList.Name = "btnNumberedList";
            this.btnNumberedList.Size = new System.Drawing.Size(50, 24);
            this.btnNumberedList.TabIndex = 3;
            this.btnNumberedList.Text = "1. List";
            this.btnNumberedList.Click += new System.EventHandler(this.btnNumberedList_Click);
            // 
            // btnBulletList
            // 
            this.btnBulletList.Location = new System.Drawing.Point(80, 3);
            this.btnBulletList.Name = "btnBulletList";
            this.btnBulletList.Size = new System.Drawing.Size(34, 24);
            this.btnBulletList.TabIndex = 2;
            this.btnBulletList.Text = "• List";
            this.btnBulletList.Click += new System.EventHandler(this.btnBulletList_Click);
            // 
            // btnItalic
            // 
            this.btnItalic.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Italic);
            this.btnItalic.Appearance.Options.UseFont = true;
            this.btnItalic.Location = new System.Drawing.Point(40, 3);
            this.btnItalic.Name = "btnItalic";
            this.btnItalic.Size = new System.Drawing.Size(34, 24);
            this.btnItalic.TabIndex = 1;
            this.btnItalic.Text = "I";
            this.btnItalic.Click += new System.EventHandler(this.btnItalic_Click);
            // 
            // btnBold
            // 
            this.btnBold.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnBold.Appearance.Options.UseFont = true;
            this.btnBold.Location = new System.Drawing.Point(0, 3);
            this.btnBold.Name = "btnBold";
            this.btnBold.Size = new System.Drawing.Size(34, 24);
            this.btnBold.TabIndex = 0;
            this.btnBold.Text = "B";
            this.btnBold.Click += new System.EventHandler(this.btnBold_Click);
            // 
            // btnInsertWorkItemLink
            // 
            this.btnInsertWorkItemLink.Location = new System.Drawing.Point(190, 3);
            this.btnInsertWorkItemLink.Name = "btnInsertWorkItemLink";
            this.btnInsertWorkItemLink.Size = new System.Drawing.Size(80, 24);
            this.btnInsertWorkItemLink.TabIndex = 4;
            this.btnInsertWorkItemLink.Text = "🔗 İş #";
            this.btnInsertWorkItemLink.ToolTip = "İş öğesi linki ekle (#123 formatında)";
            this.btnInsertWorkItemLink.Click += new System.EventHandler(this.btnInsertWorkItemLink_Click);
            // 
            // btnInsertWikiLink
            // 
            this.btnInsertWikiLink.Location = new System.Drawing.Point(280, 3);
            this.btnInsertWikiLink.Name = "btnInsertWikiLink";
            this.btnInsertWikiLink.Size = new System.Drawing.Size(90, 24);
            this.btnInsertWikiLink.TabIndex = 5;
            this.btnInsertWikiLink.Text = "📄 Wiki Link";
            this.btnInsertWikiLink.ToolTip = "Wiki sayfası linki ekle";
            this.btnInsertWikiLink.Click += new System.EventHandler(this.btnInsertWikiLink_Click);
            // 
            // btnInsertUrlLink
            // 
            this.btnInsertUrlLink.Location = new System.Drawing.Point(380, 3);
            this.btnInsertUrlLink.Name = "btnInsertUrlLink";
            this.btnInsertUrlLink.Size = new System.Drawing.Size(70, 24);
            this.btnInsertUrlLink.TabIndex = 6;
            this.btnInsertUrlLink.Text = "🌐 URL";
            this.btnInsertUrlLink.ToolTip = "Web linki ekle";
            this.btnInsertUrlLink.Click += new System.EventHandler(this.btnInsertUrlLink_Click);
            // 
            // richEditContent
            // 
            this.richEditContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richEditContent.Location = new System.Drawing.Point(15, 285);
            this.richEditContent.MenuManager = this.ribbonControl1;
            this.richEditContent.Name = "richEditContent";
            this.richEditContent.ReadOnly = true;
            this.richEditContent.Options.DocumentCapabilities.CharacterFormatting = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditContent.Options.DocumentCapabilities.CharacterStyle = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditContent.Options.DocumentCapabilities.InlinePictures = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditContent.Options.DocumentCapabilities.Hyperlinks = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditContent.Options.DocumentCapabilities.Tables = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditContent.Options.DocumentCapabilities.Paragraphs = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditContent.Options.DocumentCapabilities.ParagraphFormatting = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditContent.Options.DocumentCapabilities.Numbering.Bulleted = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditContent.Options.DocumentCapabilities.Numbering.Simple = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditContent.Options.DocumentCapabilities.Numbering.MultiLevel = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEditContent.Size = new System.Drawing.Size(865, 325);
            this.richEditContent.TabIndex = 7;
            // 
            // lblContent
            // 
            this.lblContent.Location = new System.Drawing.Point(15, 230);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(31, 13);
            this.lblContent.TabIndex = 6;
            this.lblContent.Text = "İçerik:";
            // 
            // cmbParentPage
            // 
            this.cmbParentPage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbParentPage.Enabled = false;
            this.cmbParentPage.Location = new System.Drawing.Point(100, 195);
            this.cmbParentPage.MenuManager = this.ribbonControl1;
            this.cmbParentPage.Name = "cmbParentPage";
            this.cmbParentPage.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbParentPage.Properties.NullText = "(Üst Sayfa Yok)";
            this.cmbParentPage.Size = new System.Drawing.Size(780, 20);
            this.cmbParentPage.TabIndex = 5;
            // 
            // lblParent
            // 
            this.lblParent.Location = new System.Drawing.Point(15, 198);
            this.lblParent.Name = "lblParent";
            this.lblParent.Size = new System.Drawing.Size(51, 13);
            this.lblParent.TabIndex = 4;
            this.lblParent.Text = "Üst Sayfa:";
            // 
            // txtSummary
            // 
            this.txtSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSummary.Enabled = false;
            this.txtSummary.Location = new System.Drawing.Point(100, 100);
            this.txtSummary.MenuManager = this.ribbonControl1;
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.Size = new System.Drawing.Size(780, 80);
            this.txtSummary.TabIndex = 3;
            // 
            // lblSummary
            // 
            this.lblSummary.Location = new System.Drawing.Point(15, 103);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(27, 13);
            this.lblSummary.TabIndex = 2;
            this.lblSummary.Text = "Özet:";
            // 
            // txtTitle
            // 
            this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitle.Enabled = false;
            this.txtTitle.Location = new System.Drawing.Point(100, 60);
            this.txtTitle.MenuManager = this.ribbonControl1;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(780, 20);
            this.txtTitle.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(15, 63);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(30, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Başlık:";
            // 
            // WikiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 800);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.splitter);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "WikiForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wiki Yönetimi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.WikiForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelLeft)).EndInit();
            this.panelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelRight)).EndInit();
            this.panelRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupDetails)).EndInit();
            this.groupDetails.ResumeLayout(false);
            this.groupDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelToolbar)).EndInit();
            this.panelToolbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbParentPage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSummary.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

