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
    public DbSet<RecipeCategory> RecipeCategories { get; set; } = null!;
    public DbSet<RecipeStep> RecipeSteps { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IngredientItem>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Title).HasMaxLength(200).IsRequired();
        });
        
        modelBuilder.Entity<RecipeCategory>()
            .HasKey(x => new { x.CookingRecipeId, x.CategoryId });

        modelBuilder.Entity<RecipeStep>()
            .HasIndex(x => new { x.CookingRecipeId, x.Order })
            .IsUnique();
    }
}