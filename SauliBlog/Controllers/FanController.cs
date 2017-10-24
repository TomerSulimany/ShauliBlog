using ShauliBlog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShauliBlog.Controllers
{
    public class FanController : Controller
    {
        private BlogDbContext db = new BlogDbContext();

        // GET: Fan
        public ActionResult Index()
        {
            return View(db.Fans.ToList());
        }

        // GET: Fan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                return HttpNotFound();
            }
            return View(fan);
        }

        // GET: First/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Surname,Gender,Birthday,Seniority")] Fan fan)
        {
            if (ModelState.IsValid)
            {
                db.Fans.Add(fan);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Fans/Edit/5
        public ActionResult Edit(int? id)
        {
            if ((Session["admin"] == null) || (Session["admin"].Equals(false)))
            {
                return RedirectToAction("Index", "Blog");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                return HttpNotFound();
            }
            return View(fan);
        }

        // POST: Fans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Surname,Gender,Birthday,Seniority")] Fan fan)
        {
            if ((Session["admin"] == null) || (Session["admin"].Equals(false)))
            {
                return RedirectToAction("Index", "Blog");
            }

            if (ModelState.IsValid)
            {
                db.Entry(fan).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(fan);
        }

        // GET: Fans/Delete/5
        public ActionResult Delete(int? id)
        {
            if ((Session["admin"] == null) || (Session["admin"].Equals(false)))
            {
                return RedirectToAction("Index", "Blog");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                return HttpNotFound();
            }
            return View(fan);
        }

        // POST: Fans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if ((Session["admin"] == null) || (Session["admin"].Equals(false)))
            {
                return RedirectToAction("Index", "Blog");
            }

            Fan fan = db.Fans.Find(id);

            db.Fans.Remove(fan);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult AutoCompleteFan(string txt)
        {
            List<Fan> matchFans = new List<Fan>();
            int i = 0;
            if (txt != null && txt != "")
            {
                foreach (Fan fan in db.Fans.ToList())
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