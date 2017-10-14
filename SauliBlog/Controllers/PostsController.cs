using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShauliBlog.Models;
using System.IO;

namespace ShauliBlog.Controllers
{
    public class PostsController : Controller
    {
        private BlogDbContext db = new BlogDbContext();

        // GET: Posts
        public ActionResult Index()
        {
            if ((Session["admin"] == null) || (Session["admin"].Equals(false)))
            {
                return RedirectToAction("Index", "Blog");
            }

            return View(db.Posts.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if ((Session["admin"] == null) || (Session["admin"].Equals(false)))
            {
                return RedirectToAction("Index", "Blog");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            if ((Session["logged_in"] == null) || (Session["logged_in"].Equals(false)))
            {
                return RedirectToAction("Index", "Blog");
            }

            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,AuthorWebsite,PublishDate,Content,Location")] Post post,
                                    HttpPostedFileBase image, HttpPostedFileBase video,
                                    bool chk_image, bool chk_video)
        {
            if ((Session["logged_in"] == null) || (Session["logged_in"].Equals(false)))
            {
                return RedirectToAction("Index", "Blog");
            }

            if (ModelState.IsValid)
            {
                post.PublishDate = DateTime.Now;
                post.FanUser = (int)Session["id"];
                post.Author = (string)Session["name"];
                db.Posts.Add(post);
                db.SaveChanges();

                if (chk_image && image != null && image.ContentLength > 0)
                {
                    string fileName = post.ID + ".png";
                    string path = Server.MapPath("~/Uploads");
                    image.SaveAs(Path.Combine(path, fileName));
                }

                if (chk_video && video != null && video.ContentLength > 0)
                {
                    string fileName = post.ID + ".mp4";
                    string path = Server.MapPath("~/Uploads");
                    video.SaveAs(Path.Combine(path, fileName));
                }

                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
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
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Author,AuthorWebsite,PublishDate,Content,Location")] Post post,
                                  HttpPostedFileBase image, HttpPostedFileBase video,
                                  bool chk_image, bool chk_video)
        {
            if ((Session["admin"] == null) || (Session["admin"].Equals(false)))
            {
                return RedirectToAction("Index", "Blog");
            }

            if (ModelState.IsValid)
            {
                post.PublishDate = DateTime.Now;
                post.FanUser = (int)Session["id"];
                post.Author = (string)Session["name"];
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();

                // If checkbox checked and image provided - add the image.
                if (chk_image && image != null && image.ContentLength > 0)
                {
                    string fileName = post.ID + ".png";
                    string path = Server.MapPath("~/Uploads");
                    image.SaveAs(Path.Combine(path, fileName));
                }
                // If checkbox not checked - delete existing image.
                else if (!chk_image && System.IO.File.Exists(Server.MapPath("~/Uploads/") + post.ID + ".png"))
                {
                    // Delete the image
                    System.IO.File.Delete(Server.MapPath("~/Uploads/") + post.ID + ".png");
                }
                // Else (checkbox checked, image not provided) - leave the image as is.

                // If checkbox checked and video provided - add the video.
                if (chk_video && video != null && video.ContentLength > 0)
                {
                    string fileName = post.ID + ".mp4";
                    string path = Server.MapPath("~/Uploads");
                    video.SaveAs(Path.Combine(path, fileName));
                }
                // If checkbox not checked - delete existing video.
                else if (System.IO.File.Exists(Server.MapPath("~/Uploads/") + post.ID + ".mp4"))
                {
                    // Delete the video
                    System.IO.File.Delete(Server.MapPath("~/Uploads/") + post.ID + ".mp4");
                }
                // Else (checkbox checked, video not provided) - leave the video as is.

                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
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
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if ((Session["admin"] == null) || (Session["admin"].Equals(false)))
            {
                return RedirectToAction("Index", "Blog");
            }

            Post post = db.Posts.Find(id);

            // Delete the image
            if (System.IO.File.Exists(Server.MapPath("~/Uploads/") + id + ".png"))
            {
                System.IO.File.Delete(Server.MapPath("~/Uploads/") + id + ".png");
            }

            // Delete the video
            if (System.IO.File.Exists(Server.MapPath("~/Uploads/") + id + ".mp4"))
            {
                System.IO.File.Delete(Server.MapPath("~/Uploads/") + id + ".mp4");
            }

            db.Posts.Remove(post);
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
    }
}
