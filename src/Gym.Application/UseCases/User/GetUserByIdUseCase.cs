using Gym.Application.Dtos.User.Response;
using Gym.Application.MappingImplementation;
using Gym.Domain.Abstractions.ResultPattern;
using Gym.Domain.Interfaces.Repositories;

namespace Gym.Application.UseCases.User;

public class GetUserByIdUseCase
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserResponse>> ExecuteAsync(Guid id)
    {
        var result = await _userRepository.GetByIdAsync(id);

        if (result == null)
            return Result<UserResponse>.Error("User not found");

        var response = result.MapToDto();

        return Result<UserResponse>.Success(response);
    }
}