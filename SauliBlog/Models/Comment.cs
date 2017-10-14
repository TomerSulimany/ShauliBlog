using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShauliBlog.Models
{
    public class Comment
    {
        public int ID { get; set; }

        [DisplayName("Title"), Required, StringLength(100)]
        public string Title { get; set; }

        [DisplayName("Author"), StringLength(30)]
        public string Author { get; set; }

        [DisplayName("Website"), StringLength(30), Url]
        public string AuthorWebsite { get; set; }

        [DisplayName("Comment Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime WhenTaken { get; set; }

        [DisplayName("Text"), Required]
        public string Content { get; set; }

        public int PostID { get; set; }

        [ForeignKey("PostID")]
        public virtual Post Post { get; set; }
    }
}