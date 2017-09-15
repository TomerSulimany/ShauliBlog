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
    public class BlogController : Controller
    {
        private BlogDbContext db = new BlogDbContext();

        // GET: Blog
        public ActionResult Index()
        {
            return View(db.Posts.ToList());
        }

        // POST: Blog/CommentAdd
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CommentAdd([Bind(Include = "ID,Title,Author,AuthorWebsite,WhenTaken,Content,PostID")] Comment comment)
        {
            Post post = db.Posts.Find(comment.PostID);

            if (post == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                comment.WhenTaken = DateTime.Now;

                db.Comments.Add(comment);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}