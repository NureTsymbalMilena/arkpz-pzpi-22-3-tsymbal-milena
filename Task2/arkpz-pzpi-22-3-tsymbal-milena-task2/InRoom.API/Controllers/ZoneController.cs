using InRoom.API.Contracts.Hospital;
using InRoom.API.Contracts.Zone;
using InRoom.BLL.Interfaces;
using InRoom.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InRoom.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ZoneController : ControllerBase
{
    private readonly IZoneService _zoneService;

    public ZoneController(IZoneService zoneService)
    {
        _zoneService = zoneService;
    }
    
    // Endpoint for creating a new zone
    [HttpPost]
    [SwaggerOperation("Create zone")]
    public async Task<IActionResult> AddZone(CreateZoneRequest request)
    {
        var zone = await _zoneService.Add(request.Name, request.FloorNumber, request.HospitalName);
        return Ok(zone);
    }
    
    // Endpoint for retrieving a zone by its ID
    [HttpGet("{zoneId:Guid}")]
    [SwaggerOperation("Get zone by id")]
    public async Task<IActionResult> GetZone([FromRoute] Guid zoneId)
    {
        var zone = await _zoneService.GetById(zoneId);
        return Ok(zone);
    }
    
    // Endpoint for retrieving all zones in the system
    [HttpGet]
    [SwaggerOperation("Get all zones")]
    public async Task<IActionResult> GetAllZones()
    {
        var zones = await _zoneService.GetAll();
        return Ok(zones);
    }
    
    // Endpoint for updating a zone's information by its ID
    [HttpPatch("{zoneId:Guid}")]
    [SwaggerOperation("Edit zone by id")]
    public async Task<IActionResult> EditZone([FromRoute] Guid zoneId, UpdateZoneRequest request)
    {
        var zone = await _zoneService.Update(zoneId, request.Name, request.FloorNumber, request.HospitalName);
        return Ok(zone);
    }
    
    // Endpoint for deleting a zone by its ID
    [HttpDelete("{zoneId:Guid}")]
    [SwaggerOperation("Delete zone by id")]
    public async Task<IActionResult> DeleteZone([FromRoute] Guid zoneId)
    {
        var zone = await _zoneService.Delete(zoneId);
        return Ok(zone);
    }
}
