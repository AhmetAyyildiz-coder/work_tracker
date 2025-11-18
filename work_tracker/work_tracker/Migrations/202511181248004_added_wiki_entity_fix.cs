namespace work_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_wiki_entity_fix : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.WorkItems", "InitialSprintId");
            CreateIndex("dbo.WorkItems", "CompletedInSprintId");
            AddForeignKey("dbo.WorkItems", "CompletedInSprintId", "dbo.Sprints", "Id");
            AddForeignKey("dbo.WorkItems", "InitialSprintId", "dbo.Sprints", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkItems", "InitialSprintId", "dbo.Sprints");
            DropForeignKey("dbo.WorkItems", "CompletedInSprintId", "dbo.Sprints");
            DropIndex("dbo.WorkItems", new[] { "CompletedInSprintId" });
            DropIndex("dbo.WorkItems", new[] { "InitialSprintId" });
        }
    }
}
