import { IngredientModel } from './IngredientModel';

export interface IngredientUnitOfMeasureModel {
  ingredient: IngredientModel;
  unitOfMeasure: string;
  amount: number;
  isBought: boolean;
}