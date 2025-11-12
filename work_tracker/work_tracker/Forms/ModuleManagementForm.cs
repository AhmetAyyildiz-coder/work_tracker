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
    public partial class ModuleManagementForm : XtraForm
    {
        private WorkTrackerDbContext _context;

        public ModuleManagementForm()
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
        }

        private void ModuleManagementForm_Load(object sender, EventArgs e)
        {
            LoadModules();
        }

        private void LoadModules()
        {
            var modules = _context.ProjectModules
                .Include(m => m.Project)
                .OrderBy(m => m.Project.Name)
                .ThenBy(m => m.Name)
                .Select(m => new
                {
                    m.Id,
                    ProjectName = m.Project.Name,
                    m.Name,
                    m.IsActive,
                    m.CreatedAt
                })
                .ToList();
            
            gridControl1.DataSource = modules;
            
            // GridView ayarları
            var view = gridControl1.MainView as GridView;
            if (view != null)
            {
                view.BestFitColumns();
                
                // Kolon başlıklarını Türkçeleştir
                if (view.Columns["Id"] != null) view.Columns["Id"].Caption = "ID";
                if (view.Columns["ProjectName"] != null) view.Columns["ProjectName"].Caption = "Proje";
                if (view.Columns["Name"] != null) view.Columns["Name"].Caption = "Modül Adı";
                if (view.Columns["IsActive"] != null) view.Columns["IsActive"].Caption = "Aktif";
                if (view.Columns["CreatedAt"] != null) 
                {
                    view.Columns["CreatedAt"].Caption = "Oluşturulma Tarihi";
                    view.Columns["CreatedAt"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    view.Columns["CreatedAt"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            var form = new ModuleEditForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadModules();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view != null && view.FocusedRowHandle >= 0)
            {
                var moduleId = (int)view.GetRowCellValue(view.FocusedRowHandle, "Id");
                var form = new ModuleEditForm(moduleId);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadModules();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view != null && view.FocusedRowHandle >= 0)
            {
                var moduleId = (int)view.GetRowCellValue(view.FocusedRowHandle, "Id");
                var moduleName = view.GetRowCellValue(view.FocusedRowHandle, "Name")?.ToString();

                var result = XtraMessageBox.Show(
                    $"'{moduleName}' modülünü silmek istediğinize emin misiniz?",
                    "Onay",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    var module = _context.ProjectModules.Find(moduleId);
                    if (module != null)
                    {
                        _context.ProjectModules.Remove(module);
                        _context.SaveChanges();
                        LoadModules();
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadModules();
        }
    }
}

