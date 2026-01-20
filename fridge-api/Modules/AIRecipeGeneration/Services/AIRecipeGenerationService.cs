using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.AI.Projects;
using Azure.AI.Projects.OpenAI;
using Azure.Identity;
using fridge_api.Modules.AIRecipeGeneration.Constants;
using fridge_api.Modules.Category.Queries;
using fridge_api.Modules.CookingRecipe.Commands;
using OpenAI.Responses;

namespace fridge_api.Modules.AIRecipeGeneration.Services;

public class AIRecipeGenerationService
{
    private readonly ILogger<AIRecipeGenerationService> _logger;

    public AIRecipeGenerationService(ILogger<AIRecipeGenerationService> logger)
    {
        _logger = logger;
    }

    public async Task<AIGeneratedRecipe> Generate(RawRecipeTextDto rawRecipeTextDto, CancellationToken ct)
    {
        string endpoint = "https://fridge-foundry.services.ai.azure.com/api/projects/foundry-ai-project";
        
        AIProjectClient projectClient = new(
            endpoint: new Uri(endpoint), 
            tokenProvider: new DefaultAzureCredential());
        
        var instruction = AgentInstructions.GetInstructions();
        
        PromptAgentDefinition agentDefinition = new(model: "gpt-5.2-chat")
        {
            Instructions = instruction,
            Tools = { RecipeParsingTools.emitRecipeTool }
        };

        // 2) Create agent version
        AgentVersion agentVersion = await projectClient.Agents.CreateAgentVersionAsync(
            agentName: "recipeParserAgent",
            options: new(agentDefinition));
        
        ProjectResponsesClient responseClient =
            projectClient.OpenAI.GetProjectResponsesClientForAgent(agentVersion.Name);
        
        // 4) Build request message
        ResponseItem request = ResponseItem.CreateUserMessageItem(
            $"Please parse the recipe from the following text and output it according to schema:\n\n{rawRecipeTextDto.Text}"
        );

        List<ResponseItem> inputItems = [request];

        bool functionCalled;
        ResponseResult response;

        string? recipeJson = null;
        do
        {
            response = await responseClient.CreateResponseAsync(inputItems: inputItems);
            if (response.Status != ResponseStatus.Completed)
                throw new InvalidOperationException($"Response not completed. Status={response.Status}");

            functionCalled = false;

            foreach (ResponseItem responseItem in response.OutputItems)
            {
                inputItems.Add(responseItem);

                if (responseItem is FunctionCallResponseItem functionToolCall)
                {
                    Console.WriteLine($"Calling {functionToolCall.FunctionName}...");

                    if (functionToolCall.FunctionName == RecipeParsingTools.emitRecipeTool.FunctionName)
                    {
                        recipeJson = functionToolCall.FunctionArguments.ToString();;
                    }

                    var outputItem = RecipeParsingTools.GetResolvedToolOutput(functionToolCall);
                    if (outputItem is not null)
                    {
                        inputItems.Add(outputItem);
                        functionCalled = true;
                    }
                }
            }
        } while (functionCalled);
        
        
        if (recipeJson is null)
            throw new InvalidOperationException("Model did not call emitRecipe; no recipe JSON was produced.");

        var recipe = JsonSerializer.Deserialize<AIGeneratedRecipe>(
            recipeJson,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        if (recipe is null)
            throw new InvalidOperationException("Failed to deserialize AIGeneratedRecipe.");

        return recipe;

    }
    
}

public class AIGeneratedRecipe
{
    public string Title { get; set; }
    public IReadOnlyList<IngredientInputDto> Ingredients { get; init; } = new List<IngredientInputDto>();
    public IReadOnlyList<StepInputDto> Steps { get; init; } = new List<StepInputDto>();
}

public class RawRecipeTextDto
{
    public string Text { get; init; }
}