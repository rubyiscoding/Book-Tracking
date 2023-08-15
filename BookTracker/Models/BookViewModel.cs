
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookTracker.Models
{
    public class AddEditBookViewModel
    {
        [Required(ErrorMessage = "Please select a Category.")]
        [Display(Name = "Category")]
        public short CategoryId { get; set; }
        public IEnumerable<SelectListItem> CategorySelectList { get; set; }

        [Required(ErrorMessage = "Please enter the ISBN of the book.")]
        [Display(Name = "ISBN")]
        [StringLength(20)]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Please enter a title of the book.")]
        [Display(Name = "Book Title")]
        [StringLength(250)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter the author of the book.")]
        [Display(Name = "Book Author")]
        [StringLength(250)]
        public string Author { get; set; }

        public DateTime CreatedDate = DateTime.Now;
        public DateTime LastUpdatedDate { get; set; }

        public TrackingStatus Status { get; set; }
    }
    public enum TrackingStatus
    {
        Started = 0, InProgress = 1, Completed = 2, GotBoredAndQuit = 3
    }
}
