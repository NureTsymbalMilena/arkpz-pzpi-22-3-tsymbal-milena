using InRoom.DAL.Interfaces;
using InRoom.DLL.Models;

namespace InRoom.DAL.Repositories;

public class ContactRepository : GenericRepository<Contact>, IContactRepository
{
    private readonly ApplicationDbContext _context;
    
    // Constructor that accepts the ApplicationDbContext and passes it to the base repository
    public ContactRepository(ApplicationDbContext context): base(context)
    {
        _context = context;
    }
    
}