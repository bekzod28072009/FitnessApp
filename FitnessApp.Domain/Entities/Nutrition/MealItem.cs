using FitnessApp.Domain.Common;

namespace FitnessApp.Domain.Entities.Nutrition;

public class MealItem : Auditable
{
    public Guid MealId { get; set; }
    public Meal? Meal { get; set; }

    public Guid FoodItemId { get; set; }
    public FoodItem? FoodItem { get; set; }

    public decimal QuantityServings { get; set; }   // e.g., 1.5 (servings)
    public decimal WeightGrams { get; set; }        // optional precise weight
    public int? Calories { get; set; }              // optional cached
    public decimal? ProteinGrams { get; set; }      // optional cached
    public decimal? FatGrams { get; set; }
    public decimal? CarbsGrams { get; set; }

    public int Order { get; set; }                  // meal item ordering
}
