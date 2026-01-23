import { Pressable, Text, View } from "react-native";
import React from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import { useRouter } from "expo-router";
import { Feather } from "@expo/vector-icons";

type Props = {};

const AiRecipeScreen = (props: Props) => {
  const router = useRouter();

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
      <View className="flex-1 items-center justify-center">
        <Text>AiRecipeScreen</Text>
      </View>
    </SafeAreaView>
  );
};

export default AiRecipeScreen;
