using System.Web.Mvc;
using IC.UI.Filters.AuthorizationFilters;

namespace IC.UI.Controllers
{
    [CheckRole("Admin,User")]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}