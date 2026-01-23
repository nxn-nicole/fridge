import { Pressable, Text, View } from "react-native";
import React from "react";
import { useRouter } from "expo-router";
import { Feather } from "@expo/vector-icons";
import { SafeAreaView } from "react-native-safe-area-context";

type Props = {};

const ProfileScreen = (props: Props) => {
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
        <Text>ProfileScreen</Text>
      </View>
    </SafeAreaView>
  );
};

export default ProfileScreen;
