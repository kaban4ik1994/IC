using System.Linq;
using System.Web.Mvc;
using IC.Services.Interfaces;
using IC.UI.Filters.AuthorizationFilters;
using IC.UI.Models;
using Repository.Pattern.UnitOfWork;

namespace IC.UI.Controllers
{
    [CheckRole("Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public UserController(IUserService userService, IUnitOfWorkAsync unitOfWork)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View(_userService.Query().Select(user => new UserViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                UserId = user.UserId,
                IsAdmin = user.UserRoles.Where(role => role.Role.Name == "Admin").Count() != 0,
                IsUser = user.UserRoles.Where(role => role.Role.Name == "User").Count() != 0,
            }).ToList());
        }
    }
}