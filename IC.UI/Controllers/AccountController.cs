using System.Collections.ObjectModel;
using System.Web.Mvc;
using IC.Entities.Models;
using IC.Helpers;
using IC.Services.Interfaces;
using IC.UI.Helpers;
using IC.UI.Models;
using Repository.Pattern.Infrastructure;

namespace IC.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Error");
            var user = _userService.GetUserByEmailAndPassword(model.Email, model.Password);
            if (user == null)
                return RedirectToAction("Index", "Error");
            AuthHelper.LogInUser(HttpContext, user.Email);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOff()
        {
            AuthHelper.LogOffUser(HttpContext);
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Error");

            var user = _userService.CreateUser(new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                PasswordHash = PasswordHashHelper.GetHash(model.Password),
                UserRoles = new Collection<UserRole> { new UserRole { ObjectState = ObjectState.Added, RoleId = 2 } },
                SentMessages = new Collection<Message>(),
                ReceivedMessages = new Collection<Message>(),
                ObjectState = ObjectState.Added
            });
            AuthHelper.LogInUser(HttpContext, user.Email);

            return RedirectToAction("Index", "Home");
        }
    }
}