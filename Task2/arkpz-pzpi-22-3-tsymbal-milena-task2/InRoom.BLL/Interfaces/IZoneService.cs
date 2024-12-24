using InRoom.DLL.Models;

namespace InRoom.BLL.Interfaces;

public interface IZoneService: IGenericService<Zone>
{
    Task<Zone> Add(string zoneName, int floorNumber, string hospitalName);
    Task<Zone> Update(Guid zoneId, string zoneName, int floorNumber, string hospitalName);
}