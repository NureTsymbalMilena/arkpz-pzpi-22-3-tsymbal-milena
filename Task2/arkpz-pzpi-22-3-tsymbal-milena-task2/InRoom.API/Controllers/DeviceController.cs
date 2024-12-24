using InRoom.API.Contracts.Device;
using InRoom.API.Contracts.Hospital;
using InRoom.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InRoom.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeviceController : ControllerBase
{
    private readonly IDeviceService _deviceService;

    public DeviceController(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }
    
    // Endpoint for creating a new device with provided model and room name
    [HttpPost]
    [SwaggerOperation("Create device")]
    public async Task<IActionResult> AddDevice(CreateDeviceRequest request)
    {
        var device = await _deviceService.Add(request.Model, request.RoomName);
        return Ok(device);
    }
    
    // Endpoint for retrieving a device by its ID
    [HttpGet("{deviceId:Guid}")]
    [SwaggerOperation("Get device by id")]
    public async Task<IActionResult> GetDevice([FromRoute] Guid deviceId)
    {
        var device = await _deviceService.GetById(deviceId);
        return Ok(device);
    }
    
    // Endpoint for retrieving all devices
    [HttpGet]
    [SwaggerOperation("Get all devices")]
    public async Task<IActionResult> GetAllDevices()
    {
        var devices = await _deviceService.GetAll();
        return Ok(devices);
    }
    
    // Endpoint for updating device information (model and room name) by its ID
    [HttpPatch("{deviceId:Guid}")]
    [SwaggerOperation("Edit device by id")]
    public async Task<IActionResult> EditDevice([FromRoute] Guid deviceId, UpdateDeviceRequest request)
    {
        var device = await _deviceService.Update(deviceId, request.Model, request.RoomName);
        return Ok(device);
    }
    
    // Endpoint for deleting a device by its ID
    [HttpDelete("{deviceId:Guid}")]
    [SwaggerOperation("Delete device by id")]
    public async Task<IActionResult> DeleteDevice([FromRoute] Guid deviceId)
    {
        var device = await _deviceService.Delete(deviceId);
        return Ok(device);
    }
}
