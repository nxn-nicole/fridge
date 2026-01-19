using fridge_api.Modules.CookingRecipe.Commands;
using fridge_api.Modules.CookingRecipe.Queries;
using Microsoft.AspNetCore.Mvc;

namespace fridge_api.Modules.CookingRecipe.Controllers;

[ApiController]
[Route("api/recipes")]
public class CookingRecipeController : ControllerBase
{
    private readonly AddCookingRecipeCommand _addCookingRecipeCommand;
    private readonly DeleteCookingRecipeCommand _deleteCookingRecipeCommand;
    
    public CookingRecipeController(AddCookingRecipeCommand addCookingRecipeCommand,DeleteCookingRecipeCommand deleteCookingRecipeCommand)
    {
        _addCookingRecipeCommand = addCookingRecipeCommand;
        _deleteCookingRecipeCommand = deleteCookingRecipeCommand;
    }

    [HttpPost]
    public async Task<ActionResult<string>> AddCookingRecipe(
        [FromBody] AddCookingRecipeRequest request,
        CancellationToken ct)
    {
        if (request is null || string.IsNullOrWhiteSpace(request.AddCookingReciptDto.Title))
        {
            return BadRequest("Title is required.");
        }

        var result = await _addCookingRecipeCommand.ExecuteAsync(request, ct);
        return Ok(result);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCookingRecipe([FromRoute] int id, CancellationToken ct)
    {
        var deleted = await _deleteCookingRecipeCommand.ExecuteAsync(id, ct);
        return deleted ? NoContent() : NotFound();
    }
}

[ApiController]
[Route("api/recipes/by-category")]
public class GetRecipesByCategoryController : ControllerBase
{
    private readonly GetRecipesByCategoryQuery _query;

    public GetRecipesByCategoryController(GetRecipesByCategoryQuery query)
    {
        _query = query;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CookingRecipeDto>>> GetCookingReciptsByCategory(
        [FromQuery] int? categoryId,
        CancellationToken ct)
    {
        var result = await _query.ExecuteAsync(categoryId, ct);
        return Ok(result);
    }
}

[ApiController]
[Route("api/recipes/search")]
public class SearchRecipesByTitleController : ControllerBase
{
    private readonly SearchRecipesByTitleQuery _query;

    public SearchRecipesByTitleController(SearchRecipesByTitleQuery query)
    {
        _query = query;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CookingRecipeDto>>> SearchCookingRecipt(
        [FromQuery] string? title,
        CancellationToken ct)
    {
        var result = await _query.ExecuteAsync(title, ct);
        return Ok(result);
    }
}

