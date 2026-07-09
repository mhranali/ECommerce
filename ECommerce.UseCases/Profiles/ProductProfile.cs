using AutoMapper;
using ECommerce.Domain.Entities.Products;
using ECommerce.UseCases.DTOs.Products;

namespace ECommerce.UseCases.Profiles;

internal class ProductProfile : Profile
{
    public ProductProfile() 
    {
        CreateMap<ProductBrand, BrandDto>();
        CreateMap<ProductType, TypeDto>();
        CreateMap<Product, ProductDto>()
            .ForMember(dst => dst.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name))
            .ForMember(dst => dst.ProductType, opt => opt.MapFrom(src => src.ProductType.Name));
    }
}
