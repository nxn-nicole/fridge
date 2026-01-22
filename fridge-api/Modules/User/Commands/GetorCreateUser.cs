using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json;
using fridge_api.Data;
using fridge_api.Models;
using Microsoft.EntityFrameworkCore;

namespace fridge_api.Modules.User.Commands;

public sealed record GetorCreateUserCommand(JsonElement User);

public sealed class GetorCreateUserResult
{
    public required Guid Id { get; init; }
    public required string Auth0UserId { get; init; }
}

public class GetorCreateUser(FridgeDbContext db, ILogger<GetorCreateUser> logger)
{
    public async Task<GetorCreateUserResult> GetOrCreate(GetorCreateUserCommand getorCreateUserCommand,
        CancellationToken ct)
    {
        logger.LogInformation("GetOrCreateUser at {Time}", DateTime.UtcNow);
        var user = getorCreateUserCommand.User;
        
        var auth0UserId = GetFromUser(user, "user_id");
        var email =
            GetFromUser(user, "email") ??
            GetFromUserMetadata(user, "user_email");
        var name = GetFromUser(user, "name") ??
                   email;
        var pictureUrl = GetFromUser(user, "picture");
        var createdAtStr = GetFromUser(user, "created_at")!;
                
        var lastSeenAt = DateTime.UtcNow;
        
        if (string.IsNullOrWhiteSpace(auth0UserId))
        {
            logger.LogError(
                "Invalid user payload: missing user_id or email. user_id: {Auth0UserId}, email: {Email}",
                auth0UserId, email);
            throw new ValidationException("user_id and email are required");
        }

        logger.LogInformation("Looking up existing AppUser for Auth0UserId: {Auth0UserId}",
            auth0UserId);
        
        var existingUser = await db.AppUsers.SingleOrDefaultAsync(u => u.Auth0UserId == auth0UserId, ct);
        if (existingUser == null)
        {
            var newUser = new AppUser
            {
                Auth0UserId = auth0UserId,
                Email = email,
                Name = name,
                PictureUrl = pictureUrl,
                LastSeenAt = lastSeenAt,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            db.AppUsers.Add(newUser);
            await db.SaveChangesAsync(ct);
            logger.LogInformation("Created new AppUser: {User}", newUser);
            return new GetorCreateUserResult { Id = newUser.Id, Auth0UserId = newUser.Auth0UserId };
        }
        existingUser.Email = email;
        existingUser.Name = name;
        existingUser.PictureUrl = pictureUrl;
        existingUser.LastSeenAt = DateTime.UtcNow;
        logger.LogInformation("Persisting updates to AppUser: {User}", existingUser);
        await db.SaveChangesAsync(ct);
        
        return new GetorCreateUserResult { Id = existingUser.Id, Auth0UserId = existingUser.Auth0UserId };

    }
    
    private string? Get(JsonElement source, string name)
    {
        return source.TryGetProperty(name, out var el) && el.ValueKind != JsonValueKind.Null
            ? el.GetString()
            : null;
    }

    private string? GetFromUser(JsonElement user, string name)
    {
        return Get(user, name);
    }

    private string? GetFromUserMetadata(JsonElement user, string name)
    {
        if (user.TryGetProperty("user_metadata", out var meta) &&
            meta.ValueKind == JsonValueKind.Object)
            return Get(meta, name);

        return null;
    }
    
}