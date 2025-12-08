import type { FormIngredientModel } from '@models';

export type NutritionTotals = {
  calories: number;
  proteins: number;
  carbs: number;
  fats: number;
};

const round = (n: number) => Math.round(n * 100) / 100;

export function calculateNutritionTotals(rows: FormIngredientModel[] | undefined): NutritionTotals {
  const list = Array.isArray(rows) ? rows : [];
  const acc = list.reduce<NutritionTotals>((acc, r) => {
    const unitMatchesBase = !!r && !!r.unit && !!r.uom && r.unit === r.uom;
    const amount = typeof r?.amount === 'number' ? r.amount : 0;
    const factor = unitMatchesBase ? amount / 100 : 0;
    if (factor > 0) {
      acc.calories += (r.caloriesPer100 ?? 0) * factor;
      acc.proteins += (r.proteinsPer100 ?? 0) * factor;
      acc.carbs += (r.carbsPer100 ?? 0) * factor;
      acc.fats += (r.fatsPer100 ?? 0) * factor;
    }
    return acc;
  }, { calories: 0, proteins: 0, carbs: 0, fats: 0 });

  return {
    calories: round(acc.calories),
    proteins: round(acc.proteins),
    carbs: round(acc.carbs),
    fats: round(acc.fats),
  };
}
