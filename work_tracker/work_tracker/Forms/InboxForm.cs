using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using work_tracker.Data;
using work_tracker.Data.Entities;

namespace work_tracker.Forms
{
    public partial class InboxForm : XtraForm
    {
        private WorkTrackerDbContext _context;

        public InboxForm()
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
        }

        private void InboxForm_Load(object sender, EventArgs e)
        {
            LoadInboxItems();
        }

        private void LoadInboxItems()
        {
            var inboxItems = _context.WorkItems
                .Include(w => w.Project)
                .Include(w => w.Module)
                .Include(w => w.SourceMeeting)
                .Where(w => w.Board == "Inbox" && w.Status == "Bekliyor")
                .OrderByDescending(w => w.RequestedAt)
                .Select(w => new
                {
                    w.Id,
                    w.Title,
                    w.Description,
                    w.RequestedBy,
                    w.RequestedAt,
                    ProjectName = w.Project != null ? w.Project.Name : "",
                    ModuleName = w.Module != null ? w.Module.Name : "",
                    MeetingSubject = w.SourceMeeting != null ? w.SourceMeeting.Subject : "",
                    w.Status
                })
                .ToList();

            gridControl1.DataSource = inboxItems;
            
            // GridView ayarları
            var view = gridControl1.MainView as GridView;
            if (view != null)
            {
                view.BestFitColumns();
                
                // Kolon başlıklarını Türkçeleştir ve sırala
                if (view.Columns["Id"] != null) 
                {
                    view.Columns["Id"].Caption = "ID";
                    view.Columns["Id"].Width = 50;
                }
                if (view.Columns["Title"] != null) 
                {
                    view.Columns["Title"].Caption = "Başlık";
                    view.Columns["Title"].Width = 250;
                }
                if (view.Columns["Description"] != null) 
                {
                    view.Columns["Description"].Caption = "Açıklama";
                    view.Columns["Description"].Width = 200;
                }
                if (view.Columns["RequestedBy"] != null) view.Columns["RequestedBy"].Caption = "Talep Eden";
                if (view.Columns["RequestedAt"] != null) 
                {
                    view.Columns["RequestedAt"].Caption = "Talep Tarihi";
                    view.Columns["RequestedAt"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    view.Columns["RequestedAt"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
                }
                if (view.Columns["ProjectName"] != null) view.Columns["ProjectName"].Caption = "Proje";
                if (view.Columns["ModuleName"] != null) view.Columns["ModuleName"].Caption = "Modül";
                if (view.Columns["MeetingSubject"] != null) 
                {
                    view.Columns["MeetingSubject"].Caption = "Toplantı";
                    view.Columns["MeetingSubject"].Width = 150;
                }
                if (view.Columns["Status"] != null) view.Columns["Status"].Caption = "Durum";
            }
        }

        private void btnNewWorkItem_Click(object sender, EventArgs e)
        {
            var form = new WorkItemEditForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadInboxItems();
            }
        }

        private void btnEditWorkItem_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view != null && view.FocusedRowHandle >= 0)
            {
                var workItemId = (int)view.GetRowCellValue(view.FocusedRowHandle, "Id");
                var form = new WorkItemEditForm(workItemId);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadInboxItems();
                }
            }
        }

        private void btnDeleteWorkItem_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view != null && view.FocusedRowHandle >= 0)
            {
                var workItemId = (int)view.GetRowCellValue(view.FocusedRowHandle, "Id");
                var title = view.GetRowCellValue(view.FocusedRowHandle, "Title")?.ToString();

                var result = XtraMessageBox.Show(
                    $"'{title}' iş talebini silmek istediğinize emin misiniz?",
                    "Onay",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    var workItem = _context.WorkItems.Find(workItemId);
                    if (workItem != null)
                    {
                        _context.WorkItems.Remove(workItem);
                        _context.SaveChanges();
                        LoadInboxItems();
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadInboxItems();
        }

        private void btnSendToTriage_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view != null && view.FocusedRowHandle >= 0)
            {
                var workItemId = (int)view.GetRowCellValue(view.FocusedRowHandle, "Id");
                
                // Sınıflandırma formunu aç ve seçili iş talebini aktar
                var triageForm = new TriageForm(workItemId);
                triageForm.MdiParent = this.MdiParent;
                triageForm.WindowState = FormWindowState.Maximized;
                triageForm.Show();
                
                this.Close();
            }
            else
            {
                XtraMessageBox.Show("Lütfen sınıflandırmak için bir iş talebi seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSendToOtopark_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view != null && view.FocusedRowHandle >= 0)
            {
                var workItemId = (int)view.GetRowCellValue(view.FocusedRowHandle, "Id");
                var title = view.GetRowCellValue(view.FocusedRowHandle, "Title")?.ToString();

                var result = XtraMessageBox.Show(
                    $"'{title}' iş talebini Otopark'a göndermek istediğinize emin misiniz?\n\nOtopark: Düşük öncelikli, 'bir gün yapılabilir' işler için.",
                    "Otopark'a Gönder",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    var workItem = _context.WorkItems.Find(workItemId);
                    if (workItem != null)
                    {
                        workItem.Board = "Otopark";
                        workItem.Status = "Bekliyor";

                        // Aktivite kaydı
                        _context.WorkItemActivities.Add(new WorkItemActivity
                        {
                            WorkItemId = workItem.Id,
                            ActivityType = "BoardChange",
                            OldValue = "Inbox",
                            NewValue = "Otopark",
                            Description = "İş Inbox'tan Otopark'a taşındı",
                            CreatedBy = Environment.UserName,
                            CreatedAt = DateTime.Now
                        });

                        _context.SaveChanges();
                        LoadInboxItems();
                        
                        XtraMessageBox.Show("İş başarıyla Otopark'a taşındı.", "Başarılı", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Lütfen Otopark'a göndermek için bir iş talebi seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

