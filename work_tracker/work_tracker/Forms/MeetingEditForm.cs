using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
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
    }
}

