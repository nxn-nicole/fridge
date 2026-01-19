namespace fridge_api.Modules.CookingRecipe.Dtos;

public class RecipePictureDto
{
    public int Id { get; set; }

    public string Url { get; set; } = "";

    public int CookingRecipeId { get; set; }
}