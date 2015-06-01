using System.Web.Mvc;
using IC.UI.Filters.AuthorizationFilters;

namespace IC.UI.Controllers
{

    public class StudentController : Controller
    {
        [CheckRole("Admin,User")]
        public ActionResult Index()
        {
            return View();
        }
    }
}