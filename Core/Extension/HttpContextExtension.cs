using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Core.Extension;

public static class HttpContextExtension
{
    public static string? GetUserId(this HttpContext context)
    {
        if (context.User.Identity is ClaimsIdentity identity)
        {
            var userClaims = identity.Claims;

            return userClaims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
        }
        
        return null;
    }
}