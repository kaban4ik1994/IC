using System;
using System.Linq;
using System.Web.Mvc;
using IC.Entities.Models;
using IC.Services.Interfaces;
using IC.UI.Filters.AuthorizationFilters;
using IC.UI.Helpers;
using IC.UI.Models;

namespace IC.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IComputerService _computerService;

        public HomeController(IComputerService computerService)
        {
            _computerService = computerService;
        }

        [CheckRole("Admin,User")]
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

        [CheckRole("Admin")]
        public ActionResult Delete(long id)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Error");
            _computerService.RemoveComputerById(id);
            return RedirectToAction("Index");
        }

        [CheckRole("Admin")]
        [HttpGet]
        public ActionResult Create()
        {
            return View("Edit", new ComputerEditViewModel());
        }


        [CheckRole("Admin")]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Error");
            var entity = _computerService.GetComputerById(id);
            if (entity == null) return RedirectToAction("Index", "Error");
            var model = new ComputerEditViewModel
            {
                Id = entity.ComputerId,
                RoomNumber = entity.RoomNumber.ToString(),
                IpAddress = IpAddressHelper.ConvertIpAddressToString(entity.IpAddress),
                NetworkAddress = IpAddressHelper.ConvertIpAddressToString(entity.NetworkAddress),
                SubnetAddress = IpAddressHelper.ConvertIpAddressToString(entity.SubnetAddress),
                Name = entity.Name
            };
            return View(model);
        }

        [CheckRole("Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ComputerEditViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Error");

            var entity = new Computer
            {
                ComputerId = model.Id,
                RoomNumber = Convert.ToInt32(model.RoomNumber),
                Name = model.Name,
                IpAddress = IpAddressHelper.ConvertStringToIpAddress(model.IpAddress),
                NetworkAddress = IpAddressHelper.ConvertStringToIpAddress(model.NetworkAddress),
                SubnetAddress = IpAddressHelper.ConvertStringToIpAddress(model.SubnetAddress)
            };
            if (entity.ComputerId != 0)
                _computerService.UpdateComputer(entity);
            else
                _computerService.CreateComputer(entity);

            return RedirectToAction("Index");
        }
    }
}