using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fridge_api.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Auth0UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastSeenAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "Auth0UserId", "CreatedAt", "Email", "LastSeenAt", "Name", "PictureUrl", "UpdatedAt" },
                values: new object[] { new Guid("718ccb8f-ce52-4e51-8cfe-2a44cdca77d1"), "auth0|6970222c95b87153189ea217", new DateTime(2026, 1, 9, 14, 34, 27, 575, DateTimeKind.Utc), "fridgetest@gmail.com", new DateTime(2026, 1, 9, 14, 33, 27, 955, DateTimeKind.Utc), "fridgeTestAccount", "https://s.gravatar.com/avatar/93e67547c4a33f19a557e2a3ddbe6c28?s=480&r=pg&d=https%3A%2F%2Fcdn.auth0.com%2Favatars%2Ffr.png", new DateTime(2026, 1, 9, 14, 34, 27, 575, DateTimeKind.Utc) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Categories");
        }
    }
}
