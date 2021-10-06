using M6Evidence.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M6Evidence.Controllers
{
    public class CourseController : Controller
    {
        readonly StudentDbContext db = null;
        public CourseController(StudentDbContext db) { this.db = db; }
        public IActionResult Index()
        {
            return View(db.Courses.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return PartialView("_ResultPartial", true);
            }
            return PartialView("_ResultPartial", false);
        }

        public IActionResult Edit(int id)
        {
            return View(db.Courses.First(x => x.CourseId == id));

        }
        [HttpPost]
        public IActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_ResultPartial", true);
            }
            return PartialView("_ResultPartial", false);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var course = new Course { CourseId = id };
            if (!db.Students.Any(x => x.CourseId == id))
            {
                db.Entry(course).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Can not delete.Course has related Student");
            return View(db.Courses.First(x => x.CourseId == id));
        }
    }
}
