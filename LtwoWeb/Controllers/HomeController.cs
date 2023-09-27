using LtwoWeb.Data;
using LtwoWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace LtwoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
  

        public IActionResult Index()
        {
            var departments = _context.Departments.ToList();
            var trainingtypes = new List<TrainingType>();

            //departments.Add(new Department()
            //{
            //    Id = 0,
            //    DepartmentName = "------"
            //});
            //trainingtypes.Add(new TrainingType()
            //{
            //    Id = 0,
            //    TrainingTypeName = "------"
            //});

            ViewBag.Departments = new SelectList(departments, "Id", "DepartmentName");
            ViewBag.TrainingTypes = new SelectList(trainingtypes, "Id", "TrainingTypeName");
            return View();
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

        public JsonResult GetTrainingTypeByDepartmentId(int departmentId)
        {
            return Json(_context.TrainingTypes.Where(u => u.DepartmentId == departmentId).ToList());
        }
    }
}