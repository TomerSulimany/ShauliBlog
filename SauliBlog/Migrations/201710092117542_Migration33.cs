namespace SauliBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration33 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "FanUser", c => c.Int());
            CreateIndex("dbo.Posts", "FanUser");
            AddForeignKey("dbo.Posts", "FanUser", "dbo.Fans", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "FanUser", "dbo.Fans");
            DropIndex("dbo.Posts", new[] { "FanUser" });
            DropColumn("dbo.Posts", "FanUser");
        }
    }
}
