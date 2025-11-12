namespace work_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KanbanColumnSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Board = c.String(nullable: false, maxLength: 50),
                        ColumnName = c.String(nullable: false, maxLength: 100),
                        WipLimit = c.Int(),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Meetings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 500),
                        MeetingDate = c.DateTime(nullable: false),
                        Participants = c.String(maxLength: 2000),
                        NotesHtml = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 500),
                        Description = c.String(),
                        RequestedBy = c.String(maxLength: 200),
                        RequestedAt = c.DateTime(nullable: false),
                        ProjectId = c.Int(),
                        ModuleId = c.Int(),
                        SourceMeetingId = c.Int(),
                        Type = c.String(maxLength: 100),
                        Urgency = c.String(maxLength: 50),
                        EffortEstimate = c.Decimal(precision: 18, scale: 2),
                        Board = c.String(maxLength: 50),
                        Status = c.String(maxLength: 100),
                        OrderIndex = c.Int(nullable: false),
                        TriagedBy = c.String(maxLength: 200),
                        TriagedAt = c.DateTime(),
                        CreatedAt = c.DateTime(nullable: false),
                        CompletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .ForeignKey("dbo.ProjectModules", t => t.ModuleId)
                .ForeignKey("dbo.Meetings", t => t.SourceMeetingId)
                .Index(t => t.ProjectId)
                .Index(t => t.ModuleId)
                .Index(t => t.SourceMeetingId);
            
            CreateTable(
                "dbo.ProjectModules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkItems", "SourceMeetingId", "dbo.Meetings");
            DropForeignKey("dbo.WorkItems", "ModuleId", "dbo.ProjectModules");
            DropForeignKey("dbo.WorkItems", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectModules", "ProjectId", "dbo.Projects");
            DropIndex("dbo.ProjectModules", new[] { "ProjectId" });
            DropIndex("dbo.WorkItems", new[] { "SourceMeetingId" });
            DropIndex("dbo.WorkItems", new[] { "ModuleId" });
            DropIndex("dbo.WorkItems", new[] { "ProjectId" });
            DropTable("dbo.Projects");
            DropTable("dbo.ProjectModules");
            DropTable("dbo.WorkItems");
            DropTable("dbo.Meetings");
            DropTable("dbo.KanbanColumnSettings");
        }
    }
}
