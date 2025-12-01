namespace work_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_timeentry_added_meeting_join : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeEntries", "MeetingId", c => c.Int());
            CreateIndex("dbo.TimeEntries", "MeetingId");
            AddForeignKey("dbo.TimeEntries", "MeetingId", "dbo.Meetings", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeEntries", "MeetingId", "dbo.Meetings");
            DropIndex("dbo.TimeEntries", new[] { "MeetingId" });
            DropColumn("dbo.TimeEntries", "MeetingId");
        }
    }
}
