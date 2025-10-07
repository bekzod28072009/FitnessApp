using FitnessApp.Domain.Entities.Nutrition;
using FitnessApp.Service.DTOs.MealsItemDto;
using System.Linq.Expressions;

namespace FitnessApp.Service.IService;

public interface IMealItemService
{
    Task<IEnumerable<MealItemForViewDto>> GetAllAsync(Expression<Func<MealItem, bool>> filter = null, string[] includes = null);
    Task<MealItemForViewDto> GetAsync(Expression<Func<MealItem, bool>> filter, string[] includes = null);
    Task<MealItemForViewDto> CreateAsync(MealItemForCreationDto dto);
    Task<MealItemForViewDto> UpdateAsync(Guid id, MealItemForUpdateDto dto);
    Task<bool> DeleteAsync(Expression<Func<MealItem, bool>> filter);
}
