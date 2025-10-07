using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserMealLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ConsumedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MealPlanId = table.Column<Guid>(type: "uuid", nullable: true),
                    MealId = table.Column<Guid>(type: "uuid", nullable: true),
                    FoodItemId = table.Column<Guid>(type: "uuid", nullable: true),
                    QuantityServings = table.Column<decimal>(type: "numeric", nullable: false),
                    Calories = table.Column<int>(type: "integer", nullable: false),
                    ProteinGrams = table.Column<decimal>(type: "numeric", nullable: false),
                    FatGrams = table.Column<decimal>(type: "numeric", nullable: false),
                    CarbsGrams = table.Column<decimal>(type: "numeric", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMealLogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMealLogs");
        }
    }
}
