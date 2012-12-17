using System.Web.Mvc;

namespace JavaProgrammingContest.Web.Controllers{
    /// <summary>
    ///     Controller for the homepage.
    /// </summary>
    public class HomeController : Controller{
        /// <summary>
        ///     Creates view for the indexpage.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(){
            return View();
        }

        /// <summary>
        ///     Creates view for the about page.
        /// </summary>
        /// <returns></returns>
        public ActionResult About(){
            return View();
        }

        /// <summary>
        ///     Creates view for the contacts page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact(){
            return View();
        }
    }
}