using Gym.Application.Services.Auth;
using Gym.Application.Services.BodyMeasurement;
using Gym.Application.Services.Exercise;
using Gym.Application.Services.Routine;
using Gym.Application.Services.RoutineExercise;
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
        services.AddTransient<IRoutineService, RoutineService>();
        services.AddTransient<IRoutineExerciseService, RoutineExerciseService>();
        services.AddTransient<IExerciseService, ExerciseService>();
        return services;
    }
}