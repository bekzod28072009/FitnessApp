using FitnessApp.Service.DTOs.PermissionsDto;
using FitnessApp.Service.DTOs.TokensDto;

namespace FitnessApp.Service.IService;

public interface IAuthService
{
    ValueTask<TokenForViewDto> GenerateToken(string email, string password);
    ValueTask<string> RestartToken(string token);
    ValueTask<List<PermissionForViewDto>> GetPermissinWithToken(string token);
}
