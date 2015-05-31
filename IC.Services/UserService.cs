using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using IC.Entities.Models;
using IC.Helpers;
using IC.Services.Interfaces;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;

namespace IC.Services
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IUnitOfWorkAsync _unitOfWork;

        public UserService(IRepositoryAsync<User> repository, IUnitOfWorkAsync unitOfWork)
            : base(repository)
        {
            _unitOfWork = unitOfWork;
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            var passwordHash = PasswordHashHelper.GetHash(password);
            return Query(x =>
                x.Email.ToLower() == email.ToLower()
                && x.PasswordHash == passwordHash)
                .Include(user1 => user1.UserRoles)
                .Include(user1 => user1.UserRoles.Select(role => role.Role))
                .Select().FirstOrDefault();
        }

        public bool IsAdministrator(string email)
        {
            var user = Query(x => x.Email == email)
                 .Include(user1 => user1.UserRoles)
                 .Include(user1 => user1.UserRoles.Select(role => role.Role))
                .Select().FirstOrDefault();
            return user != null && user.UserRoles.Any(x => x.Role.Name == "Admin");
        }

        public User GetUserByEmail(string email)
        {
            return Query(x => x.Email == email)
                 .Select().FirstOrDefault();
        }

        public User CreateUser(User user)
        {
            Insert(user);
            _unitOfWork.SaveChanges();
            return user;
        }

        public bool CheckUserRole(string email, List<string> roles)
        {
            var user = Query(x => x.Email == email)
                .Include(user1 => user1.UserRoles)
                .Include(user1 => user1.UserRoles.Select(role => role.Role))
                .Select();
            return user != null && user.Any(user1 => user1.UserRoles.Any(role => roles.Any(s => s == role.Role.Name)));
        }
    }
}
