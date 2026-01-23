using fridge_api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace fridge_api.Modules.AIRecipeGeneration.Queries;

public class AiChatHistoryDto
{
    public int Id { get; set; }
    public string Role { get; set; } = "";
    public string? UserText { get; set; }
    public string? AIPayloadJson { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime ExpiresAtUtc { get; set; }
}

public class GetAiChatHistoryQuery
{
    private readonly FridgeDbContext _db;
    private readonly ILogger<GetAiChatHistoryQuery> _logger;

    public GetAiChatHistoryQuery(FridgeDbContext db, ILogger<GetAiChatHistoryQuery> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<IReadOnlyList<AiChatHistoryDto>> ExecuteAsync(Guid userId, CancellationToken ct)
    {
        _logger.LogInformation("Fetching AI chat history for user {UserId}", userId);
        var result = await _db.AiChatHistories
            .AsNoTracking()
            .Where(history => history.UserId == userId)
            .OrderByDescending(history => history.CreatedAtUtc)
            .Select(history => new AiChatHistoryDto
            {
                Id = history.Id,
                Role = history.Role,
                UserText = history.UserText,
                AIPayloadJson = history.AIPayloadJson,
                CreatedAtUtc = history.CreatedAtUtc,
                ExpiresAtUtc = history.ExpiresAtUtc,
            })
            .ToListAsync(ct);
        _logger.LogInformation("Fetched {Count} AI chat history items for user {UserId}", result.Count, userId);
        return result;
    }
}
