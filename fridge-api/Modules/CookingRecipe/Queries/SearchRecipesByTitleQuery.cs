using fridge_api.Data;
using fridge_api.Models;
using fridge_api.Modules.CookingRecipe.Dtos;
using Microsoft.EntityFrameworkCore;

namespace fridge_api.Modules.CookingRecipe.Queries;

public class SearchRecipesByTitleQuery
{
    private readonly FridgeDbContext _db;

    public SearchRecipesByTitleQuery(FridgeDbContext db)
    {
        _db = db;
    }

    public async Task<List<CookingRecipeDto>> ExecuteAsync(string? title, CancellationToken ct)
    {
        IQueryable<Models.CookingRecipe> query = _db.CookingRecipes.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(title))
        {
            query = query.Where(recipe => recipe.Title.Contains(title.Trim()));
        }

       

        return await query
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

