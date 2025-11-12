using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_tracker.Data.Entities
{
    [Table("Projects")]
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public virtual ICollection<ProjectModule> Modules { get; set; }
        public virtual ICollection<WorkItem> WorkItems { get; set; }

        public Project()
        {
            IsActive = true;
            CreatedAt = DateTime.Now;
            Modules = new HashSet<ProjectModule>();
            WorkItems = new HashSet<WorkItem>();
        }
    }
}

