using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_tracker.Data.Entities
{
    /// <summary>
    /// İş öğeleri için hatırlatıcı kayıtları
    /// </summary>
    [Table("WorkItemReminders")]
    public class WorkItemReminder
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
        /// Hatırlatma zamanı
        /// </summary>
        [Required]
        public DateTime ReminderDate { get; set; }

        /// <summary>
        /// Hatırlatma notu
        /// </summary>
        [MaxLength(500)]
        public string Note { get; set; }

        /// <summary>
        /// Hatırlatma gösterildi mi?
        /// </summary>
        public bool IsShown { get; set; }

        /// <summary>
        /// Hatırlatma kapatıldı mı (dismiss)?
        /// </summary>
        public bool IsDismissed { get; set; }

        /// <summary>
        /// Erteleme sayısı
        /// </summary>
        public int SnoozeCount { get; set; }

        /// <summary>
        /// Son erteleme zamanı
        /// </summary>
        public DateTime? LastSnoozedAt { get; set; }

        /// <summary>
        /// Oluşturan kullanıcı
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Oluşturulma zamanı
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// İlgili iş öğesi (Navigation Property)
        /// </summary>
        [ForeignKey("WorkItemId")]
        public virtual WorkItem WorkItem { get; set; }

        public WorkItemReminder()
        {
            CreatedAt = DateTime.Now;
            IsShown = false;
            IsDismissed = false;
            SnoozeCount = 0;
        }
    }
}
