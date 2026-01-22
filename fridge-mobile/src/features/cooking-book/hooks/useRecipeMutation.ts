import { useMutation, useQueryClient } from "@tanstack/react-query";
import type { AddCookingRecipeDto } from "../models/add-cooking-recipe-dto";
import {
  addCookingRecipe,
  deleteCookingRecipe,
} from "../services/recipe-services";
import { recipeKeys } from "../../../shared/constants/query-key-factory";

export const useAddRecipe = () => {
  const queryClient = useQueryClient();
  const { mutate, isPending } = useMutation({
    mutationFn: (data: AddCookingRecipeDto) => addCookingRecipe(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: recipeKeys.all });
    },
  });

  return {
    mutate,
    isPending,
  };
};

export const useDeleteRecipe = () => {
  const queryClient = useQueryClient();
  const { mutate, isPending } = useMutation({
    mutationFn: (id: number) => deleteCookingRecipe(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: recipeKeys.all });
    },
  });

  return {
    mutate,
    isPending,
  };
};
