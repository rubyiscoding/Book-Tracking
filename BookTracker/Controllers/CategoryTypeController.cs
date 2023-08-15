using System.Diagnostics;
using BookTracker.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookTracker.Controllers
{

    public class CategoryTypeController : Controller
    {

        private readonly BookTrackerContext _db;
        public CategoryTypeController(BookTrackerContext db)
        {
            _db = db;
        }
    }
}
