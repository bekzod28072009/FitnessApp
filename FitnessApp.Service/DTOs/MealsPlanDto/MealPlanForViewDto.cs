using FitnessApp.Domain.Enums;
using FitnessApp.Service.DTOs.MealsDto;

namespace FitnessApp.Service.DTOs.MealsPlanDto;

public class MealPlanForViewDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public GoalType Goal { get; set; }

    public bool IsPublic { get; set; }

    public int? DailyCaloriesTarget { get; set; }
    public decimal? DailyProteinTargetGrams { get; set; }
    public decimal? DailyFatTargetGrams { get; set; }
    public decimal? DailyCarbsTargetGrams { get; set; }

    public int DurationDays { get; set; }

    // Linked meals
    public ICollection<MealForViewDto>? Meals { get; set; }
}
