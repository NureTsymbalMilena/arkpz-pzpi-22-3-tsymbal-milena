using InRoom.BLL.Helpers;
using InRoom.BLL.Interfaces;
using InRoom.DAL.Interfaces;
using InRoom.DLL.Models;

namespace InRoom.BLL.Services;

public class ContactService : GenericService<Contact>, IContactService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IDeviceRepository _deviceRepository;

    // Constructor to inject required dependencies for the ContactService
    public ContactService(
        IUnitOfWork unitOfWork, 
        IUserRepository userRepository, 
        IDeviceRepository deviceRepository) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _deviceRepository = deviceRepository;
    }
    
    // Method to create a new contact between two users with a specific distance and device
    public async Task<Contact> Add(Guid contactInitiatorId, Guid contactReceiverId, Guid deviceId, float distance)
    {
        const float MaxAllowedDistance = 5.0f;

        if (distance > MaxAllowedDistance)
        {
            throw new ApiException($"Contact cannot be created. Distance ({distance}m) exceeds the maximum allowed value of {MaxAllowedDistance}m.", 400);
        }
        
        var userInitiator = await _userRepository.GetById(contactInitiatorId);
        if (userInitiator == null)
        {
            throw new ApiException($"User initiator with ID {contactInitiatorId} not found.", 404);
        }
        
        var userReceiver = await _userRepository.GetById(contactReceiverId);
        if (userReceiver == null)
        {
            throw new ApiException($"User receiver with ID {contactReceiverId} not found.", 404);
        }
        
        var device = await _deviceRepository.GetById(deviceId);
        if (device == null)
        {
            throw new ApiException($"Device with ID {deviceId} not found.", 404);
        }
        
        var newContact = new Contact()
        {
            ContactId = Guid.NewGuid(),
            ContactInitiatorId = contactInitiatorId,
            ContactInitiator = userInitiator,
            ContactReceiverId = contactReceiverId,
            ContactReceiver = userReceiver,
            DeviceId = deviceId,
            Device = device,
            MinDistance = distance
        };
      
        await Repository.Add(newContact);
        await _unitOfWork.SaveChangesAsync();

        return newContact;
    }

    // Method to update an existing contact with a new distance value
    public async Task<Contact> Update(Guid contactId, float distance)
    {
        var contact = await Repository.GetById(contactId);
        if (contact == null)
        {
            throw new ApiException($"Contact with ID {contactId} not found.", 404);
        }
        
        if (distance < 0)
        {
            throw new ApiException("Distance cannot be negative.", 400);
        }
        
        contact.ContactEndTime = DateTime.Now;
        contact.MinDistance = distance < contact.MinDistance ? distance : contact.MinDistance;
        
        await Repository.Update(contact);
        await _unitOfWork.SaveChangesAsync();

        return contact;
    }
}
