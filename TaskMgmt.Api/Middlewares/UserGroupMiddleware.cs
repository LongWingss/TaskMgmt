using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace TaskMgmt.Api.Middlewares;

public class UserGroupMiddleware
{
    private readonly RequestDelegate _next;

    public UserGroupMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User != null)
        {
            var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "UserId");

            if (userIdClaim != null)
            {
                var claimsIdentity = new ClaimsIdentity(new[] { userIdClaim });
                await context.SignInAsync(new ClaimsPrincipal(claimsIdentity));
            }
        }
        await _next(context);
    }

}
