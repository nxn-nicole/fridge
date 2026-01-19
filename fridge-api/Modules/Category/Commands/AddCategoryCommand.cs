using fridge_api.Data;
using fridge_api.Modules.Category;

namespace fridge_api.Modules.Category.Commands;

public class AddCategoryCommand
{
    private readonly FridgeDbContext _db;

    public AddCategoryCommand(FridgeDbContext db)
    {
        _db = db;
    }

    public async Task<CategorySummary> ExecuteAsync(string title, CancellationToken ct)
    {
        var normalizedTitle = title.Trim();

        var category = new Models.Category
        {
            Title = normalizedTitle
        };

        _db.Categories.Add(category);
        await _db.SaveChangesAsync(ct);

        return new CategorySummary(category.Id, category.Title);
    }
}
