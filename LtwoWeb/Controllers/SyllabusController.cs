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
    public class SyllabusController : Controller
    {
        private readonly AppDbContext _context;

        public SyllabusController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Syllabus
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Syllabuses.Include(s => s.Analysis);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Syllabus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Syllabuses == null)
            {
                return NotFound();
            }

            var syllabus = await _context.Syllabuses
                .Include(s => s.Analysis)
                .FirstOrDefaultAsync(m => m.SyllabusId == id);
            if (syllabus == null)
            {
                return NotFound();
            }

            return View(syllabus);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _context.Departments.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Syllabus syllabus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(syllabus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Assuming you have an Index view
            }
            ViewBag.Departments = await _context.Departments.ToListAsync();
            return View(syllabus);
        }

        // GET: Syllabus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Syllabuses == null)
            {
                return NotFound();
            }

            var syllabus = await _context.Syllabuses.FindAsync(id);
            if (syllabus == null)
            {
                return NotFound();
            }
            ViewData["AnalysisId"] = new SelectList(_context.Analyses, "AnalysisId", "Description", syllabus.AnalysisId);
            return View(syllabus);
        }

        // POST: Syllabus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SyllabusId,Content,AnalysisId")] Syllabus syllabus)
        {
            if (id != syllabus.SyllabusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(syllabus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SyllabusExists(syllabus.SyllabusId))
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
            ViewData["AnalysisId"] = new SelectList(_context.Analyses, "AnalysisId", "Description", syllabus.AnalysisId);
            return View(syllabus);
        }

        // GET: Syllabus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Syllabuses == null)
            {
                return NotFound();
            }

            var syllabus = await _context.Syllabuses
                .Include(s => s.Analysis)
                .FirstOrDefaultAsync(m => m.SyllabusId == id);
            if (syllabus == null)
            {
                return NotFound();
            }

            return View(syllabus);
        }

        // POST: Syllabus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Syllabuses == null)
            {
                return Problem("Entity set 'AppDbContext.Syllabuses'  is null.");
            }
            var syllabus = await _context.Syllabuses.FindAsync(id);
            if (syllabus != null)
            {
                _context.Syllabuses.Remove(syllabus);
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

        [HttpGet]
        public async Task<IActionResult> GetTrainingTitlesByCategoryId(int id)
        {
            var trainingTitles = await _context.TrainingTitles
                .Where(tt => tt.CategoryId == id)
                .Select(tt => new {
                    id = tt.TrainingTitleId,
                    name = tt.Name
                })
                .ToListAsync();

            return Json(trainingTitles);
        }

        [HttpGet]
        public async Task<IActionResult> GetAnalysesByTrainingTitleId(int id)
        {
            var analyses = await _context.Analyses
                .Where(tt => tt.TrainingTitleId == id)
                .Select(tt => new {
                    id = tt.AnalysisId,
                    name = tt.Description
                })
                .ToListAsync();

            return Json(analyses);
        }


        private bool SyllabusExists(int id)
        {
          return (_context.Syllabuses?.Any(e => e.SyllabusId == id)).GetValueOrDefault();
        }
    }
}
