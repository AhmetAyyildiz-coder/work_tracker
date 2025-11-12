# 📘 Kaos Kontrol - Detaylı Kullanım Kılavuzu

**Dinamik İş Akışı Yönetim Aracı**  
Versiyon: 1.1 (MVP + Aktivite Takibi + Dosya Yönetimi)  
Tarih: 12 Kasım 2025  
**Yeni Özellikler:** 💬 Aktivite Timeline | 📎 Dosya Yönetimi | 📝 Yorum Sistemi

---

## 📑 İçindekiler

1. [Hızlı Başlangıç](#hızlı-başlangıç)
2. [Modül Detayları](#modül-detayları)
3. [İş Detayı ve Aktivite Takibi](#iş-detayı-ve-aktivite-takibi)
4. [Dosya Yönetimi](#dosya-yönetimi)
5. [Kullanım Senaryoları](#kullanım-senaryoları)
6. [İpuçları ve En İyi Uygulamalar](#ipuçları-ve-en-iyi-uygulamalar)
7. [Sık Sorulan Sorular](#sık-sorulan-sorular)
8. [Klavye Kısayolları](#klavye-kısayolları)

---

## 🚀 Hızlı Başlangıç

### İlk Açılış

Uygulama ilk açıldığında otomatik olarak:
- LocalDB'de `work_tracker` veritabanı oluşturulur
- Demo Proje ve 4 modül (SQL, Ekran, API, Rapor) eklenir
- Kanban ve Scrum sütun ayarları tanımlanır

### 5 Dakikada İş Takibi

**1. Projenizi Tanımlayın (2 dk)**
```
Ana Sayfa → Ayarlar → Projeler → Yeni Proje
Örnek: "CRM Sistemi", "ERP Modernizasyonu"
```

**2. Modülleri Ekleyin (1 dk)**
```
Ana Sayfa → Ayarlar → Modüller → Yeni Modül
Proje seçin ve modül ekleyin: SQL, Ekran, API, Rapor, Test vb.
```

**3. İlk İş Talebini Oluşturun (1 dk)**
```
İş Akışı → Gelen Kutusu → Yeni İş Talebi
Başlık, açıklama girin → Kaydet
```

**4. Triage Yapın (30 sn)**
```
İş seçin → Triage'e Gönder →
Tip: AcilArge, Aciliyet: Kritik, Hedef: Kanban → Kaydet ve Yönlendir
```

**5. Kanban'da Takip Edin (30 sn)**
```
İş Akışı → Kanban Panosu →
Kartı sürükle-bırak ile "Müdahale Ediliyor" sütununa taşı
```

---

## 🎯 Modül Detayları

### 1️⃣ Gelen Kutusu (Inbox)

**Amaç:** Tüm iş taleplerinin merkezi toplama noktası

**Ana Özellikler:**
- ✅ Yeni iş talebi oluşturma
- ✅ Mevcut talepleri düzenleme
- ✅ Talepleri silme
- ✅ Triage'e yönlendirme
- ✅ Filtreleme ve arama

**Alanlar:**
- **Başlık** (zorunlu): Kısa ve öz başlık
- **Açıklama:** Detaylı açıklama
- **Talep Eden** (zorunlu): Kişi adı
- **Talep Tarihi:** Otomatik doldurulur
- **Proje:** İlgili proje (opsiyonel)
- **Modül:** İlgili modül (opsiyonel, proje seçildikten sonra aktif olur)
- **İlişkili Toplantı:** Eğer toplantıdan geliyorsa otomatik bağlanır

**İş Akışı:**
```
Yeni Talep → Gelen Kutusu → Triage → Kanban/Scrum → Tamamlandı
```

**Örnek Kullanım:**
```
Başlık: "Müşteri login sayfası hata veriyor"
Açıklama: "Chrome'da login butonuna basınca 500 hatası alınıyor.
           Sadece production ortamda görülüyor."
Talep Eden: "Ahmet Yılmaz (Müşteri Hizmetleri)"
Proje: CRM Sistemi
Modül: Ekran
```

---

### 2️⃣ Triage (Sınıflandırma)

**Amaç:** İşleri kategorize edip doğru panoya yönlendirme

**Sınıflandırma Kriterleri:**

| Alan | Seçenekler | Açıklama |
|------|-----------|----------|
| **İş Tipi** | AcilArge, Bug, YeniÖzellik, İyileştirme, Diğer | İşin karakteri |
| **Aciliyet** | Kritik, Yüksek, Normal, Düşük | Öncelik seviyesi |
| **Tahmini Efor** | Gün cinsinden | Planlama için |
| **Hedef Pano** | Kanban, Scrum | Yönlendirme hedefi |

**Karar Matrisi:**

```
┌─────────────────────┬──────────┬─────────────┐
│ İş Karakteristiği   │ Aciliyet │ Hedef Pano  │
├─────────────────────┼──────────┼─────────────┤
│ Sistem Çökmesi      │ Kritik   │ → KANBAN    │
│ Müşteri Şikayeti    │ Yüksek   │ → KANBAN    │
│ Kritik Bug          │ Kritik   │ → KANBAN    │
│ Yeni Özellik        │ Normal   │ → SCRUM     │
│ İyileştirme         │ Düşük    │ → SCRUM     │
│ Planlı Geliştirme   │ Normal   │ → SCRUM     │
└─────────────────────┴──────────┴─────────────┘
```

**Örnek Senaryolar:**

**Senaryo 1: Kritik Bug**
```
İş: "Veritabanı bağlantısı kopuyor"
Tip: Bug
Aciliyet: Kritik
Efor: 0.5 gün
Hedef: Kanban → Müdahale Ediliyor
```

**Senaryo 2: Yeni Özellik**
```
İş: "Excel'e export butonu ekle"
Tip: YeniÖzellik
Aciliyet: Normal
Efor: 2 gün
Hedef: Scrum → SprintBacklog
```

---

### 3️⃣ Kanban Panosu (Acil İşler)

**Amaç:** Acil işleri hızlı akış ile yönetme

**Sütunlar ve Anlamları:**

| Sütun | Durum | WIP Limit | Açıklama |
|-------|-------|-----------|----------|
| **Gelen Acil İşler** | Yeni | - | Triage'den gelen işler |
| **Sırada** | Bekliyor | - | Öncelik sırasına göre bekleyenler |
| **Müdahale Ediliyor** | Aktif | **3** | Şu an üzerinde çalışılanlar |
| **Doğrulama Bekliyor** | Test | - | Tamamlanıp test bekleyenler |
| **Çözüldü** | Bitti | - | Kapatılan işler |

**WIP (Work In Progress) Limiti:**
- "Müdahale Ediliyor" sütununda **maksimum 3 iş** olabilir
- Limit doluysa yeni iş taşınamaz
- Amaç: Çok fazla işe aynı anda başlanmasını önlemek

**Sürükle-Bırak Kullanımı:**
```
1. Bir iş kartını fare ile tutun
2. Hedef sütuna sürükleyin
3. Bırakın
→ Durum otomatik güncellenir
```

**İş Kartı Bilgileri:**
```
┌─────────────────────────────┐
│ #123 - Login Hatası Düzelt  │ ← ID ve Başlık
│ AcilArge | Kritik            │ ← Tip ve Aciliyet
│ Ahmet Yılmaz                 │ ← Talep Eden
│ 📅                           │ ← Toplantı ikonu (varsa)
└─────────────────────────────┘
```

**Örnek Akış:**
```
Pazartesi 09:00 → Gelen Acil İşler
Pazartesi 10:00 → Sırada
Pazartesi 14:00 → Müdahale Ediliyor
Salı 11:00      → Doğrulama Bekliyor
Salı 15:00      → Çözüldü ✅
```

---

### 4️⃣ Toplantılar

**Amaç:** Toplantı notlarını kaydetmek ve aksiyonları iş talebine dönüştürmek

**Ana Özellikler:**
- ✅ Toplantı konusu ve tarihi
- ✅ Katılımcı listesi
- ✅ **Zengin metin editörü (RichEdit)**
  - 🖼️ **Resim ekleme** (Ctrl+V ile yapıştır)
  - 📋 **Tablo oluşturma**
  - 🔗 **Hyperlink (bağlantı) ekleme**
  - 📝 **Metin formatlama** (kalın, italik, renkler)
  - 📑 **Liste ve numaralandırma**
- ✅ **Seçili metni iş talebine dönüştürme**
- ✅ **Tam ekran detay görünümü**
- ✅ Toplantıdan çıkan işleri izleme

**İş Talebine Dönüştürme Adımları:**

```
1. Toplantı oluştur/aç
2. Notları yaz (örn: "CRM login ekranı yavaş açılıyor, optimize edilmeli")
3. Metni seç (fare ile vurgula)
4. "İş Talebine Dönüştür" butonuna tıkla
5. Form açılır → İlk satır başlık, geri kalanı açıklama olur
6. Toplantı otomatik bağlanır
7. Kaydet → Gelen Kutusu'na düşer
```

**Zengin Metin Editörü Özellikleri:**

**🖼️ Resim Ekleme:**
```
Yöntem 1: Sağ tık → Insert → Picture
Yöntem 2: Ctrl+C (ekran görüntüsü) → Ctrl+V (editöre yapıştır)
Yöntem 3: Resim dosyasını sürükle-bırak
```
*Örnek: Toplantı sunumundaki diyagramları direkt yapıştırın*

**📋 Tablo Oluşturma:**
```
Sağ tık → Insert → Table → Satır/Sütun sayısı belirle
```
*Örnek: Karar tablosu veya aksiyon listesi*

**📝 Metin Formatlama:**
```
Ctrl+B : Kalın (Bold)
Ctrl+I : İtalik
Ctrl+U : Alt çizgi
Font, boyut, renk seçenekleri mevcut
```

**🔗 Hyperlink Ekleme:**
```
Metin seç → Ctrl+K → URL gir
```
*Örnek: Teams toplantı linkini, Jira ticket'ını ekleyin*

**📑 Liste ve Numaralandırma:**
```
- Madde işaretli liste
1. Numaralı liste
```

---

**Toplantı Notları Örnek Formatı:**
```
PROJE PLANLAMA TOPLANTISI - 11.11.2025

Katılımcılar:
- Ahmet (PO)
- Mehmet (Dev)
- Ayşe (Test)

[RESİM: Sistem mimarisi diyagramı yapıştırıldı]

Kararlar:
1. Login sayfası öncelikli çalışılacak
2. API dokümantasyonu eksik, tamamlanmalı
3. Test ortamı yavaş, sunucu yükseltilecek

[TABLO: Sprint plan tablosu]
| Özellik    | Efor | Öncelik |
|------------|------|---------|
| Login fix  | 2 gün| Yüksek  |
| API doc    | 1 gün| Normal  |

Aksiyonlar:
→ [Seç ve dönüştür] Login sayfası performans optimizasyonu
→ [Seç ve dönüştür] API dokümantasyonu hazırlama
→ [Seç ve dönüştür] Test sunucusu upgrade
```

**Toplantı İzleme:**
```
Toplantı seç → Detayları Göster →
Alt kısımda: Bu toplantıdan gelen 3 iş talebi
- #124 Login performans (Kanban - Müdahale Ediliyor)
- #125 API dokümantasyon (Inbox - Bekliyor)
- #126 Sunucu upgrade (Scrum - SprintBacklog)
```

---

### 5️⃣ Projeler ve Modüller

**Proje Yönetimi:**

Projeniz için temel organizasyon birimi.

**Örnek Projeler:**
```
- CRM Sistemi
- ERP Modernizasyonu
- Mobil Uygulama
- Web Sitesi Yenileme
- Veri Tabanı Migrasyonu
```

**Modül Yönetimi:**

Her proje altında kategoriler oluşturur.

**Örnek Modüller:**
```
CRM Sistemi
  ├─ SQL (Veritabanı işleri)
  ├─ Ekran (UI geliştirme)
  ├─ API (Backend servisler)
  ├─ Rapor (Raporlama modülü)
  └─ Test (Test süreçleri)
```

**Modül Kullanım Faydaları:**
- İşleri kategorize etmek
- İş dağılımını görmek (hangi alanda çok iş var?)
- Uzmanlaşma bazlı atama
- Raporlama (MVP sonrası)

---

## 📝 İş Detayı ve Aktivite Takibi

**Versiyon:** 1.1+  
**Yeni Özellik:** İşler üzerinde yorum yapma, durum güncelleme ve tam aktivite geçmişi

### İş Detay Ekranına Erişim

**✨ YENİ: Tek Tıklama ile Açılır!**

**Yöntem 1: Tek Tıklama (Önerilen)** 🆕
```
Kanban/Scrum Board → İş kartına tek tıkla → Detay ekranı açılır
```

**Önemli:** Kartın **her yerine** tıklayabilirsiniz:
- Başlığa tıklayın ✓
- Badge'lere (etiketler) tıklayın ✓
- İkonlara tıklayın ✓
- Boş alana tıklayın ✓
- 🔍 Info ikonuna tıklayın ✓

**Hover (Üzerine Gelme) Efekti:**
- Kart üzerine gelindiğinde mavi gölge ve çerçeve görünür
- El işareti cursor (Cursors.Hand) belirir
- "Detayları görmek için tıklayın" tooltip'i

**Yöntem 2: Sağ Tık Menüsü** *(gelecek versiyonda)*
```
İş kartı → Sağ Tık → Detayları Göster
```

### İş Detay Ekranı Yapısı

**Üst Bölüm: İş Bilgileri**
```
┌─────────────────────────────────────────────────┐
│ İş Detayı: #123 - Login Performans Sorunu     │
├─────────────────────────────────────────────────┤
│ SOL SÜTUN              │ SAĞ SÜTUN             │
│ Başlık                 │ Durum (değiştirilebilir)│
│ Açıklama               │ Tip                   │
│ Talep Eden             │ Aciliyet              │
│ Talep Tarihi           │ Sprint                │
│ Proje                  │ Pano                  │
│ Modül                  │ Efor (gün)            │
│                        │                       │
│ Oluşturulma: 05.11.25  │ Tamamlanma: -         │
└─────────────────────────────────────────────────┘
```

**Orta Bölüm: Sekmeler**
```
┌─────────────────────────────────────────────────┐
│ [📋 Aktivite Geçmişi] [📎 Dosyalar]           │
└─────────────────────────────────────────────────┘
```

**Alt Bölüm: Yorum ve Durum Güncelleme**
```
┌─────────────────────────────────────────────────┐
│ Yeni Yorum veya Durum Güncelleme                │
│ ┌─────────────────────────────────────────────┐ │
│ │ Yorum yazın...                              │ │
│ └─────────────────────────────────────────────┘ │
│ [💬 Yorum Ekle] [📊 Durum Değiştir]           │
└─────────────────────────────────────────────────┘
```

### 📋 Aktivite Geçmişi (Timeline)

**Görüntülenen Aktiviteler:**

| İkon | Aktivite Tipi | Açıklama |
|------|--------------|----------|
| ✨ | Oluşturuldu | İş ilk kez oluşturuldu |
| 💬 | Yorum | Ekip üyesi not ekledi |
| 📊 | Durum | Durum değişti ("Geliştirmede" → "Testte") |
| 👤 | Atama | Atama değişti |
| ✏️ | Güncelleme | Alan güncellendi |
| ⚡ | Öncelik | Öncelik değişti |
| ⏱️ | Efor | Efor tahmini güncellendi |
| 📎 | Dosya | Dosya eklendi/silindi |

**Aktivite Listesi Formatı:**
```
┌──────────────────────────────────────────────────┐
│ Tarih/Saat    │ Tip     │ Açıklama      │ Kişi   │
├──────────────────────────────────────────────────┤
│ 11.11 14:30  │ 💬 Yorum │ Analiz tamam  │ Ahmet  │
│ 10.11 16:45  │ 📊 Durum │ Geliş→Test    │ Mehmet │
│ 10.11 09:15  │ 📎 Dosya │ fix.sql ekl.  │ Ayşe   │
│ 08.11 11:00  │ ✨ Oluş. │ İş oluşturuldu│ Sistem │
└──────────────────────────────────────────────────┘
```

### 💬 Yorum Ekleme

**Adım 1:** İş detayında alt bölümdeki metin kutusuna yazın
```
"Müşteri ile görüştüm. Sorun sadece Chrome'da görülüyor. 
Firefox'ta normal çalışıyor."
```

**Adım 2:** **💬 Yorum Ekle** butonuna tıklayın

**Adım 3:** Yorum timeline'da görünür
```
12.11.2025 10:30 | 💬 Yorum | "Müşteri ile görüştüm..." | Ali Demir
```

**Yorum Kullanım Senaryoları:**
- 📋 Analiz notları
- 🐛 Hata detayları
- 💡 Çözüm önerileri
- 🔍 Test sonuçları
- 📞 Müşteri geri bildirimleri
- ⏳ Gecikme sebepleri

### 📊 Durum Değiştirme

**Adım 1:** Üst bölümde **Durum** dropdown'ından yeni durumu seçin
```
Mevcut: "Geliştirmede"
Yeni: "Testte"
```

**Adım 2:** Alt bölümde **📊 Durum Değiştir** butonuna tıklayın

**Adım 3:** Sistem otomatik aktivite kaydeder
```
12.11.2025 15:45 | 📊 Durum | Geliştirmede → Testte | Ali Demir
```

**Durum Değişikliği Örnekleri:**

**Kanban için:**
```
Gelen Acil İşler → Sırada → Müdahale Ediliyor → 
Doğrulama Bekliyor → Çözüldü
```

**Scrum için:**
```
Sprint Backlog → Geliştirmede → Testte → Tamamlandı
```

### 🎯 İş Kartlarında Aktivite Göstergeleri

**Kanban/Scrum Board'da her kart üzerinde:**

```
┌─────────────────────────┐
│ #123 - Login Hatası     │
│ AcilArge | Kritik        │
│                         │
│ 👤 Ahmet  ⏱ 2g         │
│ 💬 5  📎 3  📅         │  ← Göstergeler
└─────────────────────────┘
```

**Gösterge Anlamları:**
- **💬 5**: 5 aktivite/yorum var
- **📎 3**: 3 ekli dosya var
- **📅**: Toplantıdan gelen iş

### ✨ Aktivite Takibi Avantajları

**1. Şeffaflık:**
- Her değişiklik kayıt altında
- Kim ne zaman ne yaptı görünür
- Sorumluluk takibi kolay

**2. İletişim:**
- Yorum sistemi ile ekip içi iletişim
- Bilgi paylaşımı kolaylaşır
- Tekrar soru sorma azalır

**3. Geçmiş Takibi:**
- İş üzerinde ne oldu, ne zaman oldu
- Karar geçmişi
- Süreç iyileştirme için veri

**4. Test Modu Desteği:**
- "İş bitti ama test modunda" durumu
- Yorumlarla güncelleme
- Sürekli iletişim

### 📝 Örnek Kullanım: Test Modundaki İş

**Senaryo:** İş tamamlandı, test ortamında. 5 gün sonra müşteri ek istek ekledi.

**Adım 1:** İş kartına tıklayın (tek tıklama yeterli!)
```
İş: #145 - Müşteri Raporu
Durum: Testte
```

**Adım 2:** Yorum ekleyin
```
"Müşteri geri dönüşü: Excel çıktısına toplam satırı da eklenmeli.
Ek geliştirme gerekiyor. Tahmini: +0.5 gün"
```

**Adım 3:** Durumu değiştirin
```
Testte → Geliştirmede
```

**Sonuç:** Tüm geçmiş korundu, yeni istek kayıt altına alındı! ✅

---

## 📎 Dosya Yönetimi

**Versiyon:** 1.1+  
**Yeni Özellik:** İşlere SQL scriptleri, dokümanlar ve her tür dosya ekleme

### Dosya Sistemi Yapısı

**Fiziksel Depolama:**
```
WorkItemAttachments/
  ├── WorkItem_123/
  │   ├── a1b2c3d4.sql      (fix_login_bug.sql)
  │   ├── e5f6g7h8.pdf      (analiz_raporu.pdf)
  │   └── i9j0k1l2.png      (ekran_goruntusu.png)
  ├── WorkItem_124/
  │   └── m3n4o5p6.docx     (gereksinimler.docx)
  └── WorkItem_125/
      ├── q7r8s9t0.sql
      └── u1v2w3x4.xlsx
```

**Özellikler:**
- ✅ Her iş için ayrı klasör
- ✅ Dosyalar GUID ile unique
- ✅ Orijinal dosya adları korunur
- ✅ Cascade delete: İş silinince dosyalar da silinir

### 📁 Dosya Ekleme

**Adım 1:** İş detayını açın
```
İş kartı → Tek tıkla (kartın herhangi bir yerine!)
```

**Adım 2:** **📎 Dosyalar** sekmesine geçin
```
[📋 Aktivite Geçmişi] [📎 Dosyalar] ← Tıkla
```

**Adım 3:** **📁 Dosya Ekle** butonuna tıklayın

**Adım 4:** Dosyaları seçin
```
Ctrl tuşuna basılı tutarak birden fazla dosya seçebilirsiniz!
```

**Adım 5:** Dosyalar yüklenir ve listelenir
```
┌────────────────────────────────────────────────────┐
│ 🗄️ fix_login_bug.sql    │ 2.5 KB  │ Ahmet │ 12.11│
│ 📕 analiz_raporu.pdf     │ 145 KB  │ Mehmet│ 11.11│
│ 🖼️ ekran_goruntusu.png   │ 82 KB   │ Ayşe  │ 10.11│
└────────────────────────────────────────────────────┘

Toplam 3 dosya | Toplam Boyut: 229.5 KB
```

### 📂 Desteklenen Dosya Tipleri

| İkon | Dosya Tipi | Uzantılar |
|------|-----------|-----------|
| 🗄️ | SQL | .sql |
| 📕 | PDF | .pdf |
| 📘 | Word | .doc, .docx |
| 📗 | Excel | .xls, .xlsx |
| 📝 | Metin | .txt |
| 🖼️ | Resim | .jpg, .png, .gif, .bmp |
| 📦 | Arşiv | .zip, .rar, .7z |
| 💻 | Kod | .cs, .js, .py, .java, .ts, .vb |
| 📋 | Veri | .xml, .json, .yaml |
| 📄 | Diğer | Tüm dosya tipleri |

### 💾 Dosya İndirme

**Adım 1:** Dosya listesinden dosya seçin

**Adım 2:** **💾 İndir** butonuna tıklayın

**Adım 3:** Kaydetmek istediğiniz yeri seçin
```
"Nereye kaydedilsin?" diyalogu açılır
```

**Adım 4:** Dosya bilgisayarınıza kaydedilir
```
✅ "Dosya başarıyla indirildi"
```

### 📂 Dosya Açma

**Adım 1:** Dosya listesinden dosya seçin

**Adım 2:** **📂 Aç** butonuna tıklayın

**Dosya Açılış Davranışı:**
```
.sql  → SQL Server Management Studio / SSMS
.pdf  → Adobe Reader / Chrome
.docx → Microsoft Word
.xlsx → Microsoft Excel
.png  → Windows Fotoğraf Görüntüleyici
.txt  → Notepad
```

*Not: Dosya varsayılan uygulama ile açılır*

### 🗑️ Dosya Silme

**Adım 1:** Dosya listesinden dosya seçin

**Adım 2:** **🗑️ Sil** butonuna tıklayın

**Adım 3:** Onay verin
```
⚠️ "fix_login_bug.sql dosyasını silmek istediğinizden emin misiniz?
Bu işlem geri alınamaz!"

[Evet] [Hayır]
```

**Adım 4:** Dosya silinir
```
✅ Fiziksel dosya silinir
✅ Veritabanı kaydı silinir
✅ Aktivite timeline'a eklenir: "Dosya silindi: fix_login_bug.sql"
```

### 🎯 Dosya Yönetimi Kullanım Senaryoları

**1. SQL Script Saklama**
```
Senaryo: Veritabanı hata düzeltmesi
Dosya: fix_customer_report.sql

Adımlar:
1. Hatayı analiz et
2. Düzeltme script'i yaz
3. İşe script'i ekle
4. Test ekibi script'i indirir ve test ortamında çalıştırır
5. Onay sonrası production'a alınır

Avantaj: Script kaybolmaz, versiyon kontrolü olur
```

**2. Ekran Görüntüsü Ekleme**
```
Senaryo: Kullanıcı hata bildirimi
Dosyalar: 
- hata_ekrani.png
- beklenen_sonuc.png

Adımlar:
1. Kullanıcıdan hata ekran görüntüsü al
2. İşe ekle
3. Çözüm sonrası doğru ekran görüntüsü de ekle
4. Karşılaştırma yapılabilir

Avantaj: Görsel kanıt, tekrar sorma gerekliliği azalır
```

**3. Analiz Dokümanı**
```
Senaryo: Karmaşık geliştirme
Dosya: teknik_analiz.docx

İçerik:
- Mevcut durum analizi
- Önerilen çözüm
- Risk analizi
- Efor tahmini

Avantaj: Karar süreci dokümante edilir
```

**4. Performans Test Sonuçları**
```
Senaryo: Optimizasyon işi
Dosyalar:
- before_performance.xlsx (önceki durum)
- after_performance.xlsx (sonraki durum)
- query_optimization.sql

Avantaj: Karşılaştırmalı analiz, ölçülebilir sonuçlar
```

**5. API Dokümantasyonu**
```
Senaryo: API geliştirme
Dosyalar:
- api_specs.json (OpenAPI/Swagger)
- postman_collection.json
- sample_request.txt
- sample_response.txt

Avantaj: Tam dokümantasyon bir arada
```

### 📊 İş Kartlarında Dosya Göstergeleri

**Board'larda görünüm:**
```
┌─────────────────────────┐
│ #145 - Rapor Hatası     │
│ Bug | Yüksek              │
│                         │
│ 👤 Mehmet  ⏱ 1.5g      │
│ 💬 3  📎 5  📅         │  ← 📎 5: 5 dosya var!
└─────────────────────────┘
```

**Dosya var ise kart üzerinde:**
- **📎** ikonu görünür
- Yanında dosya sayısı (örn: 📎 5)
- Tooltip: "5 ekli dosya"
- Renk: Kırmızı (dikkat çekici)

### 💡 Dosya Yönetimi En İyi Uygulamalar

**1. Dosya Adlandırma:**
```
✅ İYİ:
- fix_login_bug_v2.sql
- customer_requirements_2025-11.docx
- before_optimization.png

❌ KÖTÜ:
- script.sql
- dosya1.txt
- aaaa.png
```

**2. Dosya Organizasyonu:**
```
Bir iş için farklı kategorilerde dosyalar ekleyin:
- 📝 Analiz dokümanları
- 💻 Kod/Script dosyaları
- 🖼️ Ekran görüntüleri
- 📊 Test sonuçları
```

**3. Dosya Boyutu:**
```
✅ Küçük tutun (< 5 MB ideal)
⚠️ Büyük dosyalar (video, büyük PDF) için link kullanın
💡 Ekran görüntülerini sıkıştırın
```

**4. Güvenlik:**
```
⚠️ Şifre içeren dosyalar EKLEMEYIN
⚠️ Hassas bilgiler varsa şifreli dosya kullanın
✅ Gerekirse sadece dosya linkini yorum olarak ekleyin
```

### 🔍 Dosya Arama (Gelecek Versiyonda)

**Planlanan Özellikler:**
- Dosya içerik araması
- Dosya tipi filtreleme
- Tarih aralığı filtreleme
- Dosya yükleyen kişiye göre filtreleme

---

## 📖 Kullanım Senaryoları

### Senaryo 1: Müşteri Bug Bildirimi

**Durum:** Müşteriden telefon geldi: "Sipariş verirken hata alıyorum"

**Adımlar:**
```
1. Gelen Kutusu → Yeni İş Talebi
   Başlık: "Sipariş formu hata veriyor"
   Açıklama: "Müşteri: ABC Ltd. / İlgili Kişi: Ali Bey
              Chrome'da sipariş adımında 'undefined' hatası"
   Talep Eden: "Müşteri Hizmetleri - Zeynep"
   Proje: CRM Sistemi
   Modül: Ekran

2. İş Talebi oluştu → #127

3. Triage ekranı aç (#127)
   Tip: Bug
   Aciliyet: Kritik
   Efor: 1 gün
   Hedef: Kanban

4. Kaydet ve Yönlendir → Kanban'da "Gelen Acil İşler"

5. Kanban Panosu aç
   Kartı "Müdahale Ediliyor"ye taşı

6. Düzelt → "Doğrulama Bekliyor"ye taşı

7. Test OK → "Çözüldü"ye taşı ✅
```

---

### Senaryo 2: Sprint Planlama Toplantısı

**Durum:** Haftalık sprint planlama toplantısı yapıldı

**Adımlar:**
```
1. Toplantılar → Yeni Toplantı
   Konu: "Sprint 42 Planlama Toplantısı"
   Tarih: 11.11.2025 14:00
   Katılımcılar: "Ahmet, Mehmet, Ayşe, Fatma"

2. Toplantı Notları:
   "Bu sprint'te şu özellikler geliştirilecek:
    
    1. Kullanıcı profil sayfası redesign
    2. Excel export özelliği
    3. E-posta bildirimleri"

3. Her satırı seç ve "İş Talebine Dönüştür":
   
   → #128: Kullanıcı profil sayfası redesign
   → #129: Excel export özelliği
   → #130: E-posta bildirimleri

4. Her biri Gelen Kutusu'na düştü

5. Hepsini tek tek Triage'e gönder:
   - #128: YeniÖzellik, Normal, 3 gün, Scrum
   - #129: YeniÖzellik, Normal, 2 gün, Scrum
   - #130: İyileştirme, Düşük, 1 gün, Scrum

6. Artık Scrum panosu hazır! (MVP'de sadece Kanban var, 
   Scrum panosu sonraki versiyonda gelecek)
```

---

### Senaryo 3: Haftalık Durum Toplantısı

**Durum:** Yönetimle durum güncellemesi toplantısı

**Öncesi Hazırlık:**
```
1. Toplantılar ekranı aç
2. Son toplantı: "Sprint 41 Planlama"
3. Detayları Göster → Alt kısımda gelen işler:
   - #120: Tamamlandı ✅
   - #121: Müdahale Ediliyor 🔄
   - #122: Bekliyor ⏳

→ Toplantıda bu bilgileri paylaş
```

---

### Senaryo 4: Test Modunda İş - Ek İstek Geldi 🆕

**Durum:** İş tamamlandı, test ortamında. 5 gün sonra müşteri yeni istek ekledi.

**Adımlar:**
```
1. İş: #145 - Müşteri Raporu Excel Export
   Durum: Testte
   5 gün test ortamında, müşteri kullanıyor

2. Müşteri araması: "Raporda toplam satırı da olmalı"

3. Kanban'da kartı bul → Tek tıkla (hover efektini görün!)

4. İş Detay Ekranı açıldı
   → 📋 Aktivite Geçmişi sekmesinde:
   08.11 - İş oluşturuldu
   09.11 - Geliştirmede → Testte
   
5. Alt kısımda yorum yaz:
   "Müşteri geri dönüşü: 
    - Excel çıktısına toplam satırı eklenmeli
    - Her sütun için SUM() hesaplansın
    - Ek geliştirme gerekiyor
    Tahmini: +0.5 gün"

6. 💬 Yorum Ekle → Yorum kaydedildi

7. Üstte Durum: Testte → Geliştirmede

8. 📊 Durum Değiştir → Durum güncellendi

9. Timeline'da göründü:
   12.11 - 💬 Yorum: "Müşteri geri dönüşü..." - Sen
   12.11 - 📊 Durum: Testte → Geliştirmede - Sen

10. Geliştirmeyi yap → Yeni yorum ekle:
    "Toplam satırı eklendi. Tekrar teste alınabilir."

11. Durum: Geliştirmede → Testte

✅ Sonuç: Tüm geçmiş korundu, müşteri istekleri kayıt altında!
```

**Avantajlar:**
- İş kaybı yok
- İletişim geçmişi kayıtlı
- Neden gecikti? Kayıtlarda görünüyor
- Ekip şeffaf çalışıyor

---

### Senaryo 5: SQL Script ile Hata Düzeltme 🆕

**Durum:** Production'da kritik hata, SQL script ile düzeltme gerekiyor

**Adımlar:**
```
1. Acil iş oluştur:
   Başlık: "Müşteri raporu yanlış veri gösteriyor"
   Kanban → Gelen Acil İşler

2. İş kartına tıkla → İş Detay Ekranı (tek tıklama yeterli!)

3. Analiz yap, sorunu bul:
   "WHERE koşulunda tarih filtresi yanlış"

4. Düzeltme script'i yaz:
   fix_customer_report_date_filter.sql

5. İş detayında → 📎 Dosyalar sekmesi

6. 📁 Dosya Ekle → script'i seç

7. Dosya yüklendi:
   🗄️ fix_customer_report_date_filter.sql | 1.2 KB
   
8. Timeline'da:
   📎 Dosya eklendi: fix_customer_report_date_filter.sql

9. Yorum ekle:
   "Script hazır. Test ortamında denendi, çalışıyor.
    Production'a alınması için onay bekleniyor."

10. Test ekibi:
    - İş detayını aç
    - Dosyalar sekmesi → script'i 💾 İndir
    - Test ortamında çalıştır
    - Yorum ekle: "Test OK ✅"

11. Production onayı sonrası:
    - Yorum: "Production'a alındı"
    - Durum: Çözüldü

✅ Sonuç: Script kaybolmadı, versiyon kontrolü yapıldı!
```

**Dosya Avantajları:**
- Script güvenli saklandı
- Herkes aynı script'i kullandı
- İleride benzer sorun olursa referans var
- Audit trail tam

---

### Senaryo 6: Ekran Görüntüsü ile Hata Raporlama 🆕

**Durum:** Kullanıcı hata bildirdi, ekran görüntüsü gönderdi

**Adımlar:**
```
1. Mail geldi, içinde 2 ekran görüntüsü:
   - hata_ekrani.png
   - beklenen_sonuc.png

2. İş oluştur:
   Başlık: "Login sayfası yükleme hatası"
   Kanban → Sırada

3. İş detayı aç → 📎 Dosyalar

4. 📁 Dosya Ekle → iki görüntüyü birden seç (Ctrl+Click)

5. Dosyalar yüklendi:
   🖼️ hata_ekrani.png | 142 KB
   🖼️ beklenen_sonuc.png | 98 KB

6. Yorum ekle:
   "Kullanıcı: Chrome 118, Windows 10
    Sadece sabah saatlerinde (08:00-09:00) oluyor
    Muhtemelen yüksek trafik problemi"

7. Geliştirici işe baktı:
   - Dosyaları 📂 Aç → Görüntüleri inceledi
   - Sorunu tespit etti
   - Yorum: "Cache mekanizması eksikmiş, eklendi"

8. Çözüm sonrası:
   - Düzeltilmiş ekran görüntüsü ekle: fixed_login.png
   - Yorum: "Düzeltildi, test edilebilir"

9. Test ekibi:
   - 3 görüntüyü karşılaştır
   - Yorum: "Test OK, sorun giderilmiş ✅"

✅ Sonuç: Görsel kanıt sayesinde sorun net anlaşıldı!
```

**Görüntü Avantajları:**
- "Nasıl bir hata?" sorusunu ortadan kaldırır
- Tekrar üretme gerekliliği yok
- Önce/sonra karşılaştırması yapılabilir
- Dokümantasyon

---

### Senaryo 7: Karmaşık İş - Dokümantasyon ile 🆕

**Durum:** Büyük bir refactoring işi, detaylı analiz gerekiyor

**Adımlar:**
```
1. İş oluştur:
   Başlık: "Ödeme modülü refactoring"
   Scrum → Sprint Backlog
   Efor: 8 gün

2. İş detayı aç

3. Analiz dokümanı hazırla (Word):
   - Mevcut durum analizi
   - Sorunlar
   - Önerilen mimari
   - Risk analizi
   - Implementasyon planı

4. 📎 Dosyalar → 📁 Dosya Ekle
   📘 payment_refactoring_analysis.docx

5. Tasarım diyagramları ekle:
   🖼️ current_architecture.png
   🖼️ proposed_architecture.png

6. Yorum: "Analiz tamamlandı. PO onayı bekleniyor."

7. PO dokümanı indirir, inceler:
   💾 İndir → Oku
   Yorum: "Analiz onaylandı. Sprint'e alınabilir."

8. Geliştirme sırasında:
   - Her gün yorum ekle (progress update)
   - Kod örnekleri ekle: sample_payment.cs
   - Test script'leri ekle: payment_tests.sql

9. Tamamlandığında:
   📎 Dosyalar:
   - 📘 payment_refactoring_analysis.docx
   - 🖼️ current_architecture.png  
   - 🖼️ proposed_architecture.png
   - 💻 sample_payment.cs
   - 🗄️ payment_tests.sql
   - 📗 performance_comparison.xlsx
   
   💬 Aktiviteler: 15 yorum/güncelleme

✅ Sonuç: Proje tam dokümante edildi, gelecek referans!
```

**Dokümantasyon Avantajları:**
- Karar süreci kayıtlı
- Neden bu çözüm seçildi? Dokümanlar gösteriyor
- Yeni ekip üyesi işi anlamak için dosyalara bakabilir
- Knowledge base oluştu

---

## 💡 İpuçları ve En İyi Uygulamalar

### ✅ Yapılması Gerekenler

**1. Her Talebi Kaydet**
```
❌ Yanlış: "Telefonda söyledim, yaparsın"
✅ Doğru: Her talep Gelen Kutusu'na kayıt
```

**2. Triage'i Atla ma**
```
❌ Yanlış: Direkt Kanban'a manuel ekleme
✅ Doğru: Inbox → Triage → Kanban akışı
```

**3. WIP Limitini Koru**
```
❌ Yanlış: 10 işe aynı anda başla
✅ Doğru: Max 3 iş "Müdahale Ediliyor"de
```

**4. Toplantı Notlarını İş Yap**
```
❌ Yanlış: Word'e yaz, unutulsun
✅ Doğru: Not al → Seç → İş Talebine Dönüştür
```

**5. Düzenli İzle**
```
✅ Her gün: Kanban panosunu güncelle
✅ Her hafta: Gelen Kutusu'nu temizle
✅ Toplantı sonrası: Aksiyonları hemen dönüştür
```

**6. Aktivite ve Yorum Kültürü 🆕**
```
✅ İş üzerinde güncelleme varsa yorum ekle
✅ Test modundaki işlere düzenli yorum at
✅ Durum değiştirirken sebep belirt
✅ Gecikme varsa açıklamasını timeline'a ekle
```

**7. Dosya Yönetimi Disiplini 🆕**
```
✅ SQL script'leri mutlaka ekle
✅ Hata ekran görüntülerini dosya olarak sakla
✅ Analiz dokümanlarını işe bağla
✅ Dosya isimlerini açıklayıcı yap
```

---

### ⚠️ Yapılmaması Gerekenler

```
❌ Telefon/mail ile iş vermek (kayıt yok!)
❌ WIP limitini zorlamak (kaos başlar)
❌ Triage atlamak (önceliklendirme kaybolur)
❌ Toplantı notlarını Word'de tutmak (izlenebilirlik yok)
❌ Eski işleri "Çözüldü"ye taşımamak (pano şişer)
❌ 🆕 Yorum eklemeden durum değiştirmek (neden değişti?)
❌ 🆕 SQL script'leri mail/Teams'te paylaşmak (kaybolur!)
❌ 🆕 Dosyaları anlaşılmaz isimlerle eklemek (script1.sql ❌)
❌ 🆕 Test modundaki işe yorum eklemeden bırakmak
```

---

## ❓ Sık Sorulan Sorular

### Q1: Bir işi yanlış panoya gönderdim, nasıl değiştirebilirim?

**A:** Şu an için Triage ekranından tekrar yönlendirmeniz gerekiyor. İş talebini düzenleyip yeniden Triage'e gönderin.

---

### Q2: WIP limitini değiştirebilir miyim?

**A:** MVP'de WIP limiti kod seviyesinde tanımlı (3). İleriki versiyonlarda ayarlardan yapılabilecek.

**Geçici Çözüm:**
```
Migrations/Configuration.cs dosyasında:
WipLimit = 3  → WipLimit = 5  (örnek)
```

---

### Q3: Sprint panosu nerede?

**A:** MVP'de sadece Kanban var. Scrum panosu (Sprint Backlog → Geliştirmede → Testte → Tamamlandı) bir sonraki versiyonda eklenecek.

---

### Q4: Kullanıcı atama yapabilir miyim?

**A:** MVP'de yok. Şu an sadece "Talep Eden" ve "Triage Yapan" bilgisi tutuluyor. Kullanıcı yönetimi ve atama sistemi MVP sonrası gelecek.

---

### Q5: Raporlar ne zaman gelecek?

**A:** Modül 6 (Raporlama ve Analitik) MVP sonrasında geliştirilecek:
- Kapasite dağılım raporu (Scrum vs Kanban)
- İş dağılım raporu (Proje/Modül bazlı)
- Burndown chart'lar

---

### Q6: Grid'lerde filtreleme nasıl yapılır?

**A:** Tüm grid'lerde otomatik filtre satırı var:
```
1. Grid başlığının hemen altında bir satır var
2. İstediğiniz kolona tıklayıp yazın
3. Otomatik filtrelenir
```

---

### Q7: Veritabanı nerede?

**A:** LocalDB'de:
```
Sunucu: (localdb)\MSSQLLocalDB
Veritabanı: work_tracker
```

SQL Server Management Studio veya Azure Data Studio ile bağlanabilirsiniz.

---

### Q8: Yedekleme nasıl yapılır?

**A:** LocalDB veritabanını yedekleyin:
```sql
BACKUP DATABASE work_tracker
TO DISK = 'C:\Backups\work_tracker.bak'
```

---

### Q9: İş detayına nasıl erişirim? 🆕

**A:** Kanban veya Scrum Board'da iş kartına **tek tıklayın** (çift tıklamaya gerek yok!).
```
İş kartı → Tek tıkla (kartın herhangi bir yerine!) → İş Detay Ekranı açılır
```

**Yeni Özellikler:**
- ✨ Tek tıklama ile açılır
- 🎨 Hover efekti (kart üzerine gelindiğinde mavi gölge)
- 🔍 Clickable info ikonu
- 🖱️ Kartın her yerine tıklanabilir (başlık, etiketler, ikonlar...)

---

### Q10: Yorumları silmek mümkün mü? 🆕

**A:** MVP'de yorum silme yok. Aktiviteler kalıcı kayıttır (audit trail). Yanlış yorum eklediyseniz düzeltme yorumu ekleyin:
```
"Önceki yorumda hata var. Doğrusu: ..."
```

---

### Q11: Dosyalar nerede saklanıyor? 🆕

**A:** Uygulama dizininde:
```
C:\...\work_tracker\bin\Debug\WorkItemAttachments\
  └── WorkItem_123\
      ├── a1b2c3d4.sql
      └── e5f6g7h8.pdf
```

Her iş için ayrı klasör. Yedekleme yaparken bu klasörü de yedekleyin!

---

### Q12: Dosya boyutu limiti var mı? 🆕

**A:** Teknik limit yok ama tavsiyeler:
```
✅ İdeal: < 5 MB
⚠️ Dikkat: 5-20 MB (yavaşlama olabilir)
❌ Önerilmez: > 20 MB (link kullanın)
```

Büyük dosyalar için:
- OneDrive/SharePoint link'i yorum olarak ekleyin
- Veya dosyayı sıkıştırın (.zip)

---

### Q13: Eski aktiviteleri görmek için? 🆕

**A:** İş detayında **📋 Aktivite Geçmişi** sekmesinde tüm geçmiş var:
```
İş kartı → Tek tıkla → Aktivite Geçmişi sekmesi
→ Tarih sıralı tam liste
```

---

### Q14: Birden fazla dosya ekleyebilir miyim? 🆕

**A:** Evet! Dosya ekleme dialogunda:
```
Ctrl tuşuna basılı tutup → Birden fazla dosya seç → Aç
```

Örnek: 3 SQL script + 2 ekran görüntüsü = Tek seferde 5 dosya!

---

### Q15: Dosya açılmıyor, ne yapmalıyım? 🆕

**A:** Dosya varsayılan uygulamayla açılır. Sorun varsa:
```
1. Dosyayı 💾 İndir
2. Bilgisayarınıza kaydedin
3. Manuel olarak uygun programla açın
```

Örnek: `.sql` dosyası Notepad'de açılıyorsa:
- Dosyayı indir → Sağ tık → Birlikte Aç → SSMS

---

### Q16: Test modundaki işe nasıl not eklenir? 🆕

**A:** Tam istediğiniz özellik bu!
```
1. İş kartına tıkla (tek tıklama yeterli!)
2. Alt kısımda yorum yaz:
   "Müşteri ek istek ekledi: ..."
3. 💬 Yorum Ekle → Kayıt edildi
4. İhtiyaçsa durumu değiştir: Testte → Geliştirmede
```

Tüm geçmiş korunur, hiçbir bilgi kaybolmaz!

---

## ⌨️ Klavye Kısayolları

**Grid'lerde:**
```
Ctrl + F        : Hızlı filtre (AutoFilter Row aktifse)
Enter           : Seçili satırı aç/düzenle
Delete          : Seçili satırı sil (onay ister)
F5              : Yenile
Ctrl + Home     : İlk satıra git
Ctrl + End      : Son satıra git
```

**Form'larda:**
```
Ctrl + S        : Kaydet (çoğu formda)
Esc             : İptal / Kapat
Tab             : Sonraki alana geç
Shift + Tab     : Önceki alana geç
```

**Ana Menü:**
```
Alt             : Ribbon menüye odaklan
Alt + F4        : Uygulamayı kapat
```

---

## 📊 Başarı Metrikleri

**İlk Hafta Hedefleri:**
```
✅ En az 10 iş talebi oluşturuldu
✅ Tüm talepler Triage'den geçirildi
✅ Kanban panosu günlük güncelleniyor
✅ 1 toplantı kaydı ve aksiyonları oluşturuldu
```

**İlk Ay Hedefleri:**
```
✅ 50+ iş talebi işlendi
✅ WIP limiti hiç aşılmadı
✅ Tüm toplantılardan aksiyon çıkarıldı
✅ Ekip, telefon/mail yerine sistem kullanıyor
```

---

## 🎓 Eğitim Videoları (Gelecek)

```
📹 01 - Hızlı Başlangıç (5 dk)
📹 02 - Gelen Kutusu ve Triage (10 dk)
📹 03 - Kanban Akışı (8 dk)
📹 04 - Toplantı Yönetimi (12 dk)
📹 05 - Proje ve Modül Organizasyonu (7 dk)
```

---

## 📞 Destek ve Geri Bildirim

**Hata/Bug Bildirimi:**
```
1. Uygulamada: Gelen Kutusu → Yeni İş Talebi
2. Başlık: "[BUG] Açıklama"
3. Proje: "Kaos Kontrol (Sistem)"
4. Tip: Bug
```

**Yeni Özellik Talebi:**
```
1. Uygulamada: Gelen Kutusu → Yeni İş Talebi
2. Başlık: "[FEATURE] Açıklama"
3. Proje: "Kaos Kontrol (Sistem)"
4. Tip: YeniÖzellik
```

---

## 🗺️ Yol Haritası

**Sonraki Versiyonlar:**

**v1.1 (Scrum Panosu)**
- Sprint yönetimi
- Sprint Backlog → Geliştirmede → Testte → Tamamlandı
- Sprint planlama ve kapama

**v1.2 (Kullanıcı Yönetimi)**
- Kullanıcı tanımlama
- İş atama
- Yetkilendirme

**v1.3 (Raporlama)**
- Kapasite raporları
- İş dağılım grafikleri
- Burndown chart'lar

**v2.0 (İleri Özellikler)**
- E-posta entegrasyonu
- Bildirimler
- Dashboard
- Mobil uygulama

---

<p align="center">
<strong>Kaos Kontrol v1.0 (MVP)</strong><br/>
11 Kasım 2025<br/>
<em>Kaosu kontrol altına al, planlı çalış! 🚀</em>
</p>

