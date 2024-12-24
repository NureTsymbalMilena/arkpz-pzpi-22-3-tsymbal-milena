using InRoom.DAL.Interfaces;
using InRoom.DLL.Models;

namespace InRoom.DAL.Repositories;

public class NotificationRepository: GenericRepository<Notification>, INotificationRepository
{
    private readonly ApplicationDbContext _context;
    
    // Constructor that initializes the repository with the ApplicationDbContext
    public NotificationRepository(ApplicationDbContext context):base(context)
    {
        _context = context;
    }
}