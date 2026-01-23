import { apiClient } from "../../../shared/api/client";
import type { AddCategoryDto } from "../models/add-category-dto";
import type { CategoryDto } from "../models/category-dto";

const CATEGORY_URL = `${process.env.EXPO_PUBLIC_URL_WITH_API}/categories`;

export const fetchAllCategories = async (): Promise<CategoryDto[]> => {
  try {
    const result: CategoryDto[] = await apiClient.get(CATEGORY_URL);
    return result;
  } catch {
    throw new Error("Failed to fetch categories.");
  }
};

export const addCategory = async (data: AddCategoryDto): Promise<string> => {
  try {
    const result: string = await apiClient.post(CATEGORY_URL, data);
    return result;
  } catch {
    throw new Error("Failed to add category.");
  }
};

export const deleteCategory = async (categoryId: number): Promise<void> => {
  try {
    await apiClient.delete(`${CATEGORY_URL}/${categoryId}`);
  } catch {
    throw new Error("Failed to delete category.");
  }
};
