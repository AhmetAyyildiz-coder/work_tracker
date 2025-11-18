using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_tracker.Data.Entities
{
    [Table("WikiPages")]
    public class WikiPage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(200)]
        public string Slug { get; set; }

        public string ContentHtml { get; set; }

        [MaxLength(500)]
        public string Summary { get; set; }

        public int? ParentPageId { get; set; }

        [ForeignKey("ParentPageId")]
        public virtual WikiPage ParentPage { get; set; }

        public virtual ICollection<WikiPage> ChildPages { get; set; }

        [Required]
        [MaxLength(200)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [MaxLength(200)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsArchived { get; set; }

        public WikiPage()
        {
            CreatedAt = DateTime.Now;
            IsArchived = false;
            ChildPages = new HashSet<WikiPage>();
        }
    }
}

