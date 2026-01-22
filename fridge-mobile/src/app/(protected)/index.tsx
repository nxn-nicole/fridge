import { Text, View } from "react-native";
import React from "react";
import BackgroundLinear from "../../shared/components/background-linear";
import { SafeAreaView } from "react-native-safe-area-context";
import SearchBar from "../../features/cooking-book/components/search-bar";

type Props = {};

const ProtectedIndex = (props: Props) => {
  return (
    <BackgroundLinear>
      <SafeAreaView style={{ flex: 1 }}>
        <View style={{ paddingHorizontal: 20, paddingTop: 24 }}>
          <SearchBar />
        </View>
      </SafeAreaView>
    </BackgroundLinear>
  );
};

export default ProtectedIndex;
