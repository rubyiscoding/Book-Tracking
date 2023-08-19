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
    public class CategoryTypeController : Controller
    {
        private readonly BookTrackerContext _context;

        public CategoryTypeController(BookTrackerContext context)
        {
            _context = context;
        }

        // GET: CategoryTypes 
        public async Task<IActionResult> Index()
        {
            var model = await (from categoryType in _context.CategoryTypes
                               select new AddEditCategoryTypeViewModel
                               {
                                   Id = categoryType.Id,
                                   Name = categoryType.Name
                               }).ToListAsync();
            return View(model);
        }

        // GET: CategoryType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var categoryType = await _context.CategoryTypes.FirstOrDefaultAsync(m => m.Id == id);
            if (categoryType == null)
                return NotFound();

            var model = new AddEditCategoryTypeViewModel { Name = categoryType.Name,
            Id= categoryType.Id
            };
            return View(model);
        }

        // GET: CategoryType/Create
        public IActionResult Create()
        {
            var model = new AddEditCategoryTypeViewModel();
            return View(model);
        }

        // POST: CategoryType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddEditCategoryTypeViewModel model)

        {
            var category = new CategoryType { Name = model.Name };

            _context.Add(category);
            var response = await _context.SaveChangesAsync();
            if (response != null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);

        }

        // GET: CategoryType/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var categoryType = await _context.CategoryTypes.FindAsync(id);
            if (categoryType == null)
                return NotFound();
            var model = new AddEditCategoryTypeViewModel
            {
                Id = categoryType.Id,
                Name = categoryType.Name
            };

            return View(model);
        }

        // POST: CategoryType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AddEditCategoryTypeViewModel model)
        {
            if (model.Id == null)
                return NotFound();

            var categoryType = await _context.CategoryTypes.FindAsync(model.Id);
            if (categoryType == null)
                return NotFound();


            try
            {
                categoryType.Name = model.Name;

                _context.Update(categoryType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return NotFound();
            }
        }

        // GET: CategoryType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var categoryType = await _context.CategoryTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (categoryType == null)
                return NotFound();

            var model = new AddEditCategoryTypeViewModel
            {
                Id = categoryType.Id,
                Name = categoryType.Name
            };

            return View(model);
        }

        // POST: CategoryType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryType = await _context.CategoryTypes.FindAsync(id);
            _context.CategoryTypes.Remove(categoryType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}