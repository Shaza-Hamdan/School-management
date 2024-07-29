using Microsoft.EntityFrameworkCore;
using TRIAL.Persistence.entity;

namespace TRIAL.Persistence.Repository
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options) { }

        public DbSet<HomeworkS> HwS { get; set; }
        public DbSet<HomeworkT> HwT { get; set; }
        public DbSet<Marks> marks { get; set; }
        public DbSet<PerInfo> PersonalInfo { get; set; }
        public DbSet<Subjects> subjectNa { get; set; }
        public DbSet<Registration> registrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}