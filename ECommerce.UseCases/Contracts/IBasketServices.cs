using ECommerce.UseCases.Common;
using ECommerce.UseCases.DTOs.Basket;

namespace ECommerce.UseCases.Contracts;

public interface IBasketServices
{
    Task<Result<BasketDto>> GetBasketAsync(string basketId, CancellationToken ct = default);

    Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket, CancellationToken ct = default);

    Task<Result<bool>> DeleteBasketAsync(string basketId, CancellationToken ct = default);
}
