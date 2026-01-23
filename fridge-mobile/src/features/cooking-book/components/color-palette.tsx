import React from "react";
import { Pressable, View } from "react-native";

const COLORS = ["#D6B6DA", "#BAB53F", "#FF7C15", "#6FA7FF"] as const;

type Props = {
  value?: string | null;
  onChange?: (color: string) => void;
};

const ColorPalette = ({ value, onChange }: Props) => {
  return (
    <View className="rounded-2xl bg-white px-4 py-3">
      <View className="flex-row items-center">
        {COLORS.map((color) => {
          const isSelected = value === color;
          return (
            <Pressable
              key={color}
              onPress={() => onChange?.(color)}
              className="h-10 w-10 items-center justify-center"
            >
              <View
                className={`h-8 w-8 rounded-full ${
                  isSelected ? "border-2 border-black" : ""
                }`}
                style={{ backgroundColor: color }}
              />
            </Pressable>
          );
        })}
      </View>
    </View>
  );
};

export default ColorPalette;
