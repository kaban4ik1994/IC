using System.Collections.Generic;
using IC.Entities.Models;
using Service.Pattern;

namespace IC.Services.Interfaces
{
    public interface IUserService : IService<User>
    {
        User GetUserByEmailAndPassword(string email, string password);
        bool IsAdministrator(string email);
        User GetUserByEmail(string email);
        User CreateUser(User user);
        bool CheckUserRole(string email, List<string> roles);
    }
}
