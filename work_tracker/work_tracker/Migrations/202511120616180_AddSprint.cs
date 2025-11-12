namespace work_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSprint : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sprints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Goals = c.String(maxLength: 1000),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Status = c.String(nullable: false, maxLength: 50),
                        CreatedAt = c.DateTime(nullable: false),
                        CompletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.WorkItems", "SprintId", c => c.Int());
            CreateIndex("dbo.WorkItems", "SprintId");
            AddForeignKey("dbo.WorkItems", "SprintId", "dbo.Sprints", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkItems", "SprintId", "dbo.Sprints");
            DropIndex("dbo.WorkItems", new[] { "SprintId" });
            DropColumn("dbo.WorkItems", "SprintId");
            DropTable("dbo.Sprints");
        }
    }
}
