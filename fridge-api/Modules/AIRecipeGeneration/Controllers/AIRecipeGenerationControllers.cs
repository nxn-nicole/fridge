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
        [FromBody] RawRecipeTextDto request,
        CancellationToken ct)
    {
        if (request is null || string.IsNullOrWhiteSpace(request.Text))
        {
            return BadRequest("Text is required.");
        }

        var result = await _service.Generate(request, ct);
        return Ok(result);
    }
}
