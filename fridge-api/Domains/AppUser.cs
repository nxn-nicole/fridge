namespace fridge_api.Models;

public class AppUser
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Auth0UserId { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? PictureUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime LastSeenAt { get; set; }
}