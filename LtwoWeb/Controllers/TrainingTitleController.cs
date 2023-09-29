using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LtwoWeb.Data;

namespace LtwoWeb.Controllers
{
    public class TrainingTitleController : Controller
    {
        private readonly AppDbContext _context;

        public TrainingTitleController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TrainingTitle
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.TrainingTitles
        .Include(t => t.Category)
        .ThenInclude(c => c.TrainingType)
        .ThenInclude(tt => tt.Department);
            return View(await appDbContext.ToListAsync());
        }

        // GET: TrainingTitle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TrainingTitles == null)
            {
                return NotFound();
            }

            var trainingTitle = await _context.TrainingTitles
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.TrainingTitleId == id);
            if (trainingTitle == null)
            {
                return NotFound();
            }

            return View(trainingTitle);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _context.Departments.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrainingTitle trainingTitle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingTitle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Assuming you have an Index view
            }
            ViewBag.Departments = await _context.Departments.ToListAsync();
            return View(trainingTitle);
        }

        // GET: TrainingTitle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TrainingTitles == null)
            {
                return NotFound();
            }

            var trainingTitle = await _context.TrainingTitles.FindAsync(id);
            if (trainingTitle == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", trainingTitle.CategoryId);
            return View(trainingTitle);
        }

        // POST: TrainingTitle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrainingTitleId,Name,CategoryId")] TrainingTitle trainingTitle)
        {
            if (id != trainingTitle.TrainingTitleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingTitle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingTitleExists(trainingTitle.TrainingTitleId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", trainingTitle.CategoryId);
            return View(trainingTitle);
        }

        // GET: TrainingTitle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TrainingTitles == null)
            {
                return NotFound();
            }

            var trainingTitle = await _context.TrainingTitles
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.TrainingTitleId == id);
            if (trainingTitle == null)
            {
                return NotFound();
            }

            return View(trainingTitle);
        }

        // POST: TrainingTitle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TrainingTitles == null)
            {
                return Problem("Entity set 'AppDbContext.TrainingTitles'  is null.");
            }
            var trainingTitle = await _context.TrainingTitles.FindAsync(id);
            if (trainingTitle != null)
            {
                _context.TrainingTitles.Remove(trainingTitle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetTrainingTypesByDepartmentId(int id)
        {
            var trainingTypes = await _context.TrainingTypes
                .Where(tt => tt.DepartmentId == id)
                .Select(tt => new {
                    id = tt.TrainingTypeId,
                    name = tt.Name
                })
                .ToListAsync();

            return Json(trainingTypes);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoriesByTrainingTypeId(int id)
        {
            var categories = await _context.Categories
                .Where(tt => tt.TrainingTypeId == id)
                .Select(tt => new {
                    id = tt.CategoryId,
                    name = tt.Name
                })
                .ToListAsync();

            return Json(categories);
        }

        private bool TrainingTitleExists(int id)
        {
          return (_context.TrainingTitles?.Any(e => e.TrainingTitleId == id)).GetValueOrDefault();
        }
    }
}
