using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_tracker.Data.Entities
{
    [Table("WorkItemEmails")]
    public class WorkItemEmail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Outlook'tan gelen unique Entry ID (Outlook'ta email'i tekrar bulmak için)
        /// </summary>
        [MaxLength(500)]
        public string OutlookEntryId { get; set; }

        /// <summary>
        /// Email konusu
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Subject { get; set; }

        /// <summary>
        /// Gönderen
        /// </summary>
        [MaxLength(500)]
        public string From { get; set; }

        /// <summary>
        /// Alıcılar (virgülle ayrılmış)
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// CC alıcıları (virgülle ayrılmış)
        /// </summary>
        public string Cc { get; set; }

        /// <summary>
        /// Email içeriği (HTML veya plain text)
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Email'in HTML formatında olup olmadığı
        /// </summary>
        public bool IsHtml { get; set; }

        /// <summary>
        /// Alınma tarihi
        /// </summary>
        public DateTime? ReceivedDate { get; set; }

        /// <summary>
        /// Gönderilme tarihi
        /// </summary>
        public DateTime? SentDate { get; set; }

        /// <summary>
        /// Okundu mu?
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// Ek dosya var mı?
        /// </summary>
        public bool HasAttachments { get; set; }

        /// <summary>
        /// Ek dosya sayısı
        /// </summary>
        public int AttachmentCount { get; set; }

        /// <summary>
        /// Bağlı olduğu iş talebi
        /// </summary>
        public int? WorkItemId { get; set; }

        /// <summary>
        /// Email'i bağlayan kullanıcı
        /// </summary>
        [MaxLength(200)]
        public string LinkedBy { get; set; }

        /// <summary>
        /// Bağlanma tarihi
        /// </summary>
        public DateTime LinkedAt { get; set; }

        /// <summary>
        /// Notlar (kullanıcı tarafından eklenen)
        /// </summary>
        public string Notes { get; set; }

        // Navigation Properties
        [ForeignKey("WorkItemId")]
        public virtual WorkItem WorkItem { get; set; }

        public WorkItemEmail()
        {
            LinkedAt = DateTime.Now;
            IsRead = false;
            HasAttachments = false;
            AttachmentCount = 0;
            IsHtml = false;
        }
    }
}

