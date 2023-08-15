using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookTracker.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CategoryType")]
        public int CategoryTypeId { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string NameToken { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Description { get; set; }
        public ICollection<Book> Books { get; set; }
        //since Book uses CategoryId as a foreign key, we need to define a collection of Book.
    }
}