using AutoMapper;
using FitnessApp.DataAccess.IRepository;
using FitnessApp.Domain.Entities.Nutrition;
using FitnessApp.Service.DTOs.FoodItemsDto;
using FitnessApp.Service.Exceptions;
using FitnessApp.Service.Helpers;
using FitnessApp.Service.IService;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;

namespace FitnessApp.Service.Service;

public class FoodItemService : IFoodItemService
{
    private readonly IGenericRepository<FoodItem> repository;
    private readonly IMapper mapper;

    public FoodItemService(IGenericRepository<FoodItem> repository, IMapper mapper)
    {
         this.repository = repository;
         this.mapper = mapper;
    }


    public async Task<IEnumerable<FoodItemForViewDto>> GetAllAsync(Expression<Func<FoodItem, bool>> filter = null, string[] includes = null)
    {
        var cluster = await repository.GetAll(filter, includes).ToListAsync();
        return mapper.Map<List<FoodItemForViewDto>>(cluster);
    }

    public async Task<FoodItemForViewDto> GetAsync(Expression<Func<FoodItem, bool>> filter = null, string[] includes = null)
    {
        var cluster = await repository.GetAsync(filter, includes);
        if (cluster is null)
            throw new HttpStatusCodeException(404, "FoodItem not found");

        return mapper.Map<FoodItemForViewDto>(cluster);
    }

    public async Task<FoodItemForViewDto> CreateAsync(FoodItemForCreationDto dto)
    {
        var exists = await repository.GetAsync(cl => cl.Name == dto.Name);
        if (exists is not null)
            throw new HttpStatusCodeException(400, "FoodItem already exists");

        var level = mapper.Map<FoodItem>(dto);

        level.CreatedAt = DateTime.UtcNow;
        level.CreatedBy = HttpContextHelper.UserId;

        await repository.CreateAsync(level);
        await repository.SaveChangesAsync();

        return mapper.Map<FoodItemForViewDto>(level);
    }

    public async Task<bool> DeleteAsync(Expression<Func<FoodItem, bool>> filter)
    {
        var cluster = await repository.GetAsync(filter);
        if (cluster is null)
            throw new HttpStatusCodeException(404, "FoodItem not found");

        cluster.DeletedBy = HttpContextHelper.UserId;

        await repository.DeleteAsync(cluster);
        await repository.SaveChangesAsync();
        return true;
    }

    public async Task<FoodItemForViewDto> UpdateAsync(Guid id, FoodItemForUpdateDto dto)
    {
        var foodItem = await repository.GetAsync(l => l.Id == id)
        ?? throw new HttpStatusCodeException(404, "FoodItem not found");

        if (!string.IsNullOrWhiteSpace(dto.Name))
            foodItem.Name = dto.Name;

        if (!string.IsNullOrWhiteSpace(dto.Brand))
            foodItem.Brand = dto.Brand;

        if (!string.IsNullOrWhiteSpace(dto.Description))
            foodItem.Description = dto.Description;

        if (dto.CaloriesPerServing.HasValue)
            foodItem.CaloriesPerServing = dto.CaloriesPerServing.Value;

        if (dto.ProteinPerServingGrams.HasValue)
            foodItem.ProteinPerServingGrams = dto.ProteinPerServingGrams.Value;

        if (dto.FatPerServingGrams.HasValue)
            foodItem.FatPerServingGrams = dto.FatPerServingGrams.Value;

        if (dto.CarbsPerServingGrams.HasValue)
            foodItem.CarbsPerServingGrams = dto.CarbsPerServingGrams.Value;

        if (dto.ServingSize.HasValue)
            foodItem.ServingSize = dto.ServingSize.Value;

        if (!string.IsNullOrWhiteSpace(dto.ServingUnit))
            foodItem.ServingUnit = dto.ServingUnit;

        if (dto.IsCustom.HasValue)
            foodItem.IsCustom = dto.IsCustom.Value;

        foodItem.UpdatedAt = DateTime.UtcNow;
        foodItem.UpdatedBy = HttpContextHelper.UserId;

        repository.Update(foodItem);
        await repository.SaveChangesAsync();

        return mapper.Map<FoodItemForViewDto>(foodItem);
    }
}
