using FitnessApp.Domain.Enums;

namespace FitnessApp.Service.DTOs.MealsDto;

public class MealForUpdateDto
{
    public string? Title { get; set; }
    public MealType? Type { get; set; }
    public string? Notes { get; set; }


    public int? TotalCalories { get; set; }
    public decimal? TotalProteinGrams { get; set; }
    public decimal? TotalFatGrams { get; set; }
    public decimal? TotalCarbsGrams { get; set; }
}
