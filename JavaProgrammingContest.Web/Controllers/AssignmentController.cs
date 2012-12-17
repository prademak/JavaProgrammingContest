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
    ///     Assignment Administration Controller
    /// </summary>
    public class AssignmentController : Controller
    {
        /// <summary>
        ///     Database Context
        /// </summary>
        private readonly IDbContext db;

        /// <summary>
        ///     Controller Constructor
        /// </summary>
        /// <param name="context"></param>
        public AssignmentController(IDbContext context)
        {
            db = context;
        }

        /// <summary>
        ///     GET: /Assignment/
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(db.Assignments.ToList());
        }

        /// <summary>
        ///     GET: /Assignment/Details/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id = 0)
        {
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        /// <summary>
        ///     GET: /Assignment/Create
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        ///     POST: /Assignment/Create
        /// </summary>
        /// <param name="assignment"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                db.Assignments.Add(assignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(assignment);
        }

        /// <summary>
        ///     GET: /Assignment/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        /// <summary>
        ///     POST: /Assignment/Edit/5
        /// </summary>
        /// <param name="assignment"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Assignment assignment)
        {
            if (ModelState.IsValid)
            {
              
                Assignment assignmentdel = db.Assignments.Find(assignment.Id);
             
                db.Assignments.Remove(assignmentdel);
                 db.Assignments.Add(assignment); 
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assignment);
        }

        /// <summary>
        ///     GET: /Assignment/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
        {
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        /// <summary>
        ///     POST: /Assignment/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Assignment assignment = db.Assignments.Find(id);
            db.Assignments.Remove(assignment);
            db.SaveChanges();
            return RedirectToAction("Index");
        } 
    }
}