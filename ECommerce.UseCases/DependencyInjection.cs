using ECommerce.UseCases.Contracts;
using ECommerce.UseCases.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.UseCases;

public static class DependencyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddAutoMapper(c => { }, typeof(DependencyInjection).Assembly);

        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}
