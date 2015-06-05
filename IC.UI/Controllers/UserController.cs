using System.Linq;
using System.Web.Mvc;
using IC.Entities.Models;
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
        private readonly IUserRoleService _userRoleService;

        public UserController(IUserService userService, IUnitOfWorkAsync unitOfWork, IUserRoleService userRoleService)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
            _userRoleService = userRoleService;
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

        public ActionResult Edit(long id)
        {
            var model = _userService.Query(user => user.UserId == id).Select(user => new UserViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                UserId = user.UserId,
                IsAdmin = user.UserRoles.Where(role => role.Role.Name == "Admin").Count() != 0,
                IsUser = user.UserRoles.Where(role => role.Role.Name == "User").Count() != 0,
            }).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel model)
        {
            var entity = _userService.Find(model.UserId);
            if (entity == null) return RedirectToAction("Index", "Error");

            var userRoles = _userRoleService.Query(role => role.UserId == model.UserId).Select().ToList();

            if (model.IsAdmin &&
                !userRoles.Any(role => role.RoleId == 1))
            {
                _userRoleService.Insert(new UserRole
                {
                    UserId = model.UserId,
                    RoleId = 1
                });
            }

            if (!model.IsAdmin &&
                userRoles.Any(role => role.RoleId == 1))
            {
                var temp = userRoles.Where(role => role.RoleId == 1);
                foreach (var t in temp)
                {
                    _userRoleService.Delete(t.UserRoleId);
                }
            }

            if (model.IsUser &&
                !userRoles.Any(role => role.RoleId == 2))
            {
                _userRoleService.Insert(new UserRole
                {
                    UserId = model.UserId,
                    RoleId = 2
                });
            }

            if (!model.IsUser &&
                userRoles.Any(role => role.RoleId == 2))
            {
                var temp = userRoles.Where(role => role.RoleId == 2);
                foreach (var t in temp)
                {
                    _userRoleService.Delete(t.UserRoleId);
                }
            }
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}