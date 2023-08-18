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


            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new AddEditBookViewModel();
            try
            {
                var bookCategorySelectListItems = await CategorySelectListAsync();
                var bookStatusSelectListItems = GetBookStatusSelectList();
                model.CategorySelectList = bookCategorySelectListItems;
                model.BookStatusSelectListItems = bookStatusSelectListItems;
                model.CreatedDate = DateTime.Now;
                model.LastUpdatedDate = DateTime.Now;
                model.BookStatus = GetEnumDescription(TrackingStatus.Started);
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
        public async Task<IActionResult> Create(AddEditBookViewModel model)
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
                Status = model.BookStatus,// GetEnumDescription(TrackingStatus.Started)// set to Started by Default when you create book
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

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();


            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            var model = new AddEditBookViewModel();

            // build up book status enum DDL            
            var bookCategorySelectListItems = await CategorySelectListAsync();

            try
            {

                model.CategorySelectList = bookCategorySelectListItems;
                model.BookStatusSelectListItems = GetBookStatusSelectList();
                model.Id = book.Id;
                model.ISBN = book.ISBN;
                model.Author = book.Author;
                model.Title = book.Title;
                model.BookStatus = book.Status;// TrackingStatus.Started;//TODO book.Status huna parcha . 
                model.CreatedDate = book.CreatedDate;
                model.LastUpdatedDate = book.LastUpdatedDate;
                model.CategoryId = book.CategoryId;

                return View("Edit", model);
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
        public async Task<IActionResult> Edit(AddEditBookViewModel model)
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
            return View("Edit", model);
        }


        // GET: Book/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model =
         await (from book in _context.Books
                join category in _context.Categories on book.CategoryId equals category.Id
                where book.Id == id.Value
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
                }).FirstOrDefaultAsync();

            if (model == null)
            {
                return NotFound();
            }
            
            return View(model);
        }

        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model =
         await (from book in _context.Books
                join category in _context.Categories on book.CategoryId equals category.Id
                where book.Id == id.Value
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
                }).FirstOrDefaultAsync();

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
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
                var sth = ex.Message;
                Console.WriteLine(ex.Message); //TODO do sth with exception, either log or something.  
                return categorySelectListItems;
            }
        }

        


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


        //returns Book Status drop down list item using book status enum types
        public IEnumerable<SelectListItem> GetBookStatusSelectList()
    {
        var bookStatusSelectListItems = new List<SelectListItem>();

        var bookStatusEnums = Enum.GetValues(typeof(TrackingStatus)).Cast<TrackingStatus>();

        foreach (var item in bookStatusEnums)
        {
            bookStatusSelectListItems.Add(new SelectListItem
            {
                Text = (item).ToString(),
                Value = (item).ToString()
            });
        }


        return bookStatusSelectListItems;
    }



    #endregion

}
}
