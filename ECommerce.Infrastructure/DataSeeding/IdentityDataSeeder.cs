using ECommerce.Domain.Contracts;
using ECommerce.Infrastructure.Identity.Data;
using ECommerce.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.Infrastructure.DataSeeding;

internal class IdentityDataSeeder(StoreIdentityDbContext dbContext,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager,
    ILogger<IdentityDataSeeder> logger) : IDataSeeder
{
    private readonly StoreIdentityDbContext dbContext = dbContext;
    private readonly UserManager<ApplicationUser> userManager = userManager;
    private readonly RoleManager<IdentityRole> roleManager = roleManager;
    private readonly ILogger<IdentityDataSeeder> logger = logger;

    public async Task SeedDataAsync(CancellationToken ct = default)
    {
        try
        {
            var pindingMigrations = await dbContext.Database.GetPendingMigrationsAsync(ct);
            if (pindingMigrations.Any())
                await dbContext.Database.MigrateAsync(ct);

            if (!await roleManager.Roles.AnyAsync())
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }

            if (!await userManager.Users.AnyAsync())
            {
                var admin = new ApplicationUser
                {
                    DisplayName = "Mhran",
                    Email = "mhranali@gmil.com",
                    UserName = "mhranali31",
                    PhoneNumber = "01011834828"
                };

                var createUser = await userManager.CreateAsync(admin, "Admin@123");

                if (createUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "SuperAdmin");
                }
                else
                {
                    var errors = string.Join(',', createUser.Errors.Select(e => e.Description));
                    logger.LogWarning($"can not seed default admin {errors}");
                }
            }
        }
        catch (Exception ex)
        {

            logger.LogError(ex, "Identity data seeder faild");
            return;
        }
    }
}
