namespace LicensingServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTokens : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuthorizationTokens",
                c => new
                    {
                        TokenID = c.Int(nullable: false, identity: true),
                        ApplicationID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        TokenValue = c.String(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TokenID);
            
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Password", c => c.Binary());
            AlterColumn("dbo.Users", "Username", c => c.String());
            DropTable("dbo.AuthorizationTokens");
        }
    }
}
