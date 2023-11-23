using Microsoft.EntityFrameworkCore;
using WorkshopNo1.Entities;

namespace WorkshopNo1.Repository;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }
    
    public DbSet<Student> Students { get; set; }
}