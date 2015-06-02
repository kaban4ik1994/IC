using IC.Entities.Models;
using IC.Services.Interfaces;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace IC.Services
{
    public class GroupService : Service<Group>, IGroupService
    {
        public GroupService(IRepositoryAsync<Group> repository)
            : base(repository)
        {
        }
    }
}
