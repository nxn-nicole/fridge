import React, { useMemo, useState } from "react";
import { View } from "react-native";
import CategoryButton from "./category-button";
import RecipeList from "./recipe-list";
import type { CategoryDto } from "../models/category-dto";
import { useRecipesByCategory } from "../hooks/useRecipeQuery";

type Props = {
  categories?: CategoryDto[] | null;
};

const ALL_CATEGORY_ID = "all";

const RecipePanel = ({ categories }: Props) => {
  const tags = useMemo(() => {
    const normalized = categories ?? [];
    return [{ id: 0, title: "All", color: "#90BAFA" }, ...normalized];
  }, [categories]);

  const [activeCategory, setActiveCategory] = useState(ALL_CATEGORY_ID);
  const activeCategoryId =
    activeCategory === ALL_CATEGORY_ID ? undefined : Number(activeCategory);
  const { recipes } = useRecipesByCategory(activeCategoryId);

  return (
    <View className="flex-row mt-6 mr-6">
      <View className="w-16 gap-4">
        {tags.map((category) => (
          <CategoryButton
            key={`${category.id}-${category.title}`}
            title={category.title}
            color={category.color ?? "#FFFFF"}
            selected={
              category.title === "All"
                ? activeCategory === ALL_CATEGORY_ID
                : category.id.toString() === activeCategory
            }
            onPress={() =>
              setActiveCategory(
                category.title === "All"
                  ? ALL_CATEGORY_ID
                  : category.id.toString(),
              )
            }
          />
        ))}
      </View>
      <View className="ml-4 flex-1">
        <RecipeList items={recipes} />
      </View>
    </View>
  );
};

export default RecipePanel;
