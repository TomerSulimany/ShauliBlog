using ShauliBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShauliBlog.Controllers
{
    public class FanController : Controller
    {
        static List<Fan> Fans = new List<Fan>();
        // GET: Fan
        public ActionResult Index()
        {
            if (Fans.Count == 0)
            {
                Fan f = new Fan { Birthday = new DateTime(1995, 12 ,12), Gender = Gender.Male, Name = "Tomer", Seniority = 1, Surname = "Sulimany" };
                Fans.Add(f);
            }
            return View(Fans);
        }

        // GET: First/Details
        public ActionResult Details()
        {
            return View(Fans);
        }

        // GET: First/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: First/Create
        [HttpPost]
        public ActionResult Create(Fan fan)
        {
            try
            {
                Fans.Add(fan);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: First/Edit/5
        public ActionResult Edit(int id)
        {
            foreach (Fan fan in Fans)
            {
                if (fan.ID.Equals(id))
                {
                    return View(fan);
                }
            }
            return View("Error");
        }

        // POST: First/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Fan fanT)
        {
            try
            {
                foreach (Fan fan in Fans)
                {
                    if (fan.ID.Equals(id))
                    {
                        fan.copy(fanT);
                        return RedirectToAction("Index");
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        // GET: First/Delete/5
        public ActionResult AutoCompleteFan(string txt)
        {
            List<Fan> matchFans = new List<Fan>();
            int i = 0;
            if (txt != null && txt != "")
            {
                foreach (Fan fan in Fans)
                {
                    if (fan.Name.StartsWith(txt))
                    {
                        matchFans.Add(fan);
                    }
                    i++;
                }
            }
            return View(matchFans);
        }
    }
}