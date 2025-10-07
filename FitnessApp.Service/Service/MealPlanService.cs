using AutoMapper;
using FitnessApp.DataAccess.IRepository;
using FitnessApp.Domain.Entities.Nutrition;
using FitnessApp.Service.DTOs.MealsPlanDto;
using FitnessApp.Service.Exceptions;
using FitnessApp.Service.Helpers;
using FitnessApp.Service.IService;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitnessApp.Service.Service;

public class MealPlanService : IMealPlanService
{
    private readonly IGenericRepository<MealPlan> repository;
    private readonly IMapper mapper;

    public MealPlanService(IGenericRepository<MealPlan> repository,
            IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<MealPlanForViewDto>> GetAllAsync(Expression<Func<MealPlan, bool>> filter = null, string[] includes = null)
    {
        var plans = await repository.GetAll(filter, includes).ToListAsync();
        return mapper.Map<List<MealPlanForViewDto>>(plans);
    }

    public async Task<MealPlanForViewDto> GetAsync(Expression<Func<MealPlan, bool>> filter, string[] includes = null)
    {
        var plan = await repository.GetAsync(filter, includes);
        if (plan is null)
            throw new HttpStatusCodeException(404, "MealPlan not found");

        return mapper.Map<MealPlanForViewDto>(plan);
    }

    public async Task<MealPlanForViewDto> CreateAsync(MealPlanForCreationDto dto)
    {
        var exists = await repository.GetAsync(p => p.Title == dto.Title);
        if (exists is not null)
            throw new HttpStatusCodeException(400, "MealPlan already exists");

        var plan = mapper.Map<MealPlan>(dto);
        plan.CreatedAt = DateTime.UtcNow;
        plan.CreatedBy = HttpContextHelper.UserId;

        await repository.CreateAsync(plan);
        await repository.SaveChangesAsync();

        return mapper.Map<MealPlanForViewDto>(plan);
    }

    public async Task<MealPlanForViewDto> UpdateAsync(Guid id, MealPlanForUpdateDto dto)
    {
        var plan = await repository.GetAsync(p => p.Id == id, new[] { "Meals" })
            ?? throw new HttpStatusCodeException(404, "MealPlan not found");

        if (!string.IsNullOrWhiteSpace(dto.Title))
            plan.Title = dto.Title;

        if (!string.IsNullOrWhiteSpace(dto.Description))
            plan.Description = dto.Description;

        if (dto.Goal.HasValue)
            plan.Goal = dto.Goal.Value;

        if (dto.IsPublic.HasValue)
            plan.IsPublic = dto.IsPublic.Value;

        if (dto.DailyCaloriesTarget.HasValue)
            plan.DailyCaloriesTarget = dto.DailyCaloriesTarget.Value;

        if (dto.DailyProteinTargetGrams.HasValue)
            plan.DailyProteinTargetGrams = dto.DailyProteinTargetGrams.Value;

        if (dto.DailyFatTargetGrams.HasValue)
            plan.DailyFatTargetGrams = dto.DailyFatTargetGrams.Value;

        if (dto.DailyCarbsTargetGrams.HasValue)
            plan.DailyCarbsTargetGrams = dto.DailyCarbsTargetGrams.Value;

        if (dto.DurationDays.HasValue)
            plan.DurationDays = dto.DurationDays.Value;


        plan.UpdatedAt = DateTime.UtcNow;
        plan.UpdatedBy = HttpContextHelper.UserId;

        repository.Update(plan);
        await repository.SaveChangesAsync();

        return mapper.Map<MealPlanForViewDto>(plan);
    }

    public async Task<bool> DeleteAsync(Expression<Func<MealPlan, bool>> filter)
    {
        var plan = await repository.GetAsync(filter);
        if (plan is null)
            throw new HttpStatusCodeException(404, "MealPlan not found");

        plan.DeletedBy = HttpContextHelper.UserId;

        await repository.DeleteAsync(plan);
        await repository.SaveChangesAsync();
        return true;
    }

}

