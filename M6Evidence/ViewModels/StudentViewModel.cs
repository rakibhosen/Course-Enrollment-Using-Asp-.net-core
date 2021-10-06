using M6Evidence.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace M6Evidence.ViewModels
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }
        [Required]
        public int CourseId { get; set; }

        [Required, StringLength(50), Display(Name = "Student Name")]
        public string StudentName { get; set; }

        [Required, Display(Name = "Enrollment Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Student Image")]
        public IFormFile Picture { get; set; }
        public virtual Course Course { get; set; }
    }
}

