import React, { useMemo, useState } from "react";
import { View } from "react-native";
import TagButton from "./tag-button";
import RecipeList from "./recipe-list";
import type { RecipeItemDto } from "../models/recipe-item.dto";
import type { TagItemDto } from "../models/tag-item.dto";

type Props = {
  tags: TagItemDto[];
  recipes: (RecipeItemDto & { tagKey: string })[];
};

const RecipePanel = ({ tags, recipes }: Props) => {
  const [activeTag, setActiveTag] = useState(tags[0]?.key);

  const filteredRecipes = useMemo(() => {
    if (!activeTag) return recipes;
    return recipes.filter((recipe) => recipe.tagKey === activeTag);
  }, [activeTag, recipes]);

  return (
    <View className="flex-row mt-6 mr-6">
      <View className="w-16 gap-4">
        {tags.map((tag) => (
          <TagButton
            key={tag.key}
            title={tag.title}
            color={tag.color}
            selected={tag.key === activeTag}
            onPress={() => setActiveTag(tag.key)}
          />
        ))}
      </View>
      <View className="ml-4 flex-1">
        <RecipeList items={filteredRecipes} />
      </View>
    </View>
  );
};

export default RecipePanel;
