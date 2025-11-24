using Gym.Application.Dtos.User.Request;
using Gym.Application.Dtos.User.Response;
using Gym.Application.MappingImplementation;
using Gym.Domain.Abstractions.ResultPattern;
using Gym.Domain.Interfaces.Repositories;
using Gym.Domain.Interfaces.Shared;
using Gym.Domain.Interfaces.UnitOfWork;

namespace Gym.Application.Services.User;

public class UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    : IUserService
{
    public async Task<Result<UserResponse>> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var emailExists = await userRepository.EmailExistsAsync(request.Email);

        if (emailExists)
            return Result<UserResponse>.Error("Email already exists");

        string passwordHash = passwordHasher.Hash(request.Password);

        var entity = request.MapToEntity(passwordHash);

        await userRepository.CreateAsync(entity);
        await unitOfWork.SaveAsync();

        var response = entity.MapToDto();

        return response.ToSuccessResult();
    }

    public async Task<Result<PagedUsersResponse>> GetAllAsync(int page, int take, CancellationToken cancellationToken)
    {
        var result = await userRepository.GetAllAsync(page, take);

        var currentPage = result.page;
        var totalCount = result.totalCount;
        var pageSize = result.pageSize;

        if (result.users == null || !result.users.Any())
            return Result<PagedUsersResponse>.Error(
                page == 1
                    ? "No users found"
                    : "No users found for the requested page"
            );


        var viewUsers = result.users.Select(user => user.MapToDto()).ToList();

        var response = new PagedUsersResponse
        {
            Users = viewUsers,
            CurrentPage = currentPage,
            TotalCount = totalCount,
            PageSize = pageSize
        };

        return response.ToSuccessResult();
    }

    public async Task<Result<UserResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await userRepository.GetByIdAsync(id);

        if (result == null)
            return Result<UserResponse>.Error("User not found");

        var response = result.MapToDto();

        return Result<UserResponse>.Success(response);
    }

    public async Task<Result> UpdateAsync(Guid id, UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var result = await userRepository.GetByIdAsync(id);

        if (result == null)
            return Result.Error("User not found");

        if (!string.IsNullOrWhiteSpace(request.FirstName)) result.UpdateFirstName(request.FirstName);
        if (!string.IsNullOrWhiteSpace(request.LastName)) result.UpdateLastName(request.LastName);
        if (!string.IsNullOrWhiteSpace(request.Email)) result.UpdateEmail(request.Email);
        if (!string.IsNullOrWhiteSpace(request.Password)) result.UpdatePasswordHash(request.Password);

        userRepository.Update(result);
        await unitOfWork.SaveAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var userExists = await userRepository.GetByIdAsync(id);

        if (userExists == null)
            return Result.Error("User not found");

        userRepository.Delete(userExists);
        await unitOfWork.SaveAsync();

        return Result.Success();
    }
}