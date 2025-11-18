using Gym.Domain.Interfaces.Repositories;
using Gym.Domain.Interfaces.UnitOfWork;
using Gym.Infrastructure.Repositories;
using Gym.Infrastructure.Transaction;
using Microsoft.Extensions.DependencyInjection;

namespace Gym.Infrastructure.Config;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }
}