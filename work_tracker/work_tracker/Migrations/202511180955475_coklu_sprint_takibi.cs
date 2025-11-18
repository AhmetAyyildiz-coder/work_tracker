namespace work_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class coklu_sprint_takibi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkItems", "InitialSprintId", c => c.Int());
            AddColumn("dbo.WorkItems", "CompletedInSprintId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkItems", "CompletedInSprintId");
            DropColumn("dbo.WorkItems", "InitialSprintId");
        }
    }
}
