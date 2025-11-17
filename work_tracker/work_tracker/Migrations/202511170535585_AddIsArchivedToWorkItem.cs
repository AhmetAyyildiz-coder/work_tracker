namespace work_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsArchivedToWorkItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkItems", "IsArchived", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkItems", "IsArchived");
        }
    }
}
