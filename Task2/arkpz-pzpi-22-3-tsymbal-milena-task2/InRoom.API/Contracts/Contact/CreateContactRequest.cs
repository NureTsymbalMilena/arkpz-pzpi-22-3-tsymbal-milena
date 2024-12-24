using System.ComponentModel.DataAnnotations;

namespace InRoom.API.Contracts.Contact;

public class CreateContactRequest
{
    [Required]
    public Guid ContactInitiatorId { get; set; }
        
    [Required]
    public Guid ContactReceiverId { get; set; }
    
    [Required]
    public Guid DeviceId { get; set; }
    
    [Required]
    public float Distance { get; set; }
}