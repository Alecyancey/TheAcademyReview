namespace AcademyReview.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialConfiguration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Academy",
                c => new
                    {
                        AcademyId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AcademyId);
            
            CreateTable(
                "dbo.Program",
                c => new
                    {
                        ProgramId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        Prerequisite = c.String(),
                        AcademyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProgramId)
                .ForeignKey("dbo.Academy", t => t.AcademyId, cascadeDelete: true)
                .Index(t => t.AcademyId);
            
            CreateTable(
                "dbo.Rating",
                c => new
                    {
                        RatingId = c.Int(nullable: false, identity: true),
                        Score = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Description = c.String(),
                        ProgramId = c.Int(),
                        AcademyId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.RatingId)
                .ForeignKey("dbo.Program", t => t.ProgramId, cascadeDelete: true)
                .ForeignKey("dbo.Academy", t => t.AcademyId, cascadeDelete: false)
                .ForeignKey("dbo.ApplicationUser", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ProgramId)
                .Index(t => t.AcademyId);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Rating", "UserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.Rating", "AcademyId", "dbo.Academy");
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.Rating", "ProgramId", "dbo.Program");
            DropForeignKey("dbo.Program", "AcademyId", "dbo.Academy");
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Rating", new[] { "AcademyId" });
            DropIndex("dbo.Rating", new[] { "ProgramId" });
            DropIndex("dbo.Rating", new[] { "UserId" });
            DropIndex("dbo.Program", new[] { "AcademyId" });
            DropTable("dbo.IdentityRole");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.Rating");
            DropTable("dbo.Program");
            DropTable("dbo.Academy");
        }
    }
}
