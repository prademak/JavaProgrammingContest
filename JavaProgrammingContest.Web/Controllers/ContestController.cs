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
    public class ContestController : Controller
    {
      
        private readonly IDbContext db;

        public ContestController(IDbContext context)
        {
            db = context;
        }
        //
        // GET: /Contest/

        public ActionResult Index()
        {
            return View(db.Contests.ToList());
        }

        //
        // GET: /Contest/Details/5

        public ActionResult Details(int id = 0)
        {
            Contest contest = db.Contests.Find(id);
            if (contest == null)
            {
                return HttpNotFound();
            }
            return View(contest);
        }

        //
        // GET: /Contest/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Contest/Create

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

        //
        // GET: /Contest/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Contest contest = db.Contests.Find(id);
            if (contest == null)
            {
                return HttpNotFound();
            }
            return View(contest);
        }

        //
        // POST: /Contest/Edit/5

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

        //
        // GET: /Contest/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Contest contest = db.Contests.Find(id);
            if (contest == null)
            {
                return HttpNotFound();
            }
            return View(contest);
        }

        //
        // POST: /Contest/Delete/5

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