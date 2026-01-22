using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using fridge_api.Data;

public class UserContextMiddleware
{
    private readonly RequestDelegate _next;

    public UserContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, FridgeDbContext db)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var auth0Sub = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            if (string.IsNullOrWhiteSpace(auth0Sub))
            {
                throw new UnauthorizedAccessException("Missing Auth0 subject claim.");
            }

            var appUser = await db.AppUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Auth0UserId == auth0Sub);

            if (appUser == null)
            {
                throw new UnauthorizedAccessException("User not registered in this app.");
            }
            
            context.Items["UserId"] = appUser.Id;
        }

        await _next(context);
    }
}