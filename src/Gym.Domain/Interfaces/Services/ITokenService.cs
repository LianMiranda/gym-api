using System.Security.Claims;
using Gym.Domain.Entities;

namespace Gym.Domain.Interfaces.Services;

public interface ITokenService
{
   string GenerateToken(IEnumerable<Claim> claims, CancellationToken cancellationToken);
   string GenerateRefreshToken();
   ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}