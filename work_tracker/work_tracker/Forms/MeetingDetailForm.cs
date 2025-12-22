using System;
using System.Data.Entity;
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
    }
}

