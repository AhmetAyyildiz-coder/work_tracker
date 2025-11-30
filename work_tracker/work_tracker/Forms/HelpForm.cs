using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;

namespace work_tracker.Forms
{
    public partial class HelpForm : XtraForm
    {
        public HelpForm()
        {
            InitializeComponent();
        }

        private void HelpForm_Load(object sender, EventArgs e)
        {
            LoadHelpContent();
        }

        private void LoadHelpContent()
        {
            richEditControl1.HtmlText = @"
<h1 style='color: #0078D4;'>📋 Work Tracker - Kapsamlı Kullanım Kılavuzu</h1>
<p style='color: #666; font-size: 12px;'>Sürüm 3.0 • Son Güncelleme: 30 Kasım 2025</p>

<hr/>

<h2 style='color: #106EBE;'>🎯 Uygulama Felsefesi</h2>
<p>Work Tracker, <b>kişisel iş yönetimi</b> için tasarlanmış kapsamlı bir araçtır. Temel felsefesi:</p>
<ul>
  <li><b>Hibrit İş Yönetimi:</b> Planlı işler (Scrum) ve acil işler (Kanban) ayrı yönetilir</li>
  <li><b>İlişkisel Yapı:</b> İşler birbirine bağlanabilir (üst-alt, kardeş ilişkileri)</li>
  <li><b>Otomatik Zaman Takibi:</b> Geliştirme süresi durum değişikliklerinden otomatik hesaplanır</li>
  <li><b>Bilgi Yönetimi:</b> Wiki ile kurumsal bilgi birikimi oluşturulur</li>
  <li><b>Günlük Hatırlatıcı:</b> 17:30'da aktif işleriniz için bildirim alırsınız</li>
</ul>

<hr/>

<h2 style='color: #106EBE;'>⚖️ Kanban vs Scrum - Ne Zaman Hangisi?</h2>
<table border='1' cellpadding='6' style='border-collapse: collapse; width: 100%;'>
  <tr style='background-color: #F3F3F3;'>
    <th>Başlık</th>
    <th>Kanban</th>
    <th>Scrum</th>
  </tr>
  <tr>
    <td><b>Odak</b></td>
    <td>Akış (Flow), anlık talepler</td>
    <td>Zaman kutusu (Sprint), planlı kapsam</td>
  </tr>
  <tr>
    <td><b>Zaman</b></td>
    <td>Sabit süre yok; iş geldikçe akar</td>
    <td>1-4 hafta sprintler</td>
  </tr>
  <tr>
    <td><b>Kapsam</b></td>
    <td>Sürekli değişebilir</td>
    <td>Sprint boyunca sabit tutulur</td>
  </tr>
  <tr>
    <td><b>Kısıt</b></td>
    <td>WIP limiti (max 3 aktif iş)</td>
    <td>Kapasite/velocity</td>
  </tr>
  <tr>
    <td><b>Ne zaman?</b></td>
    <td>Acil bug, müşteri şikayeti, prod sorunları</td>
    <td>Yeni özellik, iyileştirme, planlı geliştirme</td>
  </tr>
</table>

<p><b>💡 Hızlı Karar:</b> İş 1 gün içinde çözülmeli → <b>Kanban</b> | Sprint sürecinde çözülebilir → <b>Scrum</b></p>

<hr/>

<h2 style='color: #106EBE;'>🚀 Hızlı Başlangıç</h2>

<h3>1️⃣ Proje ve Modül Tanımlama</h3>
<p>• <b>📁 Projeler</b> menüsünden projelerinizi tanımlayın (ör: CRM Sistemi, ERP Projesi)</p>
<p>• <b>📦 Modüller</b> menüsünden her proje için modüller ekleyin (ör: SQL, Ekran, API, Rapor)</p>

<h3>2️⃣ İş Talebi Oluşturma</h3>
<p><b>Yol 1 - Doğrudan:</b> <b>📥 Gelen Kutusu</b> → <b>Yeni İş Talebi</b></p>
<p><b>Yol 2 - Toplantıdan:</b> <b>📅 Toplantılar</b> → Notlardan seçim → <b>İş Talebine Dönüştür</b></p>
<p><b>Yol 3 - Outlook'tan:</b> İş detayında <b>E-posta Ekle</b> ile Outlook maillerini işe bağlayın</p>

<h3>3️⃣ Sınıflandırma (Gelen Kutusu'ndan)</h3>
<p>• İşi seçin → <b>Sınıflandır</b> butonuna tıklayın</p>
<p>• İş tipi, aciliyet, tahmini efor ve hedef pano seçin</p>
<p>• Scrum seçtiyseniz sprint seçimi yapın</p>

<h3>4️⃣ Panolarda Çalışma</h3>
<p>• <b>🏃 Scrum Panosu:</b> Sprint backlog → Geliştirmede → Testte → Tamamlandı</p>
<p>• <b>📋 Kanban Panosu:</b> Gelen → Sırada → Müdahale Ediliyor (WIP:3) → Doğrulama → Çözüldü</p>
<p>• Kartları <b>sürükle-bırak</b> ile taşıyın</p>

<hr/>

<h2 style='color: #107C10;'>🔗 İlişkili İşler (YENİ!)</h2>
<p>İşler arasında iki tür ilişki kurabilirsiniz:</p>

<h3>👨‍👧 Üst-Alt (Parent-Child) İlişkisi</h3>
<p>• Büyük işleri alt görevlere bölmek için kullanılır</p>
<p>• Örnek: ""CRM Geliştirme"" → ""Müşteri Listesi Ekranı"", ""Sipariş Modülü""</p>
<p>• Üst iş tamamlandığında alt işler de etkilenir</p>

<h3>👫 Kardeş (Sibling) İlişkisi</h3>
<p>• Birbirine bağımlı veya ilgili işler için kullanılır</p>
<p>• Örnek: ""API Geliştirme"" ↔ ""Frontend Entegrasyonu""</p>
<p>• Her iki yönde de görünür</p>

<h3>İlişki Nasıl Kurulur?</h3>
<p>1. İş detay ekranını açın (kartı çift tıklayın veya 👁 butonuna basın)</p>
<p>2. <b>İlişkiler</b> sekmesine gidin</p>
<p>3. <b>+ İlişki Ekle</b> butonuna tıklayın</p>
<p>4. İlişki tipini ve hedef işi seçin</p>
<p>5. İsteğe bağlı açıklama ekleyin</p>

<hr/>

<h2 style='color: #107C10;'>📊 Çalışma Özeti (YENİ!)</h2>
<p>Günlük, haftalık veya aylık çalışma performansınızı görüntüleyin.</p>

<h3>Özellikler:</h3>
<p>• <b>Geliştirme Süresi:</b> ""Geliştirmede"" veya ""Müdahale Ediliyor"" durumlarında geçen süre otomatik hesaplanır</p>
<p>• <b>Tamamlanan İş Sayısı:</b> Seçili dönemde bitirilmiş işler</p>
<p>• <b>Günlük Ortalama:</b> Toplam süre / çalışılan gün sayısı</p>
<p>• <b>Zaman Dağılımı:</b> Hangi işe ne kadar zaman harcandığı grafiği</p>

<h3>Kullanım:</h3>
<p>• <b>📊 Çalışma Özeti</b> butonuna tıklayın</p>
<p>• Dönem seçin: Bugün, Bu Hafta, Bu Ay veya Özel Tarih</p>
<p>• <b>📋 Panoya Kopyala</b> ile raporu paylaşın</p>

<h3>⚠️ Önemli:</h3>
<p>Geliştirme süresi, işin <b>Geliştirmede</b> veya <b>Müdahale Ediliyor</b> durumuna alındığı andan itibaren otomatik olarak hesaplanır. Manuel zaman girişi gerekmez!</p>

<hr/>

<h2 style='color: #107C10;'>🔗 İş Hiyerarşisi Diyagramı (YENİ!)</h2>
<p>İşler arasındaki ilişkileri görsel diyagram olarak görüntüleyin.</p>

<h3>Özellikler:</h3>
<p>• <b>Otomatik Yerleşim:</b> İşler hiyerarşik ağaç yapısında düzenlenir</p>
<p>• <b>Renk Kodları:</b></p>
<p>&nbsp;&nbsp;⬜ Gri: Bekliyor</p>
<p>&nbsp;&nbsp;🟦 Mavi: Sprint Backlog</p>
<p>&nbsp;&nbsp;🟨 Sarı: Geliştirmede</p>
<p>&nbsp;&nbsp;🟩 Yeşil: Tamamlandı</p>
<p>• <b>Çizgi Tipleri:</b></p>
<p>&nbsp;&nbsp;⬛ Siyah: Üst-Alt ilişkisi</p>
<p>&nbsp;&nbsp;🟦 Mavi kesikli: Kardeş ilişkisi</p>

<h3>Kullanım:</h3>
<p>• <b>🔗 İş Hiyerarşisi</b> butonuna tıklayın</p>
<p>• Proje filtresi ile daraltın</p>
<p>• Kök iş seçerek alt ağacı görüntüleyin</p>
<p>• <b>PNG Olarak Kaydet</b> ile dışa aktarın</p>

<hr/>

<h2 style='color: #107C10;'>📚 Wiki (YENİ!)</h2>
<p>Kurumsal bilgi birikimi oluşturun ve belgeleyin.</p>

<h3>Ne İçin Kullanılır?</h3>
<p>• Teknik dokümantasyon</p>
<p>• Süreç açıklamaları</p>
<p>• Kod snippetleri</p>
<p>• Proje notları</p>
<p>• Eğitim materyalleri</p>

<h3>Özellikler:</h3>
<p>• <b>Proje Bazlı:</b> Her proje için ayrı wiki sayfaları</p>
<p>• <b>Zengin İçerik:</b> Resim, tablo, link desteği</p>
<p>• <b>Arama:</b> Tüm wiki içeriğinde hızlı arama</p>

<hr/>

<h2 style='color: #107C10;'>⏱️ Zaman Kayıtları</h2>
<p>Manuel zaman girişi yapmak için kullanılır.</p>

<h3>Ne Zaman Kullanılır?</h3>
<p>• Toplantı süreleri</p>
<p>• Araştırma/analiz çalışmaları</p>
<p>• Retrospektif olarak eklenen süreler</p>

<p><b>Not:</b> Geliştirme süresi otomatik hesaplandığı için, normal kod geliştirme işleri için manuel giriş gerekmez.</p>

<hr/>

<h2 style='color: #107C10;'>🔔 Günlük Hatırlatıcı</h2>
<p>Her gün saat <b>17:30</b>'da aktif işleriniz için sistem bildirimi alırsınız.</p>

<h3>Özellikler:</h3>
<p>• Otomatik bildirim (uygulama arka planda çalışırken bile)</p>
<p>• Aktif iş sayısı ve detayları</p>
<p>• Tray menüsünden manuel tetikleme (<b>🔔 Şimdi Hatırlat</b>)</p>

<h3>Tray İkonu:</h3>
<p>Uygulamayı kapatmak yerine X'e bastığınızda, sistem tray'ine küçülür ve hatırlatıcı çalışmaya devam eder.</p>

<hr/>

<h2 style='color: #106EBE;'>📊 Uygulama Modülleri</h2>

<table border='1' cellpadding='5' style='border-collapse: collapse; width: 100%;'>
<tr style='background-color: #F3F3F3;'>
    <th>Modül</th>
    <th>Açıklama</th>
    <th>Grup</th>
</tr>
<tr>
    <td><b>📥 Gelen Kutusu</b></td>
    <td>Yeni iş talepleri + Sınıflandırma işlemi</td>
    <td>İş Akışı</td>
</tr>
<tr>
    <td><b>📋 Kanban Panosu</b></td>
    <td>Acil işler için WIP limitli akış yönetimi</td>
    <td>İş Akışı</td>
</tr>
<tr>
    <td><b>🏃 Scrum Panosu</b></td>
    <td>Sprint bazlı planlı işler</td>
    <td>İş Akışı</td>
</tr>
<tr>
    <td><b>📅 Toplantılar</b></td>
    <td>Toplantı kayıtları ve aksiyon takibi</td>
    <td>İş Akışı</td>
</tr>
<tr>
    <td><b>📋 Tüm İşler</b></td>
    <td>Tüm işlerin listesi ve arama</td>
    <td>İş Akışı</td>
</tr>
<tr>
    <td><b>⏱️ Zaman Kayıtları</b></td>
    <td>Manuel zaman girişleri</td>
    <td>İş Akışı</td>
</tr>
<tr>
    <td><b>📊 Çalışma Özeti</b></td>
    <td>Günlük/haftalık/aylık performans</td>
    <td>İş Akışı</td>
</tr>
<tr>
    <td><b>🔗 İş Hiyerarşisi</b></td>
    <td>İlişki diyagramı görselleştirme</td>
    <td>İş Akışı</td>
</tr>
<tr>
    <td><b>📁 Projeler</b></td>
    <td>Proje tanımlama</td>
    <td>Ayarlar</td>
</tr>
<tr>
    <td><b>📦 Modüller</b></td>
    <td>Proje altı modüller</td>
    <td>Ayarlar</td>
</tr>
<tr>
    <td><b>🔄 Sprint Yönetimi</b></td>
    <td>Sprint oluştur/başlat/tamamla</td>
    <td>Ayarlar</td>
</tr>
<tr>
    <td><b>📚 Wiki</b></td>
    <td>Bilgi bankası ve dokümantasyon</td>
    <td>Ayarlar</td>
</tr>
<tr>
    <td><b>📈 Raporlar</b></td>
    <td>Kapasite ve performans analizleri</td>
    <td>Ayarlar</td>
</tr>
</table>

<hr/>

<h2 style='color: #106EBE;'>💡 İpuçları</h2>

<h3>🔹 Verimli Çalışma</h3>
<p>• İşe başlarken kartı <b>Geliştirmede</b>'ye taşıyın - süre otomatik başlar</p>
<p>• Ara verirken <b>Sprint Backlog</b>'a geri taşıyın - süre durur</p>
<p>• Her gün 17:30 hatırlatmasıyla açık işlerinizi kontrol edin</p>

<h3>🔹 İlişkileri Kullanın</h3>
<p>• Büyük işleri alt görevlere bölün (üst-alt ilişkisi)</p>
<p>• Bağımlı işleri kardeş olarak işaretleyin</p>
<p>• Hiyerarşi diyagramı ile büyük resmi görün</p>

<h3>🔹 Bilgi Yönetimi</h3>
<p>• Sık kullanılan SQL sorgularını Wiki'ye kaydedin</p>
<p>• Proje dökümanlarını Wiki'de tutun</p>
<p>• Toplantı notlarından aksiyon çıkarın</p>

<h3>🔹 Outlook Entegrasyonu</h3>
<p>• İş detayında <b>E-posta Ekle</b> ile ilgili mailleri bağlayın</p>
<p>• Mail zincirini iş geçmişinde takip edin</p>

<hr/>

<h2 style='color: #106EBE;'>⚠️ Sık Sorulan Sorular</h2>

<p><b>S: Geliştirme süresi nasıl hesaplanıyor?</b></p>
<p>C: İş ""Geliştirmede"" veya ""Müdahale Ediliyor"" durumuna alındığında süre başlar, başka duruma geçince durur. Toplam süre otomatik hesaplanır.</p>

<p><b>S: İlişkili işleri nasıl görebilirim?</b></p>
<p>C: İş detayında ""İlişkiler"" sekmesi veya ""🔗 İş Hiyerarşisi"" diyagramı ile.</p>

<p><b>S: Uygulama arka planda çalışıyor mu?</b></p>
<p>C: Evet! X'e bastığınızda tray'e küçülür ve 17:30 hatırlatması aktif kalır.</p>

<p><b>S: Çalışma özetini nasıl paylaşabilirim?</b></p>
<p>C: ""📋 Panoya Kopyala"" butonu ile metin formatında kopyalayıp e-posta/Teams'e yapıştırın.</p>

<p><b>S: Wiki sayfalarını kimler görebilir?</b></p>
<p>C: Bu kişisel bir araç olduğu için tüm wiki sayfaları size özeldir.</p>

<hr/>

<h2 style='color: #106EBE;'>⌨️ Klavye Kısayolları</h2>

<table border='1' cellpadding='5' style='border-collapse: collapse; width: 100%;'>
<tr style='background-color: #F3F3F3;'>
    <th>Kısayol</th>
    <th>İşlev</th>
</tr>
<tr><td><b>F5</b></td><td>Tüm ekranlarda: Yenile</td></tr>
<tr><td><b>Ctrl+B</b></td><td>Editörlerde: Kalın yazı</td></tr>
<tr><td><b>Ctrl+I</b></td><td>Editörlerde: İtalik yazı</td></tr>
<tr><td><b>Ctrl+U</b></td><td>Editörlerde: Altı çizili</td></tr>
<tr><td><b>Ctrl+K</b></td><td>Editörlerde: Hyperlink ekle</td></tr>
<tr><td><b>Ctrl+V</b></td><td>Editörlerde: Resim yapıştır</td></tr>
<tr><td><b>Çift Tık</b></td><td>Kartlarda: Detay ekranı aç</td></tr>
</table>

<hr/>

<p style='text-align: center; color: #666; font-size: 11px;'>
<b>Work Tracker v3.0</b> • 30 Kasım 2025<br/>
Hibrit İş Yönetimi • İlişkili İşler • Otomatik Zaman Takibi • Wiki • Günlük Hatırlatıcı<br/>
🚀 Geliştirme devam ediyor - Yeni özellik önerilerinizi bekliyoruz!
</p>
";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

