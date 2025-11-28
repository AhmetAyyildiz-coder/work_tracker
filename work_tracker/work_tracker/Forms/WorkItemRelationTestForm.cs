using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using work_tracker.Data;
using work_tracker.Data.Entities;
using work_tracker.Services;

namespace work_tracker.Forms
{
    public partial class WorkItemRelationTestForm : XtraForm
    {
        private WorkTrackerDbContext _context;
        private WorkItemRelationService _relationService;

        public WorkItemRelationTestForm()
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
            _relationService = new WorkItemRelationService(_context);
        }

        private void WorkItemRelationTestForm_Load(object sender, EventArgs e)
        {
            LoadWorkItems();
            LoadTestResults();
        }

        private void LoadWorkItems()
        {
            var workItems = _context.WorkItems
                .OrderBy(w => w.Title)
                .Select(w => new { w.Id, w.Title })
                .ToList();

            cmbWorkItem1.Properties.DataSource = workItems;
            cmbWorkItem1.Properties.DisplayMember = "Title";
            cmbWorkItem1.Properties.ValueMember = "Id";

            cmbWorkItem2.Properties.DataSource = workItems;
            cmbWorkItem2.Properties.DisplayMember = "Title";
            cmbWorkItem2.Properties.ValueMember = "Id";
        }

        private void btnCreateParentRelation_Click(object sender, EventArgs e)
        {
            if (cmbWorkItem1.EditValue == null || cmbWorkItem2.EditValue == null)
            {
                XtraMessageBox.Show("Lütfen her iki iş alanını da doldurun.", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var workItemId1 = (int)cmbWorkItem1.EditValue;
            var workItemId2 = (int)cmbWorkItem2.EditValue;

            if (workItemId1 == workItemId2)
            {
                XtraMessageBox.Show("Aynı işi ilişkilendiremezsiniz.", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Mevcut ilişkiyi kontrol et
                var existingRelation = _context.WorkItemRelations
                    .FirstOrDefault(r => r.WorkItemId1 == workItemId1 && 
                                     r.WorkItemId2 == workItemId2 && 
                                     r.RelationType == WorkItemRelationTypes.Parent);

                if (existingRelation != null)
                {
                    AddTestResult("Parent ilişkisi zaten mevcut.", false);
                    return;
                }

                // Parent ilişkisi oluştur
                var relation = new WorkItemRelation
                {
                    WorkItemId1 = workItemId1,
                    WorkItemId2 = workItemId2,
                    RelationType = WorkItemRelationTypes.Parent,
                    CreatedBy = Environment.UserName
                };

                _context.WorkItemRelations.Add(relation);
                _context.SaveChanges();

                AddTestResult($"Parent ilişkisi oluşturuldu: {workItemId1} -> {workItemId2}", true);
                LoadTestResults();
            }
            catch (Exception ex)
            {
                AddTestResult($"Hata: {ex.Message}", false);
            }
        }

        private void btnCreateSiblingRelation_Click(object sender, EventArgs e)
        {
            if (cmbWorkItem1.EditValue == null || cmbWorkItem2.EditValue == null)
            {
                XtraMessageBox.Show("Lütfen her iki iş alanını da doldurun.", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var workItemId1 = (int)cmbWorkItem1.EditValue;
            var workItemId2 = (int)cmbWorkItem2.EditValue;

            if (workItemId1 == workItemId2)
            {
                XtraMessageBox.Show("Aynı işi ilişkilendiremezsiniz.", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Çift yönlü sibling ilişkisi oluştur
                var relation1 = new WorkItemRelation
                {
                    WorkItemId1 = workItemId1,
                    WorkItemId2 = workItemId2,
                    RelationType = WorkItemRelationTypes.Sibling,
                    CreatedBy = Environment.UserName
                };

                var relation2 = new WorkItemRelation
                {
                    WorkItemId1 = workItemId2,
                    WorkItemId2 = workItemId1,
                    RelationType = WorkItemRelationTypes.Sibling,
                    CreatedBy = Environment.UserName
                };

                _context.WorkItemRelations.Add(relation1);
                _context.WorkItemRelations.Add(relation2);
                _context.SaveChanges();

                AddTestResult($"Sibling ilişkisi oluşturuldu: {workItemId1} <-> {workItemId2}", true);
                LoadTestResults();
            }
            catch (Exception ex)
            {
                AddTestResult($"Hata: {ex.Message}", false);
            }
        }

        private void btnTestDeletion_Click(object sender, EventArgs e)
        {
            if (cmbWorkItem1.EditValue == null)
            {
                XtraMessageBox.Show("Lütfen silinecek işi seçin.", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var workItemId = (int)cmbWorkItem1.EditValue;

            try
            {
                var result = _relationService.HandleWorkItemDeletion(workItemId, Environment.UserName);
                AddTestResult(result.Message, result.Success);
                LoadTestResults();
            }
            catch (Exception ex)
            {
                AddTestResult($"Hata: {ex.Message}", false);
            }
        }

        private void btnTestHierarchy_Click(object sender, EventArgs e)
        {
            if (cmbWorkItem1.EditValue == null)
            {
                XtraMessageBox.Show("Lütfen bir iş seçin.", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var workItemId = (int)cmbWorkItem1.EditValue;

            try
            {
                var hierarchyPath = _relationService.GetHierarchyPath(workItemId);
                var childWorkItems = _relationService.GetAllChildWorkItems(workItemId);

                AddTestResult($"Hiyerarşi yolu: {string.Join(" -> ", hierarchyPath)}", true);
                AddTestResult($"Alt işler: {string.Join(", ", childWorkItems)}", true);
                LoadTestResults();
            }
            catch (Exception ex)
            {
                AddTestResult($"Hata: {ex.Message}", false);
            }
        }

        private void btnClearAllRelations_Click(object sender, EventArgs e)
        {
            if (cmbWorkItem1.EditValue == null)
            {
                XtraMessageBox.Show("Lütfen bir iş seçin.", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var workItemId = (int)cmbWorkItem1.EditValue;

            try
            {
                var result = _relationService.DeleteAllRelations(workItemId, Environment.UserName);
                AddTestResult(result.Message, result.Success);
                LoadTestResults();
            }
            catch (Exception ex)
            {
                AddTestResult($"Hata: {ex.Message}", false);
            }
        }

        private void AddTestResult(string message, bool success)
        {
            var item = new ListViewItem(DateTime.Now.ToString("HH:mm:ss"));
            item.SubItems.Add(message);
            item.SubItems.Add(success ? "Başarılı" : "Başarısız");
            item.ForeColor = success ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            
            lstTestResults.Items.Insert(0, item); // En üste ekle
            
            // Son 50 kaydı tut
            while (lstTestResults.Items.Count > 50)
            {
                lstTestResults.Items.RemoveAt(lstTestResults.Items.Count - 1);
            }
        }

        private void LoadTestResults()
        {
            // Mevcut ilişkileri göster
            var relations = _context.WorkItemRelations
                .Include(r => r.SourceWorkItem)
                .Include(r => r.TargetWorkItem)
                .OrderByDescending(r => r.CreatedAt)
                .Take(10)
                .ToList();

            lblRelationCount.Text = $"Toplam İlişki: {_context.WorkItemRelations.Count()}";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}