using FitnessApp.Domain.Common;

namespace FitnessApp.Domain.Entities.Nutrition;

public class FoodItem : Auditable
{
    public string Name { get; set; } = default!;
    public string? Brand { get; set; }
    public string? Description { get; set; }

    public int CaloriesPerServing { get; set; }         // kcal per serving
    public decimal ProteinPerServingGrams { get; set; } // grams
    public decimal FatPerServingGrams { get; set; }     // grams
    public decimal CarbsPerServingGrams { get; set; }   // grams

    public decimal ServingSize { get; set; }            // numeric (e.g., 100)
    public string ServingUnit { get; set; } = "g";      // "g", "ml", "piece"
    public bool IsCustom { get; set; } = false;         // user-defined item
}
