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
    private readonly DeleteCategoryCommand _deleteCategoryCommand;

    public CategoryController(
        AddCategoryCommand addCategoryCommand,
        GetAllCategoriesQuery getAllCategoriesQuery,
        DeleteCategoryCommand deleteCategoryCommand)
    {
        _addCategoryCommand = addCategoryCommand;
        _getAllCategoriesQuery = getAllCategoriesQuery;
        _deleteCategoryCommand = deleteCategoryCommand;
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
        if (!HttpContext.Items.TryGetValue("UserId", out var userIdObj) || userIdObj is not Guid userId)
            throw new UnauthorizedAccessException("Could not find valid user id from Http Context");

        var result = await _getAllCategoriesQuery.ExecuteAsync(userId, ct);
        return Ok(result);
    }

    [HttpDelete("{categoryId:int}")]
    public async Task<IActionResult> DeleteCategory(int categoryId, CancellationToken ct)
    {
        if (!HttpContext.Items.TryGetValue("UserId", out var userIdObj) || userIdObj is not Guid userId)
            throw new UnauthorizedAccessException("Could not find valid user id from Http Context");

        var deleted = await _deleteCategoryCommand.ExecuteAsync(
            new DeleteCategoryRequest
            {
                CategoryId = categoryId,
                UserId = userId,
            },
            ct);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}

