namespace work_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_workitem_relationships : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkItemRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkItemId1 = c.Int(nullable: false),
                        WorkItemId2 = c.Int(nullable: false),
                        RelationType = c.String(nullable: false, maxLength: 20),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkItems", t => t.WorkItemId1)
                .ForeignKey("dbo.WorkItems", t => t.WorkItemId2)
                .Index(t => new { t.WorkItemId1, t.WorkItemId2, t.RelationType }, unique: true, name: "IX_WorkItemRelations_Unique");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkItemRelations", "WorkItemId2", "dbo.WorkItems");
            DropForeignKey("dbo.WorkItemRelations", "WorkItemId1", "dbo.WorkItems");
            DropIndex("dbo.WorkItemRelations", "IX_WorkItemRelations_Unique");
            DropTable("dbo.WorkItemRelations");
        }
    }
}
