using System.ComponentModel.DataAnnotations;

namespace IC.UI.Models
{
    public class UserViewModel
    {
        public long UserId { get; set; }
        [Display(Name = "Email:")]
        public string Email { get; set; }
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }
        [Display(Name = "Second Name:")]
        public string SecondName { get; set; }
        [Display(Name = "Admin:")]
        public bool IsAdmin { get; set; }
        [Display(Name = "User:")]
        public bool IsUser { get; set; }
    }
}