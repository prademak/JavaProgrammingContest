using System.Web.Mvc;

namespace JavaProgrammingContest.Web.Controllers{
    /// <summary>
    ///     Controller for the Editor.
    /// </summary>
    [Authorize]
    public class EditorController : Controller{
        /// <summary>
        ///     Creates the view for the editor.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(){
            return View();
        }
    }
}