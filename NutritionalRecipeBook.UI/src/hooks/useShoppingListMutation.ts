import type { UserIngredientUnitOfMeasuresModel } from '@models';
import { toast } from '@utils/toast';
import { useCreateShoppingListMutation } from '@api';

const ML_PER_LITER = 1000;
const ML_PER_TSP = 5;
const ML_PER_TBSP = 15;

const G_PER_KG = 1000;
const G_PER_TSP = 5;
const G_PER_TBSP = 15;

function convertLiquidToMl(amount: number, unit: string): number {
  switch (unit) {
    case 'ml':
      return amount;
    case 'l':
      return amount * ML_PER_LITER;
    case 'tsp':
      return amount * ML_PER_TSP;
    case 'tbsp':
      return amount * ML_PER_TBSP;
    default:
      return amount;
  }
}

function convertSolidToGrams(amount: number, unit: string): number {
  switch (unit) {
    case 'g':
      return amount;
    case 'kg':
      return amount * G_PER_KG;
    case 'tsp':
      return amount * G_PER_TSP;
    case 'tbsp':
      return amount * G_PER_TBSP;
    default:
      return amount;
  }
}

export const useShoppingListMutation = () => {
  const [createShoppingList, state] = useCreateShoppingListMutation();

  const execute = async (payload: UserIngredientUnitOfMeasuresModel) => {
    try {
      const normalized: UserIngredientUnitOfMeasuresModel = {
        ...payload,
        ingredientUnitOfMeasures: payload.ingredientUnitOfMeasures.map((item) => {
          const isLiquid = item.ingredient.isLiquid;
          const originalAmount = Number(item.amount) || 0;
          const originalUnit = item.unitOfMeasure;

          if (isLiquid) {
            const amountInMl = convertLiquidToMl(originalAmount, originalUnit);
            return {
              ...item,
              amount: amountInMl,
              unitOfMeasure: 'ml',
            };
          }

          const amountInGrams = convertSolidToGrams(originalAmount, originalUnit);
          return {
            ...item,
            amount: amountInGrams,
            unitOfMeasure: 'g',
          };
        }),
      };

      await createShoppingList(normalized).unwrap();
      toast('Shopping list saved successfully');
    } catch (error) {
      console.error('Failed to save shopping list', error);
      toast('Failed to save shopping list');
      throw error;
    }
  };

  return {
    execute,
    isLoading: state.isLoading,
    isError: state.isError,
    isSuccess: state.isSuccess,
    error: state.error,
  };
};
