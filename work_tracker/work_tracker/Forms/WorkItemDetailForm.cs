using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraEditors;
using work_tracker.Data;
using work_tracker.Data.Entities;
using work_tracker.Helpers;

namespace work_tracker.Forms
{
    /// <summary>
    /// İş öğesi detay formu - Aktivite geçmişi ve yorum ekleme özellikleri ile
    /// </summary>
    public partial class WorkItemDetailForm : XtraForm
    {
        private WorkTrackerDbContext _context;
        private int _workItemId;
        private WorkItem _workItem;
        private string _currentUser = Environment.UserName; // Gerçek uygulamada auth sisteminden gelir

        public WorkItemDetailForm(int workItemId)
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
            _workItemId = workItemId;
        }

        private void WorkItemDetailForm_Load(object sender, EventArgs e)
        {
            LoadSprints();
            LoadWorkItemDetails();
            LoadActivities();
            LoadComments();
            LoadAttachments();
            LoadEmails();
            LoadTimeEntries();
            LoadRelations();
            
            // Yorum listesi çift tıklama event'i
            lstComments.DoubleClick += LstComments_DoubleClick;
        }

        private void LoadSprints()
        {
            var sprints = _context.Sprints
                .OrderByDescending(s => s.StartDate)
                .ToList();

            cmbSprint.Properties.DataSource = sprints;
            cmbSprint.Properties.DisplayMember = "Name";
            cmbSprint.Properties.ValueMember = "Id";
        }

        private void LoadTimeEntries()
        {
            try
            {
                var timeEntries = _context.TimeEntries
                    .Where(t => t.WorkItemId == _workItemId)
                    .OrderByDescending(t => t.EntryDate)
                    .Select(t => new
                    {
                        t.Id,
                        t.EntryDate,
                        t.DurationMinutes,
                        t.Subject,
                        t.ActivityType,
                        t.ContactName,
                        t.PhoneNumber,
                        t.Description,
                        t.CreatedBy,
                        t.CreatedAt
                    })
                    .ToList()
                    .Select(t => new
                    {
                        t.Id,
                        t.EntryDate,
                        t.DurationMinutes,
                        t.Subject,
                        Saat = TimeSpan.FromMinutes(t.DurationMinutes).ToString(@"hh\:mm"),
                        t.ActivityType,
                        AktiviteTipi = GetActivityTypeDisplay(t.ActivityType),
                        t.ContactName,
                        t.PhoneNumber,
                        t.Description,
                        t.CreatedBy,
                        t.CreatedAt
                    })
                    .ToList();

                gridTimeEntries.DataSource = timeEntries;

                var view = gridViewTimeEntries;
                view.BestFitColumns();

                // Kolon başlıkları
                if (view.Columns["Id"] != null) view.Columns["Id"].Caption = "ID";
                if (view.Columns["EntryDate"] != null) view.Columns["EntryDate"].Caption = "Tarih";
                if (view.Columns["DurationMinutes"] != null) view.Columns["DurationMinutes"].Caption = "Süre (dk)";
                if (view.Columns["Saat"] != null) view.Columns["Saat"].Caption = "Saat";
                if (view.Columns["Subject"] != null) view.Columns["Subject"].Caption = "Konu";
                if (view.Columns["ActivityType"] != null) view.Columns["ActivityType"].Visible = false;
                if (view.Columns["AktiviteTipi"] != null) view.Columns["AktiviteTipi"].Caption = "Aktivite Tipi";
                if (view.Columns["ContactName"] != null) view.Columns["ContactName"].Caption = "Kişi";
                if (view.Columns["PhoneNumber"] != null) view.Columns["PhoneNumber"].Caption = "Telefon";
                if (view.Columns["Description"] != null) view.Columns["Description"].Caption = "Açıklama";
                if (view.Columns["CreatedBy"] != null) view.Columns["CreatedBy"].Caption = "Oluşturan";
                if (view.Columns["CreatedAt"] != null) view.Columns["CreatedAt"].Caption = "Kayıt Tarihi";

                // Özetler
                view.Columns["DurationMinutes"].Summary.Clear();
                view.Columns["DurationMinutes"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "DurationMinutes", "Toplam Süre: {0} dk");

                view.Columns["Id"].Summary.Clear();
                view.Columns["Id"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "Id", "Toplam Kayıt: {0}");

                view.OptionsBehavior.Editable = false;
                view.OptionsView.ShowGroupPanel = false;
                view.OptionsView.ShowFooter = true;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Zaman kayıtları yüklenirken hata");
            }
        }

        private string GetActivityTypeDisplay(string activityType)
        {
            switch (activityType)
            {
                case TimeEntryActivityTypes.PhoneCall:
                    return "Telefon Görüşmesi";
                case TimeEntryActivityTypes.Work:
                    return "İş";
                case TimeEntryActivityTypes.Meeting:
                    return "Toplantı";
                case TimeEntryActivityTypes.Other:
                    return "Diğer";
                default:
                    return activityType;
            }
        }

        private void LoadWorkItemDetails()
        {
            _workItem = _context.WorkItems
                .Include(w => w.Project)
                .Include(w => w.Module)
                .Include(w => w.Sprint)
                .Include(w => w.InitialSprint)
                .Include(w => w.CompletedInSprint)
                .Include(w => w.SourceMeeting)
                .FirstOrDefault(w => w.Id == _workItemId);

            if (_workItem != null)
            {
                this.Text = $"İş Detayı: {_workItem.Title}";
                
                // Temel bilgileri doldur
                txtTitle.Text = _workItem.Title;
                txtDescription.Text = _workItem.Description;
                txtRequestedBy.Text = _workItem.RequestedBy;
                dtRequestedAt.EditValue = _workItem.RequestedAt;
                
                cmbStatus.EditValue = _workItem.Status;
                cmbType.EditValue = _workItem.Type;
                cmbUrgency.EditValue = _workItem.Urgency;
                
                txtProject.Text = _workItem.Project?.Name ?? "-";
                txtModule.Text = _workItem.Module?.Name ?? "-";
                
                // Sprint bilgileri
                cmbSprint.EditValue = _workItem.SprintId;
                txtBoard.Text = _workItem.Board ?? "-";
                
                // Sprint geçmişi
                lblInitialSprint.Text = _workItem.InitialSprint != null 
                    ? $"İlk Sprint: {_workItem.InitialSprint.Name}" 
                    : "İlk Sprint: -";
                
                lblCompletedInSprint.Text = _workItem.CompletedInSprint != null 
                    ? $"Tamamlanan Sprint: {_workItem.CompletedInSprint.Name}" 
                    : "Tamamlanan Sprint: -";
                
                if (_workItem.EffortEstimate.HasValue)
                    txtEffort.Text = _workItem.EffortEstimate.Value.ToString("0.0");
                
                lblCreatedAt.Text = $"Oluşturulma: {_workItem.CreatedAt:dd.MM.yyyy HH:mm}";
                if (_workItem.CompletedAt.HasValue)
                    lblCompletedAt.Text = $"Tamamlanma: {_workItem.CompletedAt:dd.MM.yyyy HH:mm}";

                // Geliştirme süresini hesapla ve göster
                CalculateAndShowDevelopmentTime();
            }
        }

        private void CalculateAndShowDevelopmentTime()
        {
            try
            {
                var statusActivities = _context.WorkItemActivities
                    .Where(a => a.WorkItemId == _workItemId && a.ActivityType == WorkItemActivityTypes.StatusChange)
                    .ToList();

                var totalDevTime = DevelopmentTimeHelper.CalculateTotalDuration(_workItem, statusActivities);

                var lblDevTime = this.Controls.Find("lblDevTime", true).FirstOrDefault() as LabelControl;
                if (totalDevTime <= TimeSpan.Zero)
                {
                    if (lblDevTime != null)
                    {
                        lblDevTime.Visible = false;
                    }
                    return;
                }

                string durationStr = "";
                if (totalDevTime.TotalDays >= 1)
                    durationStr += $"{(int)totalDevTime.TotalDays} gün ";
                if (totalDevTime.Hours > 0)
                    durationStr += $"{totalDevTime.Hours} sa ";
                if (totalDevTime.Minutes > 0)
                    durationStr += $"{totalDevTime.Minutes} dk";

                if (string.IsNullOrEmpty(durationStr))
                    durationStr = "< 1 dk";

                if (lblDevTime == null)
                {
                    lblDevTime = new LabelControl
                    {
                        Name = "lblDevTime"
                    };
                    lblDevTime.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
                    lblDevTime.Appearance.ForeColor = Color.DarkOrange;

                    if (lblCompletedAt != null)
                    {
                        lblDevTime.Location = new Point(lblCompletedAt.Location.X, lblCompletedAt.Location.Y + 20);
                        lblCompletedAt.Parent.Controls.Add(lblDevTime);
                    }
                }

                lblDevTime.Text = $"Geliştirme Süresi: {durationStr}";
                lblDevTime.Visible = true;
            }
            catch (Exception ex)
            {
                Logger.Error("Geliştirme süresi hesaplanırken hata", ex);
            }
        }

        private void LoadActivities()
        {
            var activities = _context.WorkItemActivities
                .Where(a => a.WorkItemId == _workItemId)
                .OrderByDescending(a => a.CreatedAt)
                .Select(a => new
                {
                    a.Id,
                    a.ActivityType,
                    a.Description,
                    a.OldValue,
                    a.NewValue,
                    a.CreatedBy,
                    a.CreatedAt
                })
                .ToList();

            // Activity listesini göster
            lstActivities.Items.Clear();
            foreach (var activity in activities)
            {
                var item = new ListViewItem(activity.CreatedAt.ToString("dd.MM.yyyy HH:mm"));
                item.SubItems.Add(GetActivityTypeText(activity.ActivityType));
                item.SubItems.Add(FormatActivityDescription(activity));
                item.SubItems.Add(activity.CreatedBy);
                item.Tag = activity.Id;
                
                lstActivities.Items.Add(item);
            }

            lblActivityCount.Text = $"Toplam {activities.Count} aktivite";
        }

        private string GetActivityTypeText(string activityType)
        {
            switch (activityType)
            {
                case WorkItemActivityTypes.Comment: return "💬 Yorum";
                case WorkItemActivityTypes.StatusChange: return "📊 Durum";
                case WorkItemActivityTypes.AssignmentChange: return "👤 Atama";
                case WorkItemActivityTypes.FieldUpdate: return "✏️ Güncelleme";
                case WorkItemActivityTypes.Created: return "✨ Oluşturuldu";
                case WorkItemActivityTypes.PriorityChange: return "⚡ Öncelik";
                case WorkItemActivityTypes.EstimateChange: return "⏱️ Efor";
                default: return "📝 Diğer";
            }
        }

        private string FormatActivityDescription(dynamic activity)
        {
            if (activity.ActivityType == WorkItemActivityTypes.StatusChange && 
                !string.IsNullOrEmpty(activity.OldValue) && 
                !string.IsNullOrEmpty(activity.NewValue))
            {
                return $"{activity.OldValue} → {activity.NewValue}";
            }
            
            return activity.Description ?? "";
        }

        private void btnAddComment_Click(object sender, EventArgs e)
        {
            var comment = txtNewComment.Text.Trim();
            if (string.IsNullOrWhiteSpace(comment))
            {
                XtraMessageBox.Show("Lütfen bir yorum yazın.", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Yorum ekle
            AddActivity(WorkItemActivityTypes.Comment, comment);
            
            txtNewComment.Text = "";
            XtraMessageBox.Show("Yorum başarıyla eklendi.", "Bilgi", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnChangeStatus_Click(object sender, EventArgs e)
        {
            if (cmbStatus.EditValue == null)
            {
                XtraMessageBox.Show("Lütfen bir durum seçin.", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newStatus = cmbStatus.EditValue.ToString();
            var oldStatus = _workItem.Status;

            if (oldStatus == newStatus)
            {
                XtraMessageBox.Show("Durum değişmedi.", "Bilgi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Durum değiştir
            _workItem.Status = newStatus;
            
            if (newStatus == "Tamamlandı" && !_workItem.CompletedAt.HasValue)
            {
                _workItem.CompletedAt = DateTime.Now;
            }

            _context.SaveChanges();

            // Aktivite kaydet
            AddActivity(WorkItemActivityTypes.StatusChange, 
                $"Durum değiştirildi: {oldStatus} → {newStatus}", 
                oldStatus, 
                newStatus);

            XtraMessageBox.Show("Durum başarıyla güncellendi.", "Bilgi", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            LoadWorkItemDetails();
        }

        private void AddActivity(string activityType, string description, 
            string oldValue = null, string newValue = null)
        {
            var activity = new WorkItemActivity
            {
                WorkItemId = _workItemId,
                ActivityType = activityType,
                Description = description,
                OldValue = oldValue,
                NewValue = newValue,
                CreatedBy = _currentUser,
                CreatedAt = DateTime.Now
            };

            _context.WorkItemActivities.Add(activity);
            _context.SaveChanges();
            
            LoadActivities();
            LoadComments();
        }

        private void LoadComments()
        {
            var comments = _context.WorkItemActivities
                .Where(a => a.WorkItemId == _workItemId && a.ActivityType == WorkItemActivityTypes.Comment)
                .OrderByDescending(a => a.CreatedAt)
                .Select(a => new
                {
                    a.Id,
                    a.Description,
                    a.CreatedBy,
                    a.CreatedAt
                })
                .ToList();

            // Yorum listesini göster
            lstComments.Items.Clear();
            foreach (var comment in comments)
            {
                var item = new ListViewItem(comment.CreatedAt.ToString("dd.MM.yyyy HH:mm"));
                item.SubItems.Add(comment.CreatedBy);
                item.SubItems.Add(comment.Description ?? "");
                item.Tag = comment.Id;
                
                lstComments.Items.Add(item);
            }

            lblCommentCount.Text = $"Toplam {comments.Count} yorum";
        }

        private void LstComments_DoubleClick(object sender, EventArgs e)
        {
            if (lstComments.SelectedItems.Count == 0)
                return;

            var commentId = (int)lstComments.SelectedItems[0].Tag;
            
            // Yorumu veritabanından çek
            var comment = _context.WorkItemActivities
                .FirstOrDefault(a => a.Id == commentId && a.ActivityType == WorkItemActivityTypes.Comment);

            if (comment == null)
                return;

            // Yorum detay modalını göster
            ShowCommentDetail(comment);
        }

        private void ShowCommentDetail(WorkItemActivity comment)
        {
            using (var detailForm = new XtraForm())
            {
                detailForm.Text = "Yorum Detayı";
                detailForm.Size = new Size(700, 500);
                detailForm.StartPosition = FormStartPosition.CenterParent;
                detailForm.MinimumSize = new Size(500, 300);

                // Ana panel
                var mainPanel = new DevExpress.XtraEditors.PanelControl
                {
                    Dock = DockStyle.Fill,
                    Padding = new Padding(10)
                };

                // Bilgi paneli (üst kısım)
                var infoPanel = new DevExpress.XtraEditors.PanelControl
                {
                    Dock = DockStyle.Top,
                    Height = 80,
                    Padding = new Padding(10)
                };

                var lblCreatedBy = new LabelControl
                {
                    Text = $"Yazan: {comment.CreatedBy}",
                    Location = new Point(10, 10),
                    AutoSizeMode = LabelAutoSizeMode.None,
                    Width = 300
                };

                var lblCreatedAt = new LabelControl
                {
                    Text = $"Tarih: {comment.CreatedAt:dd.MM.yyyy HH:mm:ss}",
                    Location = new Point(10, 35),
                    AutoSizeMode = LabelAutoSizeMode.None,
                    Width = 300
                };

                infoPanel.Controls.Add(lblCreatedBy);
                infoPanel.Controls.Add(lblCreatedAt);

                // Yorum içeriği (kopyalanabilir)
                var memoComment = new MemoEdit
                {
                    Dock = DockStyle.Fill,
                    Text = comment.Description ?? "",
                    Properties = 
                    {
                        ReadOnly = false, // Kopyalama için readonly false
                        ScrollBars = ScrollBars.Both,
                        WordWrap = true
                    },
                    Margin = new Padding(10, 0, 10, 10)
                };

                // Alt panel (butonlar)
                var buttonPanel = new DevExpress.XtraEditors.PanelControl
                {
                    Dock = DockStyle.Bottom,
                    Height = 50,
                    Padding = new Padding(10)
                };

                var btnCopy = new SimpleButton
                {
                    Text = "Kopyala",
                    DialogResult = DialogResult.None,
                    Location = new Point(10, 10),
                    Width = 100
                };

                btnCopy.Click += (s, args) =>
                {
                    if (!string.IsNullOrEmpty(memoComment.Text))
                    {
                        Clipboard.SetText(memoComment.Text);
                        XtraMessageBox.Show("Yorum içeriği panoya kopyalandı.", "Bilgi",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                };

                var btnClose = new SimpleButton
                {
                    Text = "Kapat",
                    DialogResult = DialogResult.OK,
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    Width = 100
                };

                buttonPanel.Controls.Add(btnCopy);
                buttonPanel.Controls.Add(btnClose);
                
                // Buton konumlarını ayarla
                detailForm.Load += (s, args) =>
                {
                    btnClose.Location = new Point(buttonPanel.Width - btnClose.Width - 10, 10);
                };

                mainPanel.Controls.Add(memoComment);
                mainPanel.Controls.Add(infoPanel);
                mainPanel.Controls.Add(buttonPanel);

                detailForm.Controls.Add(mainPanel);
                detailForm.AcceptButton = btnClose;

                detailForm.ShowDialog(this);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadWorkItemDetails();
            LoadActivities();
            LoadComments();
            LoadAttachments();
        }

        private void LoadRelations()
        {
            try
            {
                var relations = _context.WorkItemRelations
                    .Where(r => r.WorkItemId1 == _workItemId || r.WorkItemId2 == _workItemId)
                    .Include(r => r.SourceWorkItem)
                    .Include(r => r.TargetWorkItem)
                    .ToList();

                var relationList = new System.Collections.Generic.List<object>();
                var processedSiblingIds = new HashSet<int>();

                foreach (var relation in relations)
                {
                    var isCurrentSource = relation.WorkItemId1 == _workItemId;
                    var isCurrentTarget = relation.WorkItemId2 == _workItemId;
                    if (!isCurrentSource && !isCurrentTarget)
                        continue;

                    var otherWorkItemId = isCurrentSource ? relation.WorkItemId2 : relation.WorkItemId1;
                    var otherWorkItem = isCurrentSource ? relation.TargetWorkItem : relation.SourceWorkItem;

                    if (otherWorkItem == null)
                    {
                        otherWorkItem = _context.WorkItems.Find(otherWorkItemId);
                    }

                    if (otherWorkItem == null)
                        continue;

                    string relationType;
                    string relationTypeText;

                    if (relation.RelationType == WorkItemRelationTypes.Parent)
                    {
                        if (isCurrentSource)
                        {
                            relationType = "Child";
                            relationTypeText = "🔻 Alt İş";
                        }
                        else
                        {
                            relationType = "Parent";
                            relationTypeText = "🔺 Üst İş";
                        }
                    }
                    else if (relation.RelationType == WorkItemRelationTypes.Sibling)
                    {
                        if (!processedSiblingIds.Add(otherWorkItem.Id))
                        {
                            continue;
                        }

                        relationType = "Sibling";
                        relationTypeText = "🔗 Kardeş İş";
                    }
                    else
                    {
                        continue;
                    }

                    relationList.Add(new
                    {
                        Id = otherWorkItem.Id,
                        RelationType = relationType,
                        RelationTypeText = relationTypeText,
                        Title = otherWorkItem.Title,
                        Status = otherWorkItem.Status,
                        CreatedAt = relation.CreatedAt,
                        CreatedBy = relation.CreatedBy
                    });
                }

                gridRelations.DataSource = relationList;

                var view = gridViewRelations;
                view.BestFitColumns();

                // Kolon başlıkları
                if (view.Columns["RelationTypeText"] != null)
                {
                    view.Columns["RelationTypeText"].Caption = "İlişki Tipi";
                    view.Columns["RelationTypeText"].Width = 120;
                }
                if (view.Columns["Title"] != null)
                {
                    view.Columns["Title"].Caption = "İş Başlığı";
                    view.Columns["Title"].Width = 300;
                }
                if (view.Columns["Status"] != null)
                {
                    view.Columns["Status"].Caption = "Durum";
                    view.Columns["Status"].Width = 100;
                }
                if (view.Columns["CreatedAt"] != null)
                {
                    view.Columns["CreatedAt"].Caption = "İlişki Tarihi";
                    view.Columns["CreatedAt"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    view.Columns["CreatedAt"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
                    view.Columns["CreatedAt"].Width = 130;
                }
                if (view.Columns["CreatedBy"] != null)
                {
                    view.Columns["CreatedBy"].Caption = "Oluşturan";
                    view.Columns["CreatedBy"].Width = 100;
                }

                // Id kolonunu gizle
                if (view.Columns["Id"] != null) view.Columns["Id"].Visible = false;
                if (view.Columns["RelationType"] != null) view.Columns["RelationType"].Visible = false;

                lblRelationCount.Text = $"Toplam {relationList.Count} ilişki";

                view.OptionsBehavior.Editable = false;
                view.OptionsView.ShowGroupPanel = false;
                view.OptionsView.ShowFooter = false;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "İlişkiler yüklenirken hata");
            }
        }

        private void btnOpenRelatedWorkItem_Click(object sender, EventArgs e)
        {
            try
            {
                var view = gridViewRelations;
                if (view.FocusedRowHandle < 0) return;

                var relatedWorkItemId = (int)view.GetRowCellValue(view.FocusedRowHandle, "Id");
                var relationType = view.GetRowCellValue(view.FocusedRowHandle, "RelationType")?.ToString();

                if (relationType == "Parent" || relationType == "Child" || relationType == "Sibling")
                {
                    // İlgili işin detay formunu aç
                    var detailForm = new WorkItemDetailForm(relatedWorkItemId);
                    if (detailForm.ShowDialog() == DialogResult.OK)
                    {
                        // Form kapandıktan sonra ilişkileri yenile
                        LoadRelations();
                        LoadWorkItemDetails(); // Ana iş bilgilerini de güncelle
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "İlişkili iş açılırken hata");
                XtraMessageBox.Show("İlişkili iş açılırken bir hata oluştu.", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChangeSprint_Click(object sender, EventArgs e)
        {
            if (_workItem == null) return;

            var newSprintId = cmbSprint.EditValue as int?;
            var oldSprintId = _workItem.SprintId;

            // Değişiklik yoksa çık
            if (newSprintId == oldSprintId)
            {
                XtraMessageBox.Show("Sprint değişikliği yapılmadı.", "Bilgi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = XtraMessageBox.Show(
                $"İşin sprint'i değiştirilecek.\n\n" +
                $"Eski Sprint: {_workItem.Sprint?.Name ?? "(Yok)"}\n" +
                $"Yeni Sprint: {(newSprintId.HasValue ? cmbSprint.Text : "(Yok)")}\n\n" +
                $"Devam etmek istiyor musunuz?",
                "Sprint Değiştir",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            try
            {
                var dbWorkItem = _context.WorkItems.Find(_workItemId);
                if (dbWorkItem != null)
                {
                    var oldSprintName = dbWorkItem.Sprint?.Name ?? "(Yok)";
                    
                    // Sprint değiştir
                    dbWorkItem.SprintId = newSprintId;

                    // İlk kez sprint'e alınıyorsa InitialSprintId'yi set et
                    if (!dbWorkItem.InitialSprintId.HasValue && newSprintId.HasValue)
                    {
                        dbWorkItem.InitialSprintId = newSprintId;
                    }

                    // Board'u da güncelle (sprint varsa Scrum, yoksa Inbox)
                    if (newSprintId.HasValue)
                    {
                        dbWorkItem.Board = "Scrum";
                        dbWorkItem.Status = "SprintBacklog";
                    }

                    _context.SaveChanges();

                    // Aktivite kaydı
                    var newSprintName = newSprintId.HasValue 
                        ? _context.Sprints.Find(newSprintId.Value)?.Name ?? "(Bilinmeyen)" 
                        : "(Yok)";
                    
                    AddActivity(
                        WorkItemActivityTypes.FieldUpdate,
                        $"Sprint değiştirildi: {oldSprintName} → {newSprintName}",
                        oldSprintName,
                        newSprintName);

                    XtraMessageBox.Show("Sprint başarıyla değiştirildi.", "Başarılı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadWorkItemDetails();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Sprint değiştirme sırasında hata oluştu: {ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Sprint değiştirme hatası", ex);
            }
        }

        #region Dosya Yönetimi

        private void LoadAttachments()
        {
            var attachments = _context.WorkItemAttachments
                .Where(a => a.WorkItemId == _workItemId)
                .OrderByDescending(a => a.UploadedAt)
                .ToList();

            lstAttachments.Items.Clear();
            foreach (var attachment in attachments)
            {
                var item = new ListViewItem(FileStorageHelper.GetFileIcon(attachment.FileExtension));
                item.SubItems.Add(attachment.OriginalFileName);
                item.SubItems.Add(FileStorageHelper.FormatFileSize(attachment.FileSizeBytes));
                item.SubItems.Add(attachment.Description ?? "-");
                item.SubItems.Add(attachment.UploadedBy);
                item.SubItems.Add(attachment.UploadedAt.ToString("dd.MM.yyyy HH:mm"));
                item.Tag = attachment;
                
                lstAttachments.Items.Add(item);
            }

            lblAttachmentCount.Text = $"Toplam {attachments.Count} dosya";
            
            // Toplam boyut
            var totalSize = attachments.Sum(a => a.FileSizeBytes);
            lblTotalSize.Text = $"Toplam Boyut: {FileStorageHelper.FormatFileSize(totalSize)}";
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Dosya Seç";
                openFileDialog.Filter = "Tüm Dosyalar (*.*)|*.*|" +
                                       "SQL Dosyaları (*.sql)|*.sql|" +
                                       "PDF Dosyaları (*.pdf)|*.pdf|" +
                                       "Word Dosyaları (*.doc;*.docx)|*.doc;*.docx|" +
                                       "Excel Dosyaları (*.xls;*.xlsx)|*.xls;*.xlsx|" +
                                       "Resim Dosyaları (*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif|" +
                                       "Metin Dosyaları (*.txt)|*.txt";
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (var filePath in openFileDialog.FileNames)
                    {
                        try
                        {
                            UploadFile(filePath);
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show($"Dosya yüklenemedi: {Path.GetFileName(filePath)}\n\nHata: {ex.Message}", 
                                "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    LoadAttachments();
                    XtraMessageBox.Show($"{openFileDialog.FileNames.Length} dosya başarıyla yüklendi.", 
                        "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void UploadFile(string sourceFilePath)
        {
            var fileInfo = new FileInfo(sourceFilePath);
            var originalFileName = fileInfo.Name;
            var extension = fileInfo.Extension;

            // Dosyayı fiziksel olarak kaydet
            var relativePath = FileStorageHelper.SaveFile(_workItemId, sourceFilePath, originalFileName);

            // Veritabanına kaydet
            var attachment = new WorkItemAttachment
            {
                WorkItemId = _workItemId,
                OriginalFileName = originalFileName,
                StoredFileName = Path.GetFileName(relativePath),
                FileExtension = extension,
                FileSizeBytes = fileInfo.Length,
                MimeType = FileStorageHelper.GetMimeType(extension),
                FilePath = relativePath,
                UploadedBy = _currentUser,
                UploadedAt = DateTime.Now
            };

            _context.WorkItemAttachments.Add(attachment);
            _context.SaveChanges();

            // Aktivite kaydet
            AddActivity(WorkItemActivityTypes.FieldUpdate, 
                $"Dosya eklendi: {originalFileName} ({FileStorageHelper.FormatFileSize(fileInfo.Length)})");
        }

        private void btnDownloadFile_Click(object sender, EventArgs e)
        {
            if (lstAttachments.SelectedItems.Count == 0)
            {
                XtraMessageBox.Show("Lütfen indirmek istediğiniz dosyayı seçin.", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var attachment = lstAttachments.SelectedItems[0].Tag as WorkItemAttachment;
            if (attachment == null) return;

            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.FileName = attachment.OriginalFileName;
                saveFileDialog.Filter = $"*{attachment.FileExtension}|*{attachment.FileExtension}|Tüm Dosyalar (*.*)|*.*";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (FileStorageHelper.TryGetExistingFile(attachment.FilePath, out string sourcePath))
                        {
                            File.Copy(sourcePath, saveFileDialog.FileName, overwrite: true);
                            
                            XtraMessageBox.Show("Dosya başarıyla indirildi.", "Başarılı", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            XtraMessageBox.Show("Dosya bulunamadı. Silinmiş veya taşınmış olabilir.", "Hata", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show($"Dosya indirilemedi:\n\n{ex.Message}", "Hata", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (lstAttachments.SelectedItems.Count == 0)
            {
                XtraMessageBox.Show("Lütfen açmak istediğiniz dosyayı seçin.", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var attachment = lstAttachments.SelectedItems[0].Tag as WorkItemAttachment;
            if (attachment == null) return;

            try
            {
                if (FileStorageHelper.TryGetExistingFile(attachment.FilePath, out string fullPath))
                {
                    Process.Start(fullPath);
                }
                else
                {
                    XtraMessageBox.Show("Dosya bulunamadı. Silinmiş olabilir.", "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Dosya açılamadı:\n\n{ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPreviewFile_Click(object sender, EventArgs e)
        {
            if (lstAttachments.SelectedItems.Count == 0)
            {
                XtraMessageBox.Show("Lütfen önizlemek istediğiniz dosyayı seçin.", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var attachment = lstAttachments.SelectedItems[0].Tag as WorkItemAttachment;
            if (attachment == null) return;

            // Sadece metin tabanlı dosyalar için önizleme
            if (!IsTextPreviewSupported(attachment.FileExtension, attachment.MimeType))
            {
                XtraMessageBox.Show(
                    "Bu dosya türü için uygulama içi önizleme desteklenmiyor.\n\n" +
                    "'Aç' butonu ile dosyayı varsayılan uygulamada açabilirsiniz.", 
                    "Önizleme Desteklenmiyor", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
                return;
            }

            try
            {
                if (!FileStorageHelper.TryGetExistingFile(attachment.FilePath, out string fullPath))
                {
                    XtraMessageBox.Show("Dosya bulunamadı. Silinmiş veya taşınmış olabilir."  + fullPath + " - " + attachment.FilePath, "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var content = File.ReadAllText(fullPath);
                
                // Önizleme dialogu aç
                using (var previewForm = new XtraForm())
                {
                    previewForm.Text = $"Dosya Önizleme: {attachment.OriginalFileName}";
                    previewForm.Size = new Size(1000, 700);
                    previewForm.StartPosition = FormStartPosition.CenterParent;
                    
                    var memoEdit = new MemoEdit
                    {
                        Dock = DockStyle.Fill,
                        Text = content,
                        Properties = { ReadOnly = true, ScrollBars = ScrollBars.Both }
                    };
                    
                    var btnClose = new SimpleButton
                    {
                        Text = "Kapat",
                        DialogResult = DialogResult.OK,
                        Dock = DockStyle.Bottom,
                        Height = 40
                    };
                    
                    previewForm.Controls.Add(memoEdit);
                    previewForm.Controls.Add(btnClose);
                    previewForm.AcceptButton = btnClose;
                    
                    previewForm.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Önizleme sırasında hata oluştu:\n\n{ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsTextPreviewSupported(string extension, string mimeType)
        {
            extension = (extension ?? "").ToLowerInvariant();
            mimeType = (mimeType ?? "").ToLowerInvariant();

            // Yaygın metin tabanlı uzantılar
            string[] textExtensions =
            {
                ".txt", ".sql", ".log", ".cs", ".config", ".json", ".xml", ".yml", ".yaml", ".ini", ".cmd",
                ".bat"
            };

            if (textExtensions.Contains(extension))
                return true;

            if (mimeType.StartsWith("text/"))
                return true;

            return false;
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            if (lstAttachments.SelectedItems.Count == 0)
            {
                XtraMessageBox.Show("Lütfen silmek istediğiniz dosyayı seçin.", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var attachment = lstAttachments.SelectedItems[0].Tag as WorkItemAttachment;
            if (attachment == null) return;

            var result = XtraMessageBox.Show(
                $"'{attachment.OriginalFileName}' dosyasını silmek istediğinizden emin misiniz?\n\nBu işlem geri alınamaz!", 
                "Dosya Sil", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Fiziksel dosyayı sil
                    FileStorageHelper.DeleteFile(attachment.FilePath);

                    // Veritabanından sil
                    _context.WorkItemAttachments.Remove(attachment);
                    _context.SaveChanges();

                    // Aktivite kaydet
                    AddActivity(WorkItemActivityTypes.FieldUpdate, 
                        $"Dosya silindi: {attachment.OriginalFileName}");

                    LoadAttachments();
                    XtraMessageBox.Show("Dosya başarıyla silindi.", "Başarılı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Dosya silinemedi:\n\n{ex.Message}", "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Email Yönetimi

        private void LoadEmails()
        {
            var emails = _context.WorkItemEmails
                .Where(e => e.WorkItemId == _workItemId)
                .OrderByDescending(e => e.ReceivedDate ?? e.SentDate ?? e.LinkedAt)
                .ToList();

            lstEmails.Items.Clear();
            foreach (var email in emails)
            {
                var item = new ListViewItem((email.ReceivedDate ?? email.SentDate ?? email.LinkedAt).ToString("dd.MM.yyyy HH:mm"));
                item.SubItems.Add(email.From ?? "-");
                item.SubItems.Add(email.Subject ?? "-");
                item.SubItems.Add(email.IsRead ? "✓" : "✗");
                item.SubItems.Add(email.HasAttachments ? $"📎 {email.AttachmentCount}" : "-");
                item.Tag = email;
                
                lstEmails.Items.Add(item);
            }

            lblEmailCount.Text = $"Toplam {emails.Count} email";
        }

        private void btnLinkEmail_Click(object sender, EventArgs e)
        {
            try
            {
                // Outlook'tan email'leri çek
                var outlookEmails = OutlookHelper.GetEmailsFromOutlook(50, txtSearchEmail.Text);

                if (outlookEmails.Count == 0)
                {
                    XtraMessageBox.Show("Outlook'ta email bulunamadı.", "Bilgi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Email seçim formu (basit bir liste)
                using (var form = new XtraForm())
                {
                    form.Text = "Email Seç";
                    form.Size = new System.Drawing.Size(800, 500);
                    form.StartPosition = FormStartPosition.CenterParent;

                    var listBox = new System.Windows.Forms.ListBox
                    {
                        Dock = DockStyle.Fill,
                        DisplayMember = "Subject"
                    };

                    foreach (var email in outlookEmails)
                    {
                        listBox.Items.Add(email);
                    }

                    var btnSelect = new SimpleButton
                    {
                        Text = "Seç",
                        Dock = DockStyle.Bottom,
                        Height = 40
                    };

                    var btnCancel = new SimpleButton
                    {
                        Text = "İptal",
                        Dock = DockStyle.Bottom,
                        Height = 40
                    };

                    btnSelect.Click += (s, args) => { form.DialogResult = DialogResult.OK; form.Close(); };
                    btnCancel.Click += (s, args) => { form.DialogResult = DialogResult.Cancel; form.Close(); };

                    form.Controls.Add(listBox);
                    form.Controls.Add(btnSelect);
                    form.Controls.Add(btnCancel);

                    if (form.ShowDialog() == DialogResult.OK && listBox.SelectedItem != null)
                    {
                        var selectedEmail = listBox.SelectedItem as WorkItemEmail;
                        if (selectedEmail != null)
                        {
                            // Email'i veritabanına kaydet ve işe bağla
                            selectedEmail.WorkItemId = _workItemId;
                            selectedEmail.LinkedBy = Environment.UserName;
                            selectedEmail.LinkedAt = DateTime.Now;

                            _context.WorkItemEmails.Add(selectedEmail);
                            _context.SaveChanges();

                            // Aktivite kaydet
                            AddActivity(WorkItemActivityTypes.FieldUpdate, 
                                $"Email bağlandı: {selectedEmail.Subject}");

                            LoadEmails();
                            XtraMessageBox.Show("Email başarıyla bağlandı.", "Başarılı", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Email bağlanırken hata oluştu:\n\n{ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Email bağlama hatası", ex);
            }
        }

        private void btnOpenEmail_Click(object sender, EventArgs e)
        {
            if (lstEmails.SelectedItems.Count == 0)
            {
                XtraMessageBox.Show("Lütfen bir email seçin.", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var email = lstEmails.SelectedItems[0].Tag as WorkItemEmail;
            if (email == null || string.IsNullOrEmpty(email.OutlookEntryId))
            {
                XtraMessageBox.Show("Bu email Outlook'ta bulunamadı.", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                OutlookHelper.OpenEmailInOutlook(email.OutlookEntryId);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Email açılırken hata oluştu:\n\n{ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Email açma hatası", ex);
            }
        }

        private void btnUnlinkEmail_Click(object sender, EventArgs e)
        {
            if (lstEmails.SelectedItems.Count == 0)
            {
                XtraMessageBox.Show("Lütfen bir email seçin.", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var email = lstEmails.SelectedItems[0].Tag as WorkItemEmail;
            if (email == null) return;

            var result = XtraMessageBox.Show(
                $"Bu email'in bağlantısını kaldırmak istediğinizden emin misiniz?\n\n" +
                $"Konu: {email.Subject}",
                "Bağlantıyı Kaldır",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            try
            {
                email.WorkItemId = null;
                email.LinkedBy = null;
                _context.SaveChanges();

                // Aktivite kaydet
                AddActivity(WorkItemActivityTypes.FieldUpdate, 
                    $"Email bağlantısı kaldırıldı: {email.Subject}");

                LoadEmails();
                XtraMessageBox.Show("Email bağlantısı kaldırıldı.", "Başarılı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Email bağlantısı kaldırılırken hata oluştu:\n\n{ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Email bağlantısı kaldırma hatası", ex);
            }
        }

        private void btnRefreshEmails_Click(object sender, EventArgs e)
        {
            LoadEmails();
        }

        private void btnDailyReport_Click(object sender, EventArgs e)
        {
            try
            {
                var reportForm = new DailyActivityReportForm(_workItemId);
                reportForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Rapor açılırken hata oluştu:\n\n{ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Günlük rapor açma hatası", ex);
            }
        }

        //private void LoadRelations()
        //{
        //    try
        //    {
        //        // Parent işi bul
        //        var parentWorkItem = _context.WorkItemRelations
        //            .Where(r => r.WorkItemId2 == _workItemId && r.RelationType == WorkItemRelationTypes.Parent)
        //            .Select(r => r.SourceWorkItem)
        //            .FirstOrDefault();

        //        // Sibling işleri bul
        //        var siblingWorkItems = _context.WorkItemRelations
        //            .Where(r => (r.WorkItemId1 == _workItemId || r.WorkItemId2 == _workItemId) &&
        //                       r.RelationType == WorkItemRelationTypes.Sibling)
        //            .Select(r => r.WorkItemId1 == _workItemId ? r.TargetWorkItem : r.SourceWorkItem)
        //            .Distinct()
        //            .ToList();

        //        var relationsList = new List<object>();

        //        // Parent işi ekle
        //        if (parentWorkItem != null)
        //        {
        //            relationsList.Add(new
        //            {
        //                Id = parentWorkItem.Id,
        //                Title = parentWorkItem.Title,
        //                Type = "Üst İş (Parent)",
        //                Status = parentWorkItem.Status,
        //                Project = parentWorkItem.Project?.Name ?? "-",
        //                RelationType = "Parent"
        //            });
        //        }

        //        // Sibling işleri ekle
        //        foreach (var sibling in siblingWorkItems)
        //        {
        //            relationsList.Add(new
        //            {
        //                Id = sibling.Id,
        //                Title = sibling.Title,
        //                Type = "Kardeş İş (Sibling)",
        //                Status = sibling.Status,
        //                Project = sibling.Project?.Name ?? "-",
        //                RelationType = "Sibling"
        //            });
        //        }

        //        gridRelations.DataSource = relationsList;

        //        var view = gridViewRelations;
        //        view.BestFitColumns();

        //        // Kolon başlıkları
        //        if (view.Columns["Id"] != null) view.Columns["Id"].Caption = "ID";
        //        if (view.Columns["Title"] != null) view.Columns["Title"].Caption = "Başlık";
        //        if (view.Columns["Type"] != null) view.Columns["Type"].Caption = "İlişki Tipi";
        //        if (view.Columns["Status"] != null) view.Columns["Status"].Caption = "Durum";
        //        if (view.Columns["Project"] != null) view.Columns["Project"].Caption = "Proje";
        //        if (view.Columns["RelationType"] != null) view.Columns["RelationType"].Visible = false;

        //        // Özet
        //        lblRelationCount.Text = $"Toplam {relationsList.Count} ilişkili iş";

        //        view.OptionsBehavior.Editable = false;
        //        view.OptionsView.ShowGroupPanel = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogException(ex, "İlişkiler yüklenirken hata");
        //        XtraMessageBox.Show($"İlişkiler yüklenirken hata oluştu: {ex.Message}", "Hata",
        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void btnOpenRelatedWorkItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var view = gridViewRelations;
        //        if (view.FocusedRowHandle < 0) return;

        //        var workItemId = Convert.ToInt32(view.GetFocusedRowCellValue("Id"));
                
        //        // İlgili iş detay formunu aç
        //        using (var detailForm = new WorkItemDetailForm(workItemId))
        //        {
        //            detailForm.ShowDialog();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogException(ex, "İlişkili iş açılırken hata");
        //        XtraMessageBox.Show($"İş açılırken hata oluştu: {ex.Message}", "Hata",
        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        #endregion
    }
}

