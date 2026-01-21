using fridge_api.Modules.CookingRecipe.Commands;
using fridge_api.Modules.CookingRecipe.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fridge_api.Modules.CookingRecipe.Controllers;

[ApiController]
[Route("api/recipes")]
[Authorize]
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
        [FromBody] AddCookingRecipeDto newCookingRecipe,
        CancellationToken ct)
    {
        
        if (!HttpContext.Items.TryGetValue("UserId", out var userIdObj) || userIdObj is not Guid userId)
            throw new UnauthorizedAccessException("Could not find valid user id from Http Context");
        if (string.IsNullOrWhiteSpace(newCookingRecipe.Title))
        {
            return BadRequest("Title is required.");
        }

        var request = new AddCookingRecipeRequest
        {
            AddCookingRecipeDto = newCookingRecipe,
            UserId = userId
        };

        var result = await _addCookingRecipeCommand.ExecuteAsync(request, ct);
        return Ok(result);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCookingRecipe([FromRoute] int id, CancellationToken ct)
    {
        if (!HttpContext.Items.TryGetValue("UserId", out var userIdObj) || userIdObj is not Guid userId)
            throw new UnauthorizedAccessException("Could not find valid user id from Http Context");
        var request = new DeleteCookingRecipeRequest
        {
            RecipeId = id,
            UserId = userId,
        };
        var deleted = await _deleteCookingRecipeCommand.ExecuteAsync(request, ct);
        return deleted ? NoContent() : NotFound();
    }
}

[ApiController]
[Route("api/recipes/by-category")]
[Authorize]
public class GetRecipesByCategoryController : ControllerBase
{
    private readonly GetRecipesByCategoryQuery _query;

    public GetRecipesByCategoryController(GetRecipesByCategoryQuery query)
    {
        _query = query;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CookingRecipeDto>>> GetCookingRecipesByCategory(
        [FromQuery] int? categoryId,
        CancellationToken ct)
    {
        if (!HttpContext.Items.TryGetValue("UserId", out var userIdObj) || userIdObj is not Guid userId)
        throw new UnauthorizedAccessException("Could not find valid user id from Http Context");

        var request = new GetRecipesByCategoryRequest
        {
            CategoryId = categoryId,
            UserId = userId,
        };
        
        var result = await _query.ExecuteAsync(request, ct);
        return Ok(result);
    }
}

[ApiController]
[Route("api/recipes/search")]
[Authorize]
public class SearchRecipesController : ControllerBase
{
    private readonly SearchRecipesByTitleQuery _query;

    public SearchRecipesController(SearchRecipesByTitleQuery query)
    {
        _query = query;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CookingRecipeDto>>> SearchCookingRecipesByTitle(
        [FromQuery] string? title,
        CancellationToken ct)
    {
        if (!HttpContext.Items.TryGetValue("UserId", out var userIdObj) || userIdObj is not Guid userId)
            throw new UnauthorizedAccessException("Could not find valid user id from Http Context");

        var request = new SearchRecipesByTitleRequest
        {
            Title = title ?? string.Empty,
            UserId = userId
        };

        var result = await _query.ExecuteAsync(request, ct);
        return Ok(result);
    }
}
