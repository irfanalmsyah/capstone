using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApi.Models;

namespace WebApi.Data
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){
            
        }

        public DbSet<Semester> Semesters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Semester>(entity =>{
                entity.HasKey(x => x.Id);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}