namespace SauliBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTwitter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Twitters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Twitters");
        }
    }
}
