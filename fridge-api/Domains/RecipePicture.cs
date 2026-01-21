namespace fridge_api.Models;

public class RecipePicture
{
    public int Id { get; set; }

    public string Url { get; set; } = "";

    public int CookingRecipeId { get; set; }
    public CookingRecipe CookingRecipe { get; set; } = null!;
}