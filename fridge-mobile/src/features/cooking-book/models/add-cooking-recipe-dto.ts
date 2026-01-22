import { IngredientInputDto } from "./ingredient-input-dto";
import { StepInputDto } from "./step-input-dto";

export type AddCookingRecipeDto = {
  title: string;
  categoryId?: number | null;
  ingredients: IngredientInputDto[];
  steps: StepInputDto[];
  pictureUrls?: string[] | null;
};
