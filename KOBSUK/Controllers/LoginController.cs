using KOBSUK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KOBSUK.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {

            Session.RemoveAll();
            var Username = Session["Username"];
            if (Username == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Index(LoginClass model)
        {
            try
            {
                // TODO: Add insert logic here
                if (model.Username == "admin" && model.Password == "1234")
                {
                    Session["Username"] = model.Username.ToString();
                    Session["password"] = model.Password.ToString();
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.errorMsg = "รหัสผ่านไม่ถูกต้อง";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
