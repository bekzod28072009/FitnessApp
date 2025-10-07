using FitnessApp.Domain.Entities.Nutrition;
using FitnessApp.Service.DTOs.MealsDto;
using System.Linq.Expressions;

namespace FitnessApp.Service.IService;

public interface IMealService
{
    Task<IEnumerable<MealForViewDto>> GetAllAsync(Expression<Func<Meal, bool>> filter = null, string[] includes = null);
    Task<MealForViewDto> GetAsync(Expression<Func<Meal, bool>> filter, string[] includes = null);
    Task<MealForViewDto> CreateAsync(MealForCreationDto dto);
    Task<MealForViewDto> UpdateAsync(Guid id, MealForUpdateDto dto);
    Task<bool> DeleteAsync(Expression<Func<Meal, bool>> filter);
}
