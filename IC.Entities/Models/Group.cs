using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace IC.Entities.Models
{
    public partial class Group : Entity
    {
        public long GroupId { get; set; }
        public long SpecialtyId { get; set; }
        public long GroupNumber { get; set; }

        public Specialty Specialty { get; set; }
        public ICollection<Student> Students { get; set; } 
    }
}
