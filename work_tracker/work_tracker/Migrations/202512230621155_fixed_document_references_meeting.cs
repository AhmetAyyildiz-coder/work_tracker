namespace work_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixed_document_references_meeting : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DocumentReferences", "MeetingId", c => c.Int());
            CreateIndex("dbo.DocumentReferences", "MeetingId");
            AddForeignKey("dbo.DocumentReferences", "MeetingId", "dbo.Meetings", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DocumentReferences", "MeetingId", "dbo.Meetings");
            DropIndex("dbo.DocumentReferences", new[] { "MeetingId" });
            DropColumn("dbo.DocumentReferences", "MeetingId");
        }
    }
}
