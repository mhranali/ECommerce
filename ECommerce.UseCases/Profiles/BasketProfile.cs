using AutoMapper;
using ECommerce.Domain.Entities.Basket;
using ECommerce.UseCases.DTOs.Basket;

namespace ECommerce.UseCases.Profiles;

public class BasketProfile : Profile
{
    public BasketProfile()
    {
        CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        CreateMap<CustomerBasket, BasketDto>().ReverseMap();
    }
}
