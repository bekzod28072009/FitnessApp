using AutoMapper;
using FitnessApp.DataAccess.IRepository;
using FitnessApp.Domain.Entities.Nutrition;
using FitnessApp.Service.DTOs.MealsDto;
using FitnessApp.Service.Exceptions;
using FitnessApp.Service.Helpers;
using FitnessApp.Service.IService;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitnessApp.Service.Service;

public class MealService : IMealService
{
    private readonly IGenericRepository<Meal> repository;
    private readonly IGenericRepository<MealItem> mealItemRepository;
    private readonly IMapper mapper;

    public MealService(IGenericRepository<Meal> repository, IGenericRepository<MealItem> mealItemRepository, IMapper mapper)
    {
        this.repository = repository;
        this.mealItemRepository = mealItemRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<MealForViewDto>> GetAllAsync(Expression<Func<Meal, bool>> filter = null, string[] includes = null)
    {
        var meals = await repository.GetAll(filter, includes).ToListAsync();
        return mapper.Map<List<MealForViewDto>>(meals);
    }

    public async Task<MealForViewDto> GetAsync(Expression<Func<Meal, bool>> filter, string[] includes = null)
    {
        var meal = await repository.GetAsync(filter, includes)
            ?? throw new HttpStatusCodeException(404, "Meal not found");

        return mapper.Map<MealForViewDto>(meal);
    }

    public async Task<MealForViewDto> CreateAsync(MealForCreationDto dto)
    {
        var exists = await repository.GetAsync(p => p.Title == dto.Title);
        if (exists is not null)
            throw new HttpStatusCodeException(400, "Meal already exists");

        var meal = mapper.Map<Meal>(dto);
        meal.CreatedAt = DateTime.UtcNow;
        meal.CreatedBy = HttpContextHelper.UserId;

        // Automatically calculate totals if MealItems exist
        if (meal.MealItems != null && meal.MealItems.Any())
            await CalculateMealTotalsAsync(meal);

        await repository.CreateAsync(meal);
        await repository.SaveChangesAsync();

        return mapper.Map<MealForViewDto>(meal);
    }

    public async Task<MealForViewDto> UpdateAsync(Guid id, MealForUpdateDto dto)
    {
        var meal = await repository.GetAsync(m => m.Id == id, new[] { "MealItems", "MealItems.FoodItem" })
            ?? throw new HttpStatusCodeException(404, "Meal not found");

        if (!string.IsNullOrWhiteSpace(dto.Title))
            meal.Title = dto.Title;

        if (dto.Type.HasValue)
            meal.Type = dto.Type.Value;

        if (!string.IsNullOrWhiteSpace(dto.Notes))
            meal.Notes = dto.Notes;

        // Recalculate totals if requested
        await CalculateMealTotalsAsync(meal);

        meal.UpdatedAt = DateTime.UtcNow;
        meal.UpdatedBy = HttpContextHelper.UserId;

        repository.Update(meal);
        await repository.SaveChangesAsync();

        return mapper.Map<MealForViewDto>(meal);
    }

    public async Task<bool> DeleteAsync(Expression<Func<Meal, bool>> filter)
    {
        var meal = await repository.GetAsync(filter)
            ?? throw new HttpStatusCodeException(404, "Meal not found");

        meal.DeletedBy = HttpContextHelper.UserId;
        await repository.DeleteAsync(meal);
        await repository.SaveChangesAsync();

        return true;
    }

    // Helper: calculates totals using MealItems → FoodItems
    private async Task CalculateMealTotalsAsync(Meal meal)
    {
        var mealItems = await mealItemRepository
            .GetAll(mi => mi.MealId == meal.Id, new[] { "FoodItem" })
            .ToListAsync();

        meal.TotalCalories = 0;
        meal.TotalProteinGrams = 0;
        meal.TotalFatGrams = 0;
        meal.TotalCarbsGrams = 0;

        foreach (var item in mealItems)
        {
            if (item.FoodItem is null) continue;

            meal.TotalCalories += (int)(item.QuantityServings * item.FoodItem.CaloriesPerServing);
            meal.TotalProteinGrams += item.QuantityServings * item.FoodItem.ProteinPerServingGrams;
            meal.TotalFatGrams += item.QuantityServings * item.FoodItem.FatPerServingGrams;
            meal.TotalCarbsGrams += item.QuantityServings * item.FoodItem.CarbsPerServingGrams;
        }
    }
}
