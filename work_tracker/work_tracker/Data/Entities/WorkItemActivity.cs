using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_tracker.Data.Entities
{
    /// <summary>
    /// İş öğeleri üzerinde yapılan tüm aktiviteleri (yorum, durum değişikliği, atama vb.) kaydeder
    /// </summary>
    [Table("WorkItemActivities")]
    public class WorkItemActivity
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
        /// Aktivite tipi: Comment, StatusChange, AssignmentChange, FieldUpdate, Created
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ActivityType { get; set; }

        /// <summary>
        /// Aktivite açıklaması veya yorum metni
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Eski değer (durum değişikliği gibi durumlarda)
        /// </summary>
        [MaxLength(500)]
        public string OldValue { get; set; }

        /// <summary>
        /// Yeni değer (durum değişikliği gibi durumlarda)
        /// </summary>
        [MaxLength(500)]
        public string NewValue { get; set; }

        /// <summary>
        /// Aktiviteyi gerçekleştiren kişi
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Aktivite oluşturulma zamanı
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// İlgili iş öğesi (Navigation Property)
        /// </summary>
        [ForeignKey("WorkItemId")]
        public virtual WorkItem WorkItem { get; set; }

        public WorkItemActivity()
        {
            CreatedAt = DateTime.Now;
        }
    }

    /// <summary>
    /// Aktivite tipleri için sabit değerler
    /// </summary>
    public static class ActivityTypes
    {
        public const string Comment = "Comment";              // Yorum eklendi
        public const string StatusChange = "StatusChange";    // Durum değişti
        public const string AssignmentChange = "AssignmentChange"; // Atama değişti
        public const string FieldUpdate = "FieldUpdate";      // Alan güncellendi
        public const string Created = "Created";              // İş oluşturuldu
        public const string PriorityChange = "PriorityChange"; // Öncelik değişti
        public const string EstimateChange = "EstimateChange"; // Efor tahmini değişti
    }
}

