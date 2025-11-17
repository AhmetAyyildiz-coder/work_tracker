using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace work_tracker.Forms
{
    partial class AllWorkItemsForm
    {
        private GridControl gridControl1;
        private GridView gridView1;
        private PanelControl panelControl1;
        private SimpleButton btnViewDetails;
        private SimpleButton btnEditWorkItem;
        private SimpleButton btnDeleteWorkItem;
        private SimpleButton btnArchiveWorkItem;
        private SimpleButton btnRefresh;
        private SimpleButton btnClearFilters;
        private SimpleButton btnSearch;
        private LabelControl lblTotalCount;
        private ComboBoxEdit cmbBoardFilter;
        private ComboBoxEdit cmbStatusFilter;
        private ComboBoxEdit cmbTypeFilter;
        private ComboBoxEdit cmbUrgencyFilter;
        private TextEdit txtSearch;
        private LabelControl lblBoard;
        private LabelControl lblStatus;
        private LabelControl lblType;
        private LabelControl lblUrgency;
        private LabelControl lblSearch;

        private void InitializeComponent()
        {
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnViewDetails = new DevExpress.XtraEditors.SimpleButton();
            this.btnEditWorkItem = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteWorkItem = new DevExpress.XtraEditors.SimpleButton();
            this.btnArchiveWorkItem = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.btnClearFilters = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.lblTotalCount = new DevExpress.XtraEditors.LabelControl();
            this.cmbBoardFilter = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbStatusFilter = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbTypeFilter = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbUrgencyFilter = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            this.lblBoard = new DevExpress.XtraEditors.LabelControl();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.lblType = new DevExpress.XtraEditors.LabelControl();
            this.lblUrgency = new DevExpress.XtraEditors.LabelControl();
            this.lblSearch = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBoardFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStatusFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTypeFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUrgencyFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 110);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1400, 590);
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
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblSearch);
            this.panelControl1.Controls.Add(this.txtSearch);
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Controls.Add(this.lblUrgency);
            this.panelControl1.Controls.Add(this.cmbUrgencyFilter);
            this.panelControl1.Controls.Add(this.lblType);
            this.panelControl1.Controls.Add(this.cmbTypeFilter);
            this.panelControl1.Controls.Add(this.lblStatus);
            this.panelControl1.Controls.Add(this.cmbStatusFilter);
            this.panelControl1.Controls.Add(this.lblBoard);
            this.panelControl1.Controls.Add(this.cmbBoardFilter);
            this.panelControl1.Controls.Add(this.lblTotalCount);
            this.panelControl1.Controls.Add(this.btnClearFilters);
            this.panelControl1.Controls.Add(this.btnRefresh);
            this.panelControl1.Controls.Add(this.btnArchiveWorkItem);
            this.panelControl1.Controls.Add(this.btnDeleteWorkItem);
            this.panelControl1.Controls.Add(this.btnEditWorkItem);
            this.panelControl1.Controls.Add(this.btnViewDetails);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1400, 110);
            this.panelControl1.TabIndex = 1;
            // 
            // btnViewDetails
            // 
            this.btnViewDetails.Location = new System.Drawing.Point(10, 10);
            this.btnViewDetails.Name = "btnViewDetails";
            this.btnViewDetails.Size = new System.Drawing.Size(100, 30);
            this.btnViewDetails.TabIndex = 0;
            this.btnViewDetails.Text = "Detay Görüntüle";
            this.btnViewDetails.Click += new System.EventHandler(this.btnViewDetails_Click);
            // 
            // btnEditWorkItem
            // 
            this.btnEditWorkItem.Location = new System.Drawing.Point(120, 10);
            this.btnEditWorkItem.Name = "btnEditWorkItem";
            this.btnEditWorkItem.Size = new System.Drawing.Size(100, 30);
            this.btnEditWorkItem.TabIndex = 1;
            this.btnEditWorkItem.Text = "Düzenle";
            this.btnEditWorkItem.Click += new System.EventHandler(this.btnEditWorkItem_Click);
            // 
            // btnDeleteWorkItem
            // 
            this.btnDeleteWorkItem.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDeleteWorkItem.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnDeleteWorkItem.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnDeleteWorkItem.Appearance.Options.UseBackColor = true;
            this.btnDeleteWorkItem.Appearance.Options.UseFont = true;
            this.btnDeleteWorkItem.Appearance.Options.UseForeColor = true;
            this.btnDeleteWorkItem.Location = new System.Drawing.Point(230, 10);
            this.btnDeleteWorkItem.Name = "btnDeleteWorkItem";
            this.btnDeleteWorkItem.Size = new System.Drawing.Size(100, 30);
            this.btnDeleteWorkItem.TabIndex = 2;
            this.btnDeleteWorkItem.Text = "🗑️ Sil";
            this.btnDeleteWorkItem.Click += new System.EventHandler(this.btnDeleteWorkItem_Click);
            // 
            // btnArchiveWorkItem
            // 
            this.btnArchiveWorkItem.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnArchiveWorkItem.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnArchiveWorkItem.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnArchiveWorkItem.Appearance.Options.UseBackColor = true;
            this.btnArchiveWorkItem.Appearance.Options.UseFont = true;
            this.btnArchiveWorkItem.Appearance.Options.UseForeColor = true;
            this.btnArchiveWorkItem.Location = new System.Drawing.Point(340, 10);
            this.btnArchiveWorkItem.Name = "btnArchiveWorkItem";
            this.btnArchiveWorkItem.Size = new System.Drawing.Size(100, 30);
            this.btnArchiveWorkItem.TabIndex = 3;
            this.btnArchiveWorkItem.Text = "📦 Arşivle";
            this.btnArchiveWorkItem.Click += new System.EventHandler(this.btnArchiveWorkItem_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(450, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Yenile";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClearFilters
            // 
            this.btnClearFilters.Location = new System.Drawing.Point(560, 10);
            this.btnClearFilters.Name = "btnClearFilters";
            this.btnClearFilters.Size = new System.Drawing.Size(100, 30);
            this.btnClearFilters.TabIndex = 5;
            this.btnClearFilters.Text = "Filtreleri Temizle";
            this.btnClearFilters.Click += new System.EventHandler(this.btnClearFilters_Click);
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.Location = new System.Drawing.Point(10, 50);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(150, 13);
            this.lblTotalCount.TabIndex = 7;
            this.lblTotalCount.Text = "Toplam: 0 iş talebi";
            // 
            // lblSearch
            // 
            this.lblSearch.Location = new System.Drawing.Point(200, 53);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(28, 13);
            this.lblSearch.TabIndex = 8;
            this.lblSearch.Text = "Ara:";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(240, 50);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(300, 20);
            this.txtSearch.TabIndex = 9;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(550, 48);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(60, 24);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "Ara";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblBoard
            // 
            this.lblBoard.Location = new System.Drawing.Point(10, 83);
            this.lblBoard.Name = "lblBoard";
            this.lblBoard.Size = new System.Drawing.Size(28, 13);
            this.lblBoard.TabIndex = 11;
            this.lblBoard.Text = "Pano:";
            // 
            // cmbBoardFilter
            // 
            this.cmbBoardFilter.Location = new System.Drawing.Point(50, 80);
            this.cmbBoardFilter.Name = "cmbBoardFilter";
            this.cmbBoardFilter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBoardFilter.Size = new System.Drawing.Size(120, 20);
            this.cmbBoardFilter.TabIndex = 12;
            this.cmbBoardFilter.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(190, 83);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(33, 13);
            this.lblStatus.TabIndex = 13;
            this.lblStatus.Text = "Durum:";
            // 
            // cmbStatusFilter
            // 
            this.cmbStatusFilter.Location = new System.Drawing.Point(230, 80);
            this.cmbStatusFilter.Name = "cmbStatusFilter";
            this.cmbStatusFilter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbStatusFilter.Size = new System.Drawing.Size(120, 20);
            this.cmbStatusFilter.TabIndex = 14;
            this.cmbStatusFilter.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
            // 
            // lblType
            // 
            this.lblType.Location = new System.Drawing.Point(370, 83);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(22, 13);
            this.lblType.TabIndex = 15;
            this.lblType.Text = "Tip:";
            // 
            // cmbTypeFilter
            // 
            this.cmbTypeFilter.Location = new System.Drawing.Point(410, 80);
            this.cmbTypeFilter.Name = "cmbTypeFilter";
            this.cmbTypeFilter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTypeFilter.Size = new System.Drawing.Size(120, 20);
            this.cmbTypeFilter.TabIndex = 16;
            this.cmbTypeFilter.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
            // 
            // lblUrgency
            // 
            this.lblUrgency.Location = new System.Drawing.Point(550, 83);
            this.lblUrgency.Name = "lblUrgency";
            this.lblUrgency.Size = new System.Drawing.Size(40, 13);
            this.lblUrgency.TabIndex = 17;
            this.lblUrgency.Text = "Aciliyet:";
            // 
            // cmbUrgencyFilter
            // 
            this.cmbUrgencyFilter.Location = new System.Drawing.Point(600, 80);
            this.cmbUrgencyFilter.Name = "cmbUrgencyFilter";
            this.cmbUrgencyFilter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbUrgencyFilter.Size = new System.Drawing.Size(120, 20);
            this.cmbUrgencyFilter.TabIndex = 18;
            this.cmbUrgencyFilter.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
            // 
            // AllWorkItemsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 700);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "AllWorkItemsForm";
            this.Text = "Tüm İş Talepleri";
            this.Load += new System.EventHandler(this.AllWorkItemsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBoardFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStatusFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTypeFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUrgencyFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            this.ResumeLayout(false);
        }
    }
}

