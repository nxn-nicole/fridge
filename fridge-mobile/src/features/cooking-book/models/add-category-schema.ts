import z from "zod";

export const addCategorySchema = z.object({
  title: z.string().trim().min(1, "Category title is required."),
  color: z.string().min(1, "Category color is required."),
});
