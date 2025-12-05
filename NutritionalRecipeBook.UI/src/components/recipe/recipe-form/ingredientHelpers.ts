import type { FormInstance } from 'antd';
import type { FormIngredientModel, IngredientNutritionInfoModel, RecipeModel } from '@models';

export function mergeIngredientMeta(
  row: FormIngredientModel,
  ingMeta?: IngredientNutritionInfoModel
): FormIngredientModel {
  if (!ingMeta) return row;
  const patched: FormIngredientModel = {
    ...row,
    isLiquid: typeof row.isLiquid === 'boolean' ? row.isLiquid : (ingMeta.isLiquid ?? false),
    uom: row.uom ?? ingMeta.uom,
    caloriesPer100: row.caloriesPer100 ?? ingMeta.calories,
    proteinsPer100: row.proteinsPer100 ?? ingMeta.proteins,
    carbsPer100: row.carbsPer100 ?? ingMeta.carbs,
    fatsPer100: row.fatsPer100 ?? ingMeta.fats,
  };
  return patched;
}

export function prepareRecipeInitialValues(
  initialValues: RecipeModel | undefined,
  catalog: IngredientNutritionInfoModel[]
): RecipeModel | undefined {
  if (!initialValues) return undefined;

  const ingredients = Array.isArray(initialValues.ingredients)
    ? initialValues.ingredients.map((ing) =>
        mergeIngredientMeta(ing, catalog.find((i) => i.name === ing.name))
      )
    : [];

  return { ...initialValues, ingredients } as RecipeModel;
}

export function applyIngredientMetaToForm(
  form: FormInstance,
  rowIndex: number,
  ingMeta?: IngredientNutritionInfoModel
) {
  if (!ingMeta) return;
  form.setFieldValue(['ingredients', rowIndex, 'isLiquid'], ingMeta.isLiquid ?? false);
  form.setFieldValue(['ingredients', rowIndex, 'uom'], ingMeta.uom);
  form.setFieldValue(['ingredients', rowIndex, 'caloriesPer100'], ingMeta.calories);
  form.setFieldValue(['ingredients', rowIndex, 'proteinsPer100'], ingMeta.proteins);
  form.setFieldValue(['ingredients', rowIndex, 'carbsPer100'], ingMeta.carbs);
  form.setFieldValue(['ingredients', rowIndex, 'fatsPer100'], ingMeta.fats);
}
