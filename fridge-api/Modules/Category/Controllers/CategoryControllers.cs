using fridge_api.Modules.Category;
using fridge_api.Modules.Category.Commands;
using fridge_api.Modules.Category.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fridge_api.Modules.Category.Controllers;

[ApiController]
[Route("api/categories")]
[Authorize]
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
        [FromBody] AddCategoryDto newCategory,
        CancellationToken ct)
    {
        if (!HttpContext.Items.TryGetValue("UserId", out var userIdObj) || userIdObj is not Guid userId)
            throw new UnauthorizedAccessException("Could not find valid user id from Http Context");
        
        if (string.IsNullOrWhiteSpace(newCategory.Title))
        {
            return BadRequest("Title is required.");
        }

        var request = new AddCategoryRequest
        {
            AddCategoryDto = newCategory,
            UserId = userId,
        };

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



