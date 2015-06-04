using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using IC.Entities.Models;

namespace IC.UI.Models
{
    public class StudentViewModel
    {
        [Display(Name = "Course")]
        public SelectList Courses { get; set; }
        [Display(Name = "Specialty")]
        public SelectList Specialties { get; set; }
        [Display(Name = "Group")]
        public SelectList Groups { get; set; }
        public long GroupId { get; set; }
        [Display(Name = "YearOfEntrance")]
        public SelectList YearOfEntrances { get; set; }
        public int YearOfEntrance { get; set; }
        public List<StudentRowViewModel> Students { get; set; }
    }
}