using Gym.Application.Dtos.BodyMeasurement.Request;
using Gym.Application.Dtos.BodyMeasurement.Response;
using Gym.Domain.Entities;

namespace Gym.Application.MappingImplementation;

public static class BodyMeasurementMapper
{
    public static BodyMeasurementResponse MapToDto(this BodyMeasurement bm)
    {
        return new BodyMeasurementResponse
        {
            Id = bm.Id,
            Name = bm.Name,
            Biceps = bm.Biceps,
            BodyFat = bm.BodyFat,
            Calves = bm.Calves,
            Chest = bm.Chest,
            Height = bm.Height,
            Hips = bm.Hips,
            MeasurementDate = bm.MeasurementDate,
            Notes = bm.Notes,
            Thighs = bm.Thighs,
            Waist = bm.Waist,
            Weight = bm.Weight
        };
    }

    public static BodyMeasurement MapToEntity(this CreateBodyMeasurementRequest createDto)
    {
        return new BodyMeasurement(
            createDto.UserId,
            createDto.Name,
            createDto.MeasurementDate,
            createDto.Weight,
            createDto.Height,
            createDto.BodyFat,
            createDto.Chest,
            createDto.Waist,
            createDto.Hips,
            createDto.Biceps,
            createDto.Thighs,
            createDto.Calves,
            createDto.Notes
        );
    }
}