using fridge_api.Data;
using Microsoft.Extensions.Logging;

namespace fridge_api.Modules.CookingRecipe.Commands;

public class DeleteCookingRecipeCommand
{
    private readonly FridgeDbContext _db;
    private readonly ILogger<DeleteCookingRecipeCommand> _logger;

    public DeleteCookingRecipeCommand(FridgeDbContext db, ILogger<DeleteCookingRecipeCommand> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<bool> ExecuteAsync(int recipeId, CancellationToken ct)
    {
        _logger.LogInformation("Deleting cooking recipe {RecipeId}", recipeId);
        var recipe = await _db.CookingRecipes.FindAsync(new object[] { recipeId }, ct);
        if (recipe is null)
        {
            _logger.LogWarning("Cooking recipe {RecipeId} not found", recipeId);
            return false;
        }

        _db.CookingRecipes.Remove(recipe);
        await _db.SaveChangesAsync(ct);
        _logger.LogInformation("Deleted cooking recipe {RecipeId}", recipeId);
        return true;
    }
}
