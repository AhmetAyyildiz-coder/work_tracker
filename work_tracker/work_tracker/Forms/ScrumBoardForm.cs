using System;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using work_tracker.Data;
using work_tracker.Data.Entities;

namespace work_tracker.Forms
{
    public partial class ScrumBoardForm : XtraForm
    {
        private WorkTrackerDbContext _context;
        private LayoutControl layoutControl1;
        private LayoutControlGroup layoutControlGroup1;
        private LookUpEdit cmbSprint;
        private ComboBoxEdit cmbSprintFilter;
        private SimpleButton btnRefresh;
        private SimpleButton btnManageSprints;
        private readonly string[] scrumColumns = new[] { "SprintBacklog", "Gelistirmede", "Testte", "Tamamlandi" };

        // Drag & drop durumu (Kanban ile aynı mantık)
        private PanelControl draggedCard = null;
        private bool isDragging = false;
        private bool isMouseDown = false;
        private Point mouseDownPoint;

        public ScrumBoardForm()
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
        }

        private void ScrumBoardForm_Load(object sender, EventArgs e)
        {
            // Filtre combobox'ını doldur
            cmbSprintFilter.Properties.Items.AddRange(new string[] 
            { 
                "Aktif & Planlanan", 
                "Tamamlanan", 
                "İptal Edilen", 
                "Tümü" 
            });
            cmbSprintFilter.SelectedIndex = 0; // Varsayılan: Aktif & Planlanan
            
            LoadSprints();
            LoadScrumBoard();
        }
        
        private void cmbSprintFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSprints();
            LoadScrumBoard();
        }

        private void LoadSprints()
        {
            try
            {
                // Context'i yenile - eski veriler kalmasın
                _context = new WorkTrackerDbContext();
                
                // Filtre seçimine göre sprint'leri getir
                string filterValue = cmbSprintFilter?.EditValue?.ToString() ?? "Aktif & Planlanan";
                
                IQueryable<Sprint> query = _context.Sprints;
                
                switch (filterValue)
                {
                    case "Aktif & Planlanan":
                        query = query.Where(s => s.Status == "Active" || s.Status == "Planned");
                        break;
                    case "Tamamlanan":
                        query = query.Where(s => s.Status == "Completed");
                        break;
                    case "İptal Edilen":
                        query = query.Where(s => s.Status == "Cancelled");
                        break;
                    case "Tümü":
                        // Tüm sprint'leri göster, filtre yok
                        break;
                    default:
                        query = query.Where(s => s.Status == "Active" || s.Status == "Planned");
                        break;
                }
                
                var sprints = query
                    .OrderByDescending(s => s.Status == "Active")
                    .ThenByDescending(s => s.StartDate)
                    .ToList();

                cmbSprint.Properties.DataSource = sprints;
                cmbSprint.Properties.DisplayMember = "Name";
                cmbSprint.Properties.ValueMember = "Id";
                cmbSprint.Properties.NullText = "Sprint Seçin...";

                // Dropdown'da gösterilen kolonları sadeleştir
                cmbSprint.Properties.Columns.Clear();
                cmbSprint.Properties.Columns.Add(new LookUpColumnInfo("Name", "Sprint"));
                cmbSprint.Properties.Columns.Add(new LookUpColumnInfo("Goals", "Hedef"));
                cmbSprint.Properties.Columns.Add(new LookUpColumnInfo("StartDate", "Başlangıç Tarihi")
                {
                    FormatType = DevExpress.Utils.FormatType.DateTime,
                    FormatString = "dd.MM.yyyy"
                });
                cmbSprint.Properties.Columns.Add(new LookUpColumnInfo("EndDate", "Bitiş Tarihi")
                {
                    FormatType = DevExpress.Utils.FormatType.DateTime,
                    FormatString = "dd.MM.yyyy"
                });

                // Popup genişliğini ayarla
                cmbSprint.Properties.BestFitMode = BestFitMode.BestFitResizePopup;

                if (sprints.Any())
                {
                    var activeSprint = sprints.FirstOrDefault(s => s.Status == "Active");
                    if (activeSprint != null)
                    {
                        cmbSprint.EditValue = activeSprint.Id;
                    }
                    else
                    {
                        // Aktif yoksa ilk planlanmış sprint'i seç
                        cmbSprint.EditValue = sprints.First().Id;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Sprint'ler yüklenirken hata oluştu:\n{ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadScrumBoard()
        {
            if (cmbSprint.EditValue == null)
            {
                // Sprint seçilmediyse boş göster
                layoutControl1.BeginUpdate();
                try
                {
                    layoutControl1.Controls.Clear();
                    layoutControlGroup1.Items.Clear();
                    // Tablo düzenini sıfırla
                    layoutControlGroup1.OptionsTableLayoutGroup.ColumnDefinitions.Clear();
                    layoutControlGroup1.OptionsTableLayoutGroup.RowDefinitions.Clear();
                }
                finally
                {
                    layoutControl1.EndUpdate();
                }
                return;
            }

            int selectedSprintId = Convert.ToInt32(cmbSprint.EditValue);

            layoutControl1.BeginUpdate();
            try
            {
                // Önceki kontrolları temizle
                layoutControl1.Controls.Clear();
                layoutControlGroup1.Items.Clear();

                // Tablo düzenini kur (1 satır, N sütun)
                layoutControlGroup1.LayoutMode = DevExpress.XtraLayout.Utils.LayoutMode.Table;
                var table = layoutControlGroup1.OptionsTableLayoutGroup;
                table.ColumnDefinitions.Clear();
                table.RowDefinitions.Clear();

                // 1 satır, %100 yükseklik
                var row = new DevExpress.XtraLayout.RowDefinition();
                row.SizeType = System.Windows.Forms.SizeType.Percent;
                row.Height = 100;
                table.RowDefinitions.Add(row);

                // Sütunları eşit yüzde ile tanımla
                int colCount = scrumColumns.Length;
                for (int i = 0; i < colCount; i++)
                {
                    var col = new DevExpress.XtraLayout.ColumnDefinition();
                    col.SizeType = System.Windows.Forms.SizeType.Percent;
                    col.Width = 100.0 / colCount;
                    table.ColumnDefinitions.Add(col);
                }

                // Scrum sütunlarını oluştur
                for (int i = 0; i < scrumColumns.Length; i++)
                {
                    var columnName = scrumColumns[i];
                    var panel = CreateColumnPanel(columnName, selectedSprintId);
                    layoutControl1.Controls.Add(panel);

                    var layoutItem = layoutControlGroup1.AddItem();
                    layoutItem.Control = panel;
                    layoutItem.TextVisible = false;
                    // Her paneli kendi sütununa yerleştir
                    var itemOptions = layoutItem.OptionsTableLayoutItem;
                    itemOptions.RowIndex = 0;
                    itemOptions.ColumnIndex = i;
                }
            }
            finally
            {
                layoutControl1.EndUpdate();
            }
        }

        private PanelControl CreateColumnPanel(string columnName, int sprintId)
        {
            var panel = new PanelControl();
            panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panel.AllowDrop = true;
            panel.Tag = columnName;
            panel.BackColor = SystemColors.Control;

            // Başlık paneli - minimal
            var headerPanel = new PanelControl();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 35;
            headerPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;

            // İş kartlarını yükle
            var workItems = _context.WorkItems
                .Where(w => w.Board == "Scrum" && w.Status == columnName && w.SprintId == sprintId)
                .OrderBy(w => w.OrderIndex)
                .ToList();

            // Başlık - compact
            var lblHeader = new LabelControl();
            lblHeader.Text = $"{GetColumnDisplayName(columnName)} ({workItems.Count})";
            lblHeader.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            lblHeader.Appearance.ForeColor = Color.DimGray;
            lblHeader.AutoSizeMode = LabelAutoSizeMode.None;
            lblHeader.Location = new Point(8, 8);
            lblHeader.Size = new Size(200, 20);
            headerPanel.Controls.Add(lblHeader);

            panel.Controls.Add(headerPanel);

            // Kartları tutacak scroll alanı
            var scroll = new PanelControl();
            scroll.Dock = DockStyle.Fill;
            scroll.AutoScroll = true;
            scroll.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            scroll.Tag = columnName;
            scroll.BackColor = SystemColors.Control;
            scroll.AllowDrop = true;
            panel.Controls.Add(scroll);

            int yPos = 5;
            foreach (var workItem in workItems)
            {
                var card = CreateWorkItemCard(workItem);
                card.Location = new Point(5, yPos);
                card.Width = Math.Max(220, panel.Width - 15);
                scroll.Controls.Add(card);
                yPos += card.Height + 5;
            }

            // Drag & Drop olayları - hem scroll hem de ana panel için
            scroll.DragEnter += Panel_DragEnter;
            scroll.DragOver += Panel_DragOver;
            scroll.DragDrop += Panel_DragDrop;
            
            panel.DragEnter += Panel_DragEnter;
            panel.DragOver += Panel_DragOver;
            panel.DragDrop += Panel_DragDrop;

            return panel;
        }

        private string GetColumnDisplayName(string columnName)
        {
            switch (columnName)
            {
                case "SprintBacklog":
                    return "Sprint Backlog";
                case "Gelistirmede":
                    return "Geliştirmede";
                case "Testte":
                    return "Testte";
                case "Tamamlandi":
                    return "Tamamlandı";
                default:
                    return columnName;
            }
        }

        private PanelControl CreateWorkItemCard(WorkItem workItem)
        {
            // DevExpress PanelControl - Minimal ve performanslı
            var card = new PanelControl();
            card.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            card.Size = new Size(220, 110);
            card.Tag = workItem;
            card.AllowDrop = false;
            card.Cursor = Cursors.Hand;

            // Aciliyet göstergesi çizgisi (sol kenar)
            var urgencyIndicator = new SimpleButton
            {
                Width = 4,
                Height = 110,
                Location = new Point(0, 0),
                Enabled = false,
                Appearance = { BackColor = GetUrgencyColor(workItem.Urgency) },
                AllowFocus = false
            };
            card.Controls.Add(urgencyIndicator);

            // ID - LabelControl
            var lblId = new LabelControl();
            lblId.Text = $"#{workItem.Id}";
            lblId.Location = new Point(10, 8);
            lblId.AutoSizeMode = LabelAutoSizeMode.None;
            lblId.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold);
            lblId.Appearance.ForeColor = Color.Gray;
            card.Controls.Add(lblId);

            // Başlık - compact
            var titleText = workItem.Title.Length > 30 ? workItem.Title.Substring(0, 27) + "..." : workItem.Title;
            var lblTitle = new LabelControl();
            lblTitle.Text = titleText;
            lblTitle.Location = new Point(10, 24);
            lblTitle.Size = new Size(200, 30);
            lblTitle.AutoSizeMode = LabelAutoSizeMode.None;
            lblTitle.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            lblTitle.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            card.Controls.Add(lblTitle);

            // Badges - compact
            int badgeX = 10;
            var lblType = new LabelControl();
            lblType.Text = GetShortType(workItem.Type);
            lblType.Location = new Point(badgeX, 56);
            lblType.AutoSizeMode = LabelAutoSizeMode.None;
            lblType.Size = new Size(55, 16);
            lblType.Appearance.Font = new Font("Tahoma", 7F, FontStyle.Bold);
            lblType.Appearance.ForeColor = Color.DimGray;
            lblType.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            card.Controls.Add(lblType);

            badgeX += 60;
            var lblUrgency = new LabelControl();
            lblUrgency.Text = GetShortUrgency(workItem.Urgency);
            lblUrgency.Location = new Point(badgeX, 56);
            lblUrgency.AutoSizeMode = LabelAutoSizeMode.None;
            lblUrgency.Size = new Size(50, 16);
            lblUrgency.Appearance.Font = new Font("Tahoma", 7F, FontStyle.Bold);
            lblUrgency.Appearance.ForeColor = GetUrgencyColor(workItem.Urgency);
            lblUrgency.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            card.Controls.Add(lblUrgency);

            // Alt bilgiler - tek satır
            var footerText = "";
            if (!string.IsNullOrEmpty(workItem.RequestedBy))
            {
                footerText = workItem.RequestedBy.Length > 10 ? workItem.RequestedBy.Substring(0, 8) + "." : workItem.RequestedBy;
            }
            if (workItem.EffortEstimate.HasValue)
            {
                footerText += (string.IsNullOrEmpty(footerText) ? "" : " | ") + $"{workItem.EffortEstimate}g";
            }

            if (!string.IsNullOrEmpty(footerText))
            {
                var lblFooter = new LabelControl();
                lblFooter.Text = footerText;
                lblFooter.Location = new Point(10, 76);
                lblFooter.AutoSizeMode = LabelAutoSizeMode.None;
                lblFooter.Size = new Size(150, 14);
                lblFooter.Appearance.Font = new Font("Tahoma", 7F);
                lblFooter.Appearance.ForeColor = Color.Gray;
                card.Controls.Add(lblFooter);
            }

            // İkonlar - sağ alt
            int iconX = 190;
            
            var activityCount = _context.WorkItemActivities.Count(a => a.WorkItemId == workItem.Id);
            if (activityCount > 0)
            {
                var lblActivity = new LabelControl();
                lblActivity.Text = $"💬{activityCount}";
                lblActivity.Location = new Point(iconX, 76);
                lblActivity.AutoSizeMode = LabelAutoSizeMode.Horizontal;
                lblActivity.Appearance.Font = new Font("Tahoma", 7F);
                lblActivity.Appearance.ForeColor = SystemColors.HotTrack;
                lblActivity.ToolTip = $"{activityCount} aktivite";
                card.Controls.Add(lblActivity);
                iconX -= 30;
            }

            var attachmentCount = _context.WorkItemAttachments.Count(a => a.WorkItemId == workItem.Id);
            if (attachmentCount > 0)
            {
                var lblAttachment = new LabelControl();
                lblAttachment.Text = $"📎{attachmentCount}";
                lblAttachment.Location = new Point(iconX, 76);
                lblAttachment.AutoSizeMode = LabelAutoSizeMode.Horizontal;
                lblAttachment.Appearance.Font = new Font("Tahoma", 7F);
                lblAttachment.ToolTip = $"{attachmentCount} dosya";
                card.Controls.Add(lblAttachment);
            }

            // Tarih
            var lblDate = new LabelControl();
            lblDate.Text = workItem.RequestedAt.ToString("dd.MM");
            lblDate.Location = new Point(10, 92);
            lblDate.AutoSizeMode = LabelAutoSizeMode.None;
            lblDate.Size = new Size(40, 12);
            lblDate.Appearance.Font = new Font("Tahoma", 7F);
            lblDate.Appearance.ForeColor = Color.LightGray;
            card.Controls.Add(lblDate);

            // Drag & Click olayları (Kanban ile aynı yaklaşım)
            card.MouseDown += Card_MouseDown;
            card.MouseMove += Card_MouseMove;
            card.MouseUp += Card_MouseUp;
            card.Click += (s, e) => { if (!isDragging) OpenWorkItemDetail(workItem.Id); };

            foreach (Control ctrl in card.Controls)
            {
                ctrl.Cursor = Cursors.Hand;
                ctrl.MouseDown += Card_MouseDown;
                ctrl.MouseMove += Card_MouseMove;
                ctrl.MouseUp += Card_MouseUp;
                ctrl.Click += (s, e) => { if (!isDragging) OpenWorkItemDetail(workItem.Id); };
            }

            return card;
        }

        /// <summary>
        /// Tip adını kısaltır
        /// </summary>
        private string GetShortType(string type)
        {
            switch (type)
            {
                case "AcilArge": return "Acil";
                case "Bug": return "Bug";
                case "YeniOzellik": return "Özellik";
                case "Geliştirme": return "Geliş.";
                case "Destek": return "Destek";
                case "Analiz": return "Analiz";
                default: return type?.Substring(0, Math.Min(6, type.Length)) ?? "N/A";
            }
        }

        /// <summary>
        /// Aciliyet adını kısaltır
        /// </summary>
        private string GetShortUrgency(string urgency)
        {
            switch (urgency)
            {
                case "Kritik": return "KRİTİK";
                case "Yüksek": return "Yüksek";
                case "Normal": return "Normal";
                case "Düşük": return "Düşük";
                default: return urgency ?? "N/A";
            }
        }

        /// <summary>
        /// Aciliyet rengini döndürür
        /// </summary>
        private Color GetUrgencyColor(string urgency)
        {
            switch (urgency)
            {
                case "Kritik":
                    return Color.FromArgb(220, 53, 69);
                case "Yüksek":
                case "Yuksek":
                    return Color.FromArgb(253, 126, 20);
                case "Normal":
                    return Color.FromArgb(13, 110, 253);
                case "Düşük":
                    return Color.FromArgb(108, 117, 125);
                default:
                    return Color.FromArgb(108, 117, 125);
            }
        }

        private void Card_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var card = sender as PanelControl ?? (sender as Control)?.Parent as PanelControl;
                if (card != null)
                {
                    draggedCard = card;
                    mouseDownPoint = Control.MousePosition;
                    isDragging = false;
                    isMouseDown = true;
                }
            }
        }

        private void Card_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown && draggedCard != null && !isDragging)
            {
                // Sürükleme eşiği - ekran koordinatlarında kontrol et
                var currentPosition = Control.MousePosition;
                var dragSize = SystemInformation.DragSize;
                
                if (Math.Abs(currentPosition.X - mouseDownPoint.X) >= dragSize.Width / 2 ||
                    Math.Abs(currentPosition.Y - mouseDownPoint.Y) >= dragSize.Height / 2)
                {
                    isDragging = true;
                    draggedCard.DoDragDrop(draggedCard, DragDropEffects.Move);
                    isMouseDown = false;
                }
            }
        }

        private void Card_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
                if (!isDragging)
                {
                    draggedCard = null;
                }
            }
        }

        private void Panel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PanelControl)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void Panel_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PanelControl)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void Panel_DragDrop(object sender, DragEventArgs e)
        {
            var targetPanel = sender as PanelControl;
            if (targetPanel == null || draggedCard == null) return;

            var workItem = draggedCard.Tag as WorkItem;
            if (workItem == null) return;

            var targetColumn = targetPanel.Tag?.ToString();
            if (string.IsNullOrEmpty(targetColumn)) return;

            // İş talebini güncelle
            try
            {
                    var dbWorkItem = _context.WorkItems.Find(workItem.Id);
                if (dbWorkItem != null)
                {
                    var oldStatus = dbWorkItem.Status;
                        int? selectedSprintId = null;
                        if (cmbSprint.EditValue != null)
                        {
                            selectedSprintId = Convert.ToInt32(cmbSprint.EditValue);
                        }

                    // Aynı sütuna bırakıldıysa, sona taşı (sıralama)
                    if (oldStatus == targetColumn)
                    {
                        var maxOrder = _context.WorkItems
                            .Where(w => w.Board == "Scrum" && w.Status == targetColumn && w.SprintId == selectedSprintId)
                            .Select(w => (int?)w.OrderIndex)
                            .Max() ?? 0;
                        dbWorkItem.OrderIndex = maxOrder + 1;
                    }
                    else
                    {
                        dbWorkItem.Status = targetColumn;
                        var maxOrder = _context.WorkItems
                            .Where(w => w.Board == "Scrum" && w.Status == targetColumn && w.SprintId == selectedSprintId)
                            .Select(w => (int?)w.OrderIndex)
                            .Max() ?? 0;
                        dbWorkItem.OrderIndex = maxOrder + 1;
                    }
                    
                    // Geliştirmede durumuna geçildiğinde başlangıç zamanını kaydet
                    if (targetColumn == "Gelistirmede" && oldStatus != "Gelistirmede")
                    {
                        if (!dbWorkItem.StartedAt.HasValue)
                        {
                            dbWorkItem.StartedAt = DateTime.Now;
                        }
                    }
                    else if (oldStatus == "Gelistirmede" && targetColumn != "Gelistirmede")
                    {
                        // Geliştirmede durumundan çıkıldığında StartedAt'i sıfırlama (geçmiş verileri koru)
                        // Eğer geri dönülürse tekrar set edilmesin diye kontrol ediyoruz
                    }

                    // Tamamlandı durumuna geçildiğinde tamamlanma zamanını kaydet
                        if (targetColumn == "Tamamlandi")
                        {
                            dbWorkItem.CompletedAt = DateTime.Now;

                            // İş hangi sprint'te tamamlandıysa onu da kaydet
                            if (selectedSprintId.HasValue)
                            {
                                dbWorkItem.CompletedInSprintId = selectedSprintId.Value;
                            }
                        }
                        else if (oldStatus == "Tamamlandi")
                        {
                            // Tamamlandı'dan çıkıyorsa tamamlanma bilgisini temizle
                            dbWorkItem.CompletedAt = null;
                            dbWorkItem.CompletedInSprintId = null;
                        }

                    _context.SaveChanges();

                    // Aktivite kaydı oluştur
                    if (oldStatus != targetColumn)
                    {
                        var activity = new WorkItemActivity
                        {
                            WorkItemId = dbWorkItem.Id,
                            ActivityType = "StatusChange",
                            Description = $"Durum değiştirildi: {GetColumnDisplayName(oldStatus)} → {GetColumnDisplayName(targetColumn)}",
                            OldValue = oldStatus,
                            NewValue = targetColumn,
                            CreatedBy = Environment.UserName,
                            CreatedAt = DateTime.Now
                        };
                        _context.WorkItemActivities.Add(activity);
                        _context.SaveChanges();
                    }

                    LoadScrumBoard();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                draggedCard = null;
                isDragging = false;
            }
        }

        private void cmbSprint_EditValueChanged(object sender, EventArgs e)
        {
            LoadScrumBoard();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSprints();
            LoadScrumBoard();
        }

        private void btnManageSprints_Click(object sender, EventArgs e)
        {
            var form = new SprintManagementForm();
            form.ShowDialog();
            
            // Form kapandıktan sonra her zaman yenile (değişiklik olmuş olabilir)
            LoadSprints();
            LoadScrumBoard();
        }

        /// <summary>
        /// İş detay formunu açar
        /// </summary>
        private void OpenWorkItemDetail(int workItemId)
        {
            var detailForm = new WorkItemDetailForm(workItemId);
            if (detailForm.ShowDialog() == DialogResult.OK)
            {
                // Form kapandıktan sonra board'u yenile
                LoadScrumBoard();
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

