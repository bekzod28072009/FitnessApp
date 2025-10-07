using FitnessApp.Service.DTOs.MealsPlanDto;
using FitnessApp.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MealPlanController : ControllerBase
{
    private readonly IMealPlanService service;

    public MealPlanController(IMealPlanService service)
    {
        this.service = service;
    }

    /// <summary>
    /// Get all MealPlans (Authorized)
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] bool includeMeals = false)
    {
        string[] includes = includeMeals ? new[] { "Meals", "Meals.MealItems", "Meals.MealItems.FoodItem" } : null;

        var result = await service.GetAllAsync(null, includes);
        return Ok(result);
    }

    /// <summary>
    /// Get a specific MealPlan by Id (Authorized)
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAsync(Guid id, [FromQuery] bool includeMeals = false)
    {
        string[] includes = includeMeals ? new[] { "Meals", "Meals.MealItems", "Meals.MealItems.FoodItem" } : null;

        var result = await service.GetAsync(m => m.Id == id, includes);
        return Ok(result);
    }

    /// <summary>
    /// Create a new MealPlan (Authorized)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public async Task<IActionResult> CreateAsync([FromBody] MealPlanForCreationDto dto)
    {
        var result = await service.CreateAsync(dto);
        return Ok(result);
    }

    /// <summary>
    /// Update MealPlan (Authorized)
    /// </summary>
    [HttpPatch("{id:guid}")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] MealPlanForUpdateDto dto)
    {
        var result = await service.UpdateAsync(id, dto);
        return Ok(result);
    }

    /// <summary>
    /// Delete MealPlan (Authorized)
    /// </summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await service.DeleteAsync(m => m.Id == id);
        return Ok(result);
    }

}
