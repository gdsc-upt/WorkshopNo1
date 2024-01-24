namespace WorkshopNo1.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task SaveChangesAsync(CancellationToken token = default)
    { 
        await _context.SaveChangesAsync(token);
    }
}