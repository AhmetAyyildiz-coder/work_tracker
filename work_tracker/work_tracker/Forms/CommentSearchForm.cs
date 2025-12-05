using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using work_tracker.Data;
using work_tracker.Data.Entities;
using work_tracker.Helpers;

namespace work_tracker.Forms
{
    /// <summary>
    /// Yorum, not ve açıklamalarda arama yapan form
    /// </summary>
    public partial class CommentSearchForm : XtraForm
    {
        private WorkTrackerDbContext _context;

        public CommentSearchForm()
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
        }

        private void CommentSearchForm_Load(object sender, EventArgs e)
        {
            // Varsayılan olarak tüm checkbox'lar işaretli
            chkDescription.Checked = true;
            chkActivities.Checked = true;
            chkEmailNotes.Checked = true;
            chkEmailBody.Checked = false; // Email body büyük olabilir, varsayılan kapalı

            // Grid ayarları
            SetupGrid();

            // Arama textbox'ına odaklan
            txtSearch.Focus();
        }

        private void SetupGrid()
        {
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.RowAutoHeight = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsSelection.EnableAppearanceFocusedRow = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformSearch();
                e.Handled = true;
            }
        }

        private void PerformSearch()
        {
            var searchText = txtSearch.Text?.Trim();

            if (string.IsNullOrEmpty(searchText) || searchText.Length < 2)
            {
                XtraMessageBox.Show("Lütfen en az 2 karakter girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                var results = new List<SearchResult>();
                var searchLower = searchText.ToLower();

                // 1. İş Açıklamalarında Ara
                if (chkDescription.Checked)
                {
                    var descResults = _context.WorkItems
                        .Where(w => !w.IsArchived && w.Description != null && w.Description.ToLower().Contains(searchLower))
                        .Select(w => new
                        {
                            w.Id,
                            w.Title,
                            w.Description,
                            w.Status,
                            w.Board,
                            ProjectName = w.Project != null ? w.Project.Name : ""
                        })
                        .ToList()
                        .Select(w => new SearchResult
                        {
                            WorkItemId = w.Id,
                            WorkItemTitle = w.Title,
                            SourceType = "İş Açıklaması",
                            MatchedText = ExtractMatchContext(w.Description, searchText, 100),
                            Status = w.Status,
                            Board = w.Board,
                            ProjectName = w.ProjectName,
                            Date = null
                        });

                    results.AddRange(descResults);
                }

                // 2. Yorumlarda (Activities) Ara
                if (chkActivities.Checked)
                {
                    var activityResults = _context.WorkItemActivities
                        .Include(a => a.WorkItem)
                        .Include(a => a.WorkItem.Project)
                        .Where(a => a.ActivityType == WorkItemActivityTypes.Comment 
                                 && a.Description != null 
                                 && a.Description.ToLower().Contains(searchLower)
                                 && !a.WorkItem.IsArchived)
                        .Select(a => new
                        {
                            a.WorkItemId,
                            WorkItemTitle = a.WorkItem.Title,
                            a.Description,
                            a.CreatedAt,
                            a.CreatedBy,
                            Status = a.WorkItem.Status,
                            Board = a.WorkItem.Board,
                            ProjectName = a.WorkItem.Project != null ? a.WorkItem.Project.Name : ""
                        })
                        .ToList()
                        .Select(a => new SearchResult
                        {
                            WorkItemId = a.WorkItemId,
                            WorkItemTitle = a.WorkItemTitle,
                            SourceType = "Yorum",
                            MatchedText = ExtractMatchContext(a.Description, searchText, 100),
                            Author = a.CreatedBy,
                            Status = a.Status,
                            Board = a.Board,
                            ProjectName = a.ProjectName,
                            Date = a.CreatedAt
                        });

                    results.AddRange(activityResults);
                }

                // 3. Email Notlarında Ara
                if (chkEmailNotes.Checked)
                {
                    var emailNoteResults = _context.WorkItemEmails
                        .Include(e => e.WorkItem)
                        .Include(e => e.WorkItem.Project)
                        .Where(e => e.WorkItemId != null 
                                 && e.Notes != null 
                                 && e.Notes.ToLower().Contains(searchLower)
                                 && !e.WorkItem.IsArchived)
                        .Select(e => new
                        {
                            WorkItemId = e.WorkItemId.Value,
                            WorkItemTitle = e.WorkItem.Title,
                            e.Notes,
                            e.Subject,
                            e.LinkedAt,
                            e.LinkedBy,
                            Status = e.WorkItem.Status,
                            Board = e.WorkItem.Board,
                            ProjectName = e.WorkItem.Project != null ? e.WorkItem.Project.Name : ""
                        })
                        .ToList()
                        .Select(e => new SearchResult
                        {
                            WorkItemId = e.WorkItemId,
                            WorkItemTitle = e.WorkItemTitle,
                            SourceType = "Email Notu",
                            MatchedText = ExtractMatchContext(e.Notes, searchText, 100),
                            Author = e.LinkedBy,
                            AdditionalInfo = $"Email: {e.Subject}",
                            Status = e.Status,
                            Board = e.Board,
                            ProjectName = e.ProjectName,
                            Date = e.LinkedAt
                        });

                    results.AddRange(emailNoteResults);
                }

                // 4. Email İçeriklerinde Ara
                if (chkEmailBody.Checked)
                {
                    var emailBodyResults = _context.WorkItemEmails
                        .Include(e => e.WorkItem)
                        .Include(e => e.WorkItem.Project)
                        .Where(e => e.WorkItemId != null 
                                 && e.Body != null 
                                 && e.Body.ToLower().Contains(searchLower)
                                 && !e.WorkItem.IsArchived)
                        .Select(e => new
                        {
                            WorkItemId = e.WorkItemId.Value,
                            WorkItemTitle = e.WorkItem.Title,
                            e.Body,
                            e.Subject,
                            e.ReceivedDate,
                            e.From,
                            Status = e.WorkItem.Status,
                            Board = e.WorkItem.Board,
                            ProjectName = e.WorkItem.Project != null ? e.WorkItem.Project.Name : ""
                        })
                        .ToList()
                        .Select(e => new SearchResult
                        {
                            WorkItemId = e.WorkItemId,
                            WorkItemTitle = e.WorkItemTitle,
                            SourceType = "Email İçeriği",
                            MatchedText = ExtractMatchContext(e.Body, searchText, 100),
                            Author = e.From,
                            AdditionalInfo = $"Email: {e.Subject}",
                            Status = e.Status,
                            Board = e.Board,
                            ProjectName = e.ProjectName,
                            Date = e.ReceivedDate
                        });

                    results.AddRange(emailBodyResults);
                }

                // Sonuçları tarihe göre sırala (en yeni önce)
                var sortedResults = results
                    .OrderByDescending(r => r.Date ?? DateTime.MinValue)
                    .ThenBy(r => r.WorkItemId)
                    .ToList();

                // Grid'e bağla
                gridControl1.DataSource = sortedResults;

                // Kolon ayarları
                ConfigureGridColumns();

                // Sonuç sayısını göster
                lblResultCount.Text = $"📊 {sortedResults.Count} sonuç bulundu";

                if (sortedResults.Count == 0)
                {
                    XtraMessageBox.Show($"'{searchText}' için sonuç bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Logger.Info($"Yorum araması: '{searchText}' - {sortedResults.Count} sonuç");
            }
            catch (Exception ex)
            {
                Logger.Error("Yorum arama hatası", ex);
                XtraMessageBox.Show($"Arama sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ConfigureGridColumns()
        {
            if (gridView1.Columns.Count == 0) return;

            // Kolon başlıkları
            if (gridView1.Columns["WorkItemId"] != null)
            {
                gridView1.Columns["WorkItemId"].Caption = "İş ID";
                gridView1.Columns["WorkItemId"].Width = 60;
            }
            if (gridView1.Columns["WorkItemTitle"] != null)
            {
                gridView1.Columns["WorkItemTitle"].Caption = "İş Başlığı";
                gridView1.Columns["WorkItemTitle"].Width = 200;
            }
            if (gridView1.Columns["SourceType"] != null)
            {
                gridView1.Columns["SourceType"].Caption = "Kaynak";
                gridView1.Columns["SourceType"].Width = 100;
            }
            if (gridView1.Columns["MatchedText"] != null)
            {
                gridView1.Columns["MatchedText"].Caption = "Eşleşen Metin";
                gridView1.Columns["MatchedText"].Width = 300;
            }
            if (gridView1.Columns["Author"] != null)
            {
                gridView1.Columns["Author"].Caption = "Yazar";
                gridView1.Columns["Author"].Width = 120;
            }
            if (gridView1.Columns["Date"] != null)
            {
                gridView1.Columns["Date"].Caption = "Tarih";
                gridView1.Columns["Date"].Width = 120;
                gridView1.Columns["Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView1.Columns["Date"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
            }
            if (gridView1.Columns["Status"] != null)
            {
                gridView1.Columns["Status"].Caption = "Durum";
                gridView1.Columns["Status"].Width = 100;
            }
            if (gridView1.Columns["Board"] != null)
            {
                gridView1.Columns["Board"].Caption = "Pano";
                gridView1.Columns["Board"].Width = 80;
            }
            if (gridView1.Columns["ProjectName"] != null)
            {
                gridView1.Columns["ProjectName"].Caption = "Proje";
                gridView1.Columns["ProjectName"].Width = 100;
            }
            if (gridView1.Columns["AdditionalInfo"] != null)
            {
                gridView1.Columns["AdditionalInfo"].Caption = "Ek Bilgi";
                gridView1.Columns["AdditionalInfo"].Width = 150;
            }

            gridView1.BestFitColumns();
        }

        /// <summary>
        /// Eşleşen metnin etrafından context alır
        /// </summary>
        private string ExtractMatchContext(string text, string searchText, int contextLength)
        {
            if (string.IsNullOrEmpty(text)) return "";

            var index = text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase);
            if (index < 0) return text.Length > contextLength ? text.Substring(0, contextLength) + "..." : text;

            // Eşleşmenin etrafından context al
            var start = Math.Max(0, index - contextLength / 2);
            var end = Math.Min(text.Length, index + searchText.Length + contextLength / 2);

            var context = text.Substring(start, end - start);

            // Başa ve sona ... ekle
            if (start > 0) context = "..." + context;
            if (end < text.Length) context = context + "...";

            // Satır sonlarını temizle
            context = context.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " ");

            return context;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            gridControl1.DataSource = null;
            lblResultCount.Text = "";
            txtSearch.Focus();
        }

        private void btnGoToWorkItem_Click(object sender, EventArgs e)
        {
            NavigateToSelectedWorkItem();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            NavigateToSelectedWorkItem();
        }

        private void NavigateToSelectedWorkItem()
        {
            if (gridView1.FocusedRowHandle < 0)
            {
                XtraMessageBox.Show("Lütfen bir sonuç seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var workItemId = (int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "WorkItemId");

            try
            {
                // WorkItemEditForm'u aç
                var editForm = new WorkItemEditForm(workItemId);
                editForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.Error($"WorkItem açma hatası - ID: {workItemId}", ex);
                XtraMessageBox.Show($"İş açılırken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _context?.Dispose();
        }
    }

    /// <summary>
    /// Arama sonucu modeli
    /// </summary>
    public class SearchResult
    {
        public int WorkItemId { get; set; }
        public string WorkItemTitle { get; set; }
        public string SourceType { get; set; }
        public string MatchedText { get; set; }
        public string Author { get; set; }
        public string AdditionalInfo { get; set; }
        public string Status { get; set; }
        public string Board { get; set; }
        public string ProjectName { get; set; }
        public DateTime? Date { get; set; }
    }
}
