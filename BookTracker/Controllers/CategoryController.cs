using System.Diagnostics;
using BookTracker.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookTracker.Controllers
{

    public class CategoryController : Controller
    {

        private readonly BookTrackerContext _db;
        public CategoryController(BookTrackerContext db)
        {
            _db = db;
        }
    }
}
