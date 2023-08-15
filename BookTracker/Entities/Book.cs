using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookTracker.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string ISBN { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Title { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Author { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public string Status { get; set; } //started reading, in progress, completed, got bored and quit
    }
}

