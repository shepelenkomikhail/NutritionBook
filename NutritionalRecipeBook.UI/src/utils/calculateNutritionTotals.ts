import type { FormIngredientModel } from '@models';

export type NutritionTotals = {
  calories: number;
  proteins: number;
  carbs: number;
  fats: number;
};

const round = (n: number) => Math.round(n * 100) / 100;

const UNIT_CONVERSIONS: Record<string, number> = {
  g: 1,
  kg: 1000,
  ml: 1,
  l: 1000,
  tsp: 5,
  tbsp: 15,
};

export function calculateNutritionTotals(
  rows: FormIngredientModel[] | undefined
): NutritionTotals {
  const list = Array.isArray(rows) ? rows : [];

  const acc = list.reduce<NutritionTotals>((acc, r) => {
    if (!r || !r.unit || !r.uom) return acc;

    const amount = r.amount;

    const unitFactor = UNIT_CONVERSIONS[r.unit] ?? null;
    if (unitFactor === null) return acc;

    const baseUom = r.uom;

    const isSolid = baseUom === 'g';
    const isLiquid = baseUom === 'ml';

    const isVolumeUnit = ['ml', 'l', 'tsp', 'tbsp'].includes(r.unit);
    const isWeightUnit = ['g', 'kg', 'tsp', 'tbsp'].includes(r.unit);

    if ((isSolid && !isWeightUnit) || (isLiquid && !isVolumeUnit)) {
      return acc;
    }

    const amountInBaseUnit = amount * unitFactor;

    const factor = amountInBaseUnit / 100;

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