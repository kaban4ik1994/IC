using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IC.UI.Models
{
    public class SpecialtyEditViewModel
    {
        [Display(Name = "Course Number")]
        public SelectList Courses { get; set; }
        
        [HiddenInput]
        public long CourseId { get; set; }
        
        [HiddenInput]
        public long SpecialtyId { get; set; }

        [Required(ErrorMessage = "Fill in the field.")]
        [Display(Name = "Name")]
        public string SpecialtyName { get; set; }
    }
}