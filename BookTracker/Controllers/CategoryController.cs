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

namespace BookTracker.Controllers
{
    public class CategoryController : Controller
    {
        private readonly BookTrackerContext _context;

        public CategoryController(BookTrackerContext context)
        {
            _context = context;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            var model = await (from category in _context.Categories
                               join categoryType in _context.CategoryTypes on category.CategoryTypeId equals categoryType.Id
                               select new SearchCategoryViewModel
                               {
                                   Id = category.Id,
                                   CategoryTypeName = categoryType.Name,
                                   Name = category.NameToken,
                                   Description = category.Description
                               }).ToListAsync();

            return View(model);
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var model = await (from category in _context.Categories
                                   join categoryType in _context.CategoryTypes on category.CategoryTypeId equals categoryType.Id
                                   where category.Id == id.Value
                                   select new SearchCategoryViewModel
                                   {
                                       Id = category.Id,
                                       CategoryTypeName = categoryType.Name,
                                       Name = category.NameToken,
                                       Description = category.Description
                                   }).FirstOrDefaultAsync();
                if (model == null)
                    return NotFound();

                return View(model);
            }
            catch (Exception ex)
            {
                // TODO : do sth with the ex
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Category/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var model = new AddEditCategoryViewModel();
                model.CategoryTypeSelectList = await CategoryTypeSelectListAsync(); // populate the DDL

                return View(model);
            }
            catch (Exception ex)
            {
                //TODO Do something with ex
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddEditCategoryViewModel model)
        {
            var bookCategoryTypeSelectListItems = await CategoryTypeSelectListAsync();
            model.CategoryTypeSelectList = bookCategoryTypeSelectListItems;

      
                var category = new Category { NameToken = model.Name, Description = model.Description, CategoryTypeId = model.CategoryTypeId };
                _context.Add(category);
                var response = await _context.SaveChangesAsync();
                if (response > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            

            return View(model);
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue || id == null)
                return NotFound();

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            var model = new AddEditCategoryViewModel();
            var bookCategoryTypeSelectListItems = await CategoryTypeSelectListAsync();

            model.CategoryTypeSelectList = bookCategoryTypeSelectListItems;
            model.Id = id.Value;
            model.Name = category.NameToken;
            model.Description = category.Description;
            model.CategoryTypeId = category.CategoryTypeId;

            return View(model);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AddEditCategoryViewModel model)
        {
            if (model.Id == null)
            {
                return NotFound();


            }
            var editCategory = await _context.Categories.FindAsync(model.Id);
            if (editCategory == null)
                return NotFound();

            var bookCategoryTypeSelectListItems = await CategoryTypeSelectListAsync();

            model.CategoryTypeSelectList = bookCategoryTypeSelectListItems;

            editCategory.NameToken = model.Name;
            editCategory.Description = model.Description;
            editCategory.CategoryTypeId = model.CategoryTypeId;

            _context.Update(editCategory);
            

            var response = await _context.SaveChangesAsync();
            if (response>0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await (from category in _context.Categories
                               join categoryType in _context.CategoryTypes on category.CategoryTypeId equals categoryType.Id
                               where category.Id == id.Value
                               select new SearchCategoryViewModel
                               {
                                   Id = category.Id,
                                   Name = category.NameToken,
                                   Description = category.Description,
                                   CategoryTypeName = categoryType.Name
                               }).FirstOrDefaultAsync();


            if (model == null)
                return NotFound();

            return View(model);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }

        #region Create Category

        private async Task<IEnumerable<SelectListItem>> CategoryTypesSelectListAsync()
        {
            var categorySelectListItems = new List<SelectListItem>();
            try
            {
                var categories = await _context.CategoryTypes.ToListAsync();

                //we need to retrieve category type from database now we are trying hard coding.
                //    Dictionary<int, string> categories = new Dictionary<int, string>
                //{
                //    { 1,"Suspense" },
                //    { 2,"Comedy" },
                //    { 3,"Romance" },
                //    { 4,"Drama" },
                //    { 5,"Thriller" },

                //};
                if (categories == null) return categorySelectListItems;

                foreach (var item in categories)
                {
                    categorySelectListItems.Add(new SelectListItem
                    {
                        Text = item.Name,
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


        /// <summary>
        /// Returns SelectList for CategoryType from db
        /// </summary>
        /// <returns>Ienum of selectlistItem, usaage: in DDL</returns>
        private async Task<IEnumerable<SelectListItem>> CategoryTypeSelectListAsync()
        {
            var categoryTypeSelectListItems = new List<SelectListItem>();
            try
            {
                var categoryTypes = await _context.CategoryTypes.ToListAsync();

                if (categoryTypes == null) return categoryTypeSelectListItems;

                foreach (var item in categoryTypes)
                {
                    categoryTypeSelectListItems.Add(new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString()
                    });
                }
                return categoryTypeSelectListItems;

            }
            catch (Exception ex)
            {
                var sth = ex.Message;
                Console.WriteLine(ex.Message); //TODO do sth with exception, either log or something.  
                return categoryTypeSelectListItems;
            }
        }



    }
}