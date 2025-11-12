using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_tracker.Data.Entities
{
    /// <summary>
    /// İş öğelerine eklenen dosyalar (SQL scriptleri, dokümanlar, ekran görüntüleri vb.)
    /// </summary>
    [Table("WorkItemAttachments")]
    public class WorkItemAttachment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// İlgili iş öğesinin Id'si
        /// </summary>
        [Required]
        public int WorkItemId { get; set; }

        /// <summary>
        /// Orijinal dosya adı (kullanıcının yüklediği ad)
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string OriginalFileName { get; set; }

        /// <summary>
        /// Sistemde kaydedilen dosya adı (unique, GUID bazlı)
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string StoredFileName { get; set; }

        /// <summary>
        /// Dosya uzantısı (.sql, .pdf, .docx vb.)
        /// </summary>
        [MaxLength(50)]
        public string FileExtension { get; set; }

        /// <summary>
        /// Dosya boyutu (byte cinsinden)
        /// </summary>
        public long FileSizeBytes { get; set; }

        /// <summary>
        /// MIME type (application/sql, image/png vb.)
        /// </summary>
        [MaxLength(100)]
        public string MimeType { get; set; }

        /// <summary>
        /// Dosya açıklaması/notu
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// Dosyayı yükleyen kişi
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string UploadedBy { get; set; }

        /// <summary>
        /// Yükleme zamanı
        /// </summary>
        [Required]
        public DateTime UploadedAt { get; set; }

        /// <summary>
        /// Fiziksel dosya yolu (relative path)
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string FilePath { get; set; }

        /// <summary>
        /// İlgili iş öğesi (Navigation Property)
        /// </summary>
        [ForeignKey("WorkItemId")]
        public virtual WorkItem WorkItem { get; set; }

        public WorkItemAttachment()
        {
            UploadedAt = DateTime.Now;
        }
    }
}

