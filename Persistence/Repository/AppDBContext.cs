using Microsoft.EntityFrameworkCore;
using TRIAL.Persistence.entity;

namespace TRIAL.Persistence.Repository
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<HomeworkStudent> HwS { get; set; }
        public DbSet<HomeworkTeacher> HwT { get; set; }
        public DbSet<Marks> marks { get; set; }
        public DbSet<PersonalInformation> PersonalInfo { get; set; }
        public DbSet<Subjects> subjectNa { get; set; }
        public DbSet<Registration> registrations { get; set; }
        public DbSet<EmailVerification> emailVerification { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            // foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            // {
            //     relationship.DeleteBehavior = DeleteBehavior.Restrict;
            // }
            base.OnModelCreating(modelBuilder);

            // Configure the one-to-one relationship between Subjects and PersonalInformation
            modelBuilder.Entity<Subjects>()
                .HasOne(s => s.perInfo)
                .WithOne(p => p.subjects)
                .HasForeignKey<Subjects>(s => s.perInfoId)
                .OnDelete(DeleteBehavior.Restrict);  // Ensure no cascade delete

            // Configure the many-to-one or one-to-many relationships
            modelBuilder.Entity<Marks>()
                .HasOne(m => m.perInfo)
                .WithMany(p => p.subject)
                .HasForeignKey(m => m.perInfoId)
                .OnDelete(DeleteBehavior.Restrict);  // Ensure no cascade delete

            modelBuilder.Entity<Marks>()
                .HasOne(m => m.subjects)
                .WithMany(s => s.marks)
                .HasForeignKey(m => m.subjectsId)
                .OnDelete(DeleteBehavior.Restrict);  // Ensure no cascade delete

            modelBuilder.Entity<HomeworkTeacher>()
                .HasOne(h => h.subjects)
                .WithMany(s => s.homeworkTs)
                .HasForeignKey(h => h.subjectsId)
                .OnDelete(DeleteBehavior.Cascade);  // Ensure no cascade delet
        }

    }
}
