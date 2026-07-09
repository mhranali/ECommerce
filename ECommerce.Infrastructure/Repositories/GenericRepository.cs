using ECommerce.Domain.Common;
using ECommerce.Domain.Contracts;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

internal class GenericRepository<TEntity, TKey>(StoreDbContext dbContext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    public void Add(TEntity entity)
        => dbContext.Set<TEntity>().Add(entity);


    public void Remove(TEntity entity)
        => dbContext.Set<TEntity>().Remove(entity);

    public void Update(TEntity entity)
        => dbContext.Set<TEntity>().Update(entity);

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
        => await dbContext.Set<TEntity>().ToListAsync(ct);

    public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default)
        => await dbContext.Set<TEntity>().FindAsync(id, ct);

}
