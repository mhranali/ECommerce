using AutoMapper;
using ECommerce.Domain.Entities.Basket;
using ECommerce.UseCases.Common;
using ECommerce.UseCases.Contracts;
using ECommerce.UseCases.DTOs.Basket;
using ECommerce.UseCases.Profiles;

namespace ECommerce.UseCases.Services;

public class BasketServices(IBasketRepository basketRepository, IMapper mapper) : IBasketServices
{
    private readonly IBasketRepository _basketRepository = basketRepository;

    private readonly IMapper _mapper = mapper;
    public async Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket, CancellationToken ct = default)
    {
        var customerBasket = _mapper.Map<CustomerBasket>(basket);

        var result = await _basketRepository.CreateOrUpdateBasketAsync(customerBasket, TimeSpan.FromDays(1), ct);

        return result is not null ? Result<BasketDto>.Ok(_mapper.Map<BasketDto>(result)) : Result<BasketDto>.Fail(Error.Failure
            ("CreateOrUpdate.Failure","Can not Set This Basket"));
    }

    public async Task<Result<bool>> DeleteBasketAsync(string basketId, CancellationToken ct = default)
    {
        var isDeleted = await _basketRepository.DeleteBasketAsync(basketId, ct);

        return isDeleted ? Result<bool>.Ok(true) : Result<bool>.Fail(Error.Failure("DeleteBasket.Failure", "Can not Delete This Basket"));
    }

    public async Task<Result<BasketDto>> GetBasketAsync(string basketId, CancellationToken ct = default)
    {
        var basket = await _basketRepository.GetBasketAsync(basketId, ct);

        if(basket is null)
            return Result<BasketDto>.Fail(Error.NotFound("Basket.NotFound", "Basket Not Found"));

        return Result<BasketDto>.Ok(_mapper.Map<BasketDto>(basket));    
    }
}
