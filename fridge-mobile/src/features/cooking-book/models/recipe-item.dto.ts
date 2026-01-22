import type { ImageSourcePropType } from "react-native";

export type RecipeItemDto = {
  id: string;
  title: string;
  picture: ImageSourcePropType;
  description?: string;
  tagKey?: string;
};
