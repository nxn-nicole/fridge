using fridge_api.Modules.Category;
using fridge_api.Modules.Category.Commands;
using fridge_api.Modules.Category.Queries;
using Microsoft.AspNetCore.Mvc;

namespace fridge_api.Modules.Category.Controllers;

[ApiController]
[Route("api/categories")]
public class AddCategoryController : ControllerBase
{
    private readonly AddCategoryCommand _command;

    public AddCategoryController(AddCategoryCommand command)
    {
        _command = command;
    }

    [HttpPost]
    public async Task<ActionResult<CategorySummary>> AddCategory(
        [FromBody] AddCategoryRequest request,
        CancellationToken ct)
    {
        if (request is null || string.IsNullOrWhiteSpace(request.Title))
        {
            return BadRequest("Title is required.");
        }

        var result = await _command.ExecuteAsync(request.Title, ct);
        return Ok(result);
    }
}

public sealed record AddCategoryRequest(string Title);

[ApiController]
[Route("api/categories")]
public class GetAllCategoriesController : ControllerBase
{
    private readonly GetAllCategoriesQuery _query;

    public GetAllCategoriesController(GetAllCategoriesQuery query)
    {
        _query = query;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CategorySummary>>> GetAll(CancellationToken ct)
    {
        var result = await _query.ExecuteAsync(ct);
        return Ok(result);
    }
}
