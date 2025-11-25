using System.Security.Claims;
using Gym.Application.Dtos.Auth.Request;
using Gym.Application.Dtos.Auth.Response;
using Gym.Application.Dtos.User.Request;
using Gym.Application.Services.User;
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
    private readonly IUserService _userService;

    public AuthService(IPasswordHasher passwordHasher, ITokenService tokenService, IUserRepository userRepository, IUserService userService)
    {
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _userRepository = userRepository;
        _userService = userService;
    }


    public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
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
    

    public async Task<Result<LoginResponse>> RegisterAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.CreateAsync(request, cancellationToken);
        
        if(!result.IsSuccess)
            return Result<LoginResponse>.Error(result.Message);
        
        var user = result.Data!;
        
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