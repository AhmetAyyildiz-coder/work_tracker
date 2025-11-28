using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_tracker.Data.Entities
{
    [Table("WorkItemRelations")]
    public class WorkItemRelation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int WorkItemId1 { get; set; }

        [Required]
        public int WorkItemId2 { get; set; }

        [Required]
        [MaxLength(20)]
        public string RelationType { get; set; } // Parent, Sibling

        public DateTime CreatedAt { get; set; }

        [MaxLength(100)]
        public string CreatedBy { get; set; }

        // Navigation Properties
        [ForeignKey("WorkItemId1")]
        public virtual WorkItem SourceWorkItem { get; set; }

        [ForeignKey("WorkItemId2")]
        public virtual WorkItem TargetWorkItem { get; set; }

        public WorkItemRelation()
        {
            CreatedAt = DateTime.Now;
        }
    }

    public static class WorkItemRelationTypes
    {
        public const string Parent = "Parent";
        public const string Sibling = "Sibling";
    }
}