using InRoom.DLL.Models;

namespace InRoom.BLL.Interfaces;

public interface IRoomService: IGenericService<Room>
{
    Task<Room> Add(string roomName, string zoneName);
    Task<Room> Update(Guid roomId, string roomName, string zoneName);
}