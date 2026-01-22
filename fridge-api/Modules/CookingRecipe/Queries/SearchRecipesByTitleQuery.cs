using System.ComponentModel.DataAnnotations;
using fridge_api.Data;
using fridge_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace fridge_api.Modules.CookingRecipe.Queries;

public class SearchRecipesByTitleRequest
{
    public string Title { get; set; }
    [Required] public Guid UserId { get; set; }
}

public class SearchRecipesByTitleQuery
{
    private readonly FridgeDbContext _db;
    private readonly ILogger<SearchRecipesByTitleQuery> _logger;

    public SearchRecipesByTitleQuery(FridgeDbContext db, ILogger<SearchRecipesByTitleQuery> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<List<CookingRecipeSummaryDto>> ExecuteAsync(SearchRecipesByTitleRequest request, CancellationToken ct)
    {
        _logger.LogInformation("Searching recipes for user {UserId} by title '{Title}'", request.UserId, request.Title);
        IQueryable<Models.CookingRecipe> query = _db.CookingRecipes.AsNoTracking()
            .Where(recipe => recipe.UserId == request.UserId);

        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            query = query.Where(recipe => recipe.Title.Contains(request.Title.Trim()));
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
            "Search by title '{Title}' for user {UserId} returned {Count} recipes",
            request.Title,
            request.UserId,
            result.Count);
        return result;

    }
}

public class CookingRecipeSummaryDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public int? CategoryId { get; set; }
    public string? PictureUrl { get; set; }

}
