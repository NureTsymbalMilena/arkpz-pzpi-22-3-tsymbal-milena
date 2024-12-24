using InRoom.DLL.Models;

namespace InRoom.BLL.Interfaces;

public interface IContactService: IGenericService<Contact>
{
    Task<Contact> Add(Guid contactInitiatorId, Guid contactReceiverId, Guid deviceId, float distance);
    Task<Contact> Update(Guid contactId, float distance);
}