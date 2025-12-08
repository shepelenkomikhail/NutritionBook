import {
  useCreateRecipeMutation, useMarkFavoriteRecipeMutation,
  useUnmarkFavoriteRecipeMutation, useUpdateRecipeMutation
} from '@api';
import type { RecipeModel, RecipePayload, FormIngredientModel } from '@models';
import { toast } from '@utils/toast.tsx';

type CreateArgs = {
  values: RecipeModel;
  mode: 'create';
};

type UpdateArgs = {
  values: RecipeModel;
  id: string;
  mode: 'update';
};

type FavoriteArgs = {
  id: string;
  mode: 'markFavorite';
};

type NotFavoriteArgs = {
  id: string;
  mode: 'unmarkFavorite';
};

type ExecuteArgs = CreateArgs | UpdateArgs | FavoriteArgs | NotFavoriteArgs;

export function useRecipeMutation() {
  const [createRecipe, createState] = useCreateRecipeMutation();
  const [updateRecipe, updateState] = useUpdateRecipeMutation();
  const [markFavoriteRecipe, markFavoriteState] = useMarkFavoriteRecipeMutation();
  const [unmarkFavoriteRecipe, unmarkFavoriteState] = useUnmarkFavoriteRecipeMutation();

  const mapPayload = (values: RecipeModel): RecipePayload => {
    const mappedIngredients =
      Array.isArray(values.ingredients)
        ? values.ingredients.map((ing: FormIngredientModel) => ({
          ingredientDTO: {
            id: ing.id ?? null,
            name: ing.name.trim(),
            isLiquid: ing.isLiquid ?? false,
          },
          amount: ing.amount,
          unit: ing.unit!.trim(),
        }))
        : [];

    const rows = (values.ingredients as unknown as FormIngredientModel[]) || [];
    const totals = rows.reduce(
      (acc, r) => {
        const unitMatchesBase = r.unit && r.uom && r.unit === r.uom;
        const factor = unitMatchesBase ? r.amount / 100 : 0;
        if (factor > 0) {
          acc.calories += (r.caloriesPer100 ?? 0) * factor;
          acc.proteins += (r.proteinsPer100 ?? 0) * factor;
          acc.carbs += (r.carbsPer100 ?? 0) * factor;
          acc.fats += (r.fatsPer100 ?? 0) * factor;
        }
        return acc;
      },
      { calories: 0, proteins: 0, carbs: 0, fats: 0 }
    );

    const round = (n: number) => Math.round(n * 100) / 100;

    const nutrients = [
      {
        name: 'Calories',
        unitOfMeasureDTO: { id: null, name: 'kcal', isLiquidMeasure: false },
        amount: round(totals.calories),
      },
      {
        name: 'Proteins',
        unitOfMeasureDTO: { id: null, name: 'g', isLiquidMeasure: false },
        amount: round(totals.proteins),
      },
      {
        name: 'Carbs',
        unitOfMeasureDTO: { id: null, name: 'g', isLiquidMeasure: false },
        amount: round(totals.carbs),
      },
      {
        name: 'Fats',
        unitOfMeasureDTO: { id: null, name: 'g', isLiquidMeasure: false },
        amount: round(totals.fats),
      },
    ];

    return {
      recipeDTO: {
        name: values.name.trim(),
        description: values.description?.trim(),
        instructions: values.instructions?.trim(),
        cookingTimeInMin: values.cookingTimeInMin,
        servings: values.servings,
        imageUrl: values.imageUrl,
      },
      ingredients: mappedIngredients,
      nutrients,
    };
  };

  const execute = async (args: ExecuteArgs) => {
    try {
      switch (args.mode) {
        case 'create': {
          const payload = mapPayload(args.values);
          console.log('Creating recipe with payload:', payload);
          await createRecipe(payload).unwrap();
          toast('Recipe created successfully!');
          break;
        }

        case 'update': {
          const payload = mapPayload(args.values);
          console.log('Updating recipe with payload:', { id: args.id, ...payload });
          await updateRecipe({ id: args.id, data: payload }).unwrap();
          toast('Recipe updated successfully!');
          break;
        }

        case 'markFavorite': {
          await markFavoriteRecipe({ id: args.id }).unwrap();
          toast('Recipe favorited successfully!');
          break;
        }

        case 'unmarkFavorite': {
          await unmarkFavoriteRecipe({ id: args.id }).unwrap();
          toast('Recipe unfavorited successfully!');
          break;
        }
      }
    } catch (error) {
      console.error(`Failed to ${args.mode} recipe:`, error);
      toast(`Failed to ${args.mode} recipe`);
    }
  };

  const isLoading =
    createState.isLoading ||
    updateState.isLoading ||
    markFavoriteState.isLoading ||
    unmarkFavoriteState.isLoading;

  const isError =
    createState.isError ||
    updateState.isError ||
    markFavoriteState.isError ||
    unmarkFavoriteState.isError;

  return { execute, isLoading, isError };
}