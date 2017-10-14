namespace SauliBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "Author", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "Author", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
