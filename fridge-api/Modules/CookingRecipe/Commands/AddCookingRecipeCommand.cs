using System.ComponentModel.DataAnnotations;
using fridge_api.Data;
using fridge_api.Models;
using fridge_api.Modules.CookingRecipe.Dtos;
using Microsoft.Extensions.Logging;

namespace fridge_api.Modules.CookingRecipe.Commands;

public class AddCookingRecipeRequest
{
    [Required] public required AddCookingRecipeDto AddCookingRecipeDto { get; init; }
    [Required] public Guid UserId { get; init; }
};


public class AddCookingRecipeCommand
{
    private readonly FridgeDbContext _db;
    private readonly ILogger<AddCookingRecipeCommand> _logger;

    public AddCookingRecipeCommand(FridgeDbContext db, ILogger<AddCookingRecipeCommand> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<string> ExecuteAsync(AddCookingRecipeRequest request, CancellationToken ct)
    {
        var addCookingRecipeItem = request.AddCookingRecipeDto;
        _logger.LogInformation("Adding cooking recipe with title '{Title}'", addCookingRecipeItem.Title);

        var recipe = new Models.CookingRecipe
        {
            Title = addCookingRecipeItem.Title.Trim(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            UserId = request.UserId,
        };
        
        recipe.CategoryId = addCookingRecipeItem.CategoryId;

        if (addCookingRecipeItem.Ingredients is { Count: > 0 })
        {
            foreach (var ingredient in addCookingRecipeItem.Ingredients)
            {
                if (string.IsNullOrWhiteSpace(ingredient.Title))
                {
                    continue;
                }

                recipe.Ingredients.Add(new IngredientItem
                {
                    Title = ingredient.Title.Trim(),
                    Quantity = ingredient.Quantity?.Trim(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });
            }
        }

        if (addCookingRecipeItem.Steps is { Count: > 0 })
        {
            
            foreach (var step in addCookingRecipeItem.Steps)
            {
                if (string.IsNullOrWhiteSpace(step.Description))
                {
                    continue;
                }

                

                recipe.Steps.Add(new RecipeStep
                {
                    Order = step.Order,
                    Description = step.Description.Trim()
                });
            }
        }

        if (addCookingRecipeItem.PictureUrls is { Count: > 0 })
        {
            foreach (var url in addCookingRecipeItem.PictureUrls)
            {
                if (string.IsNullOrWhiteSpace(url))
                {
                    continue;
                }

                recipe.Pictures.Add(new RecipePicture
                {
                    Url = url.Trim()
                });
            }
        }

        _db.CookingRecipes.Add(recipe);
        await _db.SaveChangesAsync(ct);
        _logger.LogInformation("Added cooking recipe with id {RecipeId}", recipe.Id);

        return "Added cooking recipe successfully";
    }
}

public class AddCookingRecipeDto 
{
    public required string Title { get; init; }
    public int? CategoryId { get; init; }
    
    public IReadOnlyList<IngredientInputDto> Ingredients { get; init; } = new List<IngredientInputDto>();
    public IReadOnlyList<StepInputDto> Steps { get; init; } = new List<StepInputDto>();
    
    public IReadOnlyList<string>? PictureUrls { get; init; }

}

public class IngredientInputDto
{
    public required string Title { get; init; }
    public string? Quantity { get; init; }
}

public class StepInputDto
{
    public int Order { get; init; }
    public string? Description { get; init; }
}

