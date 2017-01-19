using Microsoft.EntityFrameworkCore;
using taskTracker.Models;

namespace taskTracker.Data
{
    public class taskTrackerContext : DbContext
    {
        public taskTrackerContext(DbContextOptions<taskTrackerContext> options)
            : base(options)
        { }

        public DbSet<Task> Task { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .Property(b => b.CompletedOn)
                .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S', 'now', 'localtime')");
            
        }
    }

}