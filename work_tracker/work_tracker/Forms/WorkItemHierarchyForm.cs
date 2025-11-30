using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Diagram.Core;
using DevExpress.XtraDiagram;
using DevExpress.XtraEditors;
using work_tracker.Data;
using work_tracker.Data.Entities;

namespace work_tracker.Forms
{
    public partial class WorkItemHierarchyForm : DevExpress.XtraEditors.XtraForm
    {
        private WorkTrackerDbContext _context;
        private DiagramControl diagramControl;
        private ComboBoxEdit cmbProject;
        private ComboBoxEdit cmbRootWorkItem;
        private SimpleButton btnRefresh;
        private SimpleButton btnExportImage;
        private SimpleButton btnFitToPage;
        private LabelControl lblSelectedItem;
        private SimpleButton btnOpenDetail;
        private SimpleButton btnEdit;
        private PanelControl topPanel;
        private PanelControl bottomPanel;

        private Dictionary<int, DiagramShape> _workItemNodes = new Dictionary<int, DiagramShape>();
        private int? _selectedWorkItemId;

        // Durum renkleri
        private readonly Dictionary<string, Color> _statusColors = new Dictionary<string, Color>
        {
            { "Yeni", Color.FromArgb(200, 200, 200) },
            { "Bekliyor", Color.FromArgb(200, 200, 200) },
            { "SprintBacklog", Color.FromArgb(100, 149, 237) },
            { "Gelistirmede", Color.FromArgb(255, 193, 7) },
            { "CodeReview", Color.FromArgb(255, 152, 0) },
            { "Testte", Color.FromArgb(156, 39, 176) },
            { "Tamamlandi", Color.FromArgb(76, 175, 80) },
            { "Kapatildi", Color.FromArgb(96, 125, 139) }
        };

        // Öncelik kenarlık renkleri
        private readonly Dictionary<string, Color> _urgencyBorderColors = new Dictionary<string, Color>
        {
            { "Kritik", Color.FromArgb(244, 67, 54) },
            { "Yuksek", Color.FromArgb(255, 87, 34) },
            { "Normal", Color.FromArgb(33, 150, 243) },
            { "Dusuk", Color.FromArgb(158, 158, 158) }
        };

        public WorkItemHierarchyForm()
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
        }

        private void InitializeComponent()
        {
            this.Text = "📊 İş Öğesi Hiyerarşisi";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Top Panel - Filtreler
            topPanel = new PanelControl
            {
                Dock = DockStyle.Top,
                Height = 50
            };

            var lblProject = new LabelControl
            {
                Text = "Proje:",
                Location = new Point(10, 15)
            };
            topPanel.Controls.Add(lblProject);

            cmbProject = new ComboBoxEdit
            {
                Location = new Point(50, 12),
                Width = 200
            };
            cmbProject.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cmbProject.SelectedIndexChanged += CmbProject_SelectedIndexChanged;
            topPanel.Controls.Add(cmbProject);

            var lblRoot = new LabelControl
            {
                Text = "Kök İş:",
                Location = new Point(270, 15)
            };
            topPanel.Controls.Add(lblRoot);

            cmbRootWorkItem = new ComboBoxEdit
            {
                Location = new Point(320, 12),
                Width = 300
            };
            cmbRootWorkItem.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cmbRootWorkItem.SelectedIndexChanged += CmbRootWorkItem_SelectedIndexChanged;
            topPanel.Controls.Add(cmbRootWorkItem);

            btnRefresh = new SimpleButton
            {
                Text = "🔄 Yenile",
                Location = new Point(640, 10),
                Width = 80
            };
            btnRefresh.Click += BtnRefresh_Click;
            topPanel.Controls.Add(btnRefresh);

            btnFitToPage = new SimpleButton
            {
                Text = "📐 Sığdır",
                Location = new Point(740, 10),
                Width = 80
            };
            btnFitToPage.Click += BtnFitToPage_Click;
            topPanel.Controls.Add(btnFitToPage);

            btnExportImage = new SimpleButton
            {
                Text = "📷 Görüntü Kaydet",
                Location = new Point(840, 10),
                Width = 120
            };
            btnExportImage.Click += BtnExportImage_Click;
            topPanel.Controls.Add(btnExportImage);

            this.Controls.Add(topPanel);

            // Bottom Panel - Seçili öğe bilgisi
            bottomPanel = new PanelControl
            {
                Dock = DockStyle.Bottom,
                Height = 50
            };

            lblSelectedItem = new LabelControl
            {
                Text = "Bir iş öğesi seçin...",
                Location = new Point(10, 15),
                AutoSizeMode = LabelAutoSizeMode.None,
                Width = 800
            };
            bottomPanel.Controls.Add(lblSelectedItem);

            btnOpenDetail = new SimpleButton
            {
                Text = "📋 Detay Aç",
                Location = new Point(900, 10),
                Width = 100,
                Enabled = false
            };
            btnOpenDetail.Click += BtnOpenDetail_Click;
            bottomPanel.Controls.Add(btnOpenDetail);

            btnEdit = new SimpleButton
            {
                Text = "✏️ Düzenle",
                Location = new Point(1010, 10),
                Width = 100,
                Enabled = false
            };
            btnEdit.Click += BtnEdit_Click;
            bottomPanel.Controls.Add(btnEdit);

            this.Controls.Add(bottomPanel);

            // Diagram Control
            diagramControl = new DiagramControl
            {
                Dock = DockStyle.Fill
            };
            diagramControl.SelectionChanged += DiagramControl_SelectionChanged;
            diagramControl.MouseDoubleClick += DiagramControl_MouseDoubleClick;
            
            // Diagram ayarları
            diagramControl.OptionsBehavior.SelectedStencils = new StencilCollection(new string[] { "BasicShapes" });
            
            this.Controls.Add(diagramControl);

            // Z-Order düzenleme
            diagramControl.BringToFront();
            topPanel.BringToFront();
            bottomPanel.BringToFront();

            this.Load += WorkItemHierarchyForm_Load;
        }

        private void WorkItemHierarchyForm_Load(object sender, EventArgs e)
        {
            LoadProjects();
            AddLegend();
        }

        private void LoadProjects()
        {
            try
            {
                var projects = _context.Projects
                    .Where(p => p.IsActive)
                    .OrderBy(p => p.Name)
                    .ToList();

                cmbProject.Properties.Items.Clear();
                cmbProject.Properties.Items.Add("-- Tüm Projeler --");
                foreach (var project in projects)
                {
                    cmbProject.Properties.Items.Add(project);
                }
                cmbProject.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Projeler yüklenirken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRootWorkItems();
        }

        private void LoadRootWorkItems()
        {
            try
            {
                cmbRootWorkItem.Properties.Items.Clear();
                cmbRootWorkItem.Properties.Items.Add("-- Tüm Kök İşler --");

                int? projectId = null;
                if (cmbProject.SelectedItem is Project selectedProject)
                {
                    projectId = selectedProject.Id;
                }

                // Parent'ı olmayan işleri bul (kök işler)
                var childWorkItemIds = _context.WorkItemRelations
                    .Where(r => r.RelationType == "Parent")
                    .Select(r => r.WorkItemId2)
                    .Distinct()
                    .ToList();

                var query = _context.WorkItems
                    .Where(w => !childWorkItemIds.Contains(w.Id));

                if (projectId.HasValue)
                {
                    query = query.Where(w => w.ProjectId == projectId.Value);
                }

                var rootWorkItems = query
                    .OrderBy(w => w.Title)
                    .Select(w => new { w.Id, w.Title })
                    .Take(100)
                    .ToList();

                foreach (var item in rootWorkItems)
                {
                    cmbRootWorkItem.Properties.Items.Add(new WorkItemComboItem
                    {
                        Id = item.Id,
                        Title = item.Title
                    });
                }

                cmbRootWorkItem.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Kök işler yüklenirken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbRootWorkItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildDiagram();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            BuildDiagram();
        }

        private void BuildDiagram()
        {
            try
            {
                diagramControl.Items.Clear();
                _workItemNodes.Clear();

                int? projectId = null;
                if (cmbProject.SelectedItem is Project selectedProject)
                {
                    projectId = selectedProject.Id;
                }

                int? rootWorkItemId = null;
                if (cmbRootWorkItem.SelectedItem is WorkItemComboItem selectedRoot)
                {
                    rootWorkItemId = selectedRoot.Id;
                }

                // İlişkileri al
                var relations = _context.WorkItemRelations
                    .Include(r => r.SourceWorkItem)
                    .Include(r => r.TargetWorkItem)
                    .ToList();

                List<WorkItem> workItems;
                
                if (rootWorkItemId.HasValue)
                {
                    // Seçili kök işten başla
                    var relatedIds = GetAllRelatedWorkItemIds(rootWorkItemId.Value, relations);
                    relatedIds.Add(rootWorkItemId.Value);
                    
                    workItems = _context.WorkItems
                        .Include(w => w.Project)
                        .Where(w => relatedIds.Contains(w.Id))
                        .ToList();
                }
                else if (projectId.HasValue)
                {
                    // Projedeki tüm işler
                    workItems = _context.WorkItems
                        .Include(w => w.Project)
                        .Where(w => w.ProjectId == projectId.Value)
                        .Take(50) // Performans için limit
                        .ToList();
                }
                else
                {
                    // İlişkisi olan tüm işler
                    var relatedWorkItemIds = relations
                        .SelectMany(r => new[] { r.WorkItemId1, r.WorkItemId2 })
                        .Distinct()
                        .ToList();

                    workItems = _context.WorkItems
                        .Include(w => w.Project)
                        .Where(w => relatedWorkItemIds.Contains(w.Id))
                        .Take(50) // Performans için limit
                        .ToList();
                }

                if (!workItems.Any())
                {
                    XtraMessageBox.Show("Gösterilecek iş öğesi bulunamadı.", "Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Node'ları oluştur
                CreateNodes(workItems);

                // Connector'ları oluştur
                CreateConnectors(relations, workItems);

                // Otomatik düzenleme
                LayoutDiagram();

                // Legend'i tekrar ekle
                AddLegend();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Diyagram oluşturulurken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private HashSet<int> GetAllRelatedWorkItemIds(int workItemId, List<WorkItemRelation> relations)
        {
            var result = new HashSet<int>();
            var queue = new Queue<int>();
            queue.Enqueue(workItemId);

            while (queue.Count > 0)
            {
                var currentId = queue.Dequeue();
                if (result.Contains(currentId))
                    continue;

                result.Add(currentId);

                // Bu işe bağlı tüm ilişkileri bul
                var relatedItems = relations
                    .Where(r => r.WorkItemId1 == currentId || r.WorkItemId2 == currentId)
                    .SelectMany(r => new[] { r.WorkItemId1, r.WorkItemId2 })
                    .Where(id => !result.Contains(id));

                foreach (var relatedId in relatedItems)
                {
                    queue.Enqueue(relatedId);
                }
            }

            return result;
        }

        private void CreateNodes(List<WorkItem> workItems)
        {
            float x = 100;
            float y = 100;
            int col = 0;

            foreach (var workItem in workItems)
            {
                var node = new DiagramShape
                {
                    Shape = BasicShapes.Rectangle,
                    Width = 180,
                    Height = 80,
                    Position = new DevExpress.Utils.PointFloat(x, y),
                    Content = $"#{workItem.Id}\n{TruncateText(workItem.Title, 25)}\n[{workItem.Status}]"
                };

                // Durum rengine göre arka plan
                var bgColor = _statusColors.ContainsKey(workItem.Status ?? "Bekliyor") 
                    ? _statusColors[workItem.Status ?? "Bekliyor"] 
                    : Color.LightGray;
                
                node.Appearance.BackColor = bgColor;
                node.Appearance.BorderColor = Color.Black;
                node.Appearance.BorderSize = 2;

                // Önceliğe göre kenarlık
                var urgency = workItem.Urgency ?? "Normal";
                if (_urgencyBorderColors.ContainsKey(urgency))
                {
                    node.Appearance.BorderColor = _urgencyBorderColors[urgency];
                    if (urgency == "Kritik" || urgency == "Yuksek")
                    {
                        node.Appearance.BorderSize = 4;
                    }
                }

                // Metin rengi (koyu arka plan için beyaz)
                if (bgColor.GetBrightness() < 0.5)
                {
                    node.Appearance.ForeColor = Color.White;
                }
                else
                {
                    node.Appearance.ForeColor = Color.Black;
                }

                node.Tag = workItem.Id;
                diagramControl.Items.Add(node);
                _workItemNodes[workItem.Id] = node;

                // Grid düzeni
                col++;
                x += 220;
                if (col >= 5)
                {
                    col = 0;
                    x = 100;
                    y += 120;
                }
            }
        }

        private void CreateConnectors(List<WorkItemRelation> relations, List<WorkItem> workItems)
        {
            var workItemIds = workItems.Select(w => w.Id).ToHashSet();

            foreach (var relation in relations)
            {
                if (!workItemIds.Contains(relation.WorkItemId1) || !workItemIds.Contains(relation.WorkItemId2))
                    continue;

                if (!_workItemNodes.ContainsKey(relation.WorkItemId1) || !_workItemNodes.ContainsKey(relation.WorkItemId2))
                    continue;

                var sourceNode = _workItemNodes[relation.WorkItemId1];
                var targetNode = _workItemNodes[relation.WorkItemId2];

                var connector = new DiagramConnector
                {
                    BeginItem = sourceNode,
                    EndItem = targetNode,
                    Type = ConnectorType.RightAngle
                };

                if (relation.RelationType == "Parent")
                {
                    // Parent -> Child: Düz siyah ok
                    connector.Appearance.BorderColor = Color.Black;
                    connector.Appearance.BorderSize = 2;
                }
                else if (relation.RelationType == "Sibling")
                {
                    // Sibling: Kesikli mavi çizgi
                    connector.Appearance.BorderColor = Color.DodgerBlue;
                    connector.Appearance.BorderSize = 2;
                }

                diagramControl.Items.Add(connector);
            }
        }

        private void LayoutDiagram()
        {
            try
            {
                // Basit otomatik düzenleme uygula
                diagramControl.ApplyTreeLayout();
            }
            catch
            {
                // Layout başarısız olursa grid düzeni kalsın
            }
        }

        private void AddLegend()
        {
            // Legend kutusu
            float legendX = 10;
            float legendY = 10;

            var legendTitle = new DiagramShape
            {
                Shape = BasicShapes.Rectangle,
                Width = 150,
                Height = 25,
                Position = new DevExpress.Utils.PointFloat(legendX, legendY),
                Content = "📋 Durum Renkleri",
                CanSelect = false,
                CanMove = false,
                CanResize = false
            };
            legendTitle.Appearance.BackColor = Color.White;
            legendTitle.Appearance.BorderColor = Color.Gray;
            diagramControl.Items.Add(legendTitle);

            legendY += 30;

            foreach (var status in _statusColors)
            {
                var legendItem = new DiagramShape
                {
                    Shape = BasicShapes.Rectangle,
                    Width = 150,
                    Height = 20,
                    Position = new DevExpress.Utils.PointFloat(legendX, legendY),
                    Content = status.Key,
                    CanSelect = false,
                    CanMove = false,
                    CanResize = false
                };
                legendItem.Appearance.BackColor = status.Value;
                legendItem.Appearance.BorderColor = Color.Gray;
                legendItem.Appearance.ForeColor = status.Value.GetBrightness() < 0.5 ? Color.White : Color.Black;
                diagramControl.Items.Add(legendItem);
                legendY += 25;
            }

            // İlişki tipi legend
            legendY += 10;
            var relationTitle = new DiagramShape
            {
                Shape = BasicShapes.Rectangle,
                Width = 150,
                Height = 25,
                Position = new DevExpress.Utils.PointFloat(legendX, legendY),
                Content = "🔗 İlişki Tipleri",
                CanSelect = false,
                CanMove = false,
                CanResize = false
            };
            relationTitle.Appearance.BackColor = Color.White;
            relationTitle.Appearance.BorderColor = Color.Gray;
            diagramControl.Items.Add(relationTitle);

            legendY += 30;
            var parentLegend = new DiagramShape
            {
                Shape = BasicShapes.Rectangle,
                Width = 150,
                Height = 20,
                Position = new DevExpress.Utils.PointFloat(legendX, legendY),
                Content = "→ Parent-Child",
                CanSelect = false,
                CanMove = false,
                CanResize = false
            };
            parentLegend.Appearance.BackColor = Color.White;
            parentLegend.Appearance.BorderColor = Color.Black;
            diagramControl.Items.Add(parentLegend);

            legendY += 25;
            var siblingLegend = new DiagramShape
            {
                Shape = BasicShapes.Rectangle,
                Width = 150,
                Height = 20,
                Position = new DevExpress.Utils.PointFloat(legendX, legendY),
                Content = "↔ Sibling",
                CanSelect = false,
                CanMove = false,
                CanResize = false
            };
            siblingLegend.Appearance.BackColor = Color.White;
            siblingLegend.Appearance.BorderColor = Color.DodgerBlue;
            diagramControl.Items.Add(siblingLegend);
        }

        private void DiagramControl_SelectionChanged(object sender, EventArgs e)
        {
            var selectedItems = diagramControl.SelectedItems.OfType<DiagramShape>().ToList();
            
            if (selectedItems.Count == 1 && selectedItems[0].Tag is int workItemId)
            {
                _selectedWorkItemId = workItemId;
                var workItem = _context.WorkItems.Find(workItemId);
                if (workItem != null)
                {
                    lblSelectedItem.Text = $"Seçili: #{workItem.Id} - {workItem.Title} | Durum: {workItem.Status} | Aciliyet: {workItem.Urgency ?? "Normal"}";
                    btnOpenDetail.Enabled = true;
                    btnEdit.Enabled = true;
                }
            }
            else
            {
                _selectedWorkItemId = null;
                lblSelectedItem.Text = "Bir iş öğesi seçin...";
                btnOpenDetail.Enabled = false;
                btnEdit.Enabled = false;
            }
        }

        private void DiagramControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Seçili öğeye çift tıklandığında detay formunu aç
            if (_selectedWorkItemId.HasValue)
            {
                OpenWorkItemDetail(_selectedWorkItemId.Value);
            }
        }

        private void BtnOpenDetail_Click(object sender, EventArgs e)
        {
            if (_selectedWorkItemId.HasValue)
            {
                OpenWorkItemDetail(_selectedWorkItemId.Value);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedWorkItemId.HasValue)
            {
                OpenWorkItemEdit(_selectedWorkItemId.Value);
            }
        }

        private void OpenWorkItemDetail(int workItemId)
        {
            try
            {
                var detailForm = new WorkItemDetailForm(workItemId);
                detailForm.MdiParent = this.MdiParent;
                detailForm.Show();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Detay formu açılırken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenWorkItemEdit(int workItemId)
        {
            try
            {
                var editForm = new WorkItemEditForm(workItemId);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    // Diyagramı yenile
                    BuildDiagram();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Düzenleme formu açılırken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnFitToPage_Click(object sender, EventArgs e)
        {
            if (diagramControl != null && diagramControl.Items.Count > 0)
            {
                diagramControl.FitToDrawing();
            }
        }

        private void BtnExportImage_Click(object sender, EventArgs e)
        {
            try
            {
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "PNG Dosyası|*.png|JPEG Dosyası|*.jpg|BMP Dosyası|*.bmp";
                    saveDialog.Title = "Diyagramı Kaydet";
                    saveDialog.FileName = $"WorkItemHierarchy_{DateTime.Now:yyyyMMdd_HHmmss}";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        diagramControl.ExportDiagram(saveDialog.FileName);
                        XtraMessageBox.Show($"Diyagram başarıyla kaydedildi:\n{saveDialog.FileName}", "Başarılı",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Dışa aktarma hatası: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string TruncateText(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
                return text;
            return text.Substring(0, maxLength - 3) + "...";
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

    // ComboBox için yardımcı sınıf
    public class WorkItemComboItem
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return $"#{Id} - {Title}";
        }
    }
}
