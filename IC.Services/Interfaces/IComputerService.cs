using System.Collections.Generic;
using IC.Entities.Models;
using Service.Pattern;

namespace IC.Services.Interfaces
{
    public interface IComputerService : IService<Computer>
    {
        IList<Computer> GetAllComputers();
        void RemoveComputerById(long id);
        Computer GetComputerById(long id);
        void UpdateComputer(Computer computer);
        long CreateComputer(Computer computer);
    }
}
