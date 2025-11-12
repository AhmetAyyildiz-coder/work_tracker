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
<h1 style='color: #0078D4;'>Kaos Kontrol - Kullanım Kılavuzu</h1>

<h2 style='color: #106EBE;'>📋 Genel Bakış</h2>
<p>Bu uygulama, <b>planlı işler (Scrum)</b> ve <b>plansız/acil işleri (Kanban)</b> birbirinden ayırarak yönetmenize olanak tanır.</p>
<p><b>Scrum:</b> Sprint bazlı planlı geliştirme için kullanılır. İşler sprint'lere atanır ve sprint süresi boyunca tamamlanır.</p>
<p><b>Kanban:</b> Acil işler ve beklenmeyen talepler için kullanılır. WIP limitleri ile akış kontrol edilir.</p>

<hr/>

<h2 style='color: #106EBE;'>⚖️ Kanban vs Scrum - Ne Zaman Hangisi?</h2>
<p>Aşağıdaki tablo, iki yaklaşım arasındaki temel farkları özetler:</p>
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
    <td>WIP limiti</td>
    <td>Kapasite/velocity</td>
  </tr>
  <tr>
    <td><b>Ne zaman?</b></td>
    <td>Acil bug, müşteri şikayeti, prod sorunları</td>
    <td>Yeni özellik, iyileştirme, planlı geliştirme</td>
  </tr>
</table>

<p><b>Karar Rehberi:</b></p>
<ul>
  <li>İş 1 gün içinde çözülmeli → <b>Kanban</b></li>
  <li>İş sprint planına sığıyor → <b>Scrum</b></li>
  <li>Planlı sprinti bozmadan acil işi almak gerekiyor → <b>Kanban</b></li>
  <li>Takım hedeflerine bağlı kapsama ihtiyaç var → <b>Scrum</b></li>
</ul>

<h2 style='color: #106EBE;'>🚀 Hızlı Başlangıç (7 Adım)</h2>

<h3>1️⃣ Proje ve Modül Tanımlama</h3>
<p>• <b>Ayarlar → Projeler</b> menüsünden projelerinizi tanımlayın (ör: CRM Sistemi, ERP Projesi)</p>
<p>• <b>Ayarlar → Modüller</b> menüsünden her proje için modüller ekleyin (ör: SQL, Ekran, API, Rapor)</p>

<h3>2️⃣ İş Talebi Oluşturma</h3>
<p><b>Yol 1 - Doğrudan Talep:</b></p>
<p>• <b>Gelen Kutusu</b> ekranından <b>Yeni İş Talebi</b> butonuna tıklayın</p>
<p>• Başlık, açıklama, talep eden kişi gibi bilgileri girin</p>
<p>• İsteğe bağlı olarak proje ve modül seçin</p>

<p><b>Yol 2 - Toplantıdan:</b></p>
<p>• <b>Toplantılar</b> ekranından yeni toplantı oluşturun</p>
<p>• Toplantı notlarını <b>Zengin Metin Editörü (RichEdit)</b>'nde yazın:</p>
<p>&nbsp;&nbsp;🖼️ <b>Resim ekleyin:</b> Sağ tık → Insert → Picture veya Ctrl+V ile yapıştır</p>
<p>&nbsp;&nbsp;📋 <b>Tablo ekleyin:</b> Sağ tık → Insert → Table</p>
<p>&nbsp;&nbsp;🔗 <b>Link ekleyin:</b> Metin seç → Ctrl+K</p>
<p>&nbsp;&nbsp;📝 <b>Format verin:</b> Ctrl+B (kalın), Ctrl+I (italik), Ctrl+U (alt çizgi)</p>
<p>• Notlardan bir bölümü seçip <b>İş Talebine Dönüştür</b> butonuna tıklayın</p>
<p>• İş talebi otomatik olarak toplantı ile ilişkilendirilir</p>

<h3>3️⃣ Sprint Oluşturma (Scrum için)</h3>
<p>• <b>Ayarlar → Sprint Yönetimi</b> menüsünden yeni sprint oluşturun</p>
<p>• Sprint adı, hedefler, başlangıç ve bitiş tarihi girin</p>
<p>• Sprint'i <b>Başlat</b> butonuna tıklayarak aktif edin</p>
<p>• <i>Not: Aktif sprint olmadan Scrum panosuna iş yönlendiremezsiniz</i></p>

<h3>4️⃣ Triage (Sınıflandırma)</h3>
<p>• <b>Gelen Kutusu</b>'nda bir iş talebini seçin</p>
<p>• <b>Triage'e Gönder</b> butonuna tıklayın</p>
<p>• Triage ekranında:</p>
<p>&nbsp;&nbsp;- İş tipini seçin (AcilArge, Bug, YeniÖzellik, İyileştirme, Diğer)</p>
<p>&nbsp;&nbsp;- Aciliyet belirleyin (Kritik, Yüksek, Normal, Düşük)</p>
<p>&nbsp;&nbsp;- Tahmini efor girin (gün cinsinden)</p>
<p>&nbsp;&nbsp;- <b>Hedef Pano</b> seçin: <b>Scrum</b> (planlı) veya <b>Kanban</b> (acil)</p>
<p>&nbsp;&nbsp;- Scrum seçtiyseniz, <b>Sprint seçimi</b> yapın (zorunlu)</p>
<p>• <b>Kaydet ve Yönlendir</b> butonuna tıklayın</p>

<h3>5️⃣ Scrum Panosunda Çalışma</h3>
<p>• <b>Scrum Panosu</b> ekranını açın</p>
<p>• Üstteki dropdown'dan <b>aktif sprint'i</b> seçin</p>
<p>• İş kartlarını <b>sürükle-bırak</b> ile sütunlar arasında taşıyın:</p>
<p>&nbsp;&nbsp;→ <b>Sprint Backlog:</b> Sprint için planlanan işler</p>
<p>&nbsp;&nbsp;→ <b>Geliştirmede:</b> Aktif olarak üzerinde çalışılan işler</p>
<p>&nbsp;&nbsp;→ <b>Testte:</b> Geliştirme tamamlanmış, test aşamasındaki işler</p>
<p>&nbsp;&nbsp;→ <b>Tamamlandı:</b> Sprint'te tamamlanan işler</p>
<p>• 📅 ikonu: Toplantıdan gelen işler</p>
<p>• ⏱ ikonu: Tahmini efor (gün cinsinden)</p>

<h3>6️⃣ Kanban Panosunda Çalışma</h3>
<p>• <b>Kanban Panosu</b> ekranını açın</p>
<p>• İş kartlarını <b>sürükle-bırak</b> ile sütunlar arasında taşıyın:</p>
<p>&nbsp;&nbsp;→ Gelen Acil İşler</p>
<p>&nbsp;&nbsp;→ Sırada</p>
<p>&nbsp;&nbsp;→ Müdahale Ediliyor (WIP Limit: 3)</p>
<p>&nbsp;&nbsp;→ Doğrulama Bekliyor</p>
<p>&nbsp;&nbsp;→ Çözüldü</p>
<p>• <b>WIP Limiti:</b> 'Müdahale Ediliyor' sütununa max 3 iş alınabilir</p>

<h3>7️⃣ Toplantı İzleme ve Raporlar</h3>
<p>• Bir toplantı kaydına tıklayın ve <b>Detayları Göster</b> butonuna basın</p>
<p>• Alt kısımda, o toplantıdan üretilen tüm iş taleplerini ve güncel durumlarını görebilirsiniz</p>
<p>• <b>Raporlar</b> menüsünden performans analizlerinizi görüntüleyin:</p>
<p>&nbsp;&nbsp;- Kapasite dağılımı (Scrum vs Kanban)</p>
<p>&nbsp;&nbsp;- İş dağılımı (Proje ve modül bazında)</p>
<p>&nbsp;&nbsp;- Sprint performans metrikleri</p>
<p>&nbsp;&nbsp;- Efor trend analizleri</p>

<hr/>

<h2 style='color: #106EBE;'>💡 İpuçları</h2>

<h3>🔹 Acil mi, Planlı mı?</h3>
<p><b>Kanban'a gönderin:</b> Kritik buglar, müşteri şikayetleri, sistem çökmeleri, acil arge talepleri</p>
<p><b>Scrum'a gönderin:</b> Yeni özellikler, iyileştirmeler, planlı geliştirmeler, refactoring işleri</p>
<p><i>💡 İpucu: Eğer iş 1 gün içinde çözülmesi gerekiyorsa → Kanban, sprint sürecinde çözülebiliyorsa → Scrum</i></p>

<h3>🔹 Sprint Yönetimi</h3>
<p>• Sprint sürelerini ekip kapasitesine göre ayarlayın (genelde 2 hafta)</p>
<p>• Sprint'e fazla iş yüklemeyin - ekip kapasitesinin %80'ini hedefleyin</p>
<p>• Sprint ortasında yeni iş eklemeyin - acil işler için Kanban kullanın</p>
<p>• Her sprint sonunda retrospektif yapın ve raporları inceleyin</p>
<p>• Sprint tamamlandıktan sonra <b>Sprint Tamamla</b> ile kapatın</p>

<h3>🔹 Toplantı Takibi</h3>
<p>• Her toplantıdan çıkan aksiyonları <b>İş Talebine Dönüştür</b> özelliğiyle kaydedin</p>
<p>• Toplantı sunumlarını ve diyagramları <b>resim olarak</b> notlara yapıştırın (Ctrl+V)</p>
<p>• Sprint planlarını, karar tablolarını <b>tablo formatında</b> kaydedin</p>
<p>• Teams/Zoom toplantı linklerini <b>hyperlink</b> olarak ekleyin (Ctrl+K)</p>
<p>• <b>Detayları Göster</b> butonu ile tam ekran görünümde çalışın</p>
<p>• Toplantı kartlarında 📅 ikonu görürseniz, o iş bir toplantıdan gelmiştir</p>

<h3>🔹 WIP Limiti (Kanban)</h3>
<p>• Aynı anda çok fazla işe başlamayın!</p>
<p>• 'Müdahale Ediliyor' sütununda max 3 iş olması ekip odağını korur</p>
<p>• WIP limiti aşıldığında sistem uyarı verir</p>

<h3>🔹 Filtreleme ve Arama</h3>
<p>• Tüm grid'lerde <b>otomatik filtre satırı</b> var (başlık satırının altında)</p>
<p>• Buradan hızlıca arama yapabilirsiniz</p>

<h3>🔹 Raporlama ve Analiz</h3>
<p>• Düzenli olarak raporları kontrol edin</p>
<p>• <b>Kapasite raporu</b> ile Scrum/Kanban dengesini izleyin</p>
<p>• Eğer Kanban işleri %50'yi geçiyorsa, plansız iş yükü fazla demektir</p>
<p>• <b>Sprint performans raporu</b> ile ekip hızını (velocity) takip edin</p>
<p>• <b>Proje dağılım raporu</b> ile hangi projelere zaman harcandığını görün</p>

<hr/>

<h2 style='color: #106EBE;'>📊 Modüller</h2>

<table border='1' cellpadding='5' style='border-collapse: collapse; width: 100%;'>
<tr style='background-color: #F3F3F3;'>
    <th>Modül</th>
    <th>Açıklama</th>
    <th>Ribbon Grubu</th>
</tr>
<tr>
    <td><b>Gelen Kutusu</b></td>
    <td>Tüm yeni iş taleplerinin toplandığı merkez</td>
    <td>İş Akışı</td>
</tr>
<tr>
    <td><b>Sınıflandırma (Triage)</b></td>
    <td>İşleri sınıflandırıp doğru panoya yönlendirme</td>
    <td>İş Akışı</td>
</tr>
<tr>
    <td><b>Kanban Panosu</b></td>
    <td>Acil işler için hızlı akış yönetimi (WIP limitli)</td>
    <td>İş Akışı</td>
</tr>
<tr>
    <td><b>Scrum Panosu</b></td>
    <td>Sprint bazlı planlı işler için görsel pano</td>
    <td>İş Akışı</td>
</tr>
<tr>
    <td><b>Toplantılar</b></td>
    <td>Toplantı kayıtları (Resim/Tablo/Link destekli) ve aksiyonları iş talebine çevirme</td>
    <td>İş Akışı</td>
</tr>
<tr>
    <td><b>Projeler</b></td>
    <td>Proje tanımlama ve yönetimi</td>
    <td>Ayarlar</td>
</tr>
<tr>
    <td><b>Modüller</b></td>
    <td>Proje altı modül tanımlama (SQL, Ekran, API, vb.)</td>
    <td>Ayarlar</td>
</tr>
<tr>
    <td><b>Sprint Yönetimi</b></td>
    <td>Sprint oluşturma, başlatma, tamamlama</td>
    <td>Ayarlar</td>
</tr>
<tr>
    <td><b>Raporlar</b></td>
    <td>Kapasite, performans ve trend analizleri</td>
    <td>Ayarlar</td>
</tr>
</table>

<hr/>

<h2 style='color: #106EBE;'>⚠️ Sık Sorulan Sorular</h2>

<p><b>S: Bir işi yanlış panoya gönderdim, nasıl değiştirebilirim?</b></p>
<p>C: Gelen Kutusu'nda işi seçip tekrar Triage'e gönderin ve doğru panoyu seçin.</p>

<p><b>S: Sprint başladıktan sonra iş ekleyebilir miyim?</b></p>
<p>C: Evet, Triage ekranından mevcut sprint'i seçerek yeni işler ekleyebilirsiniz. Ancak sprint kapasitesini aşmamaya dikkat edin.</p>

<p><b>S: WIP limitini değiştirebilir miyim?</b></p>
<p>C: Şu an kod üzerinden değiştirilebilir. İleriki versiyonlarda ayarlardan yapılabilecek.</p>

<p><b>S: Sprint tamamlanmadan yeni sprint başlatabilir miyim?</b></p>
<p>C: Hayır, önce aktif sprint'i tamamlamanız gerekir. Sistem aynı anda sadece 1 aktif sprint'e izin verir.</p>

<p><b>S: Toplantılardan gelen işleri nasıl görebilirim?</b></p>
<p>C: İş kartlarında 📅 ikonu varsa, o iş bir toplantıdan gelmiştir. Toplantı detaylarında da o toplantıdan gelen tüm işleri görebilirsiniz.</p>

<p><b>S: Raporlar ne sıklıkla güncellenir?</b></p>
<p>C: Raporlar gerçek zamanlı güncellenir. Her veri değişikliğinde otomatik olarak yenilenir.</p>

<p><b>S: Sprint'teki tamamlanmayan işler ne olur?</b></p>
<p>C: Sprint tamamlandığında, bitmemiş işleri manuel olarak yeni sprint'e taşımanız gerekir.</p>

<hr/>

<hr/>

<h2 style='color: #106EBE;'>⌨️ Klavye Kısayolları</h2>

<table border='1' cellpadding='5' style='border-collapse: collapse; width: 100%;'>
<tr style='background-color: #F3F3F3;'>
    <th>Kısayol</th>
    <th>İşlev</th>
</tr>
<tr>
    <td><b>Ctrl+B</b></td>
    <td>Toplantı notlarında: Kalın yazı</td>
</tr>
<tr>
    <td><b>Ctrl+I</b></td>
    <td>Toplantı notlarında: İtalik yazı</td>
</tr>
<tr>
    <td><b>Ctrl+U</b></td>
    <td>Toplantı notlarında: Altı çizili yazı</td>
</tr>
<tr>
    <td><b>Ctrl+K</b></td>
    <td>Toplantı notlarında: Hyperlink ekle</td>
</tr>
<tr>
    <td><b>Ctrl+V</b></td>
    <td>Toplantı notlarında: Resim/İçerik yapıştır</td>
</tr>
<tr>
    <td><b>F5</b></td>
    <td>Tüm ekranlarda: Yenile</td>
</tr>
</table>

<hr/>

<p style='text-align: center; color: #666; font-size: 11px;'>
<b>İş Takip v2.0</b> (Scrum + Raporlama) • 12 Kasım 2025<br/>
Tüm modüller aktif • Sprint bazlı çalışma desteği • Gelişmiş raporlama<br/>
Daha fazla yardım için: İş takibi → Yeni Özellik talebi açın 😊
</p>
";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

