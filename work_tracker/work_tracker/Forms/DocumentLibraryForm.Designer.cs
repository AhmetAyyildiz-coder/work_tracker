using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace work_tracker.Forms
{
    partial class DocumentLibraryForm
    {
        private System.ComponentModel.IContainer components = null;

        private PanelControl panelControl1;
        private GridControl gridControl1;
        private GridView gridView1;
        private LabelControl lblSearch;
        private TextEdit txtSearch;
        private LabelControl lblProject;
        private LookUpEdit cmbProject;
        private LabelControl lblTag;
        private LookUpEdit cmbTag;
        private LabelControl lblFileType;
        private ComboBoxEdit cmbFileType;
        private CheckEdit chkFavorites;
        private SimpleButton btnAdd;
        private SimpleButton btnEdit;
        private SimpleButton btnDelete;
        private SimpleButton btnOpen;
        private SimpleButton btnToggleFavorite;
        private SimpleButton btnOpenLocation;
        private SimpleButton btnManageTags;
        private SimpleButton btnRefresh;
        private SimpleButton btnClearFilters;
        private SimpleButton btnOpenRepository;
        private LabelControl lblStatus;
        private LabelControl lblRepositoryPath;

        private void InitializeComponent()
        {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblSearch = new DevExpress.XtraEditors.LabelControl();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            this.lblProject = new DevExpress.XtraEditors.LabelControl();
            this.cmbProject = new DevExpress.XtraEditors.LookUpEdit();
            this.lblTag = new DevExpress.XtraEditors.LabelControl();
            this.cmbTag = new DevExpress.XtraEditors.LookUpEdit();
            this.lblFileType = new DevExpress.XtraEditors.LabelControl();
            this.cmbFileType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkFavorites = new DevExpress.XtraEditors.CheckEdit();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpen = new DevExpress.XtraEditors.SimpleButton();
            this.btnToggleFavorite = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpenLocation = new DevExpress.XtraEditors.SimpleButton();
            this.btnManageTags = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.btnClearFilters = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpenRepository = new DevExpress.XtraEditors.SimpleButton();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.lblRepositoryPath = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTag.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFileType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFavorites.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblSearch);
            this.panelControl1.Controls.Add(this.txtSearch);
            this.panelControl1.Controls.Add(this.lblProject);
            this.panelControl1.Controls.Add(this.cmbProject);
            this.panelControl1.Controls.Add(this.lblTag);
            this.panelControl1.Controls.Add(this.cmbTag);
            this.panelControl1.Controls.Add(this.lblFileType);
            this.panelControl1.Controls.Add(this.cmbFileType);
            this.panelControl1.Controls.Add(this.chkFavorites);
            this.panelControl1.Controls.Add(this.btnAdd);
            this.panelControl1.Controls.Add(this.btnEdit);
            this.panelControl1.Controls.Add(this.btnDelete);
            this.panelControl1.Controls.Add(this.btnOpen);
            this.panelControl1.Controls.Add(this.btnToggleFavorite);
            this.panelControl1.Controls.Add(this.btnOpenLocation);
            this.panelControl1.Controls.Add(this.btnManageTags);
            this.panelControl1.Controls.Add(this.btnRefresh);
            this.panelControl1.Controls.Add(this.btnClearFilters);
            this.panelControl1.Controls.Add(this.btnOpenRepository);
            this.panelControl1.Controls.Add(this.lblStatus);
            this.panelControl1.Controls.Add(this.lblRepositoryPath);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1400, 100);
            this.panelControl1.TabIndex = 0;
            // 
            // lblSearch
            // 
            this.lblSearch.Location = new System.Drawing.Point(10, 15);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(33, 13);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Arama:";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(55, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(180, 20);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // lblProject
            // 
            this.lblProject.Location = new System.Drawing.Point(250, 15);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(28, 13);
            this.lblProject.TabIndex = 2;
            this.lblProject.Text = "Proje:";
            // 
            // cmbProject
            // 
            this.cmbProject.Location = new System.Drawing.Point(285, 12);
            this.cmbProject.Name = "cmbProject";
            this.cmbProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbProject.Properties.NullText = "(Tüm Projeler)";
            this.cmbProject.Size = new System.Drawing.Size(150, 20);
            this.cmbProject.TabIndex = 3;
            this.cmbProject.EditValueChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // lblTag
            // 
            this.lblTag.Location = new System.Drawing.Point(450, 15);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(30, 13);
            this.lblTag.TabIndex = 4;
            this.lblTag.Text = "Etiket:";
            // 
            // cmbTag
            // 
            this.cmbTag.Location = new System.Drawing.Point(490, 12);
            this.cmbTag.Name = "cmbTag";
            this.cmbTag.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTag.Properties.NullText = "(Tüm Etiketler)";
            this.cmbTag.Size = new System.Drawing.Size(140, 20);
            this.cmbTag.TabIndex = 5;
            this.cmbTag.EditValueChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // lblFileType
            // 
            this.lblFileType.Location = new System.Drawing.Point(645, 15);
            this.lblFileType.Name = "lblFileType";
            this.lblFileType.Size = new System.Drawing.Size(21, 13);
            this.lblFileType.TabIndex = 6;
            this.lblFileType.Text = "Tür:";
            // 
            // cmbFileType
            // 
            this.cmbFileType.Location = new System.Drawing.Point(675, 12);
            this.cmbFileType.Name = "cmbFileType";
            this.cmbFileType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbFileType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbFileType.Size = new System.Drawing.Size(100, 20);
            this.cmbFileType.TabIndex = 7;
            this.cmbFileType.SelectedIndexChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // chkFavorites
            // 
            this.chkFavorites.Location = new System.Drawing.Point(790, 12);
            this.chkFavorites.Name = "chkFavorites";
            this.chkFavorites.Properties.Caption = "⭐ Favoriler";
            this.chkFavorites.Size = new System.Drawing.Size(90, 19);
            this.chkFavorites.TabIndex = 8;
            this.chkFavorites.CheckedChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // btnClearFilters
            // 
            this.btnClearFilters.Location = new System.Drawing.Point(890, 10);
            this.btnClearFilters.Name = "btnClearFilters";
            this.btnClearFilters.Size = new System.Drawing.Size(90, 25);
            this.btnClearFilters.TabIndex = 9;
            this.btnClearFilters.Text = "Filtreleri Temizle";
            this.btnClearFilters.Click += new System.EventHandler(this.btnClearFilters_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(124)))), ((int)(((byte)(16)))));
            this.btnAdd.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Appearance.Options.UseBackColor = true;
            this.btnAdd.Appearance.Options.UseForeColor = true;
            this.btnAdd.Location = new System.Drawing.Point(10, 55);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 30);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "📄 Ekle";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(115, 55);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(90, 30);
            this.btnEdit.TabIndex = 11;
            this.btnEdit.Text = "✏️ Düzenle";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.btnDelete.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Appearance.Options.UseBackColor = true;
            this.btnDelete.Appearance.Options.UseForeColor = true;
            this.btnDelete.Location = new System.Drawing.Point(210, 55);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 30);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "🗑️ Sil";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.btnOpen.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnOpen.Appearance.Options.UseBackColor = true;
            this.btnOpen.Appearance.Options.UseForeColor = true;
            this.btnOpen.Location = new System.Drawing.Point(310, 55);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(100, 30);
            this.btnOpen.TabIndex = 13;
            this.btnOpen.Text = "📂 Aç";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnOpenLocation
            // 
            this.btnOpenLocation.Location = new System.Drawing.Point(415, 55);
            this.btnOpenLocation.Name = "btnOpenLocation";
            this.btnOpenLocation.Size = new System.Drawing.Size(100, 30);
            this.btnOpenLocation.TabIndex = 14;
            this.btnOpenLocation.Text = "📁 Konum Aç";
            this.btnOpenLocation.Click += new System.EventHandler(this.btnOpenLocation_Click);
            // 
            // btnToggleFavorite
            // 
            this.btnToggleFavorite.Location = new System.Drawing.Point(520, 55);
            this.btnToggleFavorite.Name = "btnToggleFavorite";
            this.btnToggleFavorite.Size = new System.Drawing.Size(100, 30);
            this.btnToggleFavorite.TabIndex = 15;
            this.btnToggleFavorite.Text = "⭐ Favori";
            this.btnToggleFavorite.Click += new System.EventHandler(this.btnToggleFavorite_Click);
            // 
            // btnManageTags
            // 
            this.btnManageTags.Location = new System.Drawing.Point(640, 55);
            this.btnManageTags.Name = "btnManageTags";
            this.btnManageTags.Size = new System.Drawing.Size(100, 30);
            this.btnManageTags.TabIndex = 16;
            this.btnManageTags.Text = "🏷️ Etiketler";
            this.btnManageTags.Click += new System.EventHandler(this.btnManageTags_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(760, 55);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 30);
            this.btnRefresh.TabIndex = 17;
            this.btnRefresh.Text = "🔄 Yenile";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(1000, 62);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 18;
            // 
            // btnOpenRepository
            // 
            this.btnOpenRepository.Location = new System.Drawing.Point(860, 55);
            this.btnOpenRepository.Name = "btnOpenRepository";
            this.btnOpenRepository.Size = new System.Drawing.Size(120, 30);
            this.btnOpenRepository.TabIndex = 19;
            this.btnOpenRepository.Text = "📂 Depo Klasörü";
            this.btnOpenRepository.Click += new System.EventHandler(this.btnOpenRepository_Click);
            // 
            // lblRepositoryPath
            // 
            this.lblRepositoryPath.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblRepositoryPath.Appearance.Options.UseForeColor = true;
            this.lblRepositoryPath.Location = new System.Drawing.Point(1000, 15);
            this.lblRepositoryPath.Name = "lblRepositoryPath";
            this.lblRepositoryPath.Size = new System.Drawing.Size(0, 13);
            this.lblRepositoryPath.TabIndex = 20;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 100);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1400, 600);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // DocumentLibraryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 700);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "DocumentLibraryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "📚 Döküman Kütüphanesi";
            this.Load += new System.EventHandler(this.DocumentLibraryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTag.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFileType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFavorites.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
