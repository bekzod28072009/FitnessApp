using FitnessApp.Domain.Enums;
using FitnessApp.Service.DTOs.MealsItemDto;

namespace FitnessApp.Service.DTOs.MealsDto;

public class MealForViewDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public MealType Type { get; set; }
    public string? Notes { get; set; }

    public int? TotalCalories { get; set; }
    public decimal? TotalProteinGrams { get; set; }
    public decimal? TotalFatGrams { get; set; }
    public decimal? TotalCarbsGrams { get; set; }

    // Related data
    public ICollection<MealItemForViewDto>? MealItems { get; set; }
}
