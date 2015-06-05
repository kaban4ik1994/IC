using IC.Entities.Models;
using IC.Services.Interfaces;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace IC.Services
{
    public class UserRoleService : Service<UserRole>, IUserRoleService
    {
        public UserRoleService(IRepositoryAsync<UserRole> repository) : base(repository)
        {
        }
    }
}
