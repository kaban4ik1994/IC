using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using IC.Entities.Models;
using IC.Helpers;
using IC.RandomInformation.RandomGenerators;
using IC.Services.Interfaces;
using IC.UI.Actions;
using IC.UI.Filters.AuthorizationFilters;
using IC.UI.Helpers;
using IC.UI.Models;
using Repository.Pattern.UnitOfWork;
using Action = IC.Entities.Models.Action;

namespace IC.UI.Controllers
{

    public class StudentController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ISpecialtyService _specialtyService;
        private readonly IGroupService _groupService;
        private readonly IStudentService _studentService;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IHistoryService _historyService;

        public StudentController(ICourseService courseService, ISpecialtyService specialtyService, IGroupService groupService, IStudentService studentService, IUnitOfWorkAsync unitOfWork, IHistoryService historyService)
        {
            _courseService = courseService;
            _specialtyService = specialtyService;
            _groupService = groupService;
            _studentService = studentService;
            _unitOfWork = unitOfWork;
            _historyService = historyService;
        }

        [CheckRole("Admin,User")]
        public ActionResult Index(long? courseId, long? specialtyId, long? groupId, int? yearOfEntrance)
        {

            var courses = _courseService.Queryable().ToList();

            if (courseId == null)
            {
                var firstCourse = courses.FirstOrDefault();
                courseId = firstCourse == null ? -1 : firstCourse.CourseId;
            }

            var specialties = _specialtyService
                .Query(specialty => specialty.CourseId == (long)courseId)
                .Select()
                .ToList();

            if (specialtyId == null)
            {
                var firstSpecialty = specialties.FirstOrDefault();
                specialtyId = firstSpecialty == null ? -1 : firstSpecialty.SpecialtyId;
            }
            var groups = _groupService.Query(group => group.SpecialtyId == (long)specialtyId
                && group.Specialty.CourseId == (long)courseId)
                .Select()
                .ToList();

            if (groupId == null)
            {
                var firstGroup = groups.FirstOrDefault();
                groupId = firstGroup == null ? -1 : firstGroup.GroupId;
            }

            var yearOfEntrances = new List<int>();
            var temp = _studentService
                .Query(student => student.GroupId == (long)groupId)
                .Select(student => student.YearOfEntrance).ToList();
            foreach (var value in temp.Where(value => !yearOfEntrances.Contains(value)))
            {
                yearOfEntrances.Add(value);
            }

            yearOfEntrances = yearOfEntrances.OrderBy(i => i).ToList();
            if (yearOfEntrance == null)
            {
                var firstYearOfEntrance = yearOfEntrances.FirstOrDefault();
                yearOfEntrance = firstYearOfEntrance;
            }

            var model = new StudentViewModel
            {
                Courses = new SelectList(courses, "CourseId", "CourseNumber", courseId),
                Specialties = new SelectList(specialties, "SpecialtyId", "Name", specialtyId),
                Groups = new SelectList(groups, "GroupId", "GroupNumber", groupId),
                YearOfEntrances = new SelectList(yearOfEntrances),
                YearOfEntrance = (int)yearOfEntrance,
                GroupId = (long)groupId,
                Students = _studentService.Query(student => student.GroupId == groupId && student.YearOfEntrance == yearOfEntrance)
                    .Select(student => new StudentRowViewModel
                    {
                        CourseNumber = student.Group.Specialty.Course.CourseNumber,
                        GroupNumber = student.Group.GroupNumber,
                        SpecialtyName = student.Group.Specialty.Name,
                        FirstName = student.FirstName,
                        YearOfEntrance = student.YearOfEntrance,
                        LastName = student.LastName,
                        MiddleName = student.MiddleName,
                        StudentId = student.StudentId,
                        Login = student.Login,
                        Password = student.Password
                    }).ToList()
            };

            var gv = new GridView { DataSource = model.Students };
            gv.DataBind();
            Session["Students"] = gv;

            return View(model);
        }

        [CheckRole("Admin")]
        public ActionResult Download()
        {
            if (Session["Students"] != null)
            {
                return new DownloadFileActionResult((GridView)Session["Students"], "Students.xls");
            }
            return RedirectToAction("Index", "Error");
        }

        public ActionResult DropDowns(long? courseId, long? specialtyId, long? groupId)
        {
            var courses = _courseService.Queryable().ToList();

            if (courseId == null)
            {
                var firstCourse = courses.FirstOrDefault();
                courseId = firstCourse == null ? -1 : firstCourse.CourseId;
            }

            var specialties = _specialtyService
                .Query(specialty => specialty.CourseId == (long)courseId)
                .Select()
                .ToList();

            if (specialtyId == null)
            {
                var firstSpecialty = specialties.FirstOrDefault();
                specialtyId = firstSpecialty == null ? -1 : firstSpecialty.SpecialtyId;
            }
            var groups = _groupService.Query(group => group.SpecialtyId == (long)specialtyId
                && group.Specialty.CourseId == (long)courseId)
                .Select()
                .ToList();

            if (groupId == null)
            {
                var firstGroup = groups.FirstOrDefault();
                groupId = firstGroup == null ? -1 : firstGroup.GroupId;
            }

            return View("_DropdownsPartial", new StudentEditViewModel
            {
                Groups = new SelectList(groups, "GroupId", "GroupNumber"),
                Courses = new SelectList(courses, "CourseId", "CourseNumber"),
                Specialties = new SelectList(specialties, "SpecialtyId", "Name"),
                GroupId = (long)groupId,
                CourseId = (long)courseId,
                SpecialtyId = (long)specialtyId
            });
        }

        [CheckRole("Admin")]
        public ActionResult Create()
        {
            var courses = _courseService.Queryable().ToList();

            var firstCourse = courses.FirstOrDefault();
            var courseId = firstCourse == null ? -1 : firstCourse.CourseId;

            var specialties = _specialtyService
                .Query(specialty => specialty.CourseId == (long)courseId)
                .Select()
                .ToList();

            var firstSpecialty = specialties.FirstOrDefault();
            var specialtyId = firstSpecialty == null ? -1 : firstSpecialty.SpecialtyId;

            var groups = _groupService.Query(group => group.SpecialtyId == (long)specialtyId
                && group.Specialty.CourseId == (long)courseId)
                .Select()
                .ToList();

            var firstGroup = groups.FirstOrDefault();
            var groupId = firstGroup == null ? -1 : firstGroup.GroupId;

            return View("Edit", new StudentEditViewModel
            {
                Groups = new SelectList(groups, "GroupId", "GroupNumber"),
                Courses = new SelectList(courses, "CourseId", "CourseNumber"),
                Specialties = new SelectList(specialties, "SpecialtyId", "Name"),
                GroupId = groupId,
                CourseId = courseId,
                SpecialtyId = specialtyId
            });
        }

        [CheckRole("Admin")]
        public ActionResult Delete(long id)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Error");
            _studentService.Delete(id);
            _historyService.Insert(new History
            {
                Email = AuthHelper.GetUser(HttpContext).Email,
                Action = Action.Delete,
                DateTime = DateTime.Now,
                Entity = EntityEnum.Student
            });
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }

        [CheckRole("Admin")]
        public ActionResult Edit(long id)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Error");
            var entity = _studentService.Find(id);
            if (entity == null) return RedirectToAction("Index", "Error");
            var groups = _groupService.Queryable().ToList();
            var courses = _courseService.Queryable().ToList();
            var specialties = _specialtyService.Queryable().ToList();
            var model = new StudentEditViewModel
            {
                Groups = new SelectList(groups, "GroupId", "GroupNumber"),
                Courses = new SelectList(courses, "CourseId", "CourseNumber"),
                Specialties = new SelectList(specialties, "SpecialtyId", "Name"),
                CourseId = entity.Group.Specialty.CourseId,
                SpecialtyId = entity.Group.SpecialtyId,
                GroupId = entity.GroupId,
                FirstName = entity.FirstName,
                MiddleName = entity.MiddleName,
                LastName = entity.LastName,
                Login = entity.Login,
                Password = entity.Password,
                StudentId = entity.StudentId,
                YearOfEntrance = entity.YearOfEntrance.ToString()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentEditViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Error");
            var entity = new Student
            {
                GroupId = model.GroupId,
                StudentId = model.StudentId,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                Login = GenerateLoginHelper.GenerateLogin(model.FirstName, model.MiddleName, model.LastName, Convert.ToInt32(model.YearOfEntrance)),
                Password = model.StudentId != 0
                ? model.Password :
                RandomGenerator.GetRandomPassword(10, 15),
                YearOfEntrance = Convert.ToInt32(model.YearOfEntrance),
            };

            if (entity.StudentId != 0)
            {
                _studentService.Update(entity);
                _historyService.Insert(new History
                {
                    Email = AuthHelper.GetUser(HttpContext).Email,
                    Action = Action.Update,
                    DateTime = DateTime.Now,
                    Entity = EntityEnum.Student
                });
            }
            else
            {
                _studentService.Insert(entity);
                _historyService.Insert(new History
                {
                    Email = AuthHelper.GetUser(HttpContext).Email,
                    Action = Action.Create,
                    DateTime = DateTime.Now,
                    Entity = EntityEnum.Student
                });
            }
            _unitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}