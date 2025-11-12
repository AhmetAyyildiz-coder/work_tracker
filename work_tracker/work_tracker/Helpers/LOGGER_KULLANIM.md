# Logger Kullanım Kılavuzu

## 📝 Genel Bakış

Work Tracker uygulaması için basit ve etkili dosya tabanlı loglama sistemi.

### Özellikler

✅ **Global Exception Handling** - Tüm yakalanmamış hatalar otomatik loglanır  
✅ **Dosya Tabanlı** - Günlük log dosyaları (`Logs/WorkTracker_yyyy-MM-dd.log`)  
✅ **Seviye Bazlı** - INFO, WARNING, ERROR, DEBUG  
✅ **Thread-Safe** - Çoklu thread'den güvenli yazma  
✅ **Otomatik Temizlik** - 30 günden eski loglar silinir  
✅ **Detaylı Exception Logging** - Stack trace ve inner exception

---

## 🚀 Temel Kullanım

### 1. Bilgi (Info) Logları

```csharp
using work_tracker.Helpers;

// Basit bilgi mesajı
Logger.Info("Uygulama başlatıldı");

// Değişken içeren mesaj
Logger.Info($"Kullanıcı {userName} giriş yaptı");
Logger.Info($"{workItemCount} iş yüklendi");
```

### 2. Hata (Error) Logları

```csharp
// Sadece mesaj
Logger.Error("Veritabanı bağlantısı kurulamadı");

// Exception ile birlikte
try
{
    // Riskli kod
    var result = riskyOperation();
}
catch (Exception ex)
{
    Logger.Error("İşlem başarısız oldu", ex);
    // veya
    Logger.LogException(ex, "Kullanıcı bilgileri yüklenirken hata");
}
```

### 3. Uyarı (Warning) Logları

```csharp
// Uyarı mesajları
Logger.Warning("WIP limiti aşıldı!");
Logger.Warning($"Geçersiz parametre: {param}");
```

### 4. Debug Logları

```csharp
// Sadece DEBUG modda çalışır
#if DEBUG
Logger.Debug("Test değeri: " + testValue);
Logger.Debug($"Method çağrıldı: {methodName}");
#endif
```

---

## 💡 Örnek Kullanım Senaryoları

### Form Constructor'da

```csharp
public KanbanBoardForm()
{
    InitializeComponent();
    
    try
    {
        Logger.Info("KanbanBoardForm oluşturuluyor");
        _context = new WorkTrackerDbContext();
        InitializeLayout();
        Logger.Info("KanbanBoardForm başarıyla oluşturuldu");
    }
    catch (Exception ex)
    {
        Logger.LogException(ex, "KanbanBoardForm constructor hatası");
        throw; // Hata üst katmana fırlatılır
    }
}
```

### Veritabanı İşlemlerinde

```csharp
private void SaveWorkItem(WorkItem item)
{
    try
    {
        Logger.Info($"İş kaydediliyor: #{item.Id} - {item.Title}");
        
        _context.WorkItems.Add(item);
        _context.SaveChanges();
        
        Logger.Info($"İş başarıyla kaydedildi: #{item.Id}");
    }
    catch (DbUpdateException ex)
    {
        Logger.LogException(ex, $"İş kaydedilemedi: #{item.Id}");
        XtraMessageBox.Show("İş kaydedilemedi!", "Hata", 
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    catch (Exception ex)
    {
        Logger.LogException(ex, "Beklenmeyen veritabanı hatası");
        throw;
    }
}
```

### Dosya İşlemlerinde

```csharp
private void UploadFile(string filePath)
{
    try
    {
        Logger.Info($"Dosya yükleniyor: {Path.GetFileName(filePath)}");
        
        var fileInfo = new FileInfo(filePath);
        if (fileInfo.Length > 10 * 1024 * 1024) // 10 MB
        {
            Logger.Warning($"Büyük dosya uyarısı: {fileInfo.Length} bytes");
        }
        
        // Dosya işlemleri...
        
        Logger.Info($"Dosya başarıyla yüklendi: {fileInfo.Name}");
    }
    catch (IOException ex)
    {
        Logger.LogException(ex, "Dosya okuma/yazma hatası");
        throw;
    }
}
```

### Async/Await İşlemlerde

```csharp
private async Task LoadDataAsync()
{
    try
    {
        Logger.Info("Asenkron veri yükleme başladı");
        
        var data = await _service.GetDataAsync();
        
        Logger.Info($"{data.Count} kayıt yüklendi");
    }
    catch (TaskCanceledException ex)
    {
        Logger.Warning("İşlem kullanıcı tarafından iptal edildi");
    }
    catch (Exception ex)
    {
        Logger.LogException(ex, "Asenkron veri yükleme hatası");
    }
}
```

---

## 🔧 Yardımcı Metodlar

### Log Klasörünü Aç

```csharp
// Kullanıcıya log klasörünü göster
Logger.OpenLogFolder();

// Ribbon menüde kullanımı:
private void btnOpenLogs_ItemClick(object sender, ItemClickEventArgs e)
{
    Logger.OpenLogFolder();
}
```

### Mevcut Log Dosyası Yolu

```csharp
var logPath = Logger.GetCurrentLogFilePath();
Console.WriteLine($"Log dosyası: {logPath}");
```

### Eski Logları Temizle

```csharp
// Program.cs'de otomatik çalışır
Logger.CleanOldLogs(30); // 30 günden eski logları sil

// Manuel temizlik
Logger.CleanOldLogs(7); // 7 günden eski logları sil
```

---

## 📂 Log Dosya Formatı

```
[2024-11-12 14:23:45.123] [INFO] Work Tracker uygulaması başlatıldı
[2024-11-12 14:23:45.456] [INFO] Veritabanı bağlantısı başarılı. 3 proje bulundu.
[2024-11-12 14:23:50.789] [INFO] KanbanBoardForm oluşturuluyor
[2024-11-12 14:24:12.345] [WARNING] WIP limiti aşıldı!
[2024-11-12 14:25:30.678] [ERROR] Veritabanı hatası
Exception: SqlException
Message: Connection timeout
StackTrace:
   at System.Data.SqlClient...
```

---

## 🎯 Best Practices

### ✅ Yapılması Gerekenler

1. **Önemli olayları logla**
   - Form açılışları
   - Veritabanı işlemleri
   - Dosya işlemleri
   - Kullanıcı aksiyonları

2. **Exception'ları detaylı logla**
   ```csharp
   catch (Exception ex)
   {
       Logger.LogException(ex, "Açıklayıcı context bilgisi");
   }
   ```

3. **Anlamlı mesajlar yaz**
   ```csharp
   // İyi ✅
   Logger.Info($"İş #{workItem.Id} '{workItem.Title}' kaydedildi");
   
   // Kötü ❌
   Logger.Info("İşlem tamam");
   ```

### ❌ Yapılmaması Gerekenler

1. **Her şeyi loglama**
   ```csharp
   // Gereksiz ❌
   Logger.Info("Button tıklandı");
   Logger.Info("Mouse hareket etti");
   ```

2. **Hassas bilgileri loglama**
   ```csharp
   // Güvenlik riski ❌
   Logger.Info($"Şifre: {password}");
   Logger.Info($"Kredi kartı: {cardNumber}");
   ```

3. **Loop içinde aşırı loglama**
   ```csharp
   // Performans sorunu ❌
   foreach (var item in items) // 10000 item
   {
       Logger.Info($"İşleniyor: {item.Id}"); // 10000 log!
   }
   
   // Daha iyi ✅
   Logger.Info($"{items.Count} item işlenmeye başlandı");
   // İşlemler...
   Logger.Info($"{items.Count} item başarıyla işlendi");
   ```

---

## 🔍 Log Analizi

### Windows PowerShell ile

```powershell
# Son 50 satır
Get-Content .\Logs\WorkTracker_2024-11-12.log -Tail 50

# ERROR logları
Select-String -Path .\Logs\*.log -Pattern "\[ERROR\]"

# Belirli bir kelime ara
Select-String -Path .\Logs\*.log -Pattern "KanbanBoard"
```

### Not Defteri ile

1. `Logs` klasörünü aç
2. `.log` dosyasını Not Defteri ile aç
3. Ctrl+F ile ara

---

## 🚨 Global Exception Handling

Uygulama otomatik olarak tüm yakalanmamış exception'ları loglar:

- **UI Thread Exceptions** → `Application.ThreadException`
- **Non-UI Thread Exceptions** → `AppDomain.UnhandledException`
- **Task Exceptions** → `TaskScheduler.UnobservedTaskException`

Kullanıcıya hata mesajı gösterilir ve log dosyasına kaydedilir.

---

## 📍 Log Dosyası Konumu

```
[Uygulama Klasörü]\Logs\
├── WorkTracker_2024-11-10.log
├── WorkTracker_2024-11-11.log
└── WorkTracker_2024-11-12.log (aktif)
```

Örnek: `C:\Users\[Kullanıcı]\source\repos\work_tracker\work_tracker\bin\Debug\Logs\`

---

## 💾 Performans

- **Thread-Safe**: Birden fazla thread'den güvenli
- **Dosya Kilitleme**: Lock mekanizması ile çakışma önlenir
- **Hata Toleransı**: Log yazarken hata olursa sessizce devam eder
- **Otomatik Temizlik**: Eski loglar otomatik silinir

---

## 🎓 Özet

```csharp
// Başlangıç
Logger.Info("İşlem başladı");

// Hata durumu
try { }
catch (Exception ex) 
{ 
    Logger.LogException(ex, "Context bilgisi"); 
}

// Uyarı
Logger.Warning("Dikkat edilmesi gereken durum");

// Debug
Logger.Debug("Geliştirme sırasında debug bilgisi");

// Log klasörünü aç
Logger.OpenLogFolder();
```

**Not**: Global exception handling otomatik çalışır, manuel try-catch gerekmez!

