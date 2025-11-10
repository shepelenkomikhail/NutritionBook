import { useCreateRecipeMutation, useUpdateRecipeMutation } from '@api';
import type { IngredientModel, RecipeModel, RecipePayload } from '@models';
import { toast } from '@utils/toast.tsx';

export function useRecipeMutation(mode: 'create' | 'update', id?: string) {
  const [createRecipe, createState] = useCreateRecipeMutation();
  const [updateRecipe, updateState] = useUpdateRecipeMutation();

  const execute = async (values: RecipeModel & { id?: string }) => {
    const ingredientsArray: IngredientModel[] = Array.isArray(values.ingredients)
      ? values.ingredients
      : [];

    const mappedIngredients = ingredientsArray.map((ing) => ({
      ingredientDTO: {
        name: ing.name.trim(),
        isLiquid: ing.isLiquid ?? false,
      },
      amount: ing.amount,
      unit: ing.unit.trim(),
    }));

    const payload: RecipePayload = {
      name: values.name.trim(),
      description: values.description?.trim(),
      instructions: values.instructions?.trim(),
      cookingTimeInMin: values.cookingTimeInMin,
      servings: values.servings,
      ingredients: JSON.stringify(mappedIngredients),
    };

    try {
      if (mode === 'create') {
        await createRecipe(payload).unwrap();
        toast('Recipe created successfully!');
      } else if (mode === 'update' && id) {
        await updateRecipe({ id, data: payload }).unwrap();
        toast('Recipe updated successfully!');
      }
    } catch (error) {
      console.error(`Failed to ${mode} recipe:`, error);
      toast(`Failed to ${mode} recipe`);
    }
  };

  const isLoading = createState.isLoading || updateState.isLoading;
  const isError = createState.isError || updateState.isError;

  return { execute, isLoading, isError };
}