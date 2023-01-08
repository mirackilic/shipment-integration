using ShipmentIntegration.Domain.Entities;

namespace ShipmentIntegration.Domain.IRepositories;

public interface IUnitOfWork
{
    IRepository<T> GetRepository<T>() where T : BaseEntity;
    Task<int> SaveChangesAsync();
    int SaveChanges();
}
    