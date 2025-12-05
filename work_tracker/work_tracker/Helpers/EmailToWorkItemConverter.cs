using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using work_tracker.Data.Entities;

namespace work_tracker.Helpers
{
    /// <summary>
    /// Email'leri otomatik olarak WorkItem formatına dönüştüren yardımcı sınıf
    /// </summary>
    public static class EmailToWorkItemConverter
    {
        #region Dönüşüm Kuralları

        /// <summary>
        /// Aciliyet belirleyici anahtar kelimeler ve karşılık gelen değerler
        /// </summary>
        private static readonly Dictionary<string[], string> UrgencyKeywords = new Dictionary<string[], string>
        {
            { new[] { "[ACİL]", "[ACIL]", "[URGENT]", "[KRITIK]", "[KRİTİK]", "ACİL:", "ACIL:", "URGENT:" }, "Kritik" },
            { new[] { "[ÖNEMLİ]", "[ONEMLI]", "[IMPORTANT]", "[YUKSEK]", "[YÜKSEK]", "ÖNEMLİ:", "ONEMLI:" }, "Yüksek" },
            { new[] { "[NORMAL]", "[STANDART]" }, "Normal" },
            { new[] { "[DÜŞÜK]", "[DUSUK]", "[LOW]" }, "Düşük" }
        };

        /// <summary>
        /// İş tipi belirleyici anahtar kelimeler ve karşılık gelen değerler
        /// </summary>
        private static readonly Dictionary<string[], string> TypeKeywords = new Dictionary<string[], string>
        {
            { new[] { "[BUG]", "[HATA]", "[ERROR]", "[SORUN]", "BUG:", "HATA:" }, "Bug" },
            { new[] { "[YENİ]", "[YENI]", "[NEW]", "[ÖZELLİK]", "[OZELLIK]", "[FEATURE]", "YENİ:", "YENI:" }, "YeniOzellik" },
            { new[] { "[DEĞİŞİKLİK]", "[DEGISIKLIK]", "[CHANGE]", "[GÜNCELLEME]", "[GUNCELLEME]" }, "Degisiklik" },
            { new[] { "[ACİL ARGE]", "[ACIL ARGE]", "[ACİLARGE]", "[ACILARGE]" }, "AcilArge" },
            { new[] { "[DESTEK]", "[SUPPORT]", "[YARDIM]" }, "Destek" },
            { new[] { "[ARAŞTIRMA]", "[ARASTIRMA]", "[RESEARCH]", "[ANALİZ]", "[ANALIZ]" }, "Arastirma" },
            { new[] { "[TEST]", "[QA]" }, "Test" },
            { new[] { "[DOKÜMANTASYON]", "[DOKUMANTASYON]", "[DOC]", "[DOCUMENTATION]" }, "Dokumantasyon" }
        };

        /// <summary>
        /// Subject'ten çıkarılacak yaygın prefix'ler
        /// </summary>
        private static readonly string[] SubjectPrefixesToRemove = new[]
        {
            "RE:", "Re:", "re:", "FW:", "Fw:", "fw:", "FWD:", "Fwd:", "fwd:",
            "YNT:", "Ynt:", "ynt:", "İLT:", "İlt:", "ilt:", "ILT:", "Ilt:", "ilt:"
        };

        #endregion

        #region Ana Dönüşüm Metodu

        /// <summary>
        /// WorkItemEmail'i WorkItem'a dönüştürür
        /// </summary>
        /// <param name="email">Dönüştürülecek email</param>
        /// <returns>Oluşturulan WorkItem (Id olmadan, kaydedilmemiş)</returns>
        public static WorkItem ConvertToWorkItem(WorkItemEmail email)
        {
            if (email == null)
                throw new ArgumentNullException(nameof(email));

            var workItem = new WorkItem
            {
                Title = ExtractTitle(email.Subject),
                Description = ExtractDescription(email),
                RequestedBy = ExtractRequestedBy(email.From),
                RequestedAt = email.ReceivedDate ?? email.SentDate ?? DateTime.Now,
                CreatedAt = DateTime.Now,
                Board = "Inbox",
                Status = "Bekliyor",
                Urgency = DetectUrgency(email.Subject, email.Body),
                Type = DetectType(email.Subject, email.Body)
            };

            return workItem;
        }

        /// <summary>
        /// WorkItemEmail'i WorkItem'a dönüştürür ve email'i de bağlar
        /// </summary>
        /// <param name="email">Dönüştürülecek email</param>
        /// <returns>Email bağlı WorkItem</returns>
        public static WorkItem ConvertToWorkItemWithEmail(WorkItemEmail email)
        {
            var workItem = ConvertToWorkItem(email);
            
            // Email'i WorkItem'a bağla (Id'ler kaydedildikten sonra set edilecek)
            workItem.Emails.Add(email);
            
            return workItem;
        }

        #endregion

        #region Extraction Metodları

        /// <summary>
        /// Email subject'inden temiz bir başlık çıkarır
        /// </summary>
        public static string ExtractTitle(string subject)
        {
            if (string.IsNullOrWhiteSpace(subject))
                return "(Konusuz Mail)";

            var title = subject.Trim();

            // RE:, FW:, YNT: gibi prefix'leri temizle
            foreach (var prefix in SubjectPrefixesToRemove)
            {
                while (title.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    title = title.Substring(prefix.Length).TrimStart();
                }
            }

            // Urgency ve Type tag'lerini temizle (başlıkta tutmayacağız)
            foreach (var keywords in UrgencyKeywords.Keys.Concat(TypeKeywords.Keys))
            {
                foreach (var keyword in keywords)
                {
                    if (title.StartsWith(keyword, StringComparison.OrdinalIgnoreCase))
                    {
                        title = title.Substring(keyword.Length).TrimStart();
                    }
                }
            }

            // Boşsa varsayılan değer
            if (string.IsNullOrWhiteSpace(title))
                title = "(Konusuz Mail)";

            // Max 500 karakter (WorkItem.Title MaxLength)
            if (title.Length > 500)
                title = title.Substring(0, 497) + "...";

            return title;
        }

        /// <summary>
        /// Email'den açıklama metni oluşturur
        /// </summary>
        public static string ExtractDescription(WorkItemEmail email)
        {
            var description = new System.Text.StringBuilder();

            // Mail bilgilerini başa ekle
            description.AppendLine("📧 **Email'den Oluşturuldu**");
            description.AppendLine("---");
            
            if (!string.IsNullOrWhiteSpace(email.From))
                description.AppendLine($"**Gönderen:** {email.From}");
            
            if (!string.IsNullOrWhiteSpace(email.To))
                description.AppendLine($"**Alıcı:** {email.To}");
            
            if (!string.IsNullOrWhiteSpace(email.Cc))
                description.AppendLine($"**CC:** {email.Cc}");
            
            if (email.ReceivedDate.HasValue)
                description.AppendLine($"**Tarih:** {email.ReceivedDate.Value:dd.MM.yyyy HH:mm}");
            
            if (!string.IsNullOrWhiteSpace(email.Subject))
                description.AppendLine($"**Konu:** {email.Subject}");
            
            description.AppendLine();
            description.AppendLine("---");
            description.AppendLine();
            description.AppendLine("**Mail İçeriği:**");
            description.AppendLine();

            // Mail body'sini ekle (temizlenmiş)
            var body = CleanEmailBody(email.Body, email.IsHtml);
            description.Append(body);

            // Ek varsa belirt
            if (email.HasAttachments && email.AttachmentCount > 0)
            {
                description.AppendLine();
                description.AppendLine();
                description.AppendLine($"📎 *{email.AttachmentCount} adet ek dosya mevcut*");
            }

            return description.ToString();
        }

        /// <summary>
        /// From alanından göndereni çıkarır
        /// </summary>
        public static string ExtractRequestedBy(string from)
        {
            if (string.IsNullOrWhiteSpace(from))
                return "Bilinmeyen Gönderen";

            // "Ad Soyad <email@domain.com>" formatından ismi çıkar
            var match = Regex.Match(from, @"^(.+?)\s*<.+>$");
            if (match.Success)
            {
                return match.Groups[1].Value.Trim().Trim('"');
            }

            // Sadece email varsa domain'den önce kısmı al
            match = Regex.Match(from, @"^([^@]+)@");
            if (match.Success)
            {
                var name = match.Groups[1].Value;
                // Nokta ile ayrılmış isimleri düzelt (ad.soyad -> Ad Soyad)
                name = Regex.Replace(name, @"\.", " ");
                return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
            }

            return from.Trim();
        }

        #endregion

        #region Detection Metodları

        /// <summary>
        /// Email içeriğinden aciliyet seviyesini tespit eder
        /// </summary>
        public static string DetectUrgency(string subject, string body)
        {
            var textToSearch = $"{subject} {body}".ToUpperInvariant();

            foreach (var kvp in UrgencyKeywords)
            {
                foreach (var keyword in kvp.Key)
                {
                    if (textToSearch.Contains(keyword.ToUpperInvariant().Trim('[', ']', ':')))
                    {
                        return kvp.Value;
                    }
                }
            }

            return "Normal"; // Varsayılan
        }

        /// <summary>
        /// Email içeriğinden iş tipini tespit eder
        /// </summary>
        public static string DetectType(string subject, string body)
        {
            var textToSearch = $"{subject} {body}".ToUpperInvariant();

            foreach (var kvp in TypeKeywords)
            {
                foreach (var keyword in kvp.Key)
                {
                    if (textToSearch.Contains(keyword.ToUpperInvariant().Trim('[', ']', ':')))
                    {
                        return kvp.Value;
                    }
                }
            }

            return null; // Tip tespit edilemedi
        }

        /// <summary>
        /// Email içeriğinden proje tahmin etmeye çalışır (geliştirilecek)
        /// </summary>
        public static string DetectProjectHint(string subject, string body)
        {
            // İleride proje keyword'leri eklenebilir
            // Örn: [PROJE-X], [MODÜL-Y] gibi tag'ler
            return null;
        }

        #endregion

        #region Yardımcı Metodlar

        /// <summary>
        /// Email body'sini temizler (HTML tag'leri, gereksiz boşluklar vb.)
        /// </summary>
        public static string CleanEmailBody(string body, bool isHtml)
        {
            if (string.IsNullOrWhiteSpace(body))
                return "";

            var cleaned = body;

            if (isHtml)
            {
                // HTML tag'lerini temizle
                cleaned = Regex.Replace(cleaned, @"<style[^>]*>[\s\S]*?</style>", "", RegexOptions.IgnoreCase);
                cleaned = Regex.Replace(cleaned, @"<script[^>]*>[\s\S]*?</script>", "", RegexOptions.IgnoreCase);
                cleaned = Regex.Replace(cleaned, @"<[^>]+>", " ");
                cleaned = System.Net.WebUtility.HtmlDecode(cleaned);
            }

            // Fazla boşlukları temizle
            cleaned = Regex.Replace(cleaned, @"[ \t]+", " ");
            cleaned = Regex.Replace(cleaned, @"(\r?\n){3,}", "\n\n");
            cleaned = cleaned.Trim();

            // Signature/imza bölümünü ayır (isteğe bağlı)
            var signaturePatterns = new[]
            {
                @"^--\s*$",
                @"^_{3,}$",
                @"^-{3,}$",
                @"^Saygılarımla",
                @"^Best regards",
                @"^Kind regards",
                @"^Sent from"
            };

            foreach (var pattern in signaturePatterns)
            {
                var match = Regex.Match(cleaned, pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);
                if (match.Success && match.Index > cleaned.Length / 3)
                {
                    // İmza bölümünü ayır ama tamamen silme
                    var beforeSignature = cleaned.Substring(0, match.Index).TrimEnd();
                    var signature = cleaned.Substring(match.Index);
                    cleaned = beforeSignature + "\n\n---\n*İmza:*\n" + signature;
                    break;
                }
            }

            return cleaned;
        }

        /// <summary>
        /// Dönüşüm önizlemesi oluşturur
        /// </summary>
        public static ConversionPreview PreviewConversion(WorkItemEmail email)
        {
            var workItem = ConvertToWorkItem(email);
            
            return new ConversionPreview
            {
                OriginalSubject = email.Subject,
                OriginalFrom = email.From,
                ExtractedTitle = workItem.Title,
                ExtractedRequestedBy = workItem.RequestedBy,
                DetectedUrgency = workItem.Urgency,
                DetectedType = workItem.Type,
                DescriptionPreview = workItem.Description?.Length > 500 
                    ? workItem.Description.Substring(0, 500) + "..." 
                    : workItem.Description
            };
        }

        #endregion
    }

    /// <summary>
    /// Dönüşüm önizleme modeli
    /// </summary>
    public class ConversionPreview
    {
        public string OriginalSubject { get; set; }
        public string OriginalFrom { get; set; }
        public string ExtractedTitle { get; set; }
        public string ExtractedRequestedBy { get; set; }
        public string DetectedUrgency { get; set; }
        public string DetectedType { get; set; }
        public string DescriptionPreview { get; set; }
    }
}
