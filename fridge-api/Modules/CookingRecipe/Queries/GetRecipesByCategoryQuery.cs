using System.ComponentModel.DataAnnotations;
using fridge_api.Data;
using fridge_api.Modules.CookingRecipe.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace fridge_api.Modules.CookingRecipe.Queries;

public class GetRecipesByCategoryRequest
{
    public int? CategoryId { get; set; } 
    [Required] public Guid UserId { get; set; }
}

public class GetRecipesByCategoryQuery
{
    private readonly FridgeDbContext _db;
    private readonly ILogger<GetRecipesByCategoryQuery> _logger;

    public GetRecipesByCategoryQuery(FridgeDbContext db, ILogger<GetRecipesByCategoryQuery> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<IReadOnlyList<CookingRecipeDto>> ExecuteAsync(GetRecipesByCategoryRequest request, CancellationToken ct)
    {
        _logger.LogInformation("Fetching recipes for user {UserId} by category {CategoryId}", request.UserId, request.CategoryId);
        var query = _db.CookingRecipes.AsNoTracking()
            .Where(recipe => recipe.UserId == request.UserId);

        if (request.CategoryId is not null)
        {
            query = query.Where(recipe => recipe.CategoryId == request.CategoryId);
        }

        var result = await query
            .OrderBy(recipe => recipe.Title)
            .Select(recipe => new CookingRecipeDto
            {
                Id = recipe.Id,
                Title = recipe.Title,
                CategoryId = recipe.CategoryId,
                CreatedAt = recipe.CreatedAt,
                UpdatedAt = recipe.UpdatedAt,

                Ingredients = recipe.Ingredients
                    .Select(i => new IngredientItemDto
                    {
                        Id = i.Id,
                        Title = i.Title,
                        Quantity = i.Quantity
                    })
                    .ToList(),

                Steps = recipe.Steps
                    .Select(s => new RecipeStepDto
                    {
                        Id = s.Id,
                        Order = s.Order,
                        Description = s.Description
                    })
                    .ToList(),

                Pictures = recipe.Pictures
                    .Select(p => new RecipePictureDto
                    {
                        Id = p.Id,
                        Url = p.Url
                    })
                    .ToList()
            })
            .ToListAsync(ct);
        _logger.LogInformation(
            "Fetched {Count} recipes for user {UserId} and category {CategoryId}",
            result.Count,
            request.UserId,
            request.CategoryId);
        return result;
    }
}
