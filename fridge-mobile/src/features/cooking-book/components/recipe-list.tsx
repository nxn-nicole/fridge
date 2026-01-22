import React from "react";
import { View } from "react-native";
import RecipeCard from "./recipe-card";
import type { RecipeItemSummaryDto } from "../models/recipe-item-summary-dto";

type Props = {
  items: RecipeItemSummaryDto[];
};

const RecipeList = ({ items }: Props) => {
  return (
    <View className="gap-3">
      {items.map((item) => (
        <RecipeCard
          key={item.id}
          title={item.title}
          picture={item.pictureUrl}
          description={item.description}
        />
      ))}
    </View>
  );
};

export default RecipeList;
