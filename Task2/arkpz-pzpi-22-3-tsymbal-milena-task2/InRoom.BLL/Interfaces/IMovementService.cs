using InRoom.DLL.Models;

namespace InRoom.BLL.Interfaces;

public interface IMovementService: IGenericService<Movement>
{
    Task<Movement> Add(Guid deviceId, Guid userId);
    Task<Movement> Update(Guid movementId);
}