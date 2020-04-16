namespace AcademyReview.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUserNameToItems : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rating", "UserId", "dbo.ApplicationUser");
            DropIndex("dbo.Rating", new[] { "UserId" });
            AddColumn("dbo.Academy", "OwnerId", c => c.String());
            AddColumn("dbo.Academy", "CreatedBy", c => c.String());
            AddColumn("dbo.Instructor", "OwnerId", c => c.String());
            AddColumn("dbo.Instructor", "CreatedBy", c => c.String());
            AddColumn("dbo.Program", "OwnerId", c => c.String());
            AddColumn("dbo.Program", "CreatedBy", c => c.String());
            AddColumn("dbo.Rating", "OwnerId", c => c.String());
            AddColumn("dbo.Rating", "CreatedBy", c => c.String());
            DropColumn("dbo.Rating", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rating", "UserId", c => c.String(maxLength: 128));
            DropColumn("dbo.Rating", "CreatedBy");
            DropColumn("dbo.Rating", "OwnerId");
            DropColumn("dbo.Program", "CreatedBy");
            DropColumn("dbo.Program", "OwnerId");
            DropColumn("dbo.Instructor", "CreatedBy");
            DropColumn("dbo.Instructor", "OwnerId");
            DropColumn("dbo.Academy", "CreatedBy");
            DropColumn("dbo.Academy", "OwnerId");
            CreateIndex("dbo.Rating", "UserId");
            AddForeignKey("dbo.Rating", "UserId", "dbo.ApplicationUser", "Id");
        }
    }
}
