﻿using System.Linq;
using System.Web.Mvc;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;

namespace JavaProgrammingContest.Web.Controllers{
    /// <summary>
    ///     Assignment Administration Controller
    /// </summary>
    [Authorize(Roles = "Administrator")]
    public class AssignmentController : Controller{
        /// <summary>
        ///     Database Context
        /// </summary>
        private readonly IDbContext _db;

        /// <summary>
        ///     Controller Constructor
        /// </summary>
        /// <param name="context"></param>
        public AssignmentController(IDbContext context){
            _db = context;
        }

        /// <summary>
        ///     GET: /Assignment/
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(){
            return View(_db.Assignments.ToList());
        }

        /// <summary>
        ///     GET: /Assignment/Details/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id = 0){
            Assignment assignment = _db.Assignments.Find(id);
            if (assignment == null)
                return HttpNotFound();
            return View(assignment);
        }

        /// <summary>
        ///     GET: /Assignment/Create
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(){
            return View();
        }

        /// <summary>
        ///     POST: /Assignment/Create
        /// </summary>
        /// <param name="assignment"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Assignment assignment){
            if (ModelState.IsValid){
                _db.Assignments.Add(assignment);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(assignment);
        }

        /// <summary>
        ///     GET: /Assignment/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id = 0){
            Assignment assignment = _db.Assignments.Find(id);
            if (assignment == null)
                return HttpNotFound();
            return View(assignment);
        }

        /// <summary>
        ///     POST: /Assignment/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="assignment"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id, Assignment assignment){
            if (ModelState.IsValid){
                var assignmentOld = _db.Assignments.Find(id);

                if (TryUpdateModel(assignmentOld)){
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("Internal Error", "Unexpected error while saving to the database.");
            }
            return View(assignment);
        }

        /// <summary>
        ///     GET: /Assignment/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id = 0){
            Assignment assignment = _db.Assignments.Find(id);
            if (assignment == null)
                return HttpNotFound();
            return View(assignment);
        }

        /// <summary>
        ///     POST: /Assignment/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id){
            Assignment assignment = _db.Assignments.Find(id);
            _db.Assignments.Remove(assignment);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}