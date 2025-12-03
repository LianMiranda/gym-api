using Gym.Application.Dtos.BodyMeasurement.Request;
using Gym.Application.Dtos.BodyMeasurement.Response;
using Gym.Domain.Abstractions.ResultPattern;

namespace Gym.Application.Services.BodyMeasurement;

public interface IBodyMeasurementService
{
    Task<Result<BodyMeasurementResponse>> CreateAsync(CreateBodyMeasurementRequest request, CancellationToken cancellationToken);
    Task<Result<PagedBodyMeasurementsResponse>> GetByUserIdAsync(Guid id, int page, int take, CancellationToken cancellationToken);
    Task<Result<BodyMeasurementResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(Guid id, UpdateBodyMeasurementRequest request, CancellationToken cancellationToken);
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken);
}