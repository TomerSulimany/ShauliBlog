using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShauliBlog.Models;
using System.Data.Entity.Core.Objects;

namespace ShauliBlog.Controllers
{
    public class BlogController : Controller
    {
        private BlogDbContext db = new BlogDbContext();

        // GET: Blog
        public ActionResult Index()
        {
            ViewBag.Posts = JoinPostWithFans().ToList();
            ViewBag.Comments = db.Comments.ToList();

            return View();
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


        public class PostWithFans
        {
            public Post post { get; set; }
            public string FanName { get; set; }
        }

        public class CountPostObj
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Count { get; set; }
        }

        public IEnumerable<PostWithFans> JoinPostWithFans()
        {
            var q = db.Posts.Join(db.Fans,
                p => p.FanUser,
                f => f.ID,
                (p, f) => new PostWithFans()
                {
                    post = p,
                    FanName = f.Name + " " + f.Surname
                });

            return q;
        }


        [HttpPost, ActionName("SearchPost")]
        public ActionResult SearchPost(DateTime? PublishDate, string Title, string Author)
        {
            ViewBag.Context = "Search by publish date: " + PublishDate + ", Title: " + Title + ", Author: " + Author;
            if (PublishDate == null)
            {
                ViewBag.Posts = JoinPostWithFans().Where(p => (p.post.Title.Contains(Title)) &&
                p.post.Author.Contains(Author)).ToList();
            }
            else
            {
                ViewBag.Posts = JoinPostWithFans().Where(p => (p.post.Title.Contains(Title)) &&
                (p.post.Author.Contains(Author)) &&
                (EntityFunctions.TruncateTime(p.post.PublishDate) == EntityFunctions.TruncateTime(PublishDate))).ToList();
            }
            ViewBag.Comments = db.Comments.ToList();

            return View("Index");
        }

        // ============================== Statistics ==============================

        [HttpPost, ActionName("CountPostByWriter")]
        public ActionResult CountPostByWriter()
        {
            var q = db.Posts.GroupBy(p => new { Name = p.Author })
                .Select(p => new CountPostObj()
                {
                    Name = p.Key.Name,
                    Count = p.Count()
                });
            ViewBag.CountPostByWriter = q.ToList();
            ViewBag.Posts = JoinPostWithFans().ToList();

            return View("SimpleStatistics");
        }

        [HttpPost, ActionName("RecommendPost")]
        public ActionResult RecommendPost(string first_name, string last_name)
        {
            ViewBag.Context = "Recommened Posts For You";
            ViewBag.Comments = db.Comments.ToList();
            ViewBag.Posts = new List<PostWithFans>();

            var writerPosts = JoinPostWithFans().Where(p => p.post.Author == first_name + " " + last_name);
            var nonWriterPosts = JoinPostWithFans().Where(p => p.post.Author != first_name + " " + last_name);
            foreach (var obj1 in nonWriterPosts)
            {
                var post = obj1.post;
                foreach (var obj2 in writerPosts)
                {
                    var writerPost = obj2.post;
                    if (post.Content.ToLower().Contains(writerPost.Title.ToLower()))
                    {
                        ViewBag.Posts.Add(obj1);
                    }
                }
            }

            return View("Index");
        }

    }
}