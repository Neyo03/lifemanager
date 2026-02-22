using Microsoft.EntityFrameworkCore;

namespace LifeManager.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Room> Rooms { get; set; }
    public DbSet<HouseTask> HouseTasks { get; set; }
    
    public DbSet<Tag> Tags { get; set; }
}