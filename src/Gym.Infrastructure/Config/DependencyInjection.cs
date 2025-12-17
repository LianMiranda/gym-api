using Gym.Domain.Interfaces.Repositories;
using Gym.Domain.Interfaces.Seeds;
using Gym.Domain.Interfaces.Services;
using Gym.Domain.Interfaces.Shared;
using Gym.Domain.Interfaces.UnitOfWork;
using Gym.Infrastructure.Database.Seeds;
using Gym.Infrastructure.Repositories;
using Gym.Infrastructure.Services;
using Gym.Infrastructure.Services.Security;
using Gym.Infrastructure.Transaction;
using Microsoft.Extensions.DependencyInjection;

namespace Gym.Infrastructure.Config;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IPasswordHasher, PasswordHasher>();
        services.AddTransient<ITokenService, TokenService>();
        services.AddTransient<IBodyMeasurementRepository, BodyMeasurementRepository>();
        services.AddTransient<IExerciseSeeder, ExerciseSeeder>();
        services.AddTransient<IWorkoutPlanRepository, WorkoutPlanRepository>();
        services.AddTransient<IRoutineRepository, RoutineRepository>();
        return services;
    }
}