using AutoMapper;
using FitnessApp.DataAccess.IRepository;
using FitnessApp.Domain.Entities.Nutrition;
using FitnessApp.Service.DTOs.UserMealLogsDto;
using FitnessApp.Service.Exceptions;
using FitnessApp.Service.Helpers;
using FitnessApp.Service.IService;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitnessApp.Service.Service;

public class UserMealLogService : IUserMealLogService
{
    private readonly IGenericRepository<UserMealLog> userMealLogRepository;
    private readonly IGenericRepository<FoodItem> foodItemRepository;
    private readonly IGenericRepository<Meal> mealRepository;
    private readonly IMapper mapper;

    public UserMealLogService(IGenericRepository<UserMealLog> userMealLogRepository,
            IGenericRepository<FoodItem> foodItemRepository,
            IGenericRepository<Meal> mealRepository,
            IMapper mapper)
    {
        this.userMealLogRepository = userMealLogRepository;
        this.foodItemRepository = foodItemRepository;
        this.mealRepository = mealRepository;
        this.mapper = mapper;
    }

    // ===============================
    // 🔹 GET ALL
    // ===============================
    public async Task<IEnumerable<UserMealLogForViewDto>> GetAllAsync(
        Expression<Func<UserMealLog, bool>> filter = null,
        string[] includes = null)
    {
        var logs = await userMealLogRepository.GetAll(filter, includes).ToListAsync();
        return mapper.Map<IEnumerable<UserMealLogForViewDto>>(logs);
    }

    // ===============================
    // 🔹 GET BY ID / FILTER
    // ===============================
    public async Task<UserMealLogForViewDto> GetAsync(
        Expression<Func<UserMealLog, bool>> filter,
        string[] includes = null)
    {
        var log = await userMealLogRepository.GetAsync(filter, includes)
            ?? throw new HttpStatusCodeException(404, "User meal log not found");

        return mapper.Map<UserMealLogForViewDto>(log);
    }

    // ===============================
    // 🔹 CREATE
    // ===============================
    public async Task<UserMealLogForViewDto> CreateAsync(UserMealLogForCreationDto dto)
    {
        var exists = await userMealLogRepository.GetAsync(p => p.UserId == dto.UserId);
        if (exists is not null)
            throw new HttpStatusCodeException(400, "User Meal Log already exists");

        var log = mapper.Map<UserMealLog>(dto);

        log.CreatedAt = DateTime.UtcNow;
        log.CreatedBy = HttpContextHelper.UserId;

        // Auto-calculate nutrition
        await CalculateNutritionAsync(log);

        await userMealLogRepository.CreateAsync(log);
        await userMealLogRepository.SaveChangesAsync();

        return mapper.Map<UserMealLogForViewDto>(log);
    }

    // ===============================
    // 🔹 UPDATE
    // ===============================
    public async Task<UserMealLogForViewDto> UpdateAsync(Guid id, UserMealLogForUpdateDto dto)
    {
        var log = await userMealLogRepository.GetAsync(l => l.Id == id)
            ?? throw new HttpStatusCodeException(404, "User meal log not found");

        if (dto.ConsumedAt.HasValue)
            log.ConsumedAt = dto.ConsumedAt.Value;

        if (dto.MealPlanId.HasValue)
            log.MealPlanId = dto.MealPlanId.Value;

        if (dto.MealId.HasValue)
            log.MealId = dto.MealId.Value;

        if (dto.FoodItemId.HasValue)
            log.FoodItemId = dto.FoodItemId.Value;

        if (dto.QuantityServings.HasValue)
            log.QuantityServings = dto.QuantityServings.Value;

        if (!string.IsNullOrWhiteSpace(dto.Notes))
            log.Notes = dto.Notes;

        log.UpdatedAt = DateTime.UtcNow;
        log.UpdatedBy = HttpContextHelper.UserId;

        // Recalculate nutrition values if relevant fields changed
        await CalculateNutritionAsync(log);

        userMealLogRepository.Update(log);
        await userMealLogRepository.SaveChangesAsync();

        return mapper.Map<UserMealLogForViewDto>(log);
    }

    // ===============================
    // 🔹 DELETE
    // ===============================
    public async Task<bool> DeleteAsync(Expression<Func<UserMealLog, bool>> filter)
    {
        var log = await userMealLogRepository.GetAsync(filter)
            ?? throw new HttpStatusCodeException(404, "User meal log not found");

        log.DeletedBy = HttpContextHelper.UserId;

        await userMealLogRepository.DeleteAsync(log);
        await userMealLogRepository.SaveChangesAsync();

        return true;
    }

    // ===============================
    // ⚡ PRIVATE METHOD — Auto Calculation
    // ===============================
    private async Task CalculateNutritionAsync(UserMealLog log)
    {
        log.Calories = 0;
        log.ProteinGrams = 0;
        log.FatGrams = 0;
        log.CarbsGrams = 0;

        // If FoodItem directly logged
        if (log.FoodItemId.HasValue)
        {
            var food = await foodItemRepository.GetAsync(f => f.Id == log.FoodItemId.Value)
                ?? throw new HttpStatusCodeException(404, "Food item not found");

            log.Calories = (int)(food.CaloriesPerServing * log.QuantityServings);
            log.ProteinGrams = food.ProteinPerServingGrams * log.QuantityServings;
            log.FatGrams = food.FatPerServingGrams * log.QuantityServings;
            log.CarbsGrams = food.CarbsPerServingGrams * log.QuantityServings;
        }

        // If Meal is logged
        else if (log.MealId.HasValue)
        {
            var meal = await mealRepository.GetAsync(m => m.Id == log.MealId.Value, new[] { "MealItems.FoodItem" })
                ?? throw new HttpStatusCodeException(404, "Meal not found");

            foreach (var item in meal.MealItems)
            {
                if (item.FoodItem == null) continue;

                log.Calories += (int)(item.QuantityServings * item.FoodItem.CaloriesPerServing);
                log.ProteinGrams += item.QuantityServings * item.FoodItem.ProteinPerServingGrams;
                log.FatGrams += item.QuantityServings * item.FoodItem.FatPerServingGrams;
                log.CarbsGrams += item.QuantityServings * item.FoodItem.CarbsPerServingGrams;
            }

            // Scale by QuantityServings if partial meal eaten
            log.Calories = (int)(log.Calories * log.QuantityServings);
            log.ProteinGrams *= log.QuantityServings;
            log.FatGrams *= log.QuantityServings;
            log.CarbsGrams *= log.QuantityServings;
        }
    }
}
