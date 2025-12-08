using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace work_tracker.Forms
{
    partial class InboxForm
    {
        private System.ComponentModel.IContainer components = null;
        private GridControl gridControl1;
        private GridView gridView1;
        private SimpleButton btnNewWorkItem;
        private SimpleButton btnEditWorkItem;
        private SimpleButton btnDeleteWorkItem;
        private SimpleButton btnRefresh;
        private SimpleButton btnSendToTriage;
        private SimpleButton btnSendToOtopark;
        private PanelControl panelControl1;

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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnNewWorkItem = new DevExpress.XtraEditors.SimpleButton();
            this.btnEditWorkItem = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteWorkItem = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.btnSendToTriage = new DevExpress.XtraEditors.SimpleButton();
            this.btnSendToOtopark = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 50);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1200, 650);
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
            // btnNewWorkItem
            // 
            this.btnNewWorkItem.Location = new System.Drawing.Point(10, 10);
            this.btnNewWorkItem.Name = "btnNewWorkItem";
            this.btnNewWorkItem.Size = new System.Drawing.Size(120, 30);
            this.btnNewWorkItem.TabIndex = 1;
            this.btnNewWorkItem.Text = "Yeni İş Talebi";
            this.btnNewWorkItem.Click += new System.EventHandler(this.btnNewWorkItem_Click);
            // 
            // btnEditWorkItem
            // 
            this.btnEditWorkItem.Location = new System.Drawing.Point(140, 10);
            this.btnEditWorkItem.Name = "btnEditWorkItem";
            this.btnEditWorkItem.Size = new System.Drawing.Size(100, 30);
            this.btnEditWorkItem.TabIndex = 2;
            this.btnEditWorkItem.Text = "Düzenle";
            this.btnEditWorkItem.Click += new System.EventHandler(this.btnEditWorkItem_Click);
            // 
            // btnDeleteWorkItem
            // 
            this.btnDeleteWorkItem.Location = new System.Drawing.Point(250, 10);
            this.btnDeleteWorkItem.Name = "btnDeleteWorkItem";
            this.btnDeleteWorkItem.Size = new System.Drawing.Size(100, 30);
            this.btnDeleteWorkItem.TabIndex = 3;
            this.btnDeleteWorkItem.Text = "Sil";
            this.btnDeleteWorkItem.Click += new System.EventHandler(this.btnDeleteWorkItem_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(360, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Yenile";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSendToTriage
            // 
            this.btnSendToTriage.Location = new System.Drawing.Point(480, 10);
            this.btnSendToTriage.Name = "btnSendToTriage";
            this.btnSendToTriage.Size = new System.Drawing.Size(150, 30);
            this.btnSendToTriage.TabIndex = 5;
            this.btnSendToTriage.Text = "Sınıflandır >";
            this.btnSendToTriage.Click += new System.EventHandler(this.btnSendToTriage_Click);
            // 
            // btnSendToOtopark
            // 
            this.btnSendToOtopark.Location = new System.Drawing.Point(640, 10);
            this.btnSendToOtopark.Name = "btnSendToOtopark";
            this.btnSendToOtopark.Size = new System.Drawing.Size(130, 30);
            this.btnSendToOtopark.TabIndex = 6;
            this.btnSendToOtopark.Text = "🚗 Otopark'a";
            this.btnSendToOtopark.Click += new System.EventHandler(this.btnSendToOtopark_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnNewWorkItem);
            this.panelControl1.Controls.Add(this.btnEditWorkItem);
            this.panelControl1.Controls.Add(this.btnDeleteWorkItem);
            this.panelControl1.Controls.Add(this.btnRefresh);
            this.panelControl1.Controls.Add(this.btnSendToTriage);
            this.panelControl1.Controls.Add(this.btnSendToOtopark);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1200, 50);
            this.panelControl1.TabIndex = 6;
            // 
            // InboxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "InboxForm";
            this.Text = "Gelen Kutusu (Inbox)";
            this.Load += new System.EventHandler(this.InboxForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}

