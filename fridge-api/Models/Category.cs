namespace fridge_api.Models;

public class Category
{
    public int Id { get; set; }

    public string Title { get; set; } = "";

    public ICollection<RecipeCategory> Recipes { get; set; } = new List<RecipeCategory>();
}
