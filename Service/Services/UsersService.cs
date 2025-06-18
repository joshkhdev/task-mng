using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TaskManager.Controllers.Contracts;
using TaskManager.Models;
using TaskManager.Utils;
using TaskManager.Utils.Exceptions;
using TaskManager.Utils.Extensions;

namespace TaskManager.Services;

public class UsersService
{
    private readonly TaskManagerDbContext _context;

    public UsersService(TaskManagerDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUser(string login, CancellationToken token)
    {
        return await _context.Users.FindAsync(login, token)
            ?? throw new RestNotFoundException("User not found");
    }

    public async ValueTask<User> GetUser(ClaimsPrincipal user, CancellationToken token)
    {
        var login = user.GetUserLogin()
            ?? throw new RestUnauthorizedException("Unable to read user login");

        return await GetUser(login, token);
    }

    public async Task<ClaimsPrincipal> LoginUser(LoginUserRequest request, CancellationToken token)
    {
        var user = await _context.Users.FindAsync(request.Login, token);

        if (user is null || !PasswordHelper.VerifyPassword(request.Password, user.Password))
        {
            throw new RestUnauthorizedException("Invalid credentials");
        }

        var claims = new List<Claim>
        {
            new Claim(UserClaimsPrincipalExtension.UserNameClaim, user.Name),
            new Claim(UserClaimsPrincipalExtension.UserLoginClaim, user.Login),
        };

        var claimsPrincipal = new ClaimsPrincipal(
            new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)
        );

        return claimsPrincipal;
    }

    public async Task RegisterUser(RegisterUserRequest request, CancellationToken token)
    {
        var userExists = await _context.Users.AnyAsync(user => user.Login == request.Login, token);

        if (userExists)
        {
            throw new RestConflictException("User already registered");
        }

        var password = PasswordHelper.GetPasswordHash(request.Password);

        var user = new User()
        {
            Login = request.Login,
            Password = password,
            Name = request.Name,
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(token);
    }
}
