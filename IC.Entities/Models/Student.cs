using Repository.Pattern.Ef6;

namespace IC.Entities.Models
{
    public partial class Student : Entity
    {
        public long StudentId { get; set; }
        public long GroupId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int YearOfEntrance { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public Group Group { get; set; }
    }
}
