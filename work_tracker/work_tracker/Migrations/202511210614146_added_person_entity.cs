namespace work_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_person_entity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Persons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Email = c.String(maxLength: 200),
                        PhoneNumber = c.String(maxLength: 50),
                        Notes = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkItemPersons",
                c => new
                    {
                        WorkItemId = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkItemId, t.PersonId })
                .ForeignKey("dbo.WorkItems", t => t.WorkItemId, cascadeDelete: true)
                .ForeignKey("dbo.Persons", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.WorkItemId)
                .Index(t => t.PersonId);
            
            AddColumn("dbo.TimeEntries", "PersonId", c => c.Int());
            CreateIndex("dbo.TimeEntries", "PersonId");
            AddForeignKey("dbo.TimeEntries", "PersonId", "dbo.Persons", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkItemPersons", "PersonId", "dbo.Persons");
            DropForeignKey("dbo.WorkItemPersons", "WorkItemId", "dbo.WorkItems");
            DropForeignKey("dbo.TimeEntries", "PersonId", "dbo.Persons");
            DropIndex("dbo.WorkItemPersons", new[] { "PersonId" });
            DropIndex("dbo.WorkItemPersons", new[] { "WorkItemId" });
            DropIndex("dbo.TimeEntries", new[] { "PersonId" });
            DropColumn("dbo.TimeEntries", "PersonId");
            DropTable("dbo.WorkItemPersons");
            DropTable("dbo.Persons");
        }
    }
}
