using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseType> CourseTypes { get; set; }
        public DbSet<CourseClass> CourseClasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasIndex(c => c.Code)
                .IsUnique();

            modelBuilder.Entity<Course>()
                .HasMany(c => c.CourseTypes)
                .WithOne(ct => ct.Courses)
                .HasForeignKey(ct => ct.CourseId);

            modelBuilder.Entity<CourseType>()
                .HasOne(ct => ct.Courses)
                .WithMany(c => c.CourseTypes)
                .HasForeignKey(ct => ct.CourseId);

            modelBuilder.Entity<CourseType>()
                .HasMany(ct => ct.CourseClasses)
                .WithOne(cc => cc.CourseTypes)
                .HasForeignKey(cc => cc.CourseTypeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}