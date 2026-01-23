export const categoryKeys = {
  all: ["categories"] as const,
} as const;

export const recipeKeys = {
  all: ["recipes"] as const,
  byCategory: (categoryId?: number | null) =>
    ["recipes", "category", categoryId ?? "all"] as const,
  search: (title: string) => ["recipes", "search", title] as const,
} as const;

export const aiChatHistoryKeys = {
  all: ["ai-chat-history"] as const,
} as const;
