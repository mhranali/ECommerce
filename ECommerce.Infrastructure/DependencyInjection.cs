using ECommerce.Domain.Contracts;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.DataSeeding;
using ECommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StoreDbContext>(options => 
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });


        services.AddKeyedScoped<IDataSeeder, CatalogDataSeeder>("Catalog");
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
