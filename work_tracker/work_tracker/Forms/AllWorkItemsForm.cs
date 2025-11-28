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
    public partial class AllWorkItemsForm : XtraForm
    {
        private System.ComponentModel.IContainer components = null;
        private WorkTrackerDbContext _context;

        public AllWorkItemsForm()
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
        }

        private void AllWorkItemsForm_Load(object sender, EventArgs e)
        {
            LoadFilterOptions();
            LoadWorkItems();
        }

        private void LoadFilterOptions()
        {
            // Board filtreleri
            cmbBoardFilter.Properties.Items.Clear();
            cmbBoardFilter.Properties.Items.Add("Tümü");
            cmbBoardFilter.Properties.Items.AddRange(new[] { "Inbox", "Kanban", "Scrum" });
            cmbBoardFilter.SelectedIndex = 0;

            // Status filtreleri
            cmbStatusFilter.Properties.Items.Clear();
            cmbStatusFilter.Properties.Items.Add("Tümü");
            var statuses = _context.WorkItems
                .Where(w => !w.IsArchived && !string.IsNullOrEmpty(w.Status))
                .Select(w => w.Status)
                .Distinct()
                .OrderBy(s => s)
                .ToList();
            cmbStatusFilter.Properties.Items.AddRange(statuses);
            cmbStatusFilter.SelectedIndex = 0;

            // Type filtreleri
            cmbTypeFilter.Properties.Items.Clear();
            cmbTypeFilter.Properties.Items.Add("Tümü");
            var types = _context.WorkItems
                .Where(w => !w.IsArchived && !string.IsNullOrEmpty(w.Type))
                .Select(w => w.Type)
                .Distinct()
                .OrderBy(t => t)
                .ToList();
            cmbTypeFilter.Properties.Items.AddRange(types);
            cmbTypeFilter.SelectedIndex = 0;

            // Urgency filtreleri
            cmbUrgencyFilter.Properties.Items.Clear();
            cmbUrgencyFilter.Properties.Items.Add("Tümü");
            var urgencies = _context.WorkItems
                .Where(w => !w.IsArchived && !string.IsNullOrEmpty(w.Urgency))
                .Select(w => w.Urgency)
                .Distinct()
                .OrderBy(u => u)
                .ToList();
            cmbUrgencyFilter.Properties.Items.AddRange(urgencies);
            cmbUrgencyFilter.SelectedIndex = 0;
        }

        private void LoadWorkItems()
        {
            var query = _context.WorkItems
                .Include(w => w.Project)
                .Include(w => w.Module)
                .Include(w => w.SourceMeeting)
                .Include(w => w.Sprint)
                .Where(w => !w.IsArchived) // Arşivlenmişleri gösterme
                .AsQueryable();

            // Filtreleri uygula
            if (cmbBoardFilter.SelectedIndex > 0)
            {
                var board = cmbBoardFilter.SelectedItem.ToString();
                query = query.Where(w => w.Board == board);
            }

            if (cmbStatusFilter.SelectedIndex > 0)
            {
                var status = cmbStatusFilter.SelectedItem.ToString();
                query = query.Where(w => w.Status == status);
            }

            if (cmbTypeFilter.SelectedIndex > 0)
            {
                var type = cmbTypeFilter.SelectedItem.ToString();
                query = query.Where(w => w.Type == type);
            }

            if (cmbUrgencyFilter.SelectedIndex > 0)
            {
                var urgency = cmbUrgencyFilter.SelectedItem.ToString();
                query = query.Where(w => w.Urgency == urgency);
            }

            // Arama metni varsa uygula
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                var searchText = txtSearch.Text.ToLower();
                query = query.Where(w => 
                    w.Title.ToLower().Contains(searchText) ||
                    (w.Description != null && w.Description.ToLower().Contains(searchText)) ||
                    (w.RequestedBy != null && w.RequestedBy.ToLower().Contains(searchText)));
            }

            var workItems = query
                .OrderByDescending(w => w.RequestedAt)
                .Select(w => new
                {
                    w.Id,
                    w.Title,
                    w.Description,
                    w.RequestedBy,
                    w.RequestedAt,
                    w.Board,
                    w.Status,
                    w.Type,
                    w.Urgency,
                    w.EffortEstimate,
                    ProjectName = w.Project != null ? w.Project.Name : "",
                    ModuleName = w.Module != null ? w.Module.Name : "",
                    MeetingSubject = w.SourceMeeting != null ? w.SourceMeeting.Subject : "",
                    SprintName = w.Sprint != null ? w.Sprint.Name : "",
                    w.CompletedAt,
                    w.CreatedAt
                })
                .ToList();

            gridControl1.DataSource = workItems;
            
            // GridView ayarları
            var view = gridControl1.MainView as GridView;
            if (view != null)
            {
                view.BestFitColumns();
                view.OptionsBehavior.Editable = false;
                view.OptionsView.ShowAutoFilterRow = true;
                view.OptionsFind.AlwaysVisible = true;
                view.OptionsFind.FindMode = DevExpress.XtraEditors.FindMode.Always;
                
                // Kolon başlıklarını Türkçeleştir
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
                if (view.Columns["RequestedBy"] != null) 
                {
                    view.Columns["RequestedBy"].Caption = "Talep Eden";
                    view.Columns["RequestedBy"].Width = 120;
                }
                if (view.Columns["RequestedAt"] != null) 
                {
                    view.Columns["RequestedAt"].Caption = "Talep Tarihi";
                    view.Columns["RequestedAt"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    view.Columns["RequestedAt"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
                    view.Columns["RequestedAt"].Width = 130;
                }
                if (view.Columns["Board"] != null) 
                {
                    view.Columns["Board"].Caption = "Pano";
                    view.Columns["Board"].Width = 80;
                }
                if (view.Columns["Status"] != null) 
                {
                    view.Columns["Status"].Caption = "Durum";
                    view.Columns["Status"].Width = 120;
                }
                if (view.Columns["Type"] != null) 
                {
                    view.Columns["Type"].Caption = "Tip";
                    view.Columns["Type"].Width = 100;
                }
                if (view.Columns["Urgency"] != null) 
                {
                    view.Columns["Urgency"].Caption = "Aciliyet";
                    view.Columns["Urgency"].Width = 80;
                }
                if (view.Columns["EffortEstimate"] != null)
                {
                    view.Columns["EffortEstimate"].Caption = "Efor (gün)";
                    view.Columns["EffortEstimate"].Width = 80;
                }
                if (view.Columns["ProjectName"] != null) 
                {
                    view.Columns["ProjectName"].Caption = "Proje";
                    view.Columns["ProjectName"].Width = 120;
                }
                if (view.Columns["ModuleName"] != null) 
                {
                    view.Columns["ModuleName"].Caption = "Modül";
                    view.Columns["ModuleName"].Width = 120;
                }
                if (view.Columns["MeetingSubject"] != null) 
                {
                    view.Columns["MeetingSubject"].Caption = "Toplantı";
                    view.Columns["MeetingSubject"].Width = 150;
                }
                if (view.Columns["SprintName"] != null) 
                {
                    view.Columns["SprintName"].Caption = "Sprint";
                    view.Columns["SprintName"].Width = 120;
                }
                if (view.Columns["CompletedAt"] != null) 
                {
                    view.Columns["CompletedAt"].Caption = "Tamamlanma";
                    view.Columns["CompletedAt"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    view.Columns["CompletedAt"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
                    view.Columns["CompletedAt"].Width = 130;
                }
                if (view.Columns["CreatedAt"] != null) 
                {
                    view.Columns["CreatedAt"].Caption = "Oluşturulma";
                    view.Columns["CreatedAt"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    view.Columns["CreatedAt"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
                    view.Columns["CreatedAt"].Width = 130;
                }

                // Çift tıklama ile detay aç (event handler'ı tekrar eklenmesini önlemek için önce kaldır)
                view.DoubleClick -= GridView1_DoubleClick;
                view.DoubleClick += GridView1_DoubleClick;
            }

            // Toplam sayıyı göster
            lblTotalCount.Text = $"Toplam: {workItems.Count} iş talebi";
        }

        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            btnViewDetails_Click(sender, e);
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view != null && view.FocusedRowHandle >= 0)
            {
                var workItemId = (int)view.GetRowCellValue(view.FocusedRowHandle, "Id");
                var detailForm = new WorkItemDetailForm(workItemId);
                if (detailForm.ShowDialog() == DialogResult.OK)
                {
                    LoadWorkItems();
                }
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
                    LoadWorkItems();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadWorkItems();
        }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            cmbBoardFilter.SelectedIndex = 0;
            cmbStatusFilter.SelectedIndex = 0;
            cmbTypeFilter.SelectedIndex = 0;
            cmbUrgencyFilter.SelectedIndex = 0;
            txtSearch.Text = "";
            LoadWorkItems();
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            LoadWorkItems();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadWorkItems();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadWorkItems();
        }

        private void btnDeleteWorkItem_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view != null && view.FocusedRowHandle >= 0)
            {
                var workItemId = (int)view.GetRowCellValue(view.FocusedRowHandle, "Id");
                var title = view.GetRowCellValue(view.FocusedRowHandle, "Title")?.ToString();

                var result = XtraMessageBox.Show(
                    $"'{title}' iş talebini kalıcı olarak silmek istediğinize emin misiniz?\n\nBu işlem geri alınamaz!",
                    "İş Talebi Sil",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var workItem = _context.WorkItems.Find(workItemId);
                        if (workItem != null)
                        {
                            _context.WorkItems.Remove(workItem);
                            _context.SaveChanges();
                            LoadWorkItems();
                            XtraMessageBox.Show("İş talebi başarıyla silindi.", "Bilgi", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show($"İş talebi silinirken hata oluştu:\n\n{ex.Message}", 
                            "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Lütfen silmek istediğiniz iş talebini seçin.", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnArchiveWorkItem_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view != null && view.FocusedRowHandle >= 0)
            {
                var workItemId = (int)view.GetRowCellValue(view.FocusedRowHandle, "Id");
                var title = view.GetRowCellValue(view.FocusedRowHandle, "Title")?.ToString();

                var result = XtraMessageBox.Show(
                    $"'{title}' iş talebini arşivlemek istediğinize emin misiniz?\n\nArşivlenen iş talepleri normal listede görünmez ancak silinmez.",
                    "İş Talebi Arşivle",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var workItem = _context.WorkItems.Find(workItemId);
                        if (workItem != null)
                        {
                            workItem.IsArchived = true;
                            _context.SaveChanges();
                            LoadWorkItems();
                            XtraMessageBox.Show("İş talebi başarıyla arşivlendi.", "Bilgi", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show($"İş talebi arşivlenirken hata oluştu:\n\n{ex.Message}", 
                            "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Lütfen arşivlemek istediğiniz iş talebini seçin.", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                _context?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

