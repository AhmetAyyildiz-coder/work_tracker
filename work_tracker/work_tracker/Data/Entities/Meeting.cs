using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_tracker.Data.Entities
{
    [Table("Meetings")]
    public class Meeting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Subject { get; set; }

        [Required]
        public DateTime MeetingDate { get; set; }

        [MaxLength(2000)]
        public string Participants { get; set; }

        public string NotesHtml { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public virtual ICollection<WorkItem> WorkItems { get; set; }

        public Meeting()
        {
            MeetingDate = DateTime.Now;
            CreatedAt = DateTime.Now;
            WorkItems = new HashSet<WorkItem>();
        }
    }
}

