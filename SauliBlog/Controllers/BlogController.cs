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
using System.ServiceModel.Syndication;
using System.Xml;

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

            Init();

            return View();
        }

        // POST: Blog/CommentAdd
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CommentAdd([Bind(Include = "ID,Title,AuthorWebsite,WhenTaken,Content,PostID")] Comment comment)
        {
            if ((Session["logged_in"] == null) || (Session["logged_in"].Equals(false)))
            {
                return RedirectToAction("Index", "Blog");
            }

            Post post = db.Posts.Find(comment.PostID);

            if (post == null)
            {
                return HttpNotFound();
            }

            comment.Author = (string)Session["name"];

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

        public class GenderPostObj
        {
            public int Id { get; set; }
            public Gender Gender { get; set; }
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
        public ActionResult SearchPost(DateTime? PublishDate, string Title, string Author, string submmit_button)
        {
            if (submmit_button == "Search by Comments")
            {
                return (SearchComment(PublishDate, Title, Author));
            }

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

        [HttpGet, ActionName("Statistics")]
        public ActionResult Statistics()
        {
            var q = db.Posts.GroupBy(p => new { Name = p.Author })
                .Select(p => new CountPostObj()
                {
                    Name = p.Key.Name,
                    Count = p.Count()
                });
            var t = db.Posts.GroupBy(p => new { Gender = p.Fan.Gender })
                .Select(p => new GenderPostObj()
                {
                    Gender = p.Key.Gender,
                    Count = p.Count()
                });
            ViewBag.CountPostByWriter = q.ToList();
            ViewBag.CountPostByGender = t.ToList();
            ViewBag.PostWithFans = JoinPostWithFans().ToList();


            return View("SimpleStatistics");
        }

        [HttpGet, ActionName("RecommendPost")]
        public ActionResult RecommendPost()
        {
            ViewBag.Comments = db.Comments.ToList();
            ViewBag.Posts = new List<PostWithFans>();

            List<string>  likedAuthers = new List<string>();

            string UserName = "";
            if (Session["logged_in"] != null)
            {
                UserName = Session["name"].ToString().ToLower();
            }
            else
            {
                ViewBag.Posts = JoinPostWithFans().ToList();
                ViewBag.Comments = db.Comments.ToList();

                Init();

                return View("Index");
            }

            var nonWriterPosts = JoinPostWithFans().Where(p => UserName.ToLower() != p.post.Author.ToLower());
            foreach (var obj1 in nonWriterPosts)
            {
                var post = obj1.post;
                foreach (var comm in post.Comments)
                {
                    if (comm.Author.ToLower() == UserName.ToLower())
                    {
                        if (!likedAuthers.Contains(post.Author.ToLower()))
                        {
                            likedAuthers.Add(post.Author.ToLower());
                        }
                    }
                }
            }

            bool flag = true;
            foreach (var obj1 in nonWriterPosts)
            {
                flag = true;
                var post = obj1.post;
                foreach (var f in likedAuthers)
                {
                    if (post.Author.ToLower() == f.ToLower())
                    {
                        foreach (var comm in post.Comments)
                        {
                            if (comm.Author.ToLower() == UserName.ToLower())
                            {
                                flag = false;
                            }
                        }
                        if (flag)
                        {
                            ViewBag.Posts.Add(obj1);
                        }
                    }
                }
            }
            

            return View("Index");
        }

        /// <summary>
        /// News RSS
        /// </summary>
        public static SyndicationFeed GetRSS()
        {
            using (XmlReader reader = XmlReader.Create("http://rss.walla.co.il/?w=/9/1092/0/@rss.e"))
            {
                SyndicationFeed rssData = SyndicationFeed.Load(reader);

                return rssData;
            }
        }

        [HttpPost, ActionName("SearchComment")]
        public ActionResult SearchComment(DateTime? WhenTaken, string Title, string Author)
        {
            ViewBag.Context = "Search comment date:" + WhenTaken + ", Title:" + Title + ", Author: " + Author;

            if (WhenTaken == null)
            {
                ViewBag.Posts = JoinPostWithFans().Where(p => (p.post.Comments.Where(c => c.Title.Contains(Title) &&
                                                                                          c.Author.Contains(Author)).ToList().Count() >= 1)).ToList();
            }
            else
            {
                ViewBag.Posts = JoinPostWithFans().Where(p => (p.post.Comments.Where(
                        c => c.Title.Contains(Title) &&
                        c.Author.Contains(Author) &&
                        EntityFunctions.TruncateTime(c.WhenTaken) == EntityFunctions.TruncateTime(WhenTaken)).ToList().Count() >= 1)).ToList();
            }

            ViewBag.Comments = db.Comments.ToList();

            return View("Index");
        }
        private void Init()
        {
            // Create uploads directory
            System.IO.Directory.CreateDirectory(Server.MapPath("~/Uploads/"));

            // Add admin
            var query = db.Fans.Where(fan => fan.Name == "admin" && fan.Surname == "admin");
            if (query.Count() == 0)
            {
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Fans', RESEED, 0)");
                var all = from c in db.Fans select c;
                db.Fans.RemoveRange(all);
                db.SaveChanges();

                Fan fan = new Fan()
                {
                    Name = "admin",
                    Surname = "admin",
                    Gender = Gender.Male,
                    Birthday = DateTime.Now,
                    ID = 1,
                    Seniority = 99
                };

                if (ModelState.IsValid)
                {
                    db.Fans.Add(fan);
                    db.SaveChanges();
                }
            }
        }
    }
}