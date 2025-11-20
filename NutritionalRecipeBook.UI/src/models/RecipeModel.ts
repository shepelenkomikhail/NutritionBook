export interface RecipeModel {
  id?: string;
  name: string;
  description: string;
  imageUrl: string;
  ingredients: string;
  instructions: string;
  cookingTimeInMin: number;
  servings: number;
}