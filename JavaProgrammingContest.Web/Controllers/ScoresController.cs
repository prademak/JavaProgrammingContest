using System.Web.Mvc;
using JavaProgrammingContest.DataAccess.Context;

namespace JavaProgrammingContest.Web.Controllers{
    public class ScoresController : Controller{
        private readonly IDbContext _context;

        public ScoresController(IDbContext context){
            _context = context;
        }

        public ActionResult Index(){
            return View(_context.Assignments);
        }
    }
}