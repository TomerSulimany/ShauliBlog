using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SauliBlog.Models
{
    public class Post
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string AuthorWebsite { get; set; }
        public DateTime PublishDate { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public List<Comment> Comments { get; set; }

        public Post()
        {
            Comments = new List<Comment>();
        }
    }
}