namespace KevinSharp.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SessionLogAndApplicants : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applicants",
                c => new
                    {
                        ApplicantId = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 4000),
                        TimeSlotGroupCode = c.String(nullable: false, maxLength: 4000),
                        Notes = c.String(maxLength: 4000),
                        Course_CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ApplicantId)
                .ForeignKey("dbo.Courses", t => t.Course_CourseId, cascadeDelete: true)
                .Index(t => t.Course_CourseId);
            
            CreateTable(
                "dbo.SessionLogs",
                c => new
                    {
                        SessionLogId = c.Int(nullable: false, identity: true),
                        SessionId = c.String(nullable: false, maxLength: 4000),
                        SessionStartedUtc = c.DateTime(nullable: false),
                        SessionDuration = c.String(nullable: false, maxLength: 4000),
                        UserEmail = c.String(maxLength: 4000),
                        Events = c.String(nullable: false, maxLength: 4000),
                    })
                .PrimaryKey(t => t.SessionLogId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applicants", "Course_CourseId", "dbo.Courses");
            DropIndex("dbo.Applicants", new[] { "Course_CourseId" });
            DropTable("dbo.SessionLogs");
            DropTable("dbo.Applicants");
        }
    }
}
