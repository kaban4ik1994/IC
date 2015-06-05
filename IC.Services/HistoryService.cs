using IC.Entities.Models;
using IC.Services.Interfaces;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace IC.Services
{
    public class HistoryService : Service<History>, IHistoryService
    {
        public HistoryService(IRepositoryAsync<History> repository)
            : base(repository)
        {
        }
    }
}
