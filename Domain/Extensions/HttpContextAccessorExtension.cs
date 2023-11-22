using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Domain.Extensions;

public static class HttpContextAccessorExtension
{
    public static string UserId(this IHttpContextAccessor httpContextAccessor)
    {
        var identity = httpContextAccessor?.HttpContext?.User.Identity as ClaimsIdentity;
        var userId = identity?.FindFirst(ClaimTypes.Sid)?.Value;

        return userId ?? String.Empty;
    }
}
