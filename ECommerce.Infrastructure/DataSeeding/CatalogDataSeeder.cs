using ECommerce.Domain.Common;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Products;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ECommerce.Infrastructure.DataSeeding;

internal class CatalogDataSeeder(StoreDbContext dbContext, ILogger<CatalogDataSeeder> logger) : IDataSeeder
{
    public async Task SeedDataAsync(CancellationToken ct = default)
    {
        try 
        {
            var pendingMigration = await dbContext.Database.GetPendingMigrationsAsync(ct);
            if (pendingMigration.Any())
                await dbContext.Database.MigrateAsync(ct);

            var seedRoot = Path.Combine(AppContext.BaseDirectory, "DataSeed");

            await SeedIfEmptyAsync<ProductBrand, int>(seedRoot, "brands.json", ct);
            await SeedIfEmptyAsync<ProductType, int>(seedRoot, "types.json", ct);
            await SeedIfEmptyAsync<Product, int>(seedRoot, "products.json", ct);

            var result = await dbContext.SaveChangesAsync();

            if (result > 0)
                logger.LogInformation($"{result} Rows Added");
            else
                logger.LogInformation("Database Already Seeded");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database");
        }
    }

    private async Task SeedIfEmptyAsync<T, TKey>(string rootPath,string fileName, CancellationToken ct = default) where T : BaseEntity<TKey>
    {
        if (await dbContext.Set<T>().AnyAsync())
        {
            logger.LogInformation("Table already has data");
            return;
        }

        var filePath = Path.Combine(rootPath, fileName);

        if(!File.Exists(filePath)) 
        {
            logger.LogInformation($"file {fileName} is not exists");
            return;
        }

        using var fileStream = File.OpenRead(filePath);

        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        var items = await JsonSerializer.DeserializeAsync<List<T>>(fileStream, options, ct);

        if (items?.Any() ?? false)
            dbContext.Set<T>().AddRange(items);
    }
}

