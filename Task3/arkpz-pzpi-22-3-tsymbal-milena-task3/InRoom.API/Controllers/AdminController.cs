using InRoom.API.Contracts.Admin;
using InRoom.API.Contracts.User;
using InRoom.API.Helpers;
using InRoom.BLL.Interfaces;
using InRoom.DLL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InRoom.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[RoleVerification(Roles.Admin)]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }
    
    // Endpoint for database backup
    [HttpPost("backup")]
    [SwaggerOperation("Backup database")]
    public async Task<IActionResult> Backup([FromQuery] string outputDirectory = null)
    {
        string backupFilePath = await _adminService.BackupData(outputDirectory);
        return Ok(new { message = $"Backup successfully done at {backupFilePath}" });
    }
    
    // Endpoint for database restore
    [HttpPost("restore")]
    [SwaggerOperation("Restore database")]
    public async Task<IActionResult> Restore([FromBody] string backupFilePath)
    {
        await _adminService.RestoreData(backupFilePath);
        return Ok(new { message = "Database restore successfully done" });
    }
    
    // Endpoint for settring user role
    [HttpPatch("role")]
    [SwaggerOperation("Set user role")]
    public async Task<IActionResult> SetRole(SetRoleRequest request)
    {
        var user = await _adminService.SetRole(request.UserEmail, request.Role);
        return Ok(user);
    }
    
    // Endpoint for connecting user to device
    [HttpPatch("connect")]
    [SwaggerOperation("Connect user to device")]
    public async Task<IActionResult> ConnectUserToDevice(ConnectUserToDeviceRequest request)
    {
        var user = await _adminService.ConnectUserToDevice(request.UserEmail, request.RoomName);
        return Ok(user);
    }
}