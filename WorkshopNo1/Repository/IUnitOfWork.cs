namespace WorkshopNo1.Repository;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken token = default);
}