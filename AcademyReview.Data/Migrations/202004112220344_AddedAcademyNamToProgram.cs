namespace AcademyReview.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAcademyNamToProgram : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Program", "AcademyName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Program", "AcademyName");
        }
    }
}
