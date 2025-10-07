namespace FitnessApp.Service.DTOs.FoodItemsDto;

public class FoodItemForUpdateDto
{
    public string? Name { get; set; }
    public string? Brand { get; set; }
    public string? Description { get; set; }
    public int? CaloriesPerServing { get; set; }
    public decimal? ProteinPerServingGrams { get; set; }
    public decimal? FatPerServingGrams { get; set; }
    public decimal? CarbsPerServingGrams { get; set; }
    public decimal? ServingSize { get; set; }
    public string? ServingUnit { get; set; }
    public bool? IsCustom { get; set; }

}
