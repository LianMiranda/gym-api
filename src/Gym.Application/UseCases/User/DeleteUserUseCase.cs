using Gym.Domain.Abstractions.ResultPattern;
using Gym.Domain.Interfaces.Repositories;
using Gym.Domain.Interfaces.UnitOfWork;

namespace Gym.Application.UseCases.User;

public class DeleteUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserUseCase(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultData<string>> ExecuteAsync(Guid id)
    {
        var userExists = await _userRepository.GetByIdAsync(id);

        if (userExists == null)
            return ResultData<string>.Error("User not found");

        _userRepository.Delete(userExists);
        await _unitOfWork.SaveAsync();

        return ResultData<string>.Success("User deleted successfully");
    }
}