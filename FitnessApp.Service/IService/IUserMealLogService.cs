using FitnessApp.Domain.Entities.Nutrition;
using FitnessApp.Service.DTOs.UserMealLogsDto;
using System.Linq.Expressions;

namespace FitnessApp.Service.IService;

public interface IUserMealLogService
{
    Task<IEnumerable<UserMealLogForViewDto>> GetAllAsync(Expression<Func<UserMealLog, bool>> filter = null, string[] includes = null);
    Task<UserMealLogForViewDto> GetAsync(Expression<Func<UserMealLog, bool>> filter, string[] includes = null);
    Task<UserMealLogForViewDto> CreateAsync(UserMealLogForCreationDto dto);
    Task<UserMealLogForViewDto> UpdateAsync(Guid id, UserMealLogForUpdateDto dto);
    Task<bool> DeleteAsync(Expression<Func<UserMealLog, bool>> filter);
}
