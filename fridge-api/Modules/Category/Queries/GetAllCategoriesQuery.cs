using fridge_api.Data;
using fridge_api.Modules.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace fridge_api.Modules.Category.Queries;

public class GetAllCategoriesQuery
{
    private readonly FridgeDbContext _db;
    private readonly ILogger<GetAllCategoriesQuery> _logger;

    public GetAllCategoriesQuery(FridgeDbContext db, ILogger<GetAllCategoriesQuery> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<IReadOnlyList<CategoryDto>> ExecuteAsync(CancellationToken ct)
    {
        _logger.LogInformation("Start getting all cooking recipes");
        var result = await _db.Categories
            .AsNoTracking()
            .OrderBy(category => category.Title)
            .Select(category => new CategoryDto
            {
                Id = category.Id,
                Title = category.Title,
                Color = category.Color,
            })
            .ToListAsync(ct);
        _logger.LogInformation("Fetched {Count} categories", result.Count);
        return result;
    }
}

public class CategoryDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Color { get; set; }
}
