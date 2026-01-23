namespace fridge_api.Models;

public class AiChatHistory
{
    public int Id { get; set; }

    public required Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;

    public string Role { get; set; } = "";
    public string? UserText { get; set; }
    public string? AIPayloadJson { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAtUtc => CreatedAtUtc.AddDays(2);
}
