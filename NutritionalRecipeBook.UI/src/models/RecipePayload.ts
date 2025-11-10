export interface RecipePayload {
  name: string;
  description?: string;
  instructions?: string;
  cookingTimeInMin: number;
  servings: number;
  ingredients: string;
}
