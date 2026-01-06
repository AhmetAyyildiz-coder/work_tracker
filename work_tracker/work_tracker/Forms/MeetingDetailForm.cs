using System;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit;
using work_tracker.Data;
using work_tracker.Data.Entities;

namespace work_tracker.Forms
{
    public partial class MeetingDetailForm : XtraForm
    {
        private WorkTrackerDbContext _context;
        private int _meetingId;
			private bool _isEditing;

        public MeetingDetailForm(int meetingId)
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
            _meetingId = meetingId;
        }

        private void MeetingDetailForm_Load(object sender, EventArgs e)
        {
            LoadMeetingDetails();
            LoadDocuments();
				SetEditMode(false);
        }

        private void LoadMeetingDetails()
        {
            var meeting = _context.Meetings.Find(_meetingId);
            if (meeting != null)
            {
                this.Text = $"Toplantı Detayı: {meeting.Subject}";
                txtSubject.Text = meeting.Subject;
                dtMeetingDate.EditValue = meeting.MeetingDate;
                txtParticipants.Text = meeting.Participants;
                richEditControl1.HtmlText = meeting.NotesHtml ?? "";

                LoadRelatedWorkItems();
            }
        }

        private void LoadRelatedWorkItems()
        {
            var workItems = _context.WorkItems
                .Where(w => w.SourceMeetingId == _meetingId)
                .Select(w => new
                {
                    w.Id,
                    w.Title,
                    w.Status,
                    w.Board,
                    w.Type,
                    w.CreatedAt
                })
                .ToList();

            gridControl1.DataSource = workItems;
            
            // GridView ayarları
            var view = gridControl1.MainView as GridView;
            if (view != null)
            {
                view.BestFitColumns();
                
                if (view.Columns["Id"] != null) view.Columns["Id"].Caption = "ID";
                if (view.Columns["Title"] != null) 
                {
                    view.Columns["Title"].Caption = "Başlık";
                    view.Columns["Title"].Width = 400;
                }
                if (view.Columns["Status"] != null) view.Columns["Status"].Caption = "Durum";
                if (view.Columns["Board"] != null) view.Columns["Board"].Caption = "Pano";
                if (view.Columns["Type"] != null) view.Columns["Type"].Caption = "Tip";
                if (view.Columns["CreatedAt"] != null) 
                {
                    view.Columns["CreatedAt"].Caption = "Oluşturulma Tarihi";
                    view.Columns["CreatedAt"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    view.Columns["CreatedAt"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
                }
            }

            lblWorkItemCount.Text = $"Bu toplantıdan {workItems.Count} iş talebi oluşturuldu";
        }

        private void LoadDocuments()
        {
            var documents = _context.DocumentReferences
                .Where(d => d.MeetingId == _meetingId)
                .OrderByDescending(d => d.CreatedAt)
                .Select(d => new
                {
                    d.Id,
                    d.Title,
                    d.FileType,
                    d.FilePath,
                    d.Description,
                    d.CreatedAt,
                    d.LastAccessedAt
                })
                .ToList();

            gridControlDocuments.DataSource = documents;

            var viewDocs = gridControlDocuments.MainView as GridView;
            if (viewDocs != null)
            {
                viewDocs.BestFitColumns();

                if (viewDocs.Columns["Id"] != null) viewDocs.Columns["Id"].Visible = false;
                if (viewDocs.Columns["Title"] != null)
                {
                    viewDocs.Columns["Title"].Caption = "Döküman";
                    viewDocs.Columns["Title"].Width = 300;
                }
                if (viewDocs.Columns["FileType"] != null)
                {
                    viewDocs.Columns["FileType"].Caption = "Tür";
                    viewDocs.Columns["FileType"].Width = 80;
                }
                if (viewDocs.Columns["FilePath"] != null) viewDocs.Columns["FilePath"].Visible = false;
                if (viewDocs.Columns["Description"] != null)
                {
                    viewDocs.Columns["Description"].Caption = "Açıklama";
                    viewDocs.Columns["Description"].Width = 250;
                }
                if (viewDocs.Columns["CreatedAt"] != null)
                {
                    viewDocs.Columns["CreatedAt"].Caption = "Oluşturma";
                    viewDocs.Columns["CreatedAt"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    viewDocs.Columns["CreatedAt"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
                }
                if (viewDocs.Columns["LastAccessedAt"] != null)
                {
                    viewDocs.Columns["LastAccessedAt"].Caption = "Son Erişim";
                    viewDocs.Columns["LastAccessedAt"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    viewDocs.Columns["LastAccessedAt"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
                }
            }

            lblDocumentCount.Text = $"{documents.Count} döküman";
        }

        private void btnCreateWorkItem_Click(object sender, EventArgs e)
        {
            // Seçili metni al
            string selectedText = richEditControl1.Document.GetText(richEditControl1.Document.Selection);
            string title = "";
            string description = "";

            if (!string.IsNullOrWhiteSpace(selectedText))
            {
                var lines = selectedText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                title = lines.Length > 0 ? lines[0] : "";
                description = lines.Length > 1 ? string.Join("\n", lines.Skip(1)) : "";
            }

            var form = new WorkItemEditForm(null, _meetingId, title, description);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadRelatedWorkItems();
                XtraMessageBox.Show("İş talebi oluşturuldu ve toplantı ile ilişkilendirildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
	
			private void SetEditMode(bool editing)
			{
				_isEditing = editing;
				txtSubject.Enabled = editing;
				dtMeetingDate.Enabled = editing;
				txtParticipants.Enabled = editing;
				richEditControl1.ReadOnly = !editing;
				panelNotesToolbar.Visible = editing;
	
				btnEdit.Enabled = !editing;
				btnSave.Enabled = editing;
				btnCancel.Enabled = editing;
			}
	
			private void btnEdit_Click(object sender, EventArgs e)
			{
				SetEditMode(true);
			}
	
			private void btnCancel_Click(object sender, EventArgs e)
			{
				// Değişiklikleri geri al, veriyi yeniden yükle
				LoadMeetingDetails();
				SetEditMode(false);
			}
	
			private void btnSave_Click(object sender, EventArgs e)
			{
				if (string.IsNullOrWhiteSpace(txtSubject.Text))
				{
					XtraMessageBox.Show("Toplantı konusu boş olamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
	
				var meeting = _context.Meetings.Find(_meetingId);
				if (meeting == null)
				{
					XtraMessageBox.Show("Toplantı bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
	
				meeting.Subject = txtSubject.Text.Trim();
				meeting.MeetingDate = dtMeetingDate.DateTime;
				meeting.Participants = txtParticipants.Text?.Trim();
				meeting.NotesHtml = richEditControl1.HtmlText;
	
				_context.SaveChanges();
	
				XtraMessageBox.Show("Toplantı bilgileri kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
	
				SetEditMode(false);
			}

        #region Zengin Metin Toolbar Event Handlers
        
        private void btnNoteBold_Click(object sender, EventArgs e)
        {
            richEditControl1.Document.BeginUpdate();
            var cp = richEditControl1.Document.BeginUpdateCharacters(richEditControl1.Document.Selection);
            cp.Bold = !cp.Bold.HasValue || !cp.Bold.Value;
            richEditControl1.Document.EndUpdateCharacters(cp);
            richEditControl1.Document.EndUpdate();
        }

        private void btnNoteItalic_Click(object sender, EventArgs e)
        {
            richEditControl1.Document.BeginUpdate();
            var cp = richEditControl1.Document.BeginUpdateCharacters(richEditControl1.Document.Selection);
            cp.Italic = !cp.Italic.HasValue || !cp.Italic.Value;
            richEditControl1.Document.EndUpdateCharacters(cp);
            richEditControl1.Document.EndUpdate();
        }

        private void btnNoteBulletList_Click(object sender, EventArgs e)
        {
            // Toggle bullet list using RichEditControl command
            richEditControl1.CreateCommand(DevExpress.XtraRichEdit.Commands.RichEditCommandId.ToggleBulletedListItem).Execute();
        }

        private void btnNoteNumberedList_Click(object sender, EventArgs e)
        {
            // Toggle numbered list using RichEditControl command
            richEditControl1.CreateCommand(DevExpress.XtraRichEdit.Commands.RichEditCommandId.ToggleNumberingListItem).Execute();
        }

        private void btnH1_Click(object sender, EventArgs e)
        {
            ApplyHeadingStyle(24, true);
        }

        private void btnH2_Click(object sender, EventArgs e)
        {
            ApplyHeadingStyle(18, true);
        }

        private void btnH3_Click(object sender, EventArgs e)
        {
            ApplyHeadingStyle(14, true);
        }

        private void ApplyHeadingStyle(float fontSize, bool bold)
        {
            richEditControl1.Document.BeginUpdate();
            var cp = richEditControl1.Document.BeginUpdateCharacters(richEditControl1.Document.Selection);
            cp.FontSize = fontSize;
            cp.Bold = bold;
            richEditControl1.Document.EndUpdateCharacters(cp);
            richEditControl1.Document.EndUpdate();
        }

        #endregion

        #region Döküman Yönetimi

        private void btnCreateMeetingDoc_Click(object sender, EventArgs e)
        {
            try
            {
                var meeting = _context.Meetings.Find(_meetingId);
                if (meeting == null)
                {
                    XtraMessageBox.Show("Toplantı bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Dosya yolu belirleme
                using (var sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Word Belgesi (*.docx)|*.docx";
                    sfd.DefaultExt = "docx";
                    sfd.FileName = $"Toplanti_{meeting.Subject.Replace(' ', '_')}_{DateTime.Now:yyyyMMdd_HHmmss}.docx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        // Word dökümanı oluştur
                        CreateMeetingWordDocument(meeting, sfd.FileName);

                        // DocumentReference oluştur
                        var docRef = new DocumentReference
                        {
                            Title = $"Toplantı Notu - {meeting.Subject}",
                            FilePath = sfd.FileName,
                            FileType = "Word",
                            Description = $"{meeting.MeetingDate:dd.MM.yyyy} tarihli toplantı notları",
                            MeetingId = _meetingId,
                            CreatedAt = DateTime.Now,
                            CreatedBy = Environment.UserName
                        };

                        _context.DocumentReferences.Add(docRef);
                        _context.SaveChanges();

                        LoadDocuments();

                        if (XtraMessageBox.Show("Word dökümanı oluşturuldu ve kaydedildi. Açmak ister misiniz?",
                            "Başarılı", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            Process.Start(sfd.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Döküman oluşturulurken hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateMeetingWordDocument(Meeting meeting, string filePath)
        {
            // RichEditControl kullanarak Word dosyası oluştur
            using (var richEdit = new RichEditControl())
            {
                var doc = richEdit.Document;
                doc.BeginUpdate();

                // Başlık
                var titleRange = doc.AppendText($"TOPLANTI NOTU\n");
                var titleFormat = doc.BeginUpdateCharacters(titleRange);
                titleFormat.FontSize = 20;
                titleFormat.Bold = true;
                titleFormat.ForeColor = System.Drawing.Color.DarkBlue;
                doc.EndUpdateCharacters(titleFormat);

                doc.AppendText("\n");

                // Toplantı Bilgileri
                doc.AppendText($"Konu: {meeting.Subject}\n");
                doc.AppendText($"Tarih: {meeting.MeetingDate:dd.MM.yyyy HH:mm}\n");
                if (!string.IsNullOrWhiteSpace(meeting.Participants))
                {
                    doc.AppendText($"Katılımcılar: {meeting.Participants}\n");
                }
                doc.AppendText($"Oluşturulma: {meeting.CreatedAt:dd.MM.yyyy HH:mm}\n");
                doc.AppendText("\n");

                // Notlar başlığı
                var notesHeaderRange = doc.AppendText("NOTLAR\n");
                var notesHeaderFormat = doc.BeginUpdateCharacters(notesHeaderRange);
                notesHeaderFormat.FontSize = 14;
                notesHeaderFormat.Bold = true;
                doc.EndUpdateCharacters(notesHeaderFormat);

                doc.AppendText("\n");

                // HTML notları ekle
                if (!string.IsNullOrWhiteSpace(meeting.NotesHtml))
                {
                    try
                    {
                        doc.AppendHtmlText(meeting.NotesHtml);
                    }
                    catch
                    {
                        // HTML parse hatası varsa düz metin olarak ekle
                        doc.AppendText(System.Text.RegularExpressions.Regex.Replace(meeting.NotesHtml, "<.*?>", string.Empty));
                    }
                }

                // İş talepleri
                var workItems = _context.WorkItems
                    .Where(w => w.SourceMeetingId == _meetingId)
                    .OrderBy(w => w.CreatedAt)
                    .ToList();

                if (workItems.Any())
                {
                    doc.AppendText("\n\n");
                    var workItemsHeaderRange = doc.AppendText("İLİŞKİLİ İŞ TALEPLERİ\n");
                    var workItemsHeaderFormat = doc.BeginUpdateCharacters(workItemsHeaderRange);
                    workItemsHeaderFormat.FontSize = 14;
                    workItemsHeaderFormat.Bold = true;
                    doc.EndUpdateCharacters(workItemsHeaderFormat);

                    doc.AppendText("\n");

                    foreach (var workItem in workItems)
                    {
                        doc.AppendText($"• #{workItem.Id} - {workItem.Title} ({workItem.Status})\n");
                    }
                }

                doc.EndUpdate();

                // Dosyayı kaydet
                richEdit.SaveDocument(filePath, DevExpress.XtraRichEdit.DocumentFormat.OpenXml);
            }
        }

        private void btnAddDocument_Click(object sender, EventArgs e)
        {
            var form = new DocumentReferenceEditForm()
            {
                PresetMeetingId = _meetingId
            };

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDocuments();
                XtraMessageBox.Show("Döküman eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnOpenDocument_Click(object sender, EventArgs e)
        {
            var viewDocs = gridControlDocuments.MainView as GridView;
            if (viewDocs == null || viewDocs.FocusedRowHandle < 0) return;

            var docId = (int)viewDocs.GetFocusedRowCellValue("Id");
            var doc = _context.DocumentReferences.Find(docId);

            if (doc != null)
            {
                if (File.Exists(doc.FilePath))
                {
                    doc.LastAccessedAt = DateTime.Now;
                    _context.SaveChanges();

                    try
                    {
                        Process.Start(doc.FilePath);
                        LoadDocuments();
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show($"Dosya açılırken hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    XtraMessageBox.Show("Dosya bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnDeleteDocument_Click(object sender, EventArgs e)
        {
            var viewDocs = gridControlDocuments.MainView as GridView;
            if (viewDocs == null || viewDocs.FocusedRowHandle < 0) return;

            if (XtraMessageBox.Show("Döküman referansını silmek istediğinizden emin misiniz?\n(Dosyanın kendisi silinmeyecek)",
                "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var docId = (int)viewDocs.GetFocusedRowCellValue("Id");
                var doc = _context.DocumentReferences.Find(docId);

                if (doc != null)
                {
                    _context.DocumentReferences.Remove(doc);
                    _context.SaveChanges();

                    LoadDocuments();
                    XtraMessageBox.Show("Döküman referansı silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void gridControlDocuments_DoubleClick(object sender, EventArgs e)
        {
            btnOpenDocument_Click(sender, e);
        }

        #endregion
    }
}

