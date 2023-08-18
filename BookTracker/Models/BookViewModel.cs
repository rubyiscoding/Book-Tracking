
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace BookTracker.Models
{
    public class SearchBookViewModel
    {
        public int Id { get; set; }

        public string ISBN { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        [Display(Name = "Book Logged Date")]
        public string CreatedDate { get; set; }

        [Display(Name = "Last Logged Date")]
        public string LastUpdatedDate { get; set; }

        public string Status { get; set; } //started reading, in progress, completed, got bored and quit

        public string CategoryName { get; set; }
    }

    public class AddEditBookViewModel
    {
        public int ? Id { get; set; }

        [Required(ErrorMessage = "Please select a Category.")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

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
        [Description ("Started")]
        Started ,
        [Description("In Progress")]
        InProgress ,
        [Description("Completed")]
        Completed ,
        [Description("Got Bored And Quit")]
        GotBoredAndQuit 
    }
}
