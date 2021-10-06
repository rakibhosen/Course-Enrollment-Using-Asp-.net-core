using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace M6Evidence.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        [Required, StringLength(40), Display(Name = "Course Title")]
        public string CourseTitle { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }

    public class Student
    {
        public int StudentId { get; set; }
        [Required, StringLength(40), Display(Name = "Student Name")]
        public string StudentName { get; set; }
        public string Picture { get; set; }
        public DateTime EnrollmentDate { get; set; }
        [Required, ForeignKey("CourseId")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }

    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) { }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
