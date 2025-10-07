using FitnessApp.Domain.Enums;

namespace FitnessApp.Service.DTOs.MealsPlanDto;

public class MealPlanForUpdateDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public GoalType? Goal { get; set; }

    public bool? IsPublic { get; set; }

    public int? DailyCaloriesTarget { get; set; }
    public decimal? DailyProteinTargetGrams { get; set; }
    public decimal? DailyFatTargetGrams { get; set; }
    public decimal? DailyCarbsTargetGrams { get; set; }

    public int? DurationDays { get; set; }
}
