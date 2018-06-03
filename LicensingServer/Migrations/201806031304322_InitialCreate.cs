namespace LicensingServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationDetails",
                c => new
                    {
                        DetailID = c.Int(nullable: false, identity: true),
                        ApplicationID = c.Int(nullable: false),
                        DetailName = c.String(),
                        Description = c.String(),
                        ExpireDate = c.DateTime(nullable: false),
                        Version = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.DetailID)
                .ForeignKey("dbo.Applications", t => t.ApplicationID, cascadeDelete: true)
                .Index(t => t.ApplicationID);
            
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        ApplicationID = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Password = c.Binary(),
                        EndPoint = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ApplicationID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.Binary(),
                        Email = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        State = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationDetails", "ApplicationID", "dbo.Applications");
            DropIndex("dbo.ApplicationDetails", new[] { "ApplicationID" });
            DropTable("dbo.Users");
            DropTable("dbo.Applications");
            DropTable("dbo.ApplicationDetails");
        }
    }
}
