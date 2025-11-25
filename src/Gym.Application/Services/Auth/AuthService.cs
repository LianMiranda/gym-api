using System.Security.Claims;
using Gym.Application.Dtos.Auth.Request;
using Gym.Application.Dtos.Auth.Response;
using Gym.Domain.Abstractions.ResultPattern;
using Gym.Domain.Interfaces.Repositories;
using Gym.Domain.Interfaces.Services;
using Gym.Domain.Interfaces.Shared;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Gym.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public AuthService(IPasswordHasher passwordHasher, ITokenService tokenService, IUserRepository userRepository)
    {
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _userRepository = userRepository;
    }


    public async Task<Result<LoginResponse>> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null)
            return Result<LoginResponse>.Error("Invalid credentials");

        var verifyPassword = _passwordHasher.Verify(request.Password, user.PasswordHash);

        if (!verifyPassword)
            return Result<LoginResponse>.Error("Invalid credentials");

        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString())
        };

        var token = _tokenService.GenerateToken(claims, cancellationToken);

        var response = new LoginResponse
        {
            Token = token
        };

        return Result<LoginResponse>.Success(response);
    }
    
}