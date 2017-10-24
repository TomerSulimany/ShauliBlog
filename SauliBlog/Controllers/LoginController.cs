using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShauliBlog.Models;

namespace ShauliBlog.Controllers
{
    public class LoginController : Controller
    {
        private BlogDbContext db = new BlogDbContext();

        public ActionResult Login()
        {
            if ((Session["logged_in"] != null) && (Session["logged_in"].Equals(true)))
            {
                return RedirectToAction("Index", "Blog");
            }
            else
            {
                ViewBag.WrongDetails = false;

                return View("~/Views/Login/Login.cshtml");
            }
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            ViewBag.WrongDetails = false;
            
            // Todo: for now name is treated as username and surname as password. fix! 
            List<Fan> fans = db.Fans.ToList().Where(f => f.Name.Equals(username) && f.Surname.Equals(password)).ToList();

            if (fans.Count != 1)
            {
                ViewBag.WrongDetails = true;
                return View("~/Views/Login/Login.cshtml");
            }
            else
            {
                // admin
                if (fans[0].ID == 1)
                {
                    Session["admin"] = true;
                }

                Session["logged_in"] = true;
                Session["id"] = fans[0].ID;
                Session["name"] = fans[0].Name;
                return RedirectToAction("Index", "Blog");
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["logged_in"] = false;
            Session["admin"] = false;
            return RedirectToAction("Login", "Login");
        }
    }
}
