using Gym.Application.UseCases.User;
using Microsoft.Extensions.DependencyInjection;

namespace Gym.Application.Config;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateUserUseCase>();
        
        return services;
    }
}