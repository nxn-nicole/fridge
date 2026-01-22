using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fridge_api.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "CookingRecipes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CookingRecipes_UserId",
                table: "CookingRecipes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserId",
                table: "Categories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AppUsers_UserId",
                table: "Categories",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CookingRecipes_AppUsers_UserId",
                table: "CookingRecipes",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AppUsers_UserId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_CookingRecipes_AppUsers_UserId",
                table: "CookingRecipes");

            migrationBuilder.DropIndex(
                name: "IX_CookingRecipes_UserId",
                table: "CookingRecipes");

            migrationBuilder.DropIndex(
                name: "IX_Categories_UserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CookingRecipes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Categories");
        }
    }
}
