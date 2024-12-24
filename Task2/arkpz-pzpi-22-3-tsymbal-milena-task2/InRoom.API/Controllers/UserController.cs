using InRoom.API.Contracts.User;
using InRoom.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InRoom.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    // Endpoint for retrieving a user by their ID
    [HttpGet("{userId:Guid}")]
    [SwaggerOperation("Get user by id")]
    public async Task<IActionResult> GetUser([FromRoute] Guid userId)
    {
        var user = await _userService.GetById(userId);
        return Ok(user);
    }
    
    // Endpoint for retrieving all users in the system
    [HttpGet]
    [SwaggerOperation("Get all users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAll();
        return Ok(users);
    }
    
    // Endpoint for updating a user's information by their ID
    [HttpPatch("{userId:Guid}")]
    [SwaggerOperation("Edit user by id")]
    public async Task<IActionResult> EditUser([FromRoute] Guid userId, UpdateUserRequest request)
    {
        var users = await _userService.Update(userId, request.Name, request.Email, request.Email, request.DiseaseName);
        return Ok(users);
    }
    
    // Endpoint for deleting a user by their ID
    [HttpDelete("{userId:Guid}")]
    [SwaggerOperation("Delete user by id")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
    {
        var user = await _userService.Delete(userId);
        return Ok(user);
    }
    
    // Endpoint for connecting a user to a specific device
    [HttpPatch("connect/{userId:Guid}")]
    [SwaggerOperation("Connect user to device")]
    public async Task<IActionResult> ConnectUserToDevice([FromRoute] Guid userId, Guid deviceId)
    {
        return Ok(new { Message = $"User connected to {deviceId}" });
    }
    
    // Endpoint for retrieving a user's location in the room
    [HttpGet("location/{userId:Guid}")]
    [SwaggerOperation("Get user location in the room")]
    public async Task<IActionResult> GetUserLocation([FromRoute] Guid userId)
    {
        return Ok(new { Message = $"User with ID {userId} is located" });
    }
}
