export type RecipePayload = {
  recipeDTO: {
    name: string;
    description?: string;
    instructions?: string;
    cookingTimeInMin: number;
    servings: number;
    imageUrl?: string;
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
  nutrients?: Array<{
    name: string;
    unitOfMeasureDTO: {
      id: string | null;
      name: string;
      isLiquidMeasure: boolean;
    };
    amount: number;
  }>;
};
