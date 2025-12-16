using Gym.Application.Dtos.WorkoutPlan.Request;
using Gym.Application.Dtos.WorkoutPlan.Response;
using Gym.Domain.Entities;

namespace Gym.Application.MappingImplementation;

public static class WorkoutPlanMapper
{
    public static WorkoutPlanResponseDto ToDto(this WorkoutPlan wp)
    {
        return new WorkoutPlanResponseDto
        {
            Id = wp.Id,
            Name = wp.Name,
            Description = wp.Description,
            DaysPerWeek = wp.DaysPerWeek,
            Months = wp.Months,
            Goal = wp.Goal
        };
    }

    public static WorkoutPlan ToEntity(this CreateWorkoutDto dto)
    {
        return new WorkoutPlan(dto.UserId, dto.Name, dto.Description, dto.DaysPerWeek, dto.Months, dto.Goal);
    }
}