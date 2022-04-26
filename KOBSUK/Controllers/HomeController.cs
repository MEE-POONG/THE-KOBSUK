using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KOBSUK.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var Username = Session["Username"];

            return Username == null ? RedirectToAction("Index", "Login") : (ActionResult)View();
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