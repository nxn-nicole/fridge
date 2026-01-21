import { View, Text } from "react-native";
import React from "react";
import GetStartedButton from "../../features/auth/components/get-started-button";

type Props = {};

const GetStartedScreen = (props: Props) => {
  return (
    <View className="flex-1 justify-center items-center">
      <Text>Get Started Screen</Text>
      <GetStartedButton />
    </View>
  );
};

export default GetStartedScreen;
