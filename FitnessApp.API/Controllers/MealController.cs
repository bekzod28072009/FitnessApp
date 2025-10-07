using FitnessApp.Service.DTOs.MealsDto;
using FitnessApp.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.API.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MealController : ControllerBase
{
    private readonly IMealService mealService;

    public MealController(IMealService mealService)
    {
        this.mealService = mealService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await mealService.GetAllAsync(includes: new[] { "MealItems", "MealItems.FoodItem" });
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await mealService.GetAsync(m => m.Id == id, new[] { "MealItems", "MealItems.FoodItem" });
        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> CreateAsync([FromBody] MealForCreationDto dto)
    {
        var result = await mealService.CreateAsync(dto);
        return Ok(result);
    }

    [HttpPatch("{id:guid}")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] MealForUpdateDto dto)
    {
        var result = await mealService.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await mealService.DeleteAsync(m => m.Id == id);
        return Ok(result);
    }
}
