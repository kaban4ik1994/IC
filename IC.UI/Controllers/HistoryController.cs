using System.Linq;
using System.Web.Mvc;
using IC.Services.Interfaces;
using IC.UI.Filters.AuthorizationFilters;
using Repository.Pattern.UnitOfWork;

namespace IC.UI.Controllers
{
    [CheckRole("Admin")]
    public class HistoryController : Controller
    {
        private readonly IHistoryService _historyService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public HistoryController(IHistoryService historyService, IUnitOfWorkAsync unitOfWork)
        {
            _historyService = historyService;
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View(_historyService.Query().Select().ToList());
        }

        public ActionResult Delete(long id)
        {
            _historyService.Delete(id);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}