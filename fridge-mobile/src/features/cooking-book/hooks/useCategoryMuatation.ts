import { useMutation, useQueryClient } from "@tanstack/react-query";
import { addCategory } from "../services/category-services";
import type { AddCategoryDto } from "../models/add-category.dto";
import { categoryKeys } from "../../../shared/constants/query-key-factory";

export const useAddCategory = () => {
  const queryClient = useQueryClient();
  const { mutate, isPending } = useMutation({
    mutationFn: (data: AddCategoryDto) => addCategory(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: categoryKeys.all });
    },
  });

  return {
    mutate,
    isPending,
  };
};
