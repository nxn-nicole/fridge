using fridge_api.Models;
using Microsoft.EntityFrameworkCore;

namespace fridge_api.Data;

public class FridgeDbContext: DbContext
{
    public FridgeDbContext(DbContextOptions<FridgeDbContext> options) : base(options)
    {
        
    }
    public DbSet<IngredientItem> IngredientItems { get; set; }
    public DbSet<CookingRecipe> CookingRecipes { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<AiChatHistory> AiChatHistories { get; set; } = null!;
    
    public DbSet<RecipeStep> RecipeSteps { get; set; } = null!;
    
    public DbSet<AppUser> AppUsers { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<AppUser>()
            .HasData(new AppUser
            {
                Id = new Guid("718ccb8f-ce52-4e51-8cfe-2a44cdca77d1"),
                Auth0UserId = "auth0|6970222c95b87153189ea217",
                Email = "fridgetest@gmail.com",
                Name = "fridgeTestAccount",
                PictureUrl ="https://s.gravatar.com/avatar/93e67547c4a33f19a557e2a3ddbe6c28?s=480&r=pg&d=https%3A%2F%2Fcdn.auth0.com%2Favatars%2Ffr.png",
                CreatedAt = new DateTime(2026, 1, 9, 14, 34, 27, 575, DateTimeKind.Utc),
                LastSeenAt = new DateTime(2026, 1, 9, 14, 33, 27, 955, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 9, 14, 34, 27, 575, DateTimeKind.Utc),
                
            });

        modelBuilder.Entity<IngredientItem>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Title).HasMaxLength(200).IsRequired();
        });
        

        modelBuilder.Entity<RecipeStep>()
            .HasIndex(x => new { x.CookingRecipeId, x.Order })
            .IsUnique();
    }
}
