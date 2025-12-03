using Gym.Application.Dtos.BodyMeasurement.Request;
using Gym.Application.Dtos.BodyMeasurement.Response;
using Gym.Application.Dtos.User.Response;
using Gym.Application.MappingImplementation;
using Gym.Domain.Abstractions.ResultPattern;
using Gym.Domain.Interfaces.Repositories;
using Gym.Domain.Interfaces.UnitOfWork;

namespace Gym.Application.Services.BodyMeasurement;

public class BodyMeasurementService(IBodyMeasurementRepository repository, IUnitOfWork unitOfWork)
    : IBodyMeasurementService
{
    public async Task<Result<BodyMeasurementResponse>> CreateAsync(CreateBodyMeasurementRequest request,
        CancellationToken cancellationToken)
    {
        var entity = request.MapToEntity();

        await repository.CreateAsync(entity);
        await unitOfWork.SaveAsync();

        var response = entity.MapToDto();

        return response.ToSuccessResult();
    }

    public async Task<Result<PagedBodyMeasurementsResponse>> GetByUserIdAsync(Guid id, int page, int take,
        CancellationToken cancellationToken)
    {
        var result = await repository.GetByUserIdAsync(id, page, take);

        var currentPage = result.page;
        var totalCount = result.totalCount;
        var pageSize = result.pageSize;

        if (result.bodyMeasurements == null || !result.bodyMeasurements.Any())
            return Result<PagedBodyMeasurementsResponse>.Error(
                page == 1
                    ? "No body measurements found"
                    : "No body measurements found for the requested page"
            );

        var view = result.bodyMeasurements.Select(user => user.MapToDto()).ToList();

        var response = new PagedBodyMeasurementsResponse()
        {
            Response = view,
            CurrentPage = currentPage,
            TotalCount = totalCount,
            PageSize = pageSize
        };

        return response.ToSuccessResult();
    }

    public async Task<Result<BodyMeasurementResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await repository.GetByIdAsync(id);

        if (result == null)
            return Result<BodyMeasurementResponse>.Error("Body measurement not found");

        var response = result.MapToDto();

        return response.ToSuccessResult();
    }

    public async Task<Result> UpdateAsync(Guid id, UpdateBodyMeasurementRequest request,
        CancellationToken cancellationToken)
    {
        var result = await repository.GetByIdAsync(id);

        if (result == null)
            return Result.Error("Body measurement not found");

        if (!string.IsNullOrWhiteSpace(request.Name)) result.UpdateName(request.Name);
        if (request.MeasurementDate.HasValue)
            result.UpdateMeasurementDate(request.MeasurementDate.Value);
        if (IsValidDecimal(request.Weight))
            result.UpdateWeight(request.Weight!.Value);
        if (IsValidDecimal(request.Height))
            result.UpdateHeight(request.Height!.Value);
        if (IsValidDecimal(request.BodyFat))
            result.UpdateBodyFat(request.BodyFat!.Value);
        if (IsValidDecimal(request.Chest))
            result.UpdateChest(request.Chest!.Value);
        if (IsValidDecimal(request.Waist))
            result.UpdateWaist(request.Waist!.Value);
        if (IsValidDecimal(request.Hips))
            result.UpdateHips(request.Hips!.Value);
        if (IsValidDecimal(request.Biceps))
            result.UpdateBiceps(request.Biceps!.Value);
        if (IsValidDecimal(request.Thighs))
            result.UpdateThighs(request.Thighs!.Value);
        if (IsValidDecimal(request.Calves))
            result.UpdateCalves(request.Calves!.Value);
        if (!string.IsNullOrWhiteSpace(request.Notes))
            result.UpdateNotes(request.Notes);

        repository.Update(result);
        await unitOfWork.SaveAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var bodyMeasurementExists = await repository.GetByIdAsync(id);

        if (bodyMeasurementExists == null)
            return Result.Error("Body Measurement not found");

        repository.Delete(bodyMeasurementExists);
        await unitOfWork.SaveAsync();

        return Result.Success();
    }

    private bool IsValidDecimal(decimal? value)
    {
        return value.HasValue && value.Value > 0;
    }
}