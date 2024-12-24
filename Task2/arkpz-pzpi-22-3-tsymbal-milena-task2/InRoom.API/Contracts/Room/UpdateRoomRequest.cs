using System.ComponentModel.DataAnnotations;

namespace InRoom.API.Contracts.Room;

public class UpdateRoomRequest
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string ZoneName { get; set; }
}