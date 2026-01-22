import React from "react";
import { View } from "react-native";
import RecipeCard from "./recipe-card";
import type { RecipeItemDto } from "../models/recipe-item.dto";

type Props = {
  items: RecipeItemDto[];
};

const RecipeList = ({ items }: Props) => {
  return (
    <View className="gap-3">
      {items.map((item) => (
        <RecipeCard
          key={item.id}
          title={item.title}
          picture={item.picture}
          description={item.description}
        />
      ))}
    </View>
  );
};

export default RecipeList;
