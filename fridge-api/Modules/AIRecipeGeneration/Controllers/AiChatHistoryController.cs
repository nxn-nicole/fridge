using fridge_api.Modules.AIRecipeGeneration.Commands;
using fridge_api.Modules.AIRecipeGeneration.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fridge_api.Modules.AIRecipeGeneration.Controllers;

[ApiController]
[Route("api/airecipe/history")]
[Authorize]
public class AiChatHistoryController : ControllerBase
{
    private readonly AddAiChatHistoryCommand _addAiChatHistoryCommand;
    private readonly GetAiChatHistoryQuery _getAiChatHistoryQuery;

    public AiChatHistoryController(
        AddAiChatHistoryCommand addAiChatHistoryCommand,
        GetAiChatHistoryQuery getAiChatHistoryQuery)
    {
        _addAiChatHistoryCommand = addAiChatHistoryCommand;
        _getAiChatHistoryQuery = getAiChatHistoryQuery;
    }

    [HttpPost]
    public async Task<ActionResult<int>> AddHistory(
        [FromBody] AiChatMessageDto message,
        CancellationToken ct)
    {
        if (!HttpContext.Items.TryGetValue("UserId", out var userIdObj) || userIdObj is not Guid userId)
            throw new UnauthorizedAccessException("Could not find valid user id from Http Context");

        if (message is null || string.IsNullOrWhiteSpace(message.Message))
        {
            return BadRequest("Message is required.");
        }

        var id = await _addAiChatHistoryCommand.ExecuteAsync(
            new AddAiChatHistoryRequest
            {
                UserId = userId,
                Message = message,
            },
            ct);

        return Ok(id);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AiChatHistoryDto>>> GetHistory(CancellationToken ct)
    {
        if (!HttpContext.Items.TryGetValue("UserId", out var userIdObj) || userIdObj is not Guid userId)
            throw new UnauthorizedAccessException("Could not find valid user id from Http Context");

        var result = await _getAiChatHistoryQuery.ExecuteAsync(userId, ct);
        return Ok(result);
    }
}
