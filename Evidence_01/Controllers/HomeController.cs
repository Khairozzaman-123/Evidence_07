using Evidence_01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evidence_01.Controllers
{
    public class HomeController : Controller
    {
        ClubDbContext db = new ClubDbContext();

        public ActionResult Index()
        {
            return View(db.Sports.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}