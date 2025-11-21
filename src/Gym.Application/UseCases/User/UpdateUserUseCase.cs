using Gym.Application.Dtos.User.Request;
using Gym.Application.Dtos.User.Response;
using Gym.Domain.Abstractions.ResultPattern;
using Gym.Domain.Interfaces.Repositories;
using Gym.Domain.Interfaces.UnitOfWork;

namespace Gym.Application.UseCases.User;

public class UpdateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserUseCase(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultData<UpdateUserResponse>> ExecuteAsync(Guid id, UpdateUserRequest request)
    {
        var result = await _userRepository.GetByIdAsync(id);

        if (result == null)
            return ResultData<UpdateUserResponse>.Error("User not found");

        if (!string.IsNullOrWhiteSpace(request.FirstName)) result.UpdateFirstName(request.FirstName);
        if (!string.IsNullOrWhiteSpace(request.LastName)) result.UpdateLastName(request.LastName);
        if (!string.IsNullOrWhiteSpace(request.Email)) result.UpdateEmail(request.Email);
        if (!string.IsNullOrWhiteSpace(request.Password)) result.UpdatePasswordHash(request.Password);

        _userRepository.Update(result);
        await _unitOfWork.SaveAsync();

        var response = new UpdateUserResponse
        {
            Message = "User updated successfully."
        };

        return ResultData<UpdateUserResponse>.Success(response);
    }
}