using Gym.Application.Services.Auth;
using Gym.Application.Services.BodyMeasurement;
using Gym.Application.Services.User;
using Gym.Application.Services.WorkoutPlan;
using Microsoft.Extensions.DependencyInjection;

namespace Gym.Application.Config;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IBodyMeasurementService, BodyMeasurementService>();
        services.AddTransient<IWorkoutPlanService, WorkoutPlanService>();
        return services;
    }
}