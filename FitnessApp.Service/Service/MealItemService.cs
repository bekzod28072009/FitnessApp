using AutoMapper;
using FitnessApp.DataAccess.IRepository;
using FitnessApp.Domain.Entities.Nutrition;
using FitnessApp.Service.DTOs.MealsItemDto;
using FitnessApp.Service.Exceptions;
using FitnessApp.Service.Helpers;
using FitnessApp.Service.IService;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitnessApp.Service.Service;

public class MealItemService : IMealItemService
{
    private readonly IGenericRepository<MealItem> repository;
    private readonly IGenericRepository<FoodItem> foodItemRepository;
    private readonly IGenericRepository<Meal> mealRepository;
    private readonly IMapper mapper;

    public MealItemService(IGenericRepository<MealItem> repository,
        IGenericRepository<FoodItem> foodItemRepository,
        IGenericRepository<Meal> mealRepository,
        IMapper mapper)
    {
        this.repository = repository;
        this.foodItemRepository = foodItemRepository;
        this.mealRepository = mealRepository;
        this.mapper = mapper;
    }


    public async Task<IEnumerable<MealItemForViewDto>> GetAllAsync(Expression<Func<MealItem, bool>> filter = null, string[] includes = null)
    {
        var items = await repository.GetAll(filter, includes).ToListAsync();
        return mapper.Map<List<MealItemForViewDto>>(items);
    }

    public async Task<MealItemForViewDto> GetAsync(Expression<Func<MealItem, bool>> filter, string[] includes = null)
    {
        var item = await repository.GetAsync(filter, includes)
            ?? throw new HttpStatusCodeException(404, "MealItem not found");

        return mapper.Map<MealItemForViewDto>(item);
    }

    public async Task<MealItemForViewDto> CreateAsync(MealItemForCreationDto dto)
    {
        var food = await foodItemRepository.GetAsync(f => f.Id == dto.FoodItemId)
            ?? throw new HttpStatusCodeException(400, "FoodItem already exists");

        var item = mapper.Map<MealItem>(dto);

        // Auto-calculate nutrition per quantity
        item.Calories = (int)(food.CaloriesPerServing * dto.QuantityServings);
        item.ProteinGrams = food.ProteinPerServingGrams * dto.QuantityServings;
        item.FatGrams = food.FatPerServingGrams * dto.QuantityServings;
        item.CarbsGrams = food.CarbsPerServingGrams * dto.QuantityServings;

        item.CreatedAt = DateTime.UtcNow;
        item.CreatedBy = HttpContextHelper.UserId;

        await repository.CreateAsync(item);
        await repository.SaveChangesAsync();

        // 🔄 Update meal totals
        await RecalculateMealTotalsAsync(item.MealId);

        return mapper.Map<MealItemForViewDto>(item);
    }

    public async Task<MealItemForViewDto> UpdateAsync(Guid id, MealItemForUpdateDto dto)
    {
        var item = await repository.GetAsync(i => i.Id == id, new[] { "FoodItem" })
            ?? throw new HttpStatusCodeException(404, "MealItem not found");

        if (dto.QuantityServings.HasValue)
            item.QuantityServings = dto.QuantityServings.Value;

        if (dto.WeightGrams.HasValue)
            item.WeightGrams = dto.WeightGrams.Value;

        if (dto.Order.HasValue)
            item.Order = dto.Order.Value;

        // Recalculate values
        if (item.FoodItem != null)
        {
            item.Calories = (int)(item.FoodItem.CaloriesPerServing * item.QuantityServings);
            item.ProteinGrams = item.FoodItem.ProteinPerServingGrams * item.QuantityServings;
            item.FatGrams = item.FoodItem.FatPerServingGrams * item.QuantityServings;
            item.CarbsGrams = item.FoodItem.CarbsPerServingGrams * item.QuantityServings;
        }

        item.UpdatedAt = DateTime.UtcNow;
        item.UpdatedBy = HttpContextHelper.UserId;

        repository.Update(item);
        await repository.SaveChangesAsync();

        // 🔄 Update meal totals
        await RecalculateMealTotalsAsync(item.MealId);

        return mapper.Map<MealItemForViewDto>(item);
    }

    public async Task<bool> DeleteAsync(Expression<Func<MealItem, bool>> filter)
    {
        var item = await repository.GetAsync(filter)
            ?? throw new HttpStatusCodeException(404, "MealItem not found");

        var mealId = item.MealId;

        item.DeletedBy = HttpContextHelper.UserId;
        await repository.DeleteAsync(item);
        await repository.SaveChangesAsync();

        // 🔄 Update meal totals after deletion
        await RecalculateMealTotalsAsync(mealId);

        return true;
    }

    // 🔢 Helper: Recalculate parent meal totals
    private async Task RecalculateMealTotalsAsync(Guid mealId)
    {
        var meal = await mealRepository.GetAsync(m => m.Id == mealId);
        if (meal == null) return;

        var mealItems = await repository.GetAll(mi => mi.MealId == mealId, new[] { "FoodItem" }).ToListAsync();

        meal.TotalCalories = 0;
        meal.TotalProteinGrams = 0;
        meal.TotalFatGrams = 0;
        meal.TotalCarbsGrams = 0;

        foreach (var item in mealItems)
        {
            if (item.FoodItem == null) continue;

            meal.TotalCalories += (int)(item.QuantityServings * item.FoodItem.CaloriesPerServing);
            meal.TotalProteinGrams += item.QuantityServings * item.FoodItem.ProteinPerServingGrams;
            meal.TotalFatGrams += item.QuantityServings * item.FoodItem.FatPerServingGrams;
            meal.TotalCarbsGrams += item.QuantityServings * item.FoodItem.CarbsPerServingGrams;
        }

        meal.UpdatedAt = DateTime.UtcNow;
        meal.UpdatedBy = HttpContextHelper.UserId;

        mealRepository.Update(meal);
        await mealRepository.SaveChangesAsync();
    }

}
