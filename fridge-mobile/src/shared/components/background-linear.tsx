import React from "react";
import { LinearGradient } from "expo-linear-gradient";
import { ColorValue } from "react-native";

type GradientColors = Readonly<[ColorValue, ColorValue, ...ColorValue[]]>;
type Props = { colors?: GradientColors; children: React.ReactNode };

const BackgroundLinear = ({
  colors = ["#FEF8BE", "#FFFFFF"] as const,
  children,
}: Props) => {
  return (
    <LinearGradient
      colors={colors}
      end={{ x: 0, y: 1 }}
      start={{ x: 1, y: 0 }}
      style={{ flex: 1 }}
    >
      {children}
    </LinearGradient>
  );
};

export default BackgroundLinear;
