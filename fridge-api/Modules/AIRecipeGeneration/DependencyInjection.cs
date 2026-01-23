using fridge_api.Modules.AIRecipeGeneration.Commands;
using fridge_api.Modules.AIRecipeGeneration.Queries;
using fridge_api.Modules.AIRecipeGeneration.Services;
using Microsoft.Extensions.DependencyInjection;

namespace fridge_api.Modules.AIRecipeGeneration;

public static class DependencyInjection
{
    public static IServiceCollection AddAIRecipeGenerationModule(this IServiceCollection services)
    {
        services.AddScoped<AIRecipeGenerationService>();
        services.AddScoped<AddAiChatHistoryCommand>();
        services.AddScoped<GetAiChatHistoryQuery>();

        return services;
    }
}
