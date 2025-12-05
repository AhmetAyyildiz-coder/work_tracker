using System;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using work_tracker.Data;
using work_tracker.Data.Entities;
using work_tracker.Helpers;

namespace work_tracker.Forms
{
    public partial class KanbanBoardForm : XtraForm
    {
        private WorkTrackerDbContext _context;
        private string _currentSearch;

        public KanbanBoardForm()
        {
            InitializeComponent();
            
            try
            {
                Logger.Info("KanbanBoardForm oluşturuluyor");
                _context = new WorkTrackerDbContext();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "KanbanBoardForm constructor hatası");
                throw;
            }
        }

        private void KanbanBoardForm_Load(object sender, EventArgs e)
        {
            LoadKanbanBoard();
        }

        private void LoadKanbanBoard()
        {
            try
            {
                Logger.Info("Kanban board yükleniyor");
                
                // Kanban sütunlarını al
                var columns = _context.KanbanColumnSettings
                    .Where(c => c.Board == "Kanban")
                    .OrderBy(c => c.DisplayOrder)
                    .ToList();

                if (!columns.Any())
                {
                    Logger.Warning("Kanban sütunları bulunamadı!");
                    XtraMessageBox.Show(
                        "Kanban sütunları tanımlanmamış! Lütfen Configuration.cs'de seed data'yı kontrol edin.",
                        "Uyarı",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                layoutControl1.BeginUpdate();
                try
                {
                    layoutControl1.Controls.Clear();
                    layoutControlGroup1.Items.Clear();

                    // Tablo düzenini ayarla
                    layoutControlGroup1.LayoutMode = DevExpress.XtraLayout.Utils.LayoutMode.Table;
                    var table = layoutControlGroup1.OptionsTableLayoutGroup;
                    table.ColumnDefinitions.Clear();
                    table.RowDefinitions.Clear();

                    // 1 satır
                    var row = new DevExpress.XtraLayout.RowDefinition();
                    row.SizeType = SizeType.Percent;
                    row.Height = 100;
                    table.RowDefinitions.Add(row);

                    // Sütunları oluştur
                    int colCount = columns.Count;
                    for (int i = 0; i < colCount; i++)
                    {
                        var col = new DevExpress.XtraLayout.ColumnDefinition();
                        col.SizeType = SizeType.Percent;
                        col.Width = 100.0 / colCount;
                        table.ColumnDefinitions.Add(col);
                    }

                    // Her sütun için panel oluştur
                    for (int i = 0; i < columns.Count; i++)
                    {
                        var column = columns[i];
                        var panel = CreateColumnPanel(column);
                        layoutControl1.Controls.Add(panel);

                        var layoutItem = layoutControlGroup1.AddItem();
                        layoutItem.Control = panel;
                        layoutItem.TextVisible = false;
                        layoutItem.OptionsTableLayoutItem.RowIndex = 0;
                        layoutItem.OptionsTableLayoutItem.ColumnIndex = i;
                    }
                }
                finally
                {
                    layoutControl1.EndUpdate();
                }
                
                var totalWorkItems = _context.WorkItems.Count(w => w.Board == "Kanban");
                Logger.Info($"Kanban board yüklendi. {columns.Count} sütun, {totalWorkItems} iş.");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Kanban board yüklenirken hata");
                XtraMessageBox.Show(
                    "Kanban board yüklenirken hata oluştu!\n\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private PanelControl CreateColumnPanel(KanbanColumnSetting columnSetting)
        {
            var panel = new PanelControl();
            panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panel.AllowDrop = true;
            panel.Tag = columnSetting.ColumnName;
            panel.BackColor = SystemColors.Control;

            // Başlık paneli - minimal (Scrum teması)
            var headerPanel = new PanelControl();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 35;
            headerPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;

            // İş kartlarını yükle
            var query = _context.WorkItems
                .Include(w => w.Activities)
                .Where(w => w.Board == "Kanban" && w.Status == columnSetting.ColumnName);

            // Arama filtresi uygula (ID veya başlık)
            if (!string.IsNullOrWhiteSpace(_currentSearch))
            {
                var search = _currentSearch.Trim();
                int id;
                bool isId = int.TryParse(search.TrimStart('#'), out id);

                if (isId)
                {
                    query = query.Where(w => w.Id == id);
                }
                else
                {
                    query = query.Where(w => w.Title.Contains(search));
                }
            }

            // Filtre: Çözüldü sütununda 2 haftadan eski işleri gizle
            if (columnSetting.ColumnName == "Cozuldu")
            {
                var twoWeeksAgo = DateTime.Now.AddDays(-14);
                query = query.Where(w => !w.CompletedAt.HasValue || w.CompletedAt >= twoWeeksAgo);
            }

            var workItems = query
                .OrderBy(w => w.OrderIndex)
                .ToList();

            var currentCount = workItems.Count;

            // Başlık - compact (Scrum teması)
            var lblHeader = new LabelControl();
            var countText = columnSetting.WipLimit.HasValue ? 
                $"{currentCount}/{columnSetting.WipLimit}" : 
                $"{currentCount}";
            lblHeader.Text = $"{GetColumnDisplayName(columnSetting.ColumnName)} ({countText})";
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
            scroll.Tag = columnSetting.ColumnName;
            scroll.BackColor = SystemColors.Control;
            scroll.AllowDrop = true;
            panel.Controls.Add(scroll);

            int yPos = 5;
            int orderNumber = 1; // Sıra numarası sayacı
            foreach (var workItem in workItems)
            {
                var card = CreateWorkItemCard(workItem, orderNumber);
                card.Location = new Point(5, yPos);
                card.Width = Math.Max(220, panel.Width - 15);
                scroll.Controls.Add(card);
                yPos += card.Height + 5;
                orderNumber++;
            }

            // Boyut değişince kart genişliklerini güncelle
            scroll.Resize += (s, e) =>
            {
                var innerWidth = Math.Max(220, scroll.ClientSize.Width - 10);
                foreach (Control ctrl in scroll.Controls)
                {
                    if (ctrl is PanelControl)
                    {
                        ctrl.Width = innerWidth;
                    }
                }
            };

            // Drag & Drop olayları - hem scroll hem de ana panel için
            scroll.DragEnter += Panel_DragEnter;
            scroll.DragOver += Panel_DragOver;
            scroll.DragDrop += Panel_DragDrop;
            scroll.DragLeave += Panel_DragLeave;
            
            panel.DragEnter += Panel_DragEnter;
            panel.DragOver += Panel_DragOver;
            panel.DragDrop += Panel_DragDrop;
            panel.DragLeave += Panel_DragLeave;

            return panel;
        }

        private string GetColumnDisplayName(string columnName)
        {
            switch (columnName)
            {
                case "GelenAcilIsler":
                    return "Gelen Acil İşler";
                case "Sirada":
                    return "Sırada";
                case "MudahaleEdiliyor":
                    return "Müdahale Ediliyor";
                case "DogrulamaBekliyor":
                    return "Doğrulama Bekliyor";
                case "Cozuldu":
                    return "Çözüldü";
                default:
                    return columnName;
            }
        }

        private PanelControl CreateWorkItemCard(WorkItem workItem, int orderNumber = 0)
        {
            // DevExpress PanelControl - Scrum teması ile aynı tasarım
            var card = new PanelControl();
            card.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            card.Size = new Size(220, 110);
            card.Tag = workItem;
            card.AllowDrop = false;
            card.Cursor = Cursors.Hand;

            // Aciliyet göstergesi çizgisi (sol kenar) - Scrum teması
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

            // Sıra numarası (sol üst köşe, aciliyet çizgisinin yanında)
            if (orderNumber > 0)
            {
                var lblOrder = new LabelControl();
                lblOrder.Text = $"{orderNumber}.";
                lblOrder.Location = new Point(10, 8);
                lblOrder.AutoSizeMode = LabelAutoSizeMode.None;
                lblOrder.Size = new Size(20, 14);
                lblOrder.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold);
                lblOrder.Appearance.ForeColor = Color.FromArgb(0, 122, 204); // Mavi renk
                lblOrder.ToolTip = "Yapılacaklar sırası - Sürükleyerek değiştirebilirsiniz";
                card.Controls.Add(lblOrder);
            }

            // ID - LabelControl (sıra numarasından sonra)
            var lblId = new LabelControl();
            lblId.Text = $"#{workItem.Id}";
            lblId.Location = new Point(orderNumber > 0 ? 30 : 10, 8);
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

            // Badges - compact (Scrum teması)
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

            // Çalışma süresini hesapla
            if (workItem.Activities != null)
            {
                var activities = workItem.Activities
                    .Where(a => a.ActivityType == "StatusChange")
                    .OrderBy(a => a.CreatedAt)
                    .ToList();

                TimeSpan totalDevTime = TimeSpan.Zero;
                DateTime? devStartTime = null;

                foreach (var activity in activities)
                {
                    if ((activity.NewValue == "Gelistirmede" || activity.NewValue == "MudahaleEdiliyor") && devStartTime == null)
                    {
                        devStartTime = activity.CreatedAt;
                    }
                    else if ((activity.OldValue == "Gelistirmede" || activity.OldValue == "MudahaleEdiliyor") && devStartTime != null)
                    {
                        totalDevTime += activity.CreatedAt - devStartTime.Value;
                        devStartTime = null;
                    }
                }

                if (devStartTime != null)
                {
                    totalDevTime += DateTime.Now - devStartTime.Value;
                }

                // Fallback: Eğer aktivite geçmişinden süre hesaplanamadıysa (eski kayıtlar için)
                // StartedAt ve CompletedAt alanlarını kullan
                if (totalDevTime == TimeSpan.Zero && workItem.StartedAt.HasValue)
                {
                    if (workItem.Status == "Cozuldu" || workItem.Status == "Tamamlandi")
                    {
                        if (workItem.CompletedAt.HasValue)
                            totalDevTime = workItem.CompletedAt.Value - workItem.StartedAt.Value;
                    }
                    else if (workItem.Status == "MudahaleEdiliyor" || workItem.Status == "Gelistirmede")
                    {
                        totalDevTime = DateTime.Now - workItem.StartedAt.Value;
                    }
                }

                if (totalDevTime > TimeSpan.Zero)
                {
                    string timeStr = "";
                    if (totalDevTime.TotalDays >= 1) timeStr = $"{(int)totalDevTime.TotalDays}g ";
                    if (totalDevTime.Hours > 0) timeStr += $"{totalDevTime.Hours}s";
                    if (string.IsNullOrEmpty(timeStr)) timeStr = $"{totalDevTime.Minutes}dk";
                    
                    footerText += (string.IsNullOrEmpty(footerText) ? "" : " | ") + $"⏱️{timeStr.Trim()}";
                }
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

            // İkonlar - sağ alt (Scrum teması)
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

            // Drag & Click olayları
            card.MouseDown += Card_MouseDown;
            card.MouseMove += Card_MouseMove;
            card.MouseUp += Card_MouseUp;
            card.Click += (s, e) => { if (!isDragging) OpenWorkItemDetail(workItem.Id); };

            // Child kontrollere de click ekle
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

        private PanelControl draggedCard = null;
        private bool isDragging = false;
        private bool isMouseDown = false;
        private Point mouseDownPoint;
        private Panel dropIndicator = null; // Bırakılacak yeri gösteren çizgi

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

        private void Panel_DragLeave(object sender, EventArgs e)
        {
            HideDropIndicator();
        }

        private void Panel_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PanelControl)))
            {
                e.Effect = DragDropEffects.Move;
                
                // Bırakılacak yeri gösteren çizgiyi güncelle
                var targetPanel = sender as PanelControl;
                if (targetPanel != null)
                {
                    ShowDropIndicator(targetPanel, e.X, e.Y);
                }
            }
        }

        /// <summary>
        /// Bırakılacak yeri gösteren çizgiyi göster
        /// </summary>
        private void ShowDropIndicator(PanelControl targetPanel, int screenX, int screenY)
        {
            // Önce eski göstergeyi temizle
            HideDropIndicator();

            // Ekran koordinatlarını panel koordinatlarına çevir
            var clientPoint = targetPanel.PointToClient(new Point(screenX, screenY));
            
            // Bırakılacak pozisyonu hesapla
            int dropY = 5;
            foreach (Control ctrl in targetPanel.Controls)
            {
                if (ctrl is PanelControl card && card != draggedCard)
                {
                    int cardMiddle = card.Top + card.Height / 2;
                    if (clientPoint.Y < cardMiddle)
                    {
                        dropY = card.Top - 2;
                        break;
                    }
                    dropY = card.Bottom + 2;
                }
            }

            // Drop indicator oluştur
            dropIndicator = new Panel();
            dropIndicator.BackColor = Color.FromArgb(0, 122, 204); // Mavi çizgi
            dropIndicator.Height = 3;
            dropIndicator.Width = targetPanel.ClientSize.Width - 10;
            dropIndicator.Location = new Point(5, dropY);
            targetPanel.Controls.Add(dropIndicator);
            dropIndicator.BringToFront();
        }

        /// <summary>
        /// Drop göstergesini gizle
        /// </summary>
        private void HideDropIndicator()
        {
            if (dropIndicator != null)
            {
                dropIndicator.Parent?.Controls.Remove(dropIndicator);
                dropIndicator.Dispose();
                dropIndicator = null;
            }
        }

        private void Panel_DragDrop(object sender, DragEventArgs e)
        {
            HideDropIndicator(); // Göstergeyi gizle
            
            var targetPanel = sender as PanelControl;
            if (targetPanel == null || draggedCard == null) return;

            var workItem = draggedCard.Tag as WorkItem;
            if (workItem == null) return;

            var targetColumn = targetPanel.Tag?.ToString();
            if (string.IsNullOrEmpty(targetColumn)) return;

            var sourceColumn = _context.WorkItems.Find(workItem.Id)?.Status;

            // WIP limiti kontrolü
            var columnSetting = _context.KanbanColumnSettings
                .FirstOrDefault(c => c.Board == "Kanban" && c.ColumnName == targetColumn);

            if (columnSetting?.WipLimit.HasValue == true && sourceColumn != targetColumn)
            {
                var currentCount = _context.WorkItems.Count(w => w.Board == "Kanban" && w.Status == targetColumn);
                if (currentCount >= columnSetting.WipLimit.Value)
                {
                    XtraMessageBox.Show(
                        $"Bu sütun WIP limitine ulaştı! Limit: {columnSetting.WipLimit.Value}",
                        "WIP Limiti",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    draggedCard = null;
                    return;
                }
            }

            // Bırakılacak pozisyonu hesapla (fare konumuna göre)
            var clientPoint = targetPanel.PointToClient(new Point(e.X, e.Y));
            int targetIndex = CalculateDropIndex(targetPanel, clientPoint.Y, workItem.Id);

            // İş talebini güncelle
            try
            {
                var dbWorkItem = _context.WorkItems.Find(workItem.Id);
                if (dbWorkItem != null)
                {
                    var oldStatus = dbWorkItem.Status;
                    bool isSameColumn = (sourceColumn == targetColumn);

                    if (!isSameColumn)
                    {
                        dbWorkItem.Status = targetColumn;
                    }

                    // Sütun içindeki tüm işleri al ve sırala
                    var columnWorkItems = _context.WorkItems
                        .Where(w => w.Board == "Kanban" && w.Status == targetColumn && w.Id != workItem.Id)
                        .OrderBy(w => w.OrderIndex)
                        .ToList();

                    // Yeni sıra index'ini uygula
                    int newOrder = 0;
                    for (int i = 0; i < columnWorkItems.Count; i++)
                    {
                        if (i == targetIndex)
                        {
                            dbWorkItem.OrderIndex = newOrder;
                            newOrder++;
                        }
                        columnWorkItems[i].OrderIndex = newOrder;
                        newOrder++;
                    }

                    // Eğer en sona eklenmişse
                    if (targetIndex >= columnWorkItems.Count)
                    {
                        dbWorkItem.OrderIndex = newOrder;
                    }
                    
                    // Müdahele Ediliyor durumuna geçildiğinde başlangıç zamanını kaydet
                    if (targetColumn == "MudahaleEdiliyor" && oldStatus != "MudahaleEdiliyor")
                    {
                        if (!dbWorkItem.StartedAt.HasValue)
                        {
                            dbWorkItem.StartedAt = DateTime.Now;
                        }
                    }

                    // Çözüldü durumuna geçildiğinde tamamlanma zamanını kaydet
                    if (targetColumn == "Cozuldu")
                    {
                        dbWorkItem.CompletedAt = DateTime.Now;
                    }
                    else if (oldStatus == "Cozuldu")
                    {
                        dbWorkItem.CompletedAt = null;
                    }

                    _context.SaveChanges();

                    // Aktivite kaydı oluştur (sadece sütun değiştiğinde)
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

                    LoadKanbanBoard();
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
                isMouseDown = false;
            }
        }

        /// <summary>
        /// Fare konumuna göre bırakılacak index'i hesapla
        /// </summary>
        private int CalculateDropIndex(PanelControl targetPanel, int mouseY, int draggedWorkItemId)
        {
            int index = 0;
            var cards = targetPanel.Controls.OfType<PanelControl>()
                .Where(c => c.Tag is WorkItem && ((WorkItem)c.Tag).Id != draggedWorkItemId)
                .OrderBy(c => c.Top)
                .ToList();

            foreach (var card in cards)
            {
                int cardMiddle = card.Top + card.Height / 2;
                if (mouseY < cardMiddle)
                {
                    return index;
                }
                index++;
            }

            return index; // En sona ekle
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadKanbanBoard();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            _currentSearch = txtSearch.Text?.Trim();
            LoadKanbanBoard();
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            _currentSearch = null;
            LoadKanbanBoard();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _currentSearch = txtSearch.Text?.Trim();
                LoadKanbanBoard();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
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
                LoadKanbanBoard();
            }
        }

    }
}

