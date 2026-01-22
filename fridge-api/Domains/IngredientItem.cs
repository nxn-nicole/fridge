namespace fridge_api.Models;

public class IngredientItem
{
    public int Id { get; set; }
    public string Title {get; set;}= "";
    public string? Quantity {get; set;}
    
    public int CookingRecipeId { get; set; }
    public CookingRecipe CookingRecipe { get; set; } = null!;
    
    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;
}