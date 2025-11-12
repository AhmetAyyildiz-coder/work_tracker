using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using work_tracker.Helpers;

namespace work_tracker
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Global exception handler'ları kur
            SetupGlobalExceptionHandling();
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // DevExpress skin ayarları
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.DevExpress);
            
            // Uygulama başladığında logla
            Logger.Info("========================================");
            Logger.Info("Work Tracker uygulaması başlatıldı");
            Logger.Info($"Versiyon: {Application.ProductVersion}");
            Logger.Info($"Kullanıcı: {Environment.UserName}");
            Logger.Info($"Makine: {Environment.MachineName}");
            Logger.Info("========================================");
            
            // Eski logları temizle (30 günden eski)
            Logger.CleanOldLogs(30);
            
            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Application.Run içinde kritik hata");
                ShowErrorDialog(ex);
            }
            finally
            {
                Logger.Info("Work Tracker uygulaması kapatıldı");
            }
        }

        /// <summary>
        /// Global exception handling kurulumu
        /// </summary>
        private static void SetupGlobalExceptionHandling()
        {
            // UI thread exception handler
            Application.ThreadException += (sender, e) =>
            {
                Logger.LogException(e.Exception, "UI Thread Exception");
                ShowErrorDialog(e.Exception);
            };

            // Non-UI thread exception handler
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                var ex = e.ExceptionObject as Exception;
                Logger.LogException(ex, "Unhandled Domain Exception");
                
                if (e.IsTerminating)
                {
                    Logger.Error("Uygulama sonlanıyor (IsTerminating=true)");
                }
                
                ShowErrorDialog(ex);
            };

            // Task exception handler (async/await için)
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                Logger.LogException(e.Exception, "Unobserved Task Exception");
                e.SetObserved(); // Exception'ı "gözlemlendi" olarak işaretle
            };

            // Windows Forms exception mode
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        }

        /// <summary>
        /// Kullanıcıya hata dialog'u göster
        /// </summary>
        private static void ShowErrorDialog(Exception ex)
        {
            try
            {
                var message = $"Beklenmeyen bir hata oluştu!\n\n" +
                             $"Hata: {ex?.Message ?? "Bilinmeyen hata"}\n\n" +
                             $"Detaylar log dosyasına kaydedildi.\n" +
                             $"Log: {Logger.GetCurrentLogFilePath()}\n\n" +
                             $"Devam etmek için Tamam'a tıklayın.";

                XtraMessageBox.Show(
                    message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch
            {
                // Hata dialog gösterilemezse en azından MessageBox dene
                MessageBox.Show(
                    "Kritik hata oluştu ve log kaydedilemedi!",
                    "Kritik Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
