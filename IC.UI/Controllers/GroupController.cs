using System;
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
    public class GroupController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ISpecialtyService _specialtyService;
        private readonly IGroupService _groupService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public GroupController(ICourseService courseService, ISpecialtyService specialtyService, IGroupService groupService, IUnitOfWorkAsync unitOfWork)
        {
            _courseService = courseService;
            _specialtyService = specialtyService;
            _groupService = groupService;
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View(_groupService.Query().Select(group => new GroupViewModel
            {
                Id = group.GroupId,
                CourseNumber = group.Specialty.Course.CourseNumber,
                GroupNumber = group.GroupNumber,
                SpecialtyName = group.Specialty.Name
            }).ToList());
        }

        public ActionResult Create()
        {
            var specialties = _specialtyService.Queryable().ToList();

            return View("Edit", new GroupEditViewModel
            {
                Specialties = new SelectList(specialties, "SpecialtyId", "Name"),
                SpecialtyId = specialties.First().SpecialtyId,
            });
        }

        public ActionResult Delete(long id)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Error");
            _groupService.Delete(id);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Error");
            var entity = _groupService.Find(id);
            if (entity == null) return RedirectToAction("Index", "Error");
            var specialties = _specialtyService.Queryable().ToList();
            var model = new GroupEditViewModel
            {
                Specialties = new SelectList(specialties, "SpecialtyId", "Name"),
                GroupId = entity.GroupId,
                SpecialtyId = entity.SpecialtyId,
                GroupNumber = entity.GroupNumber.ToString()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GroupEditViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Error");
            var entity = new Group
            {
                SpecialtyId = model.SpecialtyId,
                GroupId = model.GroupId,
                GroupNumber = Convert.ToInt32(model.GroupNumber),
            };

            if (entity.GroupId != 0)
                _groupService.Update(entity);
            else _groupService.Insert(entity);
            _unitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}