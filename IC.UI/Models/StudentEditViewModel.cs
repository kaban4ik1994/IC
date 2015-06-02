using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IC.UI.Models
{
    public class StudentEditViewModel
    {
        [HiddenInput]
        public long StudentId { get; set; }
        public SelectList Groups { get; set; }
        
        [HiddenInput]
        public long GroupId { get; set; }

        [Required(ErrorMessage = "Fill in the field.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Fill in the field.")]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Fill in the field.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Fill in the field.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Invalid Group Number")]
        [Display(Name = "Year Of Entrance")]
        public string YearOfEntrance { get; set; }
        [HiddenInput]
        public string Login { get; set; }
        [HiddenInput]
        public string Password { get; set; }
    }
}