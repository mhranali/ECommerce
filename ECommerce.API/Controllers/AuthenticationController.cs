using ECommerce.UseCases.Contracts;
using ECommerce.UseCases.DTOs.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

public class AuthenticationController(IAuthenticationService authenticationService) : ApiBaseController
{
    private readonly IAuthenticationService authenticationService = authenticationService;

    [HttpPost("Login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        return ToActionResult(await authenticationService.LoginAsync(loginDto));

    }
}
