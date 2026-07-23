using ECommerce.UseCases.Common;
using ECommerce.UseCases.Contracts;
using ECommerce.UseCases.DTOs.Identity;

namespace ECommerce.UseCases.Services;

internal class AuthenticationService(IIdentityService identityService) : IAuthenticationService
{
    private readonly IIdentityService identityService = identityService;

    public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default)
    {
        // get user by email
       var userResult = await identityService.FindUserByEmailAsync(loginDto.Email);

        if (!userResult.IsSuccess)
            return Result<UserDto>.Fail(userResult.Errors);

        //check password

        var passwordResult = await identityService.CheckPasswordAsync(loginDto.Email, loginDto.Password, ct);

        if(!passwordResult.IsSuccess)
            return Result<UserDto>.Fail(userResult.Errors);
            
        if(!passwordResult.Data)
            return Result<UserDto>.Fail(Error.Unauthorized("Invalid Email or Password"));

        return new UserDto
        {
            Email = loginDto.Email,
            DisplayName = userResult.Data.DisplayName,
            Token = "Token"
        };

    }
}
