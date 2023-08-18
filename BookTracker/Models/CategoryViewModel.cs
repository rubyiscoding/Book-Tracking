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

    public class AddEditCategoryViewModel
    {
        public int ? Id { get; set; }

        [Required(ErrorMessage = "Please enter a category name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a category description.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select a Category type.")]
        [Display(Name = "Category Type Id")]
        public int CategoryTypeId { get; set; }

        public IEnumerable<SelectListItem> CategoryTypeSelectList { get; set; }
    }

    public class SearchCategoryViewModel
    {
        public int ? Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryTypeName { get; set; }
    }
}