using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_tracker.Data.Entities
{
    [Table("WorkItems")]
    public class WorkItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Title { get; set; }

        public string Description { get; set; }

        [MaxLength(200)]
        public string RequestedBy { get; set; }

        [Required]
        public DateTime RequestedAt { get; set; }

        public int? ProjectId { get; set; }

        public int? ModuleId { get; set; }

        public int? SourceMeetingId { get; set; }

        public int? SprintId { get; set; }

        /// <summary>
        /// İşin ilk kez planlandığı sprint.
        /// Sprint değiştirildiğinde güncellenmez, sadece ilk atamada set edilir.
        /// </summary>
        public int? InitialSprintId { get; set; }

        /// <summary>
        /// İşin tamamlandığı sprint.
        /// Kart tamamlandı sütunundan çıkarsa bu alan isteğe göre temizlenebilir.
        /// </summary>
        public int? CompletedInSprintId { get; set; }

        [MaxLength(100)]
        public string Type { get; set; } // AcilArge, Bug, YeniOzellik, vb.

        [MaxLength(50)]
        public string Urgency { get; set; } // Kritik, Yuksek, Normal

        public decimal? EffortEstimate { get; set; }

        [MaxLength(50)]
        public string Board { get; set; } // Inbox, Scrum, Kanban

        [MaxLength(100)]
        public string Status { get; set; } // Bekliyor, Triage, SprintBacklog, Gelistirmede, Testte, Tamamlandi, vb.

        public int OrderIndex { get; set; }

        [MaxLength(200)]
        public string TriagedBy { get; set; }

        public DateTime? TriagedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? StartedAt { get; set; } // Müdahele Ediliyor/Geliştirmede durumuna geçildiğinde set edilir

        public DateTime? CompletedAt { get; set; }

        public bool IsArchived { get; set; }

        // Navigation Properties
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        [ForeignKey("ModuleId")]
        public virtual ProjectModule Module { get; set; }

        [ForeignKey("SourceMeetingId")]
        public virtual Meeting SourceMeeting { get; set; }

        [ForeignKey("SprintId")]
        public virtual Sprint Sprint { get; set; }

        [ForeignKey("InitialSprintId")]
        public virtual Sprint InitialSprint { get; set; }

        [ForeignKey("CompletedInSprintId")]
        public virtual Sprint CompletedInSprint { get; set; }

        public virtual ICollection<WorkItemActivity> Activities { get; set; }
        public virtual ICollection<WorkItemAttachment> Attachments { get; set; }
        public virtual ICollection<WorkItemEmail> Emails { get; set; }
        public virtual ICollection<Tag> Tags {get;set;}

        public WorkItem()
        {
            RequestedAt = DateTime.Now;
            CreatedAt = DateTime.Now;
            Board = "Inbox";
            Status = "Bekliyor";
            OrderIndex = 0;
            IsArchived = false;
            Activities = new HashSet<WorkItemActivity>();
            Attachments = new HashSet<WorkItemAttachment>();
            Emails = new HashSet<WorkItemEmail>();
            Tags = new HashSet<Tag>();
        }
    }
}

