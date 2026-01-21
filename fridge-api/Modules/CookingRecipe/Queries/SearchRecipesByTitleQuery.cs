using System.ComponentModel.DataAnnotations;
using fridge_api.Data;
using fridge_api.Models;
using fridge_api.Modules.CookingRecipe.Dtos;
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

    public async Task<List<CookingRecipeDto>> ExecuteAsync(SearchRecipesByTitleRequest request, CancellationToken ct)
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
            "Search by title '{Title}' for user {UserId} returned {Count} recipes",
            request.Title,
            request.UserId,
            result.Count);
        return result;

    }
}

public class CookingRecipeDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public List<IngredientItemDto> Ingredients { get; set; } = new List<IngredientItemDto>();

    public List<RecipeStepDto> Steps { get; set; } = new List<RecipeStepDto>();
    public int? CategoryId { get; set; }
    public List<RecipePictureDto> Pictures { get; set; } = new List<RecipePictureDto>();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
