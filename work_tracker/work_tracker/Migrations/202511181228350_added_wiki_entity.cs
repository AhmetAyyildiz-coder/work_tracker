namespace work_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_wiki_entity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WikiPages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        Slug = c.String(maxLength: 200),
                        ContentHtml = c.String(),
                        Summary = c.String(maxLength: 500),
                        ParentPageId = c.Int(),
                        CreatedBy = c.String(nullable: false, maxLength: 200),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 200),
                        UpdatedAt = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WikiPages", t => t.ParentPageId)
                .Index(t => t.ParentPageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WikiPages", "ParentPageId", "dbo.WikiPages");
            DropIndex("dbo.WikiPages", new[] { "ParentPageId" });
            DropTable("dbo.WikiPages");
        }
    }
}
