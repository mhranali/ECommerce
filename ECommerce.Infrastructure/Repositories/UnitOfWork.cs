using ECommerce.Domain.Common;
using ECommerce.Domain.Contracts;
using ECommerce.Infrastructure.Data;

namespace ECommerce.Infrastructure.Repositories;

internal class UnitOfWork(StoreDbContext dbContext) : IUnitOfWork
{
    private readonly Dictionary<string, object> repositories = [];
    public IGenericRepository<TEntity, TKey> GetGenericRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
    {
        var typeName = typeof(TEntity).Name;
        if (repositories.TryGetValue(typeName, out object? value))
            return (IGenericRepository<TEntity, TKey>)value;

        else
        {
            var repo = new GenericRepository<TEntity, TKey>(dbContext);
            repositories[typeName] = repo;
            return repo;
        }
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        => await dbContext.SaveChangesAsync(ct);
}
