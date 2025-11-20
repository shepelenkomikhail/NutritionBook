import { IngredientModel } from './IngredientModel.ts';

export interface ShowIngredientModel {
  ingredientDTO: IngredientModel;
  amount: number;
  unit: string;
}