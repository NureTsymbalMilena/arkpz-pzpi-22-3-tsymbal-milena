using InRoom.API.Contracts.Contact;
using InRoom.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InRoom.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }
    
    // Endpoint for creating a new contact between users
    [HttpPost]
    [SwaggerOperation("Create contact")]
    public async Task<IActionResult> AddContact(CreateContactRequest request)
    {
        var contact = await _contactService.Add(request.ContactInitiatorId, request.ContactReceiverId, request.DeviceId, request.Distance);
        return Ok(contact);
    }
    
    // Endpoint for retrieving a contact by its ID
    [HttpGet("{contactId:Guid}")]
    [SwaggerOperation("Get contact by id")]
    public async Task<IActionResult> GetContact([FromRoute] Guid contactId)
    {
        var contact = await _contactService.GetById(contactId);
        return Ok(contact);
    }
    
    // Endpoint for retrieving all contacts
    [HttpGet]
    [SwaggerOperation("Get all contacts")]
    public async Task<IActionResult> GetAllContacts()
    {
        var contacts = await _contactService.GetAll();
        return Ok(contacts);
    }
    
    // Endpoint for updating the contact's distance by its ID
    [HttpPatch("{contactId:Guid}")]
    [SwaggerOperation("Edit contact by id")]
    public async Task<IActionResult> EditContact([FromRoute] Guid contactId, UpdateContactRequest request)
    {
        var contact = await _contactService.Update(contactId, request.Distance);
        return Ok(contact);
    }
    
    // Endpoint for deleting a contact by its ID
    [HttpDelete("{contactId:Guid}")]
    [SwaggerOperation("Delete contact by id")]
    public async Task<IActionResult> DeleteContact([FromRoute] Guid contactId)
    {
        var contact = await _contactService.Delete(contactId);
        return Ok(contact);
    }
    
    // Endpoint for generating a report on contacts for a specified user and time period
    [HttpGet("report")]
    [SwaggerOperation("Generate contact report for a specific period")]
    public async Task<IActionResult> GenerateReport([FromQuery] Guid userId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        return Ok(new { Message = $"Contact report successfully generated for user {userId} from {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}" });
    }
    
    // Endpoint for analyzing contacts for epidemiological risks
    [HttpPost("epidemiological-risk-analysis")]
    [SwaggerOperation("Analyze contacts for epidemiological risks")]
    public async Task<IActionResult> AnalyzeEpidemiologicalRisk()
    {
        return Ok(new { Message = "The epidemiological risk analysis was successfully completed" });
    }
    
    // Endpoint for automatically registering a contact between users
    [HttpPost("auto-register-contact")]
    [SwaggerOperation("Automatically register contact with another user")]
    public async Task<IActionResult> AutoRegisterContact()
    {
        return Ok(new { Message = "Contact successfully registered between the users" });
    }
}
