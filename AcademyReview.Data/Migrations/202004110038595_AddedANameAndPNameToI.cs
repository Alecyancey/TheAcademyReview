namespace AcademyReview.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedANameAndPNameToI : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Instructor", "AcademyName", c => c.String());
            AddColumn("dbo.Instructor", "ProgramName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Instructor", "ProgramName");
            DropColumn("dbo.Instructor", "AcademyName");
        }
    }
}
