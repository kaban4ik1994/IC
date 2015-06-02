using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IC.UI.Models
{
    public class GroupEditViewModel
    {
        [Display(Name = "Specialty Name")]
        public SelectList Specialties { get; set; }

        [HiddenInput]
        public long GroupId { get; set; }

        [HiddenInput]
        public long SpecialtyId { get; set; }

        [Required(ErrorMessage = "Fill in the field.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Invalid Group Number")]
        [Display(Name = "Group Number")]
        public string GroupNumber { get; set; }
    }
}