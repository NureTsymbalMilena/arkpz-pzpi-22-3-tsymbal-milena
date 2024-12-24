using InRoom.DAL.Interfaces;
using InRoom.DLL.Models;

namespace InRoom.DAL.Repositories;

public class MovementRepository: GenericRepository<Movement>, IMovementRepository
{
    private readonly ApplicationDbContext _context;
    
    // Constructor that initializes the repository with the ApplicationDbContext
    public MovementRepository(ApplicationDbContext context): base(context)
    {
        _context = context;
    }
}