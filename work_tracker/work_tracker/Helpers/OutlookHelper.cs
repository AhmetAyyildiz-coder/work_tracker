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
                ConversationId = mailItem.ConversationID, // Mail taşınsa bile sabit kalır!
                LastKnownFolder = GetFolderPath(mailItem),
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
        /// Mail'in bulunduğu klasör yolunu al
        /// </summary>
        private static string GetFolderPath(MailItem mailItem)
        {
            try
            {
                var folder = mailItem.Parent as MAPIFolder;
                return folder?.FolderPath ?? "";
            }
            catch
            {
                return "";
            }
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
        /// Outlook'ta email'i aç - önce EntryId ile dener, bulamazsa ConversationId ile arar
        /// </summary>
        public static void OpenEmailInOutlook(string entryId, string conversationId = null)
        {
            try
            {
                var outlook = GetOutlookApplication();
                var namespaceObj = outlook.GetNamespace("MAPI");
                MailItem mailItem = null;

                // 1. Önce EntryId ile dene (en hızlı yol)
                try
                {
                    mailItem = namespaceObj.GetItemFromID(entryId) as MailItem;
                }
                catch
                {
                    // EntryId ile bulunamadı - mail taşınmış olabilir
                    mailItem = null;
                }

                // 2. EntryId ile bulunamadıysa ve ConversationId varsa, tüm klasörlerde ara
                if (mailItem == null && !string.IsNullOrEmpty(conversationId))
                {
                    Logger.Info($"Mail EntryId ile bulunamadı, ConversationId ile aranıyor: {conversationId}");
                    mailItem = FindMailByConversationId(namespaceObj, conversationId);
                }

                if (mailItem != null)
                {
                    mailItem.Display(false); // false = modal olmayan
                }
                else
                {
                    throw new System.Exception("Email bulunamadı. Outlook'ta silinmiş veya arşivlenmiş olabilir.");
                }
            }
            catch (System.Exception ex)
            {
                Logger.Error("Outlook'ta email açılırken hata", ex);
                throw;
            }
        }

        /// <summary>
        /// ConversationId ile tüm klasörlerde mail arar
        /// </summary>
        private static MailItem FindMailByConversationId(NameSpace namespaceObj, string conversationId)
        {
            try
            {
                // Önce Inbox ve yaygın klasörlerde ara
                var foldersToSearch = new List<MAPIFolder>
                {
                    namespaceObj.GetDefaultFolder(OlDefaultFolders.olFolderInbox),
                    namespaceObj.GetDefaultFolder(OlDefaultFolders.olFolderSentMail),
                    namespaceObj.GetDefaultFolder(OlDefaultFolders.olFolderDeletedItems)
                };

                // Inbox'ın alt klasörlerini de ekle
                var inbox = namespaceObj.GetDefaultFolder(OlDefaultFolders.olFolderInbox);
                foreach (MAPIFolder subfolder in inbox.Folders)
                {
                    foldersToSearch.Add(subfolder);
                }

                foreach (var folder in foldersToSearch)
                {
                    try
                    {
                        var items = folder.Items;
                        // ConversationID ile filtrele
                        string filter = $"@SQL=\"http://schemas.microsoft.com/mapi/proptag/0x00710102\" = '{conversationId}'";
                        
                        // Alternatif basit arama
                        foreach (object item in items)
                        {
                            if (item is MailItem mail)
                            {
                                if (mail.ConversationID == conversationId)
                                {
                                    return mail;
                                }
                                Marshal.ReleaseComObject(mail);
                            }
                            else
                            {
                                Marshal.ReleaseComObject(item);
                            }
                        }
                        Marshal.ReleaseComObject(items);
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Warning($"Klasör aramasında hata: {folder.Name} - {ex.Message}");
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.Error("ConversationId ile arama hatası", ex);
            }

            return null;
        }

        /// <summary>
        /// Outlook'tan belirli bir email'i EntryId veya ConversationId ile çek
        /// </summary>
        public static WorkItemEmail GetEmailByEntryId(string entryId, string conversationId = null)
        {
            try
            {
                var outlook = GetOutlookApplication();
                var namespaceObj = outlook.GetNamespace("MAPI");
                MailItem mailItem = null;

                // 1. Önce EntryId ile dene
                try
                {
                    mailItem = namespaceObj.GetItemFromID(entryId) as MailItem;
                }
                catch
                {
                    mailItem = null;
                }

                // 2. Bulunamadıysa ConversationId ile ara
                if (mailItem == null && !string.IsNullOrEmpty(conversationId))
                {
                    mailItem = FindMailByConversationId(namespaceObj, conversationId);
                }

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
                Logger.Error($"Email çekilirken hata - EntryId: {entryId}, ConversationId: {conversationId}", ex);
                return null;
            }
        }

        /// <summary>
        /// Kayıtlı email'in EntryId'sini güncelle (mail taşındıysa yeni EntryId'yi al)
        /// </summary>
        public static string UpdateEntryIdIfMoved(string oldEntryId, string conversationId)
        {
            if (string.IsNullOrEmpty(conversationId))
                return oldEntryId;

            try
            {
                var outlook = GetOutlookApplication();
                var namespaceObj = outlook.GetNamespace("MAPI");

                // Önce eski EntryId hala geçerli mi kontrol et
                try
                {
                    var mailItem = namespaceObj.GetItemFromID(oldEntryId) as MailItem;
                    if (mailItem != null)
                    {
                        Marshal.ReleaseComObject(mailItem);
                        return oldEntryId; // Hala geçerli
                    }
                }
                catch { }

                // Geçerli değilse ConversationId ile bul ve yeni EntryId'yi döndür
                var foundMail = FindMailByConversationId(namespaceObj, conversationId);
                if (foundMail != null)
                {
                    var newEntryId = foundMail.EntryID;
                    Marshal.ReleaseComObject(foundMail);
                    Logger.Info($"Mail taşınmış, yeni EntryId alındı. Eski: {oldEntryId?.Substring(0, 20)}..., Yeni: {newEntryId?.Substring(0, 20)}...");
                    return newEntryId;
                }
            }
            catch (System.Exception ex)
            {
                Logger.Error("EntryId güncelleme hatası", ex);
            }

            return oldEntryId;
        }

        /// <summary>
        /// Mevcut email kayıtlarının ConversationId'lerini Outlook'tan çekerek güncelle
        /// Bu metod, eski kayıtları yeni sisteme migrate etmek için kullanılır
        /// </summary>
        public static int MigrateExistingEmails(List<WorkItemEmail> emails)
        {
            int updatedCount = 0;
            
            try
            {
                var outlook = GetOutlookApplication();
                var namespaceObj = outlook.GetNamespace("MAPI");

                foreach (var email in emails)
                {
                    // Zaten ConversationId varsa atla
                    if (!string.IsNullOrEmpty(email.ConversationId))
                        continue;

                    // EntryId yoksa atla
                    if (string.IsNullOrEmpty(email.OutlookEntryId))
                        continue;

                    try
                    {
                        var mailItem = namespaceObj.GetItemFromID(email.OutlookEntryId) as MailItem;
                        if (mailItem != null)
                        {
                            email.ConversationId = mailItem.ConversationID;
                            email.LastKnownFolder = GetFolderPath(mailItem);
                            updatedCount++;
                            Marshal.ReleaseComObject(mailItem);
                            Logger.Info($"Email migrated: {email.Subject?.Substring(0, Math.Min(30, email.Subject?.Length ?? 0))}...");
                        }
                    }
                    catch (System.Exception ex)
                    {
                        // Bu mail artık bulunamıyor - muhtemelen silinmiş veya taşınmış
                        Logger.Warning($"Email migrate edilemedi (muhtemelen taşınmış/silinmiş): {email.Subject} - {ex.Message}");
                    }
                }

                Marshal.ReleaseComObject(namespaceObj);
            }
            catch (System.Exception ex)
            {
                Logger.Error("Email migration hatası", ex);
            }

            return updatedCount;
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

