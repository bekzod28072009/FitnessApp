namespace FitnessApp.Service.DTOs.UserMealLogsDto;

public class UserMealLogForUpdateDto
{
    public DateTime? ConsumedAt { get; set; }

    public Guid? MealPlanId { get; set; }
    public Guid? MealId { get; set; }
    public Guid? FoodItemId { get; set; }

    public decimal? QuantityServings { get; set; }
    public string? Notes { get; set; }
}
