export interface RecipeModel {
  id?: string;
  name: string;
  description: string;
  ingredients: string;
  instructions: string;
  cookingTimeInMin: number;
  servings: number;
}