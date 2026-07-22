using ECommerce.Domain.Entities.Basket;
using ECommerce.UseCases.Common;

namespace ECommerce.UseCases.Profiles;

public interface IBasketRepository
{
    Task<CustomerBasket?> GetBasketAsync(string basketId, CancellationToken ct = default);

    Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket,TimeSpan? timeToLive,CancellationToken ct = default);
    
    Task<bool> DeleteBasketAsync(string basketId, CancellationToken ct = default);
}
