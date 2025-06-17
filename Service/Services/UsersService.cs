using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TaskManager.Controllers.Contracts;
using TaskManager.Models;
using TaskManager.Utils;
using TaskManager.Utils.Exceptions;

namespace TaskManager.Services;

public class UsersService
{
    private readonly TaskManagerDbContext _context;

    public UsersService(TaskManagerDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUser(string login, CancellationToken token)
    {
        return await _context.Users.FindAsync(login, token);
    }

    public async Task<ClaimsPrincipal> LoginUser(LoginUserRequest request, CancellationToken token)
    {
        var user = await GetUser(request.Login, token);

        if (user is null)
        {
            throw new RestUnauthorizedException("Invalid credentials");
        }

        if (!PasswordHelper.VerifyPassword(request.Password, user.Password))
        {
            throw new RestUnauthorizedException("Invalid credentials");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.NameIdentifier, user.Login),
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
            throw new RestUnauthorizedException("User already registered");
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
