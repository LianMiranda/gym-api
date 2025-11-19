using Gym.Domain.Interfaces.Shared;

namespace Gym.Infrastructure.Security;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        string hashPassword = BCrypt.Net.BCrypt.HashPassword(password);
        return hashPassword;
    }

    public bool Verify(string password, string passwordHash)
    {
        bool verify = BCrypt.Net.BCrypt.Verify(password, passwordHash);

        return verify;
    }
}