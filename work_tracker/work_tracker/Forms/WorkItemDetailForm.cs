using System;
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
            LoadWorkItemDetails();
            LoadActivities();
            LoadAttachments();
        }

        private void LoadWorkItemDetails()
        {
            _workItem = _context.WorkItems
                .Include(w => w.Project)
                .Include(w => w.Module)
                .Include(w => w.Sprint)
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
                txtSprint.Text = _workItem.Sprint?.Name ?? "-";
                txtBoard.Text = _workItem.Board ?? "-";
                
                if (_workItem.EffortEstimate.HasValue)
                    txtEffort.Text = _workItem.EffortEstimate.Value.ToString("0.0");
                
                lblCreatedAt.Text = $"Oluşturulma: {_workItem.CreatedAt:dd.MM.yyyy HH:mm}";
                if (_workItem.CompletedAt.HasValue)
                    lblCompletedAt.Text = $"Tamamlanma: {_workItem.CompletedAt:dd.MM.yyyy HH:mm}";
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
                case ActivityTypes.Comment: return "💬 Yorum";
                case ActivityTypes.StatusChange: return "📊 Durum";
                case ActivityTypes.AssignmentChange: return "👤 Atama";
                case ActivityTypes.FieldUpdate: return "✏️ Güncelleme";
                case ActivityTypes.Created: return "✨ Oluşturuldu";
                case ActivityTypes.PriorityChange: return "⚡ Öncelik";
                case ActivityTypes.EstimateChange: return "⏱️ Efor";
                default: return "📝 Diğer";
            }
        }

        private string FormatActivityDescription(dynamic activity)
        {
            if (activity.ActivityType == ActivityTypes.StatusChange && 
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
            AddActivity(ActivityTypes.Comment, comment);
            
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
            AddActivity(ActivityTypes.StatusChange, 
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
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadWorkItemDetails();
            LoadActivities();
            LoadAttachments();
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
            AddActivity(ActivityTypes.FieldUpdate, 
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
                        var sourcePath = FileStorageHelper.GetFullPath(attachment.FilePath);
                        File.Copy(sourcePath, saveFileDialog.FileName, overwrite: true);
                        
                        XtraMessageBox.Show("Dosya başarıyla indirildi.", "Başarılı", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                var fullPath = FileStorageHelper.GetFullPath(attachment.FilePath);
                if (File.Exists(fullPath))
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
                    AddActivity(ActivityTypes.FieldUpdate, 
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
    }
}

