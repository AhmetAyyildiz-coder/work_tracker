using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_tracker.Data.Entities
{
    [Table("KanbanColumnSettings")]
    public class KanbanColumnSetting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Board { get; set; } // Kanban, Scrum

        [Required]
        [MaxLength(100)]
        public string ColumnName { get; set; }

        public int? WipLimit { get; set; }

        public int DisplayOrder { get; set; }

        public KanbanColumnSetting()
        {
            DisplayOrder = 0;
        }
    }
}

