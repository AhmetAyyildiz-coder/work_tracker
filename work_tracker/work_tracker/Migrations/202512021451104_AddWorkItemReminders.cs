namespace work_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWorkItemReminders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkItemReminders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkItemId = c.Int(nullable: false),
                        ReminderDate = c.DateTime(nullable: false),
                        Note = c.String(maxLength: 500),
                        IsShown = c.Boolean(nullable: false),
                        IsDismissed = c.Boolean(nullable: false),
                        SnoozeCount = c.Int(nullable: false),
                        LastSnoozedAt = c.DateTime(),
                        CreatedBy = c.String(nullable: false, maxLength: 200),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkItems", t => t.WorkItemId, cascadeDelete: true)
                .Index(t => t.WorkItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkItemReminders", "WorkItemId", "dbo.WorkItems");
            DropIndex("dbo.WorkItemReminders", new[] { "WorkItemId" });
            DropTable("dbo.WorkItemReminders");
        }
    }
}
