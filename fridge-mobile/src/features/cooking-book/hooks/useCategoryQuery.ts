import { useQuery } from "@tanstack/react-query";
import { fetchAllCategories } from "../services/category-services";
import { categoryKeys } from "../../../shared/constants/query-key-factory";

export const useAllCategories = () => {
  const { data: categories, isLoading } = useQuery({
    queryKey: categoryKeys.all,
    queryFn: () => fetchAllCategories(),
  });

  return {
    categories: categories ?? [],
    isLoading,
  };
};
