using ECommerce.Domain.Contracts;
using StackExchange.Redis;

namespace ECommerce.Infrastructure.Repositories;

public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
{
    private readonly IDatabase _database = connection.GetDatabase();
    public async Task<string?> GetAsync(string cacheKey, CancellationToken ct = default)
    {
        var value = await _database.StringGetAsync(cacheKey);
        return value.IsNullOrEmpty ? null : value.ToString();
    }

    public async Task SetAsync(string cacheKey, string cachevalue, TimeSpan timeToLive, CancellationToken ct = default)
    {
         await _database.StringSetAsync(cacheKey, cachevalue, timeToLive);
    }
}
