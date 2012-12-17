﻿using System;
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
    public class AssignmentController : Controller
    {
        private readonly IDbContext db;

        public AssignmentController(IDbContext context)
        {
            db = context;
        }

        //
        // GET: /Assignment/

        public ActionResult Index()
        {
            return View(db.Assignments.ToList());
        }

        //
        // GET: /Assignment/Details/5

        public ActionResult Details(int id = 0)
        {
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        //
        // GET: /Assignment/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Assignment/Create

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

        //
        // GET: /Assignment/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        //
        // POST: /Assignment/Edit/5

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

        //
        // GET: /Assignment/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        //
        // POST: /Assignment/Delete/5

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