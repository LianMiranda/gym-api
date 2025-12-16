using Gym.Application.Dtos.Auth.Request;
using Gym.Application.Dtos.Auth.Response;
using Gym.Application.Dtos.User.Request;
using Gym.Domain.Abstractions.ResultPattern;

namespace Gym.Application.Services.Auth;

public interface IAuthService
{
    Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
    Task<Result<LoginResponse>> RegisterAsync(CreateUserRequest request, CancellationToken cancellationToken);
}