using Microsoft.EntityFrameworkCore;

namespace WorkshopNo1;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}