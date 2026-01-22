import { Text } from "react-native";
import React from "react";
import GetStartedButton from "../../features/auth/components/get-started-button";
import { SafeAreaView } from "react-native-safe-area-context";
import { View } from "react-native";
import BackgroundLinear from "../../shared/components/background-linear";
import LottieView from "lottie-react-native";
import { LOTTIE_ANIMATIONS } from "../../shared/constants/assets";

type Props = {};

const GetStartedScreen = (props: Props) => {
  return (
    <BackgroundLinear>
      <SafeAreaView style={{ flex: 1 }}>
        <View className="flex-1 justify-center items-center">
          <LottieView
            source={LOTTIE_ANIMATIONS.kitchen}
            autoPlay
            loop
            style={{ width: 400, height: 400, marginBottom: 200 }}
          />
          <GetStartedButton />
        </View>
      </SafeAreaView>
    </BackgroundLinear>
  );
};

export default GetStartedScreen;
