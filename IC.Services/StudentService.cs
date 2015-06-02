using IC.Entities.Models;
using IC.Services.Interfaces;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace IC.Services
{
    public class StudentService : Service<Student>, IStudentService
    {
        public StudentService(IRepositoryAsync<Student> repository) : base(repository)
        {
        }
    }
}
