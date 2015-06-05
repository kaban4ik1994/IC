using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
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
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IHistoryService _historyService;

        public CourseController(ICourseService courseService, IUnitOfWorkAsync unitOfWork, IHistoryService historyService)
        {
            _courseService = courseService;
            _unitOfWork = unitOfWork;
            _historyService = historyService;
        }

        public ActionResult Index()
        {
            return View(_courseService.Queryable().Select(course => new CourseViewModel
            {
                Id = course.CourseId,
                Number = course.CourseNumber.ToString()
            }).ToList());
        }

        public ActionResult Delete(long id)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Error");
            _courseService.Delete(id);
            _historyService.Insert(new History
            {
                Email = AuthHelper.GetUser(HttpContext).Email,
                Action = Action.Delete,
                DateTime = DateTime.Now,
                Entity = EntityEnum.Course
            });
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View("Edit", new CourseViewModel());
        }

        public ActionResult Edit(long id)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Error");
            var entity = _courseService.Find(id);
            if (entity == null) return RedirectToAction("Index", "Error");
            var model = new CourseViewModel
            {
                Id = entity.CourseId,
                Number = entity.CourseNumber.ToString()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CourseViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Error");
            var entity = new Course
            {
                CourseId = model.Id,
                CourseNumber = Convert.ToInt32(model.Number)
            };

            if (entity.CourseId != 0)
            {
                _courseService.Update(entity);
                _historyService.Insert(new History
                {
                    Email = AuthHelper.GetUser(HttpContext).Email,
                    Action = Action.Update,
                    DateTime = DateTime.Now,
                    Entity = EntityEnum.Course
                });
            }
            else
            {
                _courseService.Insert(entity);
                _historyService.Insert(new History
                {
                    Email = AuthHelper.GetUser(HttpContext).Email,
                    Action = Action.Create,
                    DateTime = DateTime.Now,
                    Entity = EntityEnum.Course
                });
            }
            _unitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}