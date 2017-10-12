using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SauliBlog.Models;
using ShauliBlog.Models;
using System.ServiceModel.Syndication;
using System.Xml;

namespace SauliBlog.Controllers
{
    public class TwittersController : Controller
    {
        private BlogDbContext db = new BlogDbContext();

        // GET: Twitters
        public ActionResult Index()
        {
            return View(db.Twitters.ToList());
        }

        // GET: Twitters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Twitter twitter = db.Twitters.Find(id);
            if (twitter == null)
            {
                return HttpNotFound();
            }
            return View(twitter);
        }

        // GET: Twitters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Twitters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID")] Twitter twitter)
        {
            if (ModelState.IsValid)
            {
                db.Twitters.Add(twitter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(twitter);
        }

        // GET: Twitters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Twitter twitter = db.Twitters.Find(id);
            if (twitter == null)
            {
                return HttpNotFound();
            }
            return View(twitter);
        }

        // POST: Twitters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID")] Twitter twitter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(twitter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(twitter);
        }

        // GET: Twitters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Twitter twitter = db.Twitters.Find(id);
            if (twitter == null)
            {
                return HttpNotFound();
            }
            return View(twitter);
        }

        // POST: Twitters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Twitter twitter = db.Twitters.Find(id);
            db.Twitters.Remove(twitter);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// News RSS
        /// </summary>
        public static SyndicationFeed GetRSS()
        {
            using (XmlReader reader = XmlReader.Create("http://twitrss.me/twitter_search_to_rss/?term=shauli"))
            {
                SyndicationFeed rssData = SyndicationFeed.Load(reader);

                return rssData;
            }
        }
    }
}
