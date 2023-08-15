using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookTracker.Entities
{

    public class CategoryType
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Name { get; set; }

        public ICollection<Category> Categories { get; set; }
        //since Category uses CategoryType as foreign key, we need to define a collection of Category.
    }
}