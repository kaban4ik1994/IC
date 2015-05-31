using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace IC.Entities.Models
{
    public partial class User : Entity
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
