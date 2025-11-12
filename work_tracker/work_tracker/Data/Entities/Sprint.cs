using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_tracker.Data.Entities
{
    [Table("Sprints")]
    public class Sprint
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Goals { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } // Planned, Active, Completed, Cancelled

        public DateTime CreatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        // Navigation Properties
        public virtual ICollection<WorkItem> WorkItems { get; set; }

        public Sprint()
        {
            Status = "Planned";
            CreatedAt = DateTime.Now;
            WorkItems = new HashSet<WorkItem>();
        }

        // Hesaplanan özellikler
        [NotMapped]
        public int DurationDays
        {
            get { return (EndDate - StartDate).Days + 1; }
        }

        [NotMapped]
        public int RemainingDays
        {
            get
            {
                if (Status == "Completed" || Status == "Cancelled")
                    return 0;
                
                var remaining = (EndDate - DateTime.Now).Days;
                return remaining > 0 ? remaining : 0;
            }
        }

        [NotMapped]
        public bool IsActive
        {
            get { return Status == "Active"; }
        }
    }
}

