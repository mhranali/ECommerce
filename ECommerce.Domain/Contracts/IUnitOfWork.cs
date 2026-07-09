using ECommerce.Domain.Common;

namespace ECommerce.Domain.Contracts;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken ct = default);

    IGenericRepository<TEntity, TKey> GetGenericRepository<TEntity,TKey>() where TEntity : BaseEntity<TKey>;
}
