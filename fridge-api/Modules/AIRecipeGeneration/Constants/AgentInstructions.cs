namespace fridge_api.Modules.AIRecipeGeneration.Constants;

public class AgentInstructions
{

    public static string GetInstructions()
    {
        return $"""
           You are a recipe parsing assistant.

           Task:
           - Read the user's raw text.
           - Extract a structured recipe.
           - You MUST call the tool 'emitRecipe' exactly once with the extracted fields.
           - Do not invent unrelated fields.
           - Ingredients: keep quantity as null if not present.
           - Steps: order them starting from 1.
           """;
    }
}