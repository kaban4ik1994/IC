using IC.Entities.Models;
using IC.Services.Interfaces;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace IC.Services
{
    public class SpecialtyService : Service<Specialty>, ISpecialtyService
    {
        public SpecialtyService(IRepositoryAsync<Specialty> repository)
            : base(repository)
        {
        }
    }
}
