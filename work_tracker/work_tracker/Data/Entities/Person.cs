using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_tracker.Data.Entities
{
    /// <summary>
    /// İşi talep eden veya ilgili kişiler
    /// </summary>
    [Table("Persons")]
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        /// <summary>
        /// Bu kişinin talep ettiği iş öğeleri
        /// </summary>
        public virtual ICollection<WorkItem> RequestedWorkItems { get; set; } = new HashSet<WorkItem>();

        /// <summary>
        /// Bu kişiyle ilgili zaman kayıtları
        /// </summary>
        public virtual ICollection<TimeEntry> TimeEntries { get; set; } = new HashSet<TimeEntry>();
    }
}

