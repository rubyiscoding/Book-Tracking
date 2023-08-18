using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookTracker.Data;
using BookTracker.Entities;
using BookTracker.Models;
using BookTracker.Managers;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Reflection;

namespace BookTracker.Controllers
{
    public class BookController : Controller
    {
        private readonly BookTrackerContext _context;
        private readonly ICategoryManager _categoryManager;
        private readonly IBookManager _bookManager;

        public BookController(BookTrackerContext context, ICategoryManager categoryManager, IBookManager bookManager)
        {
            _context = context;
            _categoryManager = categoryManager;
            _bookManager = bookManager;
        }

        // GET: Book
        public async Task<IActionResult> Index()
        {
            var model =
         await (from book in _context.Books
            join category in _context.Categories on book.CategoryId equals category.Id
             select new SearchBookViewModel
         {
             Id = book.Id,
             CategoryName = category.NameToken,
            ISBN = book.ISBN,
            Title = book.Title,
                 Author = book.Author,
                 CreatedDate = book.CreatedDate.ToShortDateString(),
                 LastUpdatedDate = book.LastUpdatedDate.ToShortDateString(),
                 Status = book.Status
             }).ToListAsync();



            //var books = await _context.Books.ToListAsync();
            //var model = new List <SearchBookViewModel>();
            //if (books != null && books.Count()>0)
            //{
            //    foreach (var book in books)
            //    {
            //        model.Add(new SearchBookViewModel
            //        {
            //            Id = book.Id,
            //            CategoryName = book.CategoryId.ToString(),
            //            ISBN = book.ISBN,
            //            Title = book.Title,
            //            Author = book.Author,
            //            CreatedDate = book.CreatedDate.ToShortDateString(),
            //            LastUpdatedDate = book.LastUpdatedDate.ToShortDateString(),
            //            Status = book.Status
            //        });
            //    }
            //}

            return View(model);
        }

        // GET: Book/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Book/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,CategoryId,ISBN,Title,Author,CreatedDate,LastUpdatedDate,Status")] Book book)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(book);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(book);
        //}

        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,ISBN,Title,Author,CreatedDate,LastUpdatedDate,Status")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }


        #region Create Book

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new AddEditBookViewModel();
            try
            {
                var bookCategorySelectListItems = await CategorySelectListAsync();
                model.CategorySelectList = bookCategorySelectListItems;
                model.CreatedDate = DateTime.Now;
                model.LastUpdatedDate = DateTime.Now;
                return View(model);
            }
            catch (Exception ex)
            {
                //TODO Do something with ex

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddEditBookViewModel model )
        {
            //model.CategorySelectList = await _categoryManager.GetBookCategorySelectListItemsAsync();
            model.CategorySelectList = await CategorySelectListAsync();
            //if (ModelState.IsValid)
            //{
            var book = new Book
            {
            ISBN = model.ISBN,
            Author = model.Author,
            CategoryId = model.CategoryId,
            Title = model.Title,
            CreatedDate = DateTime.Now,
            LastUpdatedDate = DateTime.Now,
            Status = TrackingStatus.Started.ToString() //TODO Started.GetDescription
        };
            //TODO The below method could be extracted into a new class Book Manager. Implement this once you learn inecting context into class.

            //var response = await _bookManager.InsertAsync(book);
            //if (response)
            try
            {
                _context.Add(book);
                var sth = book;
                var response = await _context.SaveChangesAsync();
                if (response != null)
                   return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var sth = ex.Message;
            }
            //return RedirectToAction(nameof(Index));
            //else 
            //}
            return View(model);
        }

        private async Task<IEnumerable<SelectListItem>> CategorySelectListAsync()
        {
            var categorySelectListItems = new List<SelectListItem>();
            try
            {
                var categories = await _context.Categories.ToListAsync();
                
                    if (categories == null) return categorySelectListItems;

                    foreach (var item in categories)
                    {
                        categorySelectListItems.Add(new SelectListItem
                        {
                            Text = item.NameToken,
                            Value = item.Id.ToString()
                        });
                    }
                    return categorySelectListItems;

            }
            catch (Exception ex)
            {
                var sth = ex.Message; //TODO do sth with exception, either log or something.  
                return categorySelectListItems;
            }
        }
        #endregion

        #region Edit


        // GET: Book/Edit/5
        [HttpGet]
        public async Task<IActionResult>EditBook(int? id)
        {
            if (id == null)
                return NotFound();
            

            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();
            
            var model = new AddEditBookViewModel();

            try
            {
                var bookCategorySelectListItems = await CategorySelectListAsync();
                model.CategorySelectList = bookCategorySelectListItems;
                model.Id = book.Id;
                model.ISBN = book.ISBN;
                model.Author = book.Author;
                model.Title = book.Title;
                model.Status = TrackingStatus.Started;//TODO book.Status huna parcha . 
                model.CreatedDate = book.CreatedDate;
                model.LastUpdatedDate = book.LastUpdatedDate;
                return View("Create", model);
            }
            catch (Exception ex)
            {
                //TODO Do something with ex

                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBook(AddEditBookViewModel model)
        {
            if (model.Id == null)
                return NotFound();

            var editBook = await _context.Books.FindAsync(model.Id);
            if (editBook == null)
                return NotFound();

            model.CategorySelectList = await CategorySelectListAsync();

            if (ModelState.IsValid)
            {

                try
                {
                    editBook.LastUpdatedDate = DateTime.Now;
                    editBook.Status = model.Status.ToString();
                    
                    _context.Update(editBook);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(editBook.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View("Create",model); 
        }





        #endregion


        #region helper methods
        static string GetEnumDescription(Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }

            return value.ToString();
        }
    
    #endregion

}
}
