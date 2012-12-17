using System.Web.Mvc;

namespace JavaProgrammingContest.Web.Controllers{
    /// <summary>
    ///     Testsuite Controller for launching the Javascript.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    public class TestSuiteController : Controller{
        /// <summary>
        ///     Index for this controller.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(){
            return View();
        }
    }
}