using System.ComponentModel.DataAnnotations;

namespace IC.UI.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Fill in the field.")]
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Fill in the field.")]
        [StringLength(20, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Fill in the field.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Fill in the field.")]
        [Display(Name = "Second Name")]
        public string SecondName { get; set; }
    }
}