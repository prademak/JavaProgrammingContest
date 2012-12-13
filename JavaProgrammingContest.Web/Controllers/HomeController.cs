﻿using System.Web.Mvc;

namespace JavaProgrammingContest.Web.Controllers{
    public class HomeController : Controller{
        public ActionResult Index(){
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            return View();
        }

        public ActionResult About(){
            ViewBag.Message = "Your app description page.";
            return View();
        }

        public ActionResult Contact(){
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Asignments(){
            ViewBag.Message = "Your asignments page.";
            return View();
        }

        public ActionResult Leaderboard(){
            ViewBag.Message = "Your Leaderboard page.";
            return View();
        }
    }
}