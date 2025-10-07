using FitnessApp.Service.DTOs.UserMealLogsDto;
using FitnessApp.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserMealLogController : ControllerBase
{
    private readonly IUserMealLogService userMealLogService;

    public UserMealLogController(IUserMealLogService userMealLogService)
    {
        this.userMealLogService = userMealLogService;
    }

    // =====================================
    // 🔹 GET ALL
    // =====================================
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await userMealLogService.GetAllAsync();
        return Ok(result);
    }

    // =====================================
    // 🔹 GET BY ID
    // =====================================
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await userMealLogService.GetAsync(x => x.Id == id);
        if (result is null)
            return NotFound();

        return Ok(result);
    }

    // =====================================
    // 🔹 CREATE
    // =====================================
    [HttpPost]
    [Authorize(Roles = "User, Admin, SuperAdmin")]
    public async Task<IActionResult> CreateAsync([FromBody] UserMealLogForCreationDto dto)
    {
        var result = await userMealLogService.CreateAsync(dto);
        return Ok(result);
    }

    // =====================================
    // 🔹 UPDATE
    // =====================================
    [HttpPatch("{id:guid}")]
    [Authorize(Roles = "User, Admin, SuperAdmin")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UserMealLogForUpdateDto dto)
    {
        var result = await userMealLogService.UpdateAsync(id, dto);
        return Ok(result);
    }

    // =====================================
    // 🔹 DELETE
    // =====================================
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "User, Admin, SuperAdmin")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var isDeleted = await userMealLogService.DeleteAsync(x => x.Id == id);
        return Ok(isDeleted);
    }
}
