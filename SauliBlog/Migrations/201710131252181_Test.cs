namespace SauliBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Twitters");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Twitters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TweetsCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
