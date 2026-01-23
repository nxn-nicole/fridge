import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import {
  addAiChatHistory,
  fetchAiChatHistory,
  generateAiRecipe,
} from "../services/ai-chat-history-services";
import type { AddAiChatHistoryDto } from "../models/add-ai-chat-history-dto";
import { aiChatHistoryKeys } from "../../../shared/constants/query-key-factory";

export const useAiChatHistoryQuery = () => {
  const { data, isLoading } = useQuery({
    queryKey: aiChatHistoryKeys.all,
    queryFn: () => fetchAiChatHistory(),
  });

  return {
    history: data ?? [],
    isLoading,
  };
};

export const useAddAiChatHistoryMutation = () => {
  const queryClient = useQueryClient();
  const { mutate, isPending } = useMutation({
    mutationFn: (data: AddAiChatHistoryDto) => addAiChatHistory(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: aiChatHistoryKeys.all });
    },
  });

  return {
    addHistory: mutate,
    isAddingHistory: isPending,
  };
};

export const useGenerateAiRecipeMutation = () => {
  const queryClient = useQueryClient();
  const { mutate, isPending } = useMutation({
    mutationFn: (rawUserMessage: string) => generateAiRecipe(rawUserMessage),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: aiChatHistoryKeys.all });
    },
  });

  return {
    generateAiRecipe: mutate,
    isGeneratingAiRecipe: isPending,
  };
};
