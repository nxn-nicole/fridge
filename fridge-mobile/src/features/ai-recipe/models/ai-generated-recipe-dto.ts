import type { IngredientInputDto } from "../../cooking-book/models/ingredient-input-dto";
import type { StepInputDto } from "../../cooking-book/models/step-input-dto";

export type AiGeneratedRecipeDto = {
  title: string;
  ingredients: IngredientInputDto[];
  steps: StepInputDto[];
};
