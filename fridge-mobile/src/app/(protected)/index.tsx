import { Pressable, Text, View } from "react-native";
import React from "react";
import BackgroundLinear from "../../shared/components/background-linear";
import { SafeAreaView } from "react-native-safe-area-context";
import SearchBar from "../../features/cooking-book/components/search-bar";
import RecipePanel from "../../features/cooking-book/components/recipe-panel";
import type { RecipeItemDto } from "../../features/cooking-book/models/recipe-item.dto";
import type { TagItemDto } from "../../features/cooking-book/models/tag-item.dto";
import { useRouter } from "expo-router";
import { Feather, FontAwesome5 } from "@expo/vector-icons";

type Props = {};

const tags: TagItemDto[] = [
  { key: "all", title: "All", color: "#9CC3FF" },
  { key: "meat", title: "Meat", color: "#E3C3FF" },
  { key: "spicy", title: "Spicy", color: "#FF8C86" },
  { key: "green", title: "Veg", color: "#B9E7A7" },
];

const recipes: (RecipeItemDto & { tagKey: string })[] = [
  {
    id: "ribs-1",
    title: "Braised pork ribs",
    description: "Braised pork ribs are tender, slow-cooked ribs simmered in a",
    picture: require("../../../assets/icon.png"),
    tagKey: "all",
  },
  {
    id: "ribs-2",
    title: "Braised pork ribs",
    picture: require("../../../assets/icon.png"),
    tagKey: "meat",
  },
  {
    id: "ribs-3",
    title: "Braised pork ribs",
    picture: require("../../../assets/icon.png"),
    tagKey: "spicy",
  },
  {
    id: "ribs-4",
    title: "Braised pork ribs",
    picture: require("../../../assets/icon.png"),
    tagKey: "green",
  },
];

const ProtectedIndex = (props: Props) => {
  const router = useRouter();

  return (
    <BackgroundLinear>
      <SafeAreaView style={{ flex: 1 }}>
        <View className="mx-6 mt-2 flex-row items-center gap-3">
          <View className="flex-1">
            <SearchBar />
          </View>
          <Pressable
            onPress={() => router.push("/(protected)/profile")}
            className="h-12 w-12 items-center justify-center rounded-full border-2 border-[#54462B] bg-[#FEF8BE]"
          >
            <Feather name="user" size={20} color="#54462B" />
          </Pressable>
        </View>
        <View className="mt-4 flex-1">
          <RecipePanel tags={tags} recipes={recipes} />
        </View>
        <View className="flex-row items-center justify-between pb-4 mx-6">
          <Pressable
            onPress={() => router.push("/(protected)/ai-recipe")}
            className="flex-row items-center gap-2 rounded-full border-2 border-[#54462B] bg-[#FEF8BE] px-5 py-3"
          >
            <Text className="text-base font-semibold text-[#54462B]">
              Describe Your Dish
            </Text>
            <FontAwesome5 name="utensils" size={16} color="#54462B" />
          </Pressable>
          <Pressable
            onPress={() => router.push("/(protected)/add-recipe")}
            className="h-12 w-12 items-center justify-center rounded-full bg-[#F59E0B]"
          >
            <Feather name="book-open" size={20} color="#FFFFFF" />
          </Pressable>
        </View>
      </SafeAreaView>
    </BackgroundLinear>
  );
};

export default ProtectedIndex;
