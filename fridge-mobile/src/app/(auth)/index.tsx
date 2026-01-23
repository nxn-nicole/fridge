import { Text, View } from "react-native";
import React from "react";
import GetStartedButton from "../../features/auth/components/get-started-button";
import { SafeAreaView } from "react-native-safe-area-context";
import LottieView from "lottie-react-native";
import { LOTTIE_ANIMATIONS } from "../../shared/constants/assets";

type Props = {};

const GetStartedScreen = (props: Props) => {
  return (
    <SafeAreaView style={{ flex: 1 }} className="bg-[#FFFDE7]">
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
  );
};

export default GetStartedScreen;
