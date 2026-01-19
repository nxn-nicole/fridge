namespace fridge_api.Models;

public class RecipeCategory
{
    public int CookingRecipeId { get; set; }
    public CookingRecipe CookingRecipe { get; set; } = null!;

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}