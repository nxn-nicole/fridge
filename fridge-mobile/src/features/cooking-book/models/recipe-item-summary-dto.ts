import type { ImageSourcePropType } from "react-native";

export type RecipeItemSummaryDto = {
  id: string;
  title: string;
  pictureUrl?: ImageSourcePropType;
  categoryId?: string;
};
