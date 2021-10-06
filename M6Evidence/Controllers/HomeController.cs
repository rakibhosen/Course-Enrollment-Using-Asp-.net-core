using M6Evidence.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace M6Evidence.Controllers
{
    public class HomeController : Controller
    {
        readonly StudentDbContext db = null;
        public HomeController(StudentDbContext db) { this.db = db; }

        public IActionResult Index()
        {
            ViewBag.StudentCount = db.Students.Count();
            ViewBag.CourseCount = db.Courses.Count();
            return View();
        }
    }
}
