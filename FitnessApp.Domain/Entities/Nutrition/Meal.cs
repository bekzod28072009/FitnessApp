using FitnessApp.Domain.Common;
using FitnessApp.Domain.Enums;

namespace FitnessApp.Domain.Entities.Nutrition;

public class Meal : Auditable
{
    public string Title { get; set; } = default!;            // e.g., "Breakfast 1"
    public MealType Type { get; set; }                       // enum: Breakfast, Lunch, etc.
    public string? Notes { get; set; }

    // relationship
    public ICollection<MealItem> MealItems { get; set; } = new List<MealItem>();

    // Optional stored aggregates (service can compute)
    public int? TotalCalories { get; set; }
    public decimal? TotalProteinGrams { get; set; }
    public decimal? TotalFatGrams { get; set; }
    public decimal? TotalCarbsGrams { get; set; }
}
