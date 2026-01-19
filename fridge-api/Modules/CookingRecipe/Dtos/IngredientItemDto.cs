using fridge_api.Modules.CookingRecipe.Queries;

namespace fridge_api.Modules.CookingRecipe.Dtos;

public class IngredientItemDto
{
    public int Id { get; set; }
    public string Title {get; set;}= "";
    public string? Quantity {get; set;}
    
    public int CookingRecipeId { get; set; }
    
    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;
}