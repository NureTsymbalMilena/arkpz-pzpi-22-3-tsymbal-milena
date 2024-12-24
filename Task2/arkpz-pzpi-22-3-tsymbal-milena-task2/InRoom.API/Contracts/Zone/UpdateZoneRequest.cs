using System.ComponentModel.DataAnnotations;

namespace InRoom.API.Contracts.Zone;

public class UpdateZoneRequest
{
    [Required]
    public string Name { get; set; }
        
    [Required]
    public int FloorNumber { get; set; }
    
    [Required]
    public string HospitalName { get; set; }
}