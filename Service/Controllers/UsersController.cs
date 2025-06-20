using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Controllers.Contracts;
using TaskManager.Services;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace TaskManager.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private readonly UsersService _usersService;

    public UsersController(UsersService usersService)
    {
        _usersService = usersService;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
    [HttpPost("register", Name = nameof(RegisterUser))]
    public async Task<IActionResult> RegisterUser([FromBody, Required] RegisterUserRequest request, CancellationToken cancellationToken)
    {
        await _usersService.RegisterUser(request, cancellationToken);

        return Ok();
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
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

    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [HttpGet("signout", Name = nameof(AuthSignOut))]
    public async Task<IActionResult> AuthSignOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Ok();
    }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [HttpGet(Name = nameof(GetUser))]
    public async Task<ActionResult<UserResponse>> GetUser([FromQuery, Required] string login, CancellationToken token)
    {
        var user = await _usersService.GetUser(login, token);

        var userResponse = new UserResponse
        {
            Login = user.Login,
            Name = user.Name,
        };

        return Ok(userResponse);
    }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [HttpGet("current", Name = nameof(GetCurrentUser))]
    public async Task<ActionResult<UserResponse>> GetCurrentUser(CancellationToken token)
    {
        var user = await _usersService.GetUser(User, token);

        var userResponse = new UserResponse
        {
            Login = user.Login,
            Name = user.Name,
        };

        return Ok(userResponse);
    }
}
