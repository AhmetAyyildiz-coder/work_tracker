using System;
using System.Data.Entity.Migrations;
using System.Linq;
using work_tracker.Data;
using work_tracker.Data.Entities;

namespace work_tracker.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<WorkTrackerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "work_tracker.Data.WorkTrackerDbContext";
        }

        protected override void Seed(WorkTrackerDbContext context)
        {
            // Varsayılan Kanban sütunlarını ekle
            if (!context.KanbanColumnSettings.Any())
            {
                context.KanbanColumnSettings.AddRange(new[]
                {
                    new KanbanColumnSetting { Board = "Kanban", ColumnName = "GelenAcilIsler", DisplayOrder = 1, WipLimit = null },
                    new KanbanColumnSetting { Board = "Kanban", ColumnName = "Sirada", DisplayOrder = 2, WipLimit = null },
                    new KanbanColumnSetting { Board = "Kanban", ColumnName = "MudahaleEdiliyor", DisplayOrder = 3, WipLimit = 3 },
                    new KanbanColumnSetting { Board = "Kanban", ColumnName = "DogrulamaBekliyor", DisplayOrder = 4, WipLimit = null },
                    new KanbanColumnSetting { Board = "Kanban", ColumnName = "Cozuldu", DisplayOrder = 5, WipLimit = null },
                    
                    new KanbanColumnSetting { Board = "Scrum", ColumnName = "SprintBacklog", DisplayOrder = 1, WipLimit = null },
                    new KanbanColumnSetting { Board = "Scrum", ColumnName = "Gelistirmede", DisplayOrder = 2, WipLimit = null },
                    new KanbanColumnSetting { Board = "Scrum", ColumnName = "Testte", DisplayOrder = 3, WipLimit = null },
                    new KanbanColumnSetting { Board = "Scrum", ColumnName = "Tamamlandi", DisplayOrder = 4, WipLimit = null }
                });

                context.SaveChanges();
            }

            // Demo proje ekle
            if (!context.Projects.Any())
            {
                var demoProject = new Project
                {
                    Name = "Demo Proje",
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };
                context.Projects.Add(demoProject);
                context.SaveChanges();

                // Demo modüller
                context.ProjectModules.AddRange(new[]
                {
                    new ProjectModule { ProjectId = demoProject.Id, Name = "SQL", IsActive = true, CreatedAt = DateTime.Now },
                    new ProjectModule { ProjectId = demoProject.Id, Name = "Ekran", IsActive = true, CreatedAt = DateTime.Now },
                    new ProjectModule { ProjectId = demoProject.Id, Name = "API", IsActive = true, CreatedAt = DateTime.Now },
                    new ProjectModule { ProjectId = demoProject.Id, Name = "Rapor", IsActive = true, CreatedAt = DateTime.Now }
                });

                context.SaveChanges();
            }
        }
    }
}

