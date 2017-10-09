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
        public ActionResult Login()
        {
            if ((Session["logged_in"] != null) &&
                (Session["logged_in"].Equals(true)))
            {
                return RedirectToAction("Index", "Posts");
            }
            else
            {
                ViewBag.HadNoPermission = false;
                ViewBag.WrongDetails = false;

                return View("~/Views/Login/Login.cshtml");
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["logged_in"] = false;
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            ViewBag.HadNoPermission = false;
            ViewBag.WrongDetails = false;

            if (username == "admin" && password == "admin")
            {
                Session["logged_in"] = true;

                // TODO: Redirect to the caller instead to index
                return RedirectToAction("Index", "Posts");
            }
            else
            {
                ViewBag.WrongDetails = true;
                return View("~/Views/Login/Login.cshtml");
            }
        }

        public class AuthoriseSiteAccessAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                base.OnActionExecuting(filterContext);


                if ((filterContext.HttpContext.Session["logged_in"] == null) ||
                    (filterContext.HttpContext.Session["logged_in"].Equals(false)))
                    filterContext.Result = new SiteAccessDeniedResult();
            }

        }

        public class SiteAccessDeniedResult : ViewResult
        {
            public SiteAccessDeniedResult()
            {
                ViewName = "~/Views/Login/Login.cshtml";
                ViewBag.HadNoPermission = true;
                ViewBag.WrongDetails = false;
            }
        }
    }
}
