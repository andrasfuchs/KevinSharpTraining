namespace KevinSharp.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedTimeSlotGroups : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeSlotGroups", "Code", c => c.String(nullable: false, maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TimeSlotGroups", "Code");
        }
    }
}
