using ECommerce.UseCases.Common;
using ECommerce.UseCases.DTOs.Identity;

namespace ECommerce.UseCases.Contracts;

public interface IAuthenticationService
{
    Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default);
}
