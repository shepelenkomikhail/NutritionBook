export interface IngredientNutritionInfoModel {
  name: string;
  isLiquid?: boolean;
  calories: number;
  proteins: number;
  carbs: number;
  fats: number;
  uom: string;
}