using Gym.Application.Dtos.User.Request;
using Gym.Application.Dtos.User.Response;
using Gym.Domain.Abstractions.ResultPattern;

namespace Gym.Application.Services.User;

public interface IUserService
{
    Task<Result<UserResponse>> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken);
    Task<Result<PagedUsersResponse>> GetAllAsync(int page, int take, CancellationToken cancellationToken);
    Task<Result<UserResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(Guid id, UpdateUserRequest request, CancellationToken cancellationToken);
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken);
}