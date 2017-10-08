using ShauliBlog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace ShauliBlog.Models
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext()
            : base("SauliBlog")
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Fan> Fans { get; set; }
    }
    /*
    public class MyDbConfiguration : DbMigrationsConfiguration<BlogDbContext>
    {
        public MyDbConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }
    }*/
}