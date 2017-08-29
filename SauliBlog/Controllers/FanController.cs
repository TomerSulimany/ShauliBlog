using SauliBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SauliBlog.Controllers
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

                return RedirectToAction("Details");
            }
            catch
            {
                return View();
            }
        }

        // GET: First/Edit/5
        public ActionResult Edit(int id)
        {
            foreach (Fan emp in Fans)
            {
                if (emp.Name.Equals(id))
                {
                    return View(emp);
                }
            }
            return View("Error");
        }

        // POST: First/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Fan empT)
        {
            try
            {
                foreach (Fan emp in Fans)
                {
                    if (emp.ID.Equals(id))
                    {
                        emp.copy(empT);
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
        public ActionResult Delete(int id)
        {
            int i = 0;
            foreach (Fan emp in Fans)
            {
                if (emp.ID.Equals(id))
                {
                    Fans.RemoveAt(i);
                    return RedirectToAction("Details");
                }
                i++;
            }
            return RedirectToAction("Error");
        }
    }
}