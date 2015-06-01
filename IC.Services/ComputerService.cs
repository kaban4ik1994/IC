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
            var result = Queryable().ToList();
            return result;
        }

        public void RemoveComputerById(long id)
        {
            Delete(id);
            _unitOfWork.SaveChanges();
        }

        public Computer GetComputerById(long id)
        {
            var result = Find(id);
            return result;
        }

        public void UpdateComputer(Computer computer)
        {
            Update(computer);
            _unitOfWork.SaveChanges();
        }

        public long CreateComputer(Computer computer)
        {
            Insert(computer);
            _unitOfWork.SaveChanges();
            return computer.ComputerId;
        }
    }
}
