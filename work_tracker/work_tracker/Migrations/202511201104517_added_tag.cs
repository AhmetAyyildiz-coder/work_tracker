namespace work_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_tag : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ColorHex = c.String(maxLength: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkItemTags",
                c => new
                    {
                        WorkItemId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkItemId, t.TagId })
                .ForeignKey("dbo.WorkItems", t => t.WorkItemId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.WorkItemId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkItemTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.WorkItemTags", "WorkItemId", "dbo.WorkItems");
            DropIndex("dbo.WorkItemTags", new[] { "TagId" });
            DropIndex("dbo.WorkItemTags", new[] { "WorkItemId" });
            DropTable("dbo.WorkItemTags");
            DropTable("dbo.Tags");
        }
    }
}
