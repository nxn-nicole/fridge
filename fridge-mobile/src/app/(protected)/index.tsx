import { Pressable, Text, View } from "react-native";
import React from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import SearchBar from "../../features/cooking-book/components/search-bar";
import RecipePanel from "../../features/cooking-book/components/recipe-panel";
import { useRouter } from "expo-router";
import { Feather, FontAwesome5 } from "@expo/vector-icons";
import { useAllCategories } from "../../features/cooking-book/hooks/useCategoryQuery";

type Props = {};

const ProtectedIndex = (props: Props) => {
  const router = useRouter();
  const { categories } = useAllCategories();

  return (
    <SafeAreaView style={{ flex: 1 }} className="bg-[#FFFDE7]">
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
        <RecipePanel categories={categories} />
      </View>
      <View className="flex-row items-center justify-between pb-4 mx-6">
        <Pressable
          onPress={() => router.push("/(protected)/ai-recipe")}
          className="flex-row items-center gap-2 rounded-full bg-primary px-5 py-3"
        >
          <Text className="text-base font-semibold text-white">
            Describe Your Dish
          </Text>
          <FontAwesome5 name="utensils" size={16} color="#fff" />
        </Pressable>
        <Pressable
          onPress={() => router.push("/(protected)/add-recipe")}
          className="h-12 w-12 items-center justify-center rounded-full bg-[#F59E0B]"
        >
          <Feather name="book-open" size={20} color="#FFFFFF" />
        </Pressable>
      </View>
    </SafeAreaView>
  );
};

export default ProtectedIndex;
