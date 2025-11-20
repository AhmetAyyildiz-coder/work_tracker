using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Outlook;
using work_tracker.Data.Entities;

namespace work_tracker.Helpers
{
    /// <summary>
    /// Outlook COM Interop helper sınıfı
    /// Email'leri çekmek, bağlamak ve yönetmek için
    /// </summary>
    public static class OutlookHelper
    {
        private static Application _outlookApp;

        /// <summary>
        /// Outlook uygulamasına bağlan
        /// </summary>
        public static Application GetOutlookApplication()
        {
            try
            {
                if (_outlookApp == null)
                {
                    _outlookApp = new Application();
                }
                return _outlookApp;
            }
            catch (COMException ex)
            {
                throw new System.Exception("Outlook'a bağlanılamadı. Outlook'un yüklü ve çalışır durumda olduğundan emin olun.", ex);
            }
        }

        /// <summary>
        /// Outlook'tan email'leri çek (Inbox klasöründen)
        /// </summary>
        public static List<WorkItemEmail> GetEmailsFromOutlook(int maxCount = 50, string searchSubject = null)
        {
            var emails = new List<WorkItemEmail>();

            try
            {
                var outlook = GetOutlookApplication();
                var namespaceObj = outlook.GetNamespace("MAPI");
                var inbox = namespaceObj.GetDefaultFolder(OlDefaultFolders.olFolderInbox);

                // Email'leri al
                var items = inbox.Items;
                
                // Optimizasyon: Sadece son 7 günün maillerini al
                // Not: Tarih formatı sistem ayarlarına göre değişebilir, "g" (genel) formatı genellikle çalışır.
                string filter = "[ReceivedTime] > '" + DateTime.Now.AddDays(-7).ToString("g") + "'";
                
                Items restrictedItems = null;
                try 
                {
                    restrictedItems = items.Restrict(filter);
                    restrictedItems.Sort("[ReceivedTime]", true); // En yeni önce
                }
                catch (System.Exception)
                {
                    // Filtreleme hatası olursa (örn: tarih formatı) tüm kutuya dön
                    restrictedItems = items;
                    restrictedItems.Sort("[ReceivedTime]", true);
                }

                int count = 0;
                foreach (object item in restrictedItems)
                {
                    if (count >= maxCount) break;

                    if (item is MailItem mailItem)
                    {
                        try
                        {
                            // Subject filtresi varsa kontrol et
                            var subject = mailItem.Subject ?? "";
                            if (!string.IsNullOrEmpty(searchSubject) &&
                                !subject.ToLower().Contains(searchSubject.ToLower()))
                            {
                                continue;
                            }

                            var email = ConvertMailItemToWorkItemEmail(mailItem);
                            emails.Add(email);
                            count++;
                        }
                        catch (System.Exception ex)
                        {
                            Logger.Warning($"Email çekilirken hata: {ex.Message}");
                            continue;
                        }
                        finally
                        {
                            Marshal.ReleaseComObject(mailItem);
                        }
                    }
                    else
                    {
                        // MailItem değilse (örn: MeetingItem) kaynağı serbest bırak
                        Marshal.ReleaseComObject(item);
                    }
                }

                // COM objelerini temizle
                if (restrictedItems != null && restrictedItems != items)
                {
                    Marshal.ReleaseComObject(restrictedItems);
                }
                Marshal.ReleaseComObject(items);
                Marshal.ReleaseComObject(inbox);
                Marshal.ReleaseComObject(namespaceObj);
            }
            catch (System.Exception ex)
            {
                Logger.Error("Outlook'tan email çekilirken hata oluştu", ex);
                throw;
            }

            return emails;
        }

        /// <summary>
        /// Outlook MailItem'ı WorkItemEmail'e dönüştür
        /// </summary>
        private static WorkItemEmail ConvertMailItemToWorkItemEmail(MailItem mailItem)
        {
            var email = new WorkItemEmail
            {
                OutlookEntryId = mailItem.EntryID,
                Subject = mailItem.Subject ?? "",
                From = GetEmailAddress(mailItem.SenderEmailAddress, mailItem.SenderName),
                To = FormatRecipients(mailItem.To),
                Cc = FormatRecipients(mailItem.CC),
                Body = mailItem.Body ?? "",
                IsHtml = mailItem.BodyFormat == OlBodyFormat.olFormatHTML,
                ReceivedDate = mailItem.ReceivedTime,
                SentDate = mailItem.SentOn,
                IsRead = mailItem.UnRead == false,
                HasAttachments = mailItem.Attachments.Count > 0,
                AttachmentCount = mailItem.Attachments.Count
            };

            return email;
        }

        /// <summary>
        /// Email adresini formatla
        /// </summary>
        private static string GetEmailAddress(string address, string name)
        {
            if (string.IsNullOrEmpty(address))
                return name ?? "";

            if (string.IsNullOrEmpty(name))
                return address;

            return $"{name} <{address}>";
        }

        /// <summary>
        /// Outlook'un string halindeki To/Cc alanını normalize et
        /// </summary>
        private static string FormatRecipients(string recipients)
        {
            if (string.IsNullOrWhiteSpace(recipients))
                return "";

            // Outlook string'i genelde ; ile ayrılmıştır
            var parts = recipients.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Trim())
                .Where(p => !string.IsNullOrEmpty(p));

            return string.Join("; ", parts);
        }

        /// <summary>
        /// Outlook'ta email'i aç
        /// </summary>
        public static void OpenEmailInOutlook(string entryId)
        {
            try
            {
                var outlook = GetOutlookApplication();
                var namespaceObj = outlook.GetNamespace("MAPI");
                var mailItem = namespaceObj.GetItemFromID(entryId) as MailItem;

                if (mailItem != null)
                {
                    mailItem.Display(false); // false = modal olmayan
                }
                else
                {
                    throw new System.Exception("Email bulunamadı. Outlook'ta silinmiş olabilir.");
                }
            }
            catch (System.Exception ex)
            {
                Logger.Error("Outlook'ta email açılırken hata", ex);
                throw;
            }
        }

        /// <summary>
        /// Outlook'tan belirli bir email'i EntryId ile çek
        /// </summary>
        public static WorkItemEmail GetEmailByEntryId(string entryId)
        {
            try
            {
                var outlook = GetOutlookApplication();
                var namespaceObj = outlook.GetNamespace("MAPI");
                var mailItem = namespaceObj.GetItemFromID(entryId) as MailItem;

                if (mailItem != null)
                {
                    var email = ConvertMailItemToWorkItemEmail(mailItem);
                    Marshal.ReleaseComObject(mailItem);
                    return email;
                }

                return null;
            }
            catch (System.Exception ex)
            {
                Logger.Error($"EntryId ile email çekilirken hata: {entryId}", ex);
                return null;
            }
        }

        /// <summary>
        /// Outlook bağlantısını kapat
        /// </summary>
        public static void Dispose()
        {
            try
            {
                if (_outlookApp != null)
                {
                    Marshal.ReleaseComObject(_outlookApp);
                    _outlookApp = null;
                }
            }
            catch
            {
                // Sessizce devam et
            }
        }
    }
}

