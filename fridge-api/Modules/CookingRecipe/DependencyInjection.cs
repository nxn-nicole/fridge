using fridge_api.Modules.CookingRecipe.Commands;
using fridge_api.Modules.CookingRecipe.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace fridge_api.Modules.CookingRecipe;

public static class DependencyInjection
{
    public static IServiceCollection AddCookingRecipeModule(this IServiceCollection services)
    {
        services.AddScoped<AddCookingRecipeCommand>();
        services.AddScoped<DeleteCookingRecipeCommand>();
        services.AddScoped<GetRecipesByCategoryQuery>();
        services.AddScoped<SearchRecipesByTitleQuery>();

        return services;
    }
}
