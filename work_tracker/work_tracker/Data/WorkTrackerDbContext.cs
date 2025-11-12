using System;
using System.Data.Entity;
using work_tracker.Data.Entities;

namespace work_tracker.Data
{
    public class WorkTrackerDbContext : DbContext, IDisposable
    {
        public WorkTrackerDbContext() : base("name=WorkTrackerDb")
        {
        }

        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectModule> ProjectModules { get; set; }
        public virtual DbSet<Meeting> Meetings { get; set; }
        public virtual DbSet<WorkItem> WorkItems { get; set; }
        public virtual DbSet<Sprint> Sprints { get; set; }
        public virtual DbSet<KanbanColumnSetting> KanbanColumnSettings { get; set; }
        public virtual DbSet<WorkItemActivity> WorkItemActivities { get; set; }
        public virtual DbSet<WorkItemAttachment> WorkItemAttachments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Project - Modules ilişkisi
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Modules)
                .WithRequired(m => m.Project)
                .HasForeignKey(m => m.ProjectId)
                .WillCascadeOnDelete(false);

            // Project - WorkItems ilişkisi
            modelBuilder.Entity<Project>()
                .HasMany(p => p.WorkItems)
                .WithOptional(w => w.Project)
                .HasForeignKey(w => w.ProjectId)
                .WillCascadeOnDelete(false);

            // Module - WorkItems ilişkisi
            modelBuilder.Entity<ProjectModule>()
                .HasMany(m => m.WorkItems)
                .WithOptional(w => w.Module)
                .HasForeignKey(w => w.ModuleId)
                .WillCascadeOnDelete(false);

            // Meeting - WorkItems ilişkisi
            modelBuilder.Entity<Meeting>()
                .HasMany(m => m.WorkItems)
                .WithOptional(w => w.SourceMeeting)
                .HasForeignKey(w => w.SourceMeetingId)
                .WillCascadeOnDelete(false);

            // Sprint - WorkItems ilişkisi
            modelBuilder.Entity<Sprint>()
                .HasMany(s => s.WorkItems)
                .WithOptional(w => w.Sprint)
                .HasForeignKey(w => w.SprintId)
                .WillCascadeOnDelete(false);

            // WorkItem - Activities ilişkisi
            modelBuilder.Entity<WorkItem>()
                .HasMany(w => w.Activities)
                .WithRequired(a => a.WorkItem)
                .HasForeignKey(a => a.WorkItemId)
                .WillCascadeOnDelete(true); // İş silindiğinde aktiviteleri de sil

            // WorkItem - Attachments ilişkisi
            modelBuilder.Entity<WorkItem>()
                .HasMany(w => w.Attachments)
                .WithRequired(a => a.WorkItem)
                .HasForeignKey(a => a.WorkItemId)
                .WillCascadeOnDelete(true); // İş silindiğinde dosyaları da sil
        }
    }
}

