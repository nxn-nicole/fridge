using fridge_api.Data;
using fridge_api.Modules.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace fridge_api.Modules.Category.Commands;

public class AddCategoryRequest
{
    public required AddCategoryDto AddCategoryDto { get; set; }
    public required Guid UserId { get; set; }
}

public class AddCategoryCommand
{
    private readonly FridgeDbContext _db;
    private readonly ILogger<AddCategoryCommand> _logger;

    public AddCategoryCommand(FridgeDbContext db, ILogger<AddCategoryCommand> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<string> ExecuteAsync(AddCategoryRequest addCategoryRequest, CancellationToken ct)
    {
        var addCategoryDto = addCategoryRequest.AddCategoryDto;
        var normalizedTitle = addCategoryDto.Title.Trim();
        _logger.LogInformation("Adding category with title '{Title}'", normalizedTitle);

        var category = new Models.Category
        {
            Title = normalizedTitle,
            Color = addCategoryDto.Color,
            UserId = addCategoryRequest.UserId,
        };

        _db.Categories.Add(category);
        await _db.SaveChangesAsync(ct);
        _logger.LogInformation("Added category with id {CategoryId}", category.Id);

        return "Added category successfully.";
    }
}

public class AddCategoryDto
{
    public required string Title { get; init; }
    public string? Color { get; init; }
}
