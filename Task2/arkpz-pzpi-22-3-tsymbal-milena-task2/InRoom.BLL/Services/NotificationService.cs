using InRoom.BLL.Helpers;
using InRoom.BLL.Interfaces;
using InRoom.DAL.Interfaces;
using InRoom.DAL.Repositories;
using InRoom.DLL.Models;

namespace InRoom.BLL.Services;

public class NotificationService: GenericService<Notification>, INotificationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    // Constructor to inject the required dependencies: UnitOfWork and UserRepository
    public NotificationService(IUnitOfWork unitOfWork, IUserRepository userRepository) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }
    
    // Method to add a new notification for a user with a specific message
    public async Task<Notification> Add(Guid userId, string message)
    {
        var user = await _userRepository.GetById(userId);
        if (user == null)
        {
            throw new ApiException($"User with ID {userId} not found", 404);
        }

        var newNotification = new Notification()
        {
            NotificationId = Guid.NewGuid(),
            UserId = userId,
            User = user,
            Message = message
        };
      
        await Repository.Add(newNotification);
        await _unitOfWork.SaveChangesAsync();

        return newNotification;
    }

    // Method to update the message of an existing notification
    public async Task<Notification> Update(Guid notificationId, string message)
    {
        var notification = await Repository.GetById(notificationId);
        if (notification == null)
        {
            throw new ApiException($"Notification with ID {notificationId} not found", 404);
        }

        notification.Message = message;

        await Repository.Update(notification);
        await _unitOfWork.SaveChangesAsync();

        return notification;
    }
}
