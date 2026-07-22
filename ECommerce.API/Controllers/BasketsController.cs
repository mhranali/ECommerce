using ECommerce.UseCases.Contracts;
using ECommerce.UseCases.DTOs.Basket;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;
public class BasketsController(IBasketServices basketServices) : ApiBaseController
{
    private readonly IBasketServices _basketServices = basketServices;

    [HttpGet("{basketId}")]
    public async Task<ActionResult<BasketDto>> GetBasket(string basketId,CancellationToken ct = default)
    {
        var basket = await _basketServices.GetBasketAsync(basketId, ct);

        return ToActionResult(basket);
    }

    [HttpPost]
    public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket,CancellationToken ct = default)
    {
        var resultBasket = await _basketServices.CreateOrUpdateBasketAsync(basket, ct);

        return ToActionResult(resultBasket);
    }

    [HttpDelete("{basketId}")]
    public async Task<ActionResult<bool>> DeleteBasket(string basketId, CancellationToken ct = default)
    {
        var result = await _basketServices.DeleteBasketAsync(basketId, ct);

        return ToActionResult(result);
    }
}
