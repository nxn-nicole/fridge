import { View, Text } from "react-native";
import React from "react";
import BackgroundLinear from "../../shared/components/background-linear";
import { SafeAreaView } from "react-native-safe-area-context";

type Props = {};

const AiRecipeScreen = (props: Props) => {
  return (
    <BackgroundLinear>
      <SafeAreaView style={{ flex: 1 }}>
        <View>
          <Text>AiRecipeScreen</Text>
        </View>
      </SafeAreaView>
    </BackgroundLinear>
  );
};

export default AiRecipeScreen;
