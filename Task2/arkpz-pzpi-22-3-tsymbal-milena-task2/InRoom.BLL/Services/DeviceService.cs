using InRoom.BLL.Helpers;
using InRoom.BLL.Interfaces;
using InRoom.DAL.Interfaces;
using InRoom.DLL.Models;

namespace InRoom.BLL.Services;

public class DeviceService: GenericService<Device>, IDeviceService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDeviceRepository _deviceRepository;
    private readonly IRoomRepository _roomRepository;

    // Constructor to inject required dependencies for the DeviceService
    public DeviceService(IUnitOfWork unitOfWork, IDeviceRepository deviceRepository, IRoomRepository roomRepository) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _deviceRepository = deviceRepository;
        _roomRepository = roomRepository;
    }
    
    // Method to add a new device to a room, ensuring the room is available
    public async Task<Device> Add(string model, string roomName)
    {
        var device = await _deviceRepository.GetByRoomName(roomName);
        if (device != null)
        {
            throw new ApiException($"The device in room {roomName} is currently in use", 400);
        }

        var room = await _roomRepository.GetByName(roomName);
        if (room == null)
        {
            throw new ApiException($"The room {roomName} wasn't found", 404);
        }
        
        var newDevice = new Device
        {
            DeviceId = Guid.NewGuid(),
            Model = model,
            RoomId = room.RoomId,
            Room = room
        };
      
        await Repository.Add(newDevice);
        await _unitOfWork.SaveChangesAsync();

        return newDevice;
    }

    // Method to update an existing device with a new model and room
    public async Task<Device> Update(Guid deviceId, string model, string roomName)
    {
        var device = await Repository.GetById(deviceId);
        if (device == null)
        {
            throw new ApiException($"Device with ID {deviceId} not found.", 404);
        }
        
        var room = await _roomRepository.GetByName(roomName);
        if (room == null)
        {
            throw new ApiException($"The room {roomName} wasn't found", 404);
        }

        device.Model = model;
        device.RoomId = room.RoomId;
        device.Room = room;

        await Repository.Update(device);
        await _unitOfWork.SaveChangesAsync();

        return device;
    }
}
