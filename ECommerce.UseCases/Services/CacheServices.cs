using ECommerce.Domain.Contracts;
using ECommerce.UseCases.Contracts;
using System.Text.Json;

namespace ECommerce.UseCases.Services;

public class CacheServices(ICacheRepository cacheRepository) : ICacheServices
{
    private readonly ICacheRepository _cacheRepository = cacheRepository;

    public Task<string?> GetAsync(string cacheKey, CancellationToken ct = default)
    {
       return  _cacheRepository.GetAsync(cacheKey, ct);
    }

    public Task SetAsync(string cacheKey, object cachevalue, TimeSpan timeToLive, CancellationToken ct = default)
    {
        var json = JsonSerializer.Serialize(cachevalue, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return _cacheRepository.SetAsync(cacheKey, json, timeToLive, ct);
    }
}
