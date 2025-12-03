import {
  useCreateRecipeMutation, useMarkFavoriteRecipeMutation,
  useUnmarkFavoriteRecipeMutation, useUpdateRecipeMutation
} from '@api';
import type { IngredientModel, RecipeModel, RecipePayload } from '@models';
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
        ? values.ingredients.map((ing: IngredientModel) => ({
          ingredientDTO: {
            id: ing.id ?? null,
            name: ing.name.trim(),
            isLiquid: ing.isLiquid ?? false,
          },
          amount: ing.amount,
          unit: ing.unit.trim(),
        }))
        : [];

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
    };
  };

  const execute = async (args: ExecuteArgs) => {
    try {
      switch (args.mode) {
        case 'create': {
          const payload = mapPayload(args.values);
          await createRecipe(payload).unwrap();
          toast('Recipe created successfully!');
          break;
        }

        case 'update': {
          const payload = mapPayload(args.values);
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