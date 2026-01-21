import { View, Text } from "react-native";
import React from "react";
import { Auth0Provider } from "react-native-auth0";
import { GestureHandlerRootView } from "react-native-gesture-handler";
import { SafeAreaProvider } from "react-native-safe-area-context";
import { KeyboardProvider } from "react-native-keyboard-controller";
import { Stack } from "expo-router";

type Props = {};

const RootLayout = (props: Props) => {
  const domain = process.env.EXPO_PUBLIC_AUTH0_DOMAIN!;
  const clientId = process.env.EXPO_PUBLIC_AUTH0_CLIENT_ID!;

  return (
    <Auth0Provider domain={domain} clientId={clientId}>
      <GestureHandlerRootView>
        <SafeAreaProvider>
          <KeyboardProvider>
            <Stack screenOptions={{ headerShown: false }}>
              <Stack.Screen name="index" options={{ headerShown: false }} />
              <Stack.Screen name="(auth)" options={{ headerShown: false }} />
              <Stack.Screen
                name="(protected)"
                options={{ headerShown: false }}
              />
            </Stack>
          </KeyboardProvider>
        </SafeAreaProvider>
      </GestureHandlerRootView>
    </Auth0Provider>
  );
};

export default RootLayout;
