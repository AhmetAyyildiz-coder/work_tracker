using System;
using System.IO;
using System.Linq;

namespace work_tracker.Helpers
{
    /// <summary>
    /// Dosya depolama ve yönetim helper sınıfı
    /// Dosyaları organize bir klasör yapısında saklar
    /// </summary>
    public static class FileStorageHelper
    {
        // Ana depolama klasörü (uygulama dizini altında)
        // Ana depolama klasörü
        private static string BaseStoragePath
        {
            get
            {
                string path = @"C:\work_tracker_docs";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }

        /// <summary>
        /// Belirli bir iş öğesi için depolama klasörünü oluşturur
        /// Yapı: WorkItemAttachments/WorkItem_123/
        /// </summary>
        public static string GetWorkItemStoragePath(int workItemId)
        {
            var path = Path.Combine(BaseStoragePath, $"WorkItem_{workItemId}");
            
            // Klasör yoksa oluştur
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        /// <summary>
        /// Dosyayı sisteme kaydeder ve unique bir dosya adı döner
        /// </summary>
        /// <param name="workItemId">İş öğesi ID</param>
        /// <param name="sourceFilePath">Kaynak dosya yolu</param>
        /// <param name="originalFileName">Orijinal dosya adı</param>
        /// <returns>Kaydedilen dosyanın relative path'i</returns>
        public static string SaveFile(int workItemId, string sourceFilePath, string originalFileName)
        {
            try
            {
                var storagePath = GetWorkItemStoragePath(workItemId);
                
                // Unique dosya adı oluştur (GUID + orijinal uzantı)
                var extension = Path.GetExtension(originalFileName);
                var uniqueFileName = $"{Guid.NewGuid()}{extension}";
                var destinationPath = Path.Combine(storagePath, uniqueFileName);

                // Dosyayı kopyala
                File.Copy(sourceFilePath, destinationPath, overwrite: false);

                // Relative path döndür (veritabanında saklanacak)
                return Path.Combine($"WorkItem_{workItemId}", uniqueFileName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Dosya kaydedilemedi: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Dosyayı byte array olarak kaydeder
        /// </summary>
        public static string SaveFile(int workItemId, byte[] fileBytes, string originalFileName)
        {
            try
            {
                var storagePath = GetWorkItemStoragePath(workItemId);
                
                var extension = Path.GetExtension(originalFileName);
                var uniqueFileName = $"{Guid.NewGuid()}{extension}";
                var destinationPath = Path.Combine(storagePath, uniqueFileName);

                File.WriteAllBytes(destinationPath, fileBytes);

                return Path.Combine($"WorkItem_{workItemId}", uniqueFileName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Dosya kaydedilemedi: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Dosyanın tam fiziksel yolunu döner
        /// </summary>
        public static string GetFullPath(string relativePath)
        {
            return Path.Combine(BaseStoragePath, relativePath);
        }

        /// <summary>
        /// Dosyayı siler
        /// </summary>
        public static bool DeleteFile(string relativePath)
        {
            try
            {
                var fullPath = GetFullPath(relativePath);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Dosya silinemedi: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Dosyanın var olup olmadığını kontrol eder ve varsa tam yolunu döner.
        /// Eğer dosya eski konumdaysa (AppDomain) yeni konuma (C:\work_tracker_docs) taşır.
        /// </summary>
        public static bool TryGetExistingFile(string relativePath, out string fullPath)
        {
            // 1. Yeni konumda ara
            fullPath = GetFullPath(relativePath);
            if (File.Exists(fullPath))
            {
                return true;
            }

            // 2. Eski konumda ara (Legacy Support)
            try
            {
                var appDir = AppDomain.CurrentDomain.BaseDirectory;
                // Eski yapı: AppDir/WorkItem_123/file.txt veya AppDir/Attachments/WorkItem_123/file.txt
                // Bizim yapımız: WorkItem_123/file.txt
                
                // Olası eski yollar
                var legacyPaths = new[]
                {
                    Path.Combine(appDir, relativePath),
                    Path.Combine(appDir, "Attachments", relativePath)
                };

                foreach (var legacyPath in legacyPaths)
                {
                    if (File.Exists(legacyPath))
                    {
                        // Dosyayı bulduk! Yeni konuma taşıyalım.
                        try
                        {
                            var directory = Path.GetDirectoryName(fullPath);
                            if (!Directory.Exists(directory))
                            {
                                Directory.CreateDirectory(directory);
                            }

                            File.Copy(legacyPath, fullPath, overwrite: true);
                            Logger.Info($"Dosya eski konumdan taşındı: {legacyPath} -> {fullPath}");
                            
                            // Eski dosyayı silmeyi deneyebiliriz ama riskli olabilir, şimdilik kalsın veya loglayalım.
                            // File.Delete(legacyPath); 
                            
                            return true;
                        }
                        catch (Exception ex)
                        {
                            Logger.Error($"Dosya taşınırken hata oluştu: {legacyPath}", ex);
                            // Taşıyamadık ama dosya orada, eski yoldan devam edelim mi?
                            // Hayır, tutarlılık için false dönelim veya exception fırlatalım.
                            // Ancak kullanıcıya dosyayı göstermek istiyoruz.
                            // Geçici olarak eski yolu dönelim.
                            fullPath = legacyPath;
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Legacy dosya kontrolü sırasında hata", ex);
            }

            return false;
        }

        /// <summary>
        /// Dosyanın var olup olmadığını kontrol eder
        /// </summary>
        public static bool FileExists(string relativePath)
        {
            string fullPath;
            return TryGetExistingFile(relativePath, out fullPath);
        }

        /// <summary>
        /// Dosya boyutunu human-readable formatta döner (KB, MB, GB)
        /// </summary>
        public static string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            return $"{len:0.##} {sizes[order]}";
        }

        /// <summary>
        /// Dosya uzantısına göre ikon emoji döner
        /// </summary>
        public static string GetFileIcon(string extension)
        {
            if (string.IsNullOrEmpty(extension))
                return "📄";

            extension = extension.ToLower().TrimStart('.');

            switch (extension)
            {
                case "sql":
                    return "🗄️";
                case "pdf":
                    return "📕";
                case "doc":
                case "docx":
                    return "📘";
                case "xls":
                case "xlsx":
                    return "📗";
                case "txt":
                    return "📝";
                case "jpg":
                case "jpeg":
                case "png":
                case "gif":
                case "bmp":
                    return "🖼️";
                case "zip":
                case "rar":
                case "7z":
                    return "📦";
                case "cs":
                case "vb":
                case "js":
                case "ts":
                case "py":
                case "java":
                    return "💻";
                case "xml":
                case "json":
                case "yaml":
                    return "📋";
                default:
                    return "📄";
            }
        }

        /// <summary>
        /// MIME type belirleme
        /// </summary>
        public static string GetMimeType(string extension)
        {
            if (string.IsNullOrEmpty(extension))
                return "application/octet-stream";

            extension = extension.ToLower().TrimStart('.');

            switch (extension)
            {
                case "sql":
                    return "application/sql";
                case "pdf":
                    return "application/pdf";
                case "doc":
                    return "application/msword";
                case "docx":
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case "xls":
                    return "application/vnd.ms-excel";
                case "xlsx":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case "txt":
                    return "text/plain";
                case "jpg":
                case "jpeg":
                    return "image/jpeg";
                case "png":
                    return "image/png";
                case "gif":
                    return "image/gif";
                case "zip":
                    return "application/zip";
                case "xml":
                    return "text/xml";
                case "json":
                    return "application/json";
                default:
                    return "application/octet-stream";
            }
        }

        /// <summary>
        /// Toplam depolama alanı kullanımını hesaplar
        /// </summary>
        public static long GetTotalStorageUsed()
        {
            if (!Directory.Exists(BaseStoragePath))
                return 0;

            var dirInfo = new DirectoryInfo(BaseStoragePath);
            return dirInfo.GetFiles("*", SearchOption.AllDirectories).Sum(file => file.Length);
        }

        /// <summary>
        /// Belirli bir iş öğesi için kullanılan depolama alanını hesaplar
        /// </summary>
        public static long GetWorkItemStorageUsed(int workItemId)
        {
            var path = GetWorkItemStoragePath(workItemId);
            if (!Directory.Exists(path))
                return 0;

            var dirInfo = new DirectoryInfo(path);
            return dirInfo.GetFiles().Sum(file => file.Length);
        }
    }
}

