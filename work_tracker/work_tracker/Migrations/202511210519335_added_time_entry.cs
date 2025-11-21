namespace work_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_time_entry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimeEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntryDate = c.DateTime(nullable: false),
                        DurationMinutes = c.Int(nullable: false),
                        ActivityType = c.String(nullable: false, maxLength: 50),
                        WorkItemId = c.Int(),
                        ProjectId = c.Int(),
                        ContactName = c.String(maxLength: 200),
                        PhoneNumber = c.String(maxLength: 50),
                        Description = c.String(maxLength: 2000),
                        CreatedBy = c.String(nullable: false, maxLength: 200),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .ForeignKey("dbo.WorkItems", t => t.WorkItemId)
                .Index(t => t.WorkItemId)
                .Index(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeEntries", "WorkItemId", "dbo.WorkItems");
            DropForeignKey("dbo.TimeEntries", "ProjectId", "dbo.Projects");
            DropIndex("dbo.TimeEntries", new[] { "ProjectId" });
            DropIndex("dbo.TimeEntries", new[] { "WorkItemId" });
            DropTable("dbo.TimeEntries");
        }
    }
}
