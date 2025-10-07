namespace FitnessApp.Service.DTOs.UserMealLogsDto;

public class UserMealLogForCreationDto
{
    public Guid UserId { get; set; }

    public DateTime ConsumedAt { get; set; } = DateTime.UtcNow;

    public Guid? MealPlanId { get; set; }
    public Guid? MealId { get; set; }
    public Guid? FoodItemId { get; set; }

    public decimal QuantityServings { get; set; }      // e.g. 1.0, 0.5, etc.
    public string? Notes { get; set; }
}
