using System.Text.Json;
using System.Text.Json.Serialization;
using OpenAI.Responses;

namespace fridge_api.Modules.AIRecipeGeneration.Services;

public static class RecipeParsingTools
{
    private static readonly JsonSerializerOptions CamelCaseJson = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
    
    public static readonly FunctionTool emitRecipeTool = ResponseTool.CreateFunctionTool(
        functionName: "emitRecipe",
        functionDescription: "Extract and emit a structured recipe from the user provided raw text.",
        functionParameters: BinaryData.FromObjectAsJson(
            new
            {
                Type = "object",
                AdditionalProperties = false,
                Properties = new
                {
                    Title = new { Type = "string" },

                    Ingredients = new
                    {
                        Type = "array",
                        Items = new
                        {
                            Type = "object",
                            AdditionalProperties = false,
                            Properties = new
                            {
                                Title = new { Type = "string" },
                                Quantity = new { Type = "string", Nullable = true }
                            },
                            Required = new[] { "title", "quantity" }
                        }
                    },

                    Steps = new
                    {
                        Type = "array",
                        Items = new
                        {
                            Type = "object",
                            AdditionalProperties = false,
                            Properties = new
                            {
                                Order = new { Type = "integer" },
                                Description = new { Type = "string", Nullable = true }
                            },
                            Required = new[] { "order", "description" }
                        }
                    }
                },
                Required = new[] { "title", "ingredients", "steps" }
            },
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
        ),
        strictModeEnabled: true
    );

    
    private static AIGeneratedRecipe ValidateAndNormalizeRecipe(string functionArgumentsJson)
    {
        // Validate by deserializing into your DTO. You can add extra normalization here if needed.
        var recipe = JsonSerializer.Deserialize<AIGeneratedRecipe>(
            functionArgumentsJson,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        if (recipe is null)
            throw new InvalidOperationException("Model emitted invalid recipe JSON.");

        recipe.Title = (recipe.Title ?? string.Empty).Trim();

        // Example normalization: ensure step order is continuous if missing (optional)
        // You can keep it minimal for now.

        return recipe;
    }
    
    public static FunctionCallOutputResponseItem? GetResolvedToolOutput(FunctionCallResponseItem item)
    {
        if (item.FunctionName == emitRecipeTool.FunctionName)
        {
            // The model is expected to put the FINAL recipe in item.FunctionArguments.
            // We validate + normalize, then return as tool output.
            var recipe = ValidateAndNormalizeRecipe(item.FunctionArguments.ToString());

            var outputJson = JsonSerializer.Serialize(recipe, CamelCaseJson);
            return ResponseItem.CreateFunctionCallOutputItem(item.CallId, outputJson);
        }

        return null;
    }
}