using Microsoft.EntityFrameworkCore;
using WorkshopNo1.Entities;
using WorkshopNo1.Entities.Faculties;
using WorkshopNo1.Entities.Students;
using WorkshopNo1.Entities.Subjects;

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
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Subject> Subjects { get; set; }
}