using M6Evidence.Models;
using M6Evidence.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace M6Evidence.Controllers
{
    public class StudentController : Controller
    {
        readonly StudentDbContext db = null;
        readonly IWebHostEnvironment env;
        public StudentController(StudentDbContext db, IWebHostEnvironment env) { this.db = db; this.env = env; }
        public IActionResult Index()
        {

            return View(db.Students.Include(x => x.Course).ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Courses = db.Courses.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentViewModel s)
        {
            if (ModelState.IsValid)
            {
                var Student = new Student
                {
                    Picture = "demo.png",
                    StudentName = s.StudentName,
                    CourseId = s.CourseId,
                    EnrollmentDate = s.EnrollmentDate,

                };
                if (s.Picture != null && s.Picture.Length > 0)
                {

                    string dir = Path.Combine(env.WebRootPath, "Uploads");
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    string fileName = Guid.NewGuid() + Path.GetExtension(s.Picture.FileName);
                    string fullPath = Path.Combine(dir, fileName);
                    FileStream fs = new FileStream(fullPath, FileMode.Create);
                    s.Picture.CopyTo(fs);
                    fs.Flush();
                    Student.Picture = fileName;
                }
                db.Students.Add(Student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Courses = db.Courses.ToList();
            return View();
        }

        public ActionResult Edit(int id)
        {

            var data = db.Students.First(x => x.StudentId == id);

            ViewBag.Courses = db.Courses.ToList();
            ViewBag.CurrentPic = data.Picture;
            return View(new StudentViewModel
            {
                StudentId = data.StudentId,
                StudentName = data.StudentName,
                EnrollmentDate = data.EnrollmentDate,
                CourseId = data.CourseId

            });
        }
        [HttpPost]
        public IActionResult Edit(StudentViewModel s)
        {
            Student st = db.Students.First(x => x.StudentId == s.StudentId);

            if (ModelState.IsValid)
            {

                st.StudentName = s.StudentName;
                st.EnrollmentDate = s.EnrollmentDate;
                st.CourseId = s.CourseId;

                if (s.Picture != null && s.Picture.Length > 0)
                {
                    string dir = Path.Combine(env.WebRootPath, "Uploads");
                    string filename = Guid.NewGuid() + Path.GetExtension(s.Picture.FileName);
                    string filepath = Path.Combine(dir, filename);
                    FileStream fs = new FileStream(filepath, FileMode.Create);

                    s.Picture.CopyTo(fs);
                    fs.Flush();
                    fs.Close();

                    st.Picture = filename;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Courses = db.Courses.ToList();
            ViewBag.CurrentPic = s.Picture;
            return View(st);
        }

        public IActionResult Delete(int id)
        {
            var student = new Student { StudentId = id };
            db.Entry(student).State = EntityState.Deleted;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
