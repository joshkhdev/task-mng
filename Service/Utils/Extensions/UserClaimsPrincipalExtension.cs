using System.Security.Claims;

namespace TaskManager.Utils.Extensions;

public static class UserClaimsPrincipalExtension
{
    public static string UserNameClaim = ClaimTypes.Name;

    public static string UserLoginClaim = ClaimTypes.NameIdentifier;

    public static string GetUserName(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(UserNameClaim) ?? "";
    }

    public static string GetUserLogin(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(UserLoginClaim) ?? "";
    }
}
