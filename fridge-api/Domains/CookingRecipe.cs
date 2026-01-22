namespace fridge_api.Models;

public class CookingRecipe
{
    public int Id { get; set; }

    public string Title { get; set; } = "";

    public ICollection<IngredientItem> Ingredients { get; set; } = new List<IngredientItem>();

    public ICollection<RecipeStep> Steps { get; set; } = new List<RecipeStep>();

    public int? CategoryId { get; set; }
    public Category? Category { get; set; }

    public ICollection<RecipePicture> Pictures { get; set; } = new List<RecipePicture>();
    
    public required Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
