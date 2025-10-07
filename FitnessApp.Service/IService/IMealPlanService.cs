using FitnessApp.Domain.Entities.Nutrition;
using FitnessApp.Service.DTOs.MealsPlanDto;
using System.Linq.Expressions;

namespace FitnessApp.Service.IService;

public interface IMealPlanService
{
    Task<IEnumerable<MealPlanForViewDto>> GetAllAsync(Expression<Func<MealPlan, bool>> filter = null, string[] includes = null);
    Task<MealPlanForViewDto> GetAsync(Expression<Func<MealPlan, bool>> filter, string[] includes = null);
    Task<MealPlanForViewDto> CreateAsync(MealPlanForCreationDto dto);
    Task<MealPlanForViewDto> UpdateAsync(Guid id, MealPlanForUpdateDto dto);
    Task<bool> DeleteAsync(Expression<Func<MealPlan, bool>> filter);
}
