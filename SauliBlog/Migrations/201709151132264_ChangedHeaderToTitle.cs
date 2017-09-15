namespace SauliBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedHeaderToTitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Title", c => c.String());
            AddColumn("dbo.Posts", "Title", c => c.String());
            DropColumn("dbo.Comments", "Header");
            DropColumn("dbo.Posts", "Header");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Header", c => c.String());
            AddColumn("dbo.Comments", "Header", c => c.String());
            DropColumn("dbo.Posts", "Title");
            DropColumn("dbo.Comments", "Title");
        }
    }
}
