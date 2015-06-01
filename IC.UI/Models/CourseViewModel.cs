using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IC.UI.Models
{
    public class CourseViewModel
    {
        [HiddenInput]
        public long Id { get; set; }

        [Required(ErrorMessage = "Fill in the field.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Invalid Course Number")]
        [Display(Name = "Course Number")]
        public string Number { get; set; }
    }
}