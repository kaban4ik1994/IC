using System.Collections.Generic;
using IC.Entities.Models;

namespace IC.UI.Models
{
    public class StudentViewModel
    {
        public IEnumerable<int> Courses { get; set; }
        public IEnumerable<string> Specialties { get; set; }
        public IEnumerable<int> Groups { get; set; }
        public IEnumerable<int> YearOfEntrances { get; set; }
        public List<Student> Students { get; set; }
    }
}