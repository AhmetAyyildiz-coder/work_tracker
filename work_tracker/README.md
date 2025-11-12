# Kaos Kontrol - İş Akışı Yönetim Aracı

**Dinamik İş Akışı Yönetim Aracı (v1.2)**  
🆕 **Loglama Sistemi & Minimal Tasarım Güncellemesi!**

Planlı işler (Scrum) ve plansız/acil işleri (Kanban) birbirinden ayırarak yöneten hibrit (Scrumban) iş akışı yönetim uygulaması.

## Teknoloji Stack

- **.NET Framework 4.7.2**
- **WinForms** (Desktop Uygulama)
- **DevExpress 21.2** (UI Bileşenleri)
- **Entity Framework 6.4.4** (ORM)
- **SQL Server LocalDB** (Veritabanı)

## Özellikler (MVP)

### ✅ Modül 1: Gelen Kutusu (Inbox)
- Tüm iş taleplerinin merkezi olarak toplandığı yer
- Talep oluşturma, düzenleme, silme
- Triage'e yönlendirme

### ✅ Modül 2: Triage (Sınıflandırma)
- İş tiplerini belirleme (AcilArge, Bug, YeniÖzellik vb.)
- Aciliyet seviyesi (Kritik, Yüksek, Normal, Düşük)
- Tahmini efor girişi
- Scrum veya Kanban panosuna yönlendirme

### ✅ Modül 4: Kanban Panosu (Acil İşler)
- Sürükle-bırak ile iş kartı taşıma
- WIP (Work In Progress) limiti kontrolü
- Kanban sütunları:
  - Gelen Acil İşler
  - Sırada
  - Müdahale Ediliyor (WIP Limit: 3)
  - Doğrulama Bekliyor
  - Çözüldü

### ✅ Modül 5: Ayarlar (Proje & Modül Yönetimi)
- Proje tanımlama ve yönetimi
- Modül tanımlama (SQL, Ekran, API, Rapor vb.)
- Proje-modül ilişkilendirme

### ✅ Modül 7: Toplantı Kaydı
- Toplantı konusu, tarih, katılımcılar
- Zengin metin editörü ile toplantı notları (RichEdit)
- **İş Talebine Dönüştür**: Toplantı notlarından seçilen metni otomatik olarak iş talebine çevirir
- Toplantı-iş talebi ilişkilendirme ve izlenebilirlik
- Toplantıdan gelen iş taleplerinin listesi ve durumları

### ✅ 🆕 İş Detayı & Aktivite Takibi
- **Geliştirilmiş İş Kartları**: Modern, interaktif tasarım
  - 🎨 Hover efektleri ile animasyonlu gölgeler
  - 🔍 Clickable info ikonu (detayları görmek için)
  - 📌 Sol kenarda aciliyet göstergesi (renkli çubuk)
  - 🖱️ **TEK tıklama** ile detay ekranı açılır
  - 📅 Tarih, aktivite ve dosya sayısı göstergeleri
- **İş Detay Ekranı**: Kanban/Scrum kartlarına tıklayarak açılır
  - Kartın **tüm alanlarına** tıklama çalışır (child kontroller dahil)
  - Başlık, etiketler, ikonlar - her yere tıklanabilir!
- **Aktivite Timeline**: Tüm iş geçmişi kronolojik sırada
  - 💬 Yorumlar
  - 📊 Durum değişiklikleri
  - 📎 Dosya işlemleri
  - ✏️ Alan güncellemeleri
- **Yorum Sistemi**: İş üzerine not ekleme
- **Durum Güncelleme**: Test modundaki işleri yönetme
- **Kart Göstergeleri**: Aktivite/dosya sayısı görünür

### ✅ 🆕 Dosya Yönetimi
- **Dosya Ekleme**: SQL scriptleri, dokümanlar, ekran görüntüleri
- **Desteklenen Tipler**: .sql, .pdf, .docx, .xlsx, .png, .zip ve daha fazlası
- **Dosya İşlemleri**:
  - 📁 Dosya Ekle (çoklu seçim destekli)
  - 💾 İndir
  - 📂 Aç (varsayılan uygulama ile)
  - 🗑️ Sil
- **Organize Depolama**: Her iş için ayrı klasör yapısı
- **Güvenlik**: GUID bazlı unique dosya adları
- **İzlenebilirlik**: Tüm dosya işlemleri aktivite timeline'da

### ✅ 🆕 Loglama & Hata Yönetimi
- **Global Exception Handling**: Tüm yakalanmamış hatalar otomatik loglanır
- **Dosya Bazlı Loglama**: Günlük log dosyaları (`Logs/WorkTracker_yyyy-MM-dd.log`)
- **Loglama Seviyeleri**:
  - 📘 INFO - Bilgi mesajları
  - ⚠️ WARNING - Uyarılar
  - ❌ ERROR - Hatalar (stack trace ile)
  - 🔍 DEBUG - Geliştirme logları
- **Otomatik Temizlik**: 30 günden eski loglar otomatik silinir
- **Detaylı Hata Kayıtları**: Exception, stack trace ve inner exception
- **Kullanıcı Dostu Hata Dialog'ları**: Log dosyası yolu ile birlikte
- **Log Klasörü Erişimi**: Ribbon menüden log klasörünü açma
- **Thread-Safe**: Çoklu thread'den güvenli loglama
- **Performanslı**: Lock mekanizması ile hızlı yazma

## Kurulum ve Çalıştırma

### Ön Gereksinimler
1. Visual Studio 2019 veya üzeri
2. .NET Framework 4.7.2 SDK
3. DevExpress 21.2 (lisanslı kurulum gerekli)
4. SQL Server LocalDB

### Adımlar

1. **Solution'ı açın:**
   ```
   work_tracker/work_tracker.sln
   ```

2. **NuGet paketlerini geri yükleyin:**
   - Visual Studio'da: Solution'a sağ tıklayın → "Restore NuGet Packages"

3. **Veritabanını oluşturun:**
   Visual Studio'da **Package Manager Console**'u açın (Tools → NuGet Package Manager → Package Manager Console)
   
   ```powershell
   Update-Database
   ```

4. **Uygulamayı çalıştırın:**
   - F5 tuşuna basın veya "Start" butonuna tıklayın

### İlk Çalıştırma

Uygulama ilk açıldığında otomatik olarak:
- Demo proje ve modüller oluşturulur
- Kanban sütunları tanımlanır
- Seed data yüklenir

## Veritabanı Bağlantısı

LocalDB bağlantı dizgisi (App.config):
```
Data Source=(localdb)\MSSQLLocalDB;
Initial Catalog=work_tracker;
Integrated Security=True;
Connect Timeout=30;
Encrypt=False;
Trust Server Certificate=False;
Application Intent=ReadWrite;
Multi Subnet Failover=False
```

## Ana Formlar

1. **MainForm**: Ana Ribbon menü ile tüm modüllere erişim
2. **InboxForm**: Gelen kutusu - Yeni talepler
3. **TriageForm**: Sınıflandırma ekranı
4. **KanbanBoardForm**: Acil işler panosu (sürükle-bırak)
5. **MeetingForm**: Toplantı yönetimi ve aksiyona dönüştürme
6. **ProjectManagementForm**: Proje yönetimi
7. **ModuleManagementForm**: Modül yönetimi

## Proje Yapısı

```
work_tracker/
├── Data/
│   ├── Entities/              # Entity sınıfları
│   │   ├── Project.cs
│   │   ├── ProjectModule.cs
│   │   ├── Meeting.cs
│   │   ├── WorkItem.cs
│   │   └── KanbanColumnSetting.cs
│   └── WorkTrackerDbContext.cs
├── Migrations/                # EF6 Migrations
│   ├── Configuration.cs
│   └── 202411111200_InitialCreate.cs
├── Forms/                     # Tüm UI formları
│   ├── MainForm.cs
│   ├── InboxForm.cs
│   ├── TriageForm.cs
│   ├── KanbanBoardForm.cs
│   ├── MeetingForm.cs
│   └── ...
├── Program.cs                 # Uygulama başlangıcı
└── App.config                 # Yapılandırma dosyası
```

## Gelecek Özellikler (MVP Sonrası)

- **Modül 3**: Scrum Panosu (Sprint yönetimi)
- **Modül 6**: Raporlama ve Analitik
  - Kapasite dağılım raporu (Scrum vs Kanban)
  - İş dağılım raporu (Proje/Modül bazlı)
- Kullanıcı yönetimi ve atama sistemi
- Sprint planlama ve izleme
- Burndown chart'lar

## Lisans

Bu proje özel kullanım içindir.

## İletişim

Proje sahibi: [Ekip Lideri / Product Owner]  
Tarih: 11 Kasım 2025

