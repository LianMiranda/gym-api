using Gym.Application.Services.User;
using Microsoft.Extensions.DependencyInjection;

namespace Gym.Application.Config;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        return services;
    }
}