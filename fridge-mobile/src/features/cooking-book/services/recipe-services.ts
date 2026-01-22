import { apiClient } from "../../../shared/api/client";
import type { RecipeItemSummaryDto } from "../models/recipe-item-summary-dto";
import { AddCookingRecipeDto } from "../models/add-cooking-recipe-dto";

const RECIPES_URL = `${process.env.EXPO_PUBLIC_URL_WITH_API}/recipes`;
const RECIPES_BY_CATEGORY_URL = `${RECIPES_URL}/by-category`;
const RECIPES_SEARCH_URL = `${RECIPES_URL}/search`;

export const fetchRecipesByCategory = async (
  categoryId?: number | null,
): Promise<RecipeItemSummaryDto[]> => {
  const url =
    categoryId !== null && categoryId !== undefined
      ? `${RECIPES_BY_CATEGORY_URL}?categoryId=${categoryId}`
      : RECIPES_BY_CATEGORY_URL;

  try {
    const result: RecipeItemSummaryDto[] = await apiClient.get(url);
    return result;
  } catch {
    throw new Error("Failed to fetch recipes by category.");
  }
};

export const searchRecipesByTitle = async (
  title: string,
): Promise<RecipeItemSummaryDto[]> => {
  const trimmedTitle = title.trim();
  const url = trimmedTitle
    ? `${RECIPES_SEARCH_URL}?title=${encodeURIComponent(trimmedTitle)}`
    : RECIPES_SEARCH_URL;

  try {
    const result: RecipeItemSummaryDto[] = await apiClient.get(url);
    return result;
  } catch {
    throw new Error("Failed to search recipes by title.");
  }
};

export const addCookingRecipe = async (
  data: AddCookingRecipeDto,
): Promise<string> => {
  try {
    const result: string = await apiClient.post(RECIPES_URL, data);
    return result;
  } catch {
    throw new Error("Failed to add cooking recipe.");
  }
};

export const deleteCookingRecipe = async (id: number): Promise<void> => {
  try {
    await apiClient.delete(`${RECIPES_URL}/${id}`);
  } catch {
    throw new Error("Failed to delete cooking recipe.");
  }
};
