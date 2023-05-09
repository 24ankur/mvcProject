namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TitleOfTheBook = c.String(),
                        Date = c.DateTime(nullable: false),
                        Location = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        DurationInHours = c.Int(nullable: false),
                        Description = c.String(),
                        OtherDetails = c.String(),
                        InviteByEmails = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserModelEventModel",
                c => new
                    {
                        UserModel_Id = c.Int(nullable: false),
                        EventModel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserModel_Id, t.EventModel_Id })
                .ForeignKey("dbo.UserModel", t => t.UserModel_Id, cascadeDelete: true)
                .ForeignKey("dbo.EventModel", t => t.EventModel_Id, cascadeDelete: true)
                .Index(t => t.UserModel_Id)
                .Index(t => t.EventModel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserModelEventModel", "EventModel_Id", "dbo.EventModel");
            DropForeignKey("dbo.UserModelEventModel", "UserModel_Id", "dbo.UserModel");
            DropIndex("dbo.UserModelEventModel", new[] { "EventModel_Id" });
            DropIndex("dbo.UserModelEventModel", new[] { "UserModel_Id" });
            DropTable("dbo.UserModelEventModel");
            DropTable("dbo.UserModel");
            DropTable("dbo.EventModel");
        }
    }
}
