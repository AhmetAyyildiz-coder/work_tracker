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
        /// Mail araması yapılacak özel klasör isimleri (Inbox altında)
        /// Mail taşındığında bu klasörlerde aranır
        /// </summary>
        private static readonly string[] SearchFolderNames = new[]
        {
            // Türkçe klasör isimleri
            "Yapıldı", "Yapildi", "İşlendi", "Islendi", "Tamamlandı", "Tamamlandi","Patika",
            "Bitti", "Kapatıldı", "Kapatildi", "Arşiv", "Arsiv",
            // İngilizce klasör isimleri
            "Done", "Completed", "Processed", "Finished", "Closed", "Archive",
            // Genel klasörler
            "Work", "İş", "Is", "Projeler", "Projects"
        };

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
        public static List<WorkItemEmail> GetEmailsFromOutlook(int maxCount = 50, string searchSubject = null, int daysBack = 60)
        {
            var emails = new List<WorkItemEmail>();

            try
            {
                var outlook = GetOutlookApplication();
                var namespaceObj = outlook.GetNamespace("MAPI");
                var inbox = namespaceObj.GetDefaultFolder(OlDefaultFolders.olFolderInbox);

                // Email'leri al
                var items = inbox.Items;
                
                // Son N günün maillerini al (varsayılan: 60 gün = 2 ay)
                // Outlook DASL filtresi için tarih formatı
                var filterDate = DateTime.Now.AddDays(-daysBack);
                string filter = $"[ReceivedTime] >= '{filterDate:MM/dd/yyyy HH:mm}'";
                
                Items restrictedItems = null;
                try 
                {
                    restrictedItems = items.Restrict(filter);
                    restrictedItems.Sort("[ReceivedTime]", true); // En yeni önce
                    Logger.Info($"📧 Son {daysBack} günün mailleri filtrelendi. Bulunan: {restrictedItems.Count}");
                }
                catch (System.Exception ex)
                {
                    // Filtreleme hatası olursa tüm kutuya dön
                    Logger.Warning($"Mail filtreleme hatası, tüm mailler alınıyor: {ex.Message}");
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
        /// Bulunursa yeni EntryId'yi döndürür (mail taşınmışsa güncelleme için)
        /// </summary>
        public static string OpenEmailInOutlook(string entryId, string conversationId = null)
        {
            try
            {
                var outlook = GetOutlookApplication();
                var namespaceObj = outlook.GetNamespace("MAPI");
                MailItem mailItem = null;
                string newEntryId = entryId;

                // 1. Önce EntryId ile dene (en hızlı yol)
                try
                {
                    Logger.Info($"📧 EntryId ile mail açılmaya çalışılıyor...");
                    mailItem = namespaceObj.GetItemFromID(entryId) as MailItem;
                    if (mailItem != null)
                    {
                        Logger.Info($"✅ EntryId ile bulundu: {mailItem.Subject}");
                    }
                }
                catch
                {
                    // EntryId ile bulunamadı - mail taşınmış olabilir
                    Logger.Info($"⚠️ EntryId ile bulunamadı, mail taşınmış olabilir.");
                    mailItem = null;
                }

                // 2. EntryId ile bulunamadıysa ve ConversationId varsa, tüm klasörlerde ara
                if (mailItem == null && !string.IsNullOrEmpty(conversationId))
                {
                    Logger.Info($"🔍 ConversationId ile aranıyor: {conversationId}");
                    mailItem = FindMailByConversationId(namespaceObj, conversationId);
                    
                    // Mail bulunduysa yeni EntryId'yi kaydet
                    if (mailItem != null)
                    {
                        newEntryId = mailItem.EntryID;
                        Logger.Info($"✅ ConversationId ile bulundu! Yeni EntryId kaydedildi.");
                    }
                }

                if (mailItem != null)
                {
                    mailItem.Display(false); // false = modal olmayan
                    return newEntryId; // Yeni EntryId'yi döndür (güncelleme için)
                }
                else
                {
                    Logger.Error($"❌ Mail bulunamadı! EntryId: {entryId?.Substring(0, Math.Min(20, entryId?.Length ?? 0))}..., ConversationId: {conversationId}");
                    throw new System.Exception("Email bulunamadı. Mail Outlook'ta silinmiş olabilir veya farklı bir hesapta/arşivde olabilir.");
                }
            }
            catch (System.Exception ex)
            {
                Logger.Error("Outlook'ta email açılırken hata", ex);
                throw;
            }
        }

        /// <summary>
        /// ConversationId ile SADECE belirli klasörlerde mail arar (performans için)
        /// Tüm klasör taraması kaldırıldı - sadece statik klasör listesinde arar
        /// </summary>
        private static MailItem FindMailByConversationId(NameSpace namespaceObj, string conversationId)
        {
            try
            {
                Logger.Info($"🔍 Statik klasörlerde mail aranıyor: {conversationId}");
                
                // 1. Önce varsayılan klasörlerde ara (en sık kullanılanlar)
                var defaultFolders = new[]
                {
                    OlDefaultFolders.olFolderInbox,
                    OlDefaultFolders.olFolderSentMail,
                    OlDefaultFolders.olFolderDeletedItems,
                    OlDefaultFolders.olFolderDrafts
                };

                foreach (var folderType in defaultFolders)
                {
                    try
                    {
                        var folder = namespaceObj.GetDefaultFolder(folderType);
                        Logger.Info($"📁 Aranan klasör: {folder.Name}");
                        
                        var result = SearchInFolderFast(folder, conversationId);
                        if (result != null)
                        {
                            Logger.Info($"✅ Mail bulundu: {folder.Name}");
                            return result;
                        }
                        
                        Marshal.ReleaseComObject(folder);
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Warning($"Varsayılan klasör araması hatası: {ex.Message}");
                    }
                }

                // 2. Inbox altındaki özel klasörlerde ara (Yapıldı, Done, vb.)
                try
                {
                    var inbox = namespaceObj.GetDefaultFolder(OlDefaultFolders.olFolderInbox);
                    var result = SearchInCustomFolders(inbox, conversationId);
                    if (result != null)
                    {
                        return result;
                    }
                    Marshal.ReleaseComObject(inbox);
                }
                catch (System.Exception ex)
                {
                    Logger.Warning($"Özel klasör araması hatası: {ex.Message}");
                }

                // 3. Archive klasöründe ara (varsa)
                try
                {
                    var archiveFolder = namespaceObj.GetDefaultFolder((OlDefaultFolders)62);
                    Logger.Info($"📁 Aranan klasör: Archive");
                    var result = SearchInFolderFast(archiveFolder, conversationId);
                    if (result != null)
                    {
                        Logger.Info("✅ Mail Archive'da bulundu");
                        return result;
                    }
                    Marshal.ReleaseComObject(archiveFolder);
                }
                catch { /* Archive klasörü olmayabilir */ }

                Logger.Warning($"⚠️ Mail statik klasörlerde bulunamadı. ConversationId: {conversationId}");
            }
            catch (System.Exception ex)
            {
                Logger.Error("ConversationId ile arama hatası", ex);
            }

            return null;
        }

        /// <summary>
        /// Inbox altındaki özel klasörlerde (Yapıldı, Done, vb.) mail arar
        /// Sadece tek seviye derinlikte arar - recursive değil
        /// </summary>
        private static MailItem SearchInCustomFolders(MAPIFolder parentFolder, string conversationId)
        {
            try
            {
                foreach (MAPIFolder subfolder in parentFolder.Folders)
                {
                    try
                    {
                        var folderName = subfolder.Name;
                        
                        // Sadece tanımlı klasör isimlerinde ara
                        if (SearchFolderNames.Any(name => 
                            folderName.Equals(name, StringComparison.OrdinalIgnoreCase)))
                        {
                            Logger.Info($"📁 Özel klasörde aranıyor: {folderName}");
                            
                            var result = SearchInFolderFast(subfolder, conversationId);
                            if (result != null)
                            {
                                Logger.Info($"✅ Mail bulundu: {folderName}");
                                return result;
                            }
                        }
                    }
                    catch { }
                    finally
                    {
                        try { Marshal.ReleaseComObject(subfolder); } catch { }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.Warning($"Özel klasör taraması hatası: {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// Tek bir klasörde hızlı ConversationId araması yapar (Table API ile)
        /// </summary>
        private static MailItem SearchInFolderFast(MAPIFolder folder, string conversationId)
        {
            try
            {
                // Table API ile hızlı arama (sadece metadata okur)
                var table = folder.GetTable("", OlTableContents.olUserItems);
                table.Columns.RemoveAll();
                table.Columns.Add("EntryID");
                table.Columns.Add("ConversationID");

                while (!table.EndOfTable)
                {
                    var row = table.GetNextRow();
                    try
                    {
                        var rowConvId = row["ConversationID"]?.ToString();
                        if (rowConvId == conversationId)
                        {
                            var entryId = row["EntryID"]?.ToString();
                            var ns = folder.Application.GetNamespace("MAPI");
                            return ns.GetItemFromID(entryId) as MailItem;
                        }
                    }
                    catch { }
                }
            }
            catch (System.Exception ex)
            {
                // Table API çalışmazsa klasik yönteme dön
                Logger.Warning($"Table API hatası ({folder.Name}): {ex.Message}");
                return SearchInFolderClassic(folder, conversationId);
            }

            return null;
        }

        /// <summary>
        /// Tek bir klasörde klasik ConversationId araması (yedek yöntem)
        /// </summary>
        private static MailItem SearchInFolderClassic(MAPIFolder folder, string conversationId)
        {
            try
            {
                var items = folder.Items;
                foreach (object item in items)
                {
                    if (item is MailItem mail)
                    {
                        try
                        {
                            if (mail.ConversationID == conversationId)
                            {
                                return mail;
                            }
                        }
                        catch { }
                        finally
                        {
                            Marshal.ReleaseComObject(mail);
                        }
                    }
                    else
                    {
                        try { Marshal.ReleaseComObject(item); } catch { }
                    }
                }
                Marshal.ReleaseComObject(items);
            }
            catch { }

            return null;
        }

        /// <summary>
        /// Outlook Explorer'ın Search fonksiyonunu kullanarak mail arar
        /// </summary>
        private static MailItem SearchWithExplorer(string conversationId)
        {
            try
            {
                var outlook = GetOutlookApplication();
                var explorer = outlook.ActiveExplorer();
                
                if (explorer == null)
                {
                    // Explorer yoksa Inbox'ı aç
                    var ns = outlook.GetNamespace("MAPI");
                    var inbox = ns.GetDefaultFolder(OlDefaultFolders.olFolderInbox);
                    explorer = inbox.GetExplorer();
                }

                if (explorer != null)
                {
                    // Tüm klasörlerde ara - Outlook'un kendi arama motorunu kullan
                    // conversationid: ile arama yapılamıyor, subject/body ile deneyeceğiz
                    // Bu yüzden bu yöntem yerine AdvancedSearch daha iyi
                }
            }
            catch (System.Exception ex)
            {
                Logger.Warning($"Explorer Search hatası: {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// Outlook AdvancedSearch API kullanarak mail arar
        /// Bu yöntem Outlook'un indekslenmiş aramasını kullanır - en hızlı ve güvenilir
        /// </summary>
        private static MailItem SearchWithAdvancedSearch(NameSpace namespaceObj, string conversationId)
        {
            // Bu metod artık kullanılmıyor - statik klasör araması daha hızlı
            return null;
        }

        /// <summary>
        /// Store içinde Table API kullanarak ConversationID ile mail arar
        /// Bu yöntem artık kullanılmıyor - statik klasör araması yerine geçti
        /// </summary>
        private static MailItem SearchStoreWithTable(Store store, string conversationId)
        {
            // Bu metod artık kullanılmıyor
            return null;
        }

        /// <summary>
        /// Klasörde Table API ile hızlı arama yapar
        /// Bu metod artık kullanılmıyor - SearchInFolderFast yerine geçti
        /// </summary>
        private static MailItem SearchFolderWithTable(MAPIFolder folder, string conversationId, int depth)
        {
            // Bu metod artık kullanılmıyor
            return null;
        }

        /// <summary>
        /// Klasör ve alt klasörlerinde recursive olarak mail arar (KALDIRILDI - performans için)
        /// </summary>
        private static MailItem SearchFolderRecursive(MAPIFolder folder, string conversationId, int depth)
        {
            // Bu metod artık kullanılmıyor - statik klasör listesi kullanılıyor
            return null;
        }

        /// <summary>
        /// Tek bir klasörde ConversationId ile mail arar (eski yöntem)
        /// </summary>
        private static MailItem SearchInFolder(MAPIFolder folder, string conversationId)
        {
            // SearchInFolderClassic ile değiştirildi
            return SearchInFolderClassic(folder, conversationId);
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

        #region Calendar Integration

        /// <summary>
        /// Outlook takviminden toplantıları çek
        /// </summary>
        /// <param name="startDate">Başlangıç tarihi</param>
        /// <param name="endDate">Bitiş tarihi</param>
        /// <returns>Takvim öğeleri listesi</returns>
        public static List<OutlookCalendarItem> GetCalendarItems(DateTime startDate, DateTime endDate)
        {
            var items = new List<OutlookCalendarItem>();

            try
            {
                var outlook = GetOutlookApplication();
                var namespaceObj = outlook.GetNamespace("MAPI");
                var calendar = namespaceObj.GetDefaultFolder(OlDefaultFolders.olFolderCalendar);

                // Tarih filtrelemesi
                string filter = $"[Start] >= '{startDate:g}' AND [Start] <= '{endDate:g}'";
                
                Items calendarItems = null;
                try
                {
                    calendarItems = calendar.Items.Restrict(filter);
                    calendarItems.Sort("[Start]", false);
                    calendarItems.IncludeRecurrences = true;
                }
                catch (System.Exception)
                {
                    calendarItems = calendar.Items;
                    calendarItems.Sort("[Start]", false);
                }

                foreach (object item in calendarItems)
                {
                    if (item is AppointmentItem appointment)
                    {
                        try
                        {
                            // Tarih aralığında mı kontrol et (IncludeRecurrences durumunda)
                            if (appointment.Start >= startDate && appointment.Start <= endDate)
                            {
                                var calendarItem = new OutlookCalendarItem
                                {
                                    EntryId = appointment.EntryID,
                                    Subject = appointment.Subject ?? "(Konusuz)",
                                    Start = appointment.Start,
                                    End = appointment.End,
                                    Location = appointment.Location ?? "",
                                    Organizer = appointment.Organizer ?? "",
                                    RequiredAttendees = appointment.RequiredAttendees ?? "",
                                    OptionalAttendees = appointment.OptionalAttendees ?? "",
                                    IsAllDayEvent = appointment.AllDayEvent,
                                    IsRecurring = appointment.IsRecurring,
                                    Body = appointment.Body ?? "",
                                    BusyStatus = ConvertBusyStatus(appointment.BusyStatus)
                                };

                                items.Add(calendarItem);
                            }
                        }
                        catch (System.Exception ex)
                        {
                            Logger.Warning($"Takvim öğesi işlenirken hata: {ex.Message}");
                        }
                        finally
                        {
                            Marshal.ReleaseComObject(appointment);
                        }
                    }
                    else
                    {
                        Marshal.ReleaseComObject(item);
                    }
                }

                Marshal.ReleaseComObject(calendarItems);
                Marshal.ReleaseComObject(calendar);
                Marshal.ReleaseComObject(namespaceObj);

                Logger.Info($"Outlook'tan {items.Count} takvim öğesi çekildi ({startDate:d} - {endDate:d})");
            }
            catch (System.Exception ex)
            {
                Logger.Error("Outlook takviminden veri çekilirken hata", ex);
                throw;
            }

            return items;
        }

        /// <summary>
        /// Bugünün takvim öğelerini hızlıca çek
        /// </summary>
        public static List<OutlookCalendarItem> GetTodaysCalendarItems()
        {
            var today = DateTime.Today;
            return GetCalendarItems(today, today.AddDays(1).AddSeconds(-1));
        }

        /// <summary>
        /// Bu haftanın takvim öğelerini çek
        /// </summary>
        public static List<OutlookCalendarItem> GetThisWeeksCalendarItems()
        {
            var today = DateTime.Today;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            var endOfWeek = startOfWeek.AddDays(7).AddSeconds(-1);
            return GetCalendarItems(startOfWeek, endOfWeek);
        }

        /// <summary>
        /// Bu ayın takvim öğelerini çek
        /// </summary>
        public static List<OutlookCalendarItem> GetThisMonthsCalendarItems()
        {
            var today = DateTime.Today;
            var startOfMonth = new DateTime(today.Year, today.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddSeconds(-1);
            return GetCalendarItems(startOfMonth, endOfMonth);
        }

        /// <summary>
        /// Outlook meşguliyet durumunu string'e dönüştür
        /// </summary>
        private static string ConvertBusyStatus(OlBusyStatus status)
        {
            switch (status)
            {
                case OlBusyStatus.olFree: return "Müsait";
                case OlBusyStatus.olTentative: return "Geçici";
                case OlBusyStatus.olBusy: return "Meşgul";
                case OlBusyStatus.olOutOfOffice: return "Ofis Dışı";
                case OlBusyStatus.olWorkingElsewhere: return "Başka Yerde Çalışıyor";
                default: return "Bilinmiyor";
            }
        }

        /// <summary>
        /// Outlook'ta takvim öğesini aç
        /// </summary>
        public static void OpenCalendarItemInOutlook(string entryId)
        {
            try
            {
                var outlook = GetOutlookApplication();
                var namespaceObj = outlook.GetNamespace("MAPI");
                var appointment = namespaceObj.GetItemFromID(entryId) as AppointmentItem;

                if (appointment != null)
                {
                    appointment.Display(false);
                }
            }
            catch (System.Exception ex)
            {
                Logger.Error("Outlook'ta takvim öğesi açılırken hata", ex);
                throw;
            }
        }

        #endregion
    }

    /// <summary>
    /// Outlook takvim öğesi modeli
    /// </summary>
    public class OutlookCalendarItem
    {
        public string EntryId { get; set; }
        public string Subject { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Location { get; set; }
        public string Organizer { get; set; }
        public string RequiredAttendees { get; set; }
        public string OptionalAttendees { get; set; }
        public bool IsAllDayEvent { get; set; }
        public bool IsRecurring { get; set; }
        public string Body { get; set; }
        public string BusyStatus { get; set; }

        /// <summary>
        /// Toplantı süresini dakika cinsinden hesapla
        /// </summary>
        public int DurationMinutes => (int)(End - Start).TotalMinutes;

        /// <summary>
        /// Süreyi okunabilir formatta döndür
        /// </summary>
        public string DurationDisplay
        {
            get
            {
                var duration = End - Start;
                if (duration.TotalHours >= 1)
                {
                    return $"{(int)duration.TotalHours} saat {duration.Minutes} dk";
                }
                return $"{(int)duration.TotalMinutes} dk";
            }
        }

        /// <summary>
        /// Zaman aralığını okunabilir formatta döndür
        /// </summary>
        public string TimeRangeDisplay
        {
            get
            {
                if (IsAllDayEvent)
                    return "Tüm Gün";
                return $"{Start:HH:mm} - {End:HH:mm}";
            }
        }
    }
}

