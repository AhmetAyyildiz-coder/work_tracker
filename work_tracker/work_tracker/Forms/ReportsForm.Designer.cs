using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using DevExpress.XtraRichEdit;

namespace work_tracker.Forms
{
    partial class ReportsForm
    {
        private System.ComponentModel.IContainer components = null;
        private XtraTabControl tabControl1;
        private XtraTabPage tabCapacity;
        private XtraTabPage tabWorkDistribution;
        private XtraTabPage tabSprintPerformance;
        private XtraTabPage tabEffortTrend;
        private RichEditControl richEditCapacity;
        private RichEditControl richEditWorkDistribution;
        private RichEditControl richEditSprintPerformance;
        private RichEditControl richEditEffortTrend;
        private SimpleButton btnRefresh;
        private PanelControl panelControl1;

        private void InitializeComponent()
        {
            this.tabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabCapacity = new DevExpress.XtraTab.XtraTabPage();
            this.richEditCapacity = new DevExpress.XtraRichEdit.RichEditControl();
            this.tabWorkDistribution = new DevExpress.XtraTab.XtraTabPage();
            this.richEditWorkDistribution = new DevExpress.XtraRichEdit.RichEditControl();
            this.tabSprintPerformance = new DevExpress.XtraTab.XtraTabPage();
            this.richEditSprintPerformance = new DevExpress.XtraRichEdit.RichEditControl();
            this.tabEffortTrend = new DevExpress.XtraTab.XtraTabPage();
            this.richEditEffortTrend = new DevExpress.XtraRichEdit.RichEditControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabCapacity.SuspendLayout();
            this.tabWorkDistribution.SuspendLayout();
            this.tabSprintPerformance.SuspendLayout();
            this.tabEffortTrend.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnRefresh);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1200, 50);
            this.panelControl1.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(10, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Yenile";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 50);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabPage = this.tabCapacity;
            this.tabControl1.Size = new System.Drawing.Size(1200, 650);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabCapacity,
            this.tabWorkDistribution,
            this.tabSprintPerformance,
            this.tabEffortTrend});
            // 
            // tabCapacity
            // 
            this.tabCapacity.Controls.Add(this.richEditCapacity);
            this.tabCapacity.Name = "tabCapacity";
            this.tabCapacity.Size = new System.Drawing.Size(1194, 622);
            this.tabCapacity.Text = "Kapasite Dağılımı";
            // 
            // richEditCapacity
            // 
            this.richEditCapacity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richEditCapacity.Location = new System.Drawing.Point(0, 0);
            this.richEditCapacity.Name = "richEditCapacity";
            this.richEditCapacity.Options.DocumentCapabilities.CharacterFormatting = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditCapacity.Options.DocumentCapabilities.CharacterStyle = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditCapacity.Options.DocumentCapabilities.Hyperlinks = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditCapacity.Options.DocumentCapabilities.Numbering.Bulleted = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditCapacity.Options.DocumentCapabilities.Numbering.Simple = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditCapacity.Options.DocumentCapabilities.Paragraphs = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditCapacity.Options.DocumentCapabilities.ParagraphFormatting = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditCapacity.Options.DocumentCapabilities.ParagraphStyle = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditCapacity.Options.DocumentCapabilities.Tables = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditCapacity.ReadOnly = true;
            this.richEditCapacity.Size = new System.Drawing.Size(1194, 622);
            this.richEditCapacity.TabIndex = 0;
            // 
            // tabWorkDistribution
            // 
            this.tabWorkDistribution.Controls.Add(this.richEditWorkDistribution);
            this.tabWorkDistribution.Name = "tabWorkDistribution";
            this.tabWorkDistribution.Size = new System.Drawing.Size(1194, 622);
            this.tabWorkDistribution.Text = "İş Dağılımı";
            // 
            // richEditWorkDistribution
            // 
            this.richEditWorkDistribution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richEditWorkDistribution.Location = new System.Drawing.Point(0, 0);
            this.richEditWorkDistribution.Name = "richEditWorkDistribution";
            this.richEditWorkDistribution.Options.DocumentCapabilities.CharacterFormatting = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditWorkDistribution.Options.DocumentCapabilities.CharacterStyle = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditWorkDistribution.Options.DocumentCapabilities.Hyperlinks = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditWorkDistribution.Options.DocumentCapabilities.Numbering.Bulleted = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditWorkDistribution.Options.DocumentCapabilities.Numbering.Simple = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditWorkDistribution.Options.DocumentCapabilities.Paragraphs = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditWorkDistribution.Options.DocumentCapabilities.ParagraphFormatting = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditWorkDistribution.Options.DocumentCapabilities.ParagraphStyle = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditWorkDistribution.Options.DocumentCapabilities.Tables = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditWorkDistribution.ReadOnly = true;
            this.richEditWorkDistribution.Size = new System.Drawing.Size(1194, 622);
            this.richEditWorkDistribution.TabIndex = 0;
            // 
            // tabSprintPerformance
            // 
            this.tabSprintPerformance.Controls.Add(this.richEditSprintPerformance);
            this.tabSprintPerformance.Name = "tabSprintPerformance";
            this.tabSprintPerformance.Size = new System.Drawing.Size(1194, 622);
            this.tabSprintPerformance.Text = "Sprint Performansı";
            // 
            // richEditSprintPerformance
            // 
            this.richEditSprintPerformance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richEditSprintPerformance.Location = new System.Drawing.Point(0, 0);
            this.richEditSprintPerformance.Name = "richEditSprintPerformance";
            this.richEditSprintPerformance.Options.DocumentCapabilities.CharacterFormatting = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditSprintPerformance.Options.DocumentCapabilities.CharacterStyle = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditSprintPerformance.Options.DocumentCapabilities.Hyperlinks = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditSprintPerformance.Options.DocumentCapabilities.Numbering.Bulleted = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditSprintPerformance.Options.DocumentCapabilities.Numbering.Simple = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditSprintPerformance.Options.DocumentCapabilities.Paragraphs = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditSprintPerformance.Options.DocumentCapabilities.ParagraphFormatting = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditSprintPerformance.Options.DocumentCapabilities.ParagraphStyle = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditSprintPerformance.Options.DocumentCapabilities.Tables = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditSprintPerformance.ReadOnly = true;
            this.richEditSprintPerformance.Size = new System.Drawing.Size(1194, 622);
            this.richEditSprintPerformance.TabIndex = 0;
            // 
            // tabEffortTrend
            // 
            this.tabEffortTrend.Controls.Add(this.richEditEffortTrend);
            this.tabEffortTrend.Name = "tabEffortTrend";
            this.tabEffortTrend.Size = new System.Drawing.Size(1194, 622);
            this.tabEffortTrend.Text = "Efor Analizi ve Trend";
            // 
            // richEditEffortTrend
            // 
            this.richEditEffortTrend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richEditEffortTrend.Location = new System.Drawing.Point(0, 0);
            this.richEditEffortTrend.Name = "richEditEffortTrend";
            this.richEditEffortTrend.Options.DocumentCapabilities.CharacterFormatting = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditEffortTrend.Options.DocumentCapabilities.CharacterStyle = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditEffortTrend.Options.DocumentCapabilities.Hyperlinks = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditEffortTrend.Options.DocumentCapabilities.Numbering.Bulleted = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditEffortTrend.Options.DocumentCapabilities.Numbering.Simple = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditEffortTrend.Options.DocumentCapabilities.Paragraphs = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditEffortTrend.Options.DocumentCapabilities.ParagraphFormatting = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditEffortTrend.Options.DocumentCapabilities.ParagraphStyle = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditEffortTrend.Options.DocumentCapabilities.Tables = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            this.richEditEffortTrend.ReadOnly = true;
            this.richEditEffortTrend.Size = new System.Drawing.Size(1194, 622);
            this.richEditEffortTrend.TabIndex = 0;
            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "ReportsForm";
            this.Text = "Raporlar ve Analitik";
            this.Load += new System.EventHandler(this.ReportsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabCapacity.ResumeLayout(false);
            this.tabWorkDistribution.ResumeLayout(false);
            this.tabSprintPerformance.ResumeLayout(false);
            this.tabEffortTrend.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}

