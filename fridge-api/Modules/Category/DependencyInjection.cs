using fridge_api.Modules.Category.Commands;
using fridge_api.Modules.Category.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace fridge_api.Modules.Category;

public static class DependencyInjection
{
    public static IServiceCollection AddCategoryModule(this IServiceCollection services)
    {
        services.AddScoped<AddCategoryCommand>();
        services.AddScoped<DeleteCategoryCommand>();
        services.AddScoped<GetAllCategoriesQuery>();

        return services;
    }
}
