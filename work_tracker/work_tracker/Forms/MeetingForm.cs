using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Commands;
using work_tracker.Data;
using work_tracker.Data.Entities;

namespace work_tracker.Forms
{
    public partial class MeetingForm : XtraForm
    {
        private WorkTrackerDbContext _context;
        private int? _selectedMeetingId;
        
        // Açık detay formlarını takip et
        private Dictionary<int, MeetingDetailForm> _openDetailForms = new Dictionary<int, MeetingDetailForm>();

        public MeetingForm()
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
        }

        private void MeetingForm_Load(object sender, EventArgs e)
        {
            LoadMeetings();

            // Arama panelini daima görünür yap
            var view = gridControl1.MainView as GridView;
            if (view != null)
            {
                view.OptionsFind.AlwaysVisible = true;
            }

            // Not editörü için daha okunaklı bir varsayılan görünüm
            if (richEditControl1 != null)
            {
                richEditControl1.ActiveViewType = RichEditViewType.PrintLayout;
                richEditControl1.ActiveView.ZoomFactor = 1.1f;
                richEditControl1.Options.HorizontalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            }
        }

        #region Not düzenleme kısayol butonları

        private void btnNoteBold_Click(object sender, EventArgs e)
        {
            if (richEditControl1 == null || richEditControl1.ReadOnly)
                return;

            var doc = richEditControl1.Document;
            var range = doc.Selection;
            if (range.Length == 0)
                return;

            var cp = doc.BeginUpdateCharacters(range);
            try
            {
                cp.Bold = !cp.Bold;
            }
            finally
            {
                doc.EndUpdateCharacters(cp);
            }
        }

        private void btnNoteItalic_Click(object sender, EventArgs e)
        {
            if (richEditControl1 == null || richEditControl1.ReadOnly)
                return;

            var doc = richEditControl1.Document;
            var range = doc.Selection;
            if (range.Length == 0)
                return;

            var cp = doc.BeginUpdateCharacters(range);
            try
            {
                cp.Italic = !cp.Italic;
            }
            finally
            {
                doc.EndUpdateCharacters(cp);
            }
        }

        private void btnNoteBulletList_Click(object sender, EventArgs e)
        {
            XtraMessageBox.Show(
                "Bu sürümde hızlı butonla listeleme desteği eklemedim.\n" +
                "Yine de RichEdit’in yerleşik kısayollarını (Ctrl+Shift+L vb.) kullanarak madde işaretli liste oluşturabilirsin.",
                "Bilgi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnNoteNumberedList_Click(object sender, EventArgs e)
        {
            XtraMessageBox.Show(
                "Bu sürümde hızlı butonla numaralı liste desteği eklemedim.\n" +
                "Yine de RichEdit’in yerleşik kısayollarını kullanarak numaralı liste oluşturabilirsin.",
                "Bilgi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        #endregion

        private void LoadMeetings()
        {
            var meetings = _context.Meetings
                .OrderByDescending(m => m.MeetingDate)
                .Select(m => new
                {
                    m.Id,
                    m.Subject,
                    m.MeetingDate,
                    m.Participants,
                    m.CreatedAt
                })
                .ToList();

            gridControl1.DataSource = meetings;
            
            // GridView ayarları
            var view = gridControl1.MainView as GridView;
            if (view != null)
            {
                view.BestFitColumns();
                view.OptionsBehavior.Editable = false;
                view.OptionsView.ShowAutoFilterRow = true;
                view.DoubleClick -= gridView1_DoubleClick;
                view.DoubleClick += gridView1_DoubleClick;
                
                // Kolon başlıklarını Türkçeleştir
                if (view.Columns["Id"] != null) 
                {
                    view.Columns["Id"].Caption = "ID";
                    view.Columns["Id"].Width = 50;
                }
                if (view.Columns["Subject"] != null) 
                {
                    view.Columns["Subject"].Caption = "Toplantı Konusu";
                    view.Columns["Subject"].Width = 300;
                }
                if (view.Columns["MeetingDate"] != null) 
                {
                    view.Columns["MeetingDate"].Caption = "Toplantı Tarihi";
                    view.Columns["MeetingDate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    view.Columns["MeetingDate"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
                }
                if (view.Columns["Participants"] != null) 
                {
                    view.Columns["Participants"].Caption = "Katılımcılar";
                    view.Columns["Participants"].Width = 200;
                }
                if (view.Columns["CreatedAt"] != null) 
                {
                    view.Columns["CreatedAt"].Caption = "Kayıt Tarihi";
                    view.Columns["CreatedAt"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    view.Columns["CreatedAt"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
                }

                // İlk satırı seç ve detayları yükle
                if (view.RowCount > 0)
                {
                    view.FocusedRowHandle = 0;
                    var firstId = (int)view.GetRowCellValue(0, "Id");
                    _selectedMeetingId = firstId;
                    LoadMeetingDetails(firstId);
                }
                else
                {
                    ClearMeetingDetails();
                }
            }
        }

        private void btnNewMeeting_Click(object sender, EventArgs e)
        {
            var form = new MeetingEditForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadMeetings();
            }
        }

        private void btnEditMeeting_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view != null && view.FocusedRowHandle >= 0)
            {
                var meetingId = (int)view.GetRowCellValue(view.FocusedRowHandle, "Id");
                var form = new MeetingEditForm(meetingId);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadMeetings();
                }
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            btnViewDetails_Click(sender, e);
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view != null && view.FocusedRowHandle >= 0)
            {
                var meetingId = (int)view.GetRowCellValue(view.FocusedRowHandle, "Id");
                
                // Eğer bu toplantı için zaten bir detay formu açıksa, onu öne getir
                if (_openDetailForms.ContainsKey(meetingId) && 
                    _openDetailForms[meetingId] != null && 
                    !_openDetailForms[meetingId].IsDisposed)
                {
                    var existingForm = _openDetailForms[meetingId];
                    existingForm.Activate();
                    
                    if (existingForm.WindowState == FormWindowState.Minimized)
                    {
                        existingForm.WindowState = FormWindowState.Maximized;
                    }
                }
                else
                {
                    // Yeni detay formu aç
                    var detailForm = new MeetingDetailForm(meetingId);
                    detailForm.MdiParent = this.MdiParent;
                    detailForm.WindowState = FormWindowState.Maximized;
                    
                    // Form kapatıldığında dictionary'den temizle
                    detailForm.FormClosed += (s, args) => 
                    {
                        if (_openDetailForms.ContainsKey(meetingId))
                        {
                            _openDetailForms.Remove(meetingId);
                        }
                    };
                    
                    _openDetailForms[meetingId] = detailForm;
                    detailForm.Show();
                }
            }
        }

        private void LoadMeetingDetails(int meetingId)
        {
            var meeting = _context.Meetings.Find(meetingId);
            if (meeting != null)
            {
                txtSubject.Text = meeting.Subject;
                dtMeetingDate.EditValue = meeting.MeetingDate;
                txtParticipants.Text = meeting.Participants;
                richEditControl1.HtmlText = meeting.NotesHtml ?? "";

                // İlgili iş taleplerini yükle
                LoadRelatedWorkItems(meetingId);

                SetEditMode(false);
            }
        }

        private bool _isEditing = false;
        private void SetEditMode(bool editing)
        {
            _isEditing = editing;
            txtSubject.Enabled = editing;
            dtMeetingDate.Enabled = editing;
            txtParticipants.Enabled = editing;
            richEditControl1.ReadOnly = !editing;

            if (btnInlineEdit != null) btnInlineEdit.Enabled = !editing;
            if (btnInlineSave != null) btnInlineSave.Enabled = editing;
            if (btnInlineCancel != null) btnInlineCancel.Enabled = editing;
        }

        private void LoadRelatedWorkItems(int meetingId)
        {
            var workItems = _context.WorkItems
                .Where(w => w.SourceMeetingId == meetingId)
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

            gridControl2.DataSource = workItems;
            
            // GridView ayarları
            var view = gridControl2.MainView as GridView;
            if (view != null)
            {
                view.BestFitColumns();
                
                // Kolon başlıklarını Türkçeleştir
                if (view.Columns["Id"] != null) view.Columns["Id"].Caption = "ID";
                if (view.Columns["Title"] != null) 
                {
                    view.Columns["Title"].Caption = "Başlık";
                    view.Columns["Title"].Width = 250;
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
        }

        private void btnCreateWorkItem_Click(object sender, EventArgs e)
        {
            if (!_selectedMeetingId.HasValue)
            {
                XtraMessageBox.Show("Lütfen önce bir toplantı seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Seçili metni al (varsa)
            string selectedText = richEditControl1.Document.GetText(richEditControl1.Document.Selection);
            string title = "";
            string description = "";

            if (!string.IsNullOrWhiteSpace(selectedText))
            {
                // İlk satırı başlık, geri kalanını açıklama yap
                var lines = selectedText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                title = lines.Length > 0 ? lines[0] : "";
                description = lines.Length > 1 ? string.Join("\n", lines.Skip(1)) : "";
            }

            // WorkItemEditForm'u aç ve toplantı bilgisini aktar
            var form = new WorkItemEditForm(null, _selectedMeetingId.Value, title, description);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadRelatedWorkItems(_selectedMeetingId.Value);
                XtraMessageBox.Show("İş talebi oluşturuldu ve toplantı ile ilişkilendirildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnInlineEdit_Click(object sender, EventArgs e)
        {
            if (!_selectedMeetingId.HasValue)
            {
                XtraMessageBox.Show("Önce bir toplantı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SetEditMode(true);
        }

        private void btnInlineCancel_Click(object sender, EventArgs e)
        {
            if (_selectedMeetingId.HasValue)
            {
                LoadMeetingDetails(_selectedMeetingId.Value);
            }
            SetEditMode(false);
        }

        private void btnInlineSave_Click(object sender, EventArgs e)
        {
            if (!_selectedMeetingId.HasValue)
            {
                XtraMessageBox.Show("Önce bir toplantı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSubject.Text))
            {
                XtraMessageBox.Show("Toplantı konusu boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSubject.Focus();
                return;
            }

            var meeting = _context.Meetings.Find(_selectedMeetingId.Value);
            if (meeting == null)
            {
                XtraMessageBox.Show("Toplantı bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            meeting.Subject = txtSubject.Text.Trim();
            meeting.MeetingDate = Convert.ToDateTime(dtMeetingDate.EditValue);
            meeting.Participants = txtParticipants.Text?.Trim();
            meeting.NotesHtml = richEditControl1.HtmlText;

            _context.SaveChanges();
            XtraMessageBox.Show("Toplantı güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SetEditMode(false);
            LoadMeetings();
        }

        private void btnDeleteMeeting_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view != null && view.FocusedRowHandle >= 0)
            {
                var meetingId = (int)view.GetRowCellValue(view.FocusedRowHandle, "Id");
                var subject = view.GetRowCellValue(view.FocusedRowHandle, "Subject")?.ToString();

                // Bu toplantıya bağlı iş taleplerini kontrol et
                var relatedWorkItemsCount = _context.WorkItems.Count(w => w.SourceMeetingId == meetingId);
                string message = $"'{subject}' toplantısını silmek istediğinize emin misiniz?";
                
                if (relatedWorkItemsCount > 0)
                {
                    message += $"\n\nUyarı: Bu toplantıya bağlı {relatedWorkItemsCount} iş talebi bulunmaktadır. " +
                               "Toplantı silindiğinde bu iş taleplerinin toplantı bağlantısı kaldırılacaktır.";
                }

                var result = XtraMessageBox.Show(
                    message,
                    "Onay",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var meeting = _context.Meetings.Find(meetingId);
                        if (meeting != null)
                        {
                            // Önce ilgili WorkItem'ların SourceMeetingId'sini null yap
                            var relatedWorkItems = _context.WorkItems
                                .Where(w => w.SourceMeetingId == meetingId)
                                .ToList();
                            
                            foreach (var workItem in relatedWorkItems)
                            {
                                workItem.SourceMeetingId = null;
                            }

                            _context.Meetings.Remove(meeting);
                            _context.SaveChanges();
                            LoadMeetings();
                            ClearMeetingDetails();
                            
                            XtraMessageBox.Show(
                                "Toplantı başarıyla silindi.",
                                "Bilgi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(
                            $"Toplantı silinirken hata oluştu:\n\n{ex.Message}",
                            "Hata",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ClearMeetingDetails()
        {
            _selectedMeetingId = null;
            txtSubject.Text = "";
            dtMeetingDate.EditValue = null;
            txtParticipants.Text = "";
            richEditControl1.HtmlText = "";
            gridControl2.DataSource = null;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadMeetings();
        }

        private void btnFilterUpcoming_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view == null) return;
            var now = DateTime.Now;
            view.ActiveFilterString = $"[MeetingDate] >= #{now:MM/dd/yyyy HH:mm}#";
        }

        private void btnFilterToday_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view == null) return;
            var start = DateTime.Today;
            var end = start.AddDays(1).AddTicks(-1);
            view.ActiveFilterString = $"[MeetingDate] >= #{start:MM/dd/yyyy HH:mm}# AND [MeetingDate] <= #{end:MM/dd/yyyy HH:mm}#";
        }

        private void btnFilterPast_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view == null) return;
            var now = DateTime.Now;
            view.ActiveFilterString = $"[MeetingDate] < #{now:MM/dd/yyyy HH:mm}#";
        }

        private void btnFilterClear_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view == null) return;
            view.ActiveFilterString = string.Empty;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var view = sender as GridView;
            if (view == null) return;

            if (e.FocusedRowHandle >= 0)
            {
                var idObj = view.GetRowCellValue(e.FocusedRowHandle, "Id");
                if (idObj != null && int.TryParse(idObj.ToString(), out var id))
                {
                    _selectedMeetingId = id;
                    LoadMeetingDetails(id);
                }
                else
                {
                    ClearMeetingDetails();
                }
            }
            else
            {
                ClearMeetingDetails();
            }
        }
    }
}

