import React from "react";
import { Stack } from "expo-router";

type Props = {};

const ProtectedLayout = (props: Props) => {
  return (
    <Stack screenOptions={{ headerShown: false }}>
      <Stack.Screen name="index" options={{ headerShown: false }} />
      <Stack.Screen name="add-recipe" options={{ headerShown: false }} />
      <Stack.Screen name="ai-recipe" options={{ headerShown: false }} />
      <Stack.Screen name="profile" options={{ headerShown: false }} />
    </Stack>
  );
};

export default ProtectedLayout;
