import { useGetIngredientsQuery } from '@api';
import type { IngredientNutritionInfoModel, UnitOfMeasureModel } from '@models';

type IngredientItemDTO = {
  ingredientNutrientApiDTO: {
    name: string;
    calories: number;
    proteins: number;
    carbs: number;
    fats: number;
    uom: string;
  };
  unitOfMeasureDTO?: UnitOfMeasureModel;
};

function isIngredientItemDTOArray(data: unknown): data is IngredientItemDTO[] {
  if (!Array.isArray(data)) return false;
  const first = data[0] as unknown;
  if (!first || typeof first !== 'object') return false;
  return 'ingredientNutrientApiDTO' in (first as Record<string, unknown>);
}

export function useIngredientsQuery() {
  const { data, isLoading, isFetching, isError, error } = useGetIngredientsQuery();

  let ingredients: IngredientNutritionInfoModel[] = [];
  const raw = data as unknown;

  if (isIngredientItemDTOArray(raw)) {
    ingredients = raw.map((item: IngredientItemDTO) => {
      const ing = item.ingredientNutrientApiDTO;
      const uom = item.unitOfMeasureDTO;
      const mapped: IngredientNutritionInfoModel = {
        name: ing.name,
        calories: ing.calories,
        proteins: ing.proteins,
        carbs: ing.carbs,
        fats: ing.fats,
        uom: ing.uom,
        isLiquid: typeof uom?.isLiquidMeasure === 'boolean' ? uom.isLiquidMeasure : undefined,
      };
      return mapped;
    });
  } else if (Array.isArray(raw)) {
    ingredients = raw as IngredientNutritionInfoModel[];
  }

  return {
    ingredients,
    isLoading: isLoading || isFetching,
    isError,
    error,
  } as const;
}
