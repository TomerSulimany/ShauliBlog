namespace SauliBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Author = c.String(nullable: false, maxLength: 30),
                        AuthorWebsite = c.String(maxLength: 30),
                        WhenTaken = c.DateTime(nullable: false),
                        Content = c.String(nullable: false),
                        PostID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: true)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Author = c.String(nullable: false, maxLength: 30),
                        AuthorWebsite = c.String(maxLength: 30),
                        PublishDate = c.DateTime(nullable: false),
                        Content = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Fans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        Gender = c.Int(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        Seniority = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "PostID", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "PostID" });
            DropTable("dbo.Fans");
            DropTable("dbo.Posts");
            DropTable("dbo.Comments");
        }
    }
}
