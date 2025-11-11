export type RecipePayload = {
  recipeDTO: {
    name: string;
    description?: string;
    instructions?: string;
    cookingTimeInMin: number;
    servings: number;
  };
  ingredients: Array<{
    ingredientDTO: {
      id: string | null;
      name: string;
      isLiquid: boolean;
    };
    amount: number;
    unit: string;
  }>;
};
