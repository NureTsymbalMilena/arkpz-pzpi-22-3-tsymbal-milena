using InRoom.DLL.Models;

namespace InRoom.DAL.Interfaces;

public interface IDeviceRepository: IRepository<Device>
{
    Task<Device?> GetByRoomName(string deviceName);
}