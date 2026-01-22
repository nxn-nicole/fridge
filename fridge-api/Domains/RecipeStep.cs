namespace fridge_api.Models;

public class RecipeStep
{
    public int Id { get; set; }

    public int Order { get; set; }

    public string Description { get; set; } = "";

    public int CookingRecipeId { get; set; }
    public CookingRecipe CookingRecipe { get; set; } = null!;
}
