import React from "react";
import {
  ActivityIndicator,
  Pressable,
  Text,
  TextInput,
  View,
} from "react-native";
import { Controller, type Control, useWatch } from "react-hook-form";
import type { AddCategoryDto } from "../models/add-category-dto";
import ColorPalette from "./color-palette";
import Modal from "react-native-modal";

type Props = {
  control: Control<AddCategoryDto>;
  isAddingCategory: boolean;
  isValid: boolean;
  isVisible: boolean;
  onClose: () => void;
  onSubmit: () => void;
};

const AddCategoryForm = ({
  control,
  isAddingCategory,
  isValid,
  isVisible,
  onClose,
  onSubmit,
}: Props) => {
  const title = useWatch({ control, name: "title" });
  const color = useWatch({ control, name: "color" });
  const isDisabled = isAddingCategory || !title?.trim() || !color || !isValid;

  return (
    <Modal
      isVisible={isVisible}
      onBackdropPress={() => onClose()}
      backdropOpacity={0.4}
      animationIn="fadeIn"
      animationOut="fadeOut"
    >
      <View className="w-full rounded-2xl bg-white px-5 py-4">
        <Text className="text-lg font-semibold text-primary">Add Category</Text>
        <Controller
          control={control}
          name="title"
          rules={{ required: true }}
          render={({ field: { onBlur, onChange, value } }) => (
            <TextInput
              value={value}
              onBlur={onBlur}
              onChangeText={onChange}
              placeholder="Category name"
              className="mt-3 rounded-lg border border-black/10 text-black p-4"
            />
          )}
        />

        <Controller
          control={control}
          name="color"
          render={({ field: { onChange, value } }) => (
            <View className="mt-3">
              <ColorPalette value={value} onChange={onChange} />
            </View>
          )}
        />

        <View className="mt-4 flex-row items-center justify-end ">
          <Pressable
            onPress={onSubmit}
            className="min-w-[92px] items-center justify-center rounded-full px-4 py-2"
            style={{ backgroundColor: isDisabled ? "#9CA3AF" : "#54462B" }}
            disabled={isDisabled}
          >
            {isAddingCategory ? (
              <ActivityIndicator size="small" color="#FFFFFF" />
            ) : (
              <Text className="text-sm font-semibold text-white">Add</Text>
            )}
          </Pressable>
        </View>
      </View>
    </Modal>
  );
};

export default AddCategoryForm;
