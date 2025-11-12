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
    public partial class SprintManagementForm : XtraForm
    {
        private WorkTrackerDbContext _context;

        public SprintManagementForm()
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
        }

        private void SprintManagementForm_Load(object sender, EventArgs e)
        {
            LoadSprints();
        }

        private void LoadSprints()
        {
            var sprints = _context.Sprints.OrderByDescending(s => s.CreatedAt).ToList();
            gridControl1.DataSource = sprints;
            
            // GridView ayarları
            var view = gridControl1.MainView as GridView;
            if (view != null)
            {
                view.BestFitColumns();
                
                // Kolon başlıklarını Türkçeleştir
                if (view.Columns["Id"] != null) view.Columns["Id"].Caption = "ID";
                if (view.Columns["Name"] != null) view.Columns["Name"].Caption = "Sprint Adı";
                if (view.Columns["Goals"] != null) view.Columns["Goals"].Caption = "Hedefler";
                if (view.Columns["StartDate"] != null)
                {
                    view.Columns["StartDate"].Caption = "Başlangıç Tarihi";
                    view.Columns["StartDate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    view.Columns["StartDate"].DisplayFormat.FormatString = "dd.MM.yyyy";
                }
                if (view.Columns["EndDate"] != null)
                {
                    view.Columns["EndDate"].Caption = "Bitiş Tarihi";
                    view.Columns["EndDate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    view.Columns["EndDate"].DisplayFormat.FormatString = "dd.MM.yyyy";
                }
                if (view.Columns["Status"] != null) view.Columns["Status"].Caption = "Durum";
                if (view.Columns["CreatedAt"] != null)
                {
                    view.Columns["CreatedAt"].Caption = "Oluşturulma Tarihi";
                    view.Columns["CreatedAt"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    view.Columns["CreatedAt"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
                }
                if (view.Columns["CompletedAt"] != null)
                {
                    view.Columns["CompletedAt"].Caption = "Tamamlanma Tarihi";
                    view.Columns["CompletedAt"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    view.Columns["CompletedAt"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
                }
                
                // Navigation property'leri ve hesaplanan alanları gizle
                if (view.Columns["WorkItems"] != null) view.Columns["WorkItems"].Visible = false;
                if (view.Columns["DurationDays"] != null) view.Columns["DurationDays"].Visible = false;
                if (view.Columns["RemainingDays"] != null) view.Columns["RemainingDays"].Visible = false;
                if (view.Columns["IsActive"] != null) view.Columns["IsActive"].Visible = false;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            var form = new SprintEditForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadSprints();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view != null && view.FocusedRowHandle >= 0)
            {
                var sprint = view.GetRow(view.FocusedRowHandle) as Sprint;
                if (sprint != null)
                {
                    var form = new SprintEditForm(sprint.Id);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadSprints();
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view != null && view.FocusedRowHandle >= 0)
            {
                var sprint = view.GetRow(view.FocusedRowHandle) as Sprint;
                if (sprint != null)
                {
                    // Sprint içindeki iş sayısını kontrol et
                    var workItemCount = _context.WorkItems.Count(w => w.SprintId == sprint.Id);
                    
                    string message = workItemCount > 0
                        ? $"'{sprint.Name}' sprint'inde {workItemCount} adet iş bulunmaktadır. Yine de silmek istediğinize emin misiniz?"
                        : $"'{sprint.Name}' sprint'ini silmek istediğinize emin misiniz?";

                    var result = XtraMessageBox.Show(
                        message,
                        "Onay",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        var dbSprint = _context.Sprints.Find(sprint.Id);
                        if (dbSprint != null)
                        {
                            // İlişkili iş öğelerinin SprintId'sini null yap
                            var workItems = _context.WorkItems.Where(w => w.SprintId == sprint.Id).ToList();
                            foreach (var item in workItems)
                            {
                                item.SprintId = null;
                            }

                            _context.Sprints.Remove(dbSprint);
                            _context.SaveChanges();
                            LoadSprints();
                        }
                    }
                }
            }
        }

        private void btnStartSprint_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view != null && view.FocusedRowHandle >= 0)
            {
                var sprint = view.GetRow(view.FocusedRowHandle) as Sprint;
                if (sprint != null && sprint.Status == "Planned")
                {
                    var result = XtraMessageBox.Show(
                        $"'{sprint.Name}' sprint'ini başlatmak istediğinize emin misiniz?",
                        "Sprint Başlat",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        var dbSprint = _context.Sprints.Find(sprint.Id);
                        if (dbSprint != null)
                        {
                            dbSprint.Status = "Active";
                            _context.SaveChanges();
                            LoadSprints();
                            XtraMessageBox.Show("Sprint başarıyla başlatıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else if (sprint != null)
                {
                    XtraMessageBox.Show("Sadece 'Planned' durumundaki sprint'ler başlatılabilir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnCompleteSprint_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view != null && view.FocusedRowHandle >= 0)
            {
                var sprint = view.GetRow(view.FocusedRowHandle) as Sprint;
                if (sprint != null && sprint.Status == "Active")
                {
                    var result = XtraMessageBox.Show(
                        $"'{sprint.Name}' sprint'ini tamamlamak istediğinize emin misiniz?",
                        "Sprint Tamamla",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        var dbSprint = _context.Sprints.Find(sprint.Id);
                        if (dbSprint != null)
                        {
                            dbSprint.Status = "Completed";
                            dbSprint.CompletedAt = DateTime.Now;
                            _context.SaveChanges();
                            LoadSprints();
                            XtraMessageBox.Show("Sprint başarıyla tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else if (sprint != null)
                {
                    XtraMessageBox.Show("Sadece 'Active' durumundaki sprint'ler tamamlanabilir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSprints();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

