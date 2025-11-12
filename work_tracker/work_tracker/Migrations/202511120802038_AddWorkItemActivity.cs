namespace work_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWorkItemActivity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkItemActivities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkItemId = c.Int(nullable: false),
                        ActivityType = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        OldValue = c.String(maxLength: 500),
                        NewValue = c.String(maxLength: 500),
                        CreatedBy = c.String(nullable: false, maxLength: 200),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkItems", t => t.WorkItemId, cascadeDelete: true)
                .Index(t => t.WorkItemId);
            
            CreateTable(
                "dbo.WorkItemAttachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkItemId = c.Int(nullable: false),
                        OriginalFileName = c.String(nullable: false, maxLength: 255),
                        StoredFileName = c.String(nullable: false, maxLength: 255),
                        FileExtension = c.String(maxLength: 50),
                        FileSizeBytes = c.Long(nullable: false),
                        MimeType = c.String(maxLength: 100),
                        Description = c.String(maxLength: 500),
                        UploadedBy = c.String(nullable: false, maxLength: 200),
                        UploadedAt = c.DateTime(nullable: false),
                        FilePath = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkItems", t => t.WorkItemId, cascadeDelete: true)
                .Index(t => t.WorkItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkItemAttachments", "WorkItemId", "dbo.WorkItems");
            DropForeignKey("dbo.WorkItemActivities", "WorkItemId", "dbo.WorkItems");
            DropIndex("dbo.WorkItemAttachments", new[] { "WorkItemId" });
            DropIndex("dbo.WorkItemActivities", new[] { "WorkItemId" });
            DropTable("dbo.WorkItemAttachments");
            DropTable("dbo.WorkItemActivities");
        }
    }
}
