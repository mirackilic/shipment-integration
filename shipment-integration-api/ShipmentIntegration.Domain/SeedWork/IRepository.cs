using System.Linq.Expressions;
using ShipmentIntegration.Domain.Entities;

namespace ShipmentIntegration.Domain.IRepositories;

public interface IRepository<T> where T : IBaseEntity
{
    IQueryable<T> Find(Expression<Func<T, bool>> predicate);
    T FindOne(Expression<Func<T, bool>> predicate);
    IQueryable<T> GetAll();
    Task<T> GetByIdAsync(int id);
    Task CreateAsync(T entity);
    void Delete(T entity);
    void Update(T entity);
}
