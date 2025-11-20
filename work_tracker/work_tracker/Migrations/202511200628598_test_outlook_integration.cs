namespace work_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test_outlook_integration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkItemEmails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OutlookEntryId = c.String(maxLength: 500),
                        Subject = c.String(nullable: false, maxLength: 500),
                        From = c.String(maxLength: 500),
                        To = c.String(),
                        Cc = c.String(),
                        Body = c.String(),
                        IsHtml = c.Boolean(nullable: false),
                        ReceivedDate = c.DateTime(),
                        SentDate = c.DateTime(),
                        IsRead = c.Boolean(nullable: false),
                        HasAttachments = c.Boolean(nullable: false),
                        AttachmentCount = c.Int(nullable: false),
                        WorkItemId = c.Int(),
                        LinkedBy = c.String(maxLength: 200),
                        LinkedAt = c.DateTime(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkItems", t => t.WorkItemId, cascadeDelete: true)
                .Index(t => t.WorkItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkItemEmails", "WorkItemId", "dbo.WorkItems");
            DropIndex("dbo.WorkItemEmails", new[] { "WorkItemId" });
            DropTable("dbo.WorkItemEmails");
        }
    }
}
