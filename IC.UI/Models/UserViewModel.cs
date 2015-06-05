namespace IC.UI.Models
{
    public class UserViewModel
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsUser { get; set; }
    }
}