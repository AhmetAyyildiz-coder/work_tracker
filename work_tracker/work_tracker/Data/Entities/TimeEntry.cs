using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_tracker.Data.Entities
{
    /// <summary>
    /// Zaman kayıtları için entity - Telefon görüşmeleri, işler, toplantılar vb. aktiviteler
    /// </summary>
    [Table("TimeEntries")]
    public class TimeEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Aktivite tarihi ve saati
        /// </summary>
        [Required]
        public DateTime EntryDate { get; set; }

        /// <summary>
        /// Süre (dakika cinsinden)
        /// </summary>
        [Required]
        public int DurationMinutes { get; set; }

        /// <summary>
        /// Aktivite tipi: PhoneCall, Work, Meeting, Other
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ActivityType { get; set; }

        /// <summary>
        /// İlgili iş öğesi (opsiyonel)
        /// </summary>
        public int? WorkItemId { get; set; }

        /// <summary>
        /// İlgili proje (opsiyonel)
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// İlgili kişi (opsiyonel)
        /// </summary>
        public int? PersonId { get; set; }

        /// <summary>
        /// Kişi adı (telefon görüşmeleri için - geriye dönük uyumluluk)
        /// </summary>
        [MaxLength(200)]
        public string ContactName { get; set; }

        /// <summary>
        /// Telefon numarası (telefon görüşmeleri için - geriye dönük uyumluluk)
        /// </summary>
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Açıklama ve notlar
        /// </summary>
        [MaxLength(2000)]
        public string Description { get; set; }

        /// <summary>
        /// Kaydı oluşturan kişi
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Kayıt oluşturulma zamanı
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Son güncelleme zamanı
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties
        [ForeignKey("WorkItemId")]
        public virtual WorkItem WorkItem { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        public TimeEntry()
        {
            EntryDate = DateTime.Now;
            CreatedAt = DateTime.Now;
            CreatedBy = Environment.UserName;
        }
    }

    /// <summary>
    /// Zaman kaydı aktivite tipleri için sabit değerler
    /// </summary>
    public static class TimeEntryActivityTypes
    {
        public const string PhoneCall = "PhoneCall";    // Telefon görüşmesi
        public const string Work = "Work";             // İş
        public const string Meeting = "Meeting";       // Toplantı
        public const string Other = "Other";           // Diğer
    }
}