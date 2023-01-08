using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShipmentIntegration.Domain.Context;
using ShipmentIntegration.Domain.Entities;

namespace ShipmentIntegration.Domain.IRepositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    #region Settings

    private readonly ShipmentIntegrationContext _context;
    private DbSet<T> _entity;

    public Repository(ShipmentIntegrationContext context)
    {
        _context = context;
        _entity = context.Set<T>();
    }

    protected DbSet<T> Table => _entity ?? (_entity = _context.Set<T>());

    #endregion

    public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
    {
        return Table.Where(predicate).AsQueryable();
    }

    public T FindOne(Expression<Func<T, bool>> predicate)
    {
        return Table.FirstOrDefault(predicate);
    }

    public IQueryable<T> GetAll()
    {
        return Table.Where(x => x.CreatedDate != null).ToList().AsQueryable();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await Table.FindAsync(id);
    }

    public async Task CreateAsync(T entity)
    {
        await Table.AddAsync(entity);
    }

    public void Create(T entity)
    {
        entity.CreatedDate = DateTime.UtcNow;
        entity.UpdatedDate = DateTime.UtcNow;
        Table.Add(entity);
    }

    public void Delete(T entity)
    {
        Table.Remove(entity);
    }

    public void Update(T entity)
    {
        entity.UpdatedDate = DateTime.UtcNow;
        Table.Update(entity);
    }
}
