using FitnessApp.Domain.Common;
using FitnessApp.Domain.Enums;

namespace FitnessApp.Domain.Entities.Nutrition;

public class MealPlan : Auditable
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public GoalType Goal { get; set; }              // WeightLoss, MuscleGain, Maintain

    public bool IsPublic { get; set; } = false;     // visible to other users

    public int? DailyCaloriesTarget { get; set; }
    public decimal? DailyProteinTargetGrams { get; set; }
    public decimal? DailyFatTargetGrams { get; set; }
    public decimal? DailyCarbsTargetGrams { get; set; }

    public ICollection<Meal> Meals { get; set; } = new List<Meal>();
    public int DurationDays { get; set; } = 7;      // optional length
}
