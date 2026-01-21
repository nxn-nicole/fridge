import { Text } from "react-native";
import React from "react";
import GetStartedButton from "../../features/auth/components/get-started-button";
import { SafeAreaView } from "react-native-safe-area-context";
import { View } from "react-native";

type Props = {};

const GetStartedScreen = (props: Props) => {
  return (
    <SafeAreaView style={{ flex: 1 }}>
      <View className="flex-1 justify-center items-center">
        <Text>Get Started Screen</Text>
        <GetStartedButton />
      </View>
    </SafeAreaView>
  );
};

export default GetStartedScreen;
