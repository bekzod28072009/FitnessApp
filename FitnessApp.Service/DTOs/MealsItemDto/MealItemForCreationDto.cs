namespace FitnessApp.Service.DTOs.MealsItemDto;

public class MealItemForCreationDto
{
    public Guid MealId { get; set; }
    public Guid FoodItemId { get; set; }
    public decimal QuantityServings { get; set; }
    public decimal? WeightGrams { get; set; }

    public int Order { get; set; } = 0;
}
