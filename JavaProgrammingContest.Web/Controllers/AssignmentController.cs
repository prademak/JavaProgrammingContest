using System.Collections.Generic;
using System.Web.Mvc;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;

namespace JavaProgrammingContest.Web.Controllers
{
    public class AssignmentController : Controller
    {
         private readonly IDbContext _context;

         public AssignmentController(IDbContext context)
         {
            _context = context;
           }
        //
        // GET: /Assignment/

        public ActionResult Index()
        {
            var ass = new Assignment
            {
                Id = 1,
                CodeGiven =
                    "// Sample class\nclass HelloWorldApp {\n\tpublic static void main(String[] args){\n\t\tSystem.out.println(\"Hello World!\");\n\t}\n}\n",
                Description =
                    "Nullam ac venenatis arcu. Curabitur vitae malesuada sapien. Nam cursus, odio eget mollis rutrum, arcu ipsum pharetra diam, quis rhoncus magna sem non augue. Sed mollis rutrum dui, sed consequat ipsum congue eu. In luctus, orci id semper vehicula, neque lectus tristique lectus, eu interdum risus dolor non erat. Nullam ipsum eros, dignissim ac cursus non, ultrices vitae leo. Pellentesque mollis nisi ut orci euismod ac gravida magna aliquet. Aenean mi urna, fermentum ac lobortis condimentum, tincidunt in leo.",
                Title = "This is a sample assignment",
                MaxSolveTime = 15000
            }; 
            var ass2 = new Assignment
            {
                Id = 2,
                CodeGiven =
                    "// Sample class\nclass HelloWorldApp {\n\tpublic static void main(String[] args){\n\t\tSystem.out.println(\"Hello World!\");\n\t}\n}\n",
                Description =
                    "Nullam ac venenatis arcu. Curabitur vitae malesuada sapien. Nam cursus, odio eget mollis rutrum, arcu ipsum pharetra diam, quis rhoncus magna sem non augue. Sed mollis rutrum dui, sed consequat ipsum congue eu. In luctus, orci id semper vehicula, neque lectus tristique lectus, eu interdum risus dolor non erat. Nullam ipsum eros, dignissim ac cursus non, ultrices vitae leo. Pellentesque mollis nisi ut orci euismod ac gravida magna aliquet. Aenean mi urna, fermentum ac lobortis condimentum, tincidunt in leo.",
                Title = "This is another great sample assignment",
                MaxSolveTime = 15000
            };
            var items = new List<Assignment>();
            items.Add(ass);
            items.Add(ass2);
            items.Add(ass);
            items.Add(ass2);
            items.Add(ass);
            items.Add(ass2); 
            ViewBag.Items = items;
            return View();
        }

        //
        // GET: /Assignment/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Assignment/Add

        public ActionResult Add()
        {
            ViewBag.Title = "Add Assignment";
            return View();
        }

        //
        // POST: /Account/Add

        [HttpPost]
        public ActionResult Add(Assignment model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                _context.Assignments.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // If we got this far, something failed, redisplay form
              return View(model);
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
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Assignment/Edit/5

        public ActionResult Edit(int id)
        {
           // var assignment = _context.Assignments.Find(id);

            ViewBag.am = new Assignment
            {
                Id = 2,
                CodeGiven = "",
                Description = "Quis rhoncus magna sem non augue. Sed mollis rutrum dui, sed consequat ipsum congue eu. In luctus, orci id semper vehicula, neque lectus tristique lectus, eu interdum risus dolor non erat. Nullam ipsum eros, dignissim ac cursus non, ultrices vitae leo. Pellentesque mollis nisi ut orci euismod ac gravida magna aliquet. Aenean mi urna, fermentum ac lobortis condimentum, tincidunt in leo.",
                Title = "Another awesome assignment",
                MaxSolveTime = 15000
            };
            return View();
        }

        //
        // POST: /Assignment/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Assignment/Delete/5

        public ActionResult Delete(int id)
        { 
                var assignment = _context.Assignments.Find(id);
              
                   
                _context.Assignments.Remove(assignment);
             
            return View();
        }

        //
        // POST: /Assignment/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
