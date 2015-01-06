namespace Weather.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "appSchema.Forecast",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Period = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        SymbolNbr = c.Byte(nullable: false),
                        SymbolTxt = c.String(nullable: false, maxLength: 30),
                        Temperatur = c.Double(nullable: false),
                        PlaceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("appSchema.Place", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "appSchema.Place",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Region = c.String(nullable: false, maxLength: 100),
                        Country = c.String(nullable: false, maxLength: 100),
                        NextUpdate = c.DateTime(nullable: false),
                        SearchId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("appSchema.Search", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "appSchema.Search",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Word = c.String(nullable: false, maxLength: 50),
                        NextUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "appSchema.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "appSchema.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("appSchema.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "appSchema.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("appSchema.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "appSchema.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("appSchema.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("appSchema.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "appSchema.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "appSchema.UserPlace",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Place_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Place_Id })
                .ForeignKey("appSchema.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("appSchema.Place", t => t.Place_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Place_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("appSchema.AspNetUserRoles", "RoleId", "appSchema.AspNetRoles");
            DropForeignKey("appSchema.AspNetUserRoles", "UserId", "appSchema.AspNetUsers");
            DropForeignKey("appSchema.UserPlace", "Place_Id", "appSchema.Place");
            DropForeignKey("appSchema.UserPlace", "User_Id", "appSchema.AspNetUsers");
            DropForeignKey("appSchema.AspNetUserLogins", "UserId", "appSchema.AspNetUsers");
            DropForeignKey("appSchema.AspNetUserClaims", "UserId", "appSchema.AspNetUsers");
            DropForeignKey("appSchema.Place", "Id", "appSchema.Search");
            DropForeignKey("appSchema.Forecast", "Id", "appSchema.Place");
            DropIndex("appSchema.UserPlace", new[] { "Place_Id" });
            DropIndex("appSchema.UserPlace", new[] { "User_Id" });
            DropIndex("appSchema.AspNetRoles", "RoleNameIndex");
            DropIndex("appSchema.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("appSchema.AspNetUserRoles", new[] { "UserId" });
            DropIndex("appSchema.AspNetUserLogins", new[] { "UserId" });
            DropIndex("appSchema.AspNetUserClaims", new[] { "UserId" });
            DropIndex("appSchema.AspNetUsers", "UserNameIndex");
            DropIndex("appSchema.Place", new[] { "Id" });
            DropIndex("appSchema.Forecast", new[] { "Id" });
            DropTable("appSchema.UserPlace");
            DropTable("appSchema.AspNetRoles");
            DropTable("appSchema.AspNetUserRoles");
            DropTable("appSchema.AspNetUserLogins");
            DropTable("appSchema.AspNetUserClaims");
            DropTable("appSchema.AspNetUsers");
            DropTable("appSchema.Search");
            DropTable("appSchema.Place");
            DropTable("appSchema.Forecast");
        }
    }
}
