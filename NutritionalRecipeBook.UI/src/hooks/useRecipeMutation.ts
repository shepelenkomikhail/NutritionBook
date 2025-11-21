import { useCreateRecipeMutation, useUpdateRecipeMutation } from '@api';
import type { IngredientModel, RecipeModel, RecipePayload } from '@models';
import { toast } from '@utils/toast.tsx';

export function useRecipeMutation() {
  const [createRecipe, createState] = useCreateRecipeMutation();
  const [updateRecipe, updateState] = useUpdateRecipeMutation();

  const execute = async (values: RecipeModel, id?: string, mode?: 'create' | 'update') => {
    const mappedIngredients = Array.isArray(values.ingredients)
        ? values.ingredients.map((ing: IngredientModel) => ({
        ingredientDTO: {
          id: id || null,
          name: ing.name.trim(),
          isLiquid: ing.isLiquid ?? false,
        },
        amount: ing.amount,
        unit: ing.unit.trim(),
      }))
      : [];

    const payload: RecipePayload = {
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

    console.log('EXECUTE')
    console.log('id - ', id);

    try {
      if (mode == "create") {
        console.log('Creating recipe with payload:', payload);
        await createRecipe(payload).unwrap();
        toast('Recipe created successfully!');
      } else if (mode == "update" && id) {
        console.log('Update recipe with id ', id);
        await updateRecipe({ id: id, data: payload }).unwrap();
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