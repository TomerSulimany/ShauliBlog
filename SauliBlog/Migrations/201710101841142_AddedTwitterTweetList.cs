namespace SauliBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTwitterTweetList : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Twitters", "TweetsCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Twitters", "TweetsCount");
        }
    }
}
