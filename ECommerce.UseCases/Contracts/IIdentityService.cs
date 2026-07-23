using ECommerce.UseCases.Common;

namespace ECommerce.UseCases.Contracts;

public interface IIdentityService
{
    Task<Result<IdentityUserResult>> FindUserByEmailAsync(string email, CancellationToken ct = default);

    Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken ct = default);
}
