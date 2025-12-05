import { FormIngredientModel } from './FormIngredientModel.ts';

export interface RecipeModel {
  id?: string;
  name: string;
  description: string;
  imageUrl: string;
  ingredients: FormIngredientModel[];
  instructions: string;
  cookingTimeInMin: number;
  servings: number;
}