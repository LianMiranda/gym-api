using Gym.Application.Dtos.User;
using Gym.Application.MappingImplementation;
using Gym.Domain.Interfaces.Repositories;
using Gym.Domain.Interfaces.Shared;
using Gym.Domain.Interfaces.UnitOfWork;

namespace Gym.Application.UseCases.User;

public class CreateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserUseCase(IUserRepository userRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserResponse> ExecuteAsync(CreateUserRequest user)
    {
        var emailExists = await _userRepository.EmailExistsAsync(user.Email);

        if (emailExists)
            throw new InvalidOperationException("Email already exists");

        string passwordHash = _passwordHasher.Hash(user.Password);

        var entity = user.MapToEntity(passwordHash);

        await _userRepository.CreateAsync(entity);
        await _unitOfWork.SaveAsync();

        var response = entity.MapToDto();

        return response;
    }
}