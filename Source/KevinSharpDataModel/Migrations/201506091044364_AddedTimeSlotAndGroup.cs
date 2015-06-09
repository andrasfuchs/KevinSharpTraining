namespace KevinSharp.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTimeSlotAndGroup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimeSlots",
                c => new
                    {
                        TimeSlotId = c.Int(nullable: false, identity: true),
                        StartTimeUtc = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                        TimeSlotGroup_TimeSlotGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.TimeSlotId)
                .ForeignKey("dbo.TimeSlotGroups", t => t.TimeSlotGroup_TimeSlotGroupId)
                .Index(t => t.TimeSlotGroup_TimeSlotGroupId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 4000),
                        Name = c.String(nullable: false, maxLength: 4000),
                        ShortName = c.String(nullable: false, maxLength: 4000),
                        Description = c.String(nullable: false, maxLength: 4000),
                        Length = c.String(nullable: false, maxLength: 4000),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.TimeSlotGroups",
                c => new
                    {
                        TimeSlotGroupId = c.Int(nullable: false, identity: true),
                        Course_CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TimeSlotGroupId)
                .ForeignKey("dbo.Courses", t => t.Course_CourseId, cascadeDelete: true)
                .Index(t => t.Course_CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeSlots", "TimeSlotGroup_TimeSlotGroupId", "dbo.TimeSlotGroups");
            DropForeignKey("dbo.TimeSlotGroups", "Course_CourseId", "dbo.Courses");
            DropIndex("dbo.TimeSlotGroups", new[] { "Course_CourseId" });
            DropIndex("dbo.TimeSlots", new[] { "TimeSlotGroup_TimeSlotGroupId" });
            DropTable("dbo.TimeSlotGroups");
            DropTable("dbo.Courses");
            DropTable("dbo.TimeSlots");
        }
    }
}
