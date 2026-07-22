namespace ECommerce.Domain.Contracts;

public interface ICacheRepository
{
    Task<string?> GetAsync(string cacheKey, CancellationToken ct = default);

    Task SetAsync(string cacheKey, string cachevalue, TimeSpan timeToLive, CancellationToken ct = default);
}
