using FitnessApp.Service.DTOs.FoodItemsDto;
using FitnessApp.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.API.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FoodItemController : ControllerBase
{
    private readonly IFoodItemService foodItemService;

    public FoodItemController(IFoodItemService foodItemService)
    {
        this.foodItemService = foodItemService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await foodItemService.GetAllAsync();
        return Ok(result);
    }

    // GET: api/Course/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await foodItemService.GetAsync(c => c.Id == id);
        if (result is null)
            return NotFound();

        return Ok(result);
    }


    // POST: api/Course
    [HttpPost]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public async Task<IActionResult> CreateAsync([FromBody] FoodItemForCreationDto dto)
    {
        var result = await foodItemService.CreateAsync(dto);
        return Ok(result);

    }

    // PUT: api/Course/{id}
    [HttpPatch("{id:guid}")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] FoodItemForUpdateDto dto)
    {
        var updatedCourse = await foodItemService.UpdateAsync(id, dto);
        return Ok(updatedCourse);
    }


    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var isDeleted = await foodItemService.DeleteAsync(c => c.Id == id);
        return Ok(isDeleted);
    }
}
