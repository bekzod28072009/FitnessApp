using AutoMapper;
using FitnessApp.Domain.Entities.Auth;
using FitnessApp.Domain.Entities.Users;
using FitnessApp.Service.DTOs.PermissionsDto;
using FitnessApp.Service.DTOs.RolesDto;
using FitnessApp.Service.DTOs.TokensDto;
using FitnessApp.Service.DTOs.UsersDto;

namespace FitnessApp.API.Configurations;

public class MapConfiguration : Profile
{
    public MapConfiguration()
    {
        //Users
        CreateMap<UserForCreationDto, User>().ReverseMap();
        CreateMap<UserForUpdateDto, User>().ReverseMap();
        CreateMap<UserForViewDto, User>().ReverseMap();

        //Permissions
        CreateMap<PermissionForCreationDto, Permission>().ReverseMap();
        CreateMap<PermissionForUpdateDto, Permission>().ReverseMap();
        CreateMap<string, PermissionForViewDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src));
        CreateMap<PermissionForViewDto, Permission>().ReverseMap();



        //Roles
        CreateMap<RoleForCreationDto, Role>().ReverseMap();
        CreateMap<RoleForUpdateDto, Role>().ReverseMap();
        CreateMap<Role, RoleForViewDto>()
            .ForMember(d => d.Permissions, opt => opt.MapFrom(s =>
                (s.Permissions ?? new List<Permission>())
                .Select(p => p.Name)
                .ToList()
            ));
        CreateMap<RoleForViewGetDto, Role>().ReverseMap();

        //Token
        CreateMap<TokenForCreationDto, Token>().ReverseMap();  
        CreateMap<TokenForUpdateDto, Token>().ReverseMap();
        CreateMap<TokenForViewDto, Token>().ReverseMap();
    }

}
