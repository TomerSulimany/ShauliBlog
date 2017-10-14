namespace SauliBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Location : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Location", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Location");
        }
    }
}
