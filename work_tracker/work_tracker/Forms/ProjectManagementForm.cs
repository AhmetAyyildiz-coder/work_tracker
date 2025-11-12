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
    public partial class ProjectManagementForm : XtraForm
    {
        private WorkTrackerDbContext _context;

        public ProjectManagementForm()
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
        }

        private void ProjectManagementForm_Load(object sender, EventArgs e)
        {
            LoadProjects();
        }

        private void LoadProjects()
        {
            var projects = _context.Projects.OrderBy(p => p.Name).ToList();
            gridControl1.DataSource = projects;
            
            // GridView ayarları
            var view = gridControl1.MainView as GridView;
            if (view != null)
            {
                view.BestFitColumns();
                
                // Kolon başlıklarını Türkçeleştir
                if (view.Columns["Id"] != null) view.Columns["Id"].Caption = "ID";
                if (view.Columns["Name"] != null) view.Columns["Name"].Caption = "Proje Adı";
                if (view.Columns["IsActive"] != null) view.Columns["IsActive"].Caption = "Aktif";
                if (view.Columns["CreatedAt"] != null) 
                {
                    view.Columns["CreatedAt"].Caption = "Oluşturulma Tarihi";
                    view.Columns["CreatedAt"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    view.Columns["CreatedAt"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
                }
                
                // Navigation property'leri gizle
                if (view.Columns["Modules"] != null) view.Columns["Modules"].Visible = false;
                if (view.Columns["WorkItems"] != null) view.Columns["WorkItems"].Visible = false;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            var form = new ProjectEditForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadProjects();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view != null && view.FocusedRowHandle >= 0)
            {
                var project = view.GetRow(view.FocusedRowHandle) as Project;
                if (project != null)
                {
                    var form = new ProjectEditForm(project.Id);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadProjects();
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view != null && view.FocusedRowHandle >= 0)
            {
                var project = view.GetRow(view.FocusedRowHandle) as Project;
                if (project != null)
                {
                    var result = XtraMessageBox.Show(
                        $"'{project.Name}' projesini silmek istediğinize emin misiniz?",
                        "Onay",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        var dbProject = _context.Projects.Find(project.Id);
                        if (dbProject != null)
                        {
                            _context.Projects.Remove(dbProject);
                            _context.SaveChanges();
                            LoadProjects();
                        }
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProjects();
        }
    }
}

