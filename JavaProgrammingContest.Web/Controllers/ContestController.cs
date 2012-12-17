using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities; 

namespace JavaProgrammingContest.Web.Controllers
{
    /// <summary>
    ///     Contest Controller
    /// </summary>
    public class ContestController : Controller
    {
        /// <summary>
        ///     Database Context.
        /// </summary>
        private readonly IDbContext db;

        /// <summary>
        ///     Contest Controller Constructor.
        /// </summary>
        /// <param name="context">Dtabase Context to use.</param>
        public ContestController(IDbContext context)
        {
            db = context;
        }
        
        /// <summary>
        ///     /Contest/
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(db.Contests.ToList());
        }

        /// <summary>
        ///     GET: /Contest/Details/5
        /// </summary>
        /// <param name="id">Contest id</param>
        /// <returns></returns>
        public ActionResult Details(int id = 0)
        {
            Contest contest = db.Contests.Find(id);
            if (contest == null)
            {
                return HttpNotFound();
            }
            return View(contest);
        }

        /// <summary>
        ///     GET: /Contest/Create
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        ///     POST: /Contest/Create
        /// </summary>
        /// <param name="contest"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Contest contest)
        {
            if (ModelState.IsValid)
            {
                db.Contests.Add(contest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contest);
        }

        /// <summary>
        ///     GET: /Contest/Edit/5
        /// </summary>
        /// <param name="id">Contest id</param>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            Contest contest = db.Contests.Find(id);
            if (contest == null)
            {
                return HttpNotFound();
            }
            return View(contest);
        }

        /// <summary>
        ///     POST: /Contest/Edit/5
        /// </summary>
        /// <param name="contest">Contest db entry.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Contest contest)
        {
            if (ModelState.IsValid)
            {
                Contest contestdel = db.Contests.Find(contest.Id);
                db.Contests.Remove(contestdel);
                db.Contests.Add(contest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contest);
        }

        /// <summary>
        ///     GET: /Contest/Delete/5
        /// </summary>
        /// <param name="id">Contest identifier</param>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
        {
            Contest contest = db.Contests.Find(id);
            if (contest == null)
            {
                return HttpNotFound();
            }
            return View(contest);
        }

        /// <summary>
        ///     GET: /Contest/Delete/5
        /// </summary>
        /// <param name="id">Contest identifier.</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Contest contest = db.Contests.Find(id);
            db.Contests.Remove(contest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
         
    }
}