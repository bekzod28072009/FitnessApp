using FitnessApp.Service.DTOs.MealsItemDto;
using FitnessApp.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MealItemController : ControllerBase
{
    private readonly IMealItemService mealItemService;

    public MealItemController(IMealItemService mealItemService)
    {
        this.mealItemService = mealItemService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await mealItemService.GetAllAsync(includes: new[] { "FoodItem", "Meal" });
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await mealItemService.GetAsync(m => m.Id == id, new[] { "FoodItem", "Meal" });
        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> CreateAsync([FromBody] MealItemForCreationDto dto)
    {
        var result = await mealItemService.CreateAsync(dto);
        return Ok(result);
    }

    [HttpPatch("{id:guid}")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] MealItemForUpdateDto dto)
    {
        var result = await mealItemService.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await mealItemService.DeleteAsync(m => m.Id == id);
        return Ok(result);
    }
}
