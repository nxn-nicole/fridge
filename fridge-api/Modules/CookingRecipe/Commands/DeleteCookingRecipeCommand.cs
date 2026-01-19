using fridge_api.Data;

namespace fridge_api.Modules.CookingRecipe.Commands;

public class DeleteCookingRecipeCommand
{
    private readonly FridgeDbContext _db;

    public DeleteCookingRecipeCommand(FridgeDbContext db)
    {
        _db = db;
    }

    public async Task<bool> ExecuteAsync(int recipeId, CancellationToken ct)
    {
        var recipe = await _db.CookingRecipes.FindAsync(new object[] { recipeId }, ct);
        if (recipe is null)
        {
            return false;
        }

        _db.CookingRecipes.Remove(recipe);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
