using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookTracker.Models
{
    public class CategoryViewModel
    {
        [Required(ErrorMessage = "Please select a category name.")]
        [Display(Name = "Category Name")]
        public short CategoryTypeId { get; set; }
        public ICollection<SelectListItem> CategoryTypeSelectList { get; set; }

        [Required(ErrorMessage = "Please enter a category name.")]
        [Display(Name = "Category Name")]
        [StringLength(250)]
        public string NameToken { get; set; }

        [Required(ErrorMessage = "Please enter a category description.")]
        [Display(Name = "Category Description")]
        [StringLength(250)]
        public string Description { get; set; }
    }
}