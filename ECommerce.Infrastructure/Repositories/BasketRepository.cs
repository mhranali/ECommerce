using ECommerce.Domain.Entities.Basket;
using ECommerce.UseCases.Common;
using ECommerce.UseCases.Profiles;
using StackExchange.Redis;
using System.Text.Json;

namespace ECommerce.Infrastructure.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDatabase _database;
    public BasketRepository(IConnectionMultiplexer connection)
    {
        _database = connection.GetDatabase();
    }
    public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive, CancellationToken ct = default)
    {
        var json = JsonSerializer.Serialize(basket);

        var success = await _database.StringSetAsync(basket.Id, json, timeToLive ?? TimeSpan.FromDays(30));


        return success ? basket : null;
    }

    public async Task<bool> DeleteBasketAsync(string basketId, CancellationToken ct = default)
    {
         
        return await _database.KeyDeleteAsync(basketId);
    }

    public async Task<CustomerBasket?> GetBasketAsync(string basketId, CancellationToken ct = default)
    {
        var basket = await _database.StringGetAsync(basketId);

        if(basket.IsNullOrEmpty)
            return null;

        return JsonSerializer.Deserialize<CustomerBasket>(basket.ToString());
    }
}
