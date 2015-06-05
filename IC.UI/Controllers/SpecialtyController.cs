using System;
using System.Linq;
using System.Web.Mvc;
using IC.Entities.Models;
using IC.Services.Interfaces;
using IC.UI.Filters.AuthorizationFilters;
using IC.UI.Helpers;
using IC.UI.Models;
using Repository.Pattern.UnitOfWork;
using Action = IC.Entities.Models.Action;

namespace IC.UI.Controllers
{
    [CheckRole("Admin")]
    public class SpecialtyController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ISpecialtyService _specialtyService;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IHistoryService _historyService;

        public SpecialtyController(ICourseService courseService, IUnitOfWorkAsync unitOfWork, ISpecialtyService specialtyService, IHistoryService historyService)
        {
            _courseService = courseService;
            _unitOfWork = unitOfWork;
            _specialtyService = specialtyService;
            _historyService = historyService;
        }

        public ActionResult Index()
        {
            return View(_specialtyService.Query().Select(specialty => new SpecialtyViewModel
            {
                Id = specialty.SpecialtyId,
                CourseNumber = specialty.Course.CourseNumber,
                Name = specialty.Name
            }).ToList());
        }

        public ActionResult Create()
        {
            var courses = _courseService.Queryable().ToList();
            return View("Edit", new SpecialtyEditViewModel
            {
                Courses = new SelectList(courses, "CourseId", "CourseNumber"),
                CourseId = courses.First().CourseId
            });
        }

        public ActionResult Delete(long id)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Error");
            _specialtyService.Delete(id);
            _historyService.Insert(new History
            {
                Email = AuthHelper.GetUser(HttpContext).Email,
                Action = Action.Delete,
                DateTime = DateTime.Now,
                Entity = EntityEnum.Speciality
            });
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Error");
            var entity = _specialtyService.Find(id);
            if (entity == null) return RedirectToAction("Index", "Error");
            var courses = _courseService.Queryable().ToList();
            var model = new SpecialtyEditViewModel
            {
                Courses = new SelectList(courses, "CourseId", "CourseNumber"),
                CourseId = entity.CourseId,
                SpecialtyId = entity.SpecialtyId,
                SpecialtyName = entity.Name
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SpecialtyEditViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Error");
            var entity = new Specialty
            {
                SpecialtyId = model.SpecialtyId,
                CourseId = model.CourseId,
                Name = model.SpecialtyName
            };

            if (entity.SpecialtyId != 0)
            {
                _specialtyService.Update(entity);
                _historyService.Insert(new History
                {
                    Email = AuthHelper.GetUser(HttpContext).Email,
                    Action = Action.Update,
                    DateTime = DateTime.Now,
                    Entity = EntityEnum.Speciality
                });
            }
            else
            {
                _specialtyService.Insert(entity);
                _historyService.Insert(new History
                {
                    Email = AuthHelper.GetUser(HttpContext).Email,
                    Action = Action.Create,
                    DateTime = DateTime.Now,
                    Entity = EntityEnum.Speciality
                });
            }
            _unitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}