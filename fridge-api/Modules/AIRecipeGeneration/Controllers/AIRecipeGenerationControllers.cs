using fridge_api.Modules.AIRecipeGeneration.Services;
using Microsoft.AspNetCore.Mvc;

namespace fridge_api.Modules.AIRecipeGeneration.Controllers;

[ApiController]
[Route("api/airecipe")]
public class AIRecipeGenerationControllers : ControllerBase
{
    private readonly AIRecipeGenerationService _service;

    public AIRecipeGenerationControllers(AIRecipeGenerationService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<AIGeneratedRecipe>> Generate(
        [FromBody] string rawUserMessage,
        CancellationToken ct)
    {
        if (!HttpContext.Items.TryGetValue("UserId", out var userIdObj) || userIdObj is not Guid userId)
            throw new UnauthorizedAccessException("Could not find valid user id from Http Context");

        if (string.IsNullOrWhiteSpace(rawUserMessage))
        {
            return BadRequest("Text is required.");
        }

        var result = await _service.Generate(
            new AiGenerateRecipeRequest
            {
                UserId = userId,
                RawUserMessage = rawUserMessage,
            },
            ct);
        return Ok(result);
    }
}
