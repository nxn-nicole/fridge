namespace fridge_api.Models;

public class Category
{
    public int Id { get; set; }

    public string Title { get; set; } = "";
    
    public string? Color { get; set; } = "";
    
    public required Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;
    
}
