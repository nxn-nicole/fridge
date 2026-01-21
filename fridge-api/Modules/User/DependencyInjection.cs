using fridge_api.Modules.User.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace fridge_api.Modules.User;

public static class DependencyInjection
{
    public static IServiceCollection AddUserModule(this IServiceCollection services)
    {
        services.AddScoped<GetorCreateUser>();

        return services;
    }
}
