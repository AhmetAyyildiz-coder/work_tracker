using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using work_tracker.Data;
using work_tracker.Data.Entities;

namespace work_tracker.Forms
{
    public partial class MeetingEditForm : XtraForm
    {
        private WorkTrackerDbContext _context;
        private int? _meetingId;

        public MeetingEditForm(int? meetingId = null)
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
            _meetingId = meetingId;
        }

        private void MeetingEditForm_Load(object sender, EventArgs e)
        {
            if (richEditControl1 != null)
            {
                richEditControl1.ActiveViewType = RichEditViewType.PrintLayout;
                richEditControl1.ActiveView.ZoomFactor = 1.1f;
            }

            if (_meetingId.HasValue)
            {
                LoadMeeting(_meetingId.Value);
            }
            else
            {
                dtMeetingDate.EditValue = DateTime.Now;
            }
        }

        private void LoadMeeting(int meetingId)
        {
            var meeting = _context.Meetings.Find(meetingId);
            if (meeting != null)
            {
                txtSubject.Text = meeting.Subject;
                dtMeetingDate.EditValue = meeting.MeetingDate;
                txtParticipants.Text = meeting.Participants;
                richEditControl1.HtmlText = meeting.NotesHtml ?? "";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSubject.Text))
            {
                XtraMessageBox.Show("Toplantı konusu boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSubject.Focus();
                return;
            }

            try
            {
                if (_meetingId.HasValue)
                {
                    var meeting = _context.Meetings.Find(_meetingId.Value);
                    if (meeting != null)
                    {
                        meeting.Subject = txtSubject.Text.Trim();
                        meeting.MeetingDate = Convert.ToDateTime(dtMeetingDate.EditValue);
                        meeting.Participants = txtParticipants.Text;
                        meeting.NotesHtml = richEditControl1.HtmlText;
                    }
                }
                else
                {
                    var newMeeting = new Meeting
                    {
                        Subject = txtSubject.Text.Trim(),
                        MeetingDate = Convert.ToDateTime(dtMeetingDate.EditValue),
                        Participants = txtParticipants.Text,
                        NotesHtml = richEditControl1.HtmlText,
                        CreatedAt = DateTime.Now
                    };
                    _context.Meetings.Add(newMeeting);
                }

                _context.SaveChanges();
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

        #region RichEdit quick formatting

        private void btnNoteBold_Click(object sender, EventArgs e)
        {
            ToggleSelectionFormatting(p => p.Bold = !p.Bold);
        }

        private void btnNoteItalic_Click(object sender, EventArgs e)
        {
            ToggleSelectionFormatting(p => p.Italic = !p.Italic);
        }

        private void btnNoteBulletList_Click(object sender, EventArgs e)
        {
            XtraMessageBox.Show("Bu sürümde madde işaretli liste butonu yerleşik değil. RichEdit içinde Ctrl+Shift+L gibi kısayolları kullanabilirsiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnNoteNumberedList_Click(object sender, EventArgs e)
        {
            XtraMessageBox.Show("Bu sürümde numaralı liste butonu yerleşik değil. RichEdit içindeki yerleşik kısayolları kullanabilirsiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ToggleSelectionFormatting(Action<CharacterProperties> action)
        {
            if (richEditControl1 == null)
                return;

            var doc = richEditControl1.Document;
            var range = doc.Selection;
            if (range.Length == 0)
                return;

            var props = doc.BeginUpdateCharacters(range);
            try
            {
                action(props);
            }
            finally
            {
                doc.EndUpdateCharacters(props);
            }
        }

        private void btnH1_Click(object sender, EventArgs e)
        {
            ApplyHeadingStyle(24, true); // H1: 24pt, Bold
        }

        private void btnH2_Click(object sender, EventArgs e)
        {
            ApplyHeadingStyle(18, true); // H2: 18pt, Bold
        }

        private void btnH3_Click(object sender, EventArgs e)
        {
            ApplyHeadingStyle(14, true); // H3: 14pt, Bold
        }

        /// <summary>
        /// Seçili paragrafa veya imlecin bulunduğu paragrafa başlık stili uygular
        /// </summary>
        private void ApplyHeadingStyle(float fontSize, bool bold)
        {
            if (richEditControl1 == null)
                return;

            var doc = richEditControl1.Document;
            var selection = doc.Selection;

            // Seçili paragrafı veya imlecin bulunduğu paragrafı bul
            DocumentRange range;
            if (selection.Length == 0)
            {
                // İmleç konumundaki paragrafı al
                var pos = doc.CaretPosition;
                var paragraphs = doc.Paragraphs.Get(doc.CreateRange(pos, 0));
                if (paragraphs.Count > 0)
                {
                    range = paragraphs[0].Range;
                }
                else
                {
                    return;
                }
            }
            else
            {
                range = selection;
            }

            // Karakter formatını uygula
            var cp = doc.BeginUpdateCharacters(range);
            try
            {
                cp.FontSize = fontSize;
                cp.Bold = bold;
                cp.ForeColor = System.Drawing.Color.DarkBlue;
            }
            finally
            {
                doc.EndUpdateCharacters(cp);
            }
        }

        #endregion
    }
}

