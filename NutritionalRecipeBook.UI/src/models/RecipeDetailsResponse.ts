import type { RecipeDTO } from './RecipeDTO';
import type { ShowIngredientModel } from './ShowIngredientModel';

export interface RecipeDetailsResponse {
  recipeDTO: RecipeDTO;
  ingredients: ShowIngredientModel[];
}
