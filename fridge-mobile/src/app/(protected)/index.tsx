import { View } from "react-native";
import React from "react";
import BackgroundLinear from "../../shared/components/background-linear";
import { SafeAreaView } from "react-native-safe-area-context";
import SearchBar from "../../features/cooking-book/components/search-bar";
import RecipePanel from "../../features/cooking-book/components/recipe-panel";
import type { RecipeItemDto } from "../../features/cooking-book/models/recipe-item.dto";
import type { TagItemDto } from "../../features/cooking-book/models/tag-item.dto";

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
  return (
    <BackgroundLinear>
      <SafeAreaView style={{ flex: 1 }}>
        <View style={{ paddingHorizontal: 20, paddingTop: 24, gap: 16 }}>
          <SearchBar />
        </View>
        <RecipePanel tags={tags} recipes={recipes} />
      </SafeAreaView>
    </BackgroundLinear>
  );
};

export default ProtectedIndex;
