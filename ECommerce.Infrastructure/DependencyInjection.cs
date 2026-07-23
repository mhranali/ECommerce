using ECommerce.Domain.Contracts;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.DataSeeding;
using ECommerce.Infrastructure.Identity.Data;
using ECommerce.Infrastructure.Identity.Entities;
using ECommerce.Infrastructure.Identity.Services;
using ECommerce.Infrastructure.Repositories;
using ECommerce.UseCases.Contracts;
using ECommerce.UseCases.Profiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace ECommerce.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StoreDbContext>(options => 
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddDbContext<StoreIdentityDbContext>(options => 
        {
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
        });


        services.AddKeyedScoped<IDataSeeder, CatalogDataSeeder>("Catalog");
        services.AddKeyedScoped<IDataSeeder, IdentityDataSeeder>("Identity");
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddSingleton<IConnectionMultiplexer>(opt =>
        {
            return ConnectionMultiplexer.Connect("localhost");
        });

        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddSingleton<ICacheRepository, CacheRepository>();

        services.AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<StoreIdentityDbContext>();

        services.AddScoped<IIdentityService, IdentityService>();
        return services;
    }
}
