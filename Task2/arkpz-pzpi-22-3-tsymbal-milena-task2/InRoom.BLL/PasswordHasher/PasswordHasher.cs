using BCrypt.Net;
using InRoom.BLL.Interfaces;

namespace InRoom.BLL.PasswordHasher;

public class PasswordHasher: IPasswordHasher
{
    public string Generate(string password) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword(password, HashType.SHA256);

    public bool Verify(string password, string hashPassword) =>
        BCrypt.Net.BCrypt.EnhancedVerify(password, hashPassword, HashType.SHA256);
}