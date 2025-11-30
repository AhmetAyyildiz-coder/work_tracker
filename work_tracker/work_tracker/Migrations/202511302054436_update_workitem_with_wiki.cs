namespace work_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_workitem_with_wiki : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WikiPages", "WorkItemId", c => c.Int());
            CreateIndex("dbo.WikiPages", "WorkItemId");
            AddForeignKey("dbo.WikiPages", "WorkItemId", "dbo.WorkItems", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WikiPages", "WorkItemId", "dbo.WorkItems");
            DropIndex("dbo.WikiPages", new[] { "WorkItemId" });
            DropColumn("dbo.WikiPages", "WorkItemId");
        }
    }
}
