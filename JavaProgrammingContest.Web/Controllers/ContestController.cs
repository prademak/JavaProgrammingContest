using System.Linq;
using System.Web.Mvc;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;

namespace JavaProgrammingContest.Web.Controllers{
    /// <summary>
    ///     Contest Controller
    /// </summary>
    [Authorize(Roles = "Administrator")]
    public class ContestController : Controller{
        /// <summary>
        ///     Database Context.
        /// </summary>
        private readonly IDbContext _db;

        /// <summary>
        ///     Contest Controller Constructor.
        /// </summary>
        /// <param name="context">Dtabase Context to use.</param>
        public ContestController(IDbContext context){
            _db = context;
        }

        /// <summary>
        ///     /Contest/
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(){
            return View(_db.Contests.ToList());
        }

        /// <summary>
        ///     GET: /Contest/Details/5
        /// </summary>
        /// <param name="id">Contest id</param>
        /// <returns></returns>
        public ActionResult Details(int id = 0){
            Contest contest = _db.Contests.Find(id);
            if (contest == null)
                return HttpNotFound();
            return View(contest);
        }

        /// <summary>
        ///     GET: /Contest/Create
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(){
            return View();
        }

        /// <summary>
        ///     POST: /Contest/Create
        /// </summary>
        /// <param name="contest"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Contest contest){
            if (ModelState.IsValid){
                _db.Contests.Add(contest);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contest);
        }

        /// <summary>
        ///     GET: /Contest/Edit/5
        /// </summary>
        /// <param name="id">Contest id</param>
        /// <returns></returns>
        public ActionResult Edit(int id = 0){
            Contest contest = _db.Contests.Find(id);
            if (contest == null)
                return HttpNotFound();
            return View(contest);
        }

        /// <summary>
        ///     POST: /Contest/Edit/5
        /// </summary>
        /// <param name="contest">Contest db entry.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Contest contest){
            if (ModelState.IsValid){
                Contest contestdel = _db.Contests.Find(contest.Id);
                _db.Contests.Remove(contestdel);
                _db.Contests.Add(contest);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contest);
        }

        /// <summary>
        ///     GET: /Contest/Delete/5
        /// </summary>
        /// <param name="id">Contest identifier</param>
        /// <returns></returns>
        public ActionResult Delete(int id = 0){
            Contest contest = _db.Contests.Find(id);
            if (contest == null)
                return HttpNotFound();
            return View(contest);
        }

        /// <summary>
        ///     GET: /Contest/Delete/5
        /// </summary>
        /// <param name="id">Contest identifier.</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id){
            Contest contest = _db.Contests.Find(id);
            _db.Contests.Remove(contest);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}