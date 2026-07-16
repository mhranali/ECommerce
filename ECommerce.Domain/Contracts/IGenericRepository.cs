using ECommerce.Domain.Common;

namespace ECommerce.Domain.Contracts;

public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default);
    Task<TEntity?> GetByIdWithSpecificationAsync(ISpecifications<TEntity,TKey> specifications, CancellationToken ct = default);
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<TEntity>> GetAllWithSpecificationsAsync(ISpecifications<TEntity,TKey> specifications,CancellationToken ct = default);
    Task<int> GetCountWithSpecificationsAsync(ISpecifications<TEntity,TKey> specifications,CancellationToken ct = default);

}
