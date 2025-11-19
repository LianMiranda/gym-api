using System.Text;
using Gym.Application.Dtos.User;
using Gym.Application.Dtos.User.Response;
using Gym.Application.MappingImplementation;
using Gym.Domain.Abstractions.ResultPattern;
using Gym.Domain.Interfaces.Repositories;
using Gym.Domain.Interfaces.UnitOfWork;

namespace Gym.Application.UseCases.User;

public class GetAllUsersUseCase
{
    private readonly IUserRepository _repository;

    public GetAllUsersUseCase(IUserRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
    }

    public async Task<ResultData<PagedUsersResponse>> ExecuteAsync(int page, int take)
    {
        var result = await _repository.GetAllAsync(page, take);

        var currentPage = result.page;
        var totalCount = result.totalCount;
        var pageSize = result.pageSize;

        if (result.users == null || !result.users.Any())
            return ResultData<PagedUsersResponse>.Error(
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

        return ResultData<PagedUsersResponse>.Success(response);
    }
}