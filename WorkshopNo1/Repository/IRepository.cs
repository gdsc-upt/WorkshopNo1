using System.Linq.Expressions;
using WorkshopNo1.Entities;

namespace WorkshopNo1.Repository;

public interface IRepository<T> where T : class, IEntity
{
    IQueryable<T> FindAll(bool trackChanges);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}