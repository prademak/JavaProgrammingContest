using System.Web.Mvc;

namespace JavaProgrammingContest.Web.Controllers
{
    [Authorize]
    public class EditorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}