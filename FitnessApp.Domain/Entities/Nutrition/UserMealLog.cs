using FitnessApp.Domain.Common;

namespace FitnessApp.Domain.Entities.Nutrition;

public class UserMealLog : Auditable
{
    public Guid UserId { get; set; }
    // public User? User { get; set; }   // FK to User

    public DateTime ConsumedAt { get; set; }           // vaqt
    public Guid? MealPlanId { get; set; }              // agar plan bo'lsa
    public Guid? MealId { get; set; }                  // agar konkret meal
    public Guid? FoodItemId { get; set; }              // agar single food item

    public decimal QuantityServings { get; set; }      // 1.0, 0.5, ...
    public int Calories { get; set; }                  // computed at log time
    public decimal ProteinGrams { get; set; }
    public decimal FatGrams { get; set; }
    public decimal CarbsGrams { get; set; }
    public string? Notes { get; set; }

}
