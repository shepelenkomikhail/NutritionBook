export interface FormIngredientModel {
  id?: string;
  name: string;
  isLiquid?: boolean;
  amount: number;
  unit?: string;
  uom?: string;
  caloriesPer100?: number;
  proteinsPer100?: number;
  carbsPer100?: number;
  fatsPer100?: number;
}
