using AutoMapper;
using FitnessApp.Domain.Entities.Auth;
using FitnessApp.Domain.Entities.Nutrition;
using FitnessApp.Domain.Entities.Users;
using FitnessApp.Service.DTOs.FoodItemsDto;
using FitnessApp.Service.DTOs.MealsDto;
using FitnessApp.Service.DTOs.MealsItemDto;
using FitnessApp.Service.DTOs.MealsPlanDto;
using FitnessApp.Service.DTOs.PermissionsDto;
using FitnessApp.Service.DTOs.RolesDto;
using FitnessApp.Service.DTOs.TokensDto;
using FitnessApp.Service.DTOs.UserMealLogsDto;
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

        //FoodItems
        CreateMap<FoodItemForCreationDto, FoodItem>().ReverseMap();
        CreateMap<FoodItemForUpdateDto, FoodItem>().ReverseMap();
        CreateMap<FoodItemForViewDto, FoodItem>().ReverseMap();

        //MealItems
        CreateMap<MealItemForCreationDto, MealItem>().ReverseMap();
        CreateMap<MealItemForUpdateDto, MealItem>().ReverseMap();
        CreateMap<MealItemForViewDto, MealItem>().ReverseMap();

        //Meal
        CreateMap<MealForCreationDto, Meal>().ReverseMap();
        CreateMap<MealForUpdateDto, Meal>().ReverseMap();
        CreateMap<MealForViewDto, Meal>().ReverseMap();

        //MealPlan
        CreateMap<MealPlanForCreationDto, MealPlan>().ReverseMap();
        CreateMap<MealPlanForUpdateDto, MealPlan>().ReverseMap();
        CreateMap<MealPlanForViewDto, MealPlan>().ReverseMap();

        //UserMealLog
        CreateMap<UserMealLogForCreationDto, UserMealLog>().ReverseMap();
        CreateMap<UserMealLogForUpdateDto, UserMealLog>().ReverseMap();
        CreateMap<UserMealLogForViewDto, UserMealLog>().ReverseMap();
    }
}
