import React from "react";
import { Image, Text, View, type ImageSourcePropType } from "react-native";

type Props = {
  title: string;
  picture: ImageSourcePropType;
  description?: string;
};

const RecipeCard = ({ title, picture, description }: Props) => {
  return (
    <View className="flex-row items-center rounded-2xl bg-white p-3 shadow-lg shadow-black/10">
      <Image source={picture} className="h-16 w-16 rounded-xl" />
      <View className="ml-3 flex-1">
        <Text className="text-base font-semibold text-black">{title}</Text>
        {description ? (
          <Text className="mt-1 text-sm text-gray-500" numberOfLines={2}>
            {description}
          </Text>
        ) : null}
      </View>
    </View>
  );
};

export default RecipeCard;
