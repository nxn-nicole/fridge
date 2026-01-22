using System.ComponentModel.DataAnnotations;
using fridge_api.Data;
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

    public async Task<IReadOnlyList<CookingRecipeSummaryDto>> ExecuteAsync(GetRecipesByCategoryRequest request, CancellationToken ct)
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
            .Select(recipe => new CookingRecipeSummaryDto
            {
                Id = recipe.Id,
                Title = recipe.Title,
                CategoryId = recipe.CategoryId,
                PictureUrl = recipe.Pictures
                    .OrderBy(p => p.Id)
                    .Select(p => p.Url)
                    .FirstOrDefault()
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
