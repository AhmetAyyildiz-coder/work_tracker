using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_tracker.Data.Entities
{
    /// <summary>
    /// Döküman Referans Entity'si
    /// Word, Excel, PDF vb. dosyalara referans tutar (Outlook entegrasyonu gibi)
    /// Dosyanın kendisini saklamaz, sadece yolunu ve meta bilgilerini tutar
    /// </summary>
    [Table("DocumentReferences")]
    public class DocumentReference
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Döküman başlığı
        /// </summary>
        [Required]
        [MaxLength(300)]
        public string Title { get; set; }

        /// <summary>
        /// Dosyanın tam yolu (örn: \\server\docs\teknik.docx veya C:\Docs\rapor.pdf)
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string FilePath { get; set; }

        /// <summary>
        /// Dosya türü (Word, Excel, PDF, PowerPoint, Text, Other)
        /// </summary>
        [MaxLength(50)]
        public string FileType { get; set; }

        /// <summary>
        /// Kısa açıklama
        /// </summary>
        [MaxLength(1000)]
        public string Description { get; set; }

        /// <summary>
        /// İlişkili proje (opsiyonel)
        /// </summary>
        public int? ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        /// <summary>
        /// İlişkili modül (opsiyonel)
        /// </summary>
        public int? ModuleId { get; set; }

        [ForeignKey("ModuleId")]
        public virtual ProjectModule Module { get; set; }

        /// <summary>
        /// İlişkili iş kalemi (opsiyonel)
        /// </summary>
        public int? WorkItemId { get; set; }

        [ForeignKey("WorkItemId")]
        public virtual WorkItem WorkItem { get; set; }

        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Oluşturan kullanıcı
        /// </summary>
        [MaxLength(200)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Son erişim tarihi
        /// </summary>
        public DateTime? LastAccessedAt { get; set; }

        /// <summary>
        /// Favori mi?
        /// </summary>
        public bool IsFavorite { get; set; }

        /// <summary>
        /// Arşivlenmiş mi?
        /// </summary>
        public bool IsArchived { get; set; }

        /// <summary>
        /// İlişkili etiketler (Many-to-Many)
        /// </summary>
        public virtual ICollection<DocumentTag> Tags { get; set; }

        public DocumentReference()
        {
            CreatedAt = DateTime.Now;
            CreatedBy = Environment.UserName;
            IsFavorite = false;
            IsArchived = false;
            Tags = new HashSet<DocumentTag>();
        }

        /// <summary>
        /// Dosya uzantısından dosya türünü otomatik belirler
        /// </summary>
        public static string GetFileType(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return "Other";

            var extension = System.IO.Path.GetExtension(filePath).ToLowerInvariant();

            switch (extension)
            {
                case ".doc":
                case ".docx":
                case ".docm":
                case ".rtf":
                    return "Word";
                case ".xls":
                case ".xlsx":
                case ".xlsm":
                case ".csv":
                    return "Excel";
                case ".pdf":
                    return "PDF";
                case ".ppt":
                case ".pptx":
                case ".pptm":
                    return "PowerPoint";
                case ".txt":
                case ".log":
                case ".md":
                    return "Text";
                case ".png":
                case ".jpg":
                case ".jpeg":
                case ".gif":
                case ".bmp":
                    return "Image";
                case ".zip":
                case ".rar":
                case ".7z":
                    return "Archive";
                default:
                    return "Other";
            }
        }
    }
}
