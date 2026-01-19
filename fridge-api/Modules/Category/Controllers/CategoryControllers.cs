using fridge_api.Modules.Category;
using fridge_api.Modules.Category.Commands;
using fridge_api.Modules.Category.Queries;
using Microsoft.AspNetCore.Mvc;

namespace fridge_api.Modules.Category.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly AddCategoryCommand _addCategoryCommand;
    private readonly GetAllCategoriesQuery _getAllCategoriesQuery;

    public CategoryController(AddCategoryCommand addCategoryCommand,GetAllCategoriesQuery getAllCategoriesQuery)
    {
        _addCategoryCommand = addCategoryCommand;
        _getAllCategoriesQuery = getAllCategoriesQuery;
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

        var result = await _addCategoryCommand.ExecuteAsync(request.Title, ct);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CategorySummary>>> GetAll(CancellationToken ct)
    {
        var result = await _getAllCategoriesQuery.ExecuteAsync(ct);
        return Ok(result);
    }
}



