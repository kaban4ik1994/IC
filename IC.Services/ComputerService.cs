using System.Collections.Generic;
using System.Linq;
using IC.Entities.Models;
using IC.Services.Interfaces;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;

namespace IC.Services
{
    public class ComputerService : Service<Computer>, IComputerService
    {
        private readonly IUnitOfWorkAsync _unitOfWork;

        public ComputerService(IRepositoryAsync<Computer> repository, IUnitOfWorkAsync unitOfWork)
            : base(repository)
        {
            _unitOfWork = unitOfWork;
        }

        public IList<Computer> GetAllComputers()
        {
            return Query()
                .Select()
                .ToList();
        }
    }
}
