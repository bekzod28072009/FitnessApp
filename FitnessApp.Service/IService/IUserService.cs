using FitnessApp.Domain.Entities.Users;
using FitnessApp.Service.DTOs.UsersDto;
using System.Linq.Expressions;

namespace FitnessApp.Service.IService;

public interface IUserService
{
    Task<List<UserForViewDto>> GetAllAsync(Expression<Func<User, bool>> filter = null, string[] includes = null);
    Task<UserForViewDto> GetAsync(Expression<Func<User, bool>> filter, string[] includes = null);
    Task<UserForViewDto> CreateAsync(UserForCreationDto dto);
    Task<bool> DeleteAsync(Expression<Func<User, bool>> filter);
    Task<UserForViewDto> UpdateAsync(Guid id, UserForUpdateDto dto);
    Task<bool> ChangePassword(string email, string password);
}
