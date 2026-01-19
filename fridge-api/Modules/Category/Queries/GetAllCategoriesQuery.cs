using fridge_api.Data;
using fridge_api.Modules.Category;
using Microsoft.EntityFrameworkCore;

namespace fridge_api.Modules.Category.Queries;

public class GetAllCategoriesQuery
{
    private readonly FridgeDbContext _db;

    public GetAllCategoriesQuery(FridgeDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<CategorySummary>> ExecuteAsync(CancellationToken ct)
    {
        return await _db.Categories
            .AsNoTracking()
            .OrderBy(category => category.Title)
            .Select(category => new CategorySummary(category.Id, category.Title))
            .ToListAsync(ct);
    }
}
