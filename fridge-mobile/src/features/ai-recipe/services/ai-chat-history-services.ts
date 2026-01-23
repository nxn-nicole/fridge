import { apiClient } from "../../../shared/api/client";
import type { AddAiChatHistoryDto } from "../models/add-ai-chat-history-dto";
import type { AiChatHistoryDto } from "../models/ai-chat-history-dto";
import type { AiGeneratedRecipeDto } from "../models/ai-generated-recipe-dto";

const AI_CHAT_HISTORY_URL = `${process.env.EXPO_PUBLIC_URL_WITH_API}/airecipe/history`;
const AI_RECIPE_URL = `${process.env.EXPO_PUBLIC_URL_WITH_API}/airecipe`;

export const addAiChatHistory = async (
  data: AddAiChatHistoryDto,
): Promise<number> => {
  try {
    const result: number = await apiClient.post(AI_CHAT_HISTORY_URL, data);
    return result;
  } catch {
    throw new Error("Failed to add AI chat history.");
  }
};

export const fetchAiChatHistory = async (): Promise<AiChatHistoryDto[]> => {
  try {
    const result: AiChatHistoryDto[] = await apiClient.get(AI_CHAT_HISTORY_URL);
    return result;
  } catch {
    throw new Error("Failed to fetch AI chat history.");
  }
};

export const generateAiRecipe = async (
  rawUserMessage: string,
): Promise<AiGeneratedRecipeDto> => {
  try {
    const result: AiGeneratedRecipeDto = await apiClient.post(
      AI_RECIPE_URL,
      rawUserMessage,
    );
    return result;
  } catch {
    throw new Error("Failed to generate AI recipe.");
  }
};
