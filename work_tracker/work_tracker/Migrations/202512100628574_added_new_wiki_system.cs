namespace work_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_new_wiki_system : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DocumentReferences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 300),
                        FilePath = c.String(nullable: false, maxLength: 1000),
                        FileType = c.String(maxLength: 50),
                        Description = c.String(maxLength: 1000),
                        ProjectId = c.Int(),
                        ModuleId = c.Int(),
                        WorkItemId = c.Int(),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 200),
                        LastAccessedAt = c.DateTime(),
                        IsFavorite = c.Boolean(nullable: false),
                        IsArchived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProjectModules", t => t.ModuleId)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .ForeignKey("dbo.WorkItems", t => t.WorkItemId)
                .Index(t => t.ProjectId)
                .Index(t => t.ModuleId)
                .Index(t => t.WorkItemId);
            
            CreateTable(
                "dbo.DocumentTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Color = c.String(maxLength: 20),
                        Description = c.String(maxLength: 500),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DocumentReferenceTags",
                c => new
                    {
                        DocumentReferenceId = c.Int(nullable: false),
                        DocumentTagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DocumentReferenceId, t.DocumentTagId })
                .ForeignKey("dbo.DocumentReferences", t => t.DocumentReferenceId, cascadeDelete: true)
                .ForeignKey("dbo.DocumentTags", t => t.DocumentTagId, cascadeDelete: true)
                .Index(t => t.DocumentReferenceId)
                .Index(t => t.DocumentTagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DocumentReferences", "WorkItemId", "dbo.WorkItems");
            DropForeignKey("dbo.DocumentReferenceTags", "DocumentTagId", "dbo.DocumentTags");
            DropForeignKey("dbo.DocumentReferenceTags", "DocumentReferenceId", "dbo.DocumentReferences");
            DropForeignKey("dbo.DocumentReferences", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.DocumentReferences", "ModuleId", "dbo.ProjectModules");
            DropIndex("dbo.DocumentReferenceTags", new[] { "DocumentTagId" });
            DropIndex("dbo.DocumentReferenceTags", new[] { "DocumentReferenceId" });
            DropIndex("dbo.DocumentReferences", new[] { "WorkItemId" });
            DropIndex("dbo.DocumentReferences", new[] { "ModuleId" });
            DropIndex("dbo.DocumentReferences", new[] { "ProjectId" });
            DropTable("dbo.DocumentReferenceTags");
            DropTable("dbo.DocumentTags");
            DropTable("dbo.DocumentReferences");
        }
    }
}
