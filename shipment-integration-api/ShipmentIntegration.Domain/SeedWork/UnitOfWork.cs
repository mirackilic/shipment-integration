
using ShipmentIntegration.Domain.Context;
using ShipmentIntegration.Domain.Entities;

namespace ShipmentIntegration.Domain.IRepositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ShipmentIntegrationContext _context;

    public UnitOfWork(ShipmentIntegrationContext context)
    {
        _context = context;
    }

    public IRepository<T> GetRepository<T>() where T : BaseEntity
    {
        return new Repository<T>(_context);
    }


    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }
}
