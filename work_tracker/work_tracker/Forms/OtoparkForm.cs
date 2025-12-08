using System;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using work_tracker.Data;
using work_tracker.Data.Entities;
using work_tracker.Helpers;

namespace work_tracker.Forms
{
    /// <summary>
    /// Otopark Formu - Düşük öncelikli, "nice to have" işler için
    /// "Belki bir gün yapılır" mantığıyla tutulan işler
    /// </summary>
    public partial class OtoparkForm : XtraForm
    {
        private WorkTrackerDbContext _context;
        private GridControl gridControl;
        private GridView gridView;
        private BarManager barManager;
        private Bar toolbar;
        private BarEditItem searchEdit;
        private PopupMenu contextMenu;

        public OtoparkForm()
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
        }

        private void InitializeComponent()
        {
            this.Text = "🚗 Otopark - Düşük Öncelikli İşler";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterParent;

            // BarManager ve Toolbar
            barManager = new BarManager();
            barManager.Form = this;

            toolbar = new Bar();
            toolbar.BarName = "Araçlar";
            toolbar.DockCol = 0;
            toolbar.DockRow = 0;
            toolbar.DockStyle = BarDockStyle.Top;
            barManager.Bars.Add(toolbar);

            // Yenile butonu
            var btnRefresh = new BarButtonItem();
            btnRefresh.Caption = "🔄 Yenile";
            btnRefresh.ItemClick += (s, e) => LoadData();
            toolbar.LinksPersistInfo.Add(new LinkPersistInfo(btnRefresh));
            barManager.Items.Add(btnRefresh);

            // Inbox'a Taşı butonu
            var btnMoveToInbox = new BarButtonItem();
            btnMoveToInbox.Caption = "📥 Inbox'a Taşı";
            btnMoveToInbox.ItemClick += (s, e) => MoveToBoard("Inbox");
            toolbar.LinksPersistInfo.Add(new LinkPersistInfo(btnMoveToInbox));
            barManager.Items.Add(btnMoveToInbox);

            // Kanban'a Taşı butonu
            var btnMoveToKanban = new BarButtonItem();
            btnMoveToKanban.Caption = "📋 Kanban'a Taşı";
            btnMoveToKanban.ItemClick += (s, e) => MoveToBoard("Kanban");
            toolbar.LinksPersistInfo.Add(new LinkPersistInfo(btnMoveToKanban));
            barManager.Items.Add(btnMoveToKanban);

            // Ayırıcı
            toolbar.LinksPersistInfo.Add(new LinkPersistInfo(new BarItemLink(), true));

            // Arşivle butonu
            var btnArchive = new BarButtonItem();
            btnArchive.Caption = "📦 Arşivle";
            btnArchive.ItemClick += (s, e) => ArchiveSelected();
            toolbar.LinksPersistInfo.Add(new LinkPersistInfo(btnArchive));
            barManager.Items.Add(btnArchive);

            // Sil butonu
            var btnDelete = new BarButtonItem();
            btnDelete.Caption = "🗑️ Sil";
            btnDelete.ItemClick += (s, e) => DeleteSelected();
            toolbar.LinksPersistInfo.Add(new LinkPersistInfo(btnDelete));
            barManager.Items.Add(btnDelete);

            // Ayırıcı
            toolbar.LinksPersistInfo.Add(new LinkPersistInfo(new BarItemLink(), true));

            // Arama kutusu
            var searchRepo = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            searchRepo.NullValuePrompt = "🔍 Ara...";
            searchRepo.NullValuePromptShowForEmptyValue = true;
            searchEdit = new BarEditItem();
            searchEdit.Caption = "Ara";
            searchEdit.Width = 200;
            searchEdit.Edit = searchRepo;
            searchEdit.EditValueChanged += (s, e) => FilterData();
            toolbar.LinksPersistInfo.Add(new LinkPersistInfo(searchEdit));
            barManager.Items.Add(searchEdit);
            barManager.RepositoryItems.Add(searchRepo);

            // Grid
            gridControl = new GridControl();
            gridControl.Dock = DockStyle.Fill;
            gridView = new GridView(gridControl);
            gridControl.MainView = gridView;

            // Grid ayarları
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsView.RowAutoHeight = true;
            gridView.OptionsSelection.MultiSelect = true;
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gridView.RowStyle += GridView_RowStyle;
            gridView.DoubleClick += GridView_DoubleClick;

            // Sağ tık menüsü
            contextMenu = new PopupMenu(barManager);
            
            var menuMoveToInbox = new BarButtonItem(barManager, "📥 Inbox'a Taşı");
            menuMoveToInbox.ItemClick += (s, e) => MoveToBoard("Inbox");
            contextMenu.LinksPersistInfo.Add(new LinkPersistInfo(menuMoveToInbox));
            
            var menuMoveToKanban = new BarButtonItem(barManager, "📋 Kanban'a Taşı");
            menuMoveToKanban.ItemClick += (s, e) => MoveToBoard("Kanban");
            contextMenu.LinksPersistInfo.Add(new LinkPersistInfo(menuMoveToKanban));
            
            contextMenu.LinksPersistInfo.Add(new LinkPersistInfo(new BarItemLink(), true));
            
            var menuArchive = new BarButtonItem(barManager, "📦 Arşivle");
            menuArchive.ItemClick += (s, e) => ArchiveSelected();
            contextMenu.LinksPersistInfo.Add(new LinkPersistInfo(menuArchive));
            
            var menuDelete = new BarButtonItem(barManager, "🗑️ Sil");
            menuDelete.ItemClick += (s, e) => DeleteSelected();
            contextMenu.LinksPersistInfo.Add(new LinkPersistInfo(menuDelete));

            gridView.PopupMenuShowing += (s, e) =>
            {
                if (e.MenuType == GridMenuType.Row)
                {
                    e.Allow = false;
                    contextMenu.ShowPopup(barManager, MousePosition);
                }
            };

            this.Controls.Add(gridControl);

            // Bilgi paneli
            var infoPanel = new PanelControl();
            infoPanel.Dock = DockStyle.Bottom;
            infoPanel.Height = 40;
            infoPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            
            var lblInfo = new LabelControl();
            lblInfo.Text = "💡 Otopark: Düşük öncelikli, 'bir gün yapılabilir' işler için. Arşivden farkı: Arşiv = bitmiş, Otopark = belki yapılır.";
            lblInfo.Appearance.ForeColor = Color.Gray;
            lblInfo.Location = new Point(10, 12);
            infoPanel.Controls.Add(lblInfo);
            
            this.Controls.Add(infoPanel);

            this.Load += OtoparkForm_Load;
        }

        private void OtoparkForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var data = _context.WorkItems
                    .Include(w => w.Project)
                    .Where(w => w.Board == "Otopark" && !w.IsArchived)
                    .OrderByDescending(w => w.CreatedAt)
                    .Select(w => new
                    {
                        w.Id,
                        w.Title,
                        ProjectName = w.Project != null ? w.Project.Name : "-",
                        w.Type,
                        w.Urgency,
                        w.RequestedBy,
                        w.CreatedAt,
                        w.Description
                    })
                    .ToList();

                gridControl.DataSource = data;

                // Kolonları ayarla
                if (gridView.Columns.Count > 0)
                {
                    gridView.Columns["Id"].Caption = "ID";
                    gridView.Columns["Id"].Width = 50;
                    
                    gridView.Columns["Title"].Caption = "Başlık";
                    gridView.Columns["Title"].Width = 300;
                    
                    gridView.Columns["ProjectName"].Caption = "Proje";
                    gridView.Columns["ProjectName"].Width = 150;
                    
                    gridView.Columns["Type"].Caption = "Tür";
                    gridView.Columns["Type"].Width = 100;
                    
                    gridView.Columns["Urgency"].Caption = "Öncelik";
                    gridView.Columns["Urgency"].Width = 80;
                    
                    gridView.Columns["RequestedBy"].Caption = "Talep Eden";
                    gridView.Columns["RequestedBy"].Width = 120;
                    
                    gridView.Columns["CreatedAt"].Caption = "Eklenme Tarihi";
                    gridView.Columns["CreatedAt"].Width = 120;
                    gridView.Columns["CreatedAt"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    gridView.Columns["CreatedAt"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
                    
                    gridView.Columns["Description"].Caption = "Açıklama";
                    gridView.Columns["Description"].Width = 250;
                }

                this.Text = $"🚗 Otopark - Düşük Öncelikli İşler ({data.Count} iş)";
            }
            catch (Exception ex)
            {
                Logger.Error("Otopark verileri yüklenirken hata", ex);
                XtraMessageBox.Show($"Veriler yüklenirken hata oluştu:\n{ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FilterData()
        {
            var searchText = searchEdit.EditValue?.ToString() ?? "";
            
            if (string.IsNullOrWhiteSpace(searchText))
            {
                gridView.ActiveFilterString = "";
            }
            else
            {
                gridView.ActiveFilterString = $"[Title] LIKE '%{searchText}%' OR [Description] LIKE '%{searchText}%' OR [ProjectName] LIKE '%{searchText}%'";
            }
        }

        private void GridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            // Satır renklerini ayarla (opsiyonel)
        }

        private void GridView_DoubleClick(object sender, EventArgs e)
        {
            var selectedRows = gridView.GetSelectedRows();
            if (selectedRows.Length == 0) return;

            var id = (int)gridView.GetRowCellValue(selectedRows[0], "Id");
            var workItem = _context.WorkItems.Find(id);
            
            if (workItem != null)
            {
                // WorkItemEditForm açılabilir (varsa)
                // Şimdilik detay göster
                var message = $"ID: {workItem.Id}\n" +
                             $"Başlık: {workItem.Title}\n" +
                             $"Açıklama: {workItem.Description ?? "-"}\n" +
                             $"Talep Eden: {workItem.RequestedBy ?? "-"}\n" +
                             $"Oluşturulma: {workItem.CreatedAt:dd.MM.yyyy HH:mm}";
                
                XtraMessageBox.Show(message, "İş Detayı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void MoveToBoard(string targetBoard)
        {
            var selectedRows = gridView.GetSelectedRows();
            if (selectedRows.Length == 0)
            {
                XtraMessageBox.Show("Lütfen en az bir iş seçin.", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var targetStatus = targetBoard == "Kanban" ? "GelenAcilIsler" : "Bekliyor";
            var boardName = targetBoard == "Kanban" ? "Kanban Panosu" : "Gelen Kutusu";

            var result = XtraMessageBox.Show(
                $"Seçili {selectedRows.Length} iş {boardName}'na taşınacak. Devam edilsin mi?",
                "Taşı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            try
            {
                foreach (var rowHandle in selectedRows)
                {
                    var id = (int)gridView.GetRowCellValue(rowHandle, "Id");
                    var workItem = _context.WorkItems.Find(id);
                    
                    if (workItem != null)
                    {
                        var oldBoard = workItem.Board;
                        workItem.Board = targetBoard;
                        workItem.Status = targetStatus;

                        // Aktivite kaydı
                        _context.WorkItemActivities.Add(new WorkItemActivity
                        {
                            WorkItemId = workItem.Id,
                            ActivityType = "BoardChange",
                            OldValue = oldBoard,
                            NewValue = targetBoard,
                            Description = $"İş Otopark'tan {boardName}'na taşındı",
                            CreatedBy = Environment.UserName,
                            CreatedAt = DateTime.Now
                        });
                    }
                }

                _context.SaveChanges();
                Logger.Info($"{selectedRows.Length} iş Otopark'tan {boardName}'na taşındı");
                LoadData();

                XtraMessageBox.Show($"{selectedRows.Length} iş başarıyla {boardName}'na taşındı.", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Logger.Error("İş taşınırken hata", ex);
                XtraMessageBox.Show($"İşlem sırasında hata oluştu:\n{ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ArchiveSelected()
        {
            var selectedRows = gridView.GetSelectedRows();
            if (selectedRows.Length == 0)
            {
                XtraMessageBox.Show("Lütfen en az bir iş seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = XtraMessageBox.Show(
                $"Seçili {selectedRows.Length} iş arşivlenecek. Devam edilsin mi?",
                "Arşivle",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            try
            {
                foreach (var rowHandle in selectedRows)
                {
                    var id = (int)gridView.GetRowCellValue(rowHandle, "Id");
                    var workItem = _context.WorkItems.Find(id);

                    if (workItem != null)
                    {
                        workItem.IsArchived = true;

                        _context.WorkItemActivities.Add(new WorkItemActivity
                        {
                            WorkItemId = workItem.Id,
                            ActivityType = "StatusChange",
                            OldValue = "Otopark",
                            NewValue = "Arşiv",
                            Description = "İş Otopark'tan arşive taşındı",
                            CreatedBy = Environment.UserName,
                            CreatedAt = DateTime.Now
                        });
                    }
                }

                _context.SaveChanges();
                Logger.Info($"{selectedRows.Length} iş arşivlendi");
                LoadData();

                XtraMessageBox.Show($"{selectedRows.Length} iş başarıyla arşivlendi.", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Logger.Error("Arşivleme sırasında hata", ex);
                XtraMessageBox.Show($"İşlem sırasında hata oluştu:\n{ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteSelected()
        {
            var selectedRows = gridView.GetSelectedRows();
            if (selectedRows.Length == 0)
            {
                XtraMessageBox.Show("Lütfen en az bir iş seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = XtraMessageBox.Show(
                $"Seçili {selectedRows.Length} iş kalıcı olarak silinecek!\n\nBu işlem geri alınamaz. Devam edilsin mi?",
                "Sil",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            try
            {
                foreach (var rowHandle in selectedRows)
                {
                    var id = (int)gridView.GetRowCellValue(rowHandle, "Id");
                    var workItem = _context.WorkItems.Find(id);

                    if (workItem != null)
                    {
                        _context.WorkItems.Remove(workItem);
                    }
                }

                _context.SaveChanges();
                Logger.Info($"{selectedRows.Length} iş silindi");
                LoadData();

                XtraMessageBox.Show($"{selectedRows.Length} iş başarıyla silindi.", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Logger.Error("Silme sırasında hata", ex);
                XtraMessageBox.Show($"İşlem sırasında hata oluştu:\n{ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
