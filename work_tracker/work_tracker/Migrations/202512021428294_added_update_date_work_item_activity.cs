namespace work_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_update_date_work_item_activity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkItemActivities", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.WorkItemActivities", "UpdatedBy", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkItemActivities", "UpdatedBy");
            DropColumn("dbo.WorkItemActivities", "UpdatedAt");
        }
    }
}
