using FitnessApp.Domain.Enums;

namespace FitnessApp.Service.DTOs.MealsPlanDto;

public class MealPlanForCreationDto
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public GoalType Goal { get; set; } // WeightLoss, MuscleGain, Maintain

    public bool IsPublic { get; set; } = false;

    public int? DailyCaloriesTarget { get; set; }
    public decimal? DailyProteinTargetGrams { get; set; }
    public decimal? DailyFatTargetGrams { get; set; }
    public decimal? DailyCarbsTargetGrams { get; set; }

    public int DurationDays { get; set; } = 7;
}
