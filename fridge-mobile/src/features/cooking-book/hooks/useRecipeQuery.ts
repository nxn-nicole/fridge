import { useQuery } from "@tanstack/react-query";
import {
  fetchRecipesByCategory,
  searchRecipesByTitle,
} from "../services/recipe-services";
import { recipeKeys } from "../../../shared/constants/query-key-factory";

export const useRecipesByCategory = (categoryId?: number | null) => {
  const { data: recipes, isLoading } = useQuery({
    queryKey: recipeKeys.byCategory(categoryId),
    queryFn: () => fetchRecipesByCategory(categoryId),
  });

  return {
    recipes: recipes ?? [],
    isLoading,
  };
};

export const useSearchRecipes = (title: string) => {
  const trimmedTitle = title.trim();
  const { data: recipes, isLoading } = useQuery({
    queryKey: recipeKeys.search(trimmedTitle),
    queryFn: () => searchRecipesByTitle(trimmedTitle),
    enabled: trimmedTitle.length > 0,
  });

  return {
    recipes: recipes ?? [],
    isLoading,
  };
};
