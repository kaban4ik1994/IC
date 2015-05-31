using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace IC.Entities.Models
{
    public partial class Course : Entity
    {
        public long CourseId { get; set; }
        public int CourseNumber { get; set; }

        public ICollection<Specialty> Specialties { get; set; }
    }
}
