using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace work_tracker.Data.Entities
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(7)]
        public string ColorHex { get; set; } // e.g., "#FF5733"

        public virtual ICollection<WorkItem> WorkItems { get; set; } = new HashSet<WorkItem>();
    }
}
