using System.Collections.Generic;
using System.Web.Mvc;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;

namespace JavaProgrammingContest.Web.Controllers{
    [Authorize(Roles = "Administrator")]
    public class AssignmentController : Controller{
        private readonly IDbContext _context;

        public AssignmentController(IDbContext context){
            _context = context;
        }

        //
        // GET: /Assignment/

        public ActionResult Index(){
            var items = new List<Assignment>();
            var it = _context.Assignments;

            ViewBag.Items = it;

            return View();
        }

        //
        // GET: /Assignment/Details/5

        public ActionResult Details(int id){
            return View();
        }

        //
        // GET: /Assignment/Add

        public ActionResult Add(){
            ViewBag.Title = "Add Assignment";
            return View();
        }

        //
        // POST: /Account/Add

        [HttpPost]
        public ActionResult Add(Assignment model, string returnUrl){
            if (ModelState.IsValid){
                _context.Assignments.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Assignment/Create

        public ActionResult Create(){
            return View();
        }

        //
        // GET: /Assignment/Edit/5

        public ActionResult Edit(int id){
            ViewBag.am = _context.Assignments.Find(id);
            return View();
        }

        //
        // POST: /Assignment/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection){
            try{
                // TODO: Add update logic here

                return RedirectToAction("Index");
            } catch{
                return View();
            }
        }

        //
        // GET: /Assignment/Delete/5

        public ActionResult Delete(int id){
            var assignment = _context.Assignments.Find(id);


            _context.Assignments.Remove(assignment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // POST: /Assignment/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection){
            try{
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            } catch{
                return View();
            }
        }
    }
}