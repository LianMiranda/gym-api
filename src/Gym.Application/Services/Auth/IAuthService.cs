using Gym.Application.Dtos.Auth.Request;
using Gym.Application.Dtos.Auth.Response;
using Gym.Domain.Abstractions.ResultPattern;

namespace Gym.Application.Services.Auth;

public interface IAuthService
{
    Task<Result<LoginResponse>> Login(LoginRequest request, CancellationToken cancellationToken);
}