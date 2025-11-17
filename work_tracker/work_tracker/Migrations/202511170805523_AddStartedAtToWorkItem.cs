namespace work_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStartedAtToWorkItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkItems", "StartedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkItems", "StartedAt");
        }
    }
}
