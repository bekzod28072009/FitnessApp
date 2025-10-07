using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    TotalCalories = table.Column<int>(type: "integer", nullable: true),
                    TotalProteinGrams = table.Column<decimal>(type: "numeric", nullable: true),
                    TotalFatGrams = table.Column<decimal>(type: "numeric", nullable: true),
                    TotalCarbsGrams = table.Column<decimal>(type: "numeric", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MealId = table.Column<Guid>(type: "uuid", nullable: false),
                    FoodItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuantityServings = table.Column<decimal>(type: "numeric", nullable: false),
                    WeightGrams = table.Column<decimal>(type: "numeric", nullable: false),
                    Calories = table.Column<int>(type: "integer", nullable: true),
                    ProteinGrams = table.Column<decimal>(type: "numeric", nullable: true),
                    FatGrams = table.Column<decimal>(type: "numeric", nullable: true),
                    CarbsGrams = table.Column<decimal>(type: "numeric", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealItems_FoodItems_FoodItemId",
                        column: x => x.FoodItemId,
                        principalTable: "FoodItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealItems_Meal_MealId",
                        column: x => x.MealId,
                        principalTable: "Meal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MealItems_FoodItemId",
                table: "MealItems",
                column: "FoodItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MealItems_MealId",
                table: "MealItems",
                column: "MealId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MealItems");

            migrationBuilder.DropTable(
                name: "Meal");
        }
    }
}
