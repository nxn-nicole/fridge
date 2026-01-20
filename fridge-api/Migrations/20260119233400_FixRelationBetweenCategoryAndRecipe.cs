using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fridge_api.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationBetweenCategoryAndRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeCategories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "CookingRecipes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CookingRecipes_CategoryId",
                table: "CookingRecipes",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CookingRecipes_Categories_CategoryId",
                table: "CookingRecipes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CookingRecipes_Categories_CategoryId",
                table: "CookingRecipes");

            migrationBuilder.DropIndex(
                name: "IX_CookingRecipes_CategoryId",
                table: "CookingRecipes");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "CookingRecipes");

            migrationBuilder.CreateTable(
                name: "RecipeCategories",
                columns: table => new
                {
                    CookingRecipeId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeCategories", x => new { x.CookingRecipeId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_RecipeCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeCategories_CookingRecipes_CookingRecipeId",
                        column: x => x.CookingRecipeId,
                        principalTable: "CookingRecipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeCategories_CategoryId",
                table: "RecipeCategories",
                column: "CategoryId");
        }
    }
}
