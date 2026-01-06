using System;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using work_tracker.Data;
using work_tracker.Data.Entities;

namespace work_tracker.Forms
{
    public partial class DocumentReferenceEditForm : XtraForm
    {
        private WorkTrackerDbContext _context;
        private int? _documentId;
        private DocumentReference _document;

        /// <summary>
        /// Yeni döküman oluşturma
        /// </summary>
        public DocumentReferenceEditForm() : this(null)
        {
        }

        /// <summary>
        /// Mevcut dökümanı düzenleme
        /// </summary>
        public DocumentReferenceEditForm(int? documentId)
        {
            InitializeComponent();
            _context = new WorkTrackerDbContext();
            _documentId = documentId;
        }

        /// <summary>
        /// İş kaleminden döküman ekleme için
        /// </summary>
        public int? PresetWorkItemId { get; set; }
        public int? PresetProjectId { get; set; }
        public int? PresetModuleId { get; set; }
        public int? PresetMeetingId { get; set; }

        private void DocumentReferenceEditForm_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();

            if (_documentId.HasValue)
            {
                // Düzenleme modu
                LoadDocument();
                this.Text = "Döküman Düzenle";
            }
            else
            {
                // Yeni ekleme modu
                _document = new DocumentReference();
                this.Text = "Yeni Döküman Ekle";

                // Preset değerleri ayarla
                if (PresetWorkItemId.HasValue)
                    cmbWorkItem.EditValue = PresetWorkItemId.Value;
                if (PresetProjectId.HasValue)
                    cmbProject.EditValue = PresetProjectId.Value;
                if (PresetModuleId.HasValue)
                    cmbModule.EditValue = PresetModuleId.Value;
                if (PresetMeetingId.HasValue)
                    cmbMeeting.EditValue = PresetMeetingId.Value;
            }
        }

        private void LoadComboBoxes()
        {
            // Projeler
            var projects = _context.Projects
                .Where(p => p.IsActive)
                .OrderBy(p => p.Name)
                .ToList();

            cmbProject.Properties.DataSource = projects;
            cmbProject.Properties.DisplayMember = "Name";
            cmbProject.Properties.ValueMember = "Id";
            cmbProject.Properties.NullText = "(Proje Seçin - Opsiyonel)";
            cmbProject.Properties.Columns.Clear();
            cmbProject.Properties.Columns.Add(new LookUpColumnInfo("Name", "Proje"));

            // Modüller (proje seçildiğinde filtrelenecek)
            cmbModule.Properties.NullText = "(Modül Seçin - Opsiyonel)";
            cmbModule.Properties.Columns.Clear();
            cmbModule.Properties.Columns.Add(new LookUpColumnInfo("Name", "Modül"));

            // İş kalemleri
            var workItems = _context.WorkItems
                .Where(w => w.Status != "Closed" && w.Status != "Rejected")
                .OrderByDescending(w => w.Id)
                .Select(w => new
                {
                    w.Id,
                    DisplayText = "WI-" + w.Id + " - " + w.Title
                })
                .ToList();

            cmbWorkItem.Properties.DataSource = workItems;
            cmbWorkItem.Properties.DisplayMember = "DisplayText";
            cmbWorkItem.Properties.ValueMember = "Id";
            cmbWorkItem.Properties.NullText = "(İş Kalemi Seçin - Opsiyonel)";
            cmbWorkItem.Properties.Columns.Clear();
            cmbWorkItem.Properties.Columns.Add(new LookUpColumnInfo("DisplayText", "İş Kalemi"));

            // Toplantılar
            var meetings = _context.Meetings
                .OrderByDescending(m => m.MeetingDate)
                .Select(m => new
                {
                    m.Id,
                    DisplayText = m.Subject + " - " + System.Data.Entity.DbFunctions.TruncateTime(m.MeetingDate)
                })
                .ToList();

            cmbMeeting.Properties.DataSource = meetings;
            cmbMeeting.Properties.DisplayMember = "DisplayText";
            cmbMeeting.Properties.ValueMember = "Id";
            cmbMeeting.Properties.NullText = "(Toplantı Seçin - Opsiyonel)";
            cmbMeeting.Properties.Columns.Clear();
            cmbMeeting.Properties.Columns.Add(new LookUpColumnInfo("DisplayText", "Toplantı"));

            // Etiketler (CheckedComboBox)
            var tags = _context.DocumentTags.OrderBy(t => t.Name).ToList();
            foreach (var tag in tags)
            {
                chkTags.Properties.Items.Add(tag.Id, tag.Name, CheckState.Unchecked, true);
            }
        }

        private void LoadDocument()
        {
            _document = _context.DocumentReferences
                .Include(d => d.Tags)
                .FirstOrDefault(d => d.Id == _documentId);

            if (_document == null)
            {
                XtraMessageBox.Show("Döküman bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            txtTitle.Text = _document.Title;
            txtFilePath.Text = _document.FilePath;
            txtDescription.Text = _document.Description;
            cmbProject.EditValue = _document.ProjectId;
            cmbModule.EditValue = _document.ModuleId;
            cmbWorkItem.EditValue = _document.WorkItemId;
            cmbMeeting.EditValue = _document.MeetingId;
            chkFavorite.Checked = _document.IsFavorite;

            // Etiketleri seç
            foreach (var tag in _document.Tags)
            {
                for (int i = 0; i < chkTags.Properties.Items.Count; i++)
                {
                    if ((int)chkTags.Properties.Items[i].Value == tag.Id)
                    {
                        chkTags.Properties.Items[i].CheckState = CheckState.Checked;
                        break;
                    }
                }
            }
        }

        private void LoadModules(int projectId)
        {
            var modules = _context.ProjectModules
                .Where(m => m.ProjectId == projectId && m.IsActive)
                .OrderBy(m => m.Name)
                .ToList();

            cmbModule.Properties.DataSource = modules;
            cmbModule.Properties.DisplayMember = "Name";
            cmbModule.Properties.ValueMember = "Id";
        }

        private void cmbProject_EditValueChanged(object sender, EventArgs e)
        {
            cmbModule.EditValue = null;
            
            if (cmbProject.EditValue != null)
            {
                int projectId = Convert.ToInt32(cmbProject.EditValue);
                LoadModules(projectId);
                cmbModule.Enabled = true;
            }
            else
            {
                cmbModule.Properties.DataSource = null;
                cmbModule.Enabled = false;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Döküman Seçin";
                openFileDialog.Filter = "Tüm Dosyalar (*.*)|*.*|" +
                                        "Word Dosyaları (*.doc;*.docx)|*.doc;*.docx|" +
                                        "Excel Dosyaları (*.xls;*.xlsx)|*.xls;*.xlsx|" +
                                        "PDF Dosyaları (*.pdf)|*.pdf|" +
                                        "PowerPoint Dosyaları (*.ppt;*.pptx)|*.ppt;*.pptx|" +
                                        "Metin Dosyaları (*.txt)|*.txt";
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialog.FileName;

                    // Başlık boşsa dosya adından öner
                    if (string.IsNullOrWhiteSpace(txtTitle.Text))
                    {
                        txtTitle.Text = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                    }
                }
            }
        }

        /// <summary>
        /// Yeni Word dosyası oluşturur ve döküman deposuna kaydeder
        /// </summary>
        private void btnCreateNewWord_Click(object sender, EventArgs e)
        {
            // Başlık kontrolü
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                XtraMessageBox.Show("Lütfen önce bir başlık girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return;
            }

            try
            {
                // Döküman deposu yolunu al
                string repositoryPath = GetDocumentRepositoryPath();
                
                // Proje bazlı alt klasör oluştur
                string subFolder = GetSubFolderPath();
                string fullFolderPath = Path.Combine(repositoryPath, subFolder);
                
                // Klasör yoksa oluştur
                if (!Directory.Exists(fullFolderPath))
                {
                    Directory.CreateDirectory(fullFolderPath);
                }

                // Dosya adını oluştur (başlıktan)
                string safeFileName = GetSafeFileName(txtTitle.Text);
                string fileName = $"{safeFileName}_{DateTime.Now:yyyyMMdd_HHmmss}.docx";
                string fullFilePath = Path.Combine(fullFolderPath, fileName);

                // Boş Word dosyası oluştur
                CreateEmptyWordDocument(fullFilePath);

                // Dosya yolunu form'a yaz
                txtFilePath.Text = fullFilePath;

                // Kullanıcıya sor: Şimdi açmak ister misiniz?
                var result = XtraMessageBox.Show(
                    $"Word dosyası oluşturuldu:\n{fullFilePath}\n\nŞimdi düzenlemek için açmak ister misiniz?",
                    "Dosya Oluşturuldu",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = fullFilePath,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Dosya oluşturulurken hata oluştu:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Yeni Excel dosyası oluşturur
        /// </summary>
        private void btnCreateNewExcel_Click(object sender, EventArgs e)
        {
            CreateNewOfficeDocument("xlsx", "Excel");
        }

        /// <summary>
        /// Yeni PowerPoint dosyası oluşturur
        /// </summary>
        private void btnCreateNewPowerPoint_Click(object sender, EventArgs e)
        {
            CreateNewOfficeDocument("pptx", "PowerPoint");
        }

        /// <summary>
        /// Yeni Text dosyası oluşturur
        /// </summary>
        private void btnCreateNewText_Click(object sender, EventArgs e)
        {
            // Başlık kontrolü
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                XtraMessageBox.Show("Lütfen önce bir başlık girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return;
            }

            try
            {
                string repositoryPath = GetDocumentRepositoryPath();
                string subFolder = GetSubFolderPath();
                string fullFolderPath = Path.Combine(repositoryPath, subFolder);
                
                if (!Directory.Exists(fullFolderPath))
                {
                    Directory.CreateDirectory(fullFolderPath);
                }

                string safeFileName = GetSafeFileName(txtTitle.Text);
                string fileName = $"{safeFileName}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                string fullFilePath = Path.Combine(fullFolderPath, fileName);

                // Boş text dosyası oluştur
                File.WriteAllText(fullFilePath, $"# {txtTitle.Text}\n\nOluşturulma: {DateTime.Now:dd.MM.yyyy HH:mm}\n\n");

                txtFilePath.Text = fullFilePath;

                var result = XtraMessageBox.Show(
                    $"Text dosyası oluşturuldu:\n{fullFilePath}\n\nŞimdi düzenlemek için açmak ister misiniz?",
                    "Dosya Oluşturuldu",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = fullFilePath,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Dosya oluşturulurken hata oluştu:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Office dosyası oluşturur (Word dışındakiler için)
        /// </summary>
        private void CreateNewOfficeDocument(string extension, string typeName)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                XtraMessageBox.Show("Lütfen önce bir başlık girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return;
            }

            try
            {
                string repositoryPath = GetDocumentRepositoryPath();
                string subFolder = GetSubFolderPath();
                string fullFolderPath = Path.Combine(repositoryPath, subFolder);
                
                if (!Directory.Exists(fullFolderPath))
                {
                    Directory.CreateDirectory(fullFolderPath);
                }

                string safeFileName = GetSafeFileName(txtTitle.Text);
                string fileName = $"{safeFileName}_{DateTime.Now:yyyyMMdd_HHmmss}.{extension}";
                string fullFilePath = Path.Combine(fullFolderPath, fileName);

                // Boş Office dosyası oluştur
                if (extension == "xlsx")
                {
                    CreateEmptyExcelDocument(fullFilePath);
                }
                else if (extension == "pptx")
                {
                    CreateEmptyPowerPointDocument(fullFilePath);
                }

                txtFilePath.Text = fullFilePath;

                var result = XtraMessageBox.Show(
                    $"{typeName} dosyası oluşturuldu:\n{fullFilePath}\n\nŞimdi düzenlemek için açmak ister misiniz?",
                    "Dosya Oluşturuldu",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = fullFilePath,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Dosya oluşturulurken hata oluştu:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Döküman deposu yolunu döndürür
        /// </summary>
        private string GetDocumentRepositoryPath()
        {
            string path = ConfigurationManager.AppSettings["DocumentRepositoryPath"];
            
            if (string.IsNullOrEmpty(path))
            {
                // Varsayılan: Uygulama klasöründe Documents
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documents");
            }

            // Klasör yoksa oluştur
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        /// <summary>
        /// Proje/Modül bazlı alt klasör yolunu oluşturur
        /// </summary>
        private string GetSubFolderPath()
        {
            string subFolder = "";

            // Proje seçildiyse proje adıyla klasör oluştur
            if (cmbProject.EditValue != null)
            {
                var projectId = Convert.ToInt32(cmbProject.EditValue);
                var project = _context.Projects.Find(projectId);
                if (project != null)
                {
                    subFolder = GetSafeFileName(project.Name);

                    // Modül seçildiyse alt klasör ekle
                    if (cmbModule.EditValue != null)
                    {
                        var moduleId = Convert.ToInt32(cmbModule.EditValue);
                        var module = _context.ProjectModules.Find(moduleId);
                        if (module != null)
                        {
                            subFolder = Path.Combine(subFolder, GetSafeFileName(module.Name));
                        }
                    }
                }
            }
            else
            {
                // Proje seçilmediyse yıl-ay klasörü
                subFolder = DateTime.Now.ToString("yyyy-MM");
            }

            return subFolder;
        }

        /// <summary>
        /// Dosya adı için güvenli karakter dönüşümü
        /// </summary>
        private string GetSafeFileName(string fileName)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            var safeFileName = fileName;
            
            foreach (var c in invalidChars)
            {
                safeFileName = safeFileName.Replace(c, '_');
            }
            
            // Türkçe karakterleri dönüştür
            safeFileName = safeFileName
                .Replace('ı', 'i').Replace('İ', 'I')
                .Replace('ğ', 'g').Replace('Ğ', 'G')
                .Replace('ü', 'u').Replace('Ü', 'U')
                .Replace('ş', 's').Replace('Ş', 'S')
                .Replace('ö', 'o').Replace('Ö', 'O')
                .Replace('ç', 'c').Replace('Ç', 'C');

            // Boşlukları alt çizgi yap
            safeFileName = safeFileName.Replace(' ', '_');

            // Uzunluk sınırı
            if (safeFileName.Length > 50)
            {
                safeFileName = safeFileName.Substring(0, 50);
            }

            return safeFileName;
        }

        /// <summary>
        /// Boş Word dosyası oluşturur (ZipArchive kullanarak)
        /// </summary>
        private void CreateEmptyWordDocument(string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            using (var archive = new System.IO.Compression.ZipArchive(stream, System.IO.Compression.ZipArchiveMode.Create))
            {
                // [Content_Types].xml
                var contentTypesEntry = archive.CreateEntry("[Content_Types].xml");
                using (var writer = new StreamWriter(contentTypesEntry.Open()))
                {
                    writer.Write(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<Types xmlns=""http://schemas.openxmlformats.org/package/2006/content-types"">
  <Default Extension=""rels"" ContentType=""application/vnd.openxmlformats-package.relationships+xml""/>
  <Default Extension=""xml"" ContentType=""application/xml""/>
  <Override PartName=""/word/document.xml"" ContentType=""application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml""/>
</Types>");
                }

                // _rels/.rels
                var relsEntry = archive.CreateEntry("_rels/.rels");
                using (var writer = new StreamWriter(relsEntry.Open()))
                {
                    writer.Write(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<Relationships xmlns=""http://schemas.openxmlformats.org/package/2006/relationships"">
  <Relationship Id=""rId1"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument"" Target=""word/document.xml""/>
</Relationships>");
                }

                // word/document.xml
                var documentEntry = archive.CreateEntry("word/document.xml");
                using (var writer = new StreamWriter(documentEntry.Open()))
                {
                    string title = txtTitle.Text ?? "Yeni Döküman";
                    writer.Write($@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<w:document xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main"">
  <w:body>
    <w:p>
      <w:pPr><w:pStyle w:val=""Heading1""/></w:pPr>
      <w:r><w:t>{System.Security.SecurityElement.Escape(title)}</w:t></w:r>
    </w:p>
    <w:p>
      <w:r><w:t>Oluşturulma Tarihi: {DateTime.Now:dd.MM.yyyy HH:mm}</w:t></w:r>
    </w:p>
    <w:p>
      <w:r><w:t></w:t></w:r>
    </w:p>
  </w:body>
</w:document>");
                }
            }
        }

        /// <summary>
        /// Boş Excel dosyası oluşturur (ZipArchive kullanarak)
        /// </summary>
        private void CreateEmptyExcelDocument(string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            using (var archive = new System.IO.Compression.ZipArchive(stream, System.IO.Compression.ZipArchiveMode.Create))
            {
                // [Content_Types].xml
                var contentTypesEntry = archive.CreateEntry("[Content_Types].xml");
                using (var writer = new StreamWriter(contentTypesEntry.Open()))
                {
                    writer.Write(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<Types xmlns=""http://schemas.openxmlformats.org/package/2006/content-types"">
  <Default Extension=""rels"" ContentType=""application/vnd.openxmlformats-package.relationships+xml""/>
  <Default Extension=""xml"" ContentType=""application/xml""/>
  <Override PartName=""/xl/workbook.xml"" ContentType=""application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml""/>
  <Override PartName=""/xl/worksheets/sheet1.xml"" ContentType=""application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml""/>
</Types>");
                }

                // _rels/.rels
                var relsEntry = archive.CreateEntry("_rels/.rels");
                using (var writer = new StreamWriter(relsEntry.Open()))
                {
                    writer.Write(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<Relationships xmlns=""http://schemas.openxmlformats.org/package/2006/relationships"">
  <Relationship Id=""rId1"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument"" Target=""xl/workbook.xml""/>
</Relationships>");
                }

                // xl/_rels/workbook.xml.rels
                var wbRelsEntry = archive.CreateEntry("xl/_rels/workbook.xml.rels");
                using (var writer = new StreamWriter(wbRelsEntry.Open()))
                {
                    writer.Write(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<Relationships xmlns=""http://schemas.openxmlformats.org/package/2006/relationships"">
  <Relationship Id=""rId1"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet"" Target=""worksheets/sheet1.xml""/>
</Relationships>");
                }

                // xl/workbook.xml
                var wbEntry = archive.CreateEntry("xl/workbook.xml");
                using (var writer = new StreamWriter(wbEntry.Open()))
                {
                    writer.Write(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<workbook xmlns=""http://schemas.openxmlformats.org/spreadsheetml/2006/main"" xmlns:r=""http://schemas.openxmlformats.org/officeDocument/2006/relationships"">
  <sheets><sheet name=""Sayfa1"" sheetId=""1"" r:id=""rId1""/></sheets>
</workbook>");
                }

                // xl/worksheets/sheet1.xml
                var sheetEntry = archive.CreateEntry("xl/worksheets/sheet1.xml");
                using (var writer = new StreamWriter(sheetEntry.Open()))
                {
                    string title = txtTitle.Text ?? "Yeni Döküman";
                    writer.Write($@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<worksheet xmlns=""http://schemas.openxmlformats.org/spreadsheetml/2006/main"">
  <sheetData>
    <row r=""1""><c r=""A1"" t=""inlineStr""><is><t>{System.Security.SecurityElement.Escape(title)}</t></is></c></row>
  </sheetData>
</worksheet>");
                }
            }
        }

        /// <summary>
        /// Boş PowerPoint dosyası oluşturur (ZipArchive kullanarak)
        /// </summary>
        private void CreateEmptyPowerPointDocument(string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            using (var archive = new System.IO.Compression.ZipArchive(stream, System.IO.Compression.ZipArchiveMode.Create))
            {
                // [Content_Types].xml
                var contentTypesEntry = archive.CreateEntry("[Content_Types].xml");
                using (var writer = new StreamWriter(contentTypesEntry.Open()))
                {
                    writer.Write(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<Types xmlns=""http://schemas.openxmlformats.org/package/2006/content-types"">
  <Default Extension=""rels"" ContentType=""application/vnd.openxmlformats-package.relationships+xml""/>
  <Default Extension=""xml"" ContentType=""application/xml""/>
  <Override PartName=""/ppt/presentation.xml"" ContentType=""application/vnd.openxmlformats-officedocument.presentationml.presentation.main+xml""/>
  <Override PartName=""/ppt/slides/slide1.xml"" ContentType=""application/vnd.openxmlformats-officedocument.presentationml.slide+xml""/>
</Types>");
                }

                // _rels/.rels
                var relsEntry = archive.CreateEntry("_rels/.rels");
                using (var writer = new StreamWriter(relsEntry.Open()))
                {
                    writer.Write(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<Relationships xmlns=""http://schemas.openxmlformats.org/package/2006/relationships"">
  <Relationship Id=""rId1"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument"" Target=""ppt/presentation.xml""/>
</Relationships>");
                }

                // ppt/presentation.xml
                var presEntry = archive.CreateEntry("ppt/presentation.xml");
                using (var writer = new StreamWriter(presEntry.Open()))
                {
                    writer.Write(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<p:presentation xmlns:p=""http://schemas.openxmlformats.org/presentationml/2006/main"" xmlns:r=""http://schemas.openxmlformats.org/officeDocument/2006/relationships"">
  <p:sldIdLst><p:sldId id=""256"" r:id=""rId1""/></p:sldIdLst>
</p:presentation>");
                }

                // ppt/_rels/presentation.xml.rels
                var presRelsEntry = archive.CreateEntry("ppt/_rels/presentation.xml.rels");
                using (var writer = new StreamWriter(presRelsEntry.Open()))
                {
                    writer.Write(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<Relationships xmlns=""http://schemas.openxmlformats.org/package/2006/relationships"">
  <Relationship Id=""rId1"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide"" Target=""slides/slide1.xml""/>
</Relationships>");
                }

                // ppt/slides/slide1.xml
                var slideEntry = archive.CreateEntry("ppt/slides/slide1.xml");
                using (var writer = new StreamWriter(slideEntry.Open()))
                {
                    string title = txtTitle.Text ?? "Yeni Sunum";
                    writer.Write($@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<p:sld xmlns:a=""http://schemas.openxmlformats.org/drawingml/2006/main"" xmlns:p=""http://schemas.openxmlformats.org/presentationml/2006/main"">
  <p:cSld>
    <p:spTree>
      <p:nvGrpSpPr><p:cNvPr id=""1"" name=""""/><p:cNvGrpSpPr/><p:nvPr/></p:nvGrpSpPr>
      <p:grpSpPr/>
      <p:sp>
        <p:nvSpPr><p:cNvPr id=""2"" name=""Title""/><p:cNvSpPr/><p:nvPr/></p:nvSpPr>
        <p:spPr><a:xfrm><a:off x=""500000"" y=""500000""/><a:ext cx=""8000000"" cy=""1000000""/></a:xfrm></p:spPr>
        <p:txBody><a:bodyPr/><a:p><a:r><a:t>{System.Security.SecurityElement.Escape(title)}</a:t></a:r></a:p></p:txBody>
      </p:sp>
    </p:spTree>
  </p:cSld>
</p:sld>");
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validasyon
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                XtraMessageBox.Show("Başlık zorunludur.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFilePath.Text))
            {
                XtraMessageBox.Show("Dosya yolu zorunludur.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnBrowse.Focus();
                return;
            }

            // Dosya varlığını kontrol et (opsiyonel uyarı)
            if (!File.Exists(txtFilePath.Text))
            {
                var result = XtraMessageBox.Show(
                    $"Belirtilen dosya bulunamadı:\n{txtFilePath.Text}\n\nYine de kaydetmek istiyor musunuz?",
                    "Dosya Bulunamadı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                    return;
            }

            try
            {
                _document.Title = txtTitle.Text.Trim();
                _document.FilePath = txtFilePath.Text.Trim();
                _document.FileType = DocumentReference.GetFileType(txtFilePath.Text);
                _document.Description = txtDescription.Text?.Trim();
                _document.ProjectId = cmbProject.EditValue as int?;
                _document.ModuleId = cmbModule.EditValue as int?;
                _document.WorkItemId = cmbWorkItem.EditValue as int?;
                _document.MeetingId = cmbMeeting.EditValue as int?;
                _document.IsFavorite = chkFavorite.Checked;

                // Etiketleri güncelle
                _document.Tags.Clear();
                foreach (CheckedListBoxItem item in chkTags.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        var tagId = (int)item.Value;
                        var tag = _context.DocumentTags.Find(tagId);
                        if (tag != null)
                        {
                            _document.Tags.Add(tag);
                        }
                    }
                }

                if (!_documentId.HasValue)
                {
                    // Yeni ekleme
                    _context.DocumentReferences.Add(_document);
                }

                _context.SaveChanges();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Kayıt hatası:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
