using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using IC.Entities.Models;
using IC.Services.Interfaces;
using Service.Pattern;

namespace IC.UI.Filters.AuthorizationFilters
{
    public class CheckRoleAttribute : AuthorizeAttribute
    {
        private readonly List<string> _roles;

        public IUserService UserService { get; set; }

        public CheckRoleAttribute(string roles)
        {
            UserService = DependencyResolver.Current.GetService<IUserService>();
            _roles = roles.Split(',').ToList();
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authCookie = httpContext.Request.Cookies["__AUTH"];
            if (authCookie == null) return false;
            return UserService.CheckUserRole(authCookie.Value, _roles);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
            {
                {"action", "Login"},
                {"controller", "Account"}
            });
        }
    }
}