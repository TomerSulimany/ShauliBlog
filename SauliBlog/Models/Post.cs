using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShauliBlog.Models
{
    public class Post
    {
        public int ID { get; set; }

        [DisplayName("Title"), Required, StringLength(100)]
        public string Title { get; set; }

        [DisplayName("Author"), Required, StringLength(30)]
        public string Author { get; set; }

        [DisplayName("Website"), StringLength(30), Url]
        public string AuthorWebsite { get; set; }

        [DisplayName("Post Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime PublishDate { get; set; }

        [DisplayName("Content"), Required]
        public string Content { get; set; }

        public int? FanUser { get; set; }

        [ForeignKey("FanUser")]
        public Fan Fan { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}