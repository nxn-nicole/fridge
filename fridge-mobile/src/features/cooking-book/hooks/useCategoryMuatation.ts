import { useMutation, useQueryClient } from "@tanstack/react-query";
import { addCategory as createNewCategory } from "../services/category-services";
import { categoryKeys } from "../../../shared/constants/query-key-factory";
import { AddCategoryDto } from "../models/add-category-dto";

export const useAddCategory = () => {
  const queryClient = useQueryClient();
  const { mutate, isPending } = useMutation({
    mutationFn: (data: AddCategoryDto) => createNewCategory(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: categoryKeys.all });
    },
  });

  return {
    addCategory: mutate,
    isAddingCategory: isPending,
  };
};
