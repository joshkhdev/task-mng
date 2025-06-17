using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Controllers.Contracts;
using TaskManager.Services;
using TaskManager.Utils.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UsersService _usersService;

    public UsersController(UsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpPost("register", Name = nameof(RegisterUser))]
    public async Task<IActionResult> RegisterUser([FromBody, Required] RegisterUserRequest request, CancellationToken cancellationToken)
    {
        await _usersService.RegisterUser(request, cancellationToken);

        return Ok();
    }

    [HttpPost("signin", Name = nameof(AuthSignIn))]
    public async Task<IActionResult> AuthSignIn([FromBody, Required] LoginUserRequest request, CancellationToken cancellationToken)
    {
        var claimsPrincipal = await _usersService.LoginUser(request, cancellationToken);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            claimsPrincipal,
            new AuthenticationProperties
            {
                IsPersistent = true,
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24),
            });

        return Ok();
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("signout", Name = nameof(AuthSignOut))]
    public async Task<IActionResult> AuthSignOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Ok();
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("user", Name = nameof(GetUser))]
    public async Task<ActionResult<UserResponse>> GetUser([FromBody, Required] string login, CancellationToken token)
    {
        var user = await _usersService.GetUser(login, token);

        if (user is null)
        {
            throw new RestNotFoundException("User not found");
        }

        var userResponse = new UserResponse
        {
            Login = user.Login,
            Name = user.Name,
        };

        return Ok(userResponse);
    }
}
