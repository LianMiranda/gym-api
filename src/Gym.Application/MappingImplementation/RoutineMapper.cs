using Gym.Application.Dtos.Routine.Request;
using Gym.Application.Dtos.Routine.Response;
using Gym.Domain.Entities;

namespace Gym.Application.MappingImplementation;

public static class RoutineMapper
{
    public static RoutineResponseDto ToDto(this Routine routine)
    {
        return new RoutineResponseDto(
            routine.Name,
            routine.Description,
            routine.ImageUrl,
            routine.OrderIndex
        );
    }

    public static Routine ToEntity(this CreateRoutineDto dto,  Guid workoutPlanId)
    {
        return new Routine(
            workoutPlanId,
            dto.Name,
            dto.Description,
            dto.ImageUrl,
            dto.OrderIndex
        );
    }
}