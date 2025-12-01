using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using work_tracker.Forms;

namespace work_tracker.Helpers
{
    /// <summary>
    /// Metin içindeki referansları (iş numaraları, wiki sayfaları vb.) 
    /// otomatik olarak tıklanabilir linklere dönüştürür.
    /// </summary>
    public static class HyperlinkHelper
    {
        // Pattern'ler
        private static readonly Regex WorkItemPattern = new Regex(@"#(\d+)", RegexOptions.Compiled);
        private static readonly Regex WikiPattern = new Regex(@"\[\[([^\]]+)\]\]", RegexOptions.Compiled);
        private static readonly Regex UrlPattern = new Regex(@"(https?://[^\s<>""]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Düz metni HTML formatına dönüştürür ve referansları link yapar.
        /// DevExpress LabelControl veya MemoEdit ile kullanılır (AllowHtmlString = true).
        /// </summary>
        /// <param name="plainText">Düz metin</param>
        /// <returns>HTML formatında metin</returns>
        public static string ConvertToHtml(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            string result = System.Net.WebUtility.HtmlEncode(plainText);

            // #123 formatındaki iş numaralarını linke çevir
            // href="workitem:123" formatında - kendi protokolümüz
            result = WorkItemPattern.Replace(result, match =>
            {
                var id = match.Groups[1].Value;
                return $"<a href=\"workitem:{id}\">#{id}</a>";
            });

            // [[Wiki Sayfası]] formatındaki wiki referanslarını linke çevir
            result = WikiPattern.Replace(result, match =>
            {
                var title = match.Groups[1].Value;
                var encodedTitle = Uri.EscapeDataString(title);
                return $"<a href=\"wiki:{encodedTitle}\">{title}</a>";
            });

            // URL'leri linke çevir
            result = UrlPattern.Replace(result, match =>
            {
                var url = match.Value;
                return $"<a href=\"{url}\">{url}</a>";
            });

            // Satır sonlarını <br> ile değiştir
            result = result.Replace("\r\n", "<br>").Replace("\n", "<br>");

            return result;
        }

        /// <summary>
        /// Tıklanan linki işler ve uygun formu açar.
        /// </summary>
        /// <param name="href">Link href değeri (örn: "workitem:123", "wiki:Mimari")</param>
        /// <param name="parentForm">Parent form (modal açmak için)</param>
        /// <returns>Link işlendiyse true</returns>
        public static bool HandleLinkClick(string href, Form parentForm = null)
        {
            if (string.IsNullOrEmpty(href))
                return false;

            try
            {
                // İş öğesi linki: workitem:123
                if (href.StartsWith("workitem:"))
                {
                    var idStr = href.Substring("workitem:".Length);
                    if (int.TryParse(idStr, out int workItemId))
                    {
                        OpenWorkItemDetail(workItemId, parentForm);
                        return true;
                    }
                }

                // Wiki linki: wiki:Mimari%20Belgesi
                if (href.StartsWith("wiki:"))
                {
                    var title = Uri.UnescapeDataString(href.Substring("wiki:".Length));
                    OpenWikiPage(title, parentForm);
                    return true;
                }

                // Normal URL: tarayıcıda aç
                if (href.StartsWith("http://") || href.StartsWith("https://"))
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = href,
                        UseShellExecute = true
                    });
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Link açılırken hata: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return false;
        }

        /// <summary>
        /// İş öğesi detay formunu açar
        /// </summary>
        private static void OpenWorkItemDetail(int workItemId, Form parentForm)
        {
            var detailForm = new WorkItemDetailForm(workItemId);
            
            if (parentForm != null)
            {
                detailForm.StartPosition = FormStartPosition.CenterParent;
                detailForm.Show(parentForm);
            }
            else
            {
                detailForm.StartPosition = FormStartPosition.CenterScreen;
                detailForm.Show();
            }
        }

        /// <summary>
        /// Wiki sayfasını açar (başlığa göre arama yapar)
        /// </summary>
        private static void OpenWikiPage(string title, Form parentForm)
        {
            var wikiForm = new WikiForm();
            wikiForm.SearchAndNavigate(title);
            
            if (parentForm != null)
            {
                wikiForm.StartPosition = FormStartPosition.CenterParent;
                wikiForm.Show(parentForm);
            }
            else
            {
                wikiForm.StartPosition = FormStartPosition.CenterScreen;
                wikiForm.Show();
            }
        }

        /// <summary>
        /// Metin içindeki iş referanslarını bulur (#123 formatında)
        /// </summary>
        /// <param name="text">Aranacak metin</param>
        /// <returns>Bulunan iş ID'leri</returns>
        public static int[] ExtractWorkItemIds(string text)
        {
            if (string.IsNullOrEmpty(text))
                return Array.Empty<int>();

            var matches = WorkItemPattern.Matches(text);
            var ids = new System.Collections.Generic.List<int>();

            foreach (Match match in matches)
            {
                if (int.TryParse(match.Groups[1].Value, out int id))
                {
                    if (!ids.Contains(id))
                        ids.Add(id);
                }
            }

            return ids.ToArray();
        }

        /// <summary>
        /// Metin içindeki wiki referanslarını bulur ([[Sayfa Adı]] formatında)
        /// </summary>
        /// <param name="text">Aranacak metin</param>
        /// <returns>Bulunan wiki sayfa başlıkları</returns>
        public static string[] ExtractWikiReferences(string text)
        {
            if (string.IsNullOrEmpty(text))
                return Array.Empty<string>();

            var matches = WikiPattern.Matches(text);
            var titles = new System.Collections.Generic.List<string>();

            foreach (Match match in matches)
            {
                var title = match.Groups[1].Value;
                if (!titles.Contains(title))
                    titles.Add(title);
            }

            return titles.ToArray();
        }
    }
}
