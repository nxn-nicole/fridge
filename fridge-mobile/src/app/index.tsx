import { View, Text, ActivityIndicator } from "react-native";
import React, { useEffect, useState } from "react";
import * as SecureStore from "expo-secure-store";
import { AUTH_TOKEN_KEY } from "../shared/constants/token-keys";
import { Redirect } from "expo-router";

type Props = {};

const Index = (props: Props) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [isAuthenticateLoading, setIsAuthenticateLoading] = useState(true);

  useEffect(() => {
    const initialize = async () => {
      try {
        const token = await SecureStore.getItemAsync(AUTH_TOKEN_KEY);

        if (!token) {
          setIsAuthenticated(false);
          return;
        }

        setIsAuthenticated(true);
      } catch (error) {
        console.error("Error during initialization:", error);
        setIsAuthenticated(false);
      } finally {
        setIsAuthenticateLoading(false);
      }
    };

    initialize();
  }, []);

  if (isAuthenticateLoading) {
    return (
      <View style={{ flex: 1, justifyContent: "center", alignItems: "center" }}>
        <ActivityIndicator size="large" color="#667eea" />
      </View>
    );
  }

  if (isAuthenticated) {
    return <Redirect href={"/(protected)"} />;
  }

  return <Redirect href={"/(auth)"} />;
};

export default Index;
