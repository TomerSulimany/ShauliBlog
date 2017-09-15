using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SauliBlog.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public int PostID { get; set; }
        public Post MyPost { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string AuthorWebsite { get; set; }
        public string Content { get; set; }
    }
}