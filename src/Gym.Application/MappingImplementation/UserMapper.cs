using Gym.Application.Dtos.User.Request;
using Gym.Application.Dtos.User.Response;
using Gym.Domain.Entities;

namespace Gym.Application.MappingImplementation;

public static class UserMapper
{
    public static UserResponse MapToDto(this User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };
    }

    public static User MapToEntity(this CreateUserRequest createDto, string passwordHash)
    {
        return new User(
            createDto.FirstName,
            createDto.LastName,
            createDto.Email,
            passwordHash
        );
    }
}