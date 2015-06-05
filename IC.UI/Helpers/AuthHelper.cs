using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IC.Entities.Models;
using IC.Services.Interfaces;
using Service.Pattern;

namespace IC.UI.Helpers
{
    public static class AuthHelper
    {

        public static void LogInUser(HttpContextBase httpContext, string cookies)
        {
            var cookie = new HttpCookie("__AUTH")
            {
                Value = cookies,
                Expires = DateTime.Now.AddYears(1)
            };

            httpContext.Response.Cookies.Add(cookie);
        }


        public static void LogOffUser(HttpContextBase httpContext)
        {
            if (httpContext.Request.Cookies["__AUTH"] != null)
            {
                var cookie = new HttpCookie("__AUTH")
                {
                    Expires = DateTime.Now.AddDays(-1),
                };

                httpContext.Response.Cookies.Add(cookie);
            }
        }

        public static long GetNewMessagesCount(long userId)
        {
            var messageService = DependencyResolver.Current.GetService<IMessageService>();
            return messageService.GetNewMessagesCountByUserId(userId);
        }

        public static User GetUser(HttpContextBase httpContext)
        {
            var authCookie = httpContext.Request.Cookies["__AUTH"];
            if (authCookie == null) return null;
            var userService = DependencyResolver.Current.GetService<IUserService>();
            return userService.GetUserByEmail(authCookie.Value);
        }

        public static bool IsAdministrator(HttpContextBase httpContext)
        {
            var authCookie = httpContext.Request.Cookies["__AUTH"];
            if (authCookie == null) return false;
            var userService = DependencyResolver.Current.GetService<IUserService>();
            return userService.IsAdministrator(authCookie.Value);
        }
    }
}