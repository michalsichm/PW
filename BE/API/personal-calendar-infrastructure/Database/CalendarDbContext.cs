using Microsoft.EntityFrameworkCore;
using personal_calendar_domain.Models;

namespace personal_calendar_infrastructure.Database;


public class CalendarDbContext : DbContext
{
    public CalendarDbContext(DbContextOptions<CalendarDbContext> options)
    : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Event> Events { get; set; }
    // public DbSet<Admin> Admins { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasKey(u => u.UserId);

        // base.OnModelCreating(modelBuilder);
        // modelBuilder.Entity<Admin>().HasKey(a => a.AdminId);
    }
}
