import React from "react";
import { TextInput, View } from "react-native";
import { Feather } from "@expo/vector-icons";

type Props = {
  value?: string;
  onChangeText?: (text: string) => void;
  placeholder?: string;
};

const SearchBar = ({
  value,
  onChangeText,
  placeholder = "Search recipe",
}: Props) => {
  return (
    <View className="h-12 flex-row items-center rounded-full bg-white px-4 shadow-lg shadow-black/10">
      <Feather name="search" size={20} color="#1C1B1F" />
      <TextInput
        value={value}
        onChangeText={onChangeText}
        placeholder={placeholder}
        placeholderTextColor="#9CA3AF"
        className="ml-3 flex-1 text-[#1C1B1F] mb-1 py-2"
        autoCorrect={false}
        returnKeyType="search"
      />
    </View>
  );
};

export default SearchBar;
