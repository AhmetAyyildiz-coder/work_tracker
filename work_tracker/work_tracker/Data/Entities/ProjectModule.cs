using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_tracker.Data.Entities
{
    [Table("ProjectModules")]
    public class ProjectModule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        public virtual ICollection<WorkItem> WorkItems { get; set; }

        public ProjectModule()
        {
            IsActive = true;
            CreatedAt = DateTime.Now;
            WorkItems = new HashSet<WorkItem>();
        }
    }
}

