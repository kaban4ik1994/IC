using System.Collections.Generic;
using System.Collections.ObjectModel;
using Repository.Pattern.Ef6;

namespace IC.Entities.Models
{
    public partial class User : Entity
    {
        public User()
        {
            SentMessages = new Collection<Message>();
            ReceivedMessages = new Collection<Message>();
        }

        public long UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Message> ReceivedMessages { get; set; }
    }
}
