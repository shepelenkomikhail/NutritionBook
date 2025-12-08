import { IngredientUnitOfMeasureModel } from './IngredientUnitOfMeasureModel.ts';

export interface UserIngredientUnitOfMeasuresModel {
  id?: string;
  userId?: string;
  ingredientUnitOfMeasures: IngredientUnitOfMeasureModel[];
}