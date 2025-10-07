namespace FitnessApp.Service.DTOs.UserMealLogsDto;

public class UserMealLogForViewDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public DateTime ConsumedAt { get; set; }

    public Guid? MealPlanId { get; set; }
    public Guid? MealId { get; set; }
    public Guid? FoodItemId { get; set; }

    public decimal QuantityServings { get; set; }

    public int Calories { get; set; }
    public decimal ProteinGrams { get; set; }
    public decimal FatGrams { get; set; }
    public decimal CarbsGrams { get; set; }

    public string? Notes { get; set; }
}
