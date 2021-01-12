using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeBox.Migrations
{
    public partial class Ingredients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecipeIngredients",
                table: "Recipes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecipeInstructions",
                table: "Recipes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipeIngredients",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "RecipeInstructions",
                table: "Recipes");
        }
    }
}
