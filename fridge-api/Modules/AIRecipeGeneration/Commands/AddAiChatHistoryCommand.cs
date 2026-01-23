using fridge_api.Data;
using fridge_api.Models;
using Microsoft.Extensions.Logging;

namespace fridge_api.Modules.AIRecipeGeneration.Commands;

public class AddAiChatHistoryRequest
{
    public required Guid UserId { get; set; }
    public required AiChatMessageDto Message { get; set; }
}

public class AiChatMessageDto
{
    public required string Role { get; set; }
    public required string Message { get; set; }
}

public class AddAiChatHistoryCommand
{
    private readonly FridgeDbContext _db;
    private readonly ILogger<AddAiChatHistoryCommand> _logger;

    public AddAiChatHistoryCommand(FridgeDbContext db, ILogger<AddAiChatHistoryCommand> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<int> ExecuteAsync(AddAiChatHistoryRequest request, CancellationToken ct)
    {
        var role = request.Message.Role.Trim().ToLowerInvariant();
        var history = new AiChatHistory
        {
            UserId = request.UserId,
            Role = role,
        };

        if (role == "user")
        {
            history.UserText = request.Message.Message;
        }
        else if (role == "assistant")
        {
            history.AIPayloadJson = request.Message.Message;
        }
        else
        {
            throw new ArgumentException("Role must be 'user' or 'assistant'.", nameof(request.Message.Role));
        }

        _db.Add(history);
        await _db.SaveChangesAsync(ct);
        _logger.LogInformation("Saved AI chat history {HistoryId} for user {UserId}", history.Id, request.UserId);

        return history.Id;
    }
}
