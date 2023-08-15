using System.Diagnostics;
using BookTracker.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookTracker.Controllers
{

    public class BookController : Controller
    {

        private readonly BookTrackerContext _db;
        public BookController(BookTrackerContext db)
        {
            _db = db;
        }
    }
}
