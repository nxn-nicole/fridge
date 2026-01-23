import { Ionicons } from "@expo/vector-icons";
import React, { useEffect } from "react";
import { Pressable, Text, View } from "react-native";
import Animated, {
  cancelAnimation,
  interpolate,
  useAnimatedStyle,
  useSharedValue,
  withRepeat,
  withTiming,
} from "react-native-reanimated";

type Props = {
  title: string;
  color: string;
  selected?: boolean;
  onPress?: () => void;
  deleteCategory?: () => void;
  showDelete?: boolean;
  onDeletePress?: () => void;
};

const CategoryButton = ({
  title,
  color,
  selected = false,
  onPress,
  deleteCategory,
  showDelete = false,
  onDeletePress,
}: Props) => {
  const width = useSharedValue(40);
  const shake = useSharedValue(0);

  useEffect(() => {
    if (selected) {
      width.value = withTiming(50, { duration: 180 });
    } else {
      width.value = withTiming(40, { duration: 180 });
    }
  }, [selected, width]);

  useEffect(() => {
    if (showDelete) {
      shake.value = withRepeat(withTiming(1, { duration: 80 }), -1, true);
    } else {
      cancelAnimation(shake);
      shake.value = withTiming(0, { duration: 120 });
    }
  }, [showDelete, shake]);

  const animatedStyle = useAnimatedStyle(() => {
    const rotation = interpolate(shake.value, [0, 1], [0, 2]);
    return {
      width: width.value,
      transform: showDelete
        ? [{ rotateZ: `${rotation}deg` }]
        : [{ rotateZ: "0deg" }],
    };
  });

  return (
    <Animated.View style={animatedStyle} className="relative overflow-visible">
      <View className="overflow-hidden">
        <Pressable
          onPress={onPress}
          onLongPress={deleteCategory}
          className={`items-center justify-center rounded-tr-md rounded-br-md py-4 ${
            selected
              ? "border-t-2 border-r-2 border-b-2 border-black"
              : "border border-transparent"
          }`}
          style={{ backgroundColor: color }}
        >
          <Text className="text-base font-semibold text-black">{title}</Text>
        </Pressable>
      </View>
      {showDelete ? (
        <Pressable
          onPressIn={onDeletePress}
          className="absolute -right-1 -top-2 z-10 h-6 w-6 items-center justify-center rounded-full bg-red-500"
          hitSlop={8}
        >
          <Ionicons name="close" size={12} color={"#fff"} />
        </Pressable>
      ) : null}
    </Animated.View>
  );
};

export default CategoryButton;
