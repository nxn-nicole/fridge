import React, { useState } from "react";
import { Pressable, Text, View } from "react-native";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import CategoryButton from "./category-button";
import AddCategoryForm from "./add-category-form";
import RecipeList from "./recipe-list";
import type { CategoryDto } from "../models/category-dto";
import { useRecipesByCategory } from "../hooks/useRecipeQuery";
import {
  useAddCategory,
  useDeleteCategory,
} from "../hooks/useCategoryMuatation";
import { addCategorySchema } from "../models/add-category-schema";
import { AddCategoryDto } from "../models/add-category-dto";
import { Ionicons } from "@expo/vector-icons";

type Props = {
  categories?: CategoryDto[] | null;
};

const ALL_CATEGORY_ID = "all";

const defaultCategory = {
  title: "",
  color: "",
};

const RecipePanel = ({ categories }: Props) => {
  const allCategories = [
    { id: 0, title: "All", color: "#90BAFA" },
    ...(categories ?? []),
  ];

  const [activeCategory, setActiveCategory] = useState(ALL_CATEGORY_ID);
  const [isAddModalOpen, setIsAddModalOpen] = useState(false);
  const [deleteCategoryId, setDeleteCategoryId] = useState<string | null>(null);
  const { addCategory, isAddingCategory } = useAddCategory();
  const { deleteCategory, isDeleteingCategory } = useDeleteCategory();

  const {
    control,
    handleSubmit,
    reset,
    formState: { isValid },
  } = useForm<AddCategoryDto>({
    resolver: zodResolver(addCategorySchema),
    mode: "onChange",
    defaultValues: defaultCategory,
  });
  const activeCategoryId =
    activeCategory === ALL_CATEGORY_ID ? undefined : Number(activeCategory);
  const { recipes } = useRecipesByCategory(activeCategoryId);

  const handleOpenAddCategory = () => {
    setIsAddModalOpen(true);
  };

  const handleDismissDelete = () => {
    if (!deleteCategoryId) {
      return false;
    }

    setTimeout(() => {
      setDeleteCategoryId(null);
    }, 0);

    return false;
  };

  const handleSubmitAddCategory = (data: AddCategoryDto) => {
    const title = data.title.trim();
    if (!title || isAddingCategory) {
      return;
    }

    addCategory(data, {
      onSuccess: () => {
        setIsAddModalOpen(false);
        reset(defaultCategory);
      },
    });
  };

  const handleCloseAddCategory = () => {
    setIsAddModalOpen(false);
    reset(defaultCategory);
  };

  const handleDeleteCategory = (categoryId: number) => {
    console.log("deleting category id:", categoryId);
    if (isDeleteingCategory) {
      return;
    }

    deleteCategory(categoryId, {
      onSuccess: () => {
        setDeleteCategoryId(null);
      },
    });
  };

  return (
    <View
      className="flex-row mt-6 mr-6"
      onStartShouldSetResponderCapture={handleDismissDelete}
    >
      <View className="w-16 gap-4">
        {allCategories.map((category) => (
          <CategoryButton
            key={`${category.id}-${category.title}`}
            title={category.title}
            color={category.color ?? "#FFFFF"}
            showDelete={
              category.id !== 0 && deleteCategoryId === category.id.toString()
            }
            selected={
              category.id === 0
                ? activeCategory === ALL_CATEGORY_ID
                : category.id.toString() === activeCategory
            }
            onPress={() =>
              setActiveCategory(
                category.id === 0 ? ALL_CATEGORY_ID : category.id.toString(),
              )
            }
            deleteCategory={() => {
              if (category.id === 0) {
                return;
              }
              setDeleteCategoryId(category.id.toString());
            }}
            onDeletePress={() => {
              if (category.id === 0) {
                return;
              }
              handleDeleteCategory(category.id);
            }}
          />
        ))}
        <Pressable
          onPress={handleOpenAddCategory}
          className="w-10 items-center justify-center rounded-tr-md rounded-br-md border-black bg-[#D8D2C4] py-4"
        >
          <Ionicons name="add" size={24} color="black" />
        </Pressable>
      </View>
      <View className="ml-4 flex-1">
        <RecipeList items={recipes} />
      </View>

      <AddCategoryForm
        control={control}
        isAddingCategory={isAddingCategory}
        isValid={isValid}
        isVisible={isAddModalOpen}
        onClose={handleCloseAddCategory}
        onSubmit={handleSubmit(handleSubmitAddCategory)}
      />
    </View>
  );
};

export default RecipePanel;
