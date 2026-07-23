using ECommerce.Infrastructure.Identity.Entities;
using ECommerce.UseCases.Common;
using ECommerce.UseCases.Contracts;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Infrastructure.Identity.Services;

public class IdentityService(UserManager<ApplicationUser> userManager) : IIdentityService
{
    private readonly UserManager<ApplicationUser> userManager = userManager;

    public async Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken ct = default)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
            return Result<bool>.Fail(Error.NotFound("user not found", $"user with email {email} is not found"));

        else
            return await userManager.CheckPasswordAsync(user, password);
    }

    public async Task<Result<IdentityUserResult>> FindUserByEmailAsync(string email, CancellationToken ct = default)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
            return Result<IdentityUserResult>.Fail(Error.NotFound("user not found", $"user with email {email} is not found"));

        else
            return Result<IdentityUserResult>.Ok(new IdentityUserResult(user.Id, user.Email, user.DisplayName, user.UserName));
    }
}
