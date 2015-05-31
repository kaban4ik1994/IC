using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IC.Entities.Models;
using Service.Pattern;

namespace IC.UI.Filters.AuthorizationFilters
{
    public class CheckRoleAttribute : AuthorizeAttribute
    {
        private readonly List<string> _roles;

        public IService<User> UserService { get; set; }

        public CheckRoleAttribute(string roles)
        {
            UserService = DependencyResolver.Current.GetService<IService<User>>();
            _roles = roles.Split(',').ToList();
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authCooke = httpContext.Request.Cookies["__AUTH"];
            if (authCooke == null) return false;
            return UserService.Query(x => x.Email == authCooke.Value)
                .Include(user1 => user1.UserRoles)
                .Include(user1 => user1.UserRoles.Select(role => role.Role))
                .Select()
                .Any(user1 => user1.UserRoles.Any(role => _roles.Any(s => s == role.Role.Name)));
        }
    }
}