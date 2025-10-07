using FitnessApp.Domain.Enums;

namespace FitnessApp.Service.DTOs.MealsDto;

public class MealForCreationDto
{
    public string Title { get; set; } = default!;
    public MealType Type { get; set; }
    public string? Notes { get; set; }
}
