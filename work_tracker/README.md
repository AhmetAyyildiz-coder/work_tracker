# Work Tracker - Kişisel İş Yönetim Aracı

**Sürüm 3.0** • Son Güncelleme: 30 Kasım 2025

Hibrit iş yönetimi (Scrum + Kanban), ilişkili işler, otomatik zaman takibi ve kurumsal bilgi yönetimi için tasarlanmış kapsamlı masaüstü uygulaması.

---

## 🎯 Uygulama Felsefesi

Work Tracker, **kişisel iş yönetimi** için tasarlanmış kapsamlı bir araçtır:

- **Hibrit İş Yönetimi:** Planlı işler (Scrum) ve acil işler (Kanban) ayrı yönetilir
- **İlişkisel Yapı:** İşler birbirine bağlanabilir (üst-alt, kardeş ilişkileri)
- **Otomatik Zaman Takibi:** Geliştirme süresi durum değişikliklerinden otomatik hesaplanır
- **Bilgi Yönetimi:** Wiki ile kurumsal bilgi birikimi oluşturulur
- **Günlük Hatırlatıcı:** 17:30'da aktif işleriniz için sistem bildirimi

---

## 🛠️ Teknoloji Stack

| Teknoloji | Sürüm | Kullanım Alanı |
|-----------|-------|----------------|
| .NET Framework | 4.7.2 | Ana platform |
| WinForms | - | Desktop UI |
| DevExpress | 21.2 | UI Bileşenleri (Ribbon, Grid, Diagram, RichEdit) |
| Entity Framework | 6.4.4 | ORM / Veritabanı erişimi |
| SQL Server LocalDB | - | Yerel veritabanı |
| Microsoft Outlook Interop | 15.0 | E-posta entegrasyonu |

---

## ✨ Özellikler

### 📥 Gelen Kutusu & Sınıflandırma
- Tüm iş taleplerinin merkezi toplama noktası
- İş tipi belirleme (AcilArge, Bug, YeniÖzellik, İyileştirme)
- Aciliyet seviyesi (Kritik, Yüksek, Normal, Düşük)
- Tahmini efor girişi (gün cinsinden)
- Scrum veya Kanban panosuna yönlendirme

### 📋 Kanban Panosu (Acil İşler)
- Sürükle-bırak ile iş kartı taşıma
- WIP (Work In Progress) limiti kontrolü (max 3)
- Sütunlar: Gelen → Sırada → Müdahale Ediliyor → Doğrulama → Çözüldü
- Renk kodlu aciliyet göstergeleri

### 🏃 Scrum Panosu (Planlı İşler)
- Sprint bazlı iş yönetimi
- Sütunlar: Sprint Backlog → Geliştirmede → Testte → Tamamlandı
- Sprint seçimi ve filtreleme
- Toplantıdan gelen işler için özel ikon (📅)

### 🔗 İlişkili İşler (YENİ!)
- **Üst-Alt İlişkisi:** Büyük işleri alt görevlere bölme
- **Kardeş İlişkisi:** Bağımlı veya ilgili işleri eşleştirme
- İş detayında "İlişkiler" sekmesi
- İlişki açıklaması ekleme

### 📊 Çalışma Özeti (YENİ!)
- Günlük/haftalık/aylık performans görüntüleme
- **Otomatik Zaman Hesaplama:** "Geliştirmede" veya "Müdahale Ediliyor" durumunda geçen süre
- Tamamlanan iş sayısı ve günlük ortalama
- Zaman dağılımı grafiği
- Panoya kopyalama özelliği

### 🔗 İş Hiyerarşisi Diyagramı (YENİ!)
- İlişkilerin görsel diyagram olarak gösterimi
- Otomatik ağaç yerleşimi
- Renk kodları: Gri(Bekliyor), Mavi(Backlog), Sarı(Geliştirmede), Yeşil(Tamamlandı)
- PNG olarak dışa aktarma

### 📅 Toplantı Yönetimi
- Zengin metin editörü (resim, tablo, link desteği)
- Seçili metni iş talebine dönüştürme
- Toplantı-iş ilişkilendirme
- Toplantıdan gelen işlerin listesi

### 📚 Wiki (YENİ!)
- Proje bazlı bilgi bankası
- Teknik dokümantasyon
- Zengin içerik desteği (resim, tablo, link)
- Hızlı arama

### ⏱️ Zaman Kayıtları
- Manuel zaman girişi (toplantılar, araştırma vb.)
- Kişi bazlı kayıt
- İş ile ilişkilendirme

### 🔔 Günlük Hatırlatıcı
- Her gün 17:30'da otomatik bildirim
- Aktif iş sayısı ve detayları
- Tray menüsünden manuel tetikleme
- Arka planda çalışma desteği

### 📧 Outlook Entegrasyonu
- E-posta bağlama (son 7 gün)
- Otomatik arama
- İçerik ve ek dosya aktarımı

### 📎 Dosya Yönetimi
- SQL, PDF, DOCX, XLSX, PNG, ZIP desteği
- Çoklu dosya ekleme
- GUID bazlı organize depolama
- Aktivite timeline'da izlenebilirlik

### 📈 Raporlar
- Kapasite dağılımı (Scrum vs Kanban)
- Proje ve modül bazında iş dağılımı
- Sprint performans metrikleri

### 🔄 Sprint Yönetimi
- Sprint oluşturma ve hedef belirleme
- Başlatma/tamamlama işlemleri
- Aktif sprint kontrolü

### 📁 Proje & Modül Yönetimi
- Proje tanımlama
- Modül tanımlama (SQL, Ekran, API, Rapor vb.)
- Proje-modül ilişkilendirme

### 📋 Tüm İşler
- Tüm işlerin listesi
- Gelişmiş filtreleme ve arama
- Toplu işlem desteği

### 🆕 Loglama Sistemi
- Günlük log dosyaları
- Otomatik 30 günlük temizlik
- Thread-safe yazma
- Log klasörü erişimi

---

## 🖥️ Uygulama Modülleri

| Modül | Açıklama | Grup |
|-------|----------|------|
| 📥 Gelen Kutusu | Yeni iş talepleri + Sınıflandırma | İş Akışı |
| 📋 Kanban Panosu | Acil işler için WIP limitli akış | İş Akışı |
| 🏃 Scrum Panosu | Sprint bazlı planlı işler | İş Akışı |
| 📅 Toplantılar | Toplantı kayıtları ve aksiyon takibi | İş Akışı |
| 📋 Tüm İşler | Tüm işlerin listesi ve arama | İş Akışı |
| ⏱️ Zaman Kayıtları | Manuel zaman girişleri | İş Akışı |
| 📊 Çalışma Özeti | Günlük/haftalık/aylık performans | İş Akışı |
| 🔗 İş Hiyerarşisi | İlişki diyagramı görselleştirme | İş Akışı |
| 📁 Projeler | Proje tanımlama | Ayarlar |
| 📦 Modüller | Proje altı modüller | Ayarlar |
| 🔄 Sprint Yönetimi | Sprint oluştur/başlat/tamamla | Ayarlar |
| 📚 Wiki | Bilgi bankası ve dokümantasyon | Ayarlar |
| 📈 Raporlar | Kapasite ve performans analizleri | Ayarlar |
| ❓ Nasıl Kullanılır? | Kapsamlı kullanım kılavuzu | Yardım |
| 🔔 Şimdi Hatırlat | Manuel hatırlatma tetikleme | Yardım |

---

## 🚀 Kurulum

### Ön Gereksinimler
1. Visual Studio 2019 veya üzeri
2. .NET Framework 4.7.2 SDK
3. DevExpress 21.2 (lisanslı kurulum gerekli)
4. SQL Server LocalDB
5. Microsoft Outlook (e-posta entegrasyonu için)

### Adımlar

1. **Solution'ı açın:**
   ```
   work_tracker/work_tracker.sln
   ```

2. **NuGet paketlerini geri yükleyin:**
   ```
   Solution → Sağ tık → Restore NuGet Packages
   ```

3. **Veritabanını oluşturun:**
   ```powershell
   # Package Manager Console'da
   Update-Database
   ```

4. **Uygulamayı çalıştırın:**
   - F5 veya Start butonuna tıklayın

---

## 📁 Proje Yapısı

```
work_tracker/
├── Data/
│   ├── Entities/              # Entity sınıfları
│   │   ├── Project.cs
│   │   ├── WorkItem.cs
│   │   ├── WorkItemRelation.cs
│   │   ├── WorkItemActivity.cs
│   │   ├── TimeEntry.cs
│   │   ├── WikiPage.cs
│   │   └── ...
│   └── WorkTrackerDbContext.cs
├── Forms/                     # UI formları
│   ├── MainForm.cs
│   ├── InboxForm.cs
│   ├── KanbanBoardForm.cs
│   ├── ScrumBoardForm.cs
│   ├── WorkItemDetailForm.cs
│   ├── WorkSummaryForm.cs
│   ├── WorkItemHierarchyForm.cs
│   ├── WikiForm.cs
│   └── ...
├── Helpers/
│   ├── DevelopmentTimeHelper.cs
│   ├── FileStorageHelper.cs
│   ├── Logger.cs
│   └── OutlookHelper.cs
├── Services/
│   ├── WorkItemRelationService.cs
│   └── WorkReminderService.cs
├── Migrations/                # EF6 Migrations
└── App.config
```

---

## 💡 Kullanım İpuçları

### Verimli Çalışma
- İşe başlarken kartı **Geliştirmede**'ye taşıyın - süre otomatik başlar
- Ara verirken **Sprint Backlog**'a geri taşıyın - süre durur
- Her gün 17:30 hatırlatmasıyla açık işlerinizi kontrol edin

### İlişkileri Kullanın
- Büyük işleri alt görevlere bölün (üst-alt ilişkisi)
- Bağımlı işleri kardeş olarak işaretleyin
- Hiyerarşi diyagramı ile büyük resmi görün

### Bilgi Yönetimi
- Sık kullanılan SQL sorgularını Wiki'ye kaydedin
- Proje dökümanlarını Wiki'de tutun
- Toplantı notlarından aksiyon çıkarın

---

## ⌨️ Klavye Kısayolları

| Kısayol | İşlev |
|---------|-------|
| F5 | Yenile |
| Ctrl+B | Kalın yazı |
| Ctrl+I | İtalik yazı |
| Ctrl+U | Altı çizili |
| Ctrl+K | Hyperlink ekle |
| Ctrl+V | Resim yapıştır |
| Çift Tık | Kart detayı aç |

---

## 📝 Sürüm Geçmişi

### v3.0 (30 Kasım 2025)
- 🆕 İlişkili işler (üst-alt, kardeş)
- 🆕 Çalışma özeti (otomatik zaman takibi)
- 🆕 İş hiyerarşisi diyagramı (DiagramControl)
- 🆕 Wiki (bilgi bankası)
- 🆕 Günlük hatırlatıcı (17:30)
- 🆕 Tray'e küçültme desteği

### v2.0 (12 Kasım 2025)
- Scrum panosu ve sprint yönetimi
- Zaman kayıtları
- Gelişmiş raporlama
- Outlook entegrasyonu

### v1.0 (11 Kasım 2025)
- Gelen kutusu ve sınıflandırma
- Kanban panosu
- Toplantı yönetimi
- Proje/modül yönetimi
- Dosya yönetimi
- Loglama sistemi

---

## 📄 Lisans

Bu proje kişisel kullanım içindir.

---

**Work Tracker v3.0** • Hibrit İş Yönetimi • İlişkili İşler • Otomatik Zaman Takibi • Wiki • Günlük Hatırlatıcı

