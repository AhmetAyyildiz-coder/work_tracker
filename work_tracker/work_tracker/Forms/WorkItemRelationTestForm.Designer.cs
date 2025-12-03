using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.Drawing;

namespace work_tracker.Forms
{
    partial class WorkItemRelationTestForm
    {
        private System.ComponentModel.IContainer components = null;
        
        private GroupControl groupTestControls;
        private LabelControl labelControl1;
        private LabelControl labelControl2;
        private LookUpEdit cmbWorkItem1;
        private LookUpEdit cmbWorkItem2;
        private SimpleButton btnCreateParentRelation;
        private SimpleButton btnCreateSiblingRelation;
        private SimpleButton btnTestDeletion;
        private SimpleButton btnTestHierarchy;
        private SimpleButton btnClearAllRelations;
        
        private GroupControl groupResults;
        private ListView lstTestResults;
        private ColumnHeader colTime;
        private ColumnHeader colResult;
        private ColumnHeader colStatus;
        private LabelControl lblRelationCount;

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (components != null)
        //        {
        //            components.Dispose();
        //        }
        //    }
        //    base.Dispose(disposing);
        //}

        private void InitializeComponent()
        {
            this.groupTestControls = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cmbWorkItem1 = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbWorkItem2 = new DevExpress.XtraEditors.LookUpEdit();
            this.btnCreateParentRelation = new DevExpress.XtraEditors.SimpleButton();
            this.btnCreateSiblingRelation = new DevExpress.XtraEditors.SimpleButton();
            this.btnTestDeletion = new DevExpress.XtraEditors.SimpleButton();
            this.btnTestHierarchy = new DevExpress.XtraEditors.SimpleButton();
            this.btnClearAllRelations = new DevExpress.XtraEditors.SimpleButton();
            this.groupResults = new DevExpress.XtraEditors.GroupControl();
            this.lstTestResults = new System.Windows.Forms.ListView();
            this.colTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colResult = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblRelationCount = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupTestControls)).BeginInit();
            this.groupTestControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWorkItem1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWorkItem2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupResults)).BeginInit();
            this.groupResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupTestControls
            // 
            this.groupTestControls.Controls.Add(this.btnClearAllRelations);
            this.groupTestControls.Controls.Add(this.btnTestHierarchy);
            this.groupTestControls.Controls.Add(this.btnTestDeletion);
            this.groupTestControls.Controls.Add(this.btnCreateSiblingRelation);
            this.groupTestControls.Controls.Add(this.btnCreateParentRelation);
            this.groupTestControls.Controls.Add(this.cmbWorkItem2);
            this.groupTestControls.Controls.Add(this.cmbWorkItem1);
            this.groupTestControls.Controls.Add(this.labelControl2);
            this.groupTestControls.Controls.Add(this.labelControl1);
            this.groupTestControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupTestControls.Location = new System.Drawing.Point(0, 0);
            this.groupTestControls.Name = "groupTestControls";
            this.groupTestControls.Size = new System.Drawing.Size(800, 150);
            this.groupTestControls.Text = "İlişki Test Kontrolleri";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(20, 30);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "İş 1 (Parent):";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(20, 60);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(80, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "İş 2 (Child):";
            // 
            // cmbWorkItem1
            // 
            this.cmbWorkItem1.Location = new System.Drawing.Point(110, 27);
            this.cmbWorkItem1.Name = "cmbWorkItem1";
            this.cmbWorkItem1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWorkItem1.Properties.NullText = "İş seçin...";
            this.cmbWorkItem1.Size = new System.Drawing.Size(250, 20);
            this.cmbWorkItem1.TabIndex = 2;
            // 
            // cmbWorkItem2
            // 
            this.cmbWorkItem2.Location = new System.Drawing.Point(110, 57);
            this.cmbWorkItem2.Name = "cmbWorkItem2";
            this.cmbWorkItem2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWorkItem2.Properties.NullText = "İş seçin...";
            this.cmbWorkItem2.Size = new System.Drawing.Size(250, 20);
            this.cmbWorkItem2.TabIndex = 3;
            // 
            // btnCreateParentRelation
            // 
            this.btnCreateParentRelation.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnCreateParentRelation.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnCreateParentRelation.Appearance.Options.UseBackColor = true;
            this.btnCreateParentRelation.Appearance.Options.UseFont = true;
            this.btnCreateParentRelation.Location = new System.Drawing.Point(20, 90);
            this.btnCreateParentRelation.Name = "btnCreateParentRelation";
            this.btnCreateParentRelation.Size = new System.Drawing.Size(150, 30);
            this.btnCreateParentRelation.TabIndex = 4;
            this.btnCreateParentRelation.Text = "🔺 Parent İlişkisi Oluştur";
            this.btnCreateParentRelation.Click += new System.EventHandler(this.btnCreateParentRelation_Click);
            // 
            // btnCreateSiblingRelation
            // 
            this.btnCreateSiblingRelation.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnCreateSiblingRelation.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnCreateSiblingRelation.Appearance.Options.UseBackColor = true;
            this.btnCreateSiblingRelation.Appearance.Options.UseFont = true;
            this.btnCreateSiblingRelation.Location = new System.Drawing.Point(180, 90);
            this.btnCreateSiblingRelation.Name = "btnCreateSiblingRelation";
            this.btnCreateSiblingRelation.Size = new System.Drawing.Size(150, 30);
            this.btnCreateSiblingRelation.TabIndex = 5;
            this.btnCreateSiblingRelation.Text = "🔗 Sibling İlişkisi Oluştur";
            this.btnCreateSiblingRelation.Click += new System.EventHandler(this.btnCreateSiblingRelation_Click);
            // 
            // btnTestDeletion
            // 
            this.btnTestDeletion.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnTestDeletion.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnTestDeletion.Appearance.Options.UseBackColor = true;
            this.btnTestDeletion.Appearance.Options.UseFont = true;
            this.btnTestDeletion.Location = new System.Drawing.Point(340, 90);
            this.btnTestDeletion.Name = "btnTestDeletion";
            this.btnTestDeletion.Size = new System.Drawing.Size(120, 30);
            this.btnTestDeletion.TabIndex = 6;
            this.btnTestDeletion.Text = "🗑️ Silme Testi";
            this.btnTestDeletion.Click += new System.EventHandler(this.btnTestDeletion_Click);
            // 
            // btnTestHierarchy
            // 
            this.btnTestHierarchy.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnTestHierarchy.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnTestHierarchy.Appearance.Options.UseBackColor = true;
            this.btnTestHierarchy.Appearance.Options.UseFont = true;
            this.btnTestHierarchy.Location = new System.Drawing.Point(470, 90);
            this.btnTestHierarchy.Name = "btnTestHierarchy";
            this.btnTestHierarchy.Size = new System.Drawing.Size(120, 30);
            this.btnTestHierarchy.TabIndex = 7;
            this.btnTestHierarchy.Text = "🌳 Hiyerarşi Testi";
            this.btnTestHierarchy.Click += new System.EventHandler(this.btnTestHierarchy_Click);
            // 
            // btnClearAllRelations
            // 
            this.btnClearAllRelations.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.btnClearAllRelations.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnClearAllRelations.Appearance.Options.UseBackColor = true;
            this.btnClearAllRelations.Appearance.Options.UseFont = true;
            this.btnClearAllRelations.Location = new System.Drawing.Point(600, 90);
            this.btnClearAllRelations.Name = "btnClearAllRelations";
            this.btnClearAllRelations.Size = new System.Drawing.Size(150, 30);
            this.btnClearAllRelations.TabIndex = 8;
            this.btnClearAllRelations.Text = "🧹 Tüm İlişkileri Temizle";
            this.btnClearAllRelations.Click += new System.EventHandler(this.btnClearAllRelations_Click);
            // 
            // groupResults
            // 
            this.groupResults.Controls.Add(this.lblRelationCount);
            this.groupResults.Controls.Add(this.lstTestResults);
            this.groupResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupResults.Location = new System.Drawing.Point(0, 150);
            this.groupResults.Name = "groupResults";
            this.groupResults.Size = new System.Drawing.Size(800, 450);
            this.groupResults.Text = "Test Sonuçları";
            // 
            // lstTestResults
            // 
            this.lstTestResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstTestResults.FullRowSelect = true;
            this.lstTestResults.GridLines = true;
            this.lstTestResults.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstTestResults.HideSelection = false;
            this.lstTestResults.Location = new System.Drawing.Point(2, 22);
            this.lstTestResults.MultiSelect = false;
            this.lstTestResults.Name = "lstTestResults";
            this.lstTestResults.Size = new System.Drawing.Size(796, 380);
            this.lstTestResults.TabIndex = 0;
            this.lstTestResults.UseCompatibleStateImageBehavior = false;
            this.lstTestResults.View = System.Windows.Forms.View.Details;
            // 
            // colTime
            // 
            this.colTime.Text = "Saat";
            this.colTime.Width = 80;
            // 
            // colResult
            // 
            this.colResult.Text = "Sonuç";
            this.colResult.Width = 500;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Durum";
            this.colStatus.Width = 100;
            // 
            // lblRelationCount
            // 
            this.lblRelationCount.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblRelationCount.Appearance.Options.UseFont = true;
            this.lblRelationCount.Location = new System.Drawing.Point(15, 30);
            this.lblRelationCount.Name = "lblRelationCount";
            this.lblRelationCount.Size = new System.Drawing.Size(200, 13);
            this.lblRelationCount.TabIndex = 1;
            this.lblRelationCount.Text = "Toplam İlişki: 0";
            // 
            // WorkItemRelationTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.groupResults);
            this.Controls.Add(this.groupTestControls);
            this.Name = "WorkItemRelationTestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WorkItem İlişki Test Aracı";
            this.Load += new System.EventHandler(this.WorkItemRelationTestForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupTestControls)).EndInit();
            this.groupTestControls.ResumeLayout(false);
            this.groupTestControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWorkItem1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWorkItem2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupResults)).EndInit();
            this.groupResults.ResumeLayout(false);
            this.groupResults.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}