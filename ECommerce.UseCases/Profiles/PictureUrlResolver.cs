using AutoMapper;
using ECommerce.Domain.Entities.Products;
using ECommerce.UseCases.DTOs.Products;
using Microsoft.Extensions.Options;

namespace ECommerce.UseCases.Profiles;

internal class PictureUrlResolver(IOptions<UrlSettings> options) : IValueResolver<Product, ProductDto, string>
{
    private readonly UrlSettings _urlSettings = options.Value;
    public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
    {
        var baseUrl = _urlSettings.BaseUrl.TrimEnd("/");
        var path = source.PictureUrl.TrimStart("/");
        return $"{baseUrl}/Files/{path}";
    }
}

public class UrlSettings
{
    public string BaseUrl { get; set; }
}
