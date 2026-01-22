using System.ComponentModel.DataAnnotations;
using fridge_api.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace fridge_api.Modules.CookingRecipe.Commands;

public class DeleteCookingRecipeRequest
{
    [Required] public int RecipeId { get; set; }
    [Required] public Guid UserId { get; set; }
}

public class DeleteCookingRecipeCommand
{
    private readonly FridgeDbContext _db;
    private readonly ILogger<DeleteCookingRecipeCommand> _logger;

    public DeleteCookingRecipeCommand(FridgeDbContext db, ILogger<DeleteCookingRecipeCommand> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<bool> ExecuteAsync(DeleteCookingRecipeRequest deleteCookingRecipeRequest, CancellationToken ct)
    {
        _logger.LogInformation("Deleting cooking recipe {RecipeId}", deleteCookingRecipeRequest.RecipeId);
        var recipe = await _db.CookingRecipes.FindAsync(new object[] { deleteCookingRecipeRequest.RecipeId }, ct);
        if (recipe is null)
        {
            _logger.LogWarning("Cooking recipe {RecipeId} not found", deleteCookingRecipeRequest.RecipeId);
            return false;
        }

        _db.CookingRecipes.Remove(recipe);
        await _db.SaveChangesAsync(ct);
        _logger.LogInformation("Deleted cooking recipe {RecipeId}", deleteCookingRecipeRequest.RecipeId);
        return true;
    }
}
