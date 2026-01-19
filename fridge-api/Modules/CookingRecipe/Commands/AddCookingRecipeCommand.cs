using System.ComponentModel.DataAnnotations;
using fridge_api.Data;
using fridge_api.Models;
using fridge_api.Modules.CookingRecipe.Dtos;

namespace fridge_api.Modules.CookingRecipe.Commands;

public sealed record AddCookingRecipeRequest
{
    [Required] public required AddCookingReciptDto AddCookingReciptDto;
};


public class AddCookingRecipeCommand
{
    private readonly FridgeDbContext _db;

    public AddCookingRecipeCommand(FridgeDbContext db)
    {
        _db = db;
    }

    public async Task<string> ExecuteAsync(AddCookingRecipeRequest request, CancellationToken ct)
    {
        var addCookingReciptItem = request.AddCookingReciptDto;

        var recipe = new Models.CookingRecipe
        {
            Title = addCookingReciptItem.Title.Trim(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
        
        recipe.CategoryId = addCookingReciptItem.CategoryId;

        if (addCookingReciptItem.Ingredients is { Count: > 0 })
        {
            foreach (var ingredient in addCookingReciptItem.Ingredients)
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

        if (addCookingReciptItem.Steps is { Count: > 0 })
        {
            
            foreach (var step in addCookingReciptItem.Steps)
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

        if (addCookingReciptItem.PictureUrls is { Count: > 0 })
        {
            foreach (var url in addCookingReciptItem.PictureUrls)
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

        return "Added cooking recipe successfully";
    }
}

public class AddCookingReciptDto 
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


