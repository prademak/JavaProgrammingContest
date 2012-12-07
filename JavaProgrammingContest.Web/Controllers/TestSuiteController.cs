using System.Web.Mvc;

namespace JavaProgrammingContest.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class TestSuiteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}