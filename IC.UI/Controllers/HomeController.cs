using System.Linq;
using System.Web.Mvc;
using IC.Services.Interfaces;
using IC.UI.Filters.AuthorizationFilters;
using IC.UI.Helpers;
using IC.UI.Models;

namespace IC.UI.Controllers
{
    [CheckRole("Admin,User")]
    public class HomeController : Controller
    {
        private readonly IComputerService _computerService;

        public HomeController(IComputerService computerService)
        {
            _computerService = computerService;
        }

        public ActionResult Index()
        {
            return View(_computerService.GetAllComputers().Select(computer => new ComputerViewModel
            {
                Id = computer.ComputerId,
                Name = computer.Name,
                Room = computer.RoomNumber,
                IpAddress = IpAddressHelper.ConvertIpAddressToString(computer.IpAddress),
                NetworkAddress = IpAddressHelper.ConvertIpAddressToString(computer.NetworkAddress),
                SubnetAddress = IpAddressHelper.ConvertIpAddressToString(computer.SubnetAddress)
            }).ToList());
        }
    }
}