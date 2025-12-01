namespace work_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixed_outlook_integration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkItemEmails", "ConversationId", c => c.String(maxLength: 500));
            AddColumn("dbo.WorkItemEmails", "LastKnownFolder", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkItemEmails", "LastKnownFolder");
            DropColumn("dbo.WorkItemEmails", "ConversationId");
        }
    }
}
