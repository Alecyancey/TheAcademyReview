namespace AcademyReview.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedViewRatingsToInstructor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rating", "ProgramName", c => c.String());
            AddColumn("dbo.Rating", "FullName", c => c.String());
            AddColumn("dbo.Rating", "AcademyName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rating", "AcademyName");
            DropColumn("dbo.Rating", "FullName");
            DropColumn("dbo.Rating", "ProgramName");
        }
    }
}
