using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using work_tracker.Data;
using work_tracker.Data.Entities;

namespace work_tracker.Forms
{
    public partial class WorkItemEditForm : XtraForm
    {
        private WorkTrackerDbContext _context;
        private int? _workItemId;

        public WorkItemEditForm(int? workItemId = null, int? meetingId = null, string initialTitle = null, string initialDescription = null)
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
            _workItemId = workItemId;

            // Toplantıdan gelen veri varsa ön-doldur
            if (meetingId.HasValue)
            {
                cmbMeeting.EditValue = meetingId.Value;
            }
            if (!string.IsNullOrEmpty(initialTitle))
            {
                txtTitle.Text = initialTitle;
            }
            if (!string.IsNullOrEmpty(initialDescription))
            {
                txtDescription.Text = initialDescription;
            }
        }

        private void WorkItemEditForm_Load(object sender, EventArgs e)
        {
            LoadComboboxes();

            if (_workItemId.HasValue)
            {
                LoadWorkItem(_workItemId.Value);
            }
            else
            {
                // Varsayılan değerler
                dtRequestedAt.EditValue = DateTime.Now;
            }
        }

        private void LoadComboboxes()
        {
            // Projeler
            var projects = _context.Projects.Where(p => p.IsActive).OrderBy(p => p.Name).ToList();
            cmbProject.Properties.DataSource = projects;
            cmbProject.Properties.DisplayMember = "Name";
            cmbProject.Properties.ValueMember = "Id";
            cmbProject.Properties.NullText = "Proje seçin (opsiyonel)...";
            
            // LookUpEdit için kolonları ayarla
            cmbProject.Properties.Columns.Clear();
            cmbProject.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Proje Adı"));

            // Modüller - başlangıçta boş
            cmbModule.Properties.NullText = "Önce proje seçin...";
            cmbModule.EditValue = null;

            // Toplantılar
            var meetings = _context.Meetings.OrderByDescending(m => m.MeetingDate).Take(50).ToList();
            cmbMeeting.Properties.DataSource = meetings;
            cmbMeeting.Properties.DisplayMember = "Subject";
            cmbMeeting.Properties.ValueMember = "Id";
            cmbMeeting.Properties.NullText = "Toplantı seçin (opsiyonel)...";
            
            // LookUpEdit için kolonları ayarla
            cmbMeeting.Properties.Columns.Clear();
            cmbMeeting.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Subject", "Toplantı Konusu"));
            cmbMeeting.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MeetingDate", "Tarih") 
            { 
                Width = 100,
                FormatString = "dd.MM.yyyy"
            });

            LoadTags();
            LoadPersons();
            LoadWorkItemsForRelations();
        }

        private void LoadWorkItem(int workItemId)
        {
            var workItem = _context.WorkItems
                .Include(w => w.Tags)
                .Include(w => w.RequestedByPersons)
                .FirstOrDefault(w => w.Id == workItemId);
                
            if (workItem != null)
            {
                txtTitle.Text = workItem.Title;
                txtDescription.Text = workItem.Description;
                // Eğer Person seçiliyse onu göster, yoksa eski RequestedBy string'ini göster
                if (workItem.RequestedByPersons != null && workItem.RequestedByPersons.Any())
                {
                    cmbRequestedBy.EditValue = workItem.RequestedByPersons.First().Id;
                }
                else if (!string.IsNullOrEmpty(workItem.RequestedBy))
                {
                    // Eski string değerini Person olarak ekle veya göster
                    var existingPerson = _context.Persons.FirstOrDefault(p => p.Name == workItem.RequestedBy);
                    if (existingPerson != null)
                    {
                        cmbRequestedBy.EditValue = existingPerson.Id;
                    }
                }
                dtRequestedAt.EditValue = workItem.RequestedAt;
                cmbProject.EditValue = workItem.ProjectId;
                cmbModule.EditValue = workItem.ModuleId;
                cmbMeeting.EditValue = workItem.SourceMeetingId;

                LoadTags(workItem.Tags.Select(t => t.Id));
                LoadWorkItemRelations(workItem.Id);
            }
        }

        private void cmbProject_EditValueChanged(object sender, EventArgs e)
        {
            cmbModule.Properties.DataSource = null;
            cmbModule.EditValue = null;
            
            // Seçili projeye göre modülleri yükle
            if (cmbProject.EditValue != null)
            {
                var projectId = (int)cmbProject.EditValue;
                var modules = _context.ProjectModules
                    .Where(m => m.ProjectId == projectId && m.IsActive)
                    .OrderBy(m => m.Name)
                    .ToList();

                cmbModule.Properties.DataSource = modules;
                cmbModule.Properties.DisplayMember = "Name";
                cmbModule.Properties.ValueMember = "Id";
                cmbModule.Properties.NullText = "Modül seçin (opsiyonel)...";
                
                // LookUpEdit için kolonları ayarla
                cmbModule.Properties.Columns.Clear();
                cmbModule.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Modül Adı"));
            }
            else
            {
                cmbModule.Properties.NullText = "Önce proje seçin...";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                XtraMessageBox.Show("Başlık boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return;
            }

            if (cmbRequestedBy.EditValue == null)
            {
                XtraMessageBox.Show("Talep eden kişi seçilmelidir!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRequestedBy.Focus();
                return;
            }

            // Yeni iş ekliyorsak benzer iş kontrolü yap
            if (!_workItemId.HasValue)
            {
                var similarItems = FindSimilarWorkItems(txtTitle.Text, txtDescription.Text);
                if (similarItems.Any())
                {
                    var warningMessage = BuildSimilarItemsWarning(similarItems);
                    var result = XtraMessageBox.Show(
                        warningMessage,
                        "⚠️ Benzer İş Uyarısı",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.No)
                    {
                        return; // Kullanıcı vazgeçti
                    }
                }
            }

            try
            {
                WorkItem workItem;
                
                if (_workItemId.HasValue)
                {
                    // Mevcut WorkItem'ı etiketleriyle beraber yükle
                    workItem = _context.WorkItems
                        .Include(w => w.Tags)
                        .Include(w => w.RequestedByPersons)
                        .FirstOrDefault(w => w.Id == _workItemId.Value);
                        
                    if (workItem != null)
                    {
                        workItem.Title = txtTitle.Text.Trim();
                        workItem.Description = txtDescription.Text;
                        var selectedPersonId = cmbRequestedBy.EditValue as int?;
                        if (selectedPersonId.HasValue)
                        {
                            var person = _context.Persons.Find(selectedPersonId.Value);
                            workItem.RequestedBy = person?.Name ?? "";
                            // Person ilişkisini güncelle
                            workItem.RequestedByPersons.Clear();
                            if (person != null)
                            {
                                workItem.RequestedByPersons.Add(person);
                            }
                        }
                        workItem.RequestedAt = Convert.ToDateTime(dtRequestedAt.EditValue);
                        workItem.ProjectId = cmbProject.EditValue as int?;
                        workItem.ModuleId = cmbModule.EditValue as int?;
                        workItem.SourceMeetingId = cmbMeeting.EditValue as int?;
                    }
                }
                else
                {
                    workItem = new WorkItem
                    {
                        Title = txtTitle.Text.Trim(),
                        Description = txtDescription.Text,
                        RequestedAt = Convert.ToDateTime(dtRequestedAt.EditValue),
                        ProjectId = cmbProject.EditValue as int?,
                        ModuleId = cmbModule.EditValue as int?,
                        SourceMeetingId = cmbMeeting.EditValue as int?,
                        Board = "Inbox",
                        Status = "Bekliyor",
                        CreatedAt = DateTime.Now
                    };
                    var selectedPersonId = cmbRequestedBy.EditValue as int?;
                    if (selectedPersonId.HasValue)
                    {
                        var person = _context.Persons.Find(selectedPersonId.Value);
                        workItem.RequestedBy = person?.Name ?? "";
                        if (person != null)
                        {
                            workItem.RequestedByPersons.Add(person);
                        }
                    }
                    _context.WorkItems.Add(workItem);
                }

                if (workItem != null)
                {
                    // Seçili etiketleri al
                    var selectedTagIds = cmbTags.Properties.Items
                        .GetCheckedValues()
                        .Cast<int>()
                        .ToList();

                    // Etiketleri güncelle
                    workItem.Tags.Clear();
                    foreach (var tagId in selectedTagIds)
                    {
                        var tag = _context.Tags.Find(tagId);
                        if (tag != null)
                        {
                            workItem.Tags.Add(tag);
                        }
                    }
                }

                _context.SaveChanges();
                
                // İlişkileri kaydet
                if (!SaveWorkItemRelations(workItem.Id))
                {
                    DialogResult = DialogResult.Cancel;
                    return; // İlişkiler kaydedilemezse işlemi iptal et
                }
                
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Kayıt sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void LoadTags(IEnumerable<int> selectedTagIds = null)
        {
            IEnumerable<int> currentSelection = selectedTagIds ??
                cmbTags.Properties.Items
                    .GetCheckedValues()
                    .OfType<int>();

            var selectedSet = new HashSet<int>(currentSelection);

            var tags = _context.Tags
                .OrderBy(t => t.Name)
                .ToList();

            cmbTags.Properties.Items.BeginUpdate();
            cmbTags.Properties.Items.Clear();

            foreach (var tag in tags)
            {
                var item = new CheckedListBoxItem(tag.Id, tag.Name);
                if (selectedSet.Contains(tag.Id))
                {
                    item.CheckState = CheckState.Checked;
                }

                cmbTags.Properties.Items.Add(item);
            }

            cmbTags.Properties.Items.EndUpdate();
        }

        private void btnAddTag_Click(object sender, EventArgs e)
        {
            var tagName = XtraInputBox.Show("Yeni etiket adını girin:", "Etiket Ekle", string.Empty);
            if (string.IsNullOrWhiteSpace(tagName))
                return;

            tagName = tagName.Trim();

            if (_context.Tags.Any(t => t.Name == tagName))
            {
                XtraMessageBox.Show("Bu isimde bir etiket zaten mevcut.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var tag = new Tag
            {
                Name = tagName,
                ColorHex = GenerateColorHex(tagName)
            };

            _context.Tags.Add(tag);
            _context.SaveChanges();

            LoadTags(new[] { tag.Id });

            XtraMessageBox.Show("Etiket oluşturuldu ve seçildi.", "Bilgi",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadPersons()
        {
            var persons = _context.Persons
                .OrderBy(p => p.Name)
                .ToList();

            cmbRequestedBy.Properties.DataSource = persons;
            cmbRequestedBy.Properties.DisplayMember = "Name";
            cmbRequestedBy.Properties.ValueMember = "Id";
            cmbRequestedBy.Properties.NullText = "Kişi seçin...";
            
            // LookUpEdit için kolonları ayarla
            cmbRequestedBy.Properties.Columns.Clear();
            cmbRequestedBy.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Ad"));
            cmbRequestedBy.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Email", "E-posta"));
            cmbRequestedBy.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PhoneNumber", "Telefon"));
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            var personName = XtraInputBox.Show("Yeni kişi adını girin:", "Kişi Ekle", string.Empty);
            if (string.IsNullOrWhiteSpace(personName))
                return;

            personName = personName.Trim();

            if (_context.Persons.Any(p => p.Name == personName))
            {
                XtraMessageBox.Show("Bu isimde bir kişi zaten mevcut.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var person = new Person
            {
                Name = personName
            };

            _context.Persons.Add(person);
            _context.SaveChanges();

            LoadPersons();
            cmbRequestedBy.EditValue = person.Id;

            XtraMessageBox.Show("Kişi oluşturuldu ve seçildi.", "Bilgi",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string GenerateColorHex(string seed)
        {
            var palette = new[]
            {
                "#EC407A", "#AB47BC", "#7E57C2", "#5C6BC0",
                "#42A5F5", "#26A69A", "#66BB6A", "#9CCC65",
                "#D4E157", "#FFA726", "#FF7043", "#8D6E63"
            };

            var index = Math.Abs(seed.GetHashCode());
            return palette[index % palette.Length];
        }

        private void LoadWorkItemsForRelations()
        {
            try
            {
                // Mevcut iş hariç tüm işleri yükle
                var workItemsQuery = _context.WorkItems.AsQueryable();
                
                if (_workItemId.HasValue)
                {
                    workItemsQuery = workItemsQuery.Where(w => w.Id != _workItemId.Value);
                }

                var workItems = workItemsQuery
                    .OrderBy(w => w.Title)
                    .Select(w => new
                    {
                        Id = w.Id,
                        DisplayText = w.Id + " - " + w.Title
                    })
                    .ToList();

                // Parent WorkItem için LookUpEdit
                cmbParentWorkItem.Properties.DataSource = workItems;
                cmbParentWorkItem.Properties.DisplayMember = "DisplayText";
                cmbParentWorkItem.Properties.ValueMember = "Id";
                cmbParentWorkItem.Properties.NullText = "Üst iş seçin (opsiyonel)...";
                
                // LookUpEdit için kolonları ayarla
                cmbParentWorkItem.Properties.Columns.Clear();
                cmbParentWorkItem.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DisplayText", "İş"));

                // Sibling WorkItems için CheckedComboBoxEdit
                cmbSiblingWorkItems.Properties.DataSource = workItems;
                cmbSiblingWorkItems.Properties.DisplayMember = "DisplayText";
                cmbSiblingWorkItems.Properties.ValueMember = "Id";
                cmbSiblingWorkItems.Properties.NullText = "Kardeş işler seçin (opsiyonel)...";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"İşler yüklenirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadWorkItemRelations(int workItemId)
        {
            try
            {
                var relations = _context.WorkItemRelations
                    .Where(r => r.WorkItemId2 == workItemId || r.WorkItemId1 == workItemId)
                    .ToList();

                // Parent ilişkisi
                var parentRelation = relations.FirstOrDefault(r =>
                    r.WorkItemId2 == workItemId && r.RelationType == WorkItemRelationTypes.Parent);
                if (parentRelation != null)
                {
                    cmbParentWorkItem.EditValue = parentRelation.WorkItemId1;
                }

                // Sibling ilişkileri
                var siblingIds = relations
                    .Where(r => r.RelationType == WorkItemRelationTypes.Sibling)
                    .Select(r => r.WorkItemId1 == workItemId ? r.WorkItemId2 : r.WorkItemId1)
                    .Distinct()
                    .ToList();

                if (siblingIds.Any())
                {
                    cmbSiblingWorkItems.Properties.Items.Clear();
                    foreach (var siblingId in siblingIds)
                    {
                        var item = new DevExpress.XtraEditors.Controls.CheckedListBoxItem(siblingId, siblingId.ToString());
                        item.CheckState = CheckState.Checked;
                        cmbSiblingWorkItems.Properties.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"İlişkiler yüklenirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool SaveWorkItemRelations(int workItemId)
        {
            try
            {
                var currentUser = Environment.UserName;
                
                // Mevcut ilişkileri temizle - SADECE bu işin parent ve sibling ilişkilerini sil
                // Child ilişkilerini (bu işin parent olduğu ilişkiler) KORUYORUZ!
                var relationsToRemove = _context.WorkItemRelations
                    .Where(r => 
                        // Bu işin parent ilişkisini sil (bu iş child olduğu ilişki)
                        (r.WorkItemId2 == workItemId && r.RelationType == WorkItemRelationTypes.Parent) ||
                        // Bu işin sibling ilişkilerini sil (her iki yönde)
                        ((r.WorkItemId1 == workItemId || r.WorkItemId2 == workItemId) && r.RelationType == WorkItemRelationTypes.Sibling))
                    .ToList();

                _context.WorkItemRelations.RemoveRange(relationsToRemove);

                // Parent ilişkisi kaydet
                if (cmbParentWorkItem.EditValue != null)
                {
                    var parentWorkItemId = (int)cmbParentWorkItem.EditValue;
                    
                    // Döngü kontrolü
                    if (WouldCreateCycle(workItemId, parentWorkItemId))
                    {
                        XtraMessageBox.Show("Bu üst iş seçimi döngü oluşturacaktır. Lütfen başka bir üst iş seçin.", "Uyarı",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    var parentRelation = new WorkItemRelation
                    {
                        WorkItemId1 = parentWorkItemId,
                        WorkItemId2 = workItemId,
                        RelationType = WorkItemRelationTypes.Parent,
                        CreatedBy = currentUser
                    };
                    _context.WorkItemRelations.Add(parentRelation);
                }

                // Sibling ilişkileri kaydet (çift yönlü)
                if (cmbSiblingWorkItems.EditValue != null)
                {
                    var selectedSiblingIds = cmbSiblingWorkItems.Properties.Items
                        .Cast<DevExpress.XtraEditors.Controls.CheckedListBoxItem>()
                        .Where(item => item.CheckState == CheckState.Checked)
                        .Select(item => (int)item.Value)
                        .ToList();

                    foreach (var siblingId in selectedSiblingIds)
                    {
                        // A->B ilişkisi
                        var siblingRelation1 = new WorkItemRelation
                        {
                            WorkItemId1 = workItemId,
                            WorkItemId2 = siblingId,
                            RelationType = WorkItemRelationTypes.Sibling,
                            CreatedBy = currentUser
                        };
                        _context.WorkItemRelations.Add(siblingRelation1);

                        // B->A ilişkisi (çift yönlü)
                        var siblingRelation2 = new WorkItemRelation
                        {
                            WorkItemId1 = siblingId,
                            WorkItemId2 = workItemId,
                            RelationType = WorkItemRelationTypes.Sibling,
                            CreatedBy = currentUser
                        };
                        _context.WorkItemRelations.Add(siblingRelation2);
                    }
                }

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"İlişkiler kaydedilirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool WouldCreateCycle(int workItemId, int parentWorkItemId)
        {
            // Basit döngü kontrolü - parent'ın parent'ını kontrol et
            var visited = new HashSet<int>();
            return HasCycleRecursive(parentWorkItemId, visited, workItemId);
        }

        private bool HasCycleRecursive(int currentWorkItemId, HashSet<int> visited, int targetWorkItemId)
        {
            if (currentWorkItemId == targetWorkItemId)
                return true;

            if (visited.Contains(currentWorkItemId))
                return true;

            visited.Add(currentWorkItemId);

            // Bu işin parent'ını bul
            var parentRelation = _context.WorkItemRelations
                .FirstOrDefault(r => r.WorkItemId2 == currentWorkItemId && r.RelationType == WorkItemRelationTypes.Parent);

            if (parentRelation != null)
            {
                return HasCycleRecursive(parentRelation.WorkItemId1, visited, targetWorkItemId);
            }

            return false;
        }

        private void btnDeleteParentWorkItem_Click(object sender, EventArgs e)
        {
            if (cmbParentWorkItem.EditValue == null)
            {
                XtraMessageBox.Show("Silinecek üst iş bulunamadı.", "Bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = XtraMessageBox.Show("Seçili üst iş ilişkisi silinecektir. Onaylıyor musunuz?",
                "Üst İş Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var parentWorkItemId = (int)cmbParentWorkItem.EditValue;
                    
                    // Parent ilişkisini veritabanından sil
                    var parentRelation = _context.WorkItemRelations
                        .FirstOrDefault(r => r.WorkItemId1 == parentWorkItemId &&
                                           r.WorkItemId2 == _workItemId.Value &&
                                           r.RelationType == WorkItemRelationTypes.Parent);

                    if (parentRelation != null)
                    {
                        _context.WorkItemRelations.Remove(parentRelation);
                        _context.SaveChanges();

                        // UI'ı güncelle
                        cmbParentWorkItem.EditValue = null;

                        XtraMessageBox.Show("Üst iş ilişkisi başarıyla silindi.", "Başarılı",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show("Üst iş ilişkisi bulunamadı.", "Uyarı",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Üst iş silinirken hata oluştu: {ex.Message}", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #region Benzer İş Kontrolü

        /// <summary>
        /// Yeni eklenen işe benzer mevcut işleri bulur
        /// </summary>
        private List<SimilarWorkItemResult> FindSimilarWorkItems(string title, string description)
        {
            var results = new List<SimilarWorkItemResult>();
            
            // Başlık ve açıklamadan anahtar kelimeleri çıkar
            var inputKeywords = ExtractKeywords(title + " " + description);
            
            if (!inputKeywords.Any())
                return results;

            // Tüm işleri getir (son 6 ay veya aktif olanlar)
            var cutoffDate = DateTime.Now.AddMonths(-6);
            var existingItems = _context.WorkItems
                .Where(w => w.CreatedAt >= cutoffDate || 
                           (w.Status != "Cozuldu" && w.Status != "Arşivlendi"))
                .Select(w => new 
                {
                    w.Id,
                    w.Title,
                    w.Description,
                    w.Status,
                    w.Board,
                    w.CreatedAt
                })
                .ToList();

            foreach (var item in existingItems)
            {
                var itemKeywords = ExtractKeywords(item.Title + " " + (item.Description ?? ""));
                var similarity = CalculateSimilarity(inputKeywords, itemKeywords);
                
                // %40'tan fazla benzerlik varsa listeye ekle
                if (similarity >= 0.40)
                {
                    results.Add(new SimilarWorkItemResult
                    {
                        Id = item.Id,
                        Title = item.Title,
                        Status = item.Status,
                        Board = item.Board,
                        CreatedAt = item.CreatedAt,
                        SimilarityScore = similarity
                    });
                }
            }

            // En benzer olanları üstte göster, max 5 tane
            return results
                .OrderByDescending(r => r.SimilarityScore)
                .Take(5)
                .ToList();
        }

        /// <summary>
        /// Metinden anahtar kelimeleri çıkarır (stop words hariç)
        /// </summary>
        private HashSet<string> ExtractKeywords(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new HashSet<string>();

            // Türkçe ve İngilizce stop words
            var stopWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                // Türkçe
                "bir", "ve", "ile", "için", "bu", "da", "de", "mi", "mu", "mı", "mü",
                "ne", "olan", "olarak", "gibi", "daha", "çok", "en", "ama", "veya",
                "ya", "yani", "ki", "her", "hem", "kadar", "sonra", "önce", "ayrıca",
                "şu", "o", "ben", "sen", "biz", "siz", "onlar", "bunu", "şunu", "onu",
                "var", "yok", "ise", "iyi", "kötü", "büyük", "küçük", "yeni", "eski",
                "tüm", "bütün", "bazı", "hangi", "nasıl", "neden", "nerede", "zaman",
                // İngilizce
                "the", "a", "an", "is", "are", "was", "were", "be", "been", "being",
                "have", "has", "had", "do", "does", "did", "will", "would", "could",
                "should", "may", "might", "must", "shall", "can", "need", "dare",
                "to", "of", "in", "for", "on", "with", "at", "by", "from", "as",
                "into", "through", "during", "before", "after", "above", "below",
                "this", "that", "these", "those", "it", "its", "and", "but", "or",
                "not", "no", "yes", "all", "each", "every", "both", "few", "more",
                "most", "other", "some", "such", "only", "own", "same", "so", "than"
            };

            // Metni küçük harfe çevir ve kelimelere ayır
            var words = Regex.Split(text.ToLower(), @"[\s\p{P}]+")
                .Where(w => w.Length >= 3) // En az 3 karakter
                .Where(w => !stopWords.Contains(w))
                .Where(w => !Regex.IsMatch(w, @"^\d+$")) // Sadece rakamları çıkar
                .ToHashSet();

            return words;
        }

        /// <summary>
        /// İki kelime seti arasındaki benzerliği hesaplar (Jaccard similarity)
        /// </summary>
        private double CalculateSimilarity(HashSet<string> set1, HashSet<string> set2)
        {
            if (!set1.Any() || !set2.Any())
                return 0;

            var intersection = set1.Intersect(set2, StringComparer.OrdinalIgnoreCase).Count();
            var union = set1.Union(set2, StringComparer.OrdinalIgnoreCase).Count();

            return (double)intersection / union;
        }

        /// <summary>
        /// Benzer işler için uyarı mesajı oluşturur
        /// </summary>
        private string BuildSimilarItemsWarning(List<SimilarWorkItemResult> similarItems)
        {
            var sb = new StringBuilder();
            sb.AppendLine("⚠️ DİKKAT: Benzer iş kayıtları bulundu!\n");
            sb.AppendLine("Aşağıdaki işler girdiğiniz iş ile benzerlik gösteriyor:\n");
            sb.AppendLine("─────────────────────────────────────────");

            foreach (var item in similarItems)
            {
                var statusDisplay = GetStatusDisplay(item.Status);
                var boardDisplay = GetBoardDisplay(item.Board);
                var similarityPercent = (int)(item.SimilarityScore * 100);
                
                sb.AppendLine($"\n📌 #{item.Id}: {TruncateText(item.Title, 50)}");
                sb.AppendLine($"   📍 {boardDisplay} | {statusDisplay}");
                sb.AppendLine($"   📅 {item.CreatedAt:dd.MM.yyyy} | 🎯 %{similarityPercent} benzerlik");
            }

            sb.AppendLine("\n─────────────────────────────────────────");
            sb.AppendLine("\nYine de bu işi eklemek istiyor musunuz?");

            return sb.ToString();
        }

        private string GetStatusDisplay(string status)
        {
            return status switch
            {
                "Bekliyor" => "⏳ Bekliyor",
                "Beklemede" => "⏸️ Beklemede",
                "MudahaleEdiliyor" => "🔧 Müdahale Ediliyor",
                "Cozuldu" => "✅ Çözüldü",
                "Arşivlendi" => "📦 Arşivlendi",
                _ => status
            };
        }

        private string GetBoardDisplay(string board)
        {
            return board switch
            {
                "Inbox" => "📥 Gelen Kutusu",
                "Kanban" => "📋 Kanban",
                "Scrum" => "🏃 Scrum",
                "Otopark" => "🚗 Otopark",
                _ => board
            };
        }

        private string TruncateText(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
                return text;
            return text.Substring(0, maxLength - 3) + "...";
        }

        /// <summary>
        /// Benzer iş sonucu için yardımcı sınıf
        /// </summary>
        private class SimilarWorkItemResult
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Status { get; set; }
            public string Board { get; set; }
            public DateTime CreatedAt { get; set; }
            public double SimilarityScore { get; set; }
        }

        #endregion
    }
}

