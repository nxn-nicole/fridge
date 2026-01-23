using fridge_api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace fridge_api.Modules.Category.Commands;

public class DeleteCategoryRequest
{
    public required int CategoryId { get; set; }
    public required Guid UserId { get; set; }
}

public class DeleteCategoryCommand
{
    private readonly FridgeDbContext _db;
    private readonly ILogger<DeleteCategoryCommand> _logger;

    public DeleteCategoryCommand(FridgeDbContext db, ILogger<DeleteCategoryCommand> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<bool> ExecuteAsync(DeleteCategoryRequest request, CancellationToken ct)
    {
        var category = await _db.Categories
            .FirstOrDefaultAsync(
                item => item.Id == request.CategoryId && item.UserId == request.UserId,
                ct);

        if (category is null)
        {
            return false;
        }

        var recipes = await _db.CookingRecipes
            .Where(recipe => recipe.UserId == request.UserId && recipe.CategoryId == category.Id)
            .ToListAsync(ct);

        if (recipes.Count > 0)
        {
            foreach (var recipe in recipes)
            {
                recipe.CategoryId = null;
            }
        }

        _db.Categories.Remove(category);
        await _db.SaveChangesAsync(ct);
        _logger.LogInformation("Deleted category {CategoryId} for user {UserId}", category.Id, request.UserId);
        return true;
    }
}
