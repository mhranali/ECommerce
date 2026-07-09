namespace ECommerce.Domain.Contracts;

public interface IDataSeeder
{
    Task SeedDataAsync(CancellationToken ct = default);
}
