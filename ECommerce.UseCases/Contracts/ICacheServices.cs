namespace ECommerce.UseCases.Contracts;

public interface ICacheServices
{
    Task<string?> GetAsync(string cacheKey, CancellationToken ct = default);

    Task SetAsync(string cacheKey, object cachevalue, TimeSpan timeToLive, CancellationToken ct = default);
}
