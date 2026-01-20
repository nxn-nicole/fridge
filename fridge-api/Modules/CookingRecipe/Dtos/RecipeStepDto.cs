namespace fridge_api.Modules.CookingRecipe.Dtos;

public class RecipeStepDto
{
    public int Id { get; set; }

    public int Order { get; set; }

    public string Description { get; set; } = "";

    public int CookingRecipeId { get; set; }
  
}