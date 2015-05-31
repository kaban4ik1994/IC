using System.Web.Mvc;
using IC.Services.Interfaces;
using IC.UI.Helpers;
using IC.UI.Models;

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
    }
}