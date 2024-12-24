using InRoom.DLL.Enums;
using InRoom.DLL.Models;

namespace InRoom.BLL.Interfaces;

public interface IUserService : IGenericService<User>
{
    Task<User> Update(Guid userId, string name, string surname, string email, string diseaseName);
}