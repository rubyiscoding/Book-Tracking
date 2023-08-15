using System.ComponentModel.DataAnnotations;

namespace BookTracker.Models
{
    public class AddEditCategoryTypeViewModel
    {
        [Required(ErrorMessage = "Please enter a category name.")]
        [Display(Name = "Category Name")]
        [StringLength(250)]
        public string Name { get; set; }
    }
}