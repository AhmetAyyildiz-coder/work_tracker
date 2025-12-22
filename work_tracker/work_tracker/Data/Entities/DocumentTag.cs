using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_tracker.Data.Entities
{
    /// <summary>
    /// Döküman Etiketi Entity'si
    /// Dökümanları kategorize etmek için kullanılır
    /// </summary>
    [Table("DocumentTags")]
    public class DocumentTag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Etiket adı (örn: "Teknik Döküman", "Kullanım Kılavuzu", "API Referans")
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Etiket rengi (hex kodu, örn: #FF5733)
        /// </summary>
        [MaxLength(20)]
        public string Color { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Bu etikete sahip dökümanlar (Many-to-Many)
        /// </summary>
        public virtual ICollection<DocumentReference> Documents { get; set; }

        public DocumentTag()
        {
            CreatedAt = DateTime.Now;
            Color = "#107C10"; // Varsayılan yeşil
            Documents = new HashSet<DocumentReference>();
        }

        /// <summary>
        /// Varsayılan etiketler
        /// </summary>
        public static List<DocumentTag> GetDefaultTags()
        {
            return new List<DocumentTag>
            {
                new DocumentTag { Name = "Teknik Döküman", Color = "#0078D4", Description = "Teknik şartnameler ve dökümanlar" },
                new DocumentTag { Name = "Kullanım Kılavuzu", Color = "#107C10", Description = "Kullanıcı kılavuzları" },
                new DocumentTag { Name = "API Referans", Color = "#5C2D91", Description = "API dökümanları" },
                new DocumentTag { Name = "Müşteri Dökümanı", Color = "#FF8C00", Description = "Müşteriden gelen dökümanlar" },
                new DocumentTag { Name = "Toplantı Notu", Color = "#E81123", Description = "Toplantı notları" },
                new DocumentTag { Name = "Analiz", Color = "#00B294", Description = "Analiz dökümanları" },
                new DocumentTag { Name = "Test Dökümanı", Color = "#8764B8", Description = "Test senaryoları ve raporları" },
                new DocumentTag { Name = "Genel", Color = "#767676", Description = "Genel dökümanlar" }
            };
        }
    }
}
