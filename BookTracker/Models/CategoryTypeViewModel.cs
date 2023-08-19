using System.ComponentModel.DataAnnotations;

namespace BookTracker.Models
{
    public class AddEditCategoryTypeViewModel
    {

        public int? Id { get; set; }

        [Required(ErrorMessage = "Please enter a category name.")]
        [Display(Name = "Category Name")]
        [StringLength(250)]
        public string Name { get; set; }

    }
}