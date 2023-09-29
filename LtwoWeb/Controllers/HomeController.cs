using LtwoWeb.Data;
using LtwoWeb.Models;
using LtwoWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LtwoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Departments = await _context.Departments.ToListAsync();
            return View(new CascadingDropdownsViewModel());
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
        public async Task<IActionResult> GetSyllabusByTrainingTitleId(int id)
        {
            var syllabus = await _context.TrainingTitles
                .Where(tt => tt.TrainingTitleId == id)
                .Select(tt => tt.Syllabus)
                .FirstOrDefaultAsync();

            return Json(syllabus);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}