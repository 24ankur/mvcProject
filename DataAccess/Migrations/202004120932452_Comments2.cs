namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comments2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EventModel", "DurationInHours", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EventModel", "DurationInHours", c => c.Int(nullable: false));
        }
    }
}
