using Gym.Domain.Interfaces.Shared;

namespace Gym.Infrastructure.Security;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        throw new NotImplementedException();
    }

    public bool Verify(string password, string passwordHash)
    {
        throw new NotImplementedException();
    }
}