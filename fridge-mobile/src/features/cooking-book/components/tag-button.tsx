import React, { useEffect } from "react";
import { Pressable, Text } from "react-native";
import Animated, {
  useAnimatedStyle,
  useSharedValue,
  withTiming,
} from "react-native-reanimated";

type Props = {
  title: string;
  color: string;
  selected?: boolean;
  onPress?: () => void;
};

const TagButton = ({ title, color, selected = false, onPress }: Props) => {
  const width = useSharedValue(56);

  useEffect(() => {
    if (selected) {
      width.value = withTiming(64, { duration: 180 });
    } else {
      width.value = withTiming(56, { duration: 180 });
    }
  }, [selected, width]);

  const animatedStyle = useAnimatedStyle(() => {
    return {
      width: width.value,
    };
  });

  return (
    <Animated.View style={animatedStyle}>
      <Pressable
        onPress={onPress}
        className={`items-center justify-center rounded-tr-md rounded-br-md py-4 ${
          selected
            ? "border-t-2 border-r-2 border-b-2 border-black"
            : "border border-transparent"
        }`}
        style={{ backgroundColor: color }}
      >
        <Text className="text-base font-semibold text-black">{title}</Text>
      </Pressable>
    </Animated.View>
  );
};

export default TagButton;
