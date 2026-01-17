using Gym.Application.Dtos.Exercise.Response;
using Gym.Application.Dtos.RoutineExercise.Request;
using Gym.Application.Dtos.RoutineExercise.Response;
using Gym.Domain.Entities;

namespace Gym.Application.MappingImplementation;

public static class RoutineExerciseMapper
{
    public static RoutineExerciseResponseDto ToDto(this RoutineExercise routine)
    {
        return new RoutineExerciseResponseDto(
            routine.Id,
            routine.OrderIndex,
            new ExerciseInRoutineDto(
                routine.Exercise.Name
            ),
            routine.Notes ?? string.Empty,
            routine.Sets
        );
    }

    public static RoutineExercise ToEntity(this CreateRoutineExerciseDto dto, Guid routineId, Guid exerciseId,
        sbyte maxOrderIndex)
    {
        return new RoutineExercise(
            routineId,
            exerciseId,
            maxOrderIndex,
            dto.Notes
        );
    }
}