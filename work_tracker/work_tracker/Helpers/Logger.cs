using System;
using System.IO;
using System.Text;

namespace work_tracker.Helpers
{
    /// <summary>
    /// Basit ama etkili dosya tabanlı loglama sistemi
    /// </summary>
    public static class Logger
    {
        private static readonly object _lock = new object();
        private static string _logDirectory;
        private static string _logFilePath;

        static Logger()
        {
            // Log klasörünü C:\work_tracker_docs\logs altında oluştur
            _logDirectory = @"C:\work_tracker_docs\logs";
            
            // Klasör yoksa oluştur
            if (!Directory.Exists(_logDirectory))
            {
                Directory.CreateDirectory(_logDirectory);
            }

            // Günlük log dosyası (her gün yeni dosya)
            var today = DateTime.Now.ToString("yyyy-MM-dd");
            _logFilePath = Path.Combine(_logDirectory, $"WorkTracker_{today}.log");
        }

        /// <summary>
        /// Bilgi mesajı logla
        /// </summary>
        public static void Info(string message)
        {
            WriteLog("INFO", message);
        }

        /// <summary>
        /// Hata mesajı logla
        /// </summary>
        public static void Error(string message, Exception ex = null)
        {
            var fullMessage = message;
            if (ex != null)
            {
                fullMessage += $"\nException: {ex.GetType().Name}\nMessage: {ex.Message}\nStackTrace:\n{ex.StackTrace}";
                
                // Inner exception varsa onu da logla
                if (ex.InnerException != null)
                {
                    fullMessage += $"\n\nInner Exception: {ex.InnerException.GetType().Name}\nMessage: {ex.InnerException.Message}\nStackTrace:\n{ex.InnerException.StackTrace}";
                }
            }
            
            WriteLog("ERROR", fullMessage);
        }

        /// <summary>
        /// Uyarı mesajı logla
        /// </summary>
        public static void Warning(string message)
        {
            WriteLog("WARNING", message);
        }

        /// <summary>
        /// Debug mesajı logla (sadece development'ta)
        /// </summary>
        public static void Debug(string message)
        {
            #if DEBUG
            WriteLog("DEBUG", message);
            #endif
        }

        /// <summary>
        /// Exception'ı detaylı şekilde logla
        /// </summary>
        public static void LogException(Exception ex, string context = "")
        {
            var message = string.IsNullOrEmpty(context) 
                ? "Beklenmeyen hata oluştu" 
                : $"Hata oluştu: {context}";
            
            Error(message, ex);
        }

        /// <summary>
        /// Log dosyasına yaz
        /// </summary>
        private static void WriteLog(string level, string message)
        {
            try
            {
                lock (_lock)
                {
                    var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    var logEntry = $"[{timestamp}] [{level}] {message}{Environment.NewLine}";
                    
                    // Dosyaya ekle (append mode)
                    File.AppendAllText(_logFilePath, logEntry, Encoding.UTF8);
                }
            }
            catch
            {
                // Log yazarken hata olursa sessizce devam et
                // (Sonsuz döngüye girmemek için)
            }
        }

        /// <summary>
        /// Log klasörünü aç
        /// </summary>
        public static void OpenLogFolder()
        {
            try
            {
                if (Directory.Exists(_logDirectory))
                {
                    System.Diagnostics.Process.Start(_logDirectory);
                }
            }
            catch (Exception ex)
            {
                Error("Log klasörü açılamadı", ex);
            }
        }

        /// <summary>
        /// Mevcut log dosyasının yolunu döndür
        /// </summary>
        public static string GetCurrentLogFilePath()
        {
            return _logFilePath;
        }

        /// <summary>
        /// Eski log dosyalarını temizle (30 günden eski)
        /// </summary>
        public static void CleanOldLogs(int daysToKeep = 30)
        {
            try
            {
                var files = Directory.GetFiles(_logDirectory, "*.log");
                var cutoffDate = DateTime.Now.AddDays(-daysToKeep);

                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);
                    if (fileInfo.CreationTime < cutoffDate)
                    {
                        File.Delete(file);
                        Info($"Eski log dosyası silindi: {Path.GetFileName(file)}");
                    }
                }
            }
            catch (Exception ex)
            {
                Error("Eski loglar temizlenirken hata oluştu", ex);
            }
        }
    }
}

