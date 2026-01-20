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
    public async Task<ActionResult<string>> AddCategory(
        [FromBody] AddCategoryRequest request,
        CancellationToken ct)
    {
        if (request is null || string.IsNullOrWhiteSpace(request.AddCategoryDto.Title))
        {
            return BadRequest("Title is required.");
        }

        var result = await _addCategoryCommand.ExecuteAsync(request, ct);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetAllCategories(CancellationToken ct)
    {
        var result = await _getAllCategoriesQuery.ExecuteAsync(ct);
        return Ok(result);
    }
}



