namespace SauliBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationByTomerTest : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Author", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Author", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
