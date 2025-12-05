using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using work_tracker.Data;
using work_tracker.Data.Entities;
using work_tracker.Helpers;

namespace work_tracker.Forms
{
    /// <summary>
    /// Email'den otomatik iş talebi oluşturma formu
    /// </summary>
    public partial class EmailToWorkItemForm : XtraForm
    {
        private WorkTrackerDbContext _context;
        private List<WorkItemEmail> _emails;
        private WorkItemEmail _selectedEmail;

        public EmailToWorkItemForm()
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
            _emails = new List<WorkItemEmail>();
        }

        private void EmailToWorkItemForm_Load(object sender, EventArgs e)
        {
            LoadProjects();
            SetupGrid();
            
            // Varsayılan değerler
            spinDaysBack.Value = 60;
            cmbUrgency.SelectedItem = "Normal";
            
            // Mailleri yükle
            LoadEmails();
        }

        private void LoadProjects()
        {
            try
            {
                var projects = _context.Projects
                    .Where(p => p.IsActive)
                    .OrderBy(p => p.Name)
                    .Select(p => new { p.Id, p.Name })
                    .ToList();

                cmbProject.Properties.DataSource = projects;
                cmbProject.Properties.DisplayMember = "Name";
                cmbProject.Properties.ValueMember = "Id";
                cmbProject.Properties.Columns.Clear();
                cmbProject.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Proje Adı"));
            }
            catch (Exception ex)
            {
                Logger.Error("Projeler yüklenirken hata", ex);
            }
        }

        private void SetupGrid()
        {
            gridViewEmails.OptionsView.ShowGroupPanel = false;
            gridViewEmails.OptionsBehavior.Editable = false;
            gridViewEmails.OptionsSelection.EnableAppearanceFocusedRow = true;
        }

        private void btnRefreshEmails_Click(object sender, EventArgs e)
        {
            LoadEmails();
        }

        private void LoadEmails()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                lblStatus.Text = "📧 Mailler yükleniyor...";

                var daysBack = (int)spinDaysBack.Value;
                var subjectFilter = txtEmailFilter.Text?.Trim();

                _emails = OutlookHelper.GetEmailsFromOutlook(100, subjectFilter, daysBack);

                // Grid'e bağla (özet bilgiler)
                var emailSummaries = _emails.Select(e => new
                {
                    e.OutlookEntryId,
                    e.Subject,
                    From = TruncateString(e.From, 30),
                    ReceivedDate = e.ReceivedDate,
                    HasAttachments = e.HasAttachments ? "📎" : "",
                    BodyPreview = TruncateString(EmailToWorkItemConverter.CleanEmailBody(e.Body, e.IsHtml), 50)
                }).ToList();

                gridControlEmails.DataSource = emailSummaries;
                ConfigureEmailGridColumns();

                lblStatus.Text = $"✅ {_emails.Count} mail yüklendi";

                if (_emails.Count == 0)
                {
                    lblStatus.Text = "⚠️ Belirtilen kriterlere uygun mail bulunamadı";
                }

                Logger.Info($"EmailToWorkItem: {_emails.Count} mail yüklendi (son {daysBack} gün)");
            }
            catch (Exception ex)
            {
                Logger.Error("Mailler yüklenirken hata", ex);
                XtraMessageBox.Show($"Mailler yüklenirken hata oluştu:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "❌ Hata oluştu";
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ConfigureEmailGridColumns()
        {
            if (gridViewEmails.Columns.Count == 0) return;

            if (gridViewEmails.Columns["OutlookEntryId"] != null)
                gridViewEmails.Columns["OutlookEntryId"].Visible = false;

            if (gridViewEmails.Columns["Subject"] != null)
            {
                gridViewEmails.Columns["Subject"].Caption = "Konu";
                gridViewEmails.Columns["Subject"].Width = 200;
            }
            if (gridViewEmails.Columns["From"] != null)
            {
                gridViewEmails.Columns["From"].Caption = "Gönderen";
                gridViewEmails.Columns["From"].Width = 120;
            }
            if (gridViewEmails.Columns["ReceivedDate"] != null)
            {
                gridViewEmails.Columns["ReceivedDate"].Caption = "Tarih";
                gridViewEmails.Columns["ReceivedDate"].Width = 110;
                gridViewEmails.Columns["ReceivedDate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridViewEmails.Columns["ReceivedDate"].DisplayFormat.FormatString = "dd.MM.yy HH:mm";
            }
            if (gridViewEmails.Columns["HasAttachments"] != null)
            {
                gridViewEmails.Columns["HasAttachments"].Caption = "";
                gridViewEmails.Columns["HasAttachments"].Width = 25;
            }
            if (gridViewEmails.Columns["BodyPreview"] != null)
            {
                gridViewEmails.Columns["BodyPreview"].Caption = "Önizleme";
                gridViewEmails.Columns["BodyPreview"].Width = 150;
            }

            gridViewEmails.BestFitColumns();
        }

        private void gridViewEmails_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
            {
                ClearPreview();
                return;
            }

            // Seçili email'i bul
            var entryId = gridViewEmails.GetRowCellValue(e.FocusedRowHandle, "OutlookEntryId")?.ToString();
            _selectedEmail = _emails.FirstOrDefault(em => em.OutlookEntryId == entryId);

            if (_selectedEmail != null)
            {
                PreviewConversion(_selectedEmail);
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (_selectedEmail == null)
            {
                XtraMessageBox.Show("Lütfen önce bir mail seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PreviewConversion(_selectedEmail);
        }

        private void PreviewConversion(WorkItemEmail email)
        {
            try
            {
                var workItem = EmailToWorkItemConverter.ConvertToWorkItem(email);

                // Form alanlarını doldur
                txtTitle.Text = workItem.Title;
                txtRequestedBy.Text = workItem.RequestedBy;
                cmbType.SelectedItem = workItem.Type;
                cmbUrgency.SelectedItem = workItem.Urgency ?? "Normal";
                memoDescription.Text = workItem.Description;

                lblStatus.Text = $"📋 Önizleme hazır: {email.Subject}";
            }
            catch (Exception ex)
            {
                Logger.Error("Önizleme oluşturulurken hata", ex);
                XtraMessageBox.Show($"Önizleme oluşturulurken hata:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearPreview()
        {
            txtTitle.Text = "";
            txtRequestedBy.Text = "";
            cmbType.SelectedIndex = -1;
            cmbUrgency.SelectedItem = "Normal";
            memoDescription.Text = "";
            _selectedEmail = null;
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (_selectedEmail == null)
            {
                XtraMessageBox.Show("Lütfen önce bir mail seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                XtraMessageBox.Show("Başlık alanı boş olamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                // WorkItem oluştur (form değerlerinden)
                var workItem = new WorkItem
                {
                    Title = txtTitle.Text.Trim(),
                    Description = memoDescription.Text,
                    RequestedBy = txtRequestedBy.Text?.Trim(),
                    RequestedAt = _selectedEmail.ReceivedDate ?? DateTime.Now,
                    CreatedAt = DateTime.Now,
                    Board = "Inbox",
                    Status = "Bekliyor",
                    Type = cmbType.SelectedItem?.ToString(),
                    Urgency = cmbUrgency.SelectedItem?.ToString() ?? "Normal"
                };

                // Proje seçildiyse ata
                if (cmbProject.EditValue != null && cmbProject.EditValue != DBNull.Value)
                {
                    workItem.ProjectId = (int)cmbProject.EditValue;
                }

                // Veritabanına kaydet
                _context.WorkItems.Add(workItem);
                _context.SaveChanges();

                // Email'i WorkItem'a bağla
                var emailRecord = new WorkItemEmail
                {
                    WorkItemId = workItem.Id,
                    OutlookEntryId = _selectedEmail.OutlookEntryId,
                    ConversationId = _selectedEmail.ConversationId,
                    LastKnownFolder = _selectedEmail.LastKnownFolder,
                    Subject = _selectedEmail.Subject,
                    From = _selectedEmail.From,
                    To = _selectedEmail.To,
                    Cc = _selectedEmail.Cc,
                    Body = _selectedEmail.Body,
                    IsHtml = _selectedEmail.IsHtml,
                    ReceivedDate = _selectedEmail.ReceivedDate,
                    SentDate = _selectedEmail.SentDate,
                    IsRead = _selectedEmail.IsRead,
                    HasAttachments = _selectedEmail.HasAttachments,
                    AttachmentCount = _selectedEmail.AttachmentCount,
                    LinkedBy = Environment.UserName,
                    LinkedAt = DateTime.Now,
                    Notes = "Mail'den otomatik oluşturuldu"
                };

                _context.WorkItemEmails.Add(emailRecord);
                
                // Activity kaydı ekle
                var activity = new WorkItemActivity
                {
                    WorkItemId = workItem.Id,
                    ActivityType = WorkItemActivityTypes.Created,
                    Description = $"İş talebi email'den oluşturuldu: {_selectedEmail.Subject}",
                    CreatedBy = Environment.UserName,
                    CreatedAt = DateTime.Now
                };
                _context.WorkItemActivities.Add(activity);

                _context.SaveChanges();

                Logger.Info($"Mail'den iş oluşturuldu - WorkItem ID: {workItem.Id}, Subject: {_selectedEmail.Subject}");

                XtraMessageBox.Show(
                    $"İş talebi başarıyla oluşturuldu!\n\nID: {workItem.Id}\nBaşlık: {workItem.Title}",
                    "Başarılı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                lblStatus.Text = $"✅ İş #{workItem.Id} oluşturuldu";

                // Formu temizle ve listeyi güncelle
                ClearPreview();
                
                // Oluşturulan maili listeden kaldır (opsiyonel)
                _emails.Remove(_selectedEmail);
                var emailSummaries = _emails.Select(em => new
                {
                    em.OutlookEntryId,
                    em.Subject,
                    From = TruncateString(em.From, 30),
                    ReceivedDate = em.ReceivedDate,
                    HasAttachments = em.HasAttachments ? "📎" : "",
                    BodyPreview = TruncateString(EmailToWorkItemConverter.CleanEmailBody(em.Body, em.IsHtml), 50)
                }).ToList();
                gridControlEmails.DataSource = emailSummaries;
            }
            catch (Exception ex)
            {
                Logger.Error("İş oluşturulurken hata", ex);
                XtraMessageBox.Show($"İş oluşturulurken hata:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "❌ Hata oluştu";
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string TruncateString(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text)) return "";
            if (text.Length <= maxLength) return text;
            return text.Substring(0, maxLength - 3) + "...";
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _context?.Dispose();
        }
    }
}
