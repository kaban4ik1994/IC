using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IC.Services.Interfaces;
using IC.UI.Helpers;

namespace IC.UI.Controllers
{
    public class ValidationController : Controller
    {
        private readonly IUserService _userService;

        public ValidationController(IUserService userService)
        {
            _userService = userService;
        }

        public JsonResult CorrectEmailAddress(string to, string from)
        {
            var result = _userService.Query(x => x.Email == to).Select().Count() != 0;
            if (to == from) result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckIpAddress(string ipAddress, string networkAddress, string networkMask)
        {
            var networkAddressBytes = IpAddressHelper.ConvertStringToIpAddress(networkAddress);
            var maskAddress = IpAddressHelper.ConvertStringToIpAddress(networkMask);
            var currentAddress = IpAddressHelper.ConvertStringToIpAddress(ipAddress);

            var startIpBytes = new byte[networkAddressBytes.Length];
            var endIpBytes = new byte[networkAddressBytes.Length];

            for (var i = 0; i < networkAddressBytes.Length; i++)
            {
                startIpBytes[i] = (byte)(networkAddressBytes[i] & maskAddress[i]);
                endIpBytes[i] = (byte)(networkAddressBytes[i] | ~maskAddress[i]);
            }

            var result = true;
            for (var i = 0; i < networkAddressBytes.Length; i++)
            {
                if (!(currentAddress[i] >= startIpBytes[i] && currentAddress[i] <= endIpBytes[i]))
                {
                    result = false;
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}