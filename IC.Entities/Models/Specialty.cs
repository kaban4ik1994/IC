using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace IC.Entities.Models
{
    public partial class Specialty : Entity
    {
        public long SpecialtyId { get; set; }
        public long CourseId { get; set; }
        public string Name { get; set; }

        public Course Course { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}
