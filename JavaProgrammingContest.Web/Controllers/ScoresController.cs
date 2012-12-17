using System.Web.Mvc;
using JavaProgrammingContest.DataAccess.Context;

namespace JavaProgrammingContest.Web.Controllers{
    /// <summary>
    ///     Scoreboard controller.
    /// </summary>
    public class ScoresController : Controller{
        /// <summary>
        ///     Database Context
        /// </summary>
        private readonly IDbContext _context;

        /// <summary>
        ///    Scores controller constructor
        /// </summary>
        /// <param name="context">Database Context to use for the Controller</param>
        public ScoresController(IDbContext context){
            _context = context;
        }

        /// <summary>
        ///     Index for this Controller
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(){
            return View(_context.Assignments);
        }
    }
}