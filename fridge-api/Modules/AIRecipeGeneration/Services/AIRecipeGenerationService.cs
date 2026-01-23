using System.Text.Json;
using Azure.AI.Projects;
using Azure.AI.Projects.OpenAI;
using Azure.Identity;
using fridge_api.Modules.AIRecipeGeneration.Commands;
using fridge_api.Modules.AIRecipeGeneration.Constants;
using fridge_api.Modules.CookingRecipe.Commands;
using OpenAI.Responses;

namespace fridge_api.Modules.AIRecipeGeneration.Services;

public class AIRecipeGenerationService
{
    private readonly ILogger<AIRecipeGenerationService> _logger;
    private readonly AddAiChatHistoryCommand _addAiChatHistoryCommand;

    public AIRecipeGenerationService(
        ILogger<AIRecipeGenerationService> logger,
        AddAiChatHistoryCommand addAiChatHistoryCommand)
    {
        _logger = logger;
        _addAiChatHistoryCommand = addAiChatHistoryCommand;
    }

    public async Task<AIGeneratedRecipe> Generate(
        AiGenerateRecipeRequest request,
        CancellationToken ct)
    {
        _logger.LogInformation("Starting AI recipe generation for user {UserId}", request.UserId);
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
        _logger.LogInformation("Created AI agent version {AgentVersion}", agentVersion.Name);
        
        ProjectResponsesClient responseClient =
            projectClient.OpenAI.GetProjectResponsesClientForAgent(agentVersion.Name);
        
        // 4) Build request message
        ResponseItem aiResponse = ResponseItem.CreateUserMessageItem(
            $"Please parse the recipe from the following text and output it according to schema:\n\n{request.RawUserMessage}"
        );

        List<ResponseItem> inputItems = [aiResponse];

        bool functionCalled;
        ResponseResult response;

        string? recipeJson = null;
        do
        {
            _logger.LogInformation("Sending AI response request with {ItemCount} items", inputItems.Count);
            response = await responseClient.CreateResponseAsync(inputItems: inputItems);
            if (response.Status != ResponseStatus.Completed)
                throw new InvalidOperationException($"Response not completed. Status={response.Status}");

            functionCalled = false;

            foreach (ResponseItem responseItem in response.OutputItems)
            {
                inputItems.Add(responseItem);

                if (responseItem is FunctionCallResponseItem functionToolCall)
                {
                    _logger.LogInformation("Handling function call {FunctionName}", functionToolCall.FunctionName);

                    if (functionToolCall.FunctionName == RecipeParsingTools.emitRecipeTool.FunctionName)
                    {
                        recipeJson = functionToolCall.FunctionArguments.ToString();
                        _logger.LogInformation("Received recipe JSON from {FunctionName}", functionToolCall.FunctionName);
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

        _logger.LogInformation("Persisting AI chat history for user {UserId}", request.UserId);
        await _addAiChatHistoryCommand.ExecuteAsync(
            new AddAiChatHistoryRequest
            {
                UserId = request.UserId,
                Message = new AiChatMessageDto
                {
                    Role = "assistant",
                    Message = recipeJson,
                },
            },
            ct);
        _logger.LogInformation("AI chat history saved for user {UserId}", request.UserId);

        var recipe = JsonSerializer.Deserialize<AIGeneratedRecipe>(
            recipeJson,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        if (recipe is null)
            throw new InvalidOperationException("Failed to deserialize AIGeneratedRecipe.");

        _logger.LogInformation("Generated AI recipe {Title} for user {UserId}", recipe.Title, request.UserId);
        return recipe;

    }
    
}

public class AIGeneratedRecipe
{
    public string Title { get; set; }
    public IReadOnlyList<IngredientInputDto> Ingredients { get; init; } = new List<IngredientInputDto>();
    public IReadOnlyList<StepInputDto> Steps { get; init; } = new List<StepInputDto>();
}


public class AiGenerateRecipeRequest
{
    public required Guid UserId { get; init; }
    public required string RawUserMessage { get; init; }
}
