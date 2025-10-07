using FitnessApp.Domain.Entities.Nutrition;
using FitnessApp.Service.DTOs.FoodItemsDto;
using System.Linq.Expressions;

namespace FitnessApp.Service.IService;

public interface IFoodItemService
{
    Task<IEnumerable<FoodItemForViewDto>> GetAllAsync(Expression<Func<FoodItem, bool>> filter = null, string[] includes = null);
    Task<FoodItemForViewDto> GetAsync(Expression<Func<FoodItem, bool>> filter, string[] includes = null);
    Task<FoodItemForViewDto> CreateAsync(FoodItemForCreationDto dto); 
    Task<FoodItemForViewDto> UpdateAsync(Guid id, FoodItemForUpdateDto dto);
    Task<bool> DeleteAsync(Expression<Func<FoodItem, bool>> filter);
}
