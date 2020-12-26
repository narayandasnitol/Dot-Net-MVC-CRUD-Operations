using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using MVC_CRUD.Models;


namespace MVC_CRUD.Controllers
{
    public class HomeController : Controller
    {
        private Database1Entities2 db = new Database1Entities2();
        // GET: Home
        public ActionResult Index()
        {
            if (Session["id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {              

                var data = db.admins.Where(s => s.username.Equals(username) && s.password.Equals(password)).ToList();
                if (data.Count() > 0)
                {
                    return RedirectToAction("../Employee/Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
    

    }
}