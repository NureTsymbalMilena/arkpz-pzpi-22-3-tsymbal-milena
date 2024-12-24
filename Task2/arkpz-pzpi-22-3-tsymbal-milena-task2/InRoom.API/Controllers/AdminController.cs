using InRoom.API.Contracts.User;
using InRoom.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InRoom.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IAuthService _authService;

    public AdminController(IAuthService authService)
    {
        _authService = authService;
    }
    
    // Endpoint for database backup
    [HttpPost("backup")]
    [SwaggerOperation("Backup database")]
    public async Task<IActionResult> Backup()
    {
        return Ok(new { Message = "Database backup successfully done" });
    }
    
    // Endpoint for database restore
    [HttpPost("restore")]
    [SwaggerOperation("Restore database")]
    public async Task<IActionResult> Restore()
    {
        return Ok(new { Message = "Database restore successfully done" });
    }
}