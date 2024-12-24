using InRoom.DLL.Models;

namespace InRoom.BLL.Interfaces;

public interface IDeviceService: IGenericService<Device>
{
    Task<Device> Add(string model, string roomName);
    Task<Device> Update(Guid deviceId, string model, string roomName);
}