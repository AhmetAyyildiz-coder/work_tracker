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
        /// ConversationId ile tüm klasörlerde mail arar (recursive)
        /// </summary>
        private static MailItem FindMailByConversationId(NameSpace namespaceObj, string conversationId)
        {
            try
            {
                // Tüm hesaplardaki tüm klasörlerde ara
                foreach (Store store in namespaceObj.Stores)
                {
                    try
                    {
                        Logger.Info($"E-posta hesabında aranıyor: {store.DisplayName}");
                        var rootFolder = store.GetRootFolder();
                        var result = SearchFolderRecursive(rootFolder, conversationId, 0);
                        if (result != null)
                        {
                            return result;
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Warning($"Store aramasında hata: {store.DisplayName} - {ex.Message}");
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
        /// Klasör ve alt klasörlerinde recursive olarak mail arar
        /// </summary>
        private static MailItem SearchFolderRecursive(MAPIFolder folder, string conversationId, int depth)
        {
            // Çok derin aramayı engelle (performans için)
            if (depth > 10) return null;

            try
            {
                // Önce bu klasörde ara
                var result = SearchInFolder(folder, conversationId);
                if (result != null)
                {
                    Logger.Info($"Mail bulundu: {folder.FolderPath}");
                    return result;
                }

                // Alt klasörlerde ara
                foreach (MAPIFolder subfolder in folder.Folders)
                {
                    try
                    {
                        result = SearchFolderRecursive(subfolder, conversationId, depth + 1);
                        if (result != null)
                        {
                            return result;
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Warning($"Alt klasör aramasında hata: {subfolder.Name} - {ex.Message}");
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.Warning($"Klasör aramasında hata: {folder.Name} - {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// Tek bir klasörde ConversationId ile mail arar
        /// </summary>
        private static MailItem SearchInFolder(MAPIFolder folder, string conversationId)
        {
            try
            {
                var items = folder.Items;
                
                // Önce DASL filtresi ile hızlı arama dene
                try
                {
                    string filter = $"@SQL=\"urn:schemas:httpmail:thread-index\" = '{conversationId}'";
                    var filteredItems = items.Restrict(filter);
                    foreach (object item in filteredItems)
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
                }
                catch
                {
                    // DASL filtresi başarısız olursa, klasördeki son 100 mail'i kontrol et
                    int count = 0;
                    int maxItems = 100;
                    
                    // En yeni maillerden başla
                    try { items.Sort("[ReceivedTime]", true); } catch { }
                    
                    foreach (object item in items)
                    {
                        if (count >= maxItems) break;
                        count++;

                        if (item is MailItem mail)
                        {
                            try
                            {
                                if (mail.ConversationID == conversationId)
                                {
                                    return mail;
                                }
                            }
                            finally
                            {
                                Marshal.ReleaseComObject(mail);
                            }
                        }
                        else
                        {
                            Marshal.ReleaseComObject(item);
                        }
                    }
                }
                
                Marshal.ReleaseComObject(items);
            }
            catch (System.Exception ex)
            {
                Logger.Warning($"Klasör içi aramada hata: {folder.Name} - {ex.Message}");
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

