using IC.Entities.Models;
using IC.Services.Interfaces;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace IC.Services
{
    public class CourseService : Service<Course>, ICourseService
    {
        public CourseService(IRepositoryAsync<Course> repository)
            : base(repository)
        {
        }
    }
}
