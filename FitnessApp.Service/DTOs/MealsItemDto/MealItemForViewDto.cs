namespace FitnessApp.Service.DTOs.MealsItemDto;

public class MealItemForViewDto
{
    public Guid Id { get; set; }
    public Guid FoodItemId { get; set; }
    public string FoodItemName { get; set; } = default!;

    public decimal QuantityServings { get; set; }
    public decimal? WeightGrams { get; set; }

    public int? Calories { get; set; }
    public decimal? ProteinGrams { get; set; }
    public decimal? FatGrams { get; set; }
    public decimal? CarbsGrams { get; set; }

    public int Order { get; set; }
}
