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
        public virtual DbSet<WorkItemEmail> WorkItemEmails { get; set; }
        public virtual DbSet<WikiPage> WikiPages { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<TimeEntry> TimeEntries { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<WorkItemRelation> WorkItemRelations { get; set; }
        public virtual DbSet<WorkItemReminder> WorkItemReminders { get; set; }

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

            // Sprint - WorkItems ilişkisi (aktif sprint)
            modelBuilder.Entity<Sprint>()
                .HasMany(s => s.WorkItems)
                .WithOptional(w => w.Sprint)
                .HasForeignKey(w => w.SprintId)
                .WillCascadeOnDelete(false);

            // Sprint - InitialSprint ilişkisi (ilk sprint)
            modelBuilder.Entity<WorkItem>()
                .HasOptional(w => w.InitialSprint)
                .WithMany()
                .HasForeignKey(w => w.InitialSprintId)
                .WillCascadeOnDelete(false);

            // Sprint - CompletedInSprint ilişkisi (tamamlanan sprint)
            modelBuilder.Entity<WorkItem>()
                .HasOptional(w => w.CompletedInSprint)
                .WithMany()
                .HasForeignKey(w => w.CompletedInSprintId)
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

            // WorkItem - Emails ilişkisi
            modelBuilder.Entity<WorkItem>()
                .HasMany(w => w.Emails)
                .WithOptional(e => e.WorkItem)
                .HasForeignKey(e => e.WorkItemId)
                .WillCascadeOnDelete(true); // İş silindiğinde email bağlantıları da sil

            // WorkItem - Reminders ilişkisi
            modelBuilder.Entity<WorkItem>()
                .HasMany(w => w.Reminders)
                .WithRequired(r => r.WorkItem)
                .HasForeignKey(r => r.WorkItemId)
                .WillCascadeOnDelete(true); // İş silindiğinde hatırlatıcıları da sil

            // WikiPage - Parent/Children ilişkisi
            modelBuilder.Entity<WikiPage>()
                .HasMany(w => w.ChildPages)
                .WithOptional(c => c.ParentPage)
                .HasForeignKey(c => c.ParentPageId)
                .WillCascadeOnDelete(false);

            // WorkItem - Tags (Many-to-Many)
            modelBuilder.Entity<WorkItem>()
                .HasMany(w => w.Tags)
                .WithMany(t => t.WorkItems)
                .Map(m =>
                {
                    m.ToTable("WorkItemTags");
                    m.MapLeftKey("WorkItemId");
                    m.MapRightKey("TagId");
                });

            // WorkItem - Persons (Many-to-Many)
            modelBuilder.Entity<WorkItem>()
                .HasMany(w => w.RequestedByPersons)
                .WithMany(p => p.RequestedWorkItems)
                .Map(m =>
                {
                    m.ToTable("WorkItemPersons");
                    m.MapLeftKey("WorkItemId");
                    m.MapRightKey("PersonId");
                });

            // TimeEntry - WorkItem ilişkisi
            modelBuilder.Entity<TimeEntry>()
                .HasOptional(t => t.WorkItem)
                .WithMany()
                .HasForeignKey(t => t.WorkItemId)
                .WillCascadeOnDelete(false);

            // TimeEntry - Project ilişkisi
            modelBuilder.Entity<TimeEntry>()
                .HasOptional(t => t.Project)
                .WithMany()
                .HasForeignKey(t => t.ProjectId)
                .WillCascadeOnDelete(false);

            // TimeEntry - Person ilişkisi
            modelBuilder.Entity<TimeEntry>()
                .HasOptional(t => t.Person)
                .WithMany(p => p.TimeEntries)
                .HasForeignKey(t => t.PersonId)
                .WillCascadeOnDelete(false);

            // WorkItemRelation - WorkItem ilişkileri
            modelBuilder.Entity<WorkItemRelation>()
                .HasRequired(r => r.SourceWorkItem)
                .WithMany(w => w.RelatedWorkItemsAsSource)
                .HasForeignKey(r => r.WorkItemId1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkItemRelation>()
                .HasRequired(r => r.TargetWorkItem)
                .WithMany(w => w.RelatedWorkItemsAsTarget)
                .HasForeignKey(r => r.WorkItemId2)
                .WillCascadeOnDelete(false);

            // Aynı işin kendisiyle ilişki kurmasını engelle
            modelBuilder.Entity<WorkItemRelation>()
                .HasIndex(r => new { r.WorkItemId1, r.WorkItemId2, r.RelationType })
                .IsUnique()
                .HasName("IX_WorkItemRelations_Unique");
        }
    }
}

