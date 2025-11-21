namespace work_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_time_track : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeEntries", "Subject", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TimeEntries", "Subject");
        }
    }
}
