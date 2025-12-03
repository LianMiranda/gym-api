using Gym.Domain.Entities;

namespace Gym.Domain.Interfaces.Repositories;

public interface IBodyMeasurementRepository
{
        Task CreateAsync(BodyMeasurement bodyMeasurement);
        void Delete(BodyMeasurement bodyMeasurement);
        void Update(BodyMeasurement bodyMeasurement);
        Task<BodyMeasurement?> GetByIdAsync(Guid id);
        Task<(IEnumerable<BodyMeasurement>? bodyMeasurements, int totalCount, int page, int pageSize)> GetByUserIdAsync(Guid userId, int page, int take);
}