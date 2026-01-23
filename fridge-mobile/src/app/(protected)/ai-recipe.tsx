import { Pressable, Text, TextInput, View, ScrollView } from "react-native";
import React, { useState } from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import { useRouter } from "expo-router";
import { Feather } from "@expo/vector-icons";
import AiChatMessageCard from "../../features/ai-recipe/components/ai-chat-message-card";
import {
  useAddAiChatHistoryMutation,
  useAiChatHistoryQuery,
  useGenerateAiRecipeMutation,
} from "../../features/ai-recipe/hooks/useAiChatHistory";

type Props = {};

const AiRecipeScreen = (props: Props) => {
  const router = useRouter();
  const { history } = useAiChatHistoryQuery();
  const { addHistory, isAddingHistory } = useAddAiChatHistoryMutation();
  const { generateAiRecipe, isGeneratingAiRecipe } =
    useGenerateAiRecipeMutation();
  const [message, setMessage] = useState("");

  const handleSendMessage = () => {
    const trimmed = message.trim();
    if (!trimmed || isAddingHistory || isGeneratingAiRecipe) {
      return;
    }

    addHistory(
      { role: "user", message: trimmed },
      {
        onSuccess: () => {
          generateAiRecipe(trimmed);
          setMessage("");
        },
      },
    );
  };

  return (
    <SafeAreaView style={{ flex: 1 }} className="bg-[#FFFDE7]">
      <View className="px-6 pt-4">
        <Pressable
          onPress={() => router.push("/(protected)")}
          className="h-10 w-10 items-center justify-center rounded-full border border-[#54462B] bg-[#FEF8BE]"
        >
          <Feather name="arrow-left" size={20} color="#54462B" />
        </Pressable>
      </View>
      <ScrollView
        className="flex-1 px-6"
        contentContainerStyle={{ paddingTop: 16, paddingBottom: 24, gap: 12 }}
      >
        {[...history]
          .sort(
            (a, b) =>
              new Date(a.createdAtUtc).getTime() -
              new Date(b.createdAtUtc).getTime(),
          )
          .map((message) => (
          <AiChatMessageCard key={message.id} message={message} />
        ))}
      </ScrollView>
      <View className="border-t border-black/10 bg-[#FFFDE7] px-6 py-4">
        <View className="flex-row items-center gap-3 rounded-full border border-[#54462B]/20 bg-white px-4 py-2">
          <TextInput
            placeholder="Ask for a recipe..."
            className="flex-1 text-sm text-black"
            placeholderTextColor="#7A7363"
            value={message}
            onChangeText={setMessage}
          />
          <Pressable
            className="h-9 w-9 items-center justify-center rounded-full bg-[#54462B]"
            onPress={handleSendMessage}
            disabled={isAddingHistory || !message.trim()}
          >
            <Feather name="send" size={16} color="#FFFFFF" />
          </Pressable>
        </View>
      </View>
    </SafeAreaView>
  );
};

export default AiRecipeScreen;
