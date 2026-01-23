import React from "react";
import { Text, View } from "react-native";
import type { AiChatHistoryDto } from "../models/ai-chat-history-dto";

type Props = {
  message: AiChatHistoryDto;
};

const AiChatMessageCard = ({ message }: Props) => {
  const isUser = message.role === "user";
  const content = isUser ? message.userText : message.aiPayloadJson;

  return (
    <View className={isUser ? "items-end" : "items-start"}>
      <View
        className={`max-w-[80%] rounded-2xl px-4 py-3 ${
          isUser
            ? "bg-[#FFE1A3] border border-[#54462B]/30"
            : "bg-white border border-black/10"
        }`}
      >
        <Text className="text-sm text-[#2F2A1F]">
          {content ?? ""}
        </Text>
      </View>
    </View>
  );
};

export default AiChatMessageCard;
