namespace AcademyReview.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedInstructorToDataLevel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Instructor",
                c => new
                    {
                        InstructorId = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        AcademyId = c.Int(nullable: false),
                        ProgramId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InstructorId)
                .ForeignKey("dbo.Academy", t => t.AcademyId, cascadeDelete: false)
                .ForeignKey("dbo.Program", t => t.ProgramId, cascadeDelete: false)
                .Index(t => t.AcademyId)
                .Index(t => t.ProgramId);
            
            AddColumn("dbo.Rating", "InstructorId", c => c.Int());
            CreateIndex("dbo.Rating", "InstructorId");
            AddForeignKey("dbo.Rating", "InstructorId", "dbo.Instructor", "InstructorId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rating", "InstructorId", "dbo.Instructor");
            DropForeignKey("dbo.Instructor", "ProgramId", "dbo.Program");
            DropForeignKey("dbo.Instructor", "AcademyId", "dbo.Academy");
            DropIndex("dbo.Instructor", new[] { "ProgramId" });
            DropIndex("dbo.Instructor", new[] { "AcademyId" });
            DropIndex("dbo.Rating", new[] { "InstructorId" });
            DropColumn("dbo.Rating", "InstructorId");
            DropTable("dbo.Instructor");
        }
    }
}
