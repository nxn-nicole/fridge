import React from "react";
import { useAuth0 } from "react-native-auth0";
import { useRouter } from "expo-router";
import * as SecureStore from "expo-secure-store";
import { Pressable, Text } from "react-native";
import {
  AUTH_TOKEN_KEY,
  REFRESH_TOKEN_KEY,
} from "../../../shared/constants/token-keys";

export default function GetStartedButton() {
  const { authorize } = useAuth0();
  const router = useRouter();

  const onPress = async () => {
    try {
      const result = await authorize({
        audience: process.env.EXPO_PUBLIC_AUTH0_AUDIENCE,
        scope: "openid profile email offline_access",
      });

      // Check if we have a valid result with access token
      if (result && result.accessToken && result.refreshToken) {
        // Save the access token to secure storage
        await SecureStore.setItemAsync(AUTH_TOKEN_KEY, result.accessToken);
        console.log("Access token saved to storage");
        await SecureStore.setItemAsync(REFRESH_TOKEN_KEY, result.refreshToken);
        console.log("Refresh token saved to storage");

        router.replace("/(protected)");
      } else {
        console.error("No access token received from Auth0");
      }
    } catch (e) {
      console.error("Auth0 authorization error:", e);
    }
  };

  return (
    <Pressable onPress={onPress} className="px-4 py-2 bg-blue-500 rounded">
      <Text>Get Started</Text>
    </Pressable>
  );
}
